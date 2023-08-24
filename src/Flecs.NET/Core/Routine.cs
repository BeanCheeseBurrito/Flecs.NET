using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Routine
    {
        private Entity _entity;

        public ref Entity Entity => ref _entity;
        public ref ecs_world_t* World => ref _entity.World;

        public Routine(
            ecs_world_t* world,
            string name = "",
            FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default,
            RoutineBuilder routineBuilder = default,
            Ecs.IterCallback? callback = null)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback), "Callback is null");

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

            BindingContext.RoutineContext* routineContext = Memory.Alloc<BindingContext.RoutineContext>(1);
            routineContext[0] = routineBuilder.RoutineContext;
            routineContext->QueryContext = queryBuilder.QueryContext;
            BindingContext.SetCallback(ref routineContext->Iter, callback);

            ecs_system_desc_t* routineDesc = &routineBuilder.RoutineDesc;
            routineDesc->entity = entity;
            routineDesc->query = queryBuilder.QueryDesc;
            routineDesc->query.filter = filterBuilder.Desc;
            routineDesc->query.filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            routineDesc->query.filter.terms_buffer_count = filterBuilder.Terms.Count;
            routineDesc->binding_ctx = routineContext;
            routineDesc->binding_ctx_free = BindingContext.RoutineContextFreePointer;
            routineDesc->callback = BindingContext.RoutineIterPointer;

            _entity = new Entity(world, ecs_system_init(world, routineDesc));

            filterBuilder.Dispose();
        }

        public Routine(ecs_world_t* world, ulong entity)
        {
            _entity  = new Entity(world, entity);
        }

        public void Destruct()
        {
            Entity.Destruct();
        }

        public void Ctx(void* ctx)
        {
            ecs_system_desc_t desc = default;
            desc.entity = Entity;
            desc.ctx = ctx;
            ecs_system_init(World, &desc);
        }

        public void* Ctx()
        {
            return ecs_get_system_ctx(World, Entity);
        }

        public Query Query()
        {
            return new Query(World, ecs_system_get_query(World, Entity));
        }

        public void Interval(float interval)
        {
            ecs_set_interval(World, Entity, interval);
        }

        public float Interval()
        {
            return ecs_get_interval(World, Entity);
        }

        public void Timeout(float timeout)
        {
            ecs_set_timeout(World, Entity, timeout);
        }

        public float Timeout()
        {
            return ecs_get_timeout(World, Entity);
        }

        public void Rate(int rate)
        {
            ecs_set_rate(World, Entity, rate, 0);
        }

        public void Start()
        {
            ecs_start_timer(World, Entity);
        }

        public void StopTimer()
        {
            ecs_stop_timer(World, Entity);
        }

        public void SetTickSource(ulong entity)
        {
            ecs_set_tick_source(World, Entity, entity);
        }
    }
}
