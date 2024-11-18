using Flecs.NET.Core;

// Type for Platoon relationship
file struct Platoon;

public static class Relationships_ExclusiveRelations
{
    public static void Main()
    {
        using World world = World.Create();

        // Register Platoon as exclusive relationship. This ensures that an entity
        // can only belong to a single Platoon.
        world.Component<Platoon>().Add(Ecs.Exclusive);

        // Create two platoons
        Entity platoon1 = world.Entity();
        Entity platoon2 = world.Entity();

        // Create a unit
        Entity unit = world.Entity();

        // Add unit to platoon 1
        unit.Add<Platoon>(platoon1);

        // Log platoon of unit
        Console.WriteLine($"Unit in platoon 1: {unit.Has<Platoon>(platoon1)}");
        Console.WriteLine($"Unit in platoon 2: {unit.Has<Platoon>(platoon2)}");
        Console.WriteLine();

        // Add unit to platoon 2. Because Platoon is an exclusive relationship, this
        // both removes (Platoon, platoon1) and adds (Platoon, platoon2) in a
        // single operation.
        unit.Add<Platoon>(platoon2);

        Console.WriteLine($"Unit in platoon 1: {unit.Has<Platoon>(platoon1)}");
        Console.WriteLine($"Unit in platoon 2: {unit.Has<Platoon>(platoon2)}");
    }
}

// Output:
// Unit in platoon 1: True
// Unit in platoon 2: False
//
// Unit in platoon 1: False
// Unit in platoon 2: True
