// This is the same example as CustomEvent.cs, but instead of Emit() the example
// uses the Enqueue() method. Whereas Emit() invokes observers synchronously,
// Enqueue() adds the event to the command queue, which delays invoking
// observers until the next time the command queue is flushed.

#if Cpp_Observers_EnqueueEvent

using Flecs.NET.Core;

{
    using World world = World.Create(args);

    // Create observer for custom event
    world.Observer(
        filter: world.FilterBuilder<Position>(),
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

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Create tag type to use as event (could also use entity)
public struct MyEvent { }

#endif

// Output:
// Event enqueued!
//  - MyEvent: Position: e
