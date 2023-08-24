using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct ScopedWorld : IDisposable
    {
        private ecs_world_t* _world;
        private ulong _prevScope;

        public ref ecs_world_t* World => ref _world;
        public ref ulong PrevScope => ref _prevScope;

        public ScopedWorld(ecs_world_t* world, ulong scope)
        {
            _prevScope = ecs_set_scope(world, scope);
            _world = world;
        }

        public void Dispose()
        {
            ecs_set_scope(_world, _prevScope);
        }
    }
}
