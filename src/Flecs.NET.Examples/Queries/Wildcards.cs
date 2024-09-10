// Queries can have wildcard terms that can match multiple instances of a
// relationship or relationship target.

using Flecs.NET.Core;

// Components
file record struct Eats(int Amount);

// Tags
file struct Apples;
file struct Pears;

public static class Queries_Wildcards
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query that matches edible components
        using Query<Eats> q = world.QueryBuilder<Eats>()
            .TermAt(0).Second(Ecs.Wildcard) // Change first argument to (Eats, *)
            .Build();

        // Create a few entities that match the query
        world.Entity("Bob")
            .Set<Eats, Apples>(new Eats(10))
            .Set<Eats, Pears>(new Eats(5));

        world.Entity("Alice")
            .Set<Eats, Apples>(new Eats(4));

        // Iterate the query with a Iter. This makes it possible to inspect
        // the pair that we are currently matched with.
        q.Each((Iter it, int i, ref Eats eats) =>
        {
            Entity e = it.Entity(i);
            Entity food = it.Pair(0).Second();

            Console.WriteLine($"{e} eats {eats.Amount} {food}");
        });
    }
}

// Output:
// Alice eats 4 Apples
// Bob eats 10 Apples
// Bob eats 5 Pears
