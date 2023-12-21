#if Cpp_Reflection_Basics

using Flecs.NET.Core;

{
    using World world = World.Create();

    // Register component with reflection data.
    world.Component<Position>()
        .Member<float>("X")
        .Member<float>("Y");

    // Create entity with Position as usual.
    Entity e = world.Entity()
        .Set<Position>(new(10, 20));

    // Convert position component to flecs expression string.
    ref Position reference = ref e.GetMut<Position>();
    Console.WriteLine(world.ToExpr(ref reference)); // {X: 10, Y: 20}

    return 0;
}

public struct Position(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

#endif

// Output:
// {X: 10, Y: 20}
