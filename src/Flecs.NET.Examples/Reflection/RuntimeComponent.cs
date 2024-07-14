using Flecs.NET.Core;

public static unsafe class Reflection_RuntimeComponent
{
    public static void Main()
    {
        using World world = World.Create();

        // Create component for a type that isn't known at compile time.
        UntypedComponent position = world.Component("Position")
            .Member<float>("X")
            .Member<float>("Y");

        // Create entity, set value of position using reflection API.
        Entity e = world.Entity();
        void* ptr = e.EnsurePtr(position);

        Cursor cur = world.Cursor(position, ptr);
        cur.Push();
        cur.SetFloat(10);
        cur.Next();
        cur.SetFloat(20);
        cur.Pop();

        // Convert component to string.
        Console.WriteLine(world.ToExpr(position, ptr)); // {X: 10, Y: 20}
    }
}

// Output:
// {X: 10, Y: 20}
