#if Cpp_Queries_Sorting

using Flecs.NET.Core;

unsafe
{
    // Order by x member of Position */
    int ComparePosition(ulong e1, void* p1, ulong e2, void* p2)
    {
        Position* pos1 = (Position*)p1;
        Position* pos2 = (Position*)p2;
        return Macros.Bool(pos1->X > pos2->X) - Macros.Bool(pos1->X < pos2->X);
    }

    // Iterate query, printed values will be ordered
    void PrintQuery(Query q)
    {
        q.Each((Iter it, int i) =>
        {
            Column<Position> p = it.Field<Position>(1);
            Console.WriteLine($"({p[i].X}, {p[i].Y})");
        });
    }

    using World world = World.Create();

    // Create entities, set Position in random order
    Entity e = world.Entity().Set(new Position { X = 1, Y = 0 });
    world.Entity().Set(new Position { X = 6, Y = 0 });
    world.Entity().Set(new Position { X = 2, Y = 0 });
    world.Entity().Set(new Position { X = 5, Y = 0 });
    world.Entity().Set(new Position { X = 4, Y = 0 });

    // Create a sorted system
    Routine sys = world.Routine(
        filter: world.FilterBuilder().With<Position>(),
        query: world.QueryBuilder().OrderBy<Position>(ComparePosition),
        callback: (Iter it, int i) =>
        {
            Column<Position> p = it.Field<Position>(1);
            Console.WriteLine($"({p[i].X}, {p[i].Y})");
        }
    );

    // Create a sorted query
    Query q = world.Query(
        filter: world.FilterBuilder().With<Position>(),
        query: world.QueryBuilder().OrderBy<Position>(ComparePosition)
    );

    // Iterate query, print values of Position
    Console.WriteLine("-- First iteration");
    PrintQuery(q);

    // Change the value of one entity, invalidating the order
    e.Set(new Position { X = 7, Y = 0 });

    // Iterate query again, printed values are still ordered
    Console.WriteLine("\n-- Second iteration");
    PrintQuery(q);

    // Create new entity to show that data is also sorted for system
    world.Entity().Set(new Position { X = 3, Y = 0 });

    // Run system, output will be sorted
    Console.WriteLine("\n-- System iteration");
    sys.Run();
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// TODO: Come back once generic order-by is added.

// Output:
// -- First iteration
// (1, 0)
// (2, 0)
// (4, 0)
// (5, 0)
// (6, 0)
//
// -- Second iteration
// (2, 0)
// (4, 0)
// (5, 0)
// (6, 0)
// (7, 0)
//
// -- System iteration
// (2, 0)
// (3, 0)
// (4, 0)
// (5, 0)
// (6, 0)
// (7, 0)
