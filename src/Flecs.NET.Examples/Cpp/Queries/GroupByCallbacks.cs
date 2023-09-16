// This example shows how the on_group_create and on_group_delete callbacks can
// be used to get notified when a new group is registered for a query. These
// callbacks make it possible to associate and manage user data attached to
// groups.

#if Cpp_Queries_GroupByCallbacks

using System.Runtime.InteropServices;
using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

unsafe
{
    using World world = World.Create();

    // Register components in order so that id for First is lower than Third
    world.Component<First>();
    world.Component<Second>();
    world.Component<Third>();

    int groupCounter = 0;

    // Grouped query
    Query q = world.Query(
        filter: world.FilterBuilder().Term<Position>(),
        query: world.QueryBuilder()
            .GroupBy<Group>()
            // Callback invoked when a new group is created
            .OnGroupCreate((
                ecs_world_t* world,
                ulong id,            // id of the group that was created
                void* groupByArg) => // group_by_ctx parameter in ecs_query_desc_t struct
            {
                World w = new(world);
                Console.WriteLine($"Group {w.Entity(id)} created");

                // Return data that will be associated with the group
                GroupCtx* ctx = (GroupCtx*)NativeMemory.AllocZeroed((nuint)sizeof(GroupCtx));
                ctx->Counter = ++groupCounter;
                return ctx;
            })
            // Callback invoked when a group is deleted
            .OnGroupDelete((
                ecs_world_t* world,
                ulong id,            // id of the group that was deleted
                void* ctx,           // group context
                void* groupByArg) => // group_by_ctx parameter in ecs_query_desc_t struct
            {
                World w = new(world);
                Console.WriteLine($"Group {w.Entity(id)} deleted");

                // Free data associated with group
                NativeMemory.Free(ctx);
            })
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
    //     - table [Position, Tag, (Group, First)]
    //
    //  - group Second:
    //     - table [Position, (Group, Second)]
    //     - table [Position, Tag, (Group, Second)]
    //
    //  - group Third:
    //     - table [Position, (Group, Third)]
    //     - table [Position, Tag, (Group, Third)]
    //

    q.Iter((Iter it) =>
    {
        Column<Position> p = it.Field<Position>(1);

        Entity group = world.Entity(it.GroupId());
        GroupCtx* ctx = (GroupCtx*)q.GroupCtx(group);

        Console.WriteLine($" - Group {group.Path()}: table [{it.Table().Str()}]");
        Console.WriteLine($"    Counter: {ctx->Counter}");

        foreach (int i in it)
            Console.WriteLine($"    ({p[i].X}, {p[i].Y})");

        Console.WriteLine();
    });

    // Deleting the query will call the on_group_deleted callback
    q.Destruct();
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Custom type to associate with group
public struct GroupCtx
{
    public int Counter { get; set; }
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
// Group Third created
// Group Second created
// Group First created
//
//  - Group First: table [Position, (Group,First)]
//     Counter: 3
//     (3, 3)
//
//  - Group First: table [Position, Tag, (Group,First)]
//     Counter: 3
//     (6, 6)
//
//  - Group Second: table [Position, (Group,Second)]
//     Counter: 2
//     (2, 2)
//
//  - Group Second: table [Position, Tag, (Group,Second)]
//     Counter: 2
//     (5, 5)
//
//  - Group Third: table [Position, (Group,Third)]
//     Counter: 1
//     (1, 1)
//
//  - Group Third: table [Position, Tag, (Group,Third)]
//     Counter: 1
//     (4, 4)
//
// Group First deleted
// Group Third deleted
// Group Second deleted
