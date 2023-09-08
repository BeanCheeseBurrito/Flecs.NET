// Prefabs are entities that can be used as templates for other entities. They
// are created with a builtin Prefab tag, which by default excludes them from
// queries and systems.

#if Cpp_Entities_Prefab

using Flecs.NET.Core;

using World world = World.Create();

// Create a prefab hierarchy.
Entity spaceship = world.Prefab("Spaceship")
    // Add components to prefab entity as usual
    .Set(new ImpulseSpeed { Value = 50 })
    .Set(new Defense { Value = 50 })

    // By default components in an inheritance hierarchy are shared between
    // entities. The override function ensures that instances have a private
    // copy of the component.
    .Override<Position>();

    Entity freighter = world.Prefab("Freighter")
        // Short for .Add(Ecs.IsA, spaceship). This ensures the entity
        // inherits all components from spaceship.
        .IsA(spaceship)
        .Add<HasFTL>()
        .Set(new FreightCapacity { Value = 100 })
        .Set(new Defense { Value = 100 });

        Entity mammothFreighter = world.Prefab("MammothFreighter")
            .IsA(freighter)
            .Set(new FreightCapacity { Value = 500 })
            .Set(new Defense { Value = 300 });

    world.Prefab("Frigate")
        .IsA(spaceship)
        .Add<HasFTL>()
        .Set(new Attack { Value = 100 })
        .Set(new Defense { Value = 75 })
        .Set(new ImpulseSpeed { Value = 125 });

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
ref readonly ImpulseSpeed ptr = ref inst.Get<ImpulseSpeed>();
Console.WriteLine($"Impulse speed: {ptr.Value}");

// Prefab components can be iterated just like regular components:
using Filter filter = world.Filter(
    filter: world.FilterBuilder()
        .Term<ImpulseSpeed>()
        .Term<Position>()
);

filter.Iter((Iter it) =>
{
    Column<ImpulseSpeed> s = it.Field<ImpulseSpeed>(1);
    Column<Position> p = it.Field<Position>(2);

    foreach (int i in it)
    {
        p[i].X += s[i].Value;
        Console.WriteLine($"{it.Entity(i).Name()}: ({p[i].X:0.0}, {p[i].Y:0.0})");
    }
});

public struct Attack { public double Value { get; set; } }
public struct Defense { public double Value { get; set; } }
public struct FreightCapacity { public double Value { get; set; } }
public struct ImpulseSpeed { public double Value { get; set; } }
public struct HasFTL { }

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
};

#endif

// Output:
// Instance type: [Position, (Identifier,Name), (IsA,MammothFreighter)]
// Impulse speed: 50
// MyMammothFreighter: (50.0, -0.0)
