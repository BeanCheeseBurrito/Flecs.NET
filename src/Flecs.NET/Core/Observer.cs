using System;
using Flecs.NET.Core.BindingContext;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around observer.
/// </summary>
public unsafe partial struct Observer : IEquatable<Observer>, IDisposable, IEntity<Observer>
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
    ///     Gets an observer from the provided world an entity.
    /// </summary>
    /// <param name="world"></param>
    /// <param name="entity"></param>
    public Observer(ecs_world_t* world, ulong entity)
    {
        _entity = new Entity(world, entity);
    }

    /// <summary>
    ///     Creates an observer from an entity.
    /// </summary>
    /// <param name="entity"></param>
    public Observer(Entity entity)
    {
        _entity = entity;
    }

    /// <summary>
    ///     Disposes this observer.
    /// </summary>
    public void Dispose()
    {
        Entity.Destruct();
    }

    /// <summary>
    ///     Sets the observer user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(T value)
    {
        new ObserverBuilder(World, Entity)
            .Ctx(ref value)
            .Build();
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
    {
        new ObserverBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(T value, delegate*<ref T, void> callback)
    {
        new ObserverBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Sets the observer user context object.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(ref T value)
    {
        new ObserverBuilder(World, Entity)
            .Ctx(ref value)
            .Build();
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
    {
        new ObserverBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Sets the observer user context object. The provided callback will be run before the
    ///     user context object is released by flecs.
    /// </summary>
    /// <param name="value">The user context object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T">The user context type.</typeparam>
    /// <returns></returns>
    public void Ctx<T>(ref T value, delegate*<ref T, void> callback)
    {
        new ObserverBuilder(World, Entity)
            .Ctx(ref value, callback)
            .Build();
    }

    /// <summary>
    ///     Returns the context for the system.
    /// </summary>
    /// <returns></returns>
    public ref T Ctx<T>()
    {
        UserContext* context = (UserContext*)ecs_observer_get(World, Entity)->ctx;
        return ref context->Get<T>();
    }

    /// <summary>
    ///     Returns the query for the observer.
    /// </summary>
    /// <returns></returns>
    public Query Query()
    {
        return new Query(ecs_observer_get(World, Id)->query);
    }

    /// <summary>
    ///     Converts an <see cref="Observer"/> instance to its integer id.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public static implicit operator ulong(Observer observer)
    {
        return ToUInt64(observer);
    }

    /// <summary>
    ///     Converts an <see cref="Observer"/> instance to its id.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public static implicit operator Id(Observer observer)
    {
        return ToId(observer);
    }

    /// <summary>
    ///     Converts an <see cref="Observer"/> instance to its entity.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public static implicit operator Entity(Observer observer)
    {
        return ToEntity(observer);
    }

    /// <summary>
    ///     Converts an <see cref="Observer"/> instance to its integer id.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public static ulong ToUInt64(Observer observer)
    {
        return observer.Entity;
    }

    /// <summary>
    ///     Converts an <see cref="Observer"/> instance to its id.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public static Id ToId(Observer observer)
    {
        return observer.Id;
    }

    /// <summary>
    ///     Converts an <see cref="Observer"/> instance to its entity.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public static Entity ToEntity(Observer observer)
    {
        return observer.Entity;
    }

    /// <summary>
    ///     Checks if two <see cref="Observer"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Observer other)
    {
        return Entity == other.Entity;
    }

    /// <summary>
    ///     Checks if two <see cref="Observer"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is Observer other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="Observer"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Entity.GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="Observer"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Observer left, Observer right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="Observer"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Observer left, Observer right)
    {
        return !(left == right);
    }

    /// <summary>
    ///     Returns the entity's name if it has one, otherwise return its id.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Entity.ToString();
    }
}
