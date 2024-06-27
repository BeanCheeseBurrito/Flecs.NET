// Systems can be created with a custom run function that takes control over the
// entire iteration. By default, a system is invoked once per matched table,
// which means the function can be called multiple times per frame. In some
// cases that's inconvenient, like when a system has things it needs to do only
// once per frame. For these use cases, the run callback can be used which is
// called once per frame per system.

using Flecs.NET.Bindings;
using Flecs.NET.Core;
using Flecs.NET.Utilities;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static unsafe class Cpp_Systems_CustomRunner
{
    public static void Main()
    {
        using World world = World.Create();

        Routine routine = world.Routine<Position, Velocity>()
            // Forward each result from the run callback to the each callback.
            .Run((Iter it, Action<Iter> callback) =>
            {
                Console.WriteLine("Move begin");

                // Walk over the iterator, forward to the system callback
                while (it.Next())
                    callback(it);

                Console.WriteLine("Move end");
            })
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
                Console.WriteLine($"{e}: ({p.X}, {p.Y})");
            });

        // Create a few test entities for a Position, Velocity query
        world.Entity("e1")
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        world.Entity("e2")
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        // This entity will not match as it does not have Position, Velocity
        world.Entity("e3")
            .Set(new Position(10, 20));

        // Run the system
        routine.Run();
    }
}

// Output:
// Move begin
// e1: (11, 22)
// e2: (13, 24)
// Move end
