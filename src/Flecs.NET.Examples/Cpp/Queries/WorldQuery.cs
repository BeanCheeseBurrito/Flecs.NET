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
            .Set<Position>(new(10, 20))
            .Set<Velocity>(new(1, 2));

        world.Entity("e2")
            .Set<Position>(new(10, 20))
            .Set<Velocity>(new(3, 4));

        // This entity will not match as it does not have Position, Velocity
        world.Entity("e3")
            .Set<Position>(new(25, 35));

        // Ad hoc queries are bit slower to iterate than Query, but are
        // faster to create, and in most cases require no allocations. Under the
        // hood this API uses Filter, which can be used directly for more
        // complex queries.
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
