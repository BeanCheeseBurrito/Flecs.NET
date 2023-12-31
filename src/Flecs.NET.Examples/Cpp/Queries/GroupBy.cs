// Group by is a feature of cached queries that allows applications to assign a
// group id to each matched table. Tables that are assigned the same group id
// are stored together in "groups". This ensures that when a query is iterated,
// tables that share a group are iterated together.
//
// Groups in the cache are ordered by group id, which ensures that tables with
// lower ids are iterated before table with higher ids. This is the same
// mechanism that is used by the cascade feature, which groups tables by depth
// in a relationship hierarchy.
//
// This makes groups a more efficient, though less granular mechanism for
// ordering entities. Order is maintained at the group level, which means that
// once a group is created, tables can get added and removed to the group
// with is an O(1) operation.
//
// Groups can also be used as an efficient filtering mechanism. See the
// SetGroup example for more details.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

// Dummy tag to put entities in different tables
file struct Tag;

// Create a relationship to use for the GroupBy function. Tables will
// be assigned the relationship target as group id
file struct Group;

// Targets for the relationship, which will be used as group ids.
file struct First;
file struct Second;
file struct Third ;

public static class Cpp_Queries_GroupBy
{
    public static void Main()
    {
        using World world = World.Create();

        // Register components in order so that id for First is lower than Third
        world.Component<First>();
        world.Component<Second>();
        world.Component<Third>();

        // Grouped query
        Query q = world.QueryBuilder<Position>()
            .GroupBy<Group>()
            .Build();

        // Create entities in 6 different tables with 3 group ids
        world.Entity().Add<Group, Third>()
            .Set<Position>(new(1, 1));
        world.Entity().Add<Group, Second>()
            .Set<Position>(new(2, 2));
        world.Entity().Add<Group, First>()
            .Set<Position>(new(3, 3));

        world.Entity().Add<Group, Third>()
            .Set<Position>(new(4, 4))
            .Add<Tag>();
        world.Entity().Add<Group, Second>()
            .Set<Position>(new(5, 5))
            .Add<Tag>();
        world.Entity().Add<Group, First>()
            .Set<Position>(new(6, 6))
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

        q.Iter((Iter it, Column<Position> p) =>
        {
            Entity group = it.World().Entity(it.GroupId());

            Console.WriteLine($" - Group {group.Path()}: table [{it.Table()}]");

            foreach (int i in it)
                Console.WriteLine($"     ({p[i].X}, {p[i].Y})\n");
        });
    }
}

// Output:
//  - Group First: table [Position, (Group,First)]
//      (3, 3)
//
//  - Group First: table [Position, Tag, (Group,First)]
//      (6, 6)
//
//  - Group Second: table [Position, (Group,Second)]
//      (2, 2)
//
//  - Group Second: table [Position, Tag, (Group,Second)]
//      (5, 5)
//
//  - Group Third: table [Position, (Group,Third)]
//      (1, 1)
//
//  - Group Third: table [Position, Tag, (Group,Third)]
//      (4, 4)
