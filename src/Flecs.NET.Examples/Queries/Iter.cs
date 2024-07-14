using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);
file record struct Mass(float Value);

public static class Queries_Iter
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a query for Position, Velocity.
        using Query q = world.Query<Position, Velocity>();

        // Create a few test entities for a Position, Velocity query
        world.Entity("e1")
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        world.Entity("e2")
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        world.Entity("e3")
            .Set(new Position(10, 20))
            .Set(new Velocity(4, 5))
            .Set(new Mass(50));

        // The run function provides a Iter object which contains all sorts
        // of information on the entities currently being iterated.
        // The function passed to iter is by default called for each table the query
        // is matched with.
        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                // Print the table & number of entities matched in current callback
                Console.WriteLine($"Table [{it.Type()}]");
                Console.WriteLine($" - Number of entities: {it.Count()}");

                // Print information about the components being matched
                for (int i = 0; i < it.FieldCount(); i++)
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
            }
        });
    }
}

// Output:
// Table [Position, Velocity, (Identifier,Name)]
//  - Number of entities: 2
//  - Term 1:
//    - Component: Position
//    - Type size: 8
//  - Term 2:
//    - Component: Velocity
//    - Type size: 8
//
//  - e1: (11, 22)
//  - e2: (13, 24)
//
// Table [Position, Velocity, Mass, (Identifier,Name)]
//  - Number of entities: 1
//  - Term 1:
//    - Component: Position
//    - Type size: 8
//  - Term 2:
//    - Component: Velocity
//    - Type size: 8
//
//  - e3: (14, 25)
