using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Wrapper around system.
    /// </summary>
    public unsafe struct Routine
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

        private Routine(
            ecs_world_t* world,
            ecs_system_desc_t* routineDesc,
            ref FilterBuilder filterBuilder,
            ref QueryBuilder queryBuilder,
            ref RoutineBuilder routineBuilder,
            ref string name)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSep = (NativeString)"::";

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = nativeSep;

            ulong entity = ecs_entity_init(world, &entityDesc);
            ulong currentPhase = ecs_get_target(world, entity, EcsDependsOn, 0);

            if (currentPhase == 0 && routineBuilder.CurrentPhase != 0)
            {
                ecs_add_id(world, entity, Macros.DependsOn(routineBuilder.CurrentPhase));
                ecs_add_id(world, entity, routineBuilder.CurrentPhase);
            }
            else if (currentPhase == 0)
            {
                ecs_add_id(world, entity, Macros.DependsOn(EcsOnUpdate));
                ecs_add_id(world, entity, EcsOnUpdate);
            }

            BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
            queryContext[0] = queryBuilder.QueryContext;

            BindingContext.RoutineContext* routineContext = Memory.Alloc<BindingContext.RoutineContext>(1);
            routineContext[0] = routineBuilder.RoutineContext;
            routineContext->QueryContext = queryBuilder.QueryContext;

            routineDesc->entity = entity;
            routineDesc->query = queryBuilder.QueryDesc;
            routineDesc->query.filter = filterBuilder.Desc;
            routineDesc->query.filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            routineDesc->query.filter.terms_buffer_count = filterBuilder.Terms.Count;
            routineDesc->query.binding_ctx = queryContext;
            routineDesc->query.binding_ctx_free = BindingContext.QueryContextFreePointer;
            routineDesc->binding_ctx_free = BindingContext.RoutineContextFreePointer;
            routineDesc->binding_ctx = routineContext;

            _entity = default;
        }

        /// <summary>
        ///      Creates a routine for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="routineBuilder"></param>
        /// <param name="callback"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Routine(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default,
            RoutineBuilder routineBuilder = default,
            Ecs.IterCallback? callback = null,
            string name = "")
            : this(world, &routineBuilder.RoutineDesc, ref filterBuilder, ref queryBuilder, ref routineBuilder, ref name)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback), "Callback is null");

            ecs_system_desc_t* routineDesc = &routineBuilder.RoutineDesc;
            routineDesc->callback = BindingContext.RoutineIterPointer;

            BindingContext.RoutineContext* context = (BindingContext.RoutineContext*)routineDesc->binding_ctx;
            BindingContext.SetCallback(ref context->Iterator, callback);

            _entity = new Entity(world, ecs_system_init(world, routineDesc));
            filterBuilder.Dispose();
        }

        /// <summary>
        ///     Creates a routine for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="routineBuilder"></param>
        /// <param name="callback"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Routine(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default,
            RoutineBuilder routineBuilder = default,
            Ecs.EachEntityCallback? callback = null,
            string name = "")
            : this(world, &routineBuilder.RoutineDesc, ref filterBuilder, ref queryBuilder, ref routineBuilder, ref name)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback), "Callback is null");

            ecs_system_desc_t* routineDesc = &routineBuilder.RoutineDesc;
            routineDesc->callback = BindingContext.RoutineEachEntityPointer;

            BindingContext.RoutineContext* context = (BindingContext.RoutineContext*)routineDesc->binding_ctx;
            BindingContext.SetCallback(ref context->Iterator, callback);

            _entity = new Entity(world, ecs_system_init(world, routineDesc));
            filterBuilder.Dispose();
        }

        /// <summary>
        ///     Creates a routine for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        public Routine(ecs_world_t* world, ulong entity)
        {
            _entity = new Entity(world, entity);
        }

        /// <summary>
        ///     Destructs the routine.
        /// </summary>
        public void Destruct()
        {
            Entity.Destruct();
        }

        /// <summary>
        ///     Sets the context for the routine.
        /// </summary>
        /// <param name="ctx"></param>
        public void Ctx(void* ctx)
        {
            ecs_system_desc_t desc = default;
            desc.entity = Entity;
            desc.ctx = ctx;
            ecs_system_init(World, &desc);
        }

        /// <summary>
        ///     Returns the context for the routine.
        /// </summary>
        /// <returns></returns>
        public void* Ctx()
        {
            return ecs_system_get_ctx(World, Entity);
        }

        /// <summary>
        ///     Returns the query for the routine.
        /// </summary>
        /// <returns></returns>
        public Query Query()
        {
            return new Query(World, ecs_system_get_query(World, Entity));
        }

        /// <summary>
        ///     Sets the interval for the routine.
        /// </summary>
        /// <param name="interval"></param>
        public void Interval(float interval)
        {
            ecs_set_interval(World, Entity, interval);
        }

        /// <summary>
        ///     Returns the interval for the routine.
        /// </summary>
        /// <returns></returns>
        public float Interval()
        {
            return ecs_get_interval(World, Entity);
        }

        /// <summary>
        ///     Sets the timeout for the routine.
        /// </summary>
        /// <param name="timeout"></param>
        public void Timeout(float timeout)
        {
            ecs_set_timeout(World, Entity, timeout);
        }

        /// <summary>
        ///     Gets the timeout for the routine.
        /// </summary>
        /// <returns></returns>
        public float Timeout()
        {
            return ecs_get_timeout(World, Entity);
        }

        /// <summary>
        ///     Sets the rate for the routine.
        /// </summary>
        /// <param name="rate"></param>
        public void Rate(int rate)
        {
            ecs_set_rate(World, Entity, rate, 0);
        }

        /// <summary>
        ///     Starts the timer.
        /// </summary>
        public void Start()
        {
            ecs_start_timer(World, Entity);
        }

        /// <summary>
        ///     Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            ecs_stop_timer(World, Entity);
        }

        /// <summary>
        ///     Sets the external tick source.
        /// </summary>
        /// <param name="entity"></param>
        public void SetTickSource(ulong entity)
        {
            ecs_set_tick_source(World, Entity, entity);
        }
    }
}
