#if Cpp_Queries_FindEntity

using Flecs.NET.Core;

{
    using World world = World.Create();

    world.Entity("e1").Set(new Position { X = 10, Y = 20 });
    world.Entity("e2").Set(new Position { X = 20, Y = 30 });

    // Create a simple query for component Position
    Query q = world.Query(
        filter: world.FilterBuilder<Position>()
    );

    // Find the entity for which Position.x is 20
    Entity e = q.Find((ref Position p) => p.X == 20);

    if (e != 0)
        Console.WriteLine($"Found entity {e.Path()}");
    else
        Console.WriteLine($"No entity found");
}

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }
}

#endif
