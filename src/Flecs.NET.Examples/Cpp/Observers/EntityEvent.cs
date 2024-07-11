// Entity events are events that are emitted and observed for a specific entity.
// They are a thin wrapper around regular observers, which match against queries
// instead of single entities. While they work similarly under the hood, entity
// events provide a much simpler API.
//
// An entity event only needs two pieces of data:
// - The entity on which to emit the event
// - The event to emit

using Flecs.NET.Core;

// An event without payload.
file struct Click;

// An event with payload.
file record struct Resize(double Width, double Height);

public static class Cpp_Observers_EntityEvent
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

        // Emit the Click event.
        widget.Emit<Click>();

        // Emit the Resize event.
        widget.Emit(new Resize(100, 200));
    }
}

// Output:
// Clicked on MyWidget!
// Clicked!
// Resized to (100, 200)!
