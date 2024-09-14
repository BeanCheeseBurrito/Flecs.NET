using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Systems_Basics
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a system for Position, Velocity. Systems are like queries (see
        // queries) with a function that can be ran or scheduled (see pipeline).

        System<Position, Velocity> system = world.System<Position, Velocity>()
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
                Console.WriteLine($"{e} ({p.X}, {p.Y})");
            });

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
        system.Run();
    }
}

// Output:
// e1 (11, 22)
// e2 (13, 24)
