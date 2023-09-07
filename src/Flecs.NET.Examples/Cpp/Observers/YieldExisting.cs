// Observers can enable a "YieldExisting" feature that upon creation of the
// observer produces events for all entities that match the observer query. The
// feature is only implemented for the builtin EcsOnAdd and EcsOnSet events.
//
// Custom events can also implement behavior for YieldExisting by adding the
// Iterable component to the event (see EcsIterable for more details).

#if Cpp_Observers_YieldExisting

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

// Create existing entities with Position component
world.Entity("e1").Set(new Position { X = 10, Y = 20 });
world.Entity("e2").Set(new Position { X = 20, Y = 30 });

// Create observer for Position
world.Observer(
    filter: world.FilterBuilder().Term<Position>(),
    observer: world.ObserverBuilder()
        .Event(EcsOnSet)
        .YieldExisting(),
    callback: (Iter it, int i) =>
    {
        Column<Position> p = it.Field<Position>(1);

        Console.Write($" - {it.Event().Name()}: ");
        Console.Write($"{it.EventId().Str()}: ");
        Console.Write($"{it.Entity(i).Name()}: ");
        Console.Write($"({p[i].X}, {p[i].Y})");
        Console.WriteLine();
    }
);

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// Output:
//  - OnSet: Position: e1: (10, 20)
//  - OnSet: Position: e2: (20, 30)
