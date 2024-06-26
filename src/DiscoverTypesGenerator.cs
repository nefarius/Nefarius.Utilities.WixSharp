using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Nefarius.Utilities.WixSharp;

[Generator]
public sealed class DiscoverTypesGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this generator
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Create a list to store assembly locations
        HashSet<string> assemblyLocations = new HashSet<string>();

        // Get all the referenced assemblies
        foreach (MetadataReference? reference in context.Compilation.References)
        {
            PortableExecutableReference? metadataReference = reference as PortableExecutableReference;
            if (metadataReference != null && !string.IsNullOrEmpty(metadataReference.FilePath))
            {
                assemblyLocations.Add(metadataReference.FilePath);
            }
        }

        // Generate source code
        string source = GenerateSource(assemblyLocations);
        context.AddSource("DiscoveredAssemblies.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private string GenerateSource(IEnumerable<string> assemblyLocations)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("namespace Nefarius.Utilities.WixSharp");
        sb.AppendLine("{");
        sb.AppendLine("    public static class DiscoveredAssemblies");
        sb.AppendLine("    {");
        sb.AppendLine("        public static List<string> Locations { get; } = new List<string>");
        sb.AppendLine("        {");

        foreach (string? location in assemblyLocations)
        {
            sb.AppendLine($"            \"{location}\",");
        }

        sb.AppendLine("        };");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}