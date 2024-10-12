using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_pipeline_desc_t"/>.
/// </summary>
public unsafe partial struct PipelineBuilder : IDisposable, IEquatable<PipelineBuilder>, IQueryBuilder<PipelineBuilder, Pipeline>
{
    private ecs_world_t* _world;
    private ecs_pipeline_desc_t _desc;
    private QueryBuilder _queryBuilder;

    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World => ref _world;

    /// <summary>
    ///     A reference to the pipeline description.
    /// </summary>
    public ref ecs_pipeline_desc_t Desc => ref _desc;

    /// <summary>
    ///     A reference to the query builder.
    /// </summary>
    public ref QueryBuilder QueryBuilder => ref _queryBuilder;

    /// <summary>
    ///     Creates a pipeline builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    public PipelineBuilder(ecs_world_t* world)
    {
        _world = world;
        _desc = default;
        _queryBuilder = new QueryBuilder(world);
    }

    /// <summary>
    ///     Creates a pipeline builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The pipeline entity.</param>
    public PipelineBuilder(ecs_world_t* world, ulong entity) : this(world)
    {
        Desc.entity = entity;
    }

    /// <summary>
    ///     Creates a pipeline builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="name">The pipeline name.</param>
    public PipelineBuilder(ecs_world_t* world, string name) : this(world)
    {
        if (string.IsNullOrEmpty(name))
            return;

        using NativeString nativeName = (NativeString)name;

        ecs_entity_desc_t entityDesc = default;
        entityDesc.name = nativeName;
        entityDesc.sep = Pointers.DefaultSeparator;
        entityDesc.root_sep = Pointers.DefaultSeparator;
        Desc.entity = ecs_entity_init(world, &entityDesc);
    }

    /// <summary>
    ///     Disposes the pipeline builder. This should be called if the pipeline builder
    ///     will be discarded and .Build() isn't called.
    /// </summary>
    public void Dispose()
    {
        QueryBuilder.Dispose();
    }

    /// <summary>
    ///     Builds a new pipeline.
    /// </summary>
    /// <returns></returns>
    public Pipeline Build()
    {
        fixed (ecs_pipeline_desc_t* pipelineDesc = &Desc)
        {
            pipelineDesc->query = QueryBuilder.Desc;

            Entity entity = new Entity(World, ecs_pipeline_init(World, pipelineDesc));

            if (entity == 0)
                Ecs.Error("Pipeline failed to init.");

            return new Pipeline(entity);
        }
    }

    /// <summary>
    ///     Checks if two <see cref="PipelineBuilder"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(PipelineBuilder other)
    {
        return Desc == other.Desc && QueryBuilder == other.QueryBuilder;
    }

    /// <summary>
    ///     Checks if two <see cref="PipelineBuilder"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is PipelineBuilder other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="PipelineBuilder"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Desc.GetHashCode(), QueryBuilder.GetHashCode());
    }

    /// <summary>
    ///     Checks if two <see cref="PipelineBuilder"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(PipelineBuilder left, PipelineBuilder right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="PipelineBuilder"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(PipelineBuilder left, PipelineBuilder right)
    {
        return !(left == right);
    }
}
