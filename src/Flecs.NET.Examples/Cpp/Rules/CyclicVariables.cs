// This example shows how a rule may have terms with cyclic dependencies on
// variables.

#if Cpp_Rules_CyclicVariables

using Flecs.NET.Core;

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

// The following rule will only return entities that have a cyclic Likes
// relationship- that is they must both like each other.
//
// The equivalent query in the DSL is:
//   Likes($X, $Y), Likes($Y, $X)
//
// This is also an example of a query where all sources are variables. By
// default queries use the builtin "This" variable as subject, which is what
// populates the entities array in the query result (accessed by the
// Iter.Entity function).
//
// Because this query does not use This at all, the entities array will not
// be populated, and it.count() will always be 0.
Rule r = world.Rule(
    filter: world.FilterBuilder()
        .With<Likes>("$Y").Src("$X")
        .With<Likes>("$X").Src("$Y")
);

// Lookup the index of the variables. This will let us quickly lookup their
// values while we're iterating.
int xVar = r.FindVar("X");
int yVar = r.FindVar("Y");

// Because the query doesn't use the This variable we cannot use "each"
// which iterates the entities array. Instead we can use iter like this:
r.Iter((Iter it) =>
{
    Entity x = it.GetVar(xVar);
    Entity y = it.GetVar(yVar);
    Console.WriteLine($"{x.Name()} likes {y.Name()}");
});

// Note that the rule returns each pair twice. The reason for this is that
// the goal of the rule engine is to return all "facts" that are true
// within the given constraints. Since we did not give it any constraints
// that would favor a person being matched by X or Y, the rule engine
// returns both.

r.Destruct();

public struct Likes { }

#endif

// Output:
// Alice likes Bob
// John likes Jane
// Jane likes John
// Bob likes Alice
