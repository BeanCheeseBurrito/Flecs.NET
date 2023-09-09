// Prefabs can inherit from each other, which creates prefab variants. With
// variants applications can reuse a common set of components and specialize it
// by adding or overriding components on the variant.

#if Cpp_Prefabs_Variant

using Flecs.NET.Core;

using World world = World.Create();

// Create a base prefab for SpaceShips.
Entity spaceShip = world.Prefab("SpaceShip")
    .Set(new ImpulseSpeed { Value = 50 })
    .Set(new Defense { Value = 25 });

    // Create a Freighter variant which inherits from SpaceShip
    Entity freighter = world.Prefab("Freighter")
        .IsA(spaceShip)
        .Set(new FreightCapacity { Value = 100 })
        .Set(new Defense { Value = 50 });

        // Create a MammothFreighter variant which inherits from Freighter
        Entity mammothFreighter = world.Prefab("MammothFreighter")
            .IsA(freighter)
            .Set(new FreightCapacity { Value = 500 });

    // Create a Frigate variant which inherits from SpaceShip
    world.Prefab("Frigate")
        .IsA(spaceShip)
        .Set(new Attack { Value = 100 })
        .Set(new Defense { Value = 75 })
        .Set(new ImpulseSpeed { Value = 125 });

// Create an instance of the MammothFreighter. This entity will inherit the
// ImpulseSpeed from SpaceShip, Defense from Freighter and FreightCapacity
// from MammothFreighter.
Entity inst = world.Entity("my_freighter").IsA(mammothFreighter);

// Add a private Position component.
inst.Set(new Position { X = 10, Y = 20 });

// Instances can override inherited components to give them a private copy
// of the component. This freighter got an armor upgrade:
inst.Set(new Defense { Value = 100 });

// Queries can match components from multiple levels of inheritance
Query query = world.Query(
    filter: world.FilterBuilder()
        .With<Position>()
        .With<ImpulseSpeed>()
        .With<Defense>()
        .With<FreightCapacity>()
);

query.Each((Iter it, int i) =>
{
    Column<Position> p = it.Field<Position>(1);
    Column<ImpulseSpeed> s = it.Field<ImpulseSpeed>(2);
    Column<Defense> d = it.Field<Defense>(3);
    Column<FreightCapacity> c = it.Field<FreightCapacity>(4);

    Console.WriteLine(it.Entity(i).Name());
    Console.WriteLine($" - Position: {p[i].X}, {p[i].Y}");
    Console.WriteLine($" - Impulse Speed: {s[i].Value}");
    Console.WriteLine($" - Defense: {d[i].Value}");
    Console.WriteLine($" - Capacity: {c[i].Value}");
});

public struct Attack
{
    public double Value { get; set; }
}

public struct Defense
{
    public double Value { get; set; }
}

public struct FreightCapacity
{
    public double Value { get; set; }
}

public struct ImpulseSpeed
{
    public double Value { get; set; }
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// Output:
// my_freighter
//  - Position: 10, 20
//  - Impulse Speed: 50
//  - Defense: 100
//  - Capacity: 500
