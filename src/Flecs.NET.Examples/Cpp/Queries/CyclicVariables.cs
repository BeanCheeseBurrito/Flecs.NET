// This example shows how a query may have terms with cyclic dependencies on
// variables.

using Flecs.NET.Core;

// Tags
file struct Likes;

public static class Cpp_Queries_CyclicVariables
{
    public static void Main()
    {
        using World world = World.Create();

        Entity bob = world.Entity("Bob");
        Entity alice = world.Entity("Alice");
        Entity john = world.Entity("John");
        Entity jane = world.Entity("Jane");

        bob.Add<Likes>(alice);
        alice.Add<Likes>(bob);
        john.Add<Likes>(jane);
        jane.Add<Likes>(john);
        bob.Add<Likes>(jane); // inserting a bit of drama

        // The following query will only return entities that have a cyclic Likes
        // relationship- that is they must both like each other.
        //
        // The equivalent query in the DSL is:
        //   Likes($x, $y), Likes($y, $x)
        //
        // This is also an example of a query where all sources are variables. By
        // default queries use the builtin "this" variable as subject, which is what
        // populates the entities array in the query result (accessed by the
        // Iter.Entity function).
        //
        // Because this query does not use This at all, the entities array will not
        // be populated, and it.Count() will always be 0.
        using Query q = world.QueryBuilder()
            .With<Likes>("$y").Src("$x")
            .With<Likes>("$x").Src("$y")
            .Build();

        // Lookup the index of the variables. This will let us quickly lookup their
        // values while we're iterating.
        int xVar = q.FindVar("x");
        int yVar = q.FindVar("y");

        // Because the query doesn't use the This variable we cannot use "each"
        // which iterates the entities array. Instead we can use iter like this:
        q.Each((Iter it, int i) =>
        {
            Entity x = it.GetVar(xVar);
            Entity y = it.GetVar(yVar);
            Console.WriteLine($"{x.Name()} likes {y.Name()}");
        });

        // Note that the query returns each pair twice. The reason for this is that
        // the goal of the query engine is to return all "facts" that are true
        // within the given constraints. Since we did not give it any constraints
        // that would favor a person being matched by X or Y, the query engine
        // returns both.
    }
}

// Output:
// Alice likes Bob
// John likes Jane
// Jane likes John
// Bob likes Alice
