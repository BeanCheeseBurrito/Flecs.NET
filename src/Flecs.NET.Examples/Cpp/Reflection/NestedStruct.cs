#if Cpp_Reflection_NestedStruct

using Flecs.NET.Core;

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

    return 0;
}

public struct Point(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
}

public struct Line(Point start, Point stop)
{
    public Point Start { get; set; } = start;
    public Point Stop { get; set; } = stop;
}

#endif

// Output:
// {Start: {X: 10, Y: 20}, Stop: {X: 30, Y: 40}}
