using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct RoutineBuilder
    {
        public ecs_world_t* World { get; }

        internal ecs_system_desc_t RoutineDesc;
        internal BindingContext.RoutineContext RoutineContext;
        internal ulong CurrentPhase;

        public RoutineBuilder(ecs_world_t* world)
        {
            World = world;
            RoutineDesc = default;
            RoutineContext = default;
            CurrentPhase = default;
        }

        public ref RoutineBuilder Kind(ulong phase)
        {
            CurrentPhase = phase;
            return ref this;
        }

        public ref RoutineBuilder Kind<T>()
        {
            return ref Kind(Type<T>.Id(World));
        }

        public ref RoutineBuilder MultiThreaded(bool value = true)
        {
            RoutineDesc.multi_threaded = Macros.Bool(value);
            return ref this;
        }

        public ref RoutineBuilder NoReadonly(bool value = true)
        {
            RoutineDesc.no_readonly = Macros.Bool(value);
            return ref this;
        }

        public ref RoutineBuilder Interval(float interval)
        {
            RoutineDesc.interval = interval;
            return ref this;
        }

        public ref RoutineBuilder Rate(ulong tickSource, int rate)
        {
            RoutineDesc.rate = rate;
            RoutineDesc.tick_source = tickSource;
            return ref this;
        }

        public ref RoutineBuilder Rate(int rate)
        {
            RoutineDesc.rate = rate;
            return ref this;
        }

        public ref RoutineBuilder TickSource(ulong tickSource)
        {
            RoutineDesc.tick_source = tickSource;
            return ref this;
        }

        public ref RoutineBuilder Ctx(void* ctx)
        {
            RoutineDesc.ctx = ctx;
            return ref this;
        }

        public ref RoutineBuilder Run(Ecs.IterAction action)
        {
            BindingContext.SetCallback(ref RoutineContext.Run, action);
            RoutineDesc.run = RoutineContext.Run.Function;
            return ref this;
        }
    }
}