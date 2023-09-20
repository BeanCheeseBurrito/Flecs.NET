// Instancing is the ability of queries to iterate results with fields that have
// different numbers of elements. The term "instancing" is borrowed from
// graphics APIs, where it means reusing the same data for multiple "instances".
//
// Query instancing works in much the same way. By default queries match all
// components on the same entity. It is however possible to request data from
// other entities, like getting the Position from the entity's parent.
//
// Instancing refers to the ability of queries to iterate components for
// multiple entities while at the same time providing "instanced" components,
// which are always provided one element at a time.
//
// Instancing is often used in combination with parent-child relationships and
// prefabs, but is applicable to any kind of query where some of the terms are
// matched on N entities, and some on a single entity.
//
// By default queries are not instanced, which means that if a result contains
// mixed fields, entities will be iterated one by one instead of in batches.
// This is safer, as code doesn't have to do anything different for owned and
// shared fields, but does come at a performance penalty.
//
// The Each() iterator function always uses an instanced iterator under the
// hood. This is transparent to the application, but improves performance. For
// this reason using Each() can be faster than using uninstanced Iter().
//

#if Cpp_Queries_Instancing

using Flecs.NET.Core;

using World world = World.Create();

// Create a query for Position, Velocity. We'll create a few entities that
// have Velocity as owned and shared component.
Query q = world.Query(
    filter: world.FilterBuilder()
        .With<Position>().Self() // Position must always be owned by the entity
        .With<Velocity>()
        .Instanced()             // Create instanced query
);

// Create a prefab with Velocity. Prefabs are not matched with queries.
Entity prefab = world.Prefab("p")
    .Set(new Velocity { X = 1, Y = 2 });

// Create a few entities that own Position & share Velocity from the prefab.
world.Entity("e1").IsA(prefab)
    .Set(new Position { X = 10, Y = 20 });

world.Entity("e2").IsA(prefab)
    .Set(new Position { X = 10, Y = 20 });

// Create a few entities that own all components
world.Entity("e3")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 3, Y = 4 });

world.Entity("e4")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 4, Y = 5 });


// Iterate the instanced query. Note how when a query is instanced, it needs
// to check whether a field is owned or not in order to know how to access
// it. In the case of an owned field it is iterated as an array, whereas
// in the case of a shared field, it is accessed as a pointer.
q.Iter((Iter it) =>
{
    Column<Position> p = it.Field<Position>(1);
    Column<Velocity> v = it.Field<Velocity>(2);

    // Check if Velocity is owned, in which case it's accessed as array.
    // Position will always be owned, since we set the term to Self.
    if (it.IsSelf(2)) // Velocity is term 2
    {
        Console.WriteLine("Velocity is owned");

        foreach (int i in it)
        {
            p[i].X += v[i].X;
            p[i].Y += v[i].Y;
            Console.WriteLine($"{it.Entity(i)}: ({p[i].X}, {p[i].Y})");
        }
    }
    // If Velocity is shared, access the field from the index 0.
    else
    {
        Console.WriteLine("Velocity is shared");

        foreach (int i in it)
        {
            p[i].X += v[0].X;
            p[i].Y += v[0].Y;
            Console.WriteLine($"{it.Entity(i)}: ({p[i].X}, {p[i].Y})");
        }
    }
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

#endif

// Output:
// Velocity is shared
// e1: (11, 22)
// e2: (11, 22)
// Velocity is owned
// e3: (13, 24)
// e4: (14, 25)