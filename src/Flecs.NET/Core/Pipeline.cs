using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around pipeline.
    /// </summary>
    public unsafe struct Pipeline : IEquatable<Pipeline>
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
        ///     Creates a pipeline.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="entity"></param>
        public Pipeline(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default,
            ulong entity = 0)
        {
            BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
            queryContext[0] = queryBuilder.QueryContext;

            ecs_pipeline_desc_t pipelineDesc = default;
            pipelineDesc.query = queryBuilder.Desc;
            pipelineDesc.entity = entity;
            pipelineDesc.query.filter = filterBuilder.Desc;
            pipelineDesc.query.filter.terms_buffer = filterBuilder.Terms.Data;
            pipelineDesc.query.filter.terms_buffer_count = filterBuilder.Terms.Count;
            pipelineDesc.query.binding_ctx = queryContext;
            pipelineDesc.query.binding_ctx_free = BindingContext.QueryContextFreePointer;
            pipelineDesc.entity = entity;

            _entity = new Entity(world, ecs_pipeline_init(world, &pipelineDesc));

            if (_entity == 0)
                throw new InvalidOperationException("Pipeline failed to init.");

            filterBuilder.Dispose();
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
        /// <exception cref="NotImplementedException"></exception>
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
}
