using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

// Tags
file struct Npc;

public static class Cpp_Queries_Without
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query for Position, !Npc. By adding the Npc component using the
        // "Without" method, the component is not a part of the query type, and as a
        // result does not become part of the function signatures of each and iter.
        // This is useful for things like tags, which because they don't have a
        // value are less useful to pass to the Each/Iter functions as argument.
        //
        // The without method is short for:
        //   .With<Npc>().Not()
        using Query q = world.QueryBuilder<Position>()
            .Without<Npc>()
            .Build();

        // Create a few test entities for the Position query
        world.Entity("e1")
            .Set<Position>(new(10, 20));

        world.Entity("e2")
            .Set<Position>(new(10, 20));

        // This entity will not match as it has Npc
        world.Entity("e3")
            .Set<Position>(new(10, 20))
            .Add<Npc>();

        // Note how the Npc tag is not part of the each signature
        q.Each((Entity e, ref Position p) =>
        {
            Console.WriteLine($"{e}: ({p.X}, {p.Y})");
        });
    }
}

// Output:
//  e1: (10, 20)
//  e2: (10, 20)
