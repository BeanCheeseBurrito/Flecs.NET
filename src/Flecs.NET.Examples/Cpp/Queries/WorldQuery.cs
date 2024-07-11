using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Queries_WorldQuery
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a few test entities for a Position, Velocity query
        world.Entity("e1")
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        world.Entity("e2")
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        // This entity will not match as it does not have Position, Velocity
        world.Entity("e3")
            .Set(new Position(25, 35));

        // World.Each is a quick way to run simple component queries.
        world.Each((Entity e, ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine($"{e}: ({p.X}, {p.Y})");
        });
    }
}

// Output:
// e1: (11, 22)
// e2: (13, 24)
