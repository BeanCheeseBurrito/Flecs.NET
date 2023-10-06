using System;
using System.Runtime.CompilerServices;
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
        ///     A reference to the entity.
        /// </summary>
        public ref Id Id => ref _entity.Id;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _entity.World;

        /// <summary>
        ///      Creates an observer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="observerBuilder"></param>
        /// <param name="callback"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Observer(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.IterCallback? callback = null,
            string name = "")
        {
            _entity = default;

           InitObserver(
               true,
               BindingContext.ObserverIterPointer,
               ref callback,
               ref world,
               ref filterBuilder,
               ref observerBuilder,
               ref name
            );
        }

        /// <summary>
        ///      Creates an observer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="observerBuilder"></param>
        /// <param name="callback"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Observer(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.EachEntityCallback? callback = null,
            string name = "")
        {
            _entity = default;

            InitObserver(
                true,
                BindingContext.ObserverEachEntityPointer,
                ref callback,
                ref world,
                ref filterBuilder,
                ref observerBuilder,
                ref name
            );
        }

        /// <summary>
        ///     Creates an observer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="observerBuilder"></param>
        /// <param name="callback"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Observer(
            ecs_world_t* world,
            FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.EachIndexCallback? callback = null,
            string name = "")
        {
            _entity = default;

            InitObserver(
                true,
                BindingContext.ObserverEachIndexPointer,
                ref callback,
                ref world,
                ref filterBuilder,
                ref observerBuilder,
                ref name
            );
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

        internal ref Observer InitObserver<T>(
            bool storeFuncPtr,
            IntPtr internalCallback,
            ref T? userCallback,
            ref ecs_world_t* world,
            ref FilterBuilder filterBuilder,
            ref ObserverBuilder observerBuilder,
            ref string name) where T : Delegate
        {
            if (userCallback == null)
                throw new ArgumentNullException(nameof(userCallback), "User provided observer callback is null");

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;

            BindingContext.ObserverContext* observerContext = Memory.Alloc<BindingContext.ObserverContext>(1);
            observerContext[0] = observerBuilder.ObserverContext;
            BindingContext.SetCallback(ref observerContext->Iterator, userCallback, storeFuncPtr);

            ecs_observer_desc_t* observerDesc =
                (ecs_observer_desc_t*)Unsafe.AsPointer(ref observerBuilder.ObserverDesc);

            Ecs.Assert(observerBuilder.EventCount != 0,
                "Observer cannot have zero events. Use ObserverBuilder.Event() to add events.");

            observerDesc->entity = ecs_entity_init(world, &entityDesc);
            observerDesc->filter = filterBuilder.Desc;
            observerDesc->filter.terms_buffer = filterBuilder.Terms.Data;
            observerDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;
            observerDesc->binding_ctx_free = BindingContext.ObserverContextFreePointer;
            observerDesc->binding_ctx = observerContext;
            observerDesc->callback = internalCallback;

            _entity = new Entity(world, ecs_observer_init(world, observerDesc));
            filterBuilder.Dispose();

            return ref this;
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

        /// <summary>
        ///      Returns the entity's name if it has one, otherwise return its id.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Entity.ToString();
        }
    }
}
