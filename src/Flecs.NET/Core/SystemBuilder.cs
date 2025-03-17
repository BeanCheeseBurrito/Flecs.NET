using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_system_desc_t"/>.
/// </summary>
public unsafe partial struct SystemBuilder : IDisposable, IEquatable<SystemBuilder>, IQueryBuilder<SystemBuilder, System_>
{
    private ecs_world_t* _world;
    private ecs_system_desc_t _desc;
    private QueryBuilder _queryBuilder;

    internal ref UserContext UserContext => ref *EnsureUserContext();
    internal ref IteratorContext IteratorContext => ref *EnsureIteratorContext();
    internal ref RunContext RunContext => ref *EnsureRunContext();

    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World => ref _world;

    /// <summary>
    ///     A reference to the system description.
    /// </summary>
    public ref ecs_system_desc_t Desc => ref _desc;

    /// <summary>
    ///     A reference to the query builder.
    /// </summary>
    public ref QueryBuilder QueryBuilder => ref _queryBuilder;

    /// <summary>
    ///     Creates a system builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    public SystemBuilder(ecs_world_t* world)
    {
        _world = world;
        _desc = default;
        _queryBuilder = new QueryBuilder(world);

        ecs_entity_desc_t entityDesc = default;
        Desc.entity = ecs_entity_init(world, &entityDesc);
        ecs_add_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsOnUpdate));
        ecs_add_id(world, Desc.entity, EcsOnUpdate);
    }

    /// <summary>
    ///     Creates a system builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="name">The system name.</param>
    public SystemBuilder(ecs_world_t* world, string name)
    {
        _world = world;
        _desc = default;
        _queryBuilder = new QueryBuilder(world);

        using NativeString nativeName = (NativeString)name;

        ecs_entity_desc_t entityDesc = default;
        entityDesc.name = nativeName;
        entityDesc.sep = Pointers.DefaultSeparator;
        entityDesc.root_sep = Pointers.DefaultSeparator;

        Desc.entity = ecs_entity_init(world, &entityDesc);

        if (ecs_has_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsWildcard)))
            return;

        ecs_add_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsOnUpdate));
        ecs_add_id(world, Desc.entity, EcsOnUpdate);
    }

    /// <summary>
    ///     Creates a system builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The system entity.</param>
    public SystemBuilder(ecs_world_t* world, ulong entity)
    {
        _world = world;
        _desc = default;
        _queryBuilder = new QueryBuilder(world);

        ecs_entity_desc_t entityDesc = default;
        entityDesc.id = entity;

        Desc.entity = ecs_entity_init(world, &entityDesc);

        if (ecs_has_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsWildcard)))
            return;

        ecs_add_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsOnUpdate));
        ecs_add_id(world, Desc.entity, EcsOnUpdate);
    }

    /// <summary>
    ///     Disposes the system builder. This should be called if the system builder
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
    ///     Specify in which phase the system should run.
    /// </summary>
    /// <param name="phase"></param>
    /// <returns></returns>
    public ref SystemBuilder Kind(ulong phase)
    {
        ulong currentPhase = ecs_get_target(World, Desc.entity, EcsDependsOn, 0);

        if (currentPhase != 0)
        {
            ecs_remove_id(World, Desc.entity, Ecs.Pair(EcsDependsOn, currentPhase));
            ecs_remove_id(World, Desc.entity, currentPhase);
        }

        if (phase != 0)
        {
            ecs_add_id(World, Desc.entity, Ecs.Pair(EcsDependsOn, phase));
            ecs_add_id(World, Desc.entity, phase);
        }

        return ref this;
    }

    /// <summary>
    ///     Specify in which phase the system should run.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref SystemBuilder Kind<T>()
    {
        return ref Kind(Type<T>.Id(World));
    }

    /// <summary>
    ///     Specify in which phase the system should run.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref SystemBuilder Kind<T>(T value) where T : Enum
    {
        return ref Kind(Type<T>.Id(World, value));
    }

    /// <summary>
    ///     Specify whether system can run on multiple threads.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public ref SystemBuilder MultiThreaded(bool value = true)
    {
        Desc.multi_threaded = value;
        return ref this;
    }

    /// <summary>
    ///     Specify whether system should be ran in staged context.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public ref SystemBuilder Immediate(bool value = true)
    {
        Desc.immediate = value;
        return ref this;
    }

    /// <summary>
    ///     Set system interval.
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public ref SystemBuilder Interval(float interval)
    {
        Desc.interval = interval;
        return ref this;
    }

    /// <summary>
    ///     Set system rate.
    /// </summary>
    /// <param name="tickSource"></param>
    /// <param name="rate"></param>
    /// <returns></returns>
    public ref SystemBuilder Rate(ulong tickSource, int rate)
    {
        Desc.rate = rate;
        Desc.tick_source = tickSource;
        return ref this;
    }

    /// <summary>
    ///     Set system rate.
    /// </summary>
    /// <param name="rate"></param>
    /// <returns></returns>
    public ref SystemBuilder Rate(int rate)
    {
        Desc.rate = rate;
        return ref this;
    }

    /// <summary>
    ///     Set tick source.
    /// </summary>
    /// <param name="tickSource"></param>
    /// <returns></returns>
    public ref SystemBuilder TickSource(ulong tickSource)
    {
        Desc.tick_source = tickSource;
        return ref this;
    }

    /// <summary>
    ///     Set tick source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref SystemBuilder TickSource<T>()
    {
        Desc.tick_source = Type<T>.Id(World);
        return ref this;
    }

    /// <summary>
    ///     Sets the system user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref SystemBuilder Ctx<T>(T value)
    {
        UserContext.Set(ref value);
        return ref this;
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref SystemBuilder Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref SystemBuilder Ctx<T>(T value, delegate*<ref T, void> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    /// <summary>
    ///     Sets the system user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref SystemBuilder Ctx<T>(ref T value)
    {
        UserContext.Set(ref value);
        return ref this;
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref SystemBuilder Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public ref SystemBuilder Ctx<T>(ref T value, delegate*<ref T, void> callback)
    {
        UserContext.Set(ref value, callback);
        return ref this;
    }

    internal ref SystemBuilder SetCallback(InvokerCallback callback, delegate*<ecs_iter_t*, void> invoker)
    {
        IteratorContext.Callback.Set(callback, invoker);
        return ref this;
    }

    internal ref SystemBuilder SetRun(InvokerCallback callback, delegate*<ecs_iter_t*, void> invoker)
    {
        RunContext.Callback.Set(callback, invoker);
        return ref this;
    }

    internal System_ Build()
    {
        Desc.query = QueryBuilder.Desc;

        fixed (ecs_system_desc_t* ptr = &Desc)
            return new System_(World, ecs_system_init(World, ptr));
    }

    System_ IQueryBuilder<SystemBuilder, System_>.Build()
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
    ///     Checks if two <see cref="SystemBuilder"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(SystemBuilder other)
    {
        return Desc == other.Desc && QueryBuilder == other.QueryBuilder;
    }

    /// <summary>
    ///     Checks if two <see cref="SystemBuilder"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is SystemBuilder other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="SystemBuilder"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Desc.GetHashCode(), QueryBuilder.GetHashCode());
    }

    /// <summary>
    ///     Checks if two <see cref="SystemBuilder"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(SystemBuilder left, SystemBuilder right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="SystemBuilder"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(SystemBuilder left, SystemBuilder right)
    {
        return !(left == right);
    }
}
