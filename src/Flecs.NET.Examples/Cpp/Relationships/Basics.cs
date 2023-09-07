#if Cpp_Relationships_Basics

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

// Entity used for Grows relationship
Entity grows = world.Entity("Grows");

// Relationship objects
Entity apples = world.Entity("Apples");
Entity pears = world.Entity("Pears");

// Create an entity with 3 relationships. Relationships are like regular components,
// but combine two types/identifiers into an (relationship, object) pair.
Entity bob = world.Entity("Bob")
    // Pairs can be constructed from a type and entity
    .Add<Eats>(apples)
    .Add<Eats>(pears)
    // Pairs can also be constructed from two entity ids
    .Add(grows, pears);

// Has can be used with relationships as well
Console.WriteLine($"Bob eats apples? {bob.Has<Eats>(apples)}");

// Wildcards can be used to match relationships
Console.WriteLine($"Bob grows food? {bob.Has(grows, EcsWildcard)}");

// Print the type of the entity. Should output:
//   (Identifier,Name),(Eats,Apples),(Eats,Pears),(Grows,Pears)
Console.WriteLine($"Bob's type: [{bob.Type().Str()}]");

// Relationships can be iterated for an entity. This iterates (Eats, *):
bob.Each<Eats>((Entity second) =>
{
    Console.WriteLine($"Bob eats {second.Name()}");
});

// Iterate by explicitly providing the pair. This iterates (*, Pears):
bob.Each(EcsWildcard, pears, (Id id) =>
{
    Console.WriteLine($"Bob {id.First().Name()} pears");
});

public struct Eats { }

#endif

// Output:
// Bob eats apples? True
// Bob grows food? True
// Bob's type: [(Identifier,Name), (Eats,Apples), (Eats,Pears), (Grows,Pears)]
// Bob eats Apples
// Bob eats Pears
// Bob Eats pears
// Bob Grows pears
