using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A snapshot stores the state of a world in a particular point in time.
    /// </summary>
    public unsafe struct Snapshot : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_snapshot_t* _handle;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_snapshot_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a snapshot for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public Snapshot(ecs_world_t* world)
        {
            _world = world;
            _handle = null;
        }

        /// <summary>
        ///     Create a snapshot.
        ///     This operation makes a copy of the current state of the world.
        /// </summary>
        public void Take()
        {
            if (Handle != null)
                ecs_snapshot_free(Handle);

            Handle = ecs_snapshot_take(World);
        }

        /// <summary>
        ///     Create a filtered snapshot.
        ///     This operation is the same as ecs_snapshot_take, but accepts an iterator so
        ///     an application can control what is stored by the snapshot.
        /// </summary>
        /// <param name="filter"></param>
        public void Take(ref Filter filter)
        {
            if (Handle != null)
                ecs_snapshot_free(Handle);

            ecs_iter_t it = ecs_filter_iter(World, filter.Handle);
            Handle = ecs_snapshot_take_w_iter(&it);
        }

        /// <summary>
        ///     Restore a snapshot.
        ///     This operation restores the world to the state it was in when the specified
        ///     snapshot was taken. A snapshot can only be used once for restoring, as its
        ///     data replaces the data that is currently in the world.
        ///     This operation also resets the last issued entity handle, so any calls to
        ///     ecs_new may return entity ids that have been issued before restoring the
        ///     snapshot.
        /// </summary>
        public void Restore()
        {
            if (Handle == null)
                return;

            ecs_snapshot_restore(World, Handle);
            Handle = null;
        }

        /// <summary>
        ///     Disposes of the snapshot.
        /// </summary>
        public void Dispose()
        {
            if (Handle == null)
                return;

            ecs_snapshot_free(Handle);
            Handle = null;
        }
    }
}
