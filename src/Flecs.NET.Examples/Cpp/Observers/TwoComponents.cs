using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Observers_TwoComponents
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
            .Set<Position>(new(10, 20));

        // Set Velocity (emits EcsOnSet, matches observer)
        e.Set<Velocity>(new(1, 2));
    }
}

// Output:
//  - OnSet: Velocity: e: P: (10, 20) V: (1, 2)
