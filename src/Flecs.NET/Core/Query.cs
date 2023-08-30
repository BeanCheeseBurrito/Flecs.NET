using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_query_t.
    /// </summary>
    public unsafe struct Query : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_query_t* _handle;

        internal BindingContext.QueryContext QueryContext;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_query_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a query for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="queryBuilder"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public Query(ecs_world_t* world, string name = "", FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default)
        {
            QueryContext = queryBuilder.QueryContext;
            _world = world;

            ecs_query_desc_t* queryDesc = &queryBuilder.QueryDesc;
            queryDesc->filter = filterBuilder.Desc;
            queryDesc->filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            queryDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;

            if (!string.IsNullOrEmpty(name))
            {
                using NativeString nativeName = (NativeString)name;
                using NativeString nativeSep = (NativeString)"::";

                ecs_entity_desc_t entityDesc = default;
                entityDesc.name = nativeName;
                entityDesc.sep = nativeSep;
                entityDesc.root_sep = nativeSep;
                queryDesc->filter.entity = ecs_entity_init(world, &entityDesc);
            }

            _handle = ecs_query_init(world, queryDesc);

            if (_handle == null)
                throw new InvalidOperationException("Query failed to init");

            filterBuilder.Dispose();
        }

        /// <summary>
        ///     Creates a query from a world and handle.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="query"></param>
        public Query(ecs_world_t* world, ecs_query_t* query = null)
        {
            QueryContext = default;
            _world = world;
            _handle = query;
        }

        /// <summary>
        ///     Disposes query.
        /// </summary>
        public void Dispose()
        {
            Destruct();
        }

        /// <summary>
        ///     Destructs query and cleans up resources.
        /// </summary>
        public void Destruct()
        {
            if (Handle == null)
                return;

            ecs_query_fini(Handle);
            QueryContext.Dispose();
            World = null;
            Handle = null;
        }

        /// <summary>
        ///     Returns whether the query data changed since the last iteration.
        /// </summary>
        /// <returns></returns>
        public bool Changed()
        {
            return ecs_query_changed(Handle, null) == 1;
        }

        /// <summary>
        ///     Returns whether query is orphaned.
        /// </summary>
        /// <returns></returns>
        public bool Orphaned()
        {
            return ecs_query_orphaned(Handle) == 1;
        }

        /// <summary>
        ///     Get info for group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ecs_query_group_info_t* GroupInfo(ulong groupId)
        {
            return ecs_query_get_group_info(Handle, groupId);
        }

        /// <summary>
        ///     Get context for group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public void* GroupCtx(ulong groupId)
        {
            ecs_query_group_info_t* groupInfo = GroupInfo((groupId));
            return groupInfo == null ? null : groupInfo->ctx;
        }

        /// <summary>
        ///     Returns filter for query.
        /// </summary>
        /// <returns></returns>
        public Filter Filter()
        {
            return new Filter(World, ecs_query_get_filter(Handle));
        }

        /// <summary>
        ///     Returns the field count of the query.
        /// </summary>
        /// <returns></returns>
        public int FieldCount()
        {
            ecs_filter_t* filter = ecs_query_get_filter(Handle);
            return filter->term_count;
        }

        /// <summary>
        ///     Returns the filter string of the query.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            ecs_filter_t* filter = ecs_query_get_filter(Handle);
            return NativeString.GetStringAndFree(ecs_filter_str(World, filter));
        }

        /// <summary>
        ///     Returns the entity associated with the query.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(Handle));
        }

        /// <summary>
        ///     Iterates the query.
        /// </summary>
        /// <param name="func"></param>
        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_query_iter(World, Handle);
            while (ecs_query_next(&iter) == 1)
                Invoker.Iter(&iter, func);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachEntityCallback func)
        {
            ecs_iter_t iter = ecs_query_iter(World, Handle);
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.EachEntity(&iter, func);
        }
    }
}
