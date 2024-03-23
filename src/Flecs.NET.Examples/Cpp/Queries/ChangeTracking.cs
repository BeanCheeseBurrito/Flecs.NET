// Queries have a builtin mechanism for tracking changes per matched table. This
// is a cheap way of eliminating redundant work, as many entities can be skipped
// with a single check.
//
// This example shows how to use change tracking in combination with a few other
// techniques, like using prefabs to store a single dirty state for multiple
// entities and instanced queries.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Dirty(bool Value);

public static class Cpp_Queries_ChangeTracking
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query that just reads a component. We'll use this query for
        // change tracking. Change tracking for a query is automatically enabled
        // when Query.Changed() is called.
        // Each query has its own private dirty state which is reset only when the
        // query is iterated.
        Query qRead = world.QueryBuilder()
            .With<Position>().In()
            .Build();

        // Create a query that writes the component based on a Dirty state.
        Query qWrite = world.QueryBuilder()
            .With<Dirty>().Up().In() // Only match Dirty from prefab
            .With<Position>()        // Instanced iteration is faster (see example)
            .Instanced()
            .Build();

        // Create two prefabs with a Dirty component. We can use this to share a
        // single Dirty value for all entities in a table.
        Entity p1 = world.Prefab("p1").Set<Dirty>(new(false));
        Entity p2 = world.Prefab("p2").Set<Dirty>(new(true));

        // Create instances of p1 and p2. Because the entities have different
        // prefabs, they end up in different tables.
        world.Entity("e1").IsA(p1)
            .Set<Position>(new(10, 20));

        world.Entity("e2").IsA(p1)
            .Set<Position>(new(30, 40));

        world.Entity("e3").IsA(p2)
            .Set<Position>(new(40, 50));

        world.Entity("e4").IsA(p2)
            .Set<Position>(new(60, 70));

        // We can use the Changed() function on the query to check if any of the
        // tables it is matched with has changed. Since this is the first time that
        // we check this and the query is matched with the tables we just created,
        // the function will return true.
        Console.WriteLine($"qRead.Changed(): {qRead.Changed()}");

        // The changed state will remain true until we have iterated each table.
        qRead.Iter((Iter it) =>
        {
            // With the it.Changed() function we can check if the table we're
            // currently iterating has changed since last iteration.
            // Because this is the first time the query is iterated, all tables
            // will show up as changed.
            Console.WriteLine($"it.Changed() for table [{it.Type()}]: {it.Changed()}");
        });

        // Now that we have iterated all tables, the dirty state is reset.
        Console.WriteLine($"qRead.Changed(): {qRead.Changed()}");
        Console.WriteLine();

        // Iterate the write query. Because the Position term is InOut (default)
        // iterating the query will write to the dirty state of iterated tables.
        qWrite.Iter((Iter it, Field<Dirty> dirty, Field<Position> p) =>
        {
            Console.WriteLine($"Iterate table [{it.Type()}]");

            // Because we enforced that Dirty is a shared component, we can check
            // a single value for the entire table.
            if (!dirty[0].Value)
            {
                // If the dirty flag is false, skip the table. This way the table's
                // dirty state is not updated by the query.
                it.Skip();
                Console.WriteLine($"it.Skip() for table [{it.Type()}]");
                return;
            }

            // For all other tables the dirty state will be set.
            foreach (int i in it)
            {
                p[i].X++;
                p[i].Y++;
            }
        });

        // One of the tables has changed, so q_read.changed() will return true
        Console.WriteLine();
        Console.WriteLine($"qRead.Changed()): {qRead.Changed()}");

        // When we iterate the read query, we'll see that one table has changed.
        qRead.Iter((Iter it) =>
        {
            Console.WriteLine($"it.Changed() for table [{it.Type()}]: {it.Changed()}");
        });
    }
}

// Output:
// qRead.Changed(): True
// it.Changed() for table [Position, (Identifier,Name), (IsA,p1)]: True
// it.Changed() for table [Position, (Identifier,Name), (IsA,p2)]: True
// qRead.Changed(): False
//
// Iterate table [Position, (Identifier,Name), (IsA,p1)]
// it.Skip() for table [Position, (Identifier,Name), (IsA,p1)]
// Iterate table [Position, (Identifier,Name), (IsA,p2)]
//
// qRead.Changed()): True
// it.Changed() for table [Position, (Identifier,Name), (IsA,p1)]: False
// it.Changed() for table [Position, (Identifier,Name), (IsA,p2)]: True
