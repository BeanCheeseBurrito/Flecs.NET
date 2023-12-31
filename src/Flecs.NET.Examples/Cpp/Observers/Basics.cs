// Observers provide a mechanism for responding to builtin and user defined
// events. They are similar to systems, in that they have the same callback
// signature and use the same query interface to match with entities, but
// instead of a phase they have an event kind.
//
// The most commonly used builtin events are:
//  - Ecs.OnAdd: a component was added
//  - Ecs.OnRemove: a component was removed
//  - Ecs.OnSet: a component's value was changed
//
// The OnAdd and OnRemove events are only thrown when a component is
// actually added or removed. If an application invokes add and the entity
// already has the component, no event is emitted. Similarly, if an application
// invokes remove for a component the entity doesn't have, no event is
// emitted. That is in contrast to OnSet, which is invoked each time set
// or modified is invoked.
//
// Observers are different from component hooks in a number of ways:
//  - A component can only have one hook, whereas it can match many observers
//  - A hook matches one component, whereas observers can match complex queries
//  - Hooks are for add/set/remove events, observers can match custom events.

using Flecs.NET.Core;

file record struct Position(float X, float Y);

public static class Cpp_Observers_Basics
{
    public static void Main()
    {
        using World world = World.Create();

        // Create an observer for three events
        world.Observer<Position>()
            .Event(Ecs.OnAdd)
            .Event(Ecs.OnRemove)
            .Event(Ecs.OnSet)
            .Each((Iter it, int i, ref Position p) =>
            {
                if (it.Event() == Ecs.OnAdd)
                {
                    // No assumptions about the component value should be made here. If
                    // a ctor for the component was registered it will be called before
                    // the EcsOnAdd event, but a value assigned by set won't be visible.
                    Console.WriteLine($" - OnAdd: {it.EventId().Str()}: {it.Entity(i)}");
                }
                else
                {
                    // EcsOnSet or EcsOnRemove event
                    Console.WriteLine($" - OnAdd: {it.Event()}: {it.EventId()}: {it.Entity(i)}: ({p.X}, {p.Y})");
                }
            });

        // Create entity, set Position (emits EcsOnAdd and EcsOnSet)
        Entity e = world.Entity("e")
            .Set<Position>(new(10, 20));

        // Remove component (emits EcsOnRemove)
        e.Remove<Position>();

        // Remove component again (no event is emitted)
        e.Remove<Position>();
    }
}

// Output:
 // - OnAdd: Position: e
 // - OnAdd: OnSet: Position: e: (10, 20)
 // - OnAdd: OnRemove: Position: e: (10, 20)
