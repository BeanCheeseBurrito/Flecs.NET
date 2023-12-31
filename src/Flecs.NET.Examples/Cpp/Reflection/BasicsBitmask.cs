using Flecs.NET.Core;

// Components.
file record struct Sandwich(uint toppings);
file record struct Toppings(uint Bitmask)
{
    public const uint Bacon = 0x1;
    public const uint Lettuce = 0x2;
    public const uint Tomato = 0x4;
}

public static class Cpp_Reflection_BasicsBitmask
{
    public static void Main()
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
    }
}

// Output:
// {Toppings: Lettuce|Bacon}
