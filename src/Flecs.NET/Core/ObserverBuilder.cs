using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_observer_desc_t"/>.
/// </summary>
public unsafe partial struct ObserverBuilder : IDisposable, IEquatable<ObserverBuilder>, IQueryBuilder<ObserverBuilder>
{
    private ecs_world_t* _world;
    private ecs_observer_desc_t _desc;
    private QueryBuilder _queryBuilder;
    private int _eventCount;

    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World => ref _world;

    /// <summary>
    ///     A reference to the observer description.
    /// </summary>
    public ref ecs_observer_desc_t Desc => ref _desc;

    /// <summary>
    ///     A reference to the query builder.
    /// </summary>
    public ref QueryBuilder QueryBuilder => ref _queryBuilder;

    /// <summary>
    /// Creates an observer builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    public ObserverBuilder(ecs_world_t* world)
    {
        _world = world;
        _desc = default;
        _eventCount = default;
        _queryBuilder = new QueryBuilder(world);
    }

    /// <summary>
    ///     Creates an observer builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="name">The observer name.</param>
    public ObserverBuilder(ecs_world_t* world, string name) : this(world)
    {
        using NativeString nativeName = (NativeString)name;

        ecs_entity_desc_t entityDesc = default;
        entityDesc.name = nativeName;
        entityDesc.sep = Pointers.DefaultSeparator;
        entityDesc.root_sep = Pointers.DefaultSeparator;

        Desc.entity = ecs_entity_init(world, &entityDesc);
    }

    /// <summary>
    ///     Disposes the observer builder. This should be called if the observer builder
    ///     will be discarded and .Iter()/.Each()/.Run() isn't called.
    /// </summary>
    public void Dispose()
    {
        QueryBuilder.Dispose();
        FreeRun();
        FreeCallback();
    }

    /// <summary>
    ///     Specify the event(s) for when the observer should run.
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public ref ObserverBuilder Event(ulong @event)
    {
        if (_eventCount >= 8)
            Ecs.Error("Can't create an observer with more than 8 events.");

        Desc.events[_eventCount++] = @event;
        return ref this;
    }

    /// <summary>
    ///     Specify the event(s) for when the observer should run.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Event<T>()
    {
        return ref Event(Type<T>.Id(World));
    }

    /// <summary>
    ///     Invoke observer for anything that matches its filter on creation.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public ref ObserverBuilder YieldExisting(bool value = true)
    {
        Desc.yield_existing = Utils.Bool(value);
        return ref this;
    }

    /// <summary>
    ///     Set observer context.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public ref ObserverBuilder Ctx(void* data)
    {
        Desc.ctx = data;
        return ref this;
    }

