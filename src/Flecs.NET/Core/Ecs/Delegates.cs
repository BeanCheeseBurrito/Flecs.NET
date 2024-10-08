using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    /// <summary>
    ///     App init action.
    /// </summary>
    public delegate int AppInitAction(ecs_world_t* world);

    /// <summary>
    ///     Context free.
    /// </summary>
    public delegate void ContextFree(void* ctx);

    /// <summary>
    ///     Ctor type hook callback.
    /// </summary>
    public delegate void CtorCallback(void* data, int count, ecs_type_info_t* typeInfo);

    /// <summary>
    ///     Ctor type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void CtorCallback<T>(ref T data, TypeInfo typeInfo);

    /// <summary>
    ///     Dtor type hook callback.
    /// </summary>
    public delegate void DtorCallback(void* data, int count, ecs_type_info_t* typeInfo);

    /// <summary>
    ///     Dtor type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void DtorCallback<T>(ref T data, TypeInfo typeInfo);

    /// <summary>
    ///     Move type hook callback.
    /// </summary>
    public delegate void MoveCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfo);

    /// <summary>
    ///     Move type hook callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void MoveCallback<T>(ref T dst, ref T src, TypeInfo typeInfo);

    /// <summary>
    ///     Copy type hook callback.
    /// </summary>
    public delegate void CopyCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfo);

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
    ///     Finish action.
    /// </summary>
    public delegate void FiniAction(ecs_world_t* world, void* ctx);

    /// <summary>
    ///     Free.
    /// </summary>
    public delegate void Free(IntPtr data);

    /// <summary>
    ///     GroupBy action.
    /// </summary>
    public delegate ulong GroupByAction(ecs_world_t* world, ecs_table_t* table, ulong groupId, void* ctx);

    /// <summary>
    ///     GroupBy action.
    /// </summary>
    public delegate ulong GroupByCallback(World world, Table table, Entity group);

    /// <summary>
    ///     Group create action.
    /// </summary>
    public delegate void* GroupCreateAction(ecs_world_t* world, ulong groupId, void* groupByCtx);

    /// <summary>
    ///     Group delete action.
    /// </summary>
    public delegate void GroupDeleteAction(ecs_world_t* world, ulong groupId, void* groupCtx, void* groupByCtx);

    /// <summary>
    ///     Iter action.
    /// </summary>
    public delegate void IterAction(ecs_iter_t* it);

    /// <summary>
    ///     Iter callback.
    /// </summary>
    public delegate void IterCallback(Iter it);

    /// <summary>
    ///     Iter next action.
    /// </summary>
    public delegate byte IterNextAction(ecs_iter_t* it);

    /// <summary>
    ///     OrderBy action.
    /// </summary>
    public delegate int OrderByAction(ulong e1, void* ptr1, ulong e2, void* ptr2);

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
    public delegate void OsApiLog(int level, byte* file, int line, byte* message);
}