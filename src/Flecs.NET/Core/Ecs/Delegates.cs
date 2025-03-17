using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    /// <summary>
    ///     App init callback.
    /// </summary>
    public delegate void AppInitCallback(World world);

    /// <summary>
    ///     Callback to be run before a user context object is released by flecs.
    /// </summary>
    /// <typeparam name="T">The user context type.</typeparam>
    public delegate void UserContextFinish<T>(ref T value);

    /// <summary>
    ///     Ctor type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void CtorCallback<T>(ref T data, TypeInfo typeInfo);

    /// <summary>
    ///     Dtor type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void DtorCallback<T>(ref T data, TypeInfo typeInfo);

    /// <summary>
    ///     Move type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void MoveCallback<T>(ref T dst, ref T src, TypeInfo typeInfo);

    /// <summary>
    ///     Copy type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void CopyCallback<T>(ref T dst, ref T src, TypeInfo typeInfo);

    /// <summary>
    ///     Each id callback.
    /// </summary>
    public delegate void EachIdCallback(Id id);

    /// <summary>
    ///     Each callback.
    /// </summary>
    public delegate void EachEntityCallback(Entity entity);

    /// <summary>
    ///     Each callback.
    /// </summary>
    public delegate void EachIterCallback(Iter it, int i);

    /// <summary>
    ///     Find callback.
    /// </summary>
    public delegate bool FindEntityCallback(Entity entity);

    /// <summary>
    ///     Find callback.
    /// </summary>
    public delegate bool FindIterCallback(Iter it, int i);

    /// <summary>
    ///     Observe Callback.
    /// </summary>
    public delegate void ObserveCallback();

    /// <summary>
    ///     Observe Callback.
    /// </summary>
    public delegate void ObserveEntityCallback(Entity e);

    /// <summary>
    ///     Observe Callback.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    public delegate void ObserveRefCallback<T>(ref T component);

    /// <summary>
    ///     Observe Callback.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    public delegate void ObservePointerCallback<T>(T* component);

    /// <summary>
    ///     Observe Callback.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    public delegate void ObserveEntityRefCallback<T>(Entity e, ref T component);

    /// <summary>
    ///     Observe Callback.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    public delegate void ObserveEntityPointerCallback<T>(Entity e, T* component);

    /// <summary>
    ///     World finish callback.
    /// </summary>
    public delegate void WorldFinishCallback(World world);

    /// <summary>
    ///     Run post frame callback.
    /// </summary>
    public delegate void PostFrameCallback(World world);

    /// <summary>
    ///     GroupBy Callback.
    /// </summary>
    public delegate ulong GroupByCallback(World world, Table table, ulong group);

    /// <summary>
    ///     GroupBy Callback.
    /// </summary>
    public delegate ulong GroupByCallback<T>(World world, Table table, ulong group, ref T groupByContext);

    /// <summary>
    ///     Group create callback.
    /// </summary>
    public delegate void GroupCreateCallback(World world, ulong group);

    /// <summary>
    ///     Group create callback.
    /// </summary>
    public delegate void GroupCreateCallback<T>(World world, ulong group, out T groupContext);

    /// <summary>
    ///     Group delete action.
    /// </summary>
    public delegate void GroupDeleteCallback(World world, ulong group);

    /// <summary>
    ///     Group delete action.
    /// </summary>
    public delegate void GroupDeleteCallback<T>(World world, ulong group, ref T context);

    /// <summary>
    ///     Iter callback.
    /// </summary>
    public delegate void IterCallback(Iter it);

    /// <summary>
    ///     OrderBy action.
    /// </summary>
    public delegate int OrderByCallback(ulong e1, void* ptr1, ulong e2, void* ptr2);

    /// <summary>
    ///     A callback that takes a reference to a world.
    /// </summary>
    public delegate void WorldCallback(World world);

    /// <summary>
    ///     A callback that takes a reference to a term.
    /// </summary>
    public delegate void TermCallback(ref Term term);

    /// <summary>
    ///     Run callback.
    /// </summary>
    public delegate void RunCallback(Iter it);

    /// <summary>
    ///     Run delegate callback.
    /// </summary>
    public delegate void RunDelegateCallback(Iter it, Action<Iter> callback);

    /// <summary>
    ///     Run function pointer callback.
    /// </summary>
    public delegate void RunPointerCallback(Iter it, delegate*<Iter, void> callback);

    /// <summary>
    ///     Os api log callback.
    /// </summary>
    public delegate void LogCallback(int level, string file, int line, string message);
}
