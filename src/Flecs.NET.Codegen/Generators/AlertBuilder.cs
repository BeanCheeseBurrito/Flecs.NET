using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class AlertBuilder : GeneratorBase
{
    public override void Generate()
    {
        AddSource("AlertBuilder.QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(Type.AlertBuilder));
    }
}
