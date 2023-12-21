#if Cpp_Reflection_BasicsBitmask

using Flecs.NET.Core;

{
    using World world = World.Create();

    // Register components with reflection data.
    world.Component<Toppings>()
        .Bit("Bacon", Toppings.Bacon)
        .Bit("Lettuce", Toppings.Lettuce)
        .Bit("Tomato", Toppings.Tomato);

    world.Component<Sandwich>()
        .Member<Toppings>("Toppings");

    // Create entity with Sandwich component.
    Entity e = world.Entity()
        .Set<Sandwich>(new(Toppings.Bacon | Toppings.Lettuce));

    // Convert Sandwich component to flecs expression string.
    ref Sandwich sandwich = ref e.GetMut<Sandwich>();
    Console.WriteLine(world.ToExpr(ref sandwich));

    return 0;
}

public struct Toppings
{
    // Bitmask data
    public uint Bitmask { get; set; }

    public const uint Bacon = 0x1;
    public const uint Lettuce = 0x2;
    public const uint Tomato = 0x4;
}

public struct Sandwich(uint toppings)
{
    public uint Toppings { get; set; } = toppings;
}

#endif

// Output:
// {Toppings: Lettuce|Bacon}
