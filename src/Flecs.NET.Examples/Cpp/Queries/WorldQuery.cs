#if Cpp_Queries_WorldQuery

using Flecs.NET.Core;

using World world = World.Create();

// Create a few test entities for a Position, Velocity query
world.Entity("e1")
    .Set(new Position { X = 10, Y = 20 });

world.Entity("e2")
    .Set(new Position { X = 12, Y = 22 });

// This entity will not match as it does not have Position, Velocity
world.Entity("e3")
    .Set(new Position { X = 25, Y = 35 });

// Ad hoc queries are bit slower to iterate than Query, but are
// faster to create, and in most cases require no allocations. Under the
// hood this API uses Filter, which can be used directly for more
// complex queries.
world.Each((Entity e, ref Position p) =>
{
    p.X += 10;
    p.Y += 10;
    Console.WriteLine($"{e}: ({p.X}, {p.Y})");
});

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Velocity
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// Output:
// e1: (20, 30)
// e2: (22, 32)
// e3: (35, 45)
