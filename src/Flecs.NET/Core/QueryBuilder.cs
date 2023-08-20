using System;
using System.Runtime.InteropServices;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    // TODO: Allocate GC handles for all these functions ASAP
    public unsafe struct QueryBuilder
    {
        public ecs_world_t* World { get; }

        internal ecs_query_desc_t QueryDesc;

        public QueryBuilder(ecs_world_t* world)
        {
            World = world;
            QueryDesc = default;
        }

        public ref QueryBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            return ref OrderBy(Type<T>.Id(World), compare);
        }

        public ref QueryBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            QueryDesc.order_by = Marshal.GetFunctionPointerForDelegate(compare);
            QueryDesc.order_by_component = component;
            return ref this;
        }

        public ref QueryBuilder GroupBy<T>(Ecs.GroupByAction groupByAction)
        {
            return ref GroupBy(Type<T>.Id(World), groupByAction);
        }

        public ref QueryBuilder GroupBy(ulong component, Ecs.GroupByAction groupByAction)
        {
            QueryDesc.group_by = Marshal.GetFunctionPointerForDelegate(groupByAction);
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
            QueryDesc.group_by_ctx = ctx;
            QueryDesc.group_by_ctx_free = Marshal.GetFunctionPointerForDelegate(contextFree);
            return ref this;
        }

        public ref QueryBuilder GroupbyCtx(void* ctx)
        {
            QueryDesc.group_by_ctx = ctx;
            QueryDesc.group_by_ctx_free = IntPtr.Zero;
            return ref this;
        }

        public ref QueryBuilder OnGroupCreate(Ecs.GroupCreateAction action)
        {
            QueryDesc.on_group_create = Marshal.GetFunctionPointerForDelegate(action);
            return ref this;
        }

        public ref QueryBuilder OnGroupDelete(Ecs.GroupDeleteAction action)
        {
            QueryDesc.on_group_delete = Marshal.GetFunctionPointerForDelegate(action);
            return ref this;
        }
    }
}
