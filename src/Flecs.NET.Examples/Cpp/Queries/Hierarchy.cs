#if Cpp_Queries_Hierarchy

using Flecs.NET.Core;

using World world = World.Create();

// Create a hierarchy. For an explanation see the entities/hierarchy example
Entity sun = world.Entity("Sun")
    .Add<Position, Global>()
    .SetFirst<Position, Local>(new Position { X = 1, Y = 1 });

    world.Entity("Mercury")
        .ChildOf(sun)
        .Add<Position, Global>()
        .SetFirst<Position, Local>(new Position { X = 1, Y = 1 });

    world.Entity("Venus")
        .ChildOf(sun)
        .Add<Position, Global>()
        .SetFirst<Position, Local>(new Position { X = 2, Y = 2 });

    Entity earth = world.Entity("Earth")
        .ChildOf(sun)
        .Add<Position, Global>()
        .SetFirst<Position, Local>(new Position { X = 3, Y = 3 });

        world.Entity("Moon")
            .ChildOf(earth)
            .Add<Position, Global>()
            .SetFirst<Position, Local>(new Position { X = 0.1, Y = 0.1 });

// Create a hierarchical query to compute the global position from the
// local position and the parent position.
Query q = world.Query(
    filter: world.FilterBuilder()
        .With<Position, Local>()  // Self local position
        .With<Position, Global>() // Self global position
        .With<Position, Global>() // Parent global position
            .Parent().Cascade()   // Get from the parent, in breadth-first order (cascade)
            .Optional()           // Make term component optional so we also match the root (sun)
);

// Do the transform
q.Iter((Iter it) =>
{
    Column<Position> selfLocal = it.Field<Position>(1);
    Column<Position> selfGlobal = it.Field<Position>(2);
    Column<Position> parentGlobal = it.Field<Position>(3);

    foreach (int i in it)
    {
        selfGlobal[i].X = selfLocal[i].X;
        selfGlobal[i].Y = selfLocal[i].Y;

        if (!parentGlobal.IsNull)
        {
            selfGlobal[i].X += parentGlobal[i].X;
            selfGlobal[i].Y += parentGlobal[i].Y;
        }
    }
});

// Print world positions for all entities that have (Position, Global)
world.EachFirst<Position, Global>((Entity e, ref Position p) =>
{
    Console.WriteLine($"{e}: ({p.X}, {p.Y})");
});

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Tags for local/world position
public struct Local { }
public struct Global { }

#endif

// Output:
// Sun: (1, 1)
// Mercury: (2, 2)
// Venus: (3, 3)
// Earth: (4, 4)
// Moon: (4.1, 4.1)
