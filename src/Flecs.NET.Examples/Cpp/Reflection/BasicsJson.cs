#if Cpp_Reflection_BasicsJson

using Flecs.NET.Bindings;
using Flecs.NET.Core;

{
    using World world = World.Create();

    // Register component with reflection data.
    world.Component<Position>()
        .Member<float>("X")
        .Member<float>("Y");

    // Create entity with Position as usual.
    Entity e = world.Entity("Entity")
        .Set<Position>(new(10, 20));

    // Convert position component to JSON string
    ref Position reference = ref e.GetMut<Position>();
    Console.WriteLine(world.ToJson(ref reference)); // {X: 10, Y: 20}

    // Convert entity to JSON
    EntityToJsonDesc desc = world.EntityToJsonDesc()
        .Path()
        .Values();

    Console.WriteLine(e.ToJson(desc));

    return 0;
}

public struct Position(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

#endif

// Output:
// {"X":10, "Y":20}
// {"path":"Entity", "values":[{"X":10, "Y":20}]}
