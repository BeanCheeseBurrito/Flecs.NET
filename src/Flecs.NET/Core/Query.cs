using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Query : IDisposable
    {
        public ecs_world_t* World { get; private set; }
        public ecs_query_t* Handle { get; private set; }

        internal BindingContext.QueryContext QueryContext;

        public Query(ecs_world_t* world, string name = "", FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default)
        {
            World = world;
            QueryContext = queryBuilder.QueryContext;

            ecs_query_desc_t* queryDesc = &queryBuilder.QueryDesc;
            queryDesc->filter = filterBuilder.FilterDesc;
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
                queryDesc->filter.entity = ecs_entity_init(World, &entityDesc);
            }

            Handle = ecs_query_init(world, queryDesc);

            if (Handle == null)
                throw new InvalidOperationException("Query failed to init");

            filterBuilder.Dispose();
        }

        public Query(ecs_world_t* world, ecs_query_t* query = null)
        {
            World = world;
            Handle = query;
            QueryContext = default;
        }

        public void Dispose()
        {
            Destruct();
        }

        public void Destruct()
        {
            if (Handle == null)
                return;

            ecs_query_fini(Handle);
            QueryContext.Dispose();
            World = null;
            Handle = null;
        }

        public bool Changed()
        {
            return ecs_query_changed(Handle, null) == 1;
        }

        public bool Orphaned()
        {
            return ecs_query_orphaned(Handle) == 1;
        }

        public ecs_query_group_info_t* GroupInfo(ulong groupId)
        {
            return ecs_query_get_group_info(Handle, groupId);
        }

        public void* GroupCtx(ulong groupId)
        {
            ecs_query_group_info_t* groupInfo = GroupInfo((groupId));
            return groupInfo == null ? null : groupInfo->ctx;
        }

        public Filter Filter()
        {
            return new Filter(World, ecs_query_get_filter(Handle));
        }

        public int FieldCount()
        {
            ecs_filter_t* filter = ecs_query_get_filter(Handle);
            return filter->term_count;
        }

        public string Str()
        {
            ecs_filter_t* filter = ecs_query_get_filter(Handle);
            return NativeString.GetStringAndFree(ecs_filter_str(World, filter));
        }

        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(Handle));
        }

        public void Iter(Ecs.IterCallback iterCallback)
        {
            ecs_iter_t iter = ecs_query_iter(World, Handle);
            Invoker.Iter(iterCallback, ecs_query_next, &iter);
        }
    }
}
