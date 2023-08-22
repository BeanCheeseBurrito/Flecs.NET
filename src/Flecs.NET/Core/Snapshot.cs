using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Snapshot : IDisposable
    {
        public ecs_world_t* World { get; }
        public ecs_snapshot_t* Handle { get; private set; }

        public Snapshot(ecs_world_t* world)
        {
            World = world;
            Handle = null;
        }

        public void Take()
        {
            if (Handle != null)
                ecs_snapshot_free(Handle);

            Handle = ecs_snapshot_take(World);
        }

        public void Take(ref Filter filter)
        {
            if (Handle != null)
                ecs_snapshot_free(Handle);

            ecs_iter_t it = ecs_filter_iter(World, filter.FilterPtr);
            Handle = ecs_snapshot_take_w_iter(&it);
        }

        public void Restore()
        {
            if (Handle == null)
                return;

            ecs_snapshot_restore(World, Handle);
            Handle = null;
        }

        public void Dispose()
        {
            if (Handle == null)
                return;

            ecs_snapshot_free(Handle);
            Handle = null;
        }
    }
}
