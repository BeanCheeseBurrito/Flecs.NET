#if Cpp_Systems_Pipeline

using Flecs.NET.Core;

using World world = World.Create();

// Create a system for moving an entity
world.Routine(
    filter: world.FilterBuilder()
        .With<Position>()
        .With<Velocity>(),
    routine: world.RoutineBuilder().Kind(Ecs.OnUpdate), // A phase orders a system in a pipeline
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);
        Column<Velocity> v = it.Field<Velocity>(2);

        p[i].X += v[i].X;
        p[i].Y += v[i].Y;
    }
);

// Create a system for printing the entity position
world.Routine(
    filter: world.FilterBuilder().With<Position>(),
    routine: world.RoutineBuilder().Kind(Ecs.PostUpdate),
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);
        Console.WriteLine($"{it.Entity(i).Name()}: ({p[i].X}, {p[i].Y})");
    }
);

// Create a few test entities for a Position, Velocity query
world.Entity("e1")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 1,  Y = 2 });

world.Entity("e2")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 3, Y = 4 });

// Run the default pipeline. This will run all systems ordered by their
// phase. Systems within the same phase are ran in declaration order. This
// function is usually called in a loop.
world.Progress();

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
// e1: (11, 22)
// e2: (13, 24)
