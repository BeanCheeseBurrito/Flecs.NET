using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A static class for holding callback invokers.
/// </summary>
// Iter Invokers
public static unsafe partial class Invoker
{
    /// <summary>
    ///     Invokes an iterator callback.
    /// </summary>
    /// <param name="it">The iterator.</param>
    public static void Callback(Iter it)
    {
        it.Callback();
    }

    /// <summary>
    ///     Invokes an iter callback using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Iter(ecs_iter_t* iter, Ecs.IterCallback callback)
    {
        Ecs.TableLock(iter->world, iter->table);
        callback(new Iter(iter));
        Ecs.TableUnlock(iter->world, iter->table);
    }

    /// <summary>
    ///     Invokes an each callback using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Each(ecs_iter_t* iter, Ecs.EachEntityCallback callback)
    {
        Ecs.Assert(iter->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");

        iter->flags |= EcsIterCppEach;

        Ecs.TableLock(iter->world, iter->table);

        for (int i = 0; i < iter->count; i++)
            callback(new Entity(iter->world, iter->entities[i]));

        Ecs.TableUnlock(iter->world, iter->table);
    }

    /// <summary>
    ///     Invokes an each callback using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Each(ecs_iter_t* iter, Ecs.EachIterCallback callback)
    {
        iter->flags |= EcsIterCppEach;

        Ecs.TableLock(iter->world, iter->table);

        int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

        for (int i = 0; i < count; i++)
            callback(new Iter(iter), i);

        Ecs.TableUnlock(iter->world, iter->table);
    }

    /// <summary>
    ///     Invokes a run callback using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Run(ecs_iter_t* iter, Ecs.RunCallback callback)
    {
        iter->flags &= ~EcsIterIsValid;
        callback(new Iter(iter));
    }

    /// <summary>
    ///     Invokes a run callback using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Run(ecs_iter_t* iter, Ecs.RunDelegateCallback callback)
    {
        iter->flags &= ~EcsIterIsValid;
        callback(new Iter(iter), Callback);
    }

    /// <summary>
    ///     Invokes an entity observer using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Observe(ecs_iter_t* iter, Ecs.ObserveEntityCallback callback)
    {
        callback(new Entity(iter->world, ecs_field_src(iter, 0)));
    }

    /// <summary>
    ///     Invokes an entity observer using a pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Observe(ecs_iter_t* iter, delegate*<Entity, void> callback)
    {
        callback(new Entity(iter->world, ecs_field_src(iter, 0)));
    }

    /// <summary>
    ///     Invokes an entity observer using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, Ecs.ObserveRefCallback<T> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback(ref Managed.GetTypeRef<T>(iter->param));
    }

