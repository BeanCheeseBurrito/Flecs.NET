// Same example as QueryToJson, but with customized serializer parameters.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);
file record struct Mass(float Value);

public static class Reflection_QueryToCustomJson
{
    public static void Main()
    {
        using World world = World.Create();

        // Register components with reflection data.
        world.Component<Position>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Component<Velocity>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Component<Mass>()
            .Member<float>("Value");

        world.Entity("A").Set(new Position(10, 20)).Set(new Velocity(1, 2));
        world.Entity("B").Set(new Position(20, 30)).Set(new Velocity(2, 3));
        world.Entity("C").Set(new Position(30, 40)).Set(new Velocity(3, 4)).Set(new Mass(10));
        world.Entity("D").Set(new Position(30, 40)).Set(new Velocity(4, 5)).Set(new Mass(20));

        // Query for components.
        using Query<Position, Velocity> query = world.Query<Position, Velocity>();

        // Serialize query to JSON. Only serialize entity names.
        Console.WriteLine(query.Iter().ToJson(new IterToJsonDesc
        {
            SerializeFields = false
        }));
        // Iterator returns 2 sets of results, one for each table.
    }
}

// Output:
// {"results":[{"name":"A"}, {"name":"B"}, {"name":"C"}, {"name":"D"}]}
