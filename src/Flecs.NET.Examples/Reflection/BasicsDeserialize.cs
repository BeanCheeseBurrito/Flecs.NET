using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Reflection_BasicsDeserialize
{
    public static void Main()
    {
        using World world = World.Create();

        // Register component with reflection data.
        world.Component<Position>()
            .Member<float>("X")
            .Member<float>("Y");

        // Create entity, set value of position using reflection API.
        Entity e = world.Entity();
        ref Position reference = ref e.Ensure<Position>();

        Cursor cur = world.Cursor(ref reference);
        cur.Push();       // {
        cur.SetFloat(10); //   10
        cur.Next();       //   ,
        cur.SetFloat(20); //   20
        cur.Pop();        // }

        Console.WriteLine(world.ToExpr(ref reference)); // {X: 10.00, Y: 20.00}

        // Use member names before assigning values.
        cur = world.Cursor(ref reference);
        cur.Push();       // {
        cur.Member("Y");  //   Y:
        cur.SetFloat(10); //   10
        cur.Member("X");  //   X:
        cur.SetFloat(20); //   20
        cur.Pop();        // }

        Console.WriteLine(world.ToExpr(ref reference)); // {X: 20.00, Y: 10.00}
    }
}

// Output:
// {X: 10, Y: 20}
// {X: 20, Y: 10}
