// This example shows how to use queries for testing facts. A fact is a query that
// has no variable elements. Consider a regular ECS query like this:
//   Position, Velocity
//
// When written out in full, this query looks like:
//   Position($this), Velocity($this)
//
// "this" is a (builtin) query variable that is unknown before we evaluate the
// query. Therefore this query does not test a fact, we can't know which values
// This will assume.
//
// An example of a fact-checking query is:
//   IsA(Cat, Animal)
//
// This is a fact: the query has no elements that are unknown before evaluating
// the query. A query that checks a fact does not return entities, but will
// instead return the reasons why a fact is true (if it is true).

using Flecs.NET.Core;

// Tags
file struct Likes;

public static class Cpp_Queries_Facts
{
    public static void Main()
    {
        using World world = World.Create();

        Entity bob = world.Entity("Bob");
        Entity alice = world.Entity("Alice");
        Entity jane = world.Entity("Jane");
        Entity john = world.Entity("John");

        bob.Add<Likes>(alice);
        alice.Add<Likes>(bob);
        jane.Add<Likes>(john);
        john.Add<Likes>(jane);

        bob.Add<Likes>(john); // bit of drama

        // Create a query that checks if two entities like each other. By itself this
        // query is not a fact, but we can use it to check facts by populating both
        // of its variables.
        //
        // The equivalent query in the DSL is:
        //  Likes($x, $y), Likes($y, $x)
        //
        // Instead of using variables we could have created a query that referred the
        // entities directly, but then we would have to create a query for each
        // fact, vs reusing a single query for multiple facts.
        using Query friends = world.QueryBuilder()
                .With<Likes>("$y").Src("$x")
                .With<Likes>("$x").Src("$y")
                .Build();

        int xVar = friends.FindVar("x");
        int yVar = friends.FindVar("y");

        // Check a few facts

        Console.Write("Are Bob and Alice friends? ");
        Console.WriteLine(
            friends.Iter()
                .SetVar(xVar, bob)
                .SetVar(yVar, alice)
                .IsTrue()
        );

        Console.Write("Are Bob and John friends? ");
        Console.WriteLine(
            friends.Iter()
                .SetVar(xVar, bob)
                .SetVar(yVar, john)
                .IsTrue()
        );

        Console.Write("Are Jane and John friends? ");
        Console.WriteLine(
            friends.Iter()
                .SetVar(xVar, jane)
                .SetVar(yVar, john)
                .IsTrue()
        );

        // It doesn't matter who we assign to X or Y. After the variables are
        // substituted, either yields a fact that is true.
        Console.Write("Are John and Jane friends? ");
        Console.WriteLine(
            friends.Iter()
                .SetVar(xVar, john)
                .SetVar(yVar, jane)
                .IsTrue()
        );
    }
}

// Output:
// Are Bob and Alice friends? True
// Are Bob and John friends? False
// Are Jane and John friends? True
// Are John and Jane friends? True
