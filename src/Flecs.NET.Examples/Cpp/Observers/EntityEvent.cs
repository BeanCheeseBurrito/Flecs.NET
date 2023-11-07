// Entity events are events that are emitted and observed for a specific entity.
// They are a thin wrapper around regular observers, which match against queries
// instead of single entities. While they work similarly under the hood, entity
// events provide a much simpler API.
//
// An entity event only needs two pieces of data:
// - The entity on which to emit the event
// - The event to emit

#if Cpp_Observers_EntityEvent

using Flecs.NET.Core;

{
    using World world = World.Create();

    // Create a widget entity
    Entity widget = world.Entity("MyWidget");

    // Observe the OnClick event on the widget entity
    widget.Observe<OnClick>((Iter it) =>
    {
        // The event source can be obtained with it.Src(1). This allows the same
        // event function to be used for different entities.
        Console.WriteLine($"Clicked on {it.Src(1).Path()}!");
    });

    // Emit the OnClick event for the widget
    widget.Emit<OnClick>();
}

// The event to emit.
public struct OnClick { }

#endif

// Output:
// Clicked on MyWidget!
