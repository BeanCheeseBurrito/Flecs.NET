using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Observer
    {
        public ecs_world_t* World { get; }
        public Entity Entity { get; }

        public Observer(
            ecs_world_t* world,
            string name = "",
            FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
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

            BindingContext.ObserverContext* observerContext = Memory.Alloc<BindingContext.ObserverContext>(1);
            observerContext[0] = observerBuilder.ObserverContext;
            BindingContext.SetCallback(ref observerContext->Iter, callback);

            ecs_observer_desc_t* observerDesc = &observerBuilder.ObserverDesc;
            observerDesc->entity = ecs_entity_init(world, &entityDesc);
            observerDesc->filter = filterBuilder.FilterDesc;
            observerDesc->filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            observerDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;
            observerDesc->binding_ctx = observerContext;
            observerDesc->binding_ctx_free = BindingContext.ObserverContextFreePointer;
            observerDesc->callback = BindingContext.ObserverIterPointer;

            Entity = new Entity(world, ecs_observer_init(world, observerDesc));

            filterBuilder.Dispose();
        }

        public Observer(ecs_world_t* world, ulong entity)
        {
            World = world;
            Entity = new Entity(world, entity);
        }

        public void Destruct()
        {
            Entity.Destruct();
        }

        public readonly void Ctx(void* ctx)
        {
            ecs_observer_desc_t desc = default;
            desc.entity = Entity;
            desc.ctx = ctx;
            ecs_observer_init(World, &desc);
        }

        public readonly void* Ctx()
        {
            return ecs_get_observer_ctx(World, Entity);
        }

        public readonly T* Ctx<T>() where T : unmanaged
        {
            return (T*)Ctx();
        }

        public readonly Filter Filter()
        {
            ref readonly EcsPoly poly = ref Entity.Get<EcsPoly>(EcsObserver);
            ecs_observer_t* observer = (ecs_observer_t*)poly.poly;
            return new Filter(World, &observer->filter);
        }

        public static implicit operator ulong(Observer observer)
        {
            return observer.Entity;
        }
    }
}
