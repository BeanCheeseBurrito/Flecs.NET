using Flecs.NET.Core;

// Components
file record struct Position(double X, double Y);
file record struct Velocity(double X, double Y);

// Tag
file struct Human;

// Two tags used to create a pair
file struct Eats;
file struct Apples;

public static class Cpp_Entities_IterateComponents
{
    public static void Main()
    {
        using World world = World.Create();

        Entity bob = world.Entity()
            .Set<Position>(new Position(10, 20))
            .Set<Velocity>(new Velocity(1, 1))
            .Add<Human>()
            .Add<Eats, Apples>();

        Console.WriteLine("Bob's components:");
        IterateComponents(bob);

        // We can use the same function to iterate the components of a component
        Console.WriteLine("Position's components:");
        IterateComponents(world.Component<Position>().Entity);
    }

    private static void IterateComponents(Entity e)
    {
        // 1. The easiest way to print the components is to use Types.Str
        Console.WriteLine(e.Type().Str() + "\n");

        // 2. To get individual component ids, use Entity.Each
        int i = 0;
        e.Each((Id id) => Console.WriteLine($"{i++}: {id}"));
        Console.WriteLine();

        // 3. we can also inspect and print the ids in our own way. This is a
        // bit more complicated as we need to handle the edge cases of what can be
        // encoded in an id, but provides the most flexibility.
        i = 0;
        e.Each((Id id) =>
        {
            Console.Write(i++ + ": ");

            if (id.IsPair())
            {
                // If id is a pair, extract & print both parts of the pair
                Entity rel = id.First();
                Entity tgt = id.Second();
                Console.Write($"Rel: {rel}, Target: {tgt}");
            }
            else
            {
                // Id contains a regular entity. Strip role before printing.
                Entity comp = id.Entity();
                Console.Write($"Entity: {comp}");
            }

            Console.WriteLine();
        });

        Console.WriteLine("\n");
    }
}

// Output:
// Bob's components:
// Position, Velocity, Human, (Eats,Apples)
//
// 0: Position
// 1: Velocity
// 2: Human
// 3: (Eats,Apples)
//
// 0: Entity: Position
// 1: Entity: Velocity
// 2: Entity: Human
// 3: Rel: Eats, Target: Apples
//
//
// Position's components:
// Component, (Identifier,Name), (Identifier,Symbol), (OnDelete,Panic)
//
// 0: Component
// 1: (Identifier,Name)
// 2: (Identifier,Symbol)
// 3: (OnDelete,Panic)
//
// 0: Entity: Component
// 1: Rel: Identifier, Target: Name
// 2: Rel: Identifier, Target: Symbol
// 3: Rel: OnDelete, Target: Panic
