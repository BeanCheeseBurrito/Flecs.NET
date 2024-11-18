using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Component : GeneratorBase
{
    public override void Generate()
    {
        AddSource($"Component.Id.g.cs", Id.GenerateExtensions(Type.Component));
        AddSource($"Component.Entity.g.cs", Entity.GenerateExtensions(Type.Component));
    }
}
