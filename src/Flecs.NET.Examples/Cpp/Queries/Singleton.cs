// This example shows how to use singleton components in queries.

#if Cpp_Queries_Singleton

using Flecs.NET.Core;

using World world = World.Create();

// Set singleton
world.Set(new Gravity { Value = 9.81 });

// Set Velocity
world.Entity("e1").Set(new Velocity { X = 0, Y = 0 });
world.Entity("e2").Set(new Velocity { X = 0, Y = 1 });
world.Entity("e3").Set(new Velocity { X = 0, Y = 2 });

// Create query that matches Gravity as singleton
Query q = world.Query(
    filter: world.FilterBuilder()
        .With<Velocity>()
        .With<Gravity>().Singleton()
);

// In a query string expression you can use the $ shortcut for singletons:
//   Velocity, Gravity($)
q.Each((Iter it, int i) =>
{
    Column<Velocity> v = it.Field<Velocity>(1);
    ref Gravity g = ref it.Field<Gravity>(2)[0];

    v[i].Y += g.Value;
    Console.WriteLine($"{it.Entity(i)} velocity is ({v[i].X}, {v[i].Y})");
});

// Singleton component
public struct Gravity
{
    public double Value { get; set; }
}

// Entity component
public struct Velocity
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// Output:
// e1 velocity is (0, 9.81)
// e2 velocity is (0, 10.81)
// e3 velocity is (0, 11.81)
