using Flecs.NET.Core;

// Components
file record struct TypeWithEnum(Color color);

file enum Color
{
    Red,
    Green,
    Blue
}

public static class Cpp_Reflection_BasicsEnum
{
    public static void Main()
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
        ref TypeWithEnum reference = ref e.Ensure<TypeWithEnum>();
        Console.WriteLine(world.ToExpr(ref reference)); // {Color: Green}
    }
}

// Output:
// {Color: Green}
