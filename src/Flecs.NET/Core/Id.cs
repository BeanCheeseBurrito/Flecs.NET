using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Id : IEquatable<Id>
    {
        public ecs_world_t* World { get; }
        public ulong Value { get; }

        public Id(ulong id)
        {
            World = null;
            Value = id;
        }

        public Id(ulong first, ulong second)
        {
            World = null;
            Value = Macros.Pair(first, second);
        }

        public Id(ecs_world_t* world, ulong first, ulong second)
        {
            World = world;
            Value = Macros.Pair(first, second);
        }

        public Id(ecs_world_t* world, ulong id = 0)
        {
            World = world;
            Value = id;
        }

        public Id(Id first, Id second)
        {
            World = first.World;
            Value = Macros.Pair(first.Value, second.Value);
        }

        public Id(Entity first, Entity second)
        {
            World = first.World;
            Value = Macros.Pair(first, second);
        }

        public readonly bool IsPair()
        {
            return (Value & ECS_ID_FLAGS_MASK) == ECS_PAIR;
        }

        public readonly bool IsWildCard()
        {
            return ecs_id_is_wildcard(Value) == 1;
        }

        public readonly bool IsEntity()
        {
            return (Value & ECS_ID_FLAGS_MASK) == 0;
        }

        public readonly Entity Entity()
        {
            Assert.True(!IsPair());
            Assert.True(Flags() == 0);
            return new Entity(World, Value);
        }

        public readonly Entity AddFlags(ulong flags)
        {
            return new Entity(World, Value | flags);
        }

        public readonly Entity RemoveFlags(ulong flags)
        {
            Assert.True((Value & ECS_ID_FLAGS_MASK) == flags);
            return new Entity(World, Value & ECS_COMPONENT_MASK);
        }

        public readonly Entity RemoveFlags()
        {
            return new Entity(World, Value & ECS_COMPONENT_MASK);
        }

        public readonly Entity RemoveGeneration()
        {
            return new Entity(World, (uint)Value);
        }

        public readonly Entity TypeId()
        {
            return new Entity(World, ecs_get_typeid(World, Value));
        }

        public readonly bool HasFlags(ulong flags)
        {
            return (Value & flags) == flags;
        }

        public readonly bool HasFlags()
        {
            return (Value & ECS_ID_FLAGS_MASK) != 0;
        }

        public readonly Entity Flags()
        {
            return new Entity(World, Value & ECS_ID_FLAGS_MASK);
        }

        public readonly bool HasRelation(ulong first)
        {
            return IsPair() && Macros.PairFirst(Value) == first;
        }

        public readonly Entity First()
        {
            Assert.True(IsPair());
            ulong entity = Macros.PairFirst(Value);
            return World == null ? new Entity(entity) : new Entity(World, ecs_get_alive(World, entity));
        }

        public Entity Second()
        {
            ulong entity = Macros.PairSecond(Value);
            return World == null ? new Entity(entity) : new Entity(World, ecs_get_alive(World, entity));
        }

        public readonly string Str()
        {
            return NativeString.GetStringAndFree(ecs_id_str(World, Value));
        }

        public readonly string FlagsStr()
        {
            return NativeString.GetStringAndFree(ecs_id_flag_str(Value & ECS_ID_FLAGS_MASK));
        }

        public readonly World CsWorld()
        {
            return new World(World);
        }

        public static implicit operator ulong(Id id)
        {
            return id.Value;
        }

        public static implicit operator Id(ulong id)
        {
            return new Id(id);
        }

        public static bool operator ==(Id left, Id right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id left, Id right)
        {
            return !(left == right);
        }

        public static Id FromUInt64(ulong id)
        {
            return new Id(id);
        }

        public static ulong ToUInt64(Id id)
        {
            return id.Value;
        }

        public readonly bool Equals(Id other)
        {
            return Value == other.Value;
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is Id id && Equals(id);
        }

        public readonly override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
