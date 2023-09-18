// Queries can have wildcard terms that can match multiple instances of a
// relationship or relationship target.

#if Cpp_Queries_Wildcards

using Flecs.NET.Core;

using World world = World.Create();

// Create a query that matches edible components
Query q = world.Query(
    filter: world.FilterBuilder().With<Eats>(Ecs.Wildcard) // Change first argument to (Eats, *)
);

// Create a few entities that match the query
world.Entity("Bob")
    .SetFirst<Eats, Apples>(new Eats { Amount = 10 })
    .SetFirst<Eats, Pears>(new Eats { Amount = 5 });

world.Entity("Alice")
    .SetFirst<Eats, Apples>(new Eats { Amount = 4 });

// Iterate the query with a Iter. This makes it possible to inspect
// the pair that we are currently matched with.
q.Each((Iter it, int i) =>
{
    Column<Eats> eats = it.Field<Eats>(1);

    Entity e = it.Entity(i);
    Entity food = it.Pair(1).Second();

    Console.WriteLine($"{e} eats {eats[i].Amount} {food}");
});

public struct Eats
{
    public int Amount { get; set; }
}

public struct Apples { }
public struct Pears { }

#endif

// Output:
// Alice eats 4 Apples
// Bob eats 10 Apples
// Bob eats 5 Pears