    /// <summary>
    ///     Invokes an entity observer using a pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, delegate*<ref T, void> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback(ref Managed.GetTypeRef<T>(iter->param));
    }

    /// <summary>
    ///     Invokes an entity observer using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, Ecs.ObservePointerCallback<T> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback((T*)iter->param);
    }

    /// <summary>
    ///     Invokes an entity observer using a pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, delegate*<T*, void> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback((T*)iter->param);
    }

    /// <summary>
    ///     Invokes an entity observer using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, Ecs.ObserveEntityRefCallback<T> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback(new Entity(iter->world, ecs_field_src(iter, 0)), ref Managed.GetTypeRef<T>(iter->param));
    }

    /// <summary>
    ///     Invokes an entity observer using a pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, delegate*<Entity, ref T, void> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback(new Entity(iter->world, ecs_field_src(iter, 0)), ref Managed.GetTypeRef<T>(iter->param));
    }

    /// <summary>
    ///     Invokes an entity observer using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, Ecs.ObserveEntityPointerCallback<T> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback(new Entity(iter->world, ecs_field_src(iter, 0)), (T*)iter->param);
    }

    /// <summary>
    ///     Invokes an entity observer using a pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public static void Observe<T>(ecs_iter_t* iter, delegate*<Entity, T*, void> callback)
    {
        Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
        callback(new Entity(iter->world, ecs_field_src(iter, 0)), (T*)iter->param);
    }

    /// <summary>
    ///     Invokes an iter callback using a managed function pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Iter(ecs_iter_t* iter, delegate*<Iter, void> callback)
    {
        Ecs.TableLock(iter->world, iter->table);
        callback(new Iter(iter));
        Ecs.TableUnlock(iter->world, iter->table);
    }

    /// <summary>
    ///     Invokes an each callback using a managed function pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Each(ecs_iter_t* iter, delegate*<Entity, void> callback)
    {
        Ecs.Assert(iter->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");

        iter->flags |= EcsIterCppEach;

        Ecs.TableLock(iter->world, iter->table);

        for (int i = 0; i < iter->count; i++)
            callback(new Entity(iter->world, iter->entities[i]));

        Ecs.TableUnlock(iter->world, iter->table);
    }

    /// <summary>
    ///      Invokes an each callback using a managed function pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Each(ecs_iter_t* iter, delegate*<Iter, int, void> callback)
    {
        iter->flags |= EcsIterCppEach;

        Ecs.TableLock(iter->world, iter->table);

        int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

        for (int i = 0; i < count; i++)
            callback(new Iter(iter), i);

        Ecs.TableUnlock(iter->world, iter->table);
    }

    /// <summary>
    ///     Invokes a run callback using a managed function pointer.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Run(ecs_iter_t* iter, delegate*<Iter, void> callback)
    {
        iter->flags &= ~EcsIterIsValid;
        callback(new Iter(iter));
    }

    /// <summary>
    ///     Invokes a run pointer callback using a delegate.
    /// </summary>
    /// <param name="iter"></param>
    /// <param name="callback"></param>
    public static void Run(ecs_iter_t* iter, Ecs.RunPointerCallback callback)
    {
        iter->flags &= ~EcsIterIsValid;
        callback(new Iter(iter), &Callback);
    }

    /// <summary>
    ///     Invokes a run delegate callback using a managed function pointer.
    /// </summary>
    /// <param name="iter">The iterator.</param>
    /// <param name="callback">The callback.</param>
    public static void Run(ecs_iter_t* iter, delegate*<Iter, Action<Iter>, void> callback)
    {
        iter->flags &= ~EcsIterIsValid;
        callback(new Iter(iter), Callback);
    }

    /// <summary>
    ///     Invokes a run pointer callback using a managed function pointer.
    /// </summary>
    /// <param name="iter">The iterator.</param>
    /// <param name="callback">The callback.</param>
    public static void Run(ecs_iter_t* iter, delegate*<Iter, delegate*<Iter, void>, void> callback)
    {
        iter->flags &= ~EcsIterIsValid;
        callback(new Iter(iter), &Callback);
    }
}

// Iterable Invokers
public static unsafe partial class Invoker
{
    /// <summary>
    ///     Iterates over iterable with the provided Iter callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Iter<T>(ref T iterable, Ecs.IterCallback callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Iter(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Each callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Each<T>(ref T iterable, Ecs.EachEntityCallback callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Each(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Each callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Each<T>(ref T iterable, Ecs.EachIterCallback callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Each(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Run callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Run<T>(ref T iterable, Ecs.RunCallback callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        Run(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Iter callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Iter<T>(ref T iterable, delegate*<Iter, void> callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Iter(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Each callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Each<T>(ref T iterable, delegate*<Entity, void> callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Each(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Each callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Each<T>(ref T iterable, delegate*<Iter, int, void> callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Each(&iter, callback);
    }

    /// <summary>
    ///     Iterates over iterable with the provided Run callback.
    /// </summary>
    /// <param name="iterable">The iterable object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The iterable type.</typeparam>
    public static void Run<T>(ref T iterable, delegate*<Iter, void> callback) where T : unmanaged, IIterableBase
    {
        ecs_iter_t iter = iterable.GetIter();
        Run(&iter, callback);
    }
}