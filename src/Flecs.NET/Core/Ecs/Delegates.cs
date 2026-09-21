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
    ///     Each entity callback.
    /// </summary>
    public delegate void EachEntityCallback(Entity entity);

    /// <summary>
    ///     Each id callback.
    /// </summary>
    public delegate void EachIdCallback(Id id);

    /// <summary>
    ///     Each index callback.
    /// </summary>
    public delegate void EachIterCallback(Iter it, int i);

    /// <summary>
    ///     Function signature that takes an <see cref="Entity"/> argument.
    /// </summary>
    public delegate void ObserveEntityCallback(Entity e);

    /// <summary>
    ///     Function signature that takes a ref T argument.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void ObserveRefCallback<T>(ref T component);

    /// <summary>
    ///     Function signature that takes a T* callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void ObservePointerCallback<T>(T* component);

    /// <summary>
    ///     Function signature that takes an <see cref="Entity"/> and a ref T argument.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void ObserveEntityRefCallback<T>(Entity e, ref T component);

    /// <summary>
    ///     Function signature that takes an <see cref="Entity"/> and a T* argument.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

    /// <summary>
    ///     Os api trace callback.
    /// </summary>
    public delegate void TraceCallback(string file, ulong line, string name);

    /// <summary>
    ///     Opaque type serialize callback.
    /// </summary>
    /// <typeparam name="T">The opaque type.</typeparam>
    public delegate int OpaqueSerializeCallback<T>(ref readonly Serializer ser, in T value);

    /// <summary>
    ///     Opaque struct member serialize callback.
    /// </summary>
    public delegate int OpaqueSerializeMemberCallback<T>(ref readonly Serializer ser, in T value, string member);

    /// <summary>
    ///     Opaque collection element serialize callback.
    /// </summary>
    public delegate int OpaqueSerializeElementCallback<T>(ref readonly Serializer ser, in T value, nuint index);

    /// <summary>
    ///     Opaque type bool assignment callback.
    /// </summary>
    public delegate void OpaqueAssignBoolCallback<T>(ref T value, bool assigned);

    /// <summary>
    ///     Opaque type char assignment callback.
    /// </summary>
    public delegate void OpaqueAssignCharCallback<T>(ref T value, char assigned);

    /// <summary>
    ///     Opaque type signed integer assignment callback.
    /// </summary>
    public delegate void OpaqueAssignIntCallback<T>(ref T value, long assigned);

    /// <summary>
    ///     Opaque type unsigned integer assignment callback.
    /// </summary>
    public delegate void OpaqueAssignUIntCallback<T>(ref T value, ulong assigned);

    /// <summary>
    ///     Opaque type floating-point assignment callback.
    /// </summary>
    public delegate void OpaqueAssignFloatCallback<T>(ref T value, double assigned);

    /// <summary>
    ///     Opaque type string assignment callback.
    /// </summary>
    public delegate void OpaqueAssignStringCallback<T>(ref T value, string assigned);

    /// <summary>
    ///     Opaque entity assignment callback.
    /// </summary>
    public delegate void OpaqueAssignEntityCallback<T>(ref T value, World world, ulong assigned);

    /// <summary>
    ///     Opaque id assignment callback.
    /// </summary>
    public delegate void OpaqueAssignIdCallback<T>(ref T value, World world, ulong assigned);

    /// <summary>
    ///     Opaque null assignment callback.
    /// </summary>
    public delegate void OpaqueAssignNullCallback<T>(ref T value);

    /// <summary>
    ///     Opaque collection clear callback.
    /// </summary>
    public delegate void OpaqueClearCallback<T>(ref T value);

    /// <summary>
    ///     Opaque member lookup callback. The returned reference must remain valid for the operation.
    /// </summary>
    public delegate ref byte OpaqueEnsureMemberCallback<T>(ref T value, string member);

    /// <summary>
    ///     Opaque type element count callback.
    /// </summary>
    public delegate nint OpaqueCountCallback<T>(in T value);

    /// <summary>
    ///     Opaque type resize callback.
    /// </summary>
    public delegate void OpaqueResizeCallback<T>(ref T value, nuint size);

    /// <summary>
    ///     Opaque type ensure element callback. The returned reference must remain valid for the operation.
    /// </summary>
    public delegate ref TElement OpaqueEnsureElementCallback<T, TElement>(ref T value, nuint index) where TElement : unmanaged;
}