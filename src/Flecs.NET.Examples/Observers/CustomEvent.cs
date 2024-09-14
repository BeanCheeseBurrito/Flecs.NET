// Observers can be used to match custom events. Custom events can be emitted
// using the ecs_emit function. This function is also used by builtin events,
// so builtin and custom events use the same rules for matching with observers.
//
// An event consists out of three pieces of data used to match with observers:
//  - An single event kind (EcsOnAdd, EcsOnRemove, ...)
//  - One or more event ids (Position, Velocity, ...)
//  - A source (either an entity or a table)

using Flecs.NET.Core;

file record struct Position(float X, float Y);

// Create tag type to use as event (could also use entity)
file struct MyEvent;

public static class Observers_CustomEvent
{
    public static void Main()
    {
        using World world = World.Create();

        // Create observer for custom event
        world.Observer<Position>()
            .Event<MyEvent>()
            .Each((Iter it, int i, ref Position _) =>
            {
                Console.WriteLine($" - {it.Event()}: {it.EventId()}: {it.Entity(i)}");
            });

        // The observer query can be matched against the entity, so make sure it
        // has the Position component before emitting the event. This does not
        // trigger the observer yet.
        Entity e = world.Entity("e")
            .Set(new Position(10, 20));

        // Emit the custom event
        world.Event<MyEvent>()
            .Id<Position>()
            .Entity(e)
            .Emit();
    }
}

// Output:
//  - MyEvent: Position: e
