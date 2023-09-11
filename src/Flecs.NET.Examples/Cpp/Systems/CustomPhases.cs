// This application demonstrates how to use custom phases for systems. The
// default pipeline will automatically run systems for custom phases as long as
// they have the Ecs.Phase tag.

#if Cpp_Systems_CustomPhases

using Flecs.NET.Core;

// Dummy system
void Sys(Iter it)
{
    Console.WriteLine("System " + it.System().Name());
}

using World world = World.Create(args);

// Create two custom phases that branch off of EcsOnUpdate. Note that the
// phases have the Phase tag, which is necessary for the builtin pipeline
// to discover which systems it should run.
Entity physics = world.Entity()
    .Add(Ecs.Phase)
    .DependsOn(Ecs.OnUpdate);

Entity collisions = world.Entity()
    .Add(Ecs.Phase)
    .DependsOn(physics);

// Create 3 dummy systems.
world.Routine(
    name: "CollisionSystem",
    routine: world.RoutineBuilder().Kind(collisions),
    callback: Sys
);

world.Routine(
    name: "PhysicsSystem",
    routine: world.RoutineBuilder().Kind(physics),
    callback: Sys
);

world.Routine(
    name: "GameSystem",
    routine: world.RoutineBuilder().Kind(Ecs.OnUpdate),
    callback: Sys
);

// Run pipeline
world.Progress();

#endif

// Output:
// System GameSystem
// System PhysicsSystem
// System CollisionSystem
