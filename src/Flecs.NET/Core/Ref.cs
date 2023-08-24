using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Ref<T>
    {
        private ecs_world_t* _world;
        private ecs_ref_t _ref;

        public ref ecs_world_t* World => ref _world;

        public Ref(ecs_world_t* world, ulong entity, ulong id = 0)
        {
            _world = world == null ? null : ecs_get_world(world);

            if (id == 0)
                id = Type<T>.Id(world);

            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));

            _ref = ecs_ref_init_id(world, entity, id);
        }

        public T* GetPtr()
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                throw new InvalidOperationException("Can't use GetPtr on managed types");

            fixed (ecs_ref_t* refPtr = &_ref)
            {
                return (T*)ecs_ref_get_id(World, refPtr, _ref.id);
            }
        }

        public ref T Get()
        {
            fixed (ecs_ref_t* refPtr = &_ref)
            {
                void* data = ecs_ref_get_id(World, refPtr, _ref.id);
                return ref Managed.GetTypeRef<T>(data);
            }
        }

        public Entity Entity()
        {
            return new Entity(World, _ref.entity);
        }
    }
}
