using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    /// A wrapper around ecs_app_desc_t.
    /// </summary>
    public unsafe struct AppBuilder : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_app_desc_t _desc;
        private GCHandle _initHandle;

        /// <summary>
        /// Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        /// Reference to app description.
        /// </summary>
        public ref ecs_app_desc_t Desc => ref _desc;

        /// <summary>
        /// Creates an app builder for world.
        /// </summary>
        /// <param name="world"></param>
        public AppBuilder(ecs_world_t* world)
        {
            _world = world;
            _desc = default;
            _initHandle = default;

            ecs_world_info_t* stats = ecs_get_world_info(world);
            Desc.target_fps = stats->target_fps;

            if (Math.Abs(Desc.target_fps - 0) < 0.01)
                Desc.target_fps = 60;
        }

        /// <summary>
        /// Cleans up resources.
        /// </summary>
        public void Dispose()
        {
            Managed.FreeGcHandle(_initHandle);
            _initHandle = default;
        }

        /// <summary>
        /// Sets the target fps.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder TargetFps(float value)
        {
            Desc.target_fps = value;
            return ref this;
        }

        /// <summary>
        /// Sets the delta time.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder DeltaTime(float value)
        {
            Desc.delta_time = value;
            return ref this;
        }

        /// <summary>
        /// Sets the number of threads to use.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder Threads(int value)
        {
            Desc.threads = value;
            return ref this;
        }

        /// <summary>
        /// Sets the number of frames to run.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder Frames(int value)
        {
            Desc.frames = value;
            return ref this;
        }

        /// <summary>
        /// Enable ecs access over http for the explorer.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public ref AppBuilder EnableRest(ushort port = 0)
        {
            Desc.enable_rest = Macros.True;
            Desc.port = port;
            return ref this;
        }

        /// <summary>
        /// Periodically collect statistics.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder EnableMonitor(bool value = true)
        {
            Desc.enable_monitor = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        /// Sets a callback to be run before starting the main loop.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder Init(Ecs.AppInitAction value)
        {
            Managed.FreeGcHandle(_initHandle);
            _initHandle = GCHandle.Alloc(value);

            Desc.init = Marshal.GetFunctionPointerForDelegate(value);
            return ref this;
        }

        /// <summary>
        /// Context for storing custom data.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref AppBuilder Ctx(void* value)
        {
            Desc.ctx = value;
            return ref this;
        }

        /// <summary>
        /// Runs the app.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            fixed (ecs_app_desc_t* appDesc = &Desc)
            {
                int result = ecs_app_run(World, appDesc);

                if (ecs_should_quit(World) == 1)
                    _ = ecs_fini(World);

                Dispose();
                return result;
            }
        }
    }
}
