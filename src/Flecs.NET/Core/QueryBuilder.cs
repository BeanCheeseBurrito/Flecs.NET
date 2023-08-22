using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    // TODO: Free query context once .binding_context is added
    public unsafe struct QueryBuilder
    {
        public ecs_world_t* World { get; }

        internal ecs_query_desc_t QueryDesc;
        internal BindingContext.QueryContext QueryContext;

        public QueryBuilder(ecs_world_t* world)
        {
            World = world;
            QueryDesc = default;
            QueryContext = default;
        }

        public ref QueryBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            return ref OrderBy(Type<T>.Id(World), compare);
        }

        public ref QueryBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            BindingContext.SetCallback(ref QueryContext.OrderByAction, compare);
            QueryDesc.order_by = QueryContext.OrderByAction.Function;
            QueryDesc.order_by_component = component;
            return ref this;
        }

        public ref QueryBuilder GroupBy<T>(Ecs.GroupByAction groupByAction)
        {
            return ref GroupBy(Type<T>.Id(World), groupByAction);
        }

        public ref QueryBuilder GroupBy(ulong component, Ecs.GroupByAction groupByAction)
        {
            BindingContext.SetCallback(ref QueryContext.GroupByAction, groupByAction);
            QueryDesc.group_by = QueryContext.GroupByAction.Function;
            QueryDesc.group_by_id = component;
            return ref this;
        }

        public ref QueryBuilder GroupBy<T>()
        {
            return ref GroupBy(Type<T>.Id(World));
        }

        public ref QueryBuilder GroupBy(ulong component)
        {
            QueryDesc.group_by = IntPtr.Zero;
            QueryDesc.group_by_id = component;
            return ref this;
        }

        public ref QueryBuilder GroupbyCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            BindingContext.SetCallback(ref QueryContext.ContextFree, contextFree);
            QueryDesc.group_by_ctx_free = QueryContext.ContextFree.Function;
            QueryDesc.group_by_ctx = ctx;
            return ref this;
        }

        public ref QueryBuilder GroupbyCtx(void* ctx)
        {
            QueryDesc.group_by_ctx = ctx;
            QueryDesc.group_by_ctx_free = IntPtr.Zero;
            return ref this;
        }

        public ref QueryBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            BindingContext.SetCallback(ref QueryContext.GroupCreateAction, onGroupCreate);
            QueryDesc.on_group_create = QueryContext.GroupCreateAction.Function;
            return ref this;
        }

        public ref QueryBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            BindingContext.SetCallback(ref QueryContext.GroupDeleteAction, onGroupDelete);
            QueryDesc.on_group_delete = QueryContext.GroupDeleteAction.Function;
            return ref this;
        }

        public ref QueryBuilder Observable(Query parent)
        {
            return ref Observable(ref parent);
        }

        public ref QueryBuilder Observable(ref Query parent)
        {
            QueryDesc.parent = parent.Handle;
            return ref this;
        }
    }
}