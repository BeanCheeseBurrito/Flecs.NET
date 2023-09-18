// This example does the same as the GoupBy example, but with a custom
// GroupBy function. A custom function makes it possible to customize how a
// group id is calculated for a table.

#if Cpp_Queries_GroupByCustom

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

unsafe
{
    ulong GroupByRelation(ecs_world_t *ecs, ecs_table_t *table, ulong id, void *ctx)
    {
        // Use ecs_search to find the target for the relationship in the table
        ulong match;
        if (ecs_search(ecs, table, new Id(id, Ecs.Wildcard), &match) != -1)
            return new Id(ecs, match).Second(); // First, Second or Third
        return 0;
    }

    using World world = World.Create();

    // Register components in order so that id for First is lower than Third
    world.Component<First>();
    world.Component<Second>();
    world.Component<Third>();

    // Grouped query
    Query q = world.Query(
        filter: world.FilterBuilder().With<Position>(),
        query: world.QueryBuilder().GroupBy<Group>(GroupByRelation)
    );

    // Create entities in 6 different tables with 3 group ids
    world.Entity().Add<Group, Third>()
        .Set(new Position { X = 1, Y = 1 });
    world.Entity().Add<Group, Second>()
        .Set(new Position { X = 2, Y = 2 });
    world.Entity().Add<Group, First>()
        .Set(new Position { X = 3, Y = 3 });

    world.Entity().Add<Group, Third>()
        .Set(new Position { X = 4, Y = 4 })
        .Add<Tag>();
    world.Entity().Add<Group, Second>()
        .Set(new Position { X = 5, Y = 5 })
        .Add<Tag>();
    world.Entity().Add<Group, First>()
        .Set(new Position { X = 6, Y = 6 })
        .Add<Tag>();

    // The query cache now looks like this:
    //  - group First:
    //     - table [Position, (Group, First)]
    //     - table [Postion, Tag, (Group, First)]
    //
    //  - group Second:
    //     - table [Position, (Group, Second)]
    //     - table [Postion, Tag, (Group, Second)]
    //
    //  - group Third:
    //     - table [Position, (Group, Third)]
    //     - table [Postion, Tag, (Group, Third)]
    //

    q.Iter((Iter it) =>
    {
        Column<Position> p = it.Field<Position>(1);

        Entity group = world.Entity(it.GroupId());
        Console.WriteLine($" - Group {group.Path()}: Table [{it.Table()}]");

        foreach (int i in it)
            Console.WriteLine($"     ({p[i].X}, {p[i].Y})");

        Console.WriteLine();
    });
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Dummy tag to put entities in different tables
public struct Tag { }

// Create a relationship to use for the GroupBy function. Tables will
// be assigned the relationship target as group id
public struct Group { }

// Targets for the relationship, which will be used as group ids.
public struct First { }
public struct Second { }
public struct Third { }

#endif

// Output:
//  - Group First: Table [Position, (Group,First)]
//      (3, 3)
//
//  - Group First: Table [Position, Tag, (Group,First)]
//      (6, 6)
//
//  - Group Second: Table [Position, (Group,Second)]
//      (2, 2)
//
//  - Group Second: Table [Position, Tag, (Group,Second)]
//      (5, 5)
//
//  - Group Third: Table [Position, (Group,Third)]
//      (1, 1)
//
//  - Group Third: Table [Position, Tag, (Group,Third)]
//      (4, 4)
