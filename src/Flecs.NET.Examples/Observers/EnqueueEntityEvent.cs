// This is the same example as EntityEvent, but instead of Emit() the example
// uses the Enqueue() method. Whereas Emit() invokes observers synchronously,
// Enqueue() adds the event to the command queue, which delays invoking
// observers until the next time the command queue is flushed.

using Flecs.NET.Core;

// An event without payload.
file struct Click;

// An event with payload.
file record struct Resize(float Width, float Height);

public static class Observers_EnqueueEntityEvent
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a widget entity.
        Entity widget = world.Entity("MyWidget");

        // Observe the Click event on the widget entity.
        widget.Observe<Click>(() =>
        {
            Console.WriteLine($"Clicked!");
        });

        // Observers can have an entity argument that holds the event source.
        // This allows the same function to be reused for different entities.
        widget.Observe<Click>((Entity src) =>
        {
            Console.WriteLine($"Clicked on {src.Path()}!");
        });

        // Observe the Resize event. Events with payload are passed as arguments
        // to the observer callback.
        widget.Observe((ref Resize p) =>
        {
            Console.WriteLine($"Resized to ({p.Width}, {p.Height})!");
        });

        // We can only call enqueue events while the world is deferred mode.
        world.DeferBegin();

        // Emit the Click event.
        widget.Enqueue<Click>();

        // Emit the Resize event.
        widget.Enqueue(new Resize(100, 200));

        Console.WriteLine("Events enqueued!");

        // Flushes the queue, and invokes the observer
        world.DeferEnd();
    }
}

// Output:
// Events enqueued!
// Clicked on MyWidget!
// Clicked!
// Resized to (100, 200)!
