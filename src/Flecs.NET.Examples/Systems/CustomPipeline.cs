// Custom pipelines make it possible for applications to override which systems
// are ran by a pipeline and how they are ordered. Pipelines are queries under
// the hood, and custom pipelines override the query used for system matching.

// If you only want to use custom phases in addition or in place of the builtin
// phases see the CustomPhases and CustomPhasesNoBuiltIn examples, as this
// does not require using a custom pipeline.

using Flecs.NET.Core;

// Pipeline tag
struct Physics;

public static class Systems_CustomPipeline
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a pipeline that matches systems with Physics. Note that this
        // pipeline does not require the use of phases (see CustomPhases) or of the
        // DependsOn relationship.
        Pipeline pipeline = world.Pipeline()
            .With(Ecs.System) // Mandatory, must always match systems
            .With<Physics>()
            .Build();

        // Configure the world to use the custom pipeline
        world.SetPipeline(pipeline);

        // Create system with Physics tag
        world.System()
            .Kind<Physics>()
            .Run((Iter it) =>
            {
                Console.WriteLine("System ran!");
            });

        // Runs the pipeline & system
        world.Progress();
    }
}

// Output:
//   System ran!
