// This example does the same as the GroupBy example, but with a custom
// GroupBy function. A custom function makes it possible to customize how a
// group id is calculated for a table.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

// Dummy tag to put entities in different tables
file struct Tag;

// Create a relationship to use for the group_by function. Tables will
// be assigned the relationship target as group id
file struct Group;

// Targets for the relationship, which will be used as group ids.
file struct First;
file struct Second;
file struct Third;

public static class Queries_GroupByCustom
{
    public static void Main()
    {
        using World world = World.Create();

        // Register components in order so that id for First is lower than Third
        world.Component<First>();
        world.Component<Second>();
        world.Component<Third>();

        // Grouped query
        using Query q = world.QueryBuilder<Position>()
            .GroupBy<Group>(GroupByRelation)
            .Build();

        // Create entities in 6 different tables with 3 group ids
        world.Entity().Add<Group, Third>()
            .Set(new Position(1, 1));
        world.Entity().Add<Group, Second>()
            .Set(new Position(2, 2));
        world.Entity().Add<Group, First>()
            .Set(new Position(3, 3));

        world.Entity().Add<Group, Third>()
            .Set(new Position(4, 4))
            .Add<Tag>();
        world.Entity().Add<Group, Second>()
            .Set(new Position(5, 5))
            .Add<Tag>();
        world.Entity().Add<Group, First>()
            .Set(new Position(6, 6))
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

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);

                Entity group = world.Entity(it.GroupId());
                Console.WriteLine($" - Group {group.Path()}: Table [{it.Table()}]");

                foreach (int i in it)
                    Console.WriteLine($"     ({p[i].X}, {p[i].Y})");

                Console.WriteLine();
            }
        });
    }

    private static ulong GroupByRelation(World world, Table table, Entity id)
    {
        if (table.Search(id, Ecs.Wildcard, out Id match) != -1)
            return match.Second();

        return 0;
    }
}

// Output:
//  - Group .First: Table [Position, (Group,First)]
//      (3, 3)
//
//  - Group .First: Table [Position, Tag, (Group,First)]
//      (6, 6)
//
//  - Group .Second: Table [Position, (Group,Second)]
//      (2, 2)
//
//  - Group .Second: Table [Position, Tag, (Group,Second)]
//      (5, 5)
//
//  - Group .Third: Table [Position, (Group,Third)]
//      (1, 1)
//
//  - Group .Third: Table [Position, Tag, (Group,Third)]
//      (4, 4)
