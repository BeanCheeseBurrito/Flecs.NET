using Flecs.NET.Core;

// Components
file record struct Position(double X, double Y);

// Tags
file struct Star;
file struct Planet;
file struct Moon;

public static class Entities_Hierarchy
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a simple hierarchy.
        // Hierarchies use ECS relationships and the builtin EcsChildOf relationship to
        // create entities as children of other entities.

        Entity sun = world.Entity("Sun")
            .Add<Star>()
            .Set(new Position(1, 1));

        world.Entity("Mercury")
            .ChildOf(sun) // Shortcut for Add(Ecs.ChildOf, sun)
            .Add<Planet>()
            .Set(new Position(1, 1));

        world.Entity("Venus")
            .ChildOf(sun)
            .Add<Planet>()
            .Set(new Position(2, 2));

        Entity earth = world.Entity("Earth")
            .ChildOf(sun)
            .Add<Planet>()
            .Set(new Position(3, 3));

        Entity moon = world.Entity("Moon")
            .ChildOf(earth)
            .Add<Moon>()
            .Set(new Position(0.1, 0.1));

        // Is the Moon a child of Earth?
        Console.WriteLine($"Child of Earth? {moon.IsChildOf(earth)}\n");

        // Lookup the moon by name
        Console.WriteLine($"Moon found: {world.Lookup("Sun.Earth.Moon").Path()}\n");

        // Do a depth-first walk of the tree
        IterateTree(sun);

        return;

        static void IterateTree(Entity e, Position pParent = default)
        {
            // Print hierarchical name of entity & the entity type
            Console.WriteLine($"{e.Path()} [{e.Type().Str()}]");

            // Get entity position
            ref readonly Position p = ref e.Get<Position>();

            // Calculate actual position
            Position pActual = new(p.X + pParent.X, p.Y + pParent.Y);
            Console.WriteLine($"({pActual.X}, {pActual.Y})");
            Console.WriteLine();

            // Iterate children recursively
            e.Children((Entity child) => IterateTree(child, pActual));
        }
    }
}

// Output:
// Child of Earth? True
//
// Moon found: .Sun.Earth.Moon
//
// .Sun [Star, Position, (Identifier,Name)]
// (1, 1)
//
// .Sun.Mercury [Position, Planet, (Identifier,Name), (ChildOf,Sun)]
// (2, 2)
//
// .Sun.Venus [Position, Planet, (Identifier,Name), (ChildOf,Sun)]
// (3, 3)
//
// .Sun.Earth [Position, Planet, (Identifier,Name), (ChildOf,Sun)]
// (4, 4)
//
// .Sun.Earth.Moon [Position, Moon, (Identifier,Name), (ChildOf,Sun.Earth)]
// (4.1, 4.1)
