// Systems can be created with a custom run function that takes control over the
// entire iteration. By default, a system is invoked once per matched table,
// which means the function can be called multiple times per frame. In some
// cases that's inconvenient, like when a system has things it needs to do only
// once per frame. For these use cases, the run callback can be used which is
// called once per frame per system.

#if Cpp_Systems_CustomRunner

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

unsafe
{
    using World world = World.Create();

    Routine routine = world.Routine(
        filter: world.FilterBuilder()
            .With<Position>()
            .With<Velocity>(),
        routine: world.RoutineBuilder()
            // The run function has a signature that accepts a C iterator. By
            // forwarding the iterator to it->callback, the each function of the
            // system is invoked.
            .Run((ecs_iter_t* it) =>
            {
                Console.WriteLine("Move begin");

                delegate* unmanaged<ecs_iter_t*, void> callback = (delegate* unmanaged<ecs_iter_t*, void>)it->callback;

                // Walk over the iterator, forward to the system callback
                while (ecs_iter_next(it) == Macros.True)
                    callback(it);

                Console.WriteLine("Move end");
            }),
        callback: (Iter it, int i) =>
        {
            Column<Position> p = it.Field<Position>(1);
            Column<Velocity> v = it.Field<Velocity>(2);

            p[i].X += v[i].X;
            p[i].Y += v[i].Y;

            Console.WriteLine($"{it.Entity(i).Name()}: ({p[i].X}, {p[i].Y})");
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
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

struct Velocity
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// Output:
// Move begin
// e1: (11, 22)
// e2: (13, 24)
// Move end
