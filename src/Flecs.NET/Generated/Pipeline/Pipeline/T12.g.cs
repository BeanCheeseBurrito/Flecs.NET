// /_/src/Flecs.NET/Generated/Pipeline/Pipeline/T12.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Pipeline.cs
#nullable enable

using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A type-safe wrapper around <see cref="Pipeline"/> that takes 12 type arguments.
/// </summary>
/// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam>
public unsafe partial struct Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IEquatable<Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>, IEntity<Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>
{
    private Pipeline _pipeline;

    /// <inheritdoc cref="Pipeline.Entity"/>
    public ref Entity Entity => ref _pipeline.Entity;

    /// <inheritdoc cref="Pipeline.Id"/>
    public ref Id Id => ref _pipeline.Id;

    /// <inheritdoc cref="Pipeline.World"/>
    public ref ecs_world_t* World => ref _pipeline.World;
    
    /// <summary>
    ///     Creates a new pipeline with the provided pipeline.
    /// </summary>
    /// <param name="pipeline">The pipeline.</param>
    public Pipeline(Pipeline pipeline)
    {
        _pipeline = pipeline;
    }

    /// <inheritdoc cref="Pipeline(ecs_world_t*, ulong)"/>
    public Pipeline(ecs_world_t* world, ulong entity = 0)
    {
        _pipeline = new Pipeline(world, entity);
    }

    /// <inheritdoc cref="Pipeline(Core.Entity)"/>
    public Pipeline(Entity entity)
    {
       _pipeline = new Pipeline(entity);
    }

    /// <inheritdoc cref="Pipeline.ToUInt64(Pipeline)"/>
    public static implicit operator ulong(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pipeline)
    {
        return ToUInt64(pipeline);
    }

    /// <inheritdoc cref="Pipeline.ToId(Pipeline)"/>
    public static implicit operator Id(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pipeline)
    {
        return ToId(pipeline);
    }

    /// <inheritdoc cref="Pipeline.ToEntity(Pipeline)"/>
    public static implicit operator Entity(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pipeline)
    {
        return ToEntity(pipeline);
    }

    /// <inheritdoc cref="Pipeline.ToUInt64(Pipeline)"/>
    public static ulong ToUInt64(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pipeline)
    {
        return pipeline.Entity;
    }

    /// <inheritdoc cref="Pipeline.ToId(Pipeline)"/>
    public static Id ToId(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pipeline)
    {
        return pipeline.Id;
    }

    /// <inheritdoc cref="Pipeline.ToEntity(Pipeline)"/>
    public static Entity ToEntity(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pipeline)
    {
        return pipeline.Entity;
    }

    /// <inheritdoc cref="Pipeline.ToString()"/>
    public override string ToString()
    {
        return Entity.ToString();
    }

    /// <inheritdoc cref="Pipeline.Equals(Pipeline)"/>
    public bool Equals(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> other)
    {
        return Entity == other.Entity;
    }

    /// <inheritdoc cref="Pipeline.Equals(object)"/>
    public override bool Equals(object? obj)
    {
        return obj is Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> other && Equals(other);
    }

    /// <inheritdoc cref="Pipeline.GetHashCode()"/>
    public override int GetHashCode()
    {
        return Entity.GetHashCode();
    }

    /// <inheritdoc cref="Pipeline.op_Equality"/>
    public static bool operator ==(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> left, Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc cref="Pipeline.op_Inequality"/>
    public static bool operator !=(Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> left, Pipeline<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> right)
    {
        return !(left == right);
    }
}