// Prefabs are entities that can be used as templates for other entities. They
// are created with a builtin Prefab tag, which by default excludes them from
// queries and systems.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Attack(float Value);
file record struct Defense(float Value);
file record struct FreightCapacity(float Value);
file record struct ImpulseSpeed(float Value);

// Tags
file struct HasFTL;

public static class Cpp_Entities_Prefab
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a prefab hierarchy.
        Entity spaceship = world.Prefab("Spaceship")
            // Add components to prefab entity as usual
            .Set<ImpulseSpeed>(new(50))
            .Set<Defense>(new(50))

            // By default components in an inheritance hierarchy are shared between
            // entities. The override function ensures that instances have a private
            // copy of the component.
            .SetAutoOverride<Position>(default);

        Entity freighter = world.Prefab("Freighter")
            // Short for .Add(Ecs.IsA, spaceship). This ensures the entity
            // inherits all components from spaceship.
            .IsA(spaceship)
            .Add<HasFTL>()
            .Set<FreightCapacity>(new(100))
            .Set<Defense>(new(100));

        Entity mammothFreighter = world.Prefab("MammothFreighter")
            .IsA(freighter)
            .Set<FreightCapacity>(new(500))
            .Set<Defense>(new(300));

        world.Prefab("Frigate")
            .IsA(spaceship)
            .Add<HasFTL>()
            .Set<Attack>(new(100))
            .Set<Defense>(new(75))
            .Set<ImpulseSpeed>(new(125));

        // Create a regular entity from a prefab.
        // The instance will have a private copy of the Position component, because
        // of the override in the spaceship entity. All other components are shared.
        Entity inst = world.Entity("MyMammothFreighter")
            .IsA(mammothFreighter);

        // Inspect the type of the entity. This outputs:
        //    Position,(Identifier,Name),(IsA,MammothFreighter)
        Console.WriteLine($"Instance type: [{inst.Type().Str()}]");

        // Even though the instance doesn't have a private copy of ImpulseSpeed, we
        // can still get it using the regular API (outputs 50)
        ref readonly ImpulseSpeed reference = ref inst.Get<ImpulseSpeed>();
        Console.WriteLine($"Impulse speed: {reference.Value}");

        // Prefab components can be iterated just like regular components:
        world.Each((Entity e, ref ImpulseSpeed s, ref Position p) =>
        {
            p.X += s.Value;
            Console.WriteLine($"{e}: ({p.X:0.0}, {p.Y:0.0})");
        });
    }
}

// Output:
// Instance type: [Position, (Identifier,Name), (IsA,MammothFreighter)]
// Impulse speed: 50
// MyMammothFreighter: (50.0, 0.0)
