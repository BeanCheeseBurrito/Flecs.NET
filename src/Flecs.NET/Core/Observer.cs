using System;
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
        Destruct();
    }

    /// <summary>
    ///     Sets the observer context.
    /// </summary>
    /// <param name="ctx"></param>
    public void Ctx(void* ctx)
    {
        ecs_observer_desc_t desc = default;
        desc.entity = Entity;
        desc.ctx = ctx;
        ecs_observer_init(World, &desc);
    }

    /// <summary>
    ///     Gets the observer context.
    /// </summary>
    /// <returns></returns>
    public void* Ctx()
    {
        return ecs_observer_get(World, Entity)->ctx;
    }

    /// <summary>
    ///     Gets the observer context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T* Ctx<T>() where T : unmanaged
    {
        return (T*)Ctx();
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
