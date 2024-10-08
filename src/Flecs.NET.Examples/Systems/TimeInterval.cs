// This example shows how to run a system at a specified time interval.

using Flecs.NET.Core;

public static class Systems_TimeInterval
{
    public static void Main()
    {
        using World world = World.Create();

        world.System("Tick")
            .Interval(1.0f)
            .Run(Tick);

        world.System("FastTick")
            .Interval(0.5f)
            .Run(Tick);

        // Run the main loop at 60 FPS
        world.SetTargetFps(60);

        while (world.Progress()) { }
    }

    private static void Tick(Iter it)
    {
        Console.WriteLine(it.System().ToString());

        // Quit after 2 seconds.
        if (it.World().GetInfo().WorldTimeTotal > 2)
            it.World().Quit();
    }
}

// Output:
// FastTick
// Tick
// FastTick
// FastTick
// Tick
// FastTick
