using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Queries_Basics
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query for Position, Velocity. Queries are the fastest way to
        // iterate entities as they cache results.
        Query q = world.Query<Position, Velocity>();

        // Create a few test entities for a Position, Velocity query
        world.Entity("e1")
            .Set<Position>(new(10, 20))
            .Set<Velocity>(new(1, 2 ));

        world.Entity("e2")
            .Set<Position>(new(10, 20))
            .Set<Velocity>(new(3, 4));

        // This entity will not match as it does not have Position, Velocity
        world.Entity("e3")
            .Set<Position>(new(10, 20));

        // The next lines show the different ways in which a query can be iterated.

        // The Each() function iterates each entity individually and accepts an
        // entity argument plus arguments for each query component:
        q.Each((Entity e, ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine(e.ToString());
        });

        // You can omit the Entity argument if it's not needed:
        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine($"({p.X}, {p.Y})");
        });

        // Each also accepts iter + index (for the iterated entity) arguments
        // currently being iterated. An Iter has lots of information on what
        // is being iterated, which is demonstrated in the "Iter" example.
        q.Each((Iter it, int i, ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine($"{it.Entity(i)}: ({p.X}, {p.Y})");
        });

        // Iter is a bit more verbose, but allows for more control over how entities
        // are iterated as it provides multiple entities in the same callback.
        q.Iter((Iter it, Column<Position> p, Column<Velocity> v) =>
        {

            foreach (int i in it)
            {
                p[i].X += v[i].X;
                p[i].Y += v[i].Y;
                Console.WriteLine($"{it.Entity(i)}: ({p[i].X}, {p[i].Y})");
            }
        });
    }
}

// Output:
// e1
// e2
// (12, 24)
// (16, 28)
// e1: (13, 26)
// e2: (19, 32)
// e1: (14, 28)
// e2: (22, 36)
