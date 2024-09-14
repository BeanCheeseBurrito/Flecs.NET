// A group iterator iterates over a single group of a grouped query (see the
// GroupBy example for more details). This can be useful when an application
// may need to match different entities based on the context of the game, such
// as editor mode, day/night, inside/outside or location in the world.
//
// One example is that of an open game which is divided up into world
// cells. Even though a world may contain many entities, only the entities in
// cells close to the player need to be processed.
//
// Instead of creating a cached query per world cell, which could be expensive
// as there are more caches to keep in sync, applications can create a single
// query grouped by world cell, and use group iterators to only iterate the
// necessary cells.

using Flecs.NET.Core;

// A world cell relationship with four cells
file struct WorldCell;
file struct Cell00;
file struct Cell01;
file struct Cell10;
file struct Cell11;

// Npc tags
file struct Npc;
file struct Merchant;
file struct Soldier;
file struct Beggar;
file struct Mage;

public static class Queries_GroupIter
{
    public static void Main()
    {
        using World world = World.Create();

        // Create npc's in world cell 0_0
        world.Entity().Add<WorldCell, Cell00>()
            .Add<Merchant>()
            .Add<Npc>();
        world.Entity().Add<WorldCell, Cell00>()
            .Add<Merchant>()
            .Add<Npc>();

        // Create npc's in world cell 0_1
        world.Entity().Add<WorldCell, Cell01>()
            .Add<Beggar>()
            .Add<Npc>();
        world.Entity().Add<WorldCell, Cell01>()
            .Add<Soldier>()
            .Add<Npc>();

        // Create npc's in world cell 1_0
        world.Entity().Add<WorldCell, Cell10>()
            .Add<Mage>()
            .Add<Npc>();
        world.Entity().Add<WorldCell, Cell10>()
            .Add<Beggar>()
            .Add<Npc>();

        // Create npc's in world cell 1_1
        world.Entity().Add<WorldCell, Cell11>()
            .Add<Soldier>()
            .Add<Npc>();

        using Query q = world.QueryBuilder()
            .With<Npc>()
            .GroupBy<WorldCell>()
            .Build();

        // Iterate all tables
        Console.WriteLine("All tables:");
        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Entity group = it.World().Entity(it.GroupId());
                Console.WriteLine($" - Group {group.Path()}: Table [{it.Table()}]");
            }
        });

        Console.WriteLine();

        // Only iterate entities in cell 10
        Console.WriteLine("Tables for cell 1_0:");
        q.SetGroup<Cell10>().Run((Iter it) =>
        {
            while (it.Next())
            {
                Entity group = it.World().Entity(it.GroupId());
                Console.WriteLine($" - Group {group.Path()}: Table [{it.Table()}]");
            }
        });
    }
}

// Output:
// All tables:
//  - Group Cell00: Table [Merchant, Npc, (WorldCell,Cell00)]
//  - Group Cell01: Table [Npc, Beggar, (WorldCell,Cell01)]
//  - Group Cell01: Table [Npc, Soldier, (WorldCell,Cell01)]
//  - Group Cell10: Table [Npc, Mage, (WorldCell,Cell10)]
//  - Group Cell10: Table [Npc, Beggar, (WorldCell,Cell10)]
//  - Group Cell11: Table [Npc, Soldier, (WorldCell,Cell11)]
//
// Tables for cell 10:
//  - Group Cell10: Table [Npc, Mage, (WorldCell,Cell10)]
//  - Group Cell10: Table [Npc, Beggar, (WorldCell,Cell10)]
