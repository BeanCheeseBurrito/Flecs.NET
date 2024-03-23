using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

// Tags
file struct Npc;

public static class Cpp_Queries_With
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query for Position, Npc. By adding the Npc component using the
        // "With" method, the component is not a part of the query type, and as a
        // result does not become part of the function signatures of each and iter.
        // This is useful for things like tags, which because they don't have a
        // value are less useful to pass to the Each/Iter functions as argument.
        Query q = world.Query<Position, Npc>();

        // Create a few test entities for the Position, Npc query
        world.Entity("e1")
            .Set<Position>(new(10, 20))
            .Add<Npc>();

        world.Entity("e2")
            .Set<Position>(new(10, 20))
            .Add<Npc>();

        // This entity will not match as it does not have Position, Npc
        world.Entity("e3")
            .Set<Position>(new(10, 20));


        // Note how the Npc tag is not part of the each signature
        q.Each((Entity e, ref Position p) =>
        {
            Console.WriteLine($"{e}: ({p.X}, {p.Y})");
        });
    }
}

// Output:
// e1: (10, 20)
// e2: (10, 20)
