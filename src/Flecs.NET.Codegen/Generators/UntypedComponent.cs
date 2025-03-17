using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class UntypedComponent : GeneratorBase
{
    public override void Generate()
    {
        AddSource($"UntypedComponent.Id.g.cs", Id.GenerateExtensions(Type.UntypedComponent));
        AddSource($"UntypedComponent.Entity.g.cs", Entity.GenerateExtensions(Type.UntypedComponent));
        AddSource($"UntypedComponent.Entity.Observe.g.cs", Entity.GenerateObserveFunctions(Type.UntypedComponent));
    }
}
