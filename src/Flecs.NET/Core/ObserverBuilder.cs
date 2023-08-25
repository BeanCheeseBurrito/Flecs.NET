using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_observer_desc_t.
    /// </summary>
    public unsafe struct ObserverBuilder
    {
        private ecs_world_t* _world;
        private int _eventCount;

        internal ecs_observer_desc_t ObserverDesc;
        internal BindingContext.ObserverContext ObserverContext;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the observer description.
        /// </summary>
        public ref ecs_observer_desc_t Desc => ref ObserverDesc;

        /// <summary>
        ///     Creates an observer builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public ObserverBuilder(ecs_world_t* world)
        {
            ObserverDesc = default;
            ObserverContext = default;
            _world = world;
            _eventCount = default;
        }

        /// <summary>
        ///     Specify the event(s) for when the observer should run.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ref ObserverBuilder Event(ulong @event)
        {
            if (_eventCount >= 8)
                throw new InvalidOperationException();

            ObserverDesc.events[_eventCount++] = @event;
            return ref this;
        }

        /// <summary>
        ///     Specify the event(s) for when the observer should run.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref ObserverBuilder Event<T>()
        {
            return ref Event(Type<T>.Id(World));
        }

        /// <summary>
        ///     Invoke observer for anything that matches its filter on creation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref ObserverBuilder YieldExisting(bool value = true)
        {
            ObserverDesc.yield_existing = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Set observer context.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ref ObserverBuilder Ctx(void* data)
        {
            ObserverDesc.ctx = data;
            return ref this;
        }

        /// <summary>
        ///     Set observer run callback.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ref ObserverBuilder Run(Ecs.IterAction action)
        {
            BindingContext.SetCallback(ref ObserverContext.Run, action);
            ObserverDesc.run = ObserverContext.Run.Function;
            return ref this;
        }
    }
}
