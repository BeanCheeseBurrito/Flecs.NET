// A group iterator iterates over a single group of a grouped query (see the
// group_by example for more details). This can be useful when an application
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

#if Cpp_Queries_GroupIter

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

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

Query q = world.Query(
    filter: world.FilterBuilder().With<Npc>(),
    query: world.QueryBuilder().GroupBy<WorldCell>()
);

// Iterate all tables
Console.WriteLine("All tables:");
q.Iter((Iter it) =>
{
    Entity group = world.Entity(it.GroupId());
    Console.WriteLine($" - Group {group.Path()}: Table [{it.Table()}]");
});

Console.WriteLine();

// Only iterate entities in cell 10
Console.WriteLine("Tables for cell 1_0:");
q.Iter().SetGroup<Cell10>().Iter((Iter it) =>
{
    Entity group = world.Entity(it.GroupId());
    Console.WriteLine($" - Group {group.Path()}: Table [{it.Table()}]");
});

// A world cell relationship with four cells
public struct WorldCell { }
public struct Cell00 { }
public struct Cell01 { }
public struct Cell10 { }
public struct Cell11 { }

// Npc tags
public struct Npc { }
public struct Merchant { }
public struct Soldier { }
public struct Beggar { }
public struct Mage { }

#endif

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
