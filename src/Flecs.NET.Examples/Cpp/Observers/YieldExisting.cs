// Observers can enable a "YieldExisting" feature that upon creation of the
// observer produces events for all entities that match the observer query. The
// feature is only implemented for the builtin EcsOnAdd and EcsOnSet events.
//
// Custom events can also implement behavior for YieldExisting by adding the
// Iterable component to the event (see EcsIterable for more details).

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Cpp_Observers_YieldExisting
{
    public static void Main()
    {
        using World world = World.Create();

        // Create existing entities with Position component
        world.Entity("e1").Set<Position>(new Position(10, 20));
        world.Entity("e2").Set<Position>(new Position(20, 30));

        // Create observer for Position
        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .YieldExisting()
            .Each((Iter it, int i, ref Position p) =>
            {
                Console.Write($" - {it.Event()}: ");
                Console.Write($"{it.EventId()}: ");
                Console.Write($"{it.Entity(i)}: ");
                Console.Write($"({p.X}, {p.Y})");
                Console.WriteLine();
            });
    }
}

// Output:
//  - OnSet: Position: e1: (10, 20)
//  - OnSet: Position: e2: (20, 30)
