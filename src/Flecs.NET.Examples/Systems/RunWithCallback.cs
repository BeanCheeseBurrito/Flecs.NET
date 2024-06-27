// This example shows how to forward .Each and .Iter callbacks to a custom runner.
//
// See the Cpp/Systems/CustomRunner example for more information.
// https://github.com/BeanCheeseBurrito/Flecs.NET/blob/v4/src/Flecs.NET.Examples/Cpp/Systems/CustomRunner.cs

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static unsafe class Systems_RunWithCallback
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a new entity with a Position and Velocity component.
        world.Entity("e")
            .Set(new Position(0, 0))
            .Set(new Velocity(1, 1));

        // Calling .Run() with a single parameter will finish building the system and
        // use the custom runner to manually control the iteration of the entire system.
        Routine system1 = world.Routine<Position, Velocity>("System 1")
            .Run((Iter it) =>
            {
                Console.WriteLine($"{it.System()} Running...");

                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Field<Velocity> v = it.Field<Velocity>(1);

                    foreach (int i in it)
                    {
                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                        Console.WriteLine($"{it.Entity(i)}: {p[i]}");
                    }
                }
            });

        // Adding a second 'Action<Iter>' parameter will allow you to forward a callback to the custom runner.
        Routine system2 = world.Routine<Position, Velocity>("System 2")
            // This returns the routine builder and requires .Iter() or .Each()
            // to be called afterwards to finish building the system.
            .Run((Iter it, Action<Iter> callback) =>
            {
                Console.WriteLine($"{it.System()} Running...");

                while (it.Next())
                    callback(it); // Pass the iterator as an argument.
            })
            // The .Each callback will be passed to the custom runner.
            .Each((Iter it, int i, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
                Console.WriteLine($"{it.Entity(i)}: {p}");
            });

        // Alternatively, a managed function pointer can be used instead of 'Action<Iter>'.
        Routine system3 = world.Routine<Position, Velocity>("System 3")
            .Run((Iter it, delegate*<Iter, void> callback) =>
            {
                Console.WriteLine($"{it.System()} Running...");

                while (it.Next())
                    callback(it);
            })
            // The .Iter callback will be passed to the custom runner.
            .Iter((Iter it, Field<Position> p, Field<Velocity> v) =>
            {
                foreach (int i in it)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                    Console.WriteLine($"{it.Entity(i)}: {p[i]}");
                }
            });

        system1.Run();
        system2.Run();
        system3.Run();
    }
}

// Output:
// System 1 Running...
// e: Position { X = 1, Y = 1 }
// System 2 Running...
// e: Position { X = 2, Y = 2 }
// System 3 Running...
// e: Position { X = 3, Y = 3 }
