using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Component<TComponent> : IEquatable<Component<TComponent>>, IEquatable<ulong>
    {
        public Entity Entity { get; }
        public ecs_world_t* World { get; }

        public Id Id => Entity.Id;

        public Component(ecs_world_t* world, ulong id)
        {
            Entity = new Entity(world, id);
            World = world;
        }

        public Component(ecs_world_t* world, string? name = null, bool allowTag = true, ulong id = 0)
        {
            World = world;

            bool implicitName = false;

            if (string.IsNullOrEmpty(name))
            {
                name = Type<TComponent>.GetTypeName();
                implicitName = true;
            }

            using NativeString nativeSymbolName = (NativeString)Type<TComponent>.GetSymbolName();

            if (Type<TComponent>.IsRegistered(world))
            {
                id = Type<TComponent>.IdExplicit(world, name, allowTag, id);
                using NativeString nativeName = (NativeString)name;
                ecs_cpp_component_validate(
                    world, id, nativeName,
                    nativeSymbolName,
                    (ulong)Type<TComponent>.GetSize(),
                    (ulong)Type<TComponent>.GetAlignment(),
                    Macros.Bool(implicitName)
                );
            }
            else
            {
                if (implicitName && (ecs_get_scope(world) != 0))
                {
                    int start = name.IndexOf('<', StringComparison.Ordinal);
                    int lastElem = 0;

                    if (start != -1)
                    {
                        int index = start;

                        while (index != 0 && name[index] != ':')
                            index--;

                        if (name[index] == ':')
                            lastElem = index;
                    }
                    else
                    {
                        lastElem = name.IndexOf(':', StringComparison.Ordinal);
                    }

                    if (lastElem != 0) name = name[(lastElem + 1)..];
                }

                using NativeString nativeName = (NativeString)name;

                Type type = typeof(TComponent);
                StructLayoutAttribute attribute = type.StructLayoutAttribute!;

                int size = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                    ? sizeof(IntPtr)
                    : sizeof(TComponent);

                int alignment = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                    ? sizeof(IntPtr)
                    : attribute.Value == LayoutKind.Explicit
                        ? attribute.Pack
                        : Type<TComponent>.AlignOf();

                bool existing;

                id = ecs_cpp_component_register(
                    world, id, nativeName, nativeSymbolName,
                    size, alignment,
                    Macros.Bool(implicitName), (byte*)&existing
                );

                id = Type<TComponent>.IdExplicit(world, name, allowTag, id);

                if (Type<TComponent>.GetSize() != 0 && !existing)
                    Type<TComponent>.RegisterLifeCycleActions(world);
            }

            Entity = new Entity(world, id);
        }

        public ref Component<TComponent> Member(ulong typeId, string name, int count, int offset = 0)
        {
            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.add[0] = Macros.Pair(EcsChildOf, Entity);
            ulong eid = ecs_entity_init(World, &desc);
            Assert.True(eid != 0, nameof(ECS_INTERNAL_ERROR));

            Entity e = new Entity(World, eid);

            EcsMember m = default;
            m.type = typeId;
            m.count = count;
            m.offset = offset;
            e.Set(m);

            return ref this;
        }

        public ref Component<TComponent> Member<T>(string name, int count = 0, int offset = 0)
        {
            return ref Member(Type<T>.Id(World), name, count, offset);
        }

        public ref Component<TComponent> Range(double min, double max)
        {
            ecs_member_t* m = ecs_cpp_last_member(World, Entity);

            if (m == null)
                return ref this;

            World w = new World(World);
            Entity me = w.Entity(m->member);
            ref EcsMemberRanges mr = ref me.GetMut<EcsMemberRanges>();
            mr.value.min = min;
            mr.value.max = max;
            me.Modified<EcsMemberRanges>();
            return ref this;
        }

        public ref Component<TComponent> WarningRange(double min, double max)
        {
            ecs_member_t* m = ecs_cpp_last_member(World, Entity);

            if (m == null)
                return ref this;

            World w = new World(World);
            Entity me = w.Entity(m->member);
            ref EcsMemberRanges mr = ref me.GetMut<EcsMemberRanges>();
            mr.warning.min = min;
            mr.warning.max = max;
            me.Modified<EcsMemberRanges>();
            return ref this;
        }

        public ref Component<TComponent> ErrorRange(double min, double max)
        {
            ecs_member_t* m = ecs_cpp_last_member(World, Entity);

            if (m == null)
                return ref this;

            World w = new World(World);
            Entity me = w.Entity(m->member);
            ref EcsMemberRanges mr = ref me.GetMut<EcsMemberRanges>();
            mr.error.min = min;
            mr.error.max = max;
            me.Modified<EcsMemberRanges>();
            return ref this;
        }

        public static implicit operator ulong(Component<TComponent> component)
        {
            return ToUInt64(component);
        }

        public static ulong ToUInt64(Component<TComponent> component)
        {
            return component.Entity.Id.Value;
        }

        public bool Equals(Component<TComponent> other)
        {
            return Entity.Id.Value == other.Entity.Id.Value;
        }

        public bool Equals(ulong other)
        {
            return Entity.Id.Value == other;
        }

        public override bool Equals(object? obj)
        {
            return (obj is Component<TComponent> component && Equals(component)) || obj is ulong id && Equals(id);
        }

        public static bool operator ==(Component<TComponent> a, Component<TComponent> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Component<TComponent> a, Component<TComponent> b)
        {
            return !(a == b);
        }

        public static bool operator ==(Component<TComponent> a, ulong b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Component<TComponent> a, ulong b)
        {
            return !(a == b);
        }

        public static bool operator ==(ulong a, Component<TComponent> b)
        {
            return b.Equals(a);
        }

        public static bool operator !=(ulong a, Component<TComponent> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Entity.Id.Value.GetHashCode();
        }
    }
}