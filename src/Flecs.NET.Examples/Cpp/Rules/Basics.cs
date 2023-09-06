#if Cpp_Rules_Basics

using Flecs.NET.Core;

using World world = World.Create();

Entity apples = world.Entity("Apples").Add<Healthy>();
Entity salad = world.Entity("Salad").Add<Healthy>();
Entity burgers = world.Entity("Burgers");
Entity pizza = world.Entity("Pizza");
Entity chocolate = world.Entity("Chocolate");

world.Entity("Bob")
    .Add<Eats>(apples)
    .Add<Eats>(burgers)
    .Add<Eats>(pizza);

world.Entity("Alice")
    .Add<Eats>(salad)
    .Add<Eats>(chocolate)
    .Add<Eats>(apples);

// Here we're creating a rule that in the query DSL would look like this:
//   Eats($This, $Food), Healthy($Food)
//
// Rules are similar to queries, but support more advanced features. This
// example shows how the basics of how to use rules & variables.
Rule r = world.Rule(
    // Identifiers that start with _ are query variables. Query variables
    // are like wildcards, but enforce that the entity substituted by the
    // wildcard is the same across terms.
    //
    // For example, in this query it is not guaranteed that the entity
    // substituted by the * for Eats is the same as for Healthy:
    //   (Eats, *), Healthy(*)
    //
    // By replacing * with _Food, both terms are constrained to use the
    // same entity.
    filter: world.FilterBuilder()
        .With<Eats>("$Food")
        .With<Healthy>().Src("$Food")
);

// Lookup the index of the variable. This will let us quickly lookup its
// value while we're iterating.
int foodVar = r.FindVar("Food");

// Iterate the rule
r.Iter((Iter it) =>
{
    foreach (int i in it)
        Console.WriteLine($"{it.Entity(i).Name()} eats {it.GetVar(foodVar).Name()}");
});

// Rules need to be explicitly deleted.
r.Destruct();

public struct Eats { }
public struct Healthy { }

#endif

// Output:
// Bob eats Apples
// Alice eats Apples
// Alice eats Salad
