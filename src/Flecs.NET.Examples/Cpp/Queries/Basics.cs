#if Cpp_Queries_Basics

using Flecs.NET.Core;

using World world = World.Create();

// Create a query for Position, Velocity. Queries are the fastest way to
// iterate entities as they cache results.
Query q = world.Query(
    filter: world.FilterBuilder()
        .With<Position>()
        .With<Velocity>()
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

// The next lines show the different ways in which a query can be iterated.

// The Each() function iterates each entity individually and accepts an
// entity argument:
q.Each((Entity e) =>
{
    Console.WriteLine(e.ToString());
});

// Each also accepts iter + index (for the iterated entity) arguments
// currently being iterated. An Iter has lots of information on what
// is being iterated, which is demonstrated in the "Iter" example.
q.Each((Iter it, int i) =>
{
    Column<Position> p = it.Field<Position>(1);
    Column<Velocity> v = it.Field<Velocity>(2);

    p[i].X += v[i].X;
    p[i].Y += v[i].Y;

    Console.WriteLine($"({p[i].X}, {p[i].Y})");
});

// Iter is a bit more verbose, but allows for more control over how entities
// are iterated as it provides multiple entities in the same callback.
q.Iter((Iter it) =>
{
    Column<Position> p = it.Field<Position>(1);
    Column<Velocity> v = it.Field<Velocity>(2);

    foreach (int i in it)
    {
        p[i].X += v[i].X;
        p[i].Y += v[i].Y;

        Console.WriteLine($"{it.Entity(i)}: ({p[i].X}, {p[i].Y})");
    }
});

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
// e1
// e2
// (11, 22)
// (13, 24)
// e1: (12, 24)
// e2: (16, 28)
