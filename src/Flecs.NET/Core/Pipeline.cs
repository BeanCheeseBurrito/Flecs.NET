using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around pipeline.
/// </summary>
public unsafe partial struct Pipeline : IEquatable<Pipeline>, IEntity<Pipeline>
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
    ///     Creates a pipeline from the provided world and entity id.
    /// </summary>
    /// <param name="world"></param>
    /// <param name="entity"></param>
    public Pipeline(ecs_world_t* world, ulong entity = 0)
    {
        _entity = new Entity(world, entity);
    }

    /// <summary>
    ///     Creates a pipeline from the provided entity.
    /// </summary>
    /// <param name="entity"></param>
    public Pipeline(Entity entity)
    {
        _entity = entity;
    }

    /// <summary>
    ///     Converts a <see cref="Pipeline"/> instance to its integer id.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <returns></returns>
    public static implicit operator ulong(Pipeline pipeline)
    {
        return ToUInt64(pipeline);
    }

    /// <summary>
    ///     Converts a <see cref="Pipeline"/> instance to its id.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <returns></returns>
    public static implicit operator Id(Pipeline pipeline)
    {
        return ToId(pipeline);
    }

    /// <summary>
    ///     Converts a <see cref="Pipeline"/> instance to its entity.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <returns></returns>
    public static implicit operator Entity(Pipeline pipeline)
    {
        return ToEntity(pipeline);
    }

    /// <summary>
    ///     Converts a <see cref="Pipeline"/> instance to its integer id.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <returns></returns>
    public static ulong ToUInt64(Pipeline pipeline)
    {
        return pipeline.Entity;
    }

    /// <summary>
    ///     Converts a <see cref="Pipeline"/> instance to its id.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <returns></returns>
    public static Id ToId(Pipeline pipeline)
    {
        return pipeline.Id;
    }

    /// <summary>
    ///     Converts a <see cref="Pipeline"/> instance to its entity.
    /// </summary>
    /// <param name="pipeline"></param>
    /// <returns></returns>
    public static Entity ToEntity(Pipeline pipeline)
    {
        return pipeline.Entity;
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
    ///     Checks if two <see cref="Pipeline"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Pipeline other)
    {
        return Entity == other.Entity;
    }

    /// <summary>
    ///     Checks if two <see cref="Pipeline"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is Pipeline other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="Pipeline"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Entity.GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="Pipeline"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Pipeline left, Pipeline right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="Pipeline"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Pipeline left, Pipeline right)
    {
        return !(left == right);
    }
}
