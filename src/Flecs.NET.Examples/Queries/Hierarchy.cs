using System.Runtime.CompilerServices;
using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

// Tags
file struct Local;
file struct Global;

public static class Queries_Hierarchy
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a hierarchy. For an explanation see the entities/hierarchy example
        Entity sun = world.Entity("Sun")
            .Add<Position, Global>()
            .Set<Position, Local>(new Position(1, 1));

        world.Entity("Mercury")
            .ChildOf(sun)
            .Add<Position, Global>()
            .Set<Position, Local>(new Position(1, 1));

        world.Entity("Venus")
            .ChildOf(sun)
            .Add<Position, Global>()
            .Set<Position, Local>(new Position(2, 2));

        Entity earth = world.Entity("Earth")
            .ChildOf(sun)
            .Add<Position, Global>()
            .Set<Position, Local>(new Position(3, 3));

        world.Entity("Moon")
            .ChildOf(earth)
            .Add<Position, Global>()
            .Set<Position, Local>(new Position(0.1f, 0.1f));

        // Create a hierarchical query to compute the global position from the
        // local position and the parent position.
        using Query<Position, Position, Position> q = world.QueryBuilder<Position, Position, Position>()
            // Modify terms from template to make sure the query selects the
            // local, world and parent position components.
            .TermAt(0).Second<Local>()  // Self local position
            .TermAt(1).Second<Global>() // Self global position
            .TermAt(2).Second<Global>() // Parent global position

            .TermAt(2)              // Extend the 2nd query argument to select it from the parent
                .Parent().Cascade() // Get from the parent, in breadth-first order (cascade)
                .Optional()         // Make term component optional, so we also match the root (sun)

            .Build();

        q.Each((ref Position selfLocal, ref Position selfGlobal, ref Position parentGlobal) =>
        {
            selfGlobal.X = selfLocal.X;
            selfGlobal.Y = selfLocal.Y;

            if (!Unsafe.IsNullRef(ref parentGlobal))
            {
                selfGlobal.X += parentGlobal.X;
                selfGlobal.Y += parentGlobal.Y;
            }
        });

        // Print world positions for all entities that have (Position, Global)
        world.Each<Position, Global>((Entity e, ref Position p) =>
        {
            Console.WriteLine($"{e}: ({p.X}, {p.Y})");
        });
    }
}

// Output:
// Sun: (1, 1)
// Mercury: (2, 2)
// Venus: (3, 3)
// Earth: (4, 4)
// Moon: (4.1, 4.1)
