using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Reflection_WorldSerializeDeserialize
{
    public static void Main()
    {
        using World worldA = World.Create();
        worldA.Import<Move>();

        worldA.Entity("Entity 1")
            .Set(new Position(10, 20))
            .Set(new Velocity(1, -1));

        worldA.Entity("Entity 2")
            .Set(new Position(30, 40))
            .Set(new Velocity(-1, 1));

        // Serialize world to JSON.
        string json = worldA.ToJson();
        Console.WriteLine(json);
        // {"results":[{"ids":[["Move.Position"], ["Move.Velocity"], ["flecs.core.Identifier","flecs.core.Name"]], "entities":["Entity 1", "Entity 2"], "values":[[{"X":10, "Y":20}, {"X":30, "Y":40}], [{"X":1, "Y":-1}, {"X":-1, "Y":1}], 0]}]}

        // Create second world, import same module.
        using World worldB = World.Create();
        worldB.Import<Move>();

        // Deserialize JSON into second world
        worldB.FromJson(json);

        // Run system once for both worlds
        worldA.Progress();
        Console.WriteLine();
        worldB.Progress();
    }
}

// Register components and systems in a module. This excludes them by default
// from the serialized data, and makes it easier to import across worlds.
file struct Move : IFlecsModule
{
    public void InitModule(World world)
    {
        world.Component<Position>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Component<Velocity>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Routine<Position, Velocity>("Move")
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
                Console.WriteLine($"{e.Path()} moved to (X: {p.X}, Y: {p.Y})");
            });
    }
}

// Output:
// {"results":[{"ids":[["Move.Position"], ["Move.Velocity"], ["flecs.core.Identifier","flecs.core.Name"]], "entities":["Entity 1", "Entity 2"], "values":[[{"X":10, "Y":20}, {"X":30, "Y":40}], [{"X":1, "Y":-1}, {"X":-1, "Y":1}], 0]}]}
// Entity 1 moved to (X: 11, Y: 19)
// Entity 2 moved to (X: 29, Y: 41)
//
// Entity 1 moved to (X: 11, Y: 19)
// Entity 2 moved to (X: 29, Y: 41)
