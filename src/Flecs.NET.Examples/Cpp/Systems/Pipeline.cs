using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

public static class Cpp_Systems_Pipeline
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a system for moving an entity
        world.Routine<Position, Velocity>()
            .Kind(Ecs.OnUpdate) // A phase orders a system in a pipeline
            .Each((ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
            });

        // Create a system for printing the entity position
        world.Routine<Position>()
            .Kind(Ecs.PostUpdate) // A phase orders a system in a pipeline
            .Each((Entity e, ref Position p) => { Console.WriteLine($"{e}: ({p.X}, {p.Y})"); });

        // Create a few test entities for a Position, Velocity query
        world.Entity("e1")
            .Set<Position>(new(10, 20))
            .Set<Velocity>(new(1, 2));

        world.Entity("e2")
            .Set<Position>(new(10, 20))
            .Set<Velocity>(new(3, 4));

        // Run the default pipeline. This will run all systems ordered by their
        // phase. Systems within the same phase are ran in declaration order. This
        // function is usually called in a loop.
        world.Progress();
    }
}

// Output:
// e1: (11, 22)
// e2: (13, 24)
