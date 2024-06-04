using Flecs.NET.Core;

public static class Cpp_Systems_DeltaTime
{
    public static void Main()
    {
        using World world = World.Create();

        // Create system that prints delta_time. This system doesn't query for any
        // components which means it won't match any entities, but will still be ran
        // once for each call to ecs_progress.
        world.Routine()
            .Run((Iter it) =>
            {
                Console.WriteLine($"Delta time: {it.DeltaTime()}");
            });

        // Call progress with 0.0f for the delta time parameter. This will cause
        // ecs_progress to measure the time passed since the last frame. The
        // delta time of the first frame is a best guess (16ms).
        world.Progress();

        // The following calls should print a delta time of approximately 100ms

        Thread.Sleep(100);
        world.Progress();

        Thread.Sleep(100);
        world.Progress();
    }
}

// Output:
// Delta time: 0.016666668
// Delta time: 0.10039652
// Delta time: 0.10021683
