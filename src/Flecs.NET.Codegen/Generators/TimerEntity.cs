using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class TimerEntity : GeneratorBase
{
    public override void Generate()
    {
        AddSource($"TimerEntity.Id.g.cs", Id.GenerateExtensions(Type.TimerEntity));
        AddSource($"TimerEntity.Entity.g.cs", Entity.GenerateExtensions(Type.TimerEntity));
    }
}
