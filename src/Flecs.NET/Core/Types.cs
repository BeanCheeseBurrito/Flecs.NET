using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Types : IEquatable<Types>
    {
        public ecs_world_t* World { get; }
        public ecs_type_t* Type { get; }

        public Types(ecs_world_t* world, ecs_type_t* type)
        {
            World = world;
            Type = type;
        }

        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_type_str(World, Type));
        }

        public int Count()
        {
            return Type == null ? 0 : Type->count;
        }

        public Id Get(int index)
        {
            Assert.True(Type != null, nameof(ECS_INVALID_PARAMETER));
            Assert.True(Type->count > index, nameof(ECS_OUT_OF_RANGE));
            return Type == null ? new Id(null) : new Id(World, Type->array[index]);
        }

        public static implicit operator ecs_type_t*(Types types)
        {
            return To(types);
        }

        public static ecs_type_t* To(Types types)
        {
            return types.Type;
        }

        public bool Equals(Types other)
        {
            return Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            return obj is Types types && Equals(types);
        }

        public override int GetHashCode()
        {
            return Type->GetHashCode();
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