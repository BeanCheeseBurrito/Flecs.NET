using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

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
        ///     Registers this type with the provided world or returns an existing id if found.
        /// </summary>
        /// <param name="world"></param>
        public Component(ecs_world_t* world)
        {
            _untypedComponent = new UntypedComponent(world, Type<TComponent>.Id(world, false, true, 0));
        }

        /// <summary>
        ///     Registers this type with the provided world or returns an existing id if found.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public Component(ecs_world_t* world, ulong id)
        {
            _untypedComponent = new UntypedComponent(world, Type<TComponent>.Id(world, false, true, id));
        }

        /// <summary>
        ///     Registers this type with the provided world or returns an existing id if found.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public Component(ecs_world_t* world, string name)
        {
            _untypedComponent = new UntypedComponent(world, Type<TComponent>.Id(world, false, true, 0, name));
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
        ///     Add constant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref Component<TComponent> Constant<TEnum>(string name, TEnum value) where TEnum : Enum
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
        ///     Converts a <see cref="Component{TComponent}"/> instance to its integer id.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static implicit operator ulong(Component<TComponent> component)
        {
            return ToUInt64(component);
        }

        /// <summary>
        ///     Converts a <see cref="Component{TComponent}"/> instance to its id.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static implicit operator Id(Component<TComponent> component)
        {
            return ToId(component);
        }

        /// <summary>
        ///     Converts a <see cref="Component{TComponent}"/> instance to its entity.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static implicit operator Entity(Component<TComponent> component)
        {
            return ToEntity(component);
        }

        /// <summary>
        ///     Converts a <see cref="Component{TComponent}"/> instance to its integer id.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static ulong ToUInt64(Component<TComponent> component)
        {
            return component.Entity.Id.Value;
        }

        /// <summary>
        ///     Converts a <see cref="Component{TComponent}"/> instance to its id.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Id ToId(Component<TComponent> component)
        {
            return component.Id;
        }

        /// <summary>
        ///     Converts a <see cref="Component{TComponent}"/> instance to its entity.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Entity ToEntity(Component<TComponent> component)
        {
            return component.Entity;
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

    // Flecs.NET Extensions
    public unsafe partial struct Component<TComponent>
    {
        /// <summary>
        ///     Sets the component's type hooks.
        /// </summary>
        /// <param name="hooks"></param>
        public void SetHooks(TypeHooks<TComponent> hooks)
        {
            ecs_type_hooks_t desc = default;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>())
            {
                desc.ctor = hooks.Ctor == null ? BindingContext<TComponent>.DefaultManagedCtorPointer : BindingContext<TComponent>.ManagedCtorPointer;
                desc.dtor = hooks.Dtor == null ? BindingContext<TComponent>.DefaultManagedDtorPointer : BindingContext<TComponent>.ManagedDtorPointer;
                desc.move = hooks.Move == null ? BindingContext<TComponent>.DefaultManagedMovePointer : BindingContext<TComponent>.ManagedMovePointer;
                desc.copy = hooks.Copy == null ? BindingContext<TComponent>.DefaultManagedCopyPointer : BindingContext<TComponent>.ManagedCopyPointer;
            }
            else
            {
                desc.ctor = hooks.Ctor == null ? IntPtr.Zero : BindingContext<TComponent>.UnmanagedCtorPointer;
                desc.dtor = hooks.Dtor == null ? IntPtr.Zero : BindingContext<TComponent>.UnmanagedDtorPointer;
                desc.move = hooks.Move == null ? IntPtr.Zero : BindingContext<TComponent>.UnmanagedMovePointer;
                desc.copy = hooks.Copy == null ? IntPtr.Zero : BindingContext<TComponent>.UnmanagedCopyPointer;
            }

            BindingContext.TypeHooksContext* bindingContext = Memory.AllocZeroed<BindingContext.TypeHooksContext>(1);
            BindingContext.SetCallback(ref bindingContext->Ctor, hooks.Ctor, false);
            BindingContext.SetCallback(ref bindingContext->Dtor, hooks.Dtor, false);
            BindingContext.SetCallback(ref bindingContext->Move, hooks.Move, false);
            BindingContext.SetCallback(ref bindingContext->Copy, hooks.Copy, false);
            BindingContext.SetCallback(ref bindingContext->OnAdd, hooks.OnAdd, false);
            BindingContext.SetCallback(ref bindingContext->OnSet, hooks.OnSet, false);
            BindingContext.SetCallback(ref bindingContext->OnRemove, hooks.OnRemove, false);
            BindingContext.SetCallback(ref bindingContext->ContextFree, hooks.ContextFree);

            desc.on_add = hooks.OnAdd == null ? IntPtr.Zero : BindingContext<TComponent>.OnAddHookPointer;
            desc.on_set = hooks.OnSet == null ? IntPtr.Zero : BindingContext<TComponent>.OnSetHookPointer;
            desc.on_remove = hooks.OnRemove == null ? IntPtr.Zero : BindingContext<TComponent>.OnRemoveHookPointer;
            desc.ctx = hooks.Context;
            desc.ctx_free = bindingContext->ContextFree.Function;
            desc.binding_ctx = bindingContext;
            desc.binding_ctx_free = BindingContext.TypeHooksContextFreePointer;

            if (desc != default)
                ecs_set_hooks_id(World, Id, &desc);
        }
    }
}
