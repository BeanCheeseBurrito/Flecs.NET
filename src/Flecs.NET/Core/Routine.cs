using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Routine
    {
        public ecs_world_t* World { get; }
        public Entity Entity { get; }

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

            World = world;

            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSep = (NativeString)"::";

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = nativeSep;

            ulong entity = ecs_entity_init(world, &entityDesc);
            ecs_add_id(world, entity, Macros.DependsOn(EcsOnUpdate));
            ecs_add_id(world, entity, EcsOnUpdate);

            BindingContext.Callback* bindingContext = Memory.Alloc<BindingContext.Callback>(1);
            *bindingContext = BindingContext.AllocCallback(callback);

            ecs_system_desc_t* routineDesc = &routineBuilder.RoutineDesc;
            routineDesc->entity = entity;
            routineDesc->query = queryBuilder.QueryDesc;
            routineDesc->query.filter = filterBuilder.FilterDesc;
            routineDesc->query.filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            routineDesc->query.filter.terms_buffer_count = filterBuilder.Terms.Count;
            routineDesc->binding_ctx = bindingContext;
            routineDesc->binding_ctx_free = BindingContext.FreeIterPointer;
            routineDesc->callback = BindingContext.IterPointer;

            Entity = new Entity(world, ecs_system_init(world, routineDesc));

            filterBuilder.Dispose();
        }
    }
}