#if Cpp_Queries_Iter

using Flecs.NET.Core;

using World world = World.Create();

// Create a query for Position, Velocity.
Query q = world.Query(
    filter: world.FilterBuilder()
        .With<Position>()
        .With<Velocity>()
);

// Create a few test entities for a Position, Velocity query
world.Entity("e1")
    .Set(new Position { X = 10, Y = 20})
    .Set(new Velocity { X = 1, Y = 2});

world.Entity("e2")
    .Set(new Position { X = 10, Y = 20})
    .Set(new Velocity { X = 3, Y = 4});

world.Entity("e3")
    .Set(new Position { X = 10, Y = 20})
    .Set(new Velocity { X = 4, Y = 5})
    .Set(new Mass { Value = 50});

// The iter function provides a Iter object which contains all sorts
// of information on the entities currently being iterated.
// The function passed to iter is by default called for each table the query
// is matched with.
q.Iter((Iter it) =>
{
    Column<Position> p = it.Field<Position>(1);
    Column<Velocity> v = it.Field<Velocity>(2);

    // Print the table & number of entities matched in current callback
    Console.WriteLine($"Table [{it.Type()}]");
    Console.WriteLine($" - Number of entities: {it.Count()}");

    // Print information about the components being matched
    for (int i = 1; i <= it.FieldCount(); i ++)
    {
        Console.WriteLine($" - Term {i}:");
        Console.WriteLine($"   - Component: {it.Id(i)}");
        Console.WriteLine($"   - Type size: {it.Size(i)}");
    }

    Console.WriteLine();

    // Iterate entities
    foreach (int i in it)
    {
        p[i].X += v[i].X;
        p[i].Y += v[i].Y;
        Console.WriteLine($" - {it.Entity(i)}: ({p[i].X}, {p[i].Y})");
    }

    Console.WriteLine();
});

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Velocity
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Mass
{
    public double Value { get; set; }
}

#endif

// Output:
// Table [Position, Velocity, (Identifier,Name)]
//  - Number of entities: 2
//  - Term 1:
//    - Component: Position
//    - Type size: 16
//  - Term 2:
//    - Component: Velocity
//    - Type size: 16
//
//  - e1: (11, 22)
//  - e2: (13, 24)
//
// Table [Position, Velocity, Mass, (Identifier,Name)]
//  - Number of entities: 1
//  - Term 1:
//    - Component: Position
//    - Type size: 16
//  - Term 2:
//    - Component: Velocity
//    - Type size: 16
//
//  - e3: (14, 25)
