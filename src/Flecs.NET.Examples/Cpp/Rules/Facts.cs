// This example shows how to use rules for testing facts. A fact is a query that
// has no variable elements. Consider a regular ECS query like this:
//   Position, Velocity
//
// When written out in full, this query looks like:
//   Position($This), Velocity($This)
//
// "This" is a (builtin) query variable that is unknown before we evaluate the
// query. Therefore this query does not test a fact, we can't know which values
// This will assume.
//
// An example of a fact-checking query is:
//   IsA(Cat, Animal)
//
// This is a fact: the query has no elements that are unknown before evaluating
// the query. A rule that checks a fact does not return entities, but will
// instead return the reasons why a fact is true (if it is true).

// TODO: Finish this
#if Cpp_Rules_Facts

using Flecs.NET.Core;

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

// Create a rule that checks if two entities like each other. By itself this
// rule is not a fact, but we can use it to check facts by populating both
// of its variables.
//
// The equivalent query in the DSL is:
//  Likes($X, $Y), Likes($Y, $X)
//
// Instead of using variables we could have created a rule that referred the
// entities directly, but then we would have to create a rule for each
// fact, vs reusing a single rule for multiple facts.
Rule friends = world.Rule(
    filter: world.FilterBuilder()
        .With<Likes>("$Y").Src("$X")
        .With<Likes>("$X").Src("$Y")
);

int xVar = friends.FindVar("X");
int yVar = friends.FindVar("Y");

// Check a few facts

friends.Iter((Iter it) =>
{
    Entity x = it.GetVar(xVar);
    Entity y = it.GetVar(yVar);
    Console.WriteLine($"{x.Name()} likes {y.Name()}");
});

friends.Destruct();

public struct Likes { }

#endif

// Output:
// Alice likes Bob
// John likes Jane
// Jane likes John
// Bob likes Alice
