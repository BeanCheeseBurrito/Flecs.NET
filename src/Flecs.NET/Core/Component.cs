using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Struct used to register components and component metadata.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public unsafe partial struct Component<TComponent> : IEquatable<Component<TComponent>>, IEquatable<ulong>
    {
        private UntypedComponent _untypedComponent;

        /// <summary>
        ///     Reference to untyped component.
        /// </summary>
        public ref UntypedComponent UntypedComponent => ref _untypedComponent;

        /// <summary>
        ///     Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _untypedComponent.World;

        /// <summary>
        ///     Reference to entity.
        /// </summary>
        public ref Entity Entity => ref _untypedComponent.Entity;

        /// <summary>
        ///     Reference to id.
        /// </summary>
        public ref Id Id => ref Entity.Id;

        /// <summary>
        ///     Registers a component.
        ///     If the component was already registered, this operation will return a handle to the existing component.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="allowTag"></param>
        /// <param name="id"></param>
        public Component(ecs_world_t* world, string? name = null, bool allowTag = true, ulong id = 0)
        {
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

                FlecsInternal.ComponentValidate(
                    world, id, nativeName,
                    nativeSymbolName,
                    Type<TComponent>.GetSize(),
                    Type<TComponent>.GetAlignment(),
                    Macros.Bool(implicitName)
                );
            }
            else
            {
                if (implicitName && ecs_get_scope(world) != 0)
                {
                    int start = name.IndexOf('<', StringComparison.Ordinal);
                    int lastElem = 0;

                    if (start != -1)
                    {
                        int index = start;

                        while (index != 0 && name[index] != '.' && name[index] != ':')
                            index--;

                        if (name[index] == '.' || name[index] == ':')
                            lastElem = index;
                    }
                    else
                    {
                        lastElem = name.LastIndexOf('.');

                        if (lastElem == -1)
                            lastElem = name.LastIndexOf(':');
                    }

                    name = name[(lastElem + 1)..];
                }

                using NativeString nativeName = (NativeString)name;

                Type type = typeof(TComponent);
                StructLayoutAttribute attribute = type.StructLayoutAttribute!;

                int size;
                int alignment;

                if (RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>())
                {
                    size = sizeof(GCHandle);
                    alignment = Type<GCHandle>.AlignOf();
                }
                else if (attribute.Value == LayoutKind.Explicit)
                {
                    size = attribute.Size == 0 ? sizeof(TComponent) : attribute.Size;
                    alignment = attribute.Pack == 0 ? Type<TComponent>.AlignOf() : attribute.Pack;
                }
                else
                {
                    size = sizeof(TComponent);
                    alignment = Type<TComponent>.AlignOf();
                }

                byte existing;

                id = FlecsInternal.ComponentRegister(
                    world, id, nativeName, nativeSymbolName,
                    size, alignment,
                    Macros.Bool(implicitName), &existing
                );

                id = Type<TComponent>.IdExplicit(world, name, allowTag, id);

                if (Type<TComponent>.GetSize() != 0 && existing == Macros.False)
                    Type<TComponent>.RegisterLifeCycleActions(world);
            }

            _untypedComponent = new UntypedComponent(world, id);
        }

        // TODO: Port opaque stuff here later

        /// <summary>
        ///     Add member with unit.
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ref Component<TComponent> Member(ulong typeId, ulong unit, string name, int count, int offset = 0)
        {
            UntypedComponent.Member(typeId, unit, name, count, offset);
            return ref this;
        }

        /// <summary>
        ///     Add member.
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ref Component<TComponent> Member(ulong typeId, string name, int count = 0, int offset = 0)
        {
            UntypedComponent.Member(typeId, name, count, offset);
            return ref this;
        }

        /// <summary>
        ///     Add member.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Component<TComponent> Member<T>(string name, int count = 0, int offset = 0)
        {
            UntypedComponent.Member<T>(name, count, offset);
            return ref this;
        }

        /// <summary>
        ///     Add member with unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Component<TComponent> Member<T>(ulong unit, string name, int count = 0, int offset = 0)
        {
            UntypedComponent.Member<T>(unit, name, count, offset);
            return ref this;
        }

        /// <summary>
        ///     Add member with unit.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TUnit"></typeparam>
        /// <returns></returns>
        public ref Component<TComponent> Member<T, TUnit>(string name, int count = 0, int offset = 0)
        {
            UntypedComponent.Member<T, TUnit>(name, count, offset);
            return ref this;
        }

        /// <summary>
        ///     Add constant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref Component<TComponent> Constant(string name, int value)
        {
            UntypedComponent.Constant(name, value);
            return ref this;
        }

        /// <summary>
        ///     Add bitmask constant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref Component<TComponent> Bit(string name, uint value)
        {
            UntypedComponent.Bit(name, value);
            return ref this;
        }

        /// <summary>
        ///     Add member value range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public ref Component<TComponent> Range(double min, double max)
        {
            UntypedComponent.Range(min, max);
            return ref this;
        }

        /// <summary>
        ///     Add member warning range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public ref Component<TComponent> WarningRange(double min, double max)
        {
            UntypedComponent.WarningRange(min, max);
            return ref this;
        }

        /// <summary>
        ///     Add member error range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public ref Component<TComponent> ErrorRange(double min, double max)
        {
            UntypedComponent.ErrorRange(min, max);
            return ref this;
        }

        /// <summary>
        ///     Register member as metric.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="brief"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Component<TComponent> Metric<T>(ulong parent = 0, string brief = "", string name = "")
        {
            UntypedComponent.Metric<T>(parent, brief, name);
            return ref this;
        }

        /// <summary>
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static implicit operator ulong(Component<TComponent> component)
        {
            return ToUInt64(component);
        }

        /// <summary>
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static ulong ToUInt64(Component<TComponent> component)
        {
            return component.Entity.Id.Value;
        }

        /// <summary>
        ///     Checks if two <see cref="Component{TComponent}"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Component<TComponent> other)
        {
            return Entity.Id.Value == other.Entity.Id.Value;
        }

        /// <summary>
        ///     Checks if the <see cref="Component{TComponent}"/> instances is equal to an entity id.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ulong other)
        {
            return Entity.Id.Value == other;
        }

        /// <summary>
        ///     Checks if the <see cref="Component{TComponent}"/> instance is equal to a <see cref="Component{TComponent}"/> or
        ///     entity id.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return (obj is Component<TComponent> component && Equals(component)) || (obj is ulong id && Equals(id));
        }

        /// <summary>
        ///     Checks if two <see cref="Component{TComponent}"/> instances are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Component<TComponent> a, Component<TComponent> b)
        {
            return a.Equals(b);
        }

        /// <summary>
        ///     Checks if two <see cref="Component{TComponent}"/> instances are not equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Component<TComponent> a, Component<TComponent> b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Checks if the <see cref="Component{TComponent}"/> is equal to an entity id.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Component<TComponent> a, ulong b)
        {
            return a.Equals(b);
        }

        /// <summary>
        ///     Checks if the <see cref="Component{TComponent}"/> instance does not equal an entity id.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Component<TComponent> a, ulong b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Checks if an entity id is equal to a <see cref="Component{TComponent}"/> instance.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ulong a, Component<TComponent> b)
        {
            return b.Equals(a);
        }

        /// <summary>
        ///     Checks if an entity id is not equal to a <see cref="Component{TComponent}"/> instance.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ulong a, Component<TComponent> b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Component{TComponent}"/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Entity.Id.Value.GetHashCode();
        }
    }

    // Flecs.NET
    public unsafe partial struct Component<TComponent>
    {
        /// <summary>
        ///     Sets the component's type hooks.
        /// </summary>
        /// <param name="typeHooks"></param>
        public void SetHooks(TypeHooks<TComponent> typeHooks)
        {
            Type<TComponent>.SetTypeHooks(World, typeHooks);
        }
    }
}
