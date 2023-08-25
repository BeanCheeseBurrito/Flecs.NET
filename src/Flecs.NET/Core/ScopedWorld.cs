using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    /// Scoped world.
    /// </summary>
    public unsafe struct ScopedWorld : IDisposable
    {
        private ecs_world_t* _world;
        private ulong _prevScope;

        /// <summary>
        /// A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        /// A reference to the previous scope entity.
        /// </summary>
        public ref ulong PrevScope => ref _prevScope;

        /// <summary>
        /// Creates a scoped world in the scope of an entity.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="scope"></param>
        public ScopedWorld(ecs_world_t* world, ulong scope)
        {
            _prevScope = ecs_set_scope(world, scope);
            _world = world;
        }

        /// <summary>
        /// Disposes the scoped world.
        /// </summary>
        public void Dispose()
        {
            ecs_set_scope(_world, _prevScope);
        }
    }
}
