using System.Runtime.InteropServices;
using Flecs.NET.Core;

// Components
file struct NativeString(string str)
{
    public IntPtr Value = Marshal.StringToHGlobalAnsi(str);
}

public static class Entities_Hooks
{
    public static void Main()
    {
        World world = World.Create();

        world.Component<NativeString>()
            // Resource management hooks. These hooks should primarily be used for
            // managing memory used by the component.

            // The constructor should initialize the component value.
            .Ctor((ref NativeString data, TypeInfo typeInfo) =>
            {
                Ecs.Log.Trace("Ctor");
                data.Value = IntPtr.Zero;
            })

            // The destructor should free resources.
            .Dtor((ref NativeString data, TypeInfo typeInfo) =>
            {
                Ecs.Log.Trace("Dtor");
                Marshal.FreeHGlobal(data.Value);
            })

            // The move hook should move resources from one location to another.
            .Move((ref NativeString dst, ref NativeString src, TypeInfo typeInfo) =>
            {
                Ecs.Log.Trace("Move");
                Marshal.FreeHGlobal(dst.Value);
                dst.Value = src.Value;
                src.Value = IntPtr.Zero; // This makes sure the value doesn't get deleted twice
                // as the destructor is still invoked after a move.
            })

            // The copy hook should copy resources from one location to another.
            .Copy((ref NativeString dst, ref NativeString src, TypeInfo typeInfo) =>
            {
                Ecs.Log.Trace("Copy");
                Marshal.FreeHGlobal(dst.Value);
                dst = new NativeString(Marshal.PtrToStringAnsi(src.Value)!); // Allocate new copy of the string.
            })

            // Lifecycle hooks. These hooks should be used for application logic.
            // Note that the signature is the same as query callbacks.
            .OnAdd((Iter it, int i, ref NativeString str) =>
            {
                Ecs.Log.Trace($"{it.Event()}: {it.Entity(i)}");
            })
            .OnSet((Iter it, int i, ref NativeString str) =>
            {
                Ecs.Log.Trace($"{it.Event()}: {it.Entity(i)}");
            })
            .OnRemove((Iter it, int i, ref NativeString str) =>
            {
                Ecs.Log.Trace($"{it.Event()}: {it.Entity(i)}");
            });

        Ecs.Log.SetLevel(0);

        Entity e = world.Entity("Entity");
        Entity tag = world.Entity();

        Ecs.Log.Trace("e.Add<NativeString>()");
        Ecs.Log.Push();
        e.Add<NativeString>();
        Ecs.Log.Pop();

        Ecs.Log.Trace("e.Set<NativeString>(new(\"Hello World\"))");
        Ecs.Log.Push();
        e.Set(new NativeString("Hello World"));
        Ecs.Log.Pop();

        // This operation changes the entity's archetype, which invokes a move
        Ecs.Log.Trace("e.Add(tag)");
        Ecs.Log.Push();
        e.Add(tag);
        Ecs.Log.Pop();

        Ecs.Log.Trace("e.Destruct()");
        Ecs.Log.Push();
        e.Destruct();
        Ecs.Log.Pop();

        Ecs.Log.SetLevel(-1);
    }
}

// Output:
// info: e.Add<NativeString>()
// info: | Ctor
// info: | OnAdd: Entity
// info: e.Set<NativeString>(new("Hello World"))
// info: | Copy
// info: | OnSet: Entity
// info: e.Add(tag)
// info: | Ctor
// info: | Move
// info: | Dtor
// info: e.Destruct()
// info: | OnRemove: Entity
// info: | Dtor
