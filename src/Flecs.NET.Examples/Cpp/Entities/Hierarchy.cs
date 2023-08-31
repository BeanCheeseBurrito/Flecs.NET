#if Cpp_Entities_Hierarchy

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

void IterateTree(Entity e, Position pParent = default)
{
    // Print hierarchical name of entity & the entity type
    Console.WriteLine($"{e.Path()} [{e.Types().Str()}]");

    // Get entity position
    ref readonly Position p = ref e.Get<Position>();

    // Calculate actual position
    Position pActual = new() { X = p.X + pParent.X, Y = p.Y + pParent.Y };
    Console.WriteLine($"({pActual.X}, {pActual.Y})\n");

    // Iterate children recursively
    e.Children(child => IterateTree(child, pActual));
}

using World world = World.Create();

// Create a simple hierarchy.
// Hierarchies use ECS relationships and the builtin flecs::ChildOf relationship to
// create entities as children of other entities.

Entity sun = world.Entity("Sun")
    .Add<Star>()
    .Set(new Position { X = 1, Y = 1 });

world.Entity("Mercury")
    .ChildOf(sun) // Shortcut for add(flecs::ChildOf, sun)
    .Add<Planet>()
    .Set(new Position { X = 1, Y = 1 });

world.Entity("Venus")
    .ChildOf(sun)
    .Add<Planet>()
    .Set(new Position { X = 2, Y = 2 });

Entity earth = world.Entity("Earth")
    .ChildOf(sun)
    .Add<Planet>()
    .Set(new Position { X = 3, Y = 3 });

Entity moon = world.Entity("Moon")
    .ChildOf(earth)
    .Add<Moon>()
    .Set(new Position { X = 0.1, Y = 0.1 });

// Is the Moon a child of Earth?
Console.WriteLine($"Child of Earth? {moon.Has(EcsChildOf, earth)}\n");

// Do a depth-first walk of the tree
IterateTree(sun);

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Star { }
public struct Planet { }
public struct Moon { }

#endif
