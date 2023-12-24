#if Cpp_Reflection_RuntimeNestedComponent

using Flecs.NET.Core;

unsafe
{
    using World world = World.Create();

    // Create component for types that aren't known at compile time.
    UntypedComponent point = world.Component("Point")
        .Member<float>("X")
        .Member<float>("Y");

    UntypedComponent line = world.Component("Line")
        .Member(point, "Start")
        .Member(point, "Stop");

    // Create entity, set value of line using reflection API.
    Entity e = world.Entity();
    void* ptr = e.GetMutPtr(line);

    Cursor cur = world.Cursor(line, ptr);
    cur.Push();        // {
    cur.Push();        //   {
    cur.SetFloat(10);  //     10
    cur.Next();        //     ,
    cur.SetFloat(20);  //     20
    cur.Pop();         //   }
    cur.Next();        //   ,
    cur.Push();        //   {
    cur.SetFloat(30);  //     30
    cur.Next();        //     ,
    cur.SetFloat(40);  //     40
    cur.Pop();         //   }
    cur.Pop();         // }

    // Convert component to string.
    Console.WriteLine(world.ToExpr(line, ptr));
    // {Start: {X: 10, Y: 20}, Stop: {X: 30, Y: 40}}

    return 0;
}

#endif

// Output:
// {Start: {X: 10, Y: 20}, Stop: {X: 30, Y: 40}}
