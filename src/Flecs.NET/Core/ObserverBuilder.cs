using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct ObserverBuilder
    {
        public ecs_world_t* World { get; }

        internal ecs_observer_desc_t ObserverDesc;
        internal BindingContext.ObserverContext ObserverContext;

        private int _eventCount;

        public ObserverBuilder(ecs_world_t* world)
        {
            World = world;
            ObserverDesc = default;
            ObserverContext = default;
            _eventCount = default;
        }

        public ref ObserverBuilder Event(ulong @event)
        {
            if (_eventCount >= 8)
                throw new InvalidOperationException();

            ObserverDesc.events[_eventCount++] = @event;
            return ref this;
        }

        public ref ObserverBuilder Event<T>()
        {
            return ref Event(Type<T>.Id(World));
        }

        public ref ObserverBuilder YieldExisting(bool value = true)
        {
            ObserverDesc.yield_existing = Macros.Bool(value);
            return ref this;
        }

        public ref ObserverBuilder Ctx(void* data)
        {
            ObserverDesc.ctx = data;
            return ref this;
        }

        public ref ObserverBuilder Run(Ecs.IterAction action)
        {
            BindingContext.SetCallback(ref ObserverContext.Run, action);
            ObserverDesc.run = ObserverContext.Run.Function;
            return ref this;
        }
    }
}