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

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Queries_Instancing
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query for Position, Velocity. We'll create a few entities that
        // have Velocity as owned and shared component.
        Query q = world.QueryBuilder<Position, Velocity>()
            .TermAt(1).Self() // Position must always be owned by the entity
            .Instanced()      // Create instanced query
            .Build();

        // Create a prefab with Velocity. Prefabs are not matched with queries.
        Entity prefab = world.Prefab("p")
            .Set<Velocity>(new Velocity(1, 2));

        // Create a few entities that own Position & share Velocity from the prefab.
        world.Entity("e1").IsA(prefab)
            .Set<Position>(new Position(10, 20));

        world.Entity("e2").IsA(prefab)
            .Set<Position>(new Position(10, 20));

        // Create a few entities that own all components
        world.Entity("e3")
            .Set<Position>(new Position(10, 20))
            .Set<Velocity>(new Velocity(3, 4 ));

        world.Entity("e4")
            .Set<Position>(new Position(10, 20))
            .Set<Velocity>(new Velocity(4, 5));


        // Iterate the instanced query. Note how when a query is instanced, it needs
        // to check whether a field is owned or not in order to know how to access
        // it. In the case of an owned field it is iterated as an array, whereas
        // in the case of a shared field, it is accessed as a pointer.
        q.Iter((Iter it, Field<Position> p , Field<Velocity> v) =>
        {
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
    }
}

// Output:
// Velocity is shared
// e1: (11, 22)
// e2: (11, 22)
// Velocity is owned
// e3: (13, 24)
// e4: (14, 25)
