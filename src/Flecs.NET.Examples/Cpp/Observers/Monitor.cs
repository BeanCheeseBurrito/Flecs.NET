using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Observers_Monitor
{
    public static void Main()
    {
        using World world = World.Create();

        // Create observer for custom event
        world.Observer<Position, Velocity>()
            .Event(Ecs.MonitorId)
            .Each((Iter it, int i) =>
            {
                if (it.Event() == Ecs.OnAdd)
                    Console.WriteLine($" - Enter: {it.EventId()}: {it.Entity(i)}");
                else if (it.Event() == Ecs.OnRemove)
                    Console.WriteLine($" - Leave: {it.EventId()}: {it.Entity(i)}");
            });

        // Create entity
        Entity e = world.Entity("e");

        // This does not yet trigger the monitor, as the entity does not yet match.
        e.Set<Position>(new(10, 20));

        // This triggers the monitor with EcsOnAdd, as the entity now matches.
        e.Set<Velocity>(new(1, 2));

        // This triggers the monitor with EcsOnRemove, as the entity no longer matches.
        e.Remove<Position>();
    }
}

// Output:
//  - Enter: Velocity: e
//  - Leave: Position: e
