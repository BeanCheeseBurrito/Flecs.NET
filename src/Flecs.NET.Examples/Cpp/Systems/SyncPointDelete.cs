#if Cpp_Systems_SyncPointDelete

using Flecs.NET.Core;

using World world = World.Create();

// This example shows how to annotate systems that delete entities, in a way
// that allows the scheduler to correctly insert sync points. See the
// SyncPoint example for more details on sync points.
//
// While annotating a system for a delete operation follows the same
// design as other operations, one key difference is that a system often
// does not know which components a to be deleted entity has. This makes it
// impossible to annotate the system in advance for specific components.
//
// To ensure the scheduler is still able to insert the correct sync points,
// a system can use a wildcard to indicate that any component could be
// modified by the system, which forces the scheduler to insert a sync.

// Basic move system.
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

// Delete entities when p.X >= 3. Add wildcard annotation to indicate any
// component could be written by the system.
world.Routine(
    name: "DeleteEntity",
    filter: world.FilterBuilder()
        .With<Position>()
        .Write(Ecs.Wildcard),
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);

        if (p[i].X >= 3)
        {
            Entity e = it.Entity(i);
            Console.WriteLine($"Delete entity {e.Name()}");
            e.Destruct();
        }
    }
);

// Print resulting Position. Note that this system will never print entities
// that have been deleted by the previous system.
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
    .Set(new Position { X = 0, Y = 0 })
    .Set(new Velocity { X = 1, Y = 2 });

world.Entity("e2")
     .Set(new Position { X = 1, Y = 2 })
     .Set(new Velocity { X = 1, Y = 2 });

// Run systems. Debug logging enables us to see the generated schedule
Ecs.Log.SetLevel(1);
while (world.Progress())
{
    if (world.Count<Position>() == 0)
        break; // No more entities left with Position
}
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
// info: | | system Move
// info: | | system DeleteEntity
// info: | | merge
// info: | schedule: threading: 0, staging: 1:
// info: | | system PrintPosition
// info: | | merge
// e1 (1, 2)
// e2 (2, 4)
// Delete entity e2
// e1 (2, 4)
// Delete entity e1

// Removing the wildcard annotation from the DeleteEntity system will
// remove the first sync point.
//
// Note how after both entities are deleted, all three systems are
// deactivated. This happens when there are no matching entities for a
// system. A deactivated system is not ran by the scheduler, which reduces
// overhead.
