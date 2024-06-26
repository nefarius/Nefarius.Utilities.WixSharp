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
        Compilation compilation = context.Compilation;
        foreach (AssemblyIdentity? reference in compilation.ReferencedAssemblyNames)
        {
            IAssemblySymbol? assembly = compilation.GetAssemblyOrModuleSymbol(reference) as IAssemblySymbol;
            if (assembly != null && !string.IsNullOrEmpty(assembly.Locations.FirstOrDefault()?.SourceTree?.FilePath))
            {
                assemblyLocations.Add(assembly.Locations.First().SourceTree.FilePath);
            }
        }

        // Generate source code
        string source = GenerateSource(assemblyLocations);
        context.AddSource("DiscoveredTypes.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private string GenerateSource(IEnumerable<string> assemblyLocations)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("namespace Generated");
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