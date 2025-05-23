using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Reflection_BasicsJson
{
    public static void Main()
    {
        using World world = World.Create();

        // Register component with reflection data.
        world.Component<Position>()
            .Member<float>("X")
            .Member<float>("Y");

        // Create entity with Position as usual.
        Entity e = world.Entity("Entity")
            .Set(new Position(10, 20));

        // Convert position component to JSON string
        ref Position reference = ref e.Ensure<Position>();
        Console.WriteLine(world.ToJson(ref reference)); // {X: 10, Y: 20}

        Console.WriteLine(e.ToJson());
    }
}

// Output:
// {"X":10, "Y":20}
// {"name":"Entity", "components":{"Position":{"X":10, "Y":20}}}