    /// <summary>
    ///     Creates an observer with the provided Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Run(Action callback)
    {
        return SetCallback(callback, Pointers.ActionCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Run(delegate*<void> callback)
    {
        return SetCallback((IntPtr)callback, Pointers.ActionCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Run callback.
    /// </summary>
    /// <param name="run">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Run(Ecs.RunCallback run)
    {
        return SetRun(run, Pointers.RunCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Run(delegate*<Iter, void> callback)
    {
        return SetRun((IntPtr)callback, Pointers.RunCallbackPointer).Build();
    }

    /// <summary>
    ///     Sets a run callback. .Iter() or .Each() must be called after this to build the observer.
    /// </summary>
    /// <param name="run">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref ObserverBuilder Run(Ecs.RunDelegateCallback run)
    {
        return ref SetRun(run, Pointers.RunDelegateCallbackDelegate);
    }

    /// <summary>
    ///     Sets a run callback. .Iter() or .Each() must be called after this to build the observer.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref ObserverBuilder Run(delegate*<Iter, Action<Iter>, void> callback)
    {
        return ref SetRun((IntPtr)callback, Pointers.RunDelegateCallbackPointer);
    }

    /// <summary>
    ///     Sets a run callback. .Iter() or .Each() must be called after this to build the observer.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref ObserverBuilder Run(Ecs.RunPointerCallback callback)
    {
        return ref SetRun(callback, Pointers.RunPointerCallbackDelegate);
    }

    /// <summary>
    ///     Sets a run callback. .Iter() or .Each() must be called after this to build the observer.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref ObserverBuilder Run(delegate*<Iter, delegate*<Iter, void>, void> callback)
    {
        return ref SetRun((IntPtr)callback, Pointers.RunPointerCallbackPointer);
    }

    /// <summary>
    ///     Creates an observer with the provided Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Iter(Action callback)
    {
        return SetCallback(callback, Pointers.ActionCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Iter(delegate*<void> callback)
    {
        return SetCallback((IntPtr)callback, Pointers.ActionCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Iter(Ecs.IterCallback callback)
    {
        return SetCallback(callback, Pointers.IterCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Iter(delegate*<Iter, void> callback)
    {
        return SetCallback((IntPtr)callback, Pointers.IterCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Each(Action callback)
    {
        return SetCallback(callback, Pointers.ActionCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Each(delegate*<void> callback)
    {
        return SetCallback((IntPtr)callback, Pointers.ActionCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Each(Ecs.EachEntityCallback callback)
    {
        return SetCallback(callback, Pointers.EachEntityCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Each(delegate*<Entity, void> callback)
    {
        return SetCallback((IntPtr)callback, Pointers.EachEntityCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Each(Ecs.EachIterCallback callback)
    {
        return SetCallback(callback, Pointers.EachIterCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates an observer with the provided Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>The created observer.</returns>
    public Observer Each(delegate*<Iter, int, void> callback)
    {
        return SetCallback((IntPtr)callback, Pointers.EachIterCallbackPointer).Build();
    }

    private ref ObserverBuilder SetCallback<T>(T callback, IntPtr invoker) where T : Delegate
    {
        FreeCallback();
        IteratorContext context = default;
        Callback.Set(ref context.Callback, callback, false);
        Desc.callback = invoker;
        Desc.callback_ctx = Memory.Alloc(context);
        Desc.callback_ctx_free = Pointers.IteratorContextFree;
        return ref this;
    }

    private ref ObserverBuilder SetCallback(IntPtr callback, IntPtr invoker)
    {
        FreeCallback();
        IteratorContext context = default;
        Callback.Set(ref context.Callback, callback);
        Desc.callback = invoker;
        Desc.callback_ctx = Memory.Alloc(context);
        Desc.callback_ctx_free = Pointers.IteratorContextFree;
        return ref this;
    }

    private ref ObserverBuilder SetRun<T>(T callback, IntPtr invoker) where T : Delegate
    {
        FreeRun();
        RunContext context = default;
        Callback.Set(ref context.Callback, callback, false);
        Desc.run = invoker;
        Desc.run_ctx = Memory.Alloc(context);
        Desc.run_ctx_free = Pointers.RunContextFree;
        return ref this;
    }

    private ref ObserverBuilder SetRun(IntPtr callback, IntPtr invoker)
    {
        FreeRun();
        RunContext context = default;
        Callback.Set(ref context.Callback, callback);
        Desc.run = invoker;
        Desc.run_ctx = Memory.Alloc(context);
        Desc.run_ctx_free = Pointers.RunContextFree;
        return ref this;
    }

    private Observer Build()
    {
        Desc.query = QueryBuilder.Desc;
        Desc.query.binding_ctx = Memory.Alloc(QueryBuilder.Context);
        Desc.query.binding_ctx_free = Pointers.QueryContextFree;

        fixed (ecs_observer_desc_t* ptr = &Desc)
        {
            Ecs.Assert(_eventCount != 0, "Observer cannot have zero events. Use ObserverBuilder.Event() to add events.");
            Ecs.Assert(ptr->query.terms[0] != default || ptr->query.expr != null, "Observers require at least 1 term.");
            return new Observer(World, ecs_observer_init(World, ptr));
        }
    }

    private void FreeRun()
    {
        if (Desc.run == IntPtr.Zero)
            return;

        ((delegate*<void*, void>)Desc.run_ctx_free)(Desc.run_ctx);
    }

    private void FreeCallback()
    {
        if (Desc.callback == IntPtr.Zero)
            return;

        ((delegate*<void*, void>)Desc.callback_ctx_free)(Desc.callback_ctx);
    }

    /// <summary>
    ///     Checks if two <see cref="ObserverBuilder"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(ObserverBuilder other)
    {
        return Desc == other.Desc && QueryBuilder == other.QueryBuilder;
    }

    /// <summary>
    ///     Checks if two <see cref="ObserverBuilder"/> instance are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is ObserverBuilder other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="ObserverBuilder"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Desc.GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="ObserverBuilder"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(ObserverBuilder left, ObserverBuilder right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="ObserverBuilder"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(ObserverBuilder left, ObserverBuilder right)
    {
        return !(left == right);
    }
}