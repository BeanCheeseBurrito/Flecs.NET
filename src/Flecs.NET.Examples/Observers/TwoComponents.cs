// An observer can match multiple components/tags. Only entities that match the
// entire observer query will be forwarded to the callback. For example, an
// observer for Position,Velocity won't match an entity that only has Position.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Observers_TwoComponents
{
    public static void Main()
    {
        using World world = World.Create();

        // Create observer for custom event
        world.Observer<Position, Velocity>()
            .Event(Ecs.OnSet)
            .Each((Iter it, int i, ref Position p, ref Velocity v) =>
            {
                Console.Write($" - {it.Event()}: ");
                Console.Write($"{it.EventId()}: ");
                Console.Write($"{it.Entity(i)}: ");
                Console.Write($"P: ({p.X}, {p.Y}) ");
                Console.Write($"V: ({v.X}, {v.Y})");
                Console.WriteLine();
            });

        // Create entity, set Position (emits EcsOnSet, does not yet match observer)
        Entity e = world.Entity("e")
            .Set(new Position(10, 20));

        // Set Velocity (emits EcsOnSet, matches observer)
        e.Set(new Velocity(1, 2));
    }
}

// Output:
//  - OnSet: Velocity: e: P: (10, 20) V: (1, 2)
