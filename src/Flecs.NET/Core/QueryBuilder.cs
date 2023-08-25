using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Wrapper around ecs_query_desc_t.
    /// </summary>
    public unsafe struct QueryBuilder
    {
        private ecs_world_t* _world;

        internal ecs_query_desc_t QueryDesc;
        internal BindingContext.QueryContext QueryContext;

        /// <summary>
        ///     Reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Reference to the query description.
        /// </summary>
        public ref ecs_query_desc_t Desc => ref QueryDesc;

        /// <summary>
        ///     Creates a query builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public QueryBuilder(ecs_world_t* world)
        {
            QueryDesc = default;
            QueryContext = default;
            _world = world;
        }

        /// <summary>
        ///     Sort the output of a query.
        /// </summary>
        /// <param name="compare"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            return ref OrderBy(Type<T>.Id(World), compare);
        }

        /// <summary>
        ///     Sort the output of a query.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public ref QueryBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            BindingContext.SetCallback(ref QueryContext.OrderByAction, compare);
            QueryDesc.order_by = QueryContext.OrderByAction.Function;
            QueryDesc.order_by_component = component;
            return ref this;
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="groupByAction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder GroupBy<T>(Ecs.GroupByAction groupByAction)
        {
            return ref GroupBy(Type<T>.Id(World), groupByAction);
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="groupByAction"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component, Ecs.GroupByAction groupByAction)
        {
            BindingContext.SetCallback(ref QueryContext.GroupByAction, groupByAction);
            QueryDesc.group_by = QueryContext.GroupByAction.Function;
            QueryDesc.group_by_id = component;
            return ref this;
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder GroupBy<T>()
        {
            return ref GroupBy(Type<T>.Id(World));
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component)
        {
            QueryDesc.group_by = IntPtr.Zero;
            QueryDesc.group_by_id = component;
            return ref this;
        }

        /// <summary>
        ///     Specify context to be passed to group_by function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="contextFree"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupbyCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            BindingContext.SetCallback(ref QueryContext.ContextFree, contextFree);
            QueryDesc.group_by_ctx_free = QueryContext.ContextFree.Function;
            QueryDesc.group_by_ctx = ctx;
            return ref this;
        }

        /// <summary>
        ///     Specify context to be passed to group_by function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupbyCtx(void* ctx)
        {
            QueryDesc.group_by_ctx = ctx;
            QueryDesc.group_by_ctx_free = IntPtr.Zero;
            return ref this;
        }

        /// <summary>
        ///     Specify on_group_create action.
        /// </summary>
        /// <param name="onGroupCreate"></param>
        /// <returns></returns>
        public ref QueryBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            BindingContext.SetCallback(ref QueryContext.GroupCreateAction, onGroupCreate);
            QueryDesc.on_group_create = QueryContext.GroupCreateAction.Function;
            return ref this;
        }

        /// <summary>
        ///     Specify on_group_delete action.
        /// </summary>
        /// <param name="onGroupDelete"></param>
        /// <returns></returns>
        public ref QueryBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            BindingContext.SetCallback(ref QueryContext.GroupDeleteAction, onGroupDelete);
            QueryDesc.on_group_delete = QueryContext.GroupDeleteAction.Function;
            return ref this;
        }

        /// <summary>
        ///     Specify parent query (creates subquery)
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public ref QueryBuilder Observable(Query parent)
        {
            return ref Observable(ref parent);
        }

        /// <summary>
        ///     Specify parent query (creates subquery)
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public ref QueryBuilder Observable(ref Query parent)
        {
            QueryDesc.parent = parent.Handle;
            return ref this;
        }
    }
}
