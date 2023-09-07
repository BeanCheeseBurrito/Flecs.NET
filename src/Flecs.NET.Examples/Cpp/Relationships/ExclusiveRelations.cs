#if Cpp_Relationships_ExclusiveRelations

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

// Register Platoon as exclusive relationship. This ensures that an entity
// can only belong to a single Platoon.
world.Component<Platoon>().Entity.Add(EcsExclusive);

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
// both removes (Platoon, platoon_1) and adds (Platoon, platoon_2) in a
// single operation.
unit.Add<Platoon>(platoon2);

Console.WriteLine($"Unit in platoon 1: {unit.Has<Platoon>(platoon1)}");
Console.WriteLine($"Unit in platoon 2: {unit.Has<Platoon>(platoon2)}");

// Type for Platoon relationship
public struct Platoon { }

#endif

// Output:
// Unit in platoon 1: True
// Unit in platoon 2: False
//
// Unit in platoon 1: False
// Unit in platoon 2: True
