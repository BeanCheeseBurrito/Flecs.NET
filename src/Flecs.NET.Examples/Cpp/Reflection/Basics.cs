using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Cpp_Reflection_Basics
{
    public static void Main()
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
    }
}

// Output:
// {X: 10, Y: 20}
