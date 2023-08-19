using System;
using System.Runtime.InteropServices;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct ObserverBuilder
    {
        public ecs_world_t* World { get; }
        internal ecs_observer_desc_t ObserverDesc;

        private int _eventCount;

        public ObserverBuilder(ecs_world_t* world)
        {
            World = world;
            ObserverDesc = default;
            _eventCount = default;
        }

        // TODO: Add indexers for fixed-sized buffers to Bindgen.NET
        public ref ObserverBuilder Event(ulong @event)
        {
            if (_eventCount >= 8)
                throw new InvalidOperationException();

            fixed (void* eventsBuffer = &ObserverDesc.events)
            {
                ((ulong*)eventsBuffer)[_eventCount++] = @event;
                return ref this;
            }
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

        // TODO: Allocate GC handle later
        public ref ObserverBuilder Run(Ecs.IterAction action)
        {
            ObserverDesc.run = Marshal.GetFunctionPointerForDelegate(action);
            return ref this;
        }
    }
}