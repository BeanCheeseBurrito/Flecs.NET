#if Cpp_Systems_TimeInterval

using Flecs.NET.Core;

void Tick(Iter it)
{
    Console.WriteLine(it.System().ToString());
}

using World world = World.Create();

world.Routine(
    name: "Tick",
    routine: world.RoutineBuilder().Interval(1.0f),
    callback: Tick
);

world.Routine(
    name: "FastTick",
    routine: world.RoutineBuilder().Interval(0.5f),
    callback: Tick
);

// Run the main loop at 60 FPS
world.SetTargetFps(60);

while (world.Progress()) { }

#endif

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
