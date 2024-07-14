// This application demonstrates how to use custom phases for systems. The
// default pipeline will automatically run systems for custom phases as long as
// they have the Ecs.Phase tag.

using Flecs.NET.Core;

public static class Systems_CustomPhasesNoBuiltIn
{
    public static void Main()
    {
        using World world = World.Create();

        // Create three custom phases. Note that the phases have the Phase tag,
        // which is necessary for the builtin pipeline to discover which systems it
        // should run.
        Entity update = world.Entity()
            .Add(Ecs.Phase);

        Entity physics = world.Entity()
            .Add(Ecs.Phase)
            .DependsOn(update);

        Entity collisions = world.Entity()
            .Add(Ecs.Phase)
            .DependsOn(physics);

        // Create 3 dummy systems.
        world.Routine("CollisionSystem")
            .Kind(collisions)
            .Run(Sys);

        world.Routine("PhysicsSystem")
            .Kind(collisions)
            .Run(Sys);

        world.Routine("GameSystem")
            .Kind(update)
            .Run(Sys);

        // Run pipeline
        world.Progress();
    }

    private static void Sys(Iter it)
    {
        Console.WriteLine("System " + it.System().Name());
    }
}

// Output:
// System GameSystem
// System PhysicsSystem
// System CollisionSystem
