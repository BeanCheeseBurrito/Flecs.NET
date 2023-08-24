using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Types : IEquatable<Types>
    {
        private ecs_world_t* _world;
        private ecs_type_t* _handle;

        public ref ecs_world_t* World => ref _world;
        public ref ecs_type_t* Handle => ref _handle;

        public Types(ecs_world_t* world, ecs_type_t* type)
        {
            _world = world;
            _handle = type;
        }

        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_type_str(World, Handle));
        }

        public int Count()
        {
            return Handle == null ? 0 : Handle->count;
        }

        public Id Get(int index)
        {
            Assert.True(Handle != null, nameof(ECS_INVALID_PARAMETER));
            Assert.True(Handle->count > index, nameof(ECS_OUT_OF_RANGE));
            return Handle == null ? new Id(null) : new Id(World, Handle->array[index]);
        }

        public static implicit operator ecs_type_t*(Types types)
        {
            return To(types);
        }

        public static ecs_type_t* To(Types types)
        {
            return types.Handle;
        }

        public bool Equals(Types other)
        {
            return Handle == other.Handle;
        }

        public override bool Equals(object obj)
        {
            return obj is Types types && Equals(types);
        }

        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        public static bool operator ==(Types left, Types right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Types left, Types right)
        {
            return !(left == right);
        }
    }
}
