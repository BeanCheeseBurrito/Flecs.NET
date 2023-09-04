using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_system_desc_t.
    /// </summary>
    public unsafe struct RoutineBuilder : IDisposable
    {
        private ecs_world_t* _world;

        internal ecs_system_desc_t RoutineDesc;
        internal BindingContext.RoutineContext RoutineContext;
        internal ulong CurrentPhase;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the routine description.
        /// </summary>
        public ref ecs_system_desc_t Desc => ref RoutineDesc;

        /// <summary>
        ///     Creates a routine builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public RoutineBuilder(ecs_world_t* world)
        {
            RoutineDesc = default;
            RoutineContext = default;
            CurrentPhase = default;
            _world = world;
        }

        /// <summary>
        ///     Disposes the routine builder.
        /// </summary>
        public void Dispose()
        {
            RoutineContext.Dispose();
        }

        /// <summary>
        ///     Specify in which phase the system should run.
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public ref RoutineBuilder Kind(ulong phase)
        {
            CurrentPhase = phase;
            return ref this;
        }

        /// <summary>
        ///     Specify in which phase the system should run.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref RoutineBuilder Kind<T>()
        {
            return ref Kind(Type<T>.Id(World));
        }

        /// <summary>
        ///     Specify whether system can run on multiple threads.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref RoutineBuilder MultiThreaded(bool value = true)
        {
            RoutineDesc.multi_threaded = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Specify whether system should be ran in staged context.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref RoutineBuilder NoReadonly(bool value = true)
        {
            RoutineDesc.no_readonly = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Set system interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public ref RoutineBuilder Interval(float interval)
        {
            RoutineDesc.interval = interval;
            return ref this;
        }

        /// <summary>
        ///     Set system rate.
        /// </summary>
        /// <param name="tickSource"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ref RoutineBuilder Rate(ulong tickSource, int rate)
        {
            RoutineDesc.rate = rate;
            RoutineDesc.tick_source = tickSource;
            return ref this;
        }

        /// <summary>
        ///     Set system rate.
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ref RoutineBuilder Rate(int rate)
        {
            RoutineDesc.rate = rate;
            return ref this;
        }

        /// <summary>
        ///     Set tick source.
        /// </summary>
        /// <param name="tickSource"></param>
        /// <returns></returns>
        public ref RoutineBuilder TickSource(ulong tickSource)
        {
            RoutineDesc.tick_source = tickSource;
            return ref this;
        }

        /// <summary>
        ///     Set system context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ref RoutineBuilder Ctx(void* ctx)
        {
            RoutineDesc.ctx = ctx;
            return ref this;
        }

        /// <summary>
        ///     Set system run callback.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ref RoutineBuilder Run(Ecs.IterAction action)
        {
            BindingContext.SetCallback(ref RoutineContext.Run, action);
            RoutineDesc.run = RoutineContext.Run.Function;
            return ref this;
        }
    }
}
