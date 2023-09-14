// Startup systems are systems registered with the EcsOnStart phase, and are
// only ran during the first frame. Just like with regular phases, custom phases
// can depend on the EcsOnStart phase (see CustomPhases example). Phases that
// depend on EcsOnStart are also only ran during the first frame.
//
// Other than that, startup systems behave just like regular systems (they can
// match components, can introduce merge points), with as only exception that
// they are guaranteed to always run on the main thread.

#if Cpp_Systems_StartupSystem

using Flecs.NET.Core;

using World world = World.Create();

// Startup system
world.Routine(
    name: "Startup",
    routine: world.RoutineBuilder().Kind(Ecs.OnStart),
    callback: (Iter it) =>
    {
        Console.WriteLine(it.System().ToString());
    }
);

// Regular system
world.Routine(
    name: "Update",
    callback: (Iter it) =>
    {
        Console.WriteLine(it.System().ToString());
    }
);

// First frame. This runs both the Startup and Update systems
world.Progress();

// Second frame. This runs only the Update system
world.Progress();

#endif

// Output:
// Startup
// Update
// Update
