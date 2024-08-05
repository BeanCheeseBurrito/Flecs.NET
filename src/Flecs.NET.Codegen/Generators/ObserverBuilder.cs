using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class ObserverBuilder : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            Generator.AddSource(postContext, "QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(nameof(ObserverBuilder)));

            for (int i = 0; i < Generator.GenericCount; i++)
                Generator.AddSource(postContext, $"NodeBuilder/T{i + 1}.g.cs", NodeBuilder.GenerateExtensions(i, Type.Observer));
        });
    }
}
