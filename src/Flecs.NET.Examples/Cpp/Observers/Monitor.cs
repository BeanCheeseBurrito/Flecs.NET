// A monitor observer triggers when an entity starts/stop matching the observer
// filter. The observer communicates whether an entity is "entering/leaving" the
// monitor by setting ecs_iter_t::event to EcsOnAdd (for entering) or
// EcsOnRemove (for leaving).
//
// To specify that an observer is a monitor observer, the EcsMonitor tag must be
// provided as event. No additional event kinds should be provided for a monitor
// observer.

#if Cpp_Observers_Monitor

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

// Create observer for custom event
world.Observer(
    filter: world.FilterBuilder()
        .Term<Position>()
        .Term<Velocity>(),
    observer: world.ObserverBuilder()
        .Event(EcsMonitor), // Monitor entities entering/leaving the query
    callback: (Iter it, int i) =>
    {
        if (it.Event() == EcsOnAdd)
            Console.WriteLine($" - Enter: {it.EventId().Str()}: {it.Entity(i).Name()}");
        else if (it.Event() == EcsOnRemove)
            Console.WriteLine($" - Leave: {it.EventId().Str()}: {it.Entity(i).Name()}");
    }
);

// Create entity
Entity e = world.Entity("e");

// This does not yet trigger the monitor, as the entity does not yet match.
e.Set(new Position { X = 10, Y = 20 });

// This triggers the monitor with EcsOnAdd, as the entity now matches.
e.Set(new Velocity { X = 1, Y = 2 });

// This triggers the monitor with EcsOnRemove, as the entity no longer matches.
e.Remove<Position>();

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
//  - Enter: Velocity: e
//  - Leave: Position: e
