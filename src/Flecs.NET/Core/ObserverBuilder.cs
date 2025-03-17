using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_observer_desc_t"/>.
/// </summary>
public unsafe partial struct ObserverBuilder : IDisposable, IEquatable<ObserverBuilder>, IQueryBuilder<ObserverBuilder, Observer>
{
    private ecs_world_t* _world;
    private ecs_observer_desc_t _desc;
    private QueryBuilder _queryBuilder;
    private int _eventCount;

    internal ref UserContext UserContext => ref *EnsureUserContext();
    internal ref IteratorContext IteratorContext => ref *EnsureIteratorContext();
    internal ref RunContext RunContext => ref *EnsureRunContext();

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
    ///     Creates an observer builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The observer entity.</param>
    public ObserverBuilder(ecs_world_t* world, ulong entity) : this(world)
    {
        ecs_entity_desc_t entityDesc = default;
        entityDesc.id = entity;

        Desc.entity = ecs_entity_init(world, &entityDesc);
    }

    /// <summary>
    ///     Disposes the observer builder. This should be called if the observer builder
    ///     will be discarded and .Iter()/.Each()/.Run() isn't called.
    /// </summary>
    public void Dispose()
    {
        QueryBuilder.Dispose();
        UserContext.Free(ref UserContext);
        IteratorContext.Free(ref IteratorContext);
        RunContext.Free(ref RunContext);
        this = default;
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
        Desc.yield_existing = value;
        return ref this;
    }

    /// <summary>
    ///     Set observer flags.
    /// </summary>
    /// <param name="flags">The flags value.</param>
    /// <returns></returns>
    public ref ObserverBuilder ObserverFlags(uint flags)
    {
        Desc.flags_ |= flags;
        return ref this;
    }

    /// <summary>
    ///     Sets the observer user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Ctx<T>(T value)
    {
        UserContext.Set(ref value);
        return ref this;
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Ctx<T>(T value, delegate*<ref T, void> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    /// <summary>
    ///     Sets the observer user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Ctx<T>(ref T value)
    {
        UserContext.Set(ref value);
        return ref this;
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref ObserverBuilder Ctx<T>(ref T value, delegate*<ref T, void> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    internal ref ObserverBuilder SetCallback(InvokerCallback callback, delegate*<ecs_iter_t*, void> invoker)
    {
        IteratorContext.Callback.Set(callback, invoker);
        return ref this;
    }

    internal ref ObserverBuilder SetRun(InvokerCallback callback, delegate*<ecs_iter_t*, void> invoker)
    {
        RunContext.Callback.Set(callback, invoker);
        return ref this;
    }

    internal Observer Build()
    {
        Desc.query = QueryBuilder.Desc;

        fixed (ecs_observer_desc_t* ptr = &Desc)
        {
            Ecs.Assert(_eventCount != 0, "Observer cannot have zero events. Use ObserverBuilder.Event() to add events.");
            Ecs.Assert(ptr->query.terms[0] != default || ptr->query.expr != null, "Observers require at least 1 term.");
            return new Observer(World, ecs_observer_init(World, ptr));
        }
    }

    Observer IQueryBuilder<ObserverBuilder, Observer>.Build()
    {
        return Build();
    }

    private UserContext* EnsureUserContext()
    {
        if (Desc.ctx != null)
            return (UserContext*)Desc.ctx;

        Desc.ctx = Memory.AllocZeroed<UserContext>(1);
        Desc.ctx_free = &Functions.UserContextFree;
        return (UserContext*)Desc.ctx;
    }

    private IteratorContext* EnsureIteratorContext()
    {
        if (Desc.callback_ctx != null)
            return (IteratorContext*)Desc.callback_ctx;

        Desc.callback = &Functions.IteratorCallback;
        Desc.callback_ctx = Memory.AllocZeroed<IteratorContext>(1);
        Desc.callback_ctx_free = &Functions.IteratorContextFree;
        return (IteratorContext*)Desc.callback_ctx;
    }

    private RunContext* EnsureRunContext()
    {
        if (Desc.run_ctx != null)
            return (RunContext*)Desc.run_ctx;

        Desc.run = &Functions.RunCallback;
        Desc.run_ctx = Memory.AllocZeroed<RunContext>(1);
        Desc.run_ctx_free = &Functions.RunContextFree;
        return (RunContext*)Desc.run_ctx;
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
