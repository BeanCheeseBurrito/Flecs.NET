using Flecs.NET.Core;

public static class Cpp_Systems_TargetFps
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

        // Set target FPS to 1 frame per second
        world.SetTargetFps(1);

        // Run 5 frames
        for (int i = 0; i < 5; i++)
        {
            // To make sure the frame doesn't run faster than the specified target
            // FPS ecs_progress will insert a sleep if the measured delta_time is
            // smaller than 1 / target_fps.
            //
            // By default ecs_progress uses the sleep function provided by the OS
            // which is not always very accurate. If more accuracy is required the
            // sleep function of the OS API can be overridden with a custom one.
            //
            // If a value other than 0 is provided to the delta_time argument of
            // ecs_progress, this value will be used to calculate the length of
            // the sleep to insert.
            world.Progress();
        }
    }
}

// Output:
// Delta time: 1
// Delta time: 1.0002875
// Delta time: 1.00039
// Delta time: 1.0004625
// Delta time: 1.0005361
