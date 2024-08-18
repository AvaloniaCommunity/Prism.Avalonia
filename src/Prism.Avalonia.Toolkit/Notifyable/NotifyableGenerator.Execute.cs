using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
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
    }
}
