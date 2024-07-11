using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Mass(float Value);

public static class Cpp_Entities_MultiSetGet
{
    public static void Main()
    {
        using World world = World.Create();

        // Create new entity, set Position and Mass component
        Entity e = world.Entity()
            .Insert((ref Position p, ref Mass m) =>
            {
                p.X = 10;
                p.Y = 20;
                m.Value = 100;
            });

        // Print values of Position and Mass component
        bool found = e.Read((in Position p, in Mass m) =>
        {
            Console.WriteLine($"Position: ({p.X}, {p.Y})");
            Console.WriteLine($"Mass: ({m.Value})");
        });

        Console.WriteLine($"Components Found: {found}");
    }
}

// Output:
// Position: (10, 20)
// Mass: (100)
// Components Found: True
