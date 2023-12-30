// Prefabs can inherit from each other, which creates prefab variants. With
// variants applications can reuse a common set of components and specialize it
// by adding or overriding components on the variant.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Attack(float Value);
file record struct Defense(float Value);
file record struct FreightCapacity(float Value);
file record struct ImpulseSpeed(float Value);

public static class Cpp_Prefabs_Variant
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a base prefab for SpaceShips.
        Entity spaceShip = world.Prefab("SpaceShip")
            .Set<ImpulseSpeed>(new(50))
            .Set<Defense>(new(25));

        // Create a Freighter variant which inherits from SpaceShip
        Entity freighter = world.Prefab("Freighter")
            .IsA(spaceShip)
            .Set<FreightCapacity>(new(100))
            .Set<Defense>(new(50));

        // Create a MammothFreighter variant which inherits from Freighter
        Entity mammothFreighter = world.Prefab("MammothFreighter")
            .IsA(freighter)
            .Set<FreightCapacity>(new(500));

        // Create a Frigate variant which inherits from SpaceShip
        world.Prefab("Frigate")
            .IsA(spaceShip)
            .Set<Attack>(new(100))
            .Set<Defense>(new(75))
            .Set<ImpulseSpeed>(new(125));

        // Create an instance of the MammothFreighter. This entity will inherit the
        // ImpulseSpeed from SpaceShip, Defense from Freighter and FreightCapacity
        // from MammothFreighter.
        Entity inst = world.Entity("NyFreighter").IsA(mammothFreighter);

        // Add a private Position component.
        inst.Set(new Position { X = 10, Y = 20 });

        // Instances can override inherited components to give them a private copy
        // of the component. This freighter got an armor upgrade:
        inst.Set<Defense>(new(100));

        // Queries can match components from multiple levels of inheritance
        world.Each((Entity e, ref Position p, ref ImpulseSpeed s, ref Defense d, ref FreightCapacity c) =>
        {
            Console.WriteLine(e.Name());
            Console.WriteLine($" - Position: {p.X}, {p.Y}");
            Console.WriteLine($" - Impulse Speed: {s.Value}");
            Console.WriteLine($" - Defense: {d.Value}");
            Console.WriteLine($" - Capacity: {c.Value}");
        });
    }
}

// Output:
// NyFreighter
//  - Position: 10, 20
//  - Impulse Speed: 50
//  - Defense: 100
//  - Capacity: 500
