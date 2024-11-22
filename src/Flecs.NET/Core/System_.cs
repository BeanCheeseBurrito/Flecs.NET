using System;
using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core.BindingContext;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Wrapper around system.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public unsafe partial struct System_ : IDisposable, IEquatable<System_>, IEntity<System_>
{
    private Entity _entity;

    /// <summary>
    ///     A reference to the entity.
    /// </summary>
    public ref Entity Entity => ref _entity;

    /// <summary>
    ///     A reference to the entity.
    /// </summary>
    public ref Id Id => ref _entity.Id;

    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World => ref _entity.World;

    /// <summary>
    ///     Creates a system from world and id.
    /// </summary>
    /// <param name="world"></param>
    /// <param name="entity"></param>
    public System_(ecs_world_t* world, ulong entity)
    {
        _entity = new Entity(world, entity);
    }

    /// <summary>
    ///     Creates a system from an entity.
    /// </summary>
    /// <param name="entity"></param>
    public System_(Entity entity)
    {
        _entity = entity;
    }

    /// <summary>
    ///     Disposes this system.
    /// </summary>
    public void Dispose()
    {
        Entity.Destruct();
    }

    /// <summary>
    ///     Sets the system user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(T value)
    {
        new SystemBuilder(World, Entity)
            .Ctx(ref value)
            .Build();
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
    {
        new SystemBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(T value, delegate*<ref T, void> callback)
    {
        new SystemBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Sets the system user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(ref T value)
    {
        new SystemBuilder(World, Entity)
            .Ctx(ref value)
            .Build();
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
    {
        new SystemBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Sets the system user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(ref T value, delegate*<ref T, void> callback)
    {
        new SystemBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Returns the context for the system.
    /// </summary>
    /// <returns></returns>
    public ref T Ctx<T>()
    {
        UserContext* context = (UserContext*)ecs_system_get(World, Entity)->ctx;
        return ref context->Get<T>();
    }

    /// <summary>
    ///     Returns the query for the system.
    /// </summary>
    /// <returns></returns>
    public Query Query()
    {
        return new Query(ecs_system_get(World, Entity)->query);
    }

    /// <summary>
    ///     Run the system.
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Run(float deltaTime = 0)
    {
        ecs_run(World, Id, deltaTime, null);
    }

    /// <summary>
    ///     Run the system with a param.
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="param"></param>
    public void RunWithParam(float deltaTime = 0, void* param = null)
    {
        ecs_run(World, Id, deltaTime, param);
    }

    /// <summary>
    ///     Run the system.
    /// </summary>
    /// <param name="stageCurrent"></param>
    /// <param name="stageCount"></param>
    /// <param name="deltaTime"></param>
    public void RunWorker(int stageCurrent, int stageCount, float deltaTime = 0)
    {
        RunWorkerWithParam(stageCurrent, stageCount, deltaTime, null);
    }

    /// <summary>
    ///     Run the system with a param.
    /// </summary>
    /// <param name="stageCurrent"></param>
    /// <param name="stageCount"></param>
    /// <param name="deltaTime"></param>
    /// <param name="param"></param>
    public void RunWorkerWithParam(int stageCurrent, int stageCount, float deltaTime = 0, void* param = null)
    {
        if (stageCount != 0)
            ecs_run_worker(World, Id, stageCurrent, stageCount, deltaTime, param);
        else
            ecs_run(World, Id, deltaTime, param);

    }

    /// <summary>
    ///     Sets the interval for the system.
    /// </summary>
    /// <param name="interval"></param>
    public void Interval(float interval)
    {
        ecs_set_interval(World, Entity, interval);
    }

    /// <summary>
    ///     Returns the interval for the system.
    /// </summary>
    /// <returns></returns>
    public float Interval()
    {
        return ecs_get_interval(World, Entity);
    }

    /// <summary>
    ///     Sets the timeout for the system.
    /// </summary>
    /// <param name="timeout"></param>
    public void Timeout(float timeout)
    {
        ecs_set_timeout(World, Entity, timeout);
    }

    /// <summary>
    ///     Gets the timeout for the system.
    /// </summary>
    /// <returns></returns>
    public float Timeout()
    {
        return ecs_get_timeout(World, Entity);
    }

    /// <summary>
    ///     Sets the rate for the system.
    /// </summary>
    /// <param name="rate"></param>
    public void Rate(int rate)
    {
        ecs_set_rate(World, Entity, rate, 0);
    }

    /// <summary>
    ///     Starts the timer.
    /// </summary>
    public void Start()
    {
        ecs_start_timer(World, Entity);
    }

    /// <summary>
    ///     Stops the timer.
    /// </summary>
    public void StopTimer()
    {
        ecs_stop_timer(World, Entity);
    }

    /// <summary>
    ///     Sets the external tick source.
    /// </summary>
    /// <param name="entity"></param>
    public void SetTickSource(ulong entity)
    {
        ecs_set_tick_source(World, Entity, entity);
    }

    /// <summary>
    ///     Sets the external tick source.
    /// </summary>
    /// <param name="timerEntity"></param>
    public void SetTickSource(TimerEntity timerEntity)
    {
        ecs_set_tick_source(World, Entity, timerEntity.Entity);
    }

    /// <summary>
    ///     Sets the external tick source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SetTickSource<T>()
    {
        ecs_set_tick_source(World, Entity, Type<T>.Id(World));
    }

    /// <summary>
    ///     Converts a <see cref="System_"/> instance to its integer id.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public static implicit operator ulong(System_ system)
    {
        return ToUInt64(system);
    }

    /// <summary>
    ///     Converts a <see cref="System_"/> instance to its id.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public static implicit operator Id(System_ system)
    {
        return ToId(system);
    }

    /// <summary>
    ///     Converts a <see cref="System_"/> instance to its entity.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public static implicit operator Entity(System_ system)
    {
        return ToEntity(system);
    }

    /// <summary>
    ///     Converts a <see cref="System_"/> instance to its integer id.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public static ulong ToUInt64(System_ system)
    {
        return system.Entity;
    }

    /// <summary>
    ///     Converts a <see cref="System_"/> instance to its id.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public static Id ToId(System_ system)
    {
        return system.Id;
    }

    /// <summary>
    ///     Converts a <see cref="System_"/> instance to its entity.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public static Entity ToEntity(System_ system)
    {
        return system.Entity;
    }

    /// <summary>
    ///     Returns the entity's name if it has one, otherwise return its id.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Entity.ToString();
    }

    /// <summary>
    ///     Checks if two <see cref="System_"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(System_ other)
    {
        return Entity == other.Entity;
    }

    /// <summary>
    ///     Checks if two <see cref="System_"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is System_ system && Equals(system);
    }

    /// <summary>
    ///     Return the hash code of the <see cref="System_"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Entity.GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="System_"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(System_ left, System_ right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="System_"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(System_ left, System_ right)
    {
        return !(left == right);
    }
}
