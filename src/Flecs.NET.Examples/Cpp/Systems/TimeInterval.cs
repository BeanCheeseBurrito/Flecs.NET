// This example shows how to run a system at a specified time interval.

using Flecs.NET.Core;

public static class Cpp_Systems_TimeInterval
{
    public static void Main()
    {
        using World world = World.Create();

        world.Routine("Tick")
            .Interval(1.0f)
            .Iter(Tick);

        world.Routine("FastTick")
            .Interval(0.5f)
            .Iter(Tick);

        // Run the main loop at 60 FPS
        world.SetTargetFps(60);

        while (world.Progress()) { }
    }

    private static void Tick(Iter it)
    {
        Console.WriteLine(it.System().ToString());
    }
}

// Output:
// FastTick
// Tick
// FastTick
// FastTick
// Tick
// FastTick
// FastTick
// Tick
// FastTick
// FastTick
// ...
