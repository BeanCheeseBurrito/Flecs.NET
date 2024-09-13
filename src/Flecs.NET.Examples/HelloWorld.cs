using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Velocity(float X, float Y);

// Tags
file struct Eats;
file struct Apples;

public static class HelloWorld
{
    public static void Main()
    {
        using World world = World.Create();

        // Register a system
        world.System<Position, Velocity>()
            .Each((ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
            });

        // Create an entity with name Bob, add Position and food preference
        Entity bob = world.Entity("Bob")
            .Set(new Position(0, 0))
            .Set(new Velocity(1, 2))
            .Add<Eats, Apples>();

        // Show us what you got
        Console.WriteLine($"{bob.Name()}'s got [{bob.Type()}]");

        // Run systems twice. Usually this function is called once per frame
        world.Progress();
        world.Progress();

        // See if Bob has moved (he has)
        ref readonly Position p = ref bob.Get<Position>();
        Console.WriteLine($"{bob.Name()}'s position is ({p.X}, {p.Y})");
    }
}

// Output:
// Bob's got [Position, Velocity, (Identifier,Name), (Eats,Apples)]
// Bob's position is (2, 4)
