// An observer can match multiple components/tags. Only entities that match the
// entire observer filter will be forwarded to the callback. For example, an
// observer for Position,Velocity won't match an entity that only has Position.

#if Cpp_Observers_TwoComponents

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

// Create observer for custom event
world.Observer(
    filter: world.FilterBuilder()
        .Term<Position>()
        .Term<Velocity>(),
    observer: world.ObserverBuilder().Event(EcsOnSet),
    callback: (Iter it) =>
    {
        Column<Position> p = it.Field<Position>(1);
        Column<Velocity> v = it.Field<Velocity>(2);

        foreach (int i in it)
        {
            Console.Write($" - {it.Event().Name()}: ");
            Console.Write($"{it.EventId().Str()}: ");
            Console.Write($"{it.Entity(i).Name()}: ");
            Console.Write($"p: ({p[i].X}, {p[i].Y}) ");
            Console.Write($"v: ({v[i].X}, {v[i].Y})");
            Console.WriteLine();
        }
    }
);

// Create entity, set Position (emits EcsOnSet, does not yet match observer)
Entity e = world.Entity("e")
    .Set(new Position { X = 10, Y = 20 });

// Set Velocity (emits EcsOnSet, matches observer)
e.Set(new Velocity { X = 1, Y = 2 });

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
//  - OnSet: Velocity: e: p: (10, 20) v: (1, 2)
