using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Id : IEquatable<Id>
    {
        private ecs_world_t* _world;
        private ulong _value;

        public ref ecs_world_t* World => ref _world;
        public ref ulong Value => ref _value;

        public Id(ulong id)
        {
            _world = null;
            _value = id;
        }

        public Id(ulong first, ulong second)
        {
            _world = null;
            _value = Macros.Pair(first, second);
        }

        public Id(ecs_world_t* world, ulong first, ulong second)
        {
            _world = world;
            _value = Macros.Pair(first, second);
        }

        public Id(ecs_world_t* world, ulong id = 0)
        {
            _world = world;
            _value = id;
        }

        public Id(Id first, Id second)
        {
            _world = first.World;
            _value = Macros.Pair(first.Value, second.Value);
        }

        public Id(Entity first, Entity second)
        {
            _world = first.World;
            _value = Macros.Pair(first, second);
        }

        public bool IsPair()
        {
            return (Value & ECS_ID_FLAGS_MASK) == ECS_PAIR;
        }

        public bool IsWildCard()
        {
            return ecs_id_is_wildcard(Value) == 1;
        }

        public bool IsEntity()
        {
            return (Value & ECS_ID_FLAGS_MASK) == 0;
        }

        public Entity Entity()
        {
            Assert.True(!IsPair());
            Assert.True(Flags() == 0);
            return new Entity(World, Value);
        }

        public Entity AddFlags(ulong flags)
        {
            return new Entity(World, Value | flags);
        }

        public Entity RemoveFlags(ulong flags)
        {
            Assert.True((Value & ECS_ID_FLAGS_MASK) == flags);
            return new Entity(World, Value & ECS_COMPONENT_MASK);
        }

        public Entity RemoveFlags()
        {
            return new Entity(World, Value & ECS_COMPONENT_MASK);
        }

        public Entity RemoveGeneration()
        {
            return new Entity(World, (uint)Value);
        }

        public Entity TypeId()
        {
            return new Entity(World, ecs_get_typeid(World, Value));
        }

        public bool HasFlags(ulong flags)
        {
            return (Value & flags) == flags;
        }

        public bool HasFlags()
        {
            return (Value & ECS_ID_FLAGS_MASK) != 0;
        }

        public Entity Flags()
        {
            return new Entity(World, Value & ECS_ID_FLAGS_MASK);
        }

        public bool HasRelation(ulong first)
        {
            return IsPair() && Macros.PairFirst(Value) == first;
        }

        public Entity First()
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

        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_id_str(World, Value));
        }

        public string FlagsStr()
        {
            return NativeString.GetStringAndFree(ecs_id_flag_str(Value & ECS_ID_FLAGS_MASK));
        }

        public World CsWorld()
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

        public bool Equals(Id other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Id id && Equals(id);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
