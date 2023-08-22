using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct AppBuilder : IDisposable
    {
        public ecs_world_t* World { get; }
        public ref ecs_app_desc_t AppDesc => ref _appDesc;

        private ecs_app_desc_t _appDesc;
        private GCHandle _initHandle;

        public AppBuilder(ecs_world_t* world)
        {
            World = world;
            _appDesc = default;
            _initHandle = default;

            ecs_world_info_t* stats = ecs_get_world_info(world);
            AppDesc.target_fps = stats->target_fps;

            if (Math.Abs(AppDesc.target_fps - 0) < 0.01)
                AppDesc.target_fps = 60;
        }

        public void Dispose()
        {
            Managed.FreeGcHandle(_initHandle);
            _initHandle = default;
        }

        public ref AppBuilder TargetFps(float value)
        {
            AppDesc.target_fps = value;
            return ref this;
        }

        public ref AppBuilder DeltaTime(float value)
        {
            AppDesc.delta_time = value;
            return ref this;
        }

        public ref AppBuilder Threads(int value)
        {
            AppDesc.threads = value;
            return ref this;
        }

        public ref AppBuilder Frames(int value)
        {
            AppDesc.frames = value;
            return ref this;
        }

        public ref AppBuilder EnableRest(ushort port = 0)
        {
            AppDesc.enable_rest = Macros.True;
            AppDesc.port = port;
            return ref this;
        }

        public ref AppBuilder EnableMonitor(bool value = true)
        {
            AppDesc.enable_monitor = Macros.Bool(value);
            return ref this;
        }

        public ref AppBuilder Init(Ecs.AppInitAction value)
        {
            Managed.FreeGcHandle(_initHandle);
            _initHandle = GCHandle.Alloc(value);

            AppDesc.init = Marshal.GetFunctionPointerForDelegate(value);
            return ref this;
        }

        public ref AppBuilder Ctx(void* value)
        {
            AppDesc.ctx = value;
            return ref this;
        }

        public int Run()
        {
            fixed (ecs_app_desc_t* appDesc = &AppDesc)
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