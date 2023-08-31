#if Cpp_Entities_Basics

using Flecs.NET.Core;

using World world = World.Create();

// Create an entity with name Bob
Entity bob = world.Entity("Bob")
    // The set operation finds or creates a component, and sets it.
    // Components are automatically registered with the world.
    .Set(new Position { X = 10, Y = 20 })
    // The add operation adds a component without setting a value. This is
    // useful for tags, or when adding a component with its default value.
    .Add<Walking>();

// Get the value for the Position component
ref readonly Position ptr = ref bob.Get<Position>();
Console.WriteLine($"({ptr.X}, {ptr.Y})");

// Overwrite the value of the Position component
bob.Set(new Position { X = 20, Y = 30 });

// Create another named entity
Entity alice = world.Entity("Alice")
    .Set(new Position { X = 10, Y = 20 });

// Add a tag after entity is created
alice.Add<Walking>();

// Print all of the components the entity has. This will output:
//    Position, Walking, (Identifier,Name)
Console.WriteLine($"[{alice.Types().Str()}]");

// Remove tag
alice.Remove<Walking>();

// Iterate all entities with Position
using Filter filter = world.Filter(
    filter: world.FilterBuilder().Term<Position>()
);

filter.Iter(it =>
{
    Column<Position> p = it.Field<Position>(1);
    foreach (int i in it)
        Console.WriteLine($"{it.Entity(i).Name()}: ({p[i].X}, {p[i].Y})");
});

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Walking
{
}

// Output
// (10, 20)
// [Position, Walking, (Identifier,Name)]
// Alice: (10, 20)
// Bob: (20, 30)

#endif
