#if Cpp_Reflection_WorldSerializeDeserialize

using Flecs.NET.Core;

{
    using World worldA = World.Create();
    worldA.Import<Move>();

    worldA.Entity("Entity 1")
        .Set<Position>(new(10, 20))
        .Set<Velocity>(new(1, -1));

    worldA.Entity("Entity 2")
        .Set<Position>(new(30, 40))
        .Set<Velocity>(new(-1, 1));

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

    return 0;
}

public struct Position(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

public struct Velocity(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

// Register components and systems in a module. This excludes them by default
// from the serialized data, and makes it easier to import across worlds.
public struct Move : IFlecsModule
{
    public void InitModule(ref World world)
    {
        world.Component<Position>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Component<Velocity>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Routine(
            name: "Move",
            filter: world.FilterBuilder<Position, Velocity>(),
            callback: (Entity e, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
                Console.WriteLine($"{e.Path()} moved to (X: {p.X}, Y: {p.Y})");
            }
        );
    }
}

#endif

// Output:
// {"results":[{"ids":[["Move.Position"], ["Move.Velocity"], ["flecs.core.Identifier","flecs.core.Name"]], "entities":["Entity 1", "Entity 2"], "values":[[{"X":10, "Y":20}, {"X":30, "Y":40}], [{"X":1, "Y":-1}, {"X":-1, "Y":1}], 0]}]}
// Entity 1 moved to (X: 11, Y: 19)
// Entity 2 moved to (X: 29, Y: 41)
//
// Entity 1 moved to (X: 11, Y: 19)
// Entity 2 moved to (X: 29, Y: 41)
