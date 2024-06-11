using Flecs.NET.Core;
using Flecs.NET.Utilities;

// Components
file record struct Position(float X, float Y);

public static unsafe class Cpp_Queries_Sorting
{
    public static void Main()
    {
        using World world = World.Create();

        // Create entities, set Position in random order
        Entity e = world.Entity().Set<Position>(new(1, 0));
        world.Entity().Set<Position>(new(6, 0));
        world.Entity().Set<Position>(new(2, 0));
        world.Entity().Set<Position>(new(5, 0));
        world.Entity().Set<Position>(new(4, 0));

        // Create a sorted system
        Routine sys = world.Routine<Position>()
            .OrderBy<Position>(ComparePosition)
            .Each((ref Position p) =>
            {
                Console.WriteLine($"({p.X}, {p.Y})");
            });

        // Create a sorted query
        using Query q = world.QueryBuilder<Position>()
            .OrderBy<Position>(ComparePosition)
            .Build();

        // Iterate query, print values of Position
        Console.WriteLine("-- First iteration");
        PrintQuery(q);

        // Change the value of one entity, invalidating the order
        e.Set<Position>(new(7, 0));

        // Iterate query again, printed values are still ordered
        Console.WriteLine("\n-- Second iteration");
        PrintQuery(q);

        // Create new entity to show that data is also sorted for system
        world.Entity().Set<Position>(new(3, 0 ));

        // Run system, output will be sorted
        Console.WriteLine("\n-- System iteration");
        sys.Run();
    }

    // Order by x member of Position */
    private static int ComparePosition(ulong e1, void* p1, ulong e2, void* p2)
    {
        Position* pos1 = (Position*)p1;
        Position* pos2 = (Position*)p2;
        return Macros.Bool(pos1->X > pos2->X) - Macros.Bool(pos1->X < pos2->X);
    }

    // Iterate query, printed values will be ordered
    private static void PrintQuery(Query q)
    {
        q.Each((ref Position p) =>
        {
            Console.WriteLine($"({p.X}, {p.Y})");
        });
    }
}

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
