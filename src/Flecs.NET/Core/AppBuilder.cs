using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
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

        public void Dispose()
        {
            Managed.FreeGcHandle(_initHandle);
            _initHandle = default;
        }

        public ref AppBuilder TargetFps(float value)
        {
            Desc.target_fps = value;
            return ref this;
        }

        public ref AppBuilder DeltaTime(float value)
        {
            Desc.delta_time = value;
            return ref this;
        }

        public ref AppBuilder Threads(int value)
        {
            Desc.threads = value;
            return ref this;
        }

        public ref AppBuilder Frames(int value)
        {
            Desc.frames = value;
            return ref this;
        }

        public ref AppBuilder EnableRest(ushort port = 0)
        {
            Desc.enable_rest = Macros.True;
            Desc.port = port;
            return ref this;
        }

        public ref AppBuilder EnableMonitor(bool value = true)
        {
            Desc.enable_monitor = Macros.Bool(value);
            return ref this;
        }

        public ref AppBuilder Init(Ecs.AppInitAction value)
        {
            Managed.FreeGcHandle(_initHandle);
            _initHandle = GCHandle.Alloc(value);

            Desc.init = Marshal.GetFunctionPointerForDelegate(value);
            return ref this;
        }

        public ref AppBuilder Ctx(void* value)
        {
            Desc.ctx = value;
            return ref this;
        }

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
