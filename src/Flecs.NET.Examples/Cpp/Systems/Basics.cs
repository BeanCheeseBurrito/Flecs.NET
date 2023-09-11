#if Cpp_Systems_Basics

using Flecs.NET.Core;

using World world = World.Create();

// Create a system for Position, Velocity. Systems are like queries (see
// queries) with a function that can be ran or scheduled (see pipeline).
Routine routine = world.Routine(
    filter: world.FilterBuilder()
        .With<Position>()
        .With<Velocity>(),
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);
        Column<Velocity> v = it.Field<Velocity>(2);

        p[i].X += v[i].X;
        p[i].Y += v[i].Y;
        Console.WriteLine($"{it.Entity(i).Name()} ({p[i].X}, {p[i].Y})");
    }
);
// Create a few test entities for a Position, Velocity query
world.Entity("e1")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 1, Y = 2 });

world.Entity("e2")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 3, Y = 4 });
// This entity will not match as it does not have Position, Velocity
world.Entity("e3")
    .Set(new Position { X = 10, Y = 20 });

// Run the system
routine.Run();

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
// e1 (11, 22)
// e2 (13, 24)
