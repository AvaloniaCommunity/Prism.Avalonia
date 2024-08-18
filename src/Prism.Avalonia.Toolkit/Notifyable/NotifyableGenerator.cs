using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Prism.Avalonia.Toolkit;

/// <summary>Source generator for notifiable property.</summary>
[Generator(LanguageNames.CSharp)]
public partial class NotifyableGenerator
{
    private const string NotifyableAttribute = "Prism.Avalonia.Generators.NotifyableAttribute";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(callback =>
        callback.AddSource($"{NotifyableAttribute}.g.cs", SourceText.From(AttributeDefinitions.NotifyableAttribute, Encoding.UTF8)));
    }
}
