using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Cpp_Observers_YieldExisting
{
    public static void Main()
    {
        using World world = World.Create();

        // Create existing entities with Position component
        world.Entity("e1").Set<Position>(new(10, 20));
        world.Entity("e2").Set<Position>(new(20, 30));

        // Create observer for Position
        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .YieldExisting()
            .Each((Iter it, int i, ref Position p) =>
            {
                Console.Write($" - {it.Event()}: ");
                Console.Write($"{it.EventId()}: ");
                Console.Write($"{it.Entity(i)}: ");
                Console.Write($"({p.X}, {p.Y})");
                Console.WriteLine();
            });
    }
}

// Output:
//  - OnSet: Position: e1: (10, 20)
//  - OnSet: Position: e2: (20, 30)
