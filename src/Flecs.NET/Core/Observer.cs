using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around observer..
    /// </summary>
    public unsafe struct Observer
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

        private Observer(
            ecs_world_t* world,
            ecs_observer_desc_t* observerDesc,
            ref FilterBuilder filterBuilder,
            ref ObserverBuilder observerBuilder,
            ref string name)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSep = (NativeString)"::";

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = nativeSep;

            BindingContext.ObserverContext* observerContext = Memory.Alloc<BindingContext.ObserverContext>(1);
            observerContext[0] = observerBuilder.ObserverContext;

            observerDesc->entity = ecs_entity_init(world, &entityDesc);
            observerDesc->filter = filterBuilder.Desc;
            observerDesc->filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            observerDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;
            observerDesc->binding_ctx_free = BindingContext.ObserverContextFreePointer;
            observerDesc->binding_ctx = observerContext;

            _entity = default;
        }

        /// <summary>
        ///     Creates an observer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="observerBuilder"></param>
        /// <param name="callback"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Observer(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.IterCallback? callback = null,
            string name = "")
            : this(world, &observerBuilder.ObserverDesc, ref filterBuilder, ref observerBuilder, ref name)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback), "Callback is null");

            ecs_observer_desc_t* observerDesc = &observerBuilder.ObserverDesc;
            observerDesc->callback = BindingContext.ObserverIterPointer;

            BindingContext.ObserverContext* context = (BindingContext.ObserverContext*)observerDesc->binding_ctx;
            BindingContext.SetCallback(ref context->Iterator, callback);

            _entity = new Entity(world, ecs_observer_init(world, observerDesc));
            filterBuilder.Dispose();
        }

        /// <summary>
        ///     Creates an observer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="observerBuilder"></param>
        /// <param name="callback"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Observer(
            ecs_world_t* world,
            string name = "",
            FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.EachEntityCallback? callback = null)
            : this(world, &observerBuilder.ObserverDesc, ref filterBuilder, ref observerBuilder, ref name)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback), "Callback is null");

            ecs_observer_desc_t* observerDesc = &observerBuilder.ObserverDesc;
            observerDesc->callback = BindingContext.ObserverEachEntityPointer;

            BindingContext.ObserverContext* context = (BindingContext.ObserverContext*)observerDesc->binding_ctx;
            BindingContext.SetCallback(ref context->Iterator, callback);

            _entity = new Entity(world, ecs_observer_init(world, observerDesc));
            filterBuilder.Dispose();
        }

        /// <summary>
        ///     Gets an observer from the provided world an entity.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        public Observer(ecs_world_t* world, ulong entity)
        {
            _entity = new Entity(world, entity);
        }

        /// <summary>
        ///     Destructs the observer.
        /// </summary>
        public void Destruct()
        {
            Entity.Destruct();
        }

        /// <summary>
        ///     Sets the observer context.
        /// </summary>
        /// <param name="ctx"></param>
        public void Ctx(void* ctx)
        {
            ecs_observer_desc_t desc = default;
            desc.entity = Entity;
            desc.ctx = ctx;
            ecs_observer_init(World, &desc);
        }

        /// <summary>
        ///     Gets the observer context.
        /// </summary>
        /// <returns></returns>
        public void* Ctx()
        {
            return ecs_observer_get_ctx(World, Entity);
        }

        /// <summary>
        ///     Gets the observer context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* Ctx<T>() where T : unmanaged
        {
            return (T*)Ctx();
        }

        /// <summary>
        ///     Returns the filter for the observer.
        /// </summary>
        /// <returns></returns>
        public Filter Filter()
        {
            ref readonly EcsPoly poly = ref Entity.Get<EcsPoly>(EcsObserver);
            ecs_observer_t* observer = (ecs_observer_t*)poly.poly;
            return new Filter(World, &observer->filter);
        }

        /// <summary>
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public static implicit operator ulong(Observer observer)
        {
            return observer.Entity;
        }
    }
}
