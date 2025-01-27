using Microsoft.CodeAnalysis;

namespace Prism.Avalonia.Toolkit.Helpers;

internal static class DiagnosticDescriptors
{
    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a generated property created with <c>[ObservableAsProperty]</c> would cause conflicts with other generated members.
    /// <para>
    /// Format: <c>"The field {0}.{1} cannot be used to generate an observable property, as its name or type would cause conflicts with other generated members"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidObservableAsPropertyError = new DiagnosticDescriptor(
        id: "PTK0001",
        title: "Invalid generated property declaration",
        messageFormat: "The field {0}.{1} cannot be used to generate an observable As property, as its name or type would cause conflicts with other generated members",
        category: typeof(NotifyableGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The fields annotated with [NotifyableGenerator] cannot result in a property name or have a type that would cause conflicts with other generated members.",
        helpLinkUri: "https://www.reactiveui.net/errors/RXUISG0018");
}
