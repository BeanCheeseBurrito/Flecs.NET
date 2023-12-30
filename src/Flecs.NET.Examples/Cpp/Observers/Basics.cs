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
