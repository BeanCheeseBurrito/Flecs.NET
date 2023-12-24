#if Cpp_Reflection_NestedSetMember

using Flecs.NET.Core;

{
    using World world = World.Create();

    world.Component<Point>()
        .Member<float>("X")
        .Member<float>("Y");

    world.Component<Line>()
        .Member<Point>("Start")
        .Member<Point>("Stop");

    // Create entity, set value of Line using reflection API
    Entity e = world.Entity();
    ref Line reference = ref e.GetMut<Line>();

    Cursor cur = world.Cursor<Line>(ref reference);
    cur.Push();          // {
    cur.Member("Start"); //   Start:
    cur.Push();          //   {
    cur.Member("X");     //     X:
    cur.SetFloat(10);    //     10
    cur.Member("Y");     //     Y:
    cur.SetFloat(20);    //     20
    cur.Pop();           //   }
    cur.Member("Stop");  //   Stop:
    cur.Push();          //   {
    cur.Member("X");     //     X:
    cur.SetFloat(30);    //     30
    cur.Member("Y");     //     Y:
    cur.SetFloat(40);    //     40
    cur.Pop();           //   }
    cur.Pop();           // }

    // Convert component to string
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
