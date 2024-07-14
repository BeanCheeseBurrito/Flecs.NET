// This is the same example as CustomEvent, but instead of Emit() the example
// uses the Enqueue() method. Whereas Emit() invokes observers synchronously,
// Enqueue() adds the event to the command queue, which delays invoking
// observers until the next time the command queue is flushed.

using Flecs.NET.Core;

// Component
file record struct Position(float X, float Y);

// Create tag type to use as event (could also use entity)
file struct MyEvent;

public static class Observers_EnqueueEvent
{
    public static void Main()
    {
        using World world = World.Create();

        // Create observer for custom event
        world.Observer<Position>()
            .Event<MyEvent>()
            .Each((Iter it, int i) =>
            {
                Console.WriteLine($" - {it.Event()}: {it.EventId()}: {it.Entity(i)}");
            });

        // The observer query can be matched against the entity, so make sure it
        // has the Position component before emitting the event. This does not
        // trigger the observer yet.
        Entity e = world.Entity("e")
            .Set(new Position(10, 20));

        // We can only call enqueue events while the world is deferred mode.
        world.DeferBegin();

        // Emit the custom event
        world.Event<MyEvent>()
            .Id<Position>()
            .Entity(e)
            .Enqueue();

        Console.WriteLine("Event enqueued!");

        // Flushes the queue, and invokes the observer
        world.DeferEnd();
    }
}

// Output:
// Event enqueued!
//  - MyEvent: Position: e
