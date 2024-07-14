using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);
file record struct Mass(float Value);

public static class Reflection_QueryToJson
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
        using Query query = world.Query<Position, Velocity>();

        // Serialize query to JSON. Note that this works for any iterable object.
        Console.WriteLine(query.Iter().ToJson());
        // {"ids":[["Position"], ["Velocity"]], "results":[{"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["A", "B"], "values":[[{"X":10, "Y":20}, {"X":20, "Y":30}], [{"X":1, "Y":2}, {"X":2, "Y":3}]]}, {"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["C", "D"], "values":[[{"X":30, "Y":40}, {"X":30, "Y":40}], [{"X":3, "Y":4}, {"X":4, "Y":5}]]}]}
    }
}

// Output:
// {"ids":[["Position"], ["Velocity"]], "results":[{"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["A", "B"], "values":[[{"X":10, "Y":20}, {"X":20, "Y":30}], [{"X":1, "Y":2}, {"X":2, "Y":3}]]}, {"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["C", "D"], "values":[[{"X":30, "Y":40}, {"X":30, "Y":40}], [{"X":3, "Y":4}, {"X":4, "Y":5}]]}]}
