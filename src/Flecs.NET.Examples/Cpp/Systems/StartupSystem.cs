// Startup systems are systems registered with the Ecs.OnStart phase, and are
// only ran during the first frame. Just like with regular phases, custom phases
// can depend on the Ecs.OnStart phase (see CustomPhases example). Phases that
// depend on Ecs.OnStart are also only ran during the first frame.
//
// Other than that, startup systems behave just like regular systems (they can
// match components, can introduce merge points), with as only exception that
// they are guaranteed to always run on the main thread.

using Flecs.NET.Core;

public static class Cpp_Systems_StartupSystem
{
    public static void Main()
    {
        using World world = World.Create();

        // Startup system
        world.Routine("Startup")
            .Kind(Ecs.OnStart)
            .Iter((Iter it) =>
            {
                Console.WriteLine(it.System().ToString());
            });

        // Regular system
        world.Routine("Update")
            .Iter((Iter it) => { Console.WriteLine(it.System().ToString()); });

        // First frame. This runs both the Startup and Update systems
        world.Progress();

        // Second frame. This runs only the Update system
        world.Progress();
    }
}

// Output:
// Startup
// Update
// Update
