// Component hooks are callbacks that can be registered for a type that are
// invoked during different parts of the component lifecycle.

#if Cpp_Entities_Hooks

using System.Runtime.InteropServices;
using Flecs.NET.Core;

{
    World world = World.Create(args);

    world.Component<NativeString>().SetHooks(new TypeHooks<NativeString>
    {
        // Resource management hooks. These hooks should primarily be used for
        // managing memory used by the component.
        Ctor = Ctor,
        Move = Move,
        Copy = Copy,
        Dtor = Dtor,

        // Lifecycle hooks. These hooks should be used for application logic.
        OnAdd = HookCallback,
        OnRemove = HookCallback,
        OnSet = HookCallback
    });

    Ecs.Log.SetLevel(0);

    Entity e = world.Entity("Entity");
    Entity tag = world.Entity();

    Ecs.Log.Trace("e.Add<NativeString>()");
    Ecs.Log.Push();
        e.Add<NativeString>();
    Ecs.Log.Pop();

    Ecs.Log.Trace("e.Set(new NativeString(\"Hello World\"))");
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

// Resource management hooks.
// The constructor should initialize the component value.
void Ctor(ref NativeString data, TypeInfo typeInfo)
{
    Ecs.Log.Trace("Ctor");
    data.Value = IntPtr.Zero;
}

// The destructor should free resources.
void Dtor(ref NativeString data, TypeInfo typeInfo)
{
    Ecs.Log.Trace("Dtor");
    Marshal.FreeHGlobal(data.Value);
}

// The move hook should move resources from one location to another.
void Move(ref NativeString dst, ref NativeString src, TypeInfo typeInfo)
{
    Ecs.Log.Trace("Move");
    Marshal.FreeHGlobal(dst.Value);
    dst.Value = src.Value;
    src.Value = IntPtr.Zero; // This makes sure the value doesn't get deleted twice
    // as the destructor is still invoked after a move.
}

// The copy hook should copy resources from one location to another.
void Copy(ref NativeString dst, ref NativeString src, TypeInfo typeInfo)
{
    Ecs.Log.Trace("Copy");
    Marshal.FreeHGlobal(dst.Value);
    dst.Value = src.Value;
}

// This callback is used for the add, remove and set hooks. Note that the
// signature is the same as systems, triggers, observers.
void HookCallback(Iter it, Column<NativeString> str)
{
    Entity eventEntity = it.Event();

    foreach (int i in it)
        Ecs.Log.Trace($"{eventEntity}: {it.Entity(i)}");
}

public struct NativeString
{
    public IntPtr Value { get; set; }

    public NativeString(string str)
    {
        Value = Marshal.StringToHGlobalAnsi(str);
    }
}

#endif

// Output:
// info: e.Add<NativeString>()
// info: | Ctor
// info: | OnAdd: Entity
// info: e.Set(new NativeString("Hello World"))
// info: | Copy
// info: | OnSet: Entity
// info: e.Add(tag)
// info: | Ctor
// info: | Move
// info: | Dtor
// info: e.Destruct()
// info: | OnRemove: Entity
// info: | Dtor
