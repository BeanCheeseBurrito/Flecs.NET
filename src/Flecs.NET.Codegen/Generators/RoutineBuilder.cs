using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class RoutineBuilder : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            Generator.AddSource(postContext, "QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(Type.RoutineBuilder));

            for (int i = 0; i < Generator.GenericCount; i++)
                Generator.AddSource(postContext, $"NodeBuilder/T{i + 1}.g.cs", NodeBuilder.GenerateExtensions(i, Type.RoutineBuilder, Type.Routine));
        });
    }
}
