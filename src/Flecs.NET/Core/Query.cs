using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Query
    {
        public ecs_world_t* World { get; }
        public ecs_query_t* QueryPtr { get; }

        public Query(ecs_world_t* world, FilterBuilder filterBuilder = default, QueryBuilder queryBuilder = default)
        {
            World = world;

            ecs_query_desc_t* queryDesc = &queryBuilder.QueryDesc;
            queryDesc->filter = filterBuilder.FilterDesc;
            queryDesc->filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            queryDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;

            QueryPtr = ecs_query_init(world, queryDesc);

            if (QueryPtr == null)
                throw new InvalidOperationException("Query failed to init");

            filterBuilder.Dispose();
        }

        public void Iter(Ecs.IterCallback iterCallback)
        {
            ecs_iter_t iter = ecs_query_iter(World, QueryPtr);
            Invoker.Iter(iterCallback, ecs_query_next, &iter);
        }

        public void Each(Ecs.EachCallback func)
        {
            ecs_iter_t iter = ecs_query_iter(World, QueryPtr);
            Invoker.Each(func, ecs_query_next_instanced, &iter);
        }
    }
}