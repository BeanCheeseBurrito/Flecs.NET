#if Cpp_Entities_IterateComponents

using Flecs.NET.Core;

void IterateComponents(Entity e) {
    // 1. The easiest way to print the components is to use type::str
    Console.WriteLine(e.Types().Str() + "\n");

    // 2. To get individual component ids, use entity::each
    int i = 0;
    e.Each((Id id) => Console.WriteLine($"{i++}: {id.Str()}"));
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
            Entity rel = id.First();
            Entity tgt = id.Second();
            Console.Write($"rel: {rel.Name()}, tgt: {tgt.Name()}");
        }
        else
        {
            Entity comp = id.Entity();
            Console.Write($"entity: {comp.Name()}");
        }

        Console.WriteLine();
    });

    Console.WriteLine("\n");
}

using World world = World.Create();

Entity bob = world.Entity()
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 1, Y = 1 })
    .Add<Human>()
    .Add<Eats, Apples>();

Console.WriteLine("Bob's components:");
IterateComponents(bob);

Console.WriteLine("Position's components:");
IterateComponents(world.Component<Position>().Entity);

// Ordinary components
public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Velocity
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Tag
public struct Human { }

// Two tags used to create a pair
public struct Eats { }
public struct Apples { }

#endif

// Output:
// Bob's components:
// Position, Velocity, Human, (Eats,Apples)
//
// 0: Position
// 1: Velocity
// 2: Human
// 3: (Eats,Apples)
//
// 0: entity: Position
// 1: entity: Velocity
// 2: entity: Human
// 3: rel: Eats, tgt: Apples
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
// 0: entity: Component
// 1: rel: Identifier, tgt: Name
// 2: rel: Identifier, tgt: Symbol
// 3: rel: OnDelete, tgt: Panic
