// Custom pipelines make it possible for applications to override which systems
// are ran by a pipeline and how they are ordered. Pipelines are queries under
// the hood, and custom pipelines override the query used for system matching.

// If you only want to use custom phases in addition or in place of the builtin
// phases see the CustomPhases and CustomPhasesNoBuiltIn examples, as this
// does not require using a custom pipeline.

#if Cpp_Systems_CustomPipeline

using Flecs.NET.Core;

using World world = World.Create();

// Create a pipeline that matches systems with Physics. Note that this
// pipeline does not require the use of phases (see CustomPhases) or of the
// DependsOn relationship.
Pipeline pipeline = world.Pipeline(
    filter: world.FilterBuilder()
        .With(Ecs.System) // Mandatory, must always match systems
        .With<Physics>()
);
// Configure the world to use the custom pipeline
world.SetPipeline(pipeline.Entity);

// Create system with Physics tag
world.Routine(
    routine: world.RoutineBuilder().Kind<Physics>(),
    callback: (Iter _) => { Console.WriteLine("System ran!"); }
);

// Runs the pipeline & system
world.Progress();

public struct Physics { }

#endif

// Output:
//   System ran!
