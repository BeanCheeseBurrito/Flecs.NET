using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Alert : GeneratorBase
{
    public override void Generate()
    {
        AddSource($"Alert.Id.g.cs", Id.GenerateExtensions(Type.Alert));
        AddSource($"Alert.Entity.g.cs", Entity.GenerateExtensions(Type.Alert));
        AddSource($"Alert.Entity.Observe.g.cs", Entity.GenerateObserveFunctions(Type.Alert));
    }
}
