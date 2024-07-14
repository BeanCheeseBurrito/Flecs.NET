using Flecs.NET.Core;

// Components
file record struct Point(float X, float Y);
file record struct Line(Point Start, Point Stop);

public static class Reflection_NestedSetMember
{
    public static void Main()
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
        ref Line reference = ref e.Ensure<Line>();

        Cursor cur = world.Cursor(ref reference);
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
    }
}

// Output:
// {Start: {X: 10, Y: 20}, Stop: {X: 30, Y: 40}}
