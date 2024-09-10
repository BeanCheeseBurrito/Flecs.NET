// This example shows how to use singleton components in queries.

using Flecs.NET.Core;

// Singleton component
file record struct Gravity(float Value);

// Entity component
file record struct Velocity(float X, float Y);

public static class Queries_Singleton
{
    public static void Main()
    {
        using World world = World.Create();

        // Set singleton
        world.Set(new Gravity(9.81f));

        // Set Velocity
        world.Entity("e1").Set(new Velocity(0, 0));
        world.Entity("e2").Set(new Velocity(0, 1));
        world.Entity("e3").Set(new Velocity(0, 2));

        // Create query that matches Gravity as singleton
        using Query<Velocity, Gravity> q = world.QueryBuilder<Velocity, Gravity>()
            .TermAt(1).Singleton()
            .Build();

        // In a query string expression you can use the $ shortcut for singletons:
        //   Velocity, Gravity($)
        q.Each((Entity e, ref Velocity v, ref Gravity g) =>
        {
            Console.WriteLine(v);
            v.Y += g.Value;
            Console.WriteLine($"{e} velocity is ({v.X}, {v.Y})");
        });
    }
}

// Output:
// e1 velocity is (0, 9.81)
// e2 velocity is (0, 10.81)
// e3 velocity is (0, 11.81)
