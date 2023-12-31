using Flecs.NET.Core;

// Components
file record struct Point(float X, float Y);
file record struct Line(Point Start, Point Stop);

public static class Cpp_Reflection_NestedStruct
{
    public static void Main()
    {
        using World world = World.Create();

        // Register components with reflection data
        world.Component<Point>()
            .Member<float>("X")
            .Member<float>("Y");

        world.Component<Line>()
            .Member<Point>("Start")
            .Member<Point>("Stop");

        // Create entity with Line component as usual.
        Entity e = world.Entity()
            .Set<Line>(new(new(10, 20), new(30, 40)));

        // Convert Line component to flecs expression string.
        ref Line reference = ref e.GetMut<Line>();
        Console.WriteLine(world.ToExpr(ref reference));
        // {Start: {X: 10, Y: 20}, Stop: {X: 30, Y: 40}}
    }
}

// Output:
// {Start: {X: 10, Y: 20}, Stop: {X: 30, Y: 40}}
