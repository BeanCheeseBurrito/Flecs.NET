using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static unsafe class Queries_BasicIteration
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a new entity with a Position and Velocity component.
        world.Entity("e")
            .Set(new Position(0, 0))
            .Set(new Velocity(1, 1));

        // Create a query that matches entities with a Position and Velocity component.
        using Query<Position, Velocity> query = world.Query<Position, Velocity>();

        // The .Each() function is called once for every individual entity matched.
        // The .Iter() function is called once for every archetype matched and allows for manual control over the iteration of entities.
        // The .Run() function is called only once and allows for manual control over the iteration of both archetypes and entities.

        // Provides managed references to components.
        query.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine(p);
        });

        // Provides entity + managed references to components.
        query.Each((Entity e, ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine($"{e}: {p}");
        });

        // Provides iterator + row + unsafe pointers to components.
        query.Each((Iter it, int i, ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
            Console.WriteLine($"{it.Entity(i)}: {p}");
        });

        // Provides unsafe pointers to components. Only supports unmanaged types.
        query.Each((Position* p, Velocity* v) =>
        {
            p->X += v->X;
            p->Y += v->Y;
            Console.WriteLine(*p);
        });

        // Provides entity + unsafe pointers to components. Only supports unmanaged types.
        query.Each((Entity e, Position* p, Velocity* v) =>
        {
            p->X += v->X;
            p->Y += v->Y;
            Console.WriteLine($"{e}: {*p}");
        });

        // Provides iterator + row + unsafe pointers to components. Only supports unmanaged types.
        query.Each((Iter it, int i, Position* p, Velocity* v) =>
        {
            p->X += v->X;
            p->Y += v->Y;
            Console.WriteLine($"{it.Entity(i)}: {*p}");
        });

        // Provides iterator + component columns as fields.
        query.Iter((Iter it, Field<Position> p, Field<Velocity> v) =>
        {
            foreach (int i in it)
            {
                p[i].X += v[i].X;
                p[i].Y += v[i].Y;
                Console.WriteLine($"{it.Entity(i)}: {p[i]}");
            }
        });

        // Provides iterator + component columns as spans. Only supports unmanaged types.
        query.Iter((Iter it, Span<Position> p, Span<Velocity> v) =>
        {
            foreach (int i in it)
            {
                p[i].X += v[i].X;
                p[i].Y += v[i].Y;
                Console.WriteLine($"{it.Entity(i)}: {p[i]}");
            }
        });

        // Provides iterator + component columns as pointers. Only supports unmanaged types.
        query.Iter((Iter it, Position* p, Velocity* v) =>
        {
            foreach (int i in it)
            {
                p[i].X += v[i].X;
                p[i].Y += v[i].Y;
                Console.WriteLine($"{it.Entity(i)}: {p[i]}");
            }
        });

        // Provides iterator
        query.Run((Iter it) =>
        {
            while (it.Next())
            {
                // Component columns as fields. Recommended by default as they support both managed and unmanaged types.
                Field<Position> pField = it.Field<Position>(0);
                Field<Velocity> vField = it.Field<Velocity>(1);

                // Component columns as spans. Only supports unmanaged types.
                Span<Position> pSpan = it.Span<Position>(0);
                Span<Velocity> vSpan = it.Span<Velocity>(1);

                // Component columns as pointers. Only supports unmanaged types.
                Position* pPointer = it.Pointer<Position>(0);
                Velocity* vPointer = it.Pointer<Velocity>(1);

                foreach (int i in it)
                {
                    pField[i].X += vField[i].X;
                    pField[i].Y += vField[i].Y;
                    Console.WriteLine($"{it.Entity(i)}: {pField[i]}");

                    pSpan[i].X += vSpan[i].X;
                    pSpan[i].Y += vSpan[i].Y;
                    Console.WriteLine($"{it.Entity(i)}: {pSpan[i]}");

                    pPointer[i].X += vPointer[i].X;
                    pPointer[i].Y += vPointer[i].Y;
                    Console.WriteLine($"{it.Entity(i)}: {pPointer[i]}");
                }
            }
        });
    }
}

// Output:
// Position { X = 1, Y = 1 }
// e: Position { X = 2, Y = 2 }
// e: Position { X = 3, Y = 3 }
// Position { X = 4, Y = 4 }
// e: Position { X = 5, Y = 5 }
// e: Position { X = 6, Y = 6 }
// e: Position { X = 7, Y = 7 }
// e: Position { X = 8, Y = 8 }
// e: Position { X = 9, Y = 9 }
// e: Position { X = 10, Y = 10 }
// e: Position { X = 11, Y = 11 }
// e: Position { X = 12, Y = 12 }
