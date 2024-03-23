using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);
file record struct Mass(float Value);

public static class Cpp_Reflection_QueryToJson
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

        world.Entity("A").Set<Position>(new(10, 20)).Set<Velocity>(new(1, 2));
        world.Entity("B").Set<Position>(new(20, 30)).Set<Velocity>(new(2, 3));
        world.Entity("C").Set<Position>(new(30, 40)).Set<Velocity>(new(3, 4)).Set<Mass>(new(10));
        world.Entity("D").Set<Position>(new(30, 40)).Set<Velocity>(new(4, 5)).Set<Mass>(new(20));

        // Query for components.
        Query query = world.Query<Position, Velocity>();

        // Serialize query to JSON. Note that this works for any iterable object,
        // including filters & rules.
        Console.WriteLine(query.Iter().ToJson());
        // {"ids":[["Position"], ["Velocity"]], "results":[{"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["A", "B"], "values":[[{"X":10, "Y":20}, {"X":20, "Y":30}], [{"X":1, "Y":2}, {"X":2, "Y":3}]]}, {"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["C", "D"], "values":[[{"X":30, "Y":40}, {"X":30, "Y":40}], [{"X":3, "Y":4}, {"X":4, "Y":5}]]}]}
    }
}

// Output:
// {"ids":[["Position"], ["Velocity"]], "results":[{"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["A", "B"], "values":[[{"X":10, "Y":20}, {"X":20, "Y":30}], [{"X":1, "Y":2}, {"X":2, "Y":3}]]}, {"ids":[["Position"], ["Velocity"]], "sources":[0, 0], "entities":["C", "D"], "values":[[{"X":30, "Y":40}, {"X":30, "Y":40}], [{"X":3, "Y":4}, {"X":4, "Y":5}]]}]}
