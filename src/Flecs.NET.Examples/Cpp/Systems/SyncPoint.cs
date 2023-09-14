#if Cpp_Systems_SyncPoint

using Flecs.NET.Core;

using World world = World.Create();

// System that sets velocity using ecs_set for entities with Position.
// While systems are progressing, operations like ecs_set are deferred until
// it is safe to merge. By default this merge happens at the end of the
// frame, but we can annotate systems to give the scheduler more information
// about what it's doing, which allows it to insert sync points earlier.
//
// Note that sync points are never necessary/inserted for systems that write
// components provided by their signature, as these writes directly happen
// in the ECS storage and are never deferred.
//
// .InOutNone() for Position tells the scheduler that while we
// want to match entities with Position, we're not interested in reading or
// writing the component value.
world.Routine(
    name: "SetVelocity",
    filter: world.FilterBuilder()
        .With<Position>().InOutNone()
        .Write<Velocity>(),// Velocity is written, but shouldn't be matched
    callback: (Entity e) =>
    {
        e.Set(new Velocity { X = 1, Y = 2 });
    }
);

// This system reads Velocity, which causes the insertion of a sync point.
world.Routine(
    name: "Move",
    filter: world.FilterBuilder()
        .With<Position>()
        .With<Velocity>(),
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);
        Column<Velocity> v = it.Field<Velocity>(2);

        p[i].X += v[i].X;
        p[i].Y += v[i].Y;
    }
);

// Print resulting Position
world.Routine(
    name: "PrintPosition",
    filter: world.FilterBuilder().With<Position>(),
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);
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

// Run systems. Debug logging enables us to see the generated schedule
Ecs.Log.SetLevel(1);
world.Progress();
Ecs.Log.SetLevel(-1); // Restore so we don't get world cleanup logs

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
// info: pipeline rebuild
// info: | schedule: threading: 0, staging: 1:
// info: | | system SetVelocity
// info: | | merge
// info: | schedule: threading: 0, staging: 1:
// info: | | system Move
// info: | | system PrintPosition
// info: | | merge
// e1 (11, 22)
// e2 (11, 22)

// The "merge" lines indicate sync points.
//
// Removing '.Write<Velocity>()' from the system will remove the first
// sync point from the schedule.
