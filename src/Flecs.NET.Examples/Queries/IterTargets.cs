// This example shows how to iterate matching targets of a relationship without
// iterating the same entity multiple times.
//
// When creating a (Relationship, *) query, the query will return one result per
// matching relationship pair, which returns the same entity multiple times.
// This example uses a (Relationship, _) query which at most returns a single
// result per matching pair, and a targets() function that iterates the targets
// for the current entity.

using Flecs.NET.Core;

// Tags
file struct Eats;
file struct Pizza;
file struct Salad;

public static unsafe class Queries_IterTargets
{
    public static void Main()
    {
        using World world = World.Create();

        world.Entity("Bob")
            .Add<Eats, Pizza>()
            .Add<Eats, Salad>();

        // Ecs.Any ensures that only a single result
        // is returned per entity, as opposed to
        // Ecs.Wildcard which returns a result per
        // matched pair.
        using Query query = world.QueryBuilder()
            .With<Eats>(Ecs.Any)
            .Build();

        query.Each((Iter it, int row) =>
        {
            Entity e = it.Entity(row);
            Console.WriteLine($"{e} eats:");

            it.Targets(0, (Entity tgt) =>
            {
                Console.WriteLine($" - {tgt}");
            });
        });
    }
}

// Output:
// Bob eats:
//  - Pizza
//  - Salad
