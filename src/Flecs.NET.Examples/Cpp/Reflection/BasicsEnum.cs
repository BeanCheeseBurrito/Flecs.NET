#if Cpp_Reflection_BasicsEnum

using Flecs.NET.Core;

{
    using World world = World.Create();

    // Register components with reflection data.
    world.Component<Color>()
        .Constant("Red", Color.Red)
        .Constant("Green", Color.Green)
        .Constant("Blue", Color.Blue);

    world.Component<TypeWithEnum>()
        .Member<Color>("Color");

    // Create entity with TypeWithEnum component.
    Entity e = world.Entity()
        .Set<TypeWithEnum>(new(Color.Green));

    // Convert TypeWithEnum component to flecs expression string.
    ref TypeWithEnum reference = ref e.GetMut<TypeWithEnum>();
    Console.WriteLine(world.ToExpr(ref reference)); // {Color: Green}

    return 0;
}

public enum Color
{
    Red,
    Green,
    Blue
}

public struct TypeWithEnum(Color color)
{
    public Color Color { get; set; } = color;
}

#endif

// Output:
// {Color: Green}
