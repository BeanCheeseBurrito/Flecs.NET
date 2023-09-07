// Observers can be used to match custom events. Custom events can be emitted
// using the ecs_emit function. This function is also used by builtin events,
// so builtin and custom events use the same rules for matching with observers.
//
// An event consists out of three pieces of data used to match with observers:
//  - An single event kind (EcsOnAdd, EcsOnRemove, ...)
//  - One or more event ids (Position, Velocity, ...)
//  - A source (either an entity or a table)

#if Cpp_Observers_CustomEvent

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

// Create observer for custom event
world.Observer(
    filter: world.FilterBuilder().Term<Position>(),
    observer: world.ObserverBuilder().Event<MyEvent>(),
    callback: (Iter it, int i) =>
    {
        Console.WriteLine($" - {it.Event().Name()}: {it.EventId().Str()}: {it.Entity(i).Name()}");
    }
);

// The observer filter can be matched against the entity, so make sure it
// has the Position component before emitting the event. This does not
// trigger the observer yet.
Entity e = world.Entity("e")
    .Set(new Position { X = 10, Y = 20 });

// Emit the custom event
world.Event<MyEvent>()
    .Id<Position>()
    .Entity(e)
    .Emit();

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Create tag type to use as event (could also use entity)
public struct MyEvent { }

#endif

// Output:
//  - MyEvent: Position: e
