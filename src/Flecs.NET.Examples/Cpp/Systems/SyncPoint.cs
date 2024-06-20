using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Systems_SyncPoint
{
    public static void Main()
    {
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
        world.Routine("SetVelocity")
            .With<Position>().InOutNone()
            .Write<Velocity>() // Velocity is written, but shouldn't be matched
            .Each((Entity e) =>
            {
                e.Set(new Velocity(1, 2));
            });

        // This system reads Velocity, which causes the insertion of a sync point.
        world.Routine<Position, Velocity>("Move")
            .Each((ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
            });

        // Print resulting Position
        world.Routine<Position>("PrintPosition")
            .Each((Entity e, ref Position p) =>
            {
                Console.WriteLine($"{e} ({p.X}, {p.Y})");
            });

        // Create a few test entities for a Position, Velocity query
        world.Entity("e1")
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        world.Entity("e2")
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        // Run systems. Debug logging enables us to see the generated schedule
        Ecs.Log.SetLevel(1);
        world.Progress();
        Ecs.Log.SetLevel(-1); // Restore so we don't get world cleanup logs
    }
}

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
