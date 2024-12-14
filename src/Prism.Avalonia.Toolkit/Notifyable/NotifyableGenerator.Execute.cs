using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Prism.Avalonia.Toolkit;

/// <summary>NotifyableGenerator.</summary>
public partial class NotifyableGenerator
{
    private const string GeneratedCode = "global::System.CodeDom.Compiler.GeneratedCode";
    private const string ExcludeFromCodeCoverage = "global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage";

    internal static class Execute
    {
        internal static bool GetFieldInfoFromClass(
            FieldDeclarationSyntax fieldSyntax,
            IFieldSymbol fieldSymbol,
            SemanticModel semanticModel,
            CancellationToken token,
            [NotNullWhen(true)] out PropertyInfo? propertyInfo,
            out ImmutableArray<DiagnosticInfo> diagnostics)
        {
            using var builder = ImmutableArrayBuilder<DiagnosticInfo>.Rent();

            // Validate the target type
            if (!IsTargetTypeValid(fieldSymbol))
            {
                builder.Add(
                    InvalidObservableAsPropertyError,
                    fieldSymbol,
                    fieldSymbol.ContainingType,
                    fieldSymbol.Name);

                propertyInfo = null;
                diagnostics = builder.ToImmutable();

                return false;
            }

            // Get the property type and name
            var typeNameWithNullabilityAnnotations = fieldSymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations();
            var fieldName = fieldSymbol.Name;
            var propertyName = GetGeneratedPropertyName(fieldSymbol);

            // Check for name collisions
            if (fieldName == propertyName)
            {
                builder.Add(
                    ReactivePropertyNameCollisionError,
                    fieldSymbol,
                    fieldSymbol.ContainingType,
                    fieldSymbol.Name);

                propertyInfo = null;
                diagnostics = builder.ToImmutable();

                // If the generated property would collide, skip generating it entirely. This makes sure that
                // users only get the helpful diagnostic about the collision, and not the normal compiler error
                // about a definition for "Property" already existing on the target type, which might be confusing.
                return false;
            }

            token.ThrowIfCancellationRequested();

            using var forwardedAttributes = ImmutableArrayBuilder<AttributeInfo>.Rent();

            // Gather attributes info
            foreach (var attributeData in fieldSymbol.GetAttributes())
            {
                token.ThrowIfCancellationRequested();

                // Track the current attribute for forwarding if it is a validation attribute
                if (attributeData.AttributeClass?.InheritsFromFullyQualifiedMetadataName("System.ComponentModel.DataAnnotations.ValidationAttribute") == true)
                {
                    forwardedAttributes.Add(AttributeInfo.Create(attributeData));
                }

                // Track the current attribute for forwarding if it is a Json Serialization attribute
                if (attributeData.AttributeClass?.InheritsFromFullyQualifiedMetadataName("System.Text.Json.Serialization.JsonAttribute") == true)
                {
                    forwardedAttributes.Add(AttributeInfo.Create(attributeData));
                }

                // Also track the current attribute for forwarding if it is of any of the following types:
                if (attributeData.AttributeClass?.HasOrInheritsFromFullyQualifiedMetadataName("System.ComponentModel.DataAnnotations.UIHintAttribute") == true ||
                    attributeData.AttributeClass?.HasOrInheritsFromFullyQualifiedMetadataName("System.ComponentModel.DataAnnotations.ScaffoldColumnAttribute") == true ||
                    attributeData.AttributeClass?.HasFullyQualifiedMetadataName("System.ComponentModel.DataAnnotations.DisplayAttribute") == true ||
                    attributeData.AttributeClass?.HasFullyQualifiedMetadataName("System.ComponentModel.DataAnnotations.EditableAttribute") == true ||
                    attributeData.AttributeClass?.HasFullyQualifiedMetadataName("System.ComponentModel.DataAnnotations.KeyAttribute") == true ||
                    attributeData.AttributeClass?.HasFullyQualifiedMetadataName("System.Runtime.Serialization.DataMemberAttribute") == true ||
                    attributeData.AttributeClass?.HasFullyQualifiedMetadataName("System.Runtime.Serialization.IgnoreDataMemberAttribute") == true)
                {
                    forwardedAttributes.Add(AttributeInfo.Create(attributeData));
                }
            }

            token.ThrowIfCancellationRequested();

            // Gather explicit forwarded attributes info
            foreach (var attributeList in fieldSyntax.AttributeLists)
            {
                // Only look for attribute lists explicitly targeting the (generated) property. Roslyn will normally emit a
                // CS0657 warning (invalid target), but that is automatically suppressed by a dedicated diagnostic suppressor
                // that recognizes uses of this target specifically to support [ObservableAsProperty].
                if (attributeList.Target?.Identifier is not SyntaxToken(SyntaxKind.PropertyKeyword))
                {
                    continue;
                }

                token.ThrowIfCancellationRequested();

                foreach (var attribute in attributeList.Attributes)
                {
                    // Roslyn ignores attributes in an attribute list with an invalid target, so we can't get the AttributeData as usual.
                    // To reconstruct all necessary attribute info to generate the serialized model, we use the following steps:
                    //   - We try to get the attribute symbol from the semantic model, for the current attribute syntax. In case this is not
                    //     available (in theory it shouldn't, but it can be), we try to get it from the candidate symbols list for the node.
                    //     If there are no candidates or more than one, we just issue a diagnostic and stop processing the current attribute.
                    //     The returned symbols might be method symbols (constructor attribute) so in that case we can get the declaring type.
                    //   - We then go over each attribute argument expression and get the operation for it. This will still be available even
                    //     though the rest of the attribute is not validated nor bound at all. From the operation we can still retrieve all
                    //     constant values to build the AttributeInfo model. After all, attributes only support constant values, typeof(T)
                    //     expressions, or arrays of either these two types, or of other arrays with the same rules, recursively.
                    //   - From the syntax, we can also determine the identifier names for named attribute arguments, if any.
                    // There is no need to validate anything here: the attribute will be forwarded as is, and then Roslyn will validate on the
                    // generated property. Users will get the same validation they'd have had directly over the field. The only drawback is the
                    // lack of IntelliSense when constructing attributes over the field, but this is the best we can do from this end anyway.
                    if (!semanticModel.GetSymbolInfo(attribute, token).TryGetAttributeTypeSymbol(out var attributeTypeSymbol))
                    {
                        builder.Add(
                            InvalidPropertyTargetedAttributeOnObservableAsPropertyField,
                            attribute,
                            fieldSymbol,
                            attribute.Name);

                        continue;
                    }

                    var attributeArguments = attribute.ArgumentList?.Arguments ?? Enumerable.Empty<AttributeArgumentSyntax>();

                    // Try to extract the forwarded attribute
                    if (!AttributeInfo.TryCreate(attributeTypeSymbol, semanticModel, attributeArguments, token, out var attributeInfo))
                    {
                        builder.Add(
                            InvalidPropertyTargetedAttributeExpressionOnObservableAsPropertyField,
                            attribute,
                            fieldSymbol,
                            attribute.Name);

                        continue;
                    }

                    forwardedAttributes.Add(attributeInfo);
                }
            }

            token.ThrowIfCancellationRequested();

            // Get the nullability info for the property
            GetNullabilityInfo(
                fieldSymbol,
                semanticModel,
                out var isReferenceTypeOrUnconstraindTypeParameter,
                out var includeMemberNotNullOnSetAccessor);

            token.ThrowIfCancellationRequested();

            propertyInfo = new PropertyInfo(
                typeNameWithNullabilityAnnotations,
                fieldName,
                propertyName,
                isReferenceTypeOrUnconstraindTypeParameter,
                includeMemberNotNullOnSetAccessor,
                forwardedAttributes.ToImmutable());

            diagnostics = builder.ToImmutable();

            return true;
        }
    }
}
