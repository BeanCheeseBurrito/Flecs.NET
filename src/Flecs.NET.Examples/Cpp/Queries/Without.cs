#if Cpp_Queries_Without

using Flecs.NET.Core;

using World world = World.Create();

// Create a query for Position, !Npc. By adding the Npc component using the
// "Without" method, the component is not a part of the query type, and as a
// result does not become part of the function signatures of each and iter.
// This is useful for things like tags, which because they don't have a
// value are less useful to pass to the each/iter functions as argument.
//
// The without method is short for:
//   .Term<Npc>().Not()
Query q = world.Query(
    filter: world.FilterBuilder()
        .With<Position>()
        .Without<Npc>()
);

// Create a few test entities for the Position query
world.Entity("e1")
    .Set(new Position { X = 10, Y = 20 });

world.Entity("e2")
    .Set(new Position { X = 10, Y = 20 });

// This entity will not match as it has Npc
world.Entity("e3")
    .Set(new Position { X = 10, Y = 20 })
    .Add<Npc>();


// Note how the Npc tag is not part of the each signature
q.Each((Iter it, int i) =>
{
    Column<Position> p = it.Field<Position>(1);
    Console.WriteLine($"{it.Entity(i)}: ({p[i].X}, {p[i].Y})");
});

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Npc { }

#endif

// Output:
//  e1: (10, 20)
//  e2: (10, 20)
