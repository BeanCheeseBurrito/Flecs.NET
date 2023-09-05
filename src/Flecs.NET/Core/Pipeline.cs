using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around pipeline.
    /// </summary>
    public unsafe struct Pipeline
    {
        private Entity _entity;

        /// <summary>
        ///     A reference to the entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

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
            pipelineDesc.query.filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            pipelineDesc.query.filter.terms_buffer_count = filterBuilder.Terms.Count;
            pipelineDesc.query.binding_ctx = queryContext;
            pipelineDesc.query.binding_ctx_free = BindingContext.QueryContextFreePointer;
            pipelineDesc.entity = entity;

            _entity = new Entity(world, ecs_pipeline_init(world, &pipelineDesc));

            if (_entity == 0)
                throw new InvalidOperationException("Pipeline failed to init.");

            filterBuilder.Dispose();
        }
    }
}
