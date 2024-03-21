using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

// Tags
file struct Walking;

public static class Cpp_Entities_Basics
{
    public static void Main()
    {
        using World world = World.Create();

        // Create an entity with name Bob
        Entity bob = world.Entity("Bob")
            // The set operation finds or creates a component, and sets it.
            // Components are automatically registered with the world.
            .Set<Position>(new(10, 20))
            // The add operation adds a component without setting a value. This is
            // useful for tags, or when adding a component with its default value.
            .Add<Walking>();

        // Get the value for the Position component
        ref Position ptr = ref bob.Ensure<Position>();
        Console.WriteLine($"({ptr.X}, {ptr.Y})");

        // Overwrite the value of the Position component
        bob.Set<Position>(new(20, 30));

        // Create another named entity
        Entity alice = world.Entity("Alice")
            .Set<Position>(new(10, 20));

        // Add a tag after entity is created
        alice.Add<Walking>();

        // Print all of the components the entity has. This will output:
        //    [Cpp_Entities_Basics.Position, Cpp_Entities_Basics.Walking, (Identifier,Name)]
        Console.WriteLine($"[{alice.Type().Str()}]");

        // Remove tag
        alice.Remove<Walking>();

        world.Each((Entity e, ref Position p) =>
        {
            Console.WriteLine($"{e}: ({p.X}, {p.Y})");
        });
    }
}

// Output:
// (10, 20)
// [Position, Walking, (Identifier,Name)]
// Alice: (10, 20)
// Bob: (20, 30)
