using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Core.BindingContext;
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
        /// <inheritdoc cref="Core.UntypedComponent.TypeInfo"/>
        public TypeInfo TypeInfo()
        {
            return UntypedComponent.TypeInfo();
        }

        private void GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context)
        {
            ecs_type_hooks_t* existingHooks = ecs_get_hooks_id(World, Id);
            hooks = existingHooks == null ? default : *existingHooks;

            context = (TypeHooksContext*)hooks.binding_ctx;

            if (context == null)
            {
                context = Memory.Alloc(TypeHooksContext.Default);
                hooks.binding_ctx = context;
                hooks.binding_ctx_free = Pointers.TypeHooksContextFree;
            }

            Ecs.Assert(context->Header == Ecs.Header, "Type hook binding context is not owned by Flecs.NET.");
        }

        /// <summary>
        ///     Registers a Ctor callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Ctor(Ecs.CtorCallback<TComponent> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.ctor == IntPtr.Zero ||
                hooks.ctor == Pointers<TComponent>.DefaultManagedCtorCallback ||
                hooks.ctor == Pointers<TComponent>.ManagedCtorCallbackDelegate ||
                hooks.ctor == Pointers<TComponent>.ManagedCtorCallbackPointer ||
                hooks.ctor == Pointers<TComponent>.UnmanagedCtorCallbackDelegate ||
                hooks.ctor == Pointers<TComponent>.UnmanagedCtorCallbackPointer,
                "Cannot register Ctor hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Ctor, callback, false);

            hooks.ctor = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedCtorCallbackDelegate
                : Pointers<TComponent>.UnmanagedCtorCallbackDelegate;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Ctor callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Ctor(delegate*<ref TComponent, TypeInfo, void> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.ctor == IntPtr.Zero ||
                hooks.ctor == Pointers<TComponent>.DefaultManagedCtorCallback ||
                hooks.ctor == Pointers<TComponent>.ManagedCtorCallbackDelegate ||
                hooks.ctor == Pointers<TComponent>.ManagedCtorCallbackPointer ||
                hooks.ctor == Pointers<TComponent>.UnmanagedCtorCallbackDelegate ||
                hooks.ctor == Pointers<TComponent>.UnmanagedCtorCallbackPointer,
                "Cannot register Ctor hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Ctor, (IntPtr)callback);

            hooks.ctor = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedCtorCallbackPointer
                : Pointers<TComponent>.UnmanagedCtorCallbackPointer;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Dtor callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Dtor(Ecs.DtorCallback<TComponent> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.dtor == IntPtr.Zero ||
                hooks.dtor == Pointers<TComponent>.DefaultManagedDtorCallback ||
                hooks.dtor == Pointers<TComponent>.ManagedDtorCallbackDelegate ||
                hooks.dtor == Pointers<TComponent>.ManagedDtorCallbackPointer ||
                hooks.dtor == Pointers<TComponent>.UnmanagedDtorCallbackDelegate ||
                hooks.dtor == Pointers<TComponent>.UnmanagedDtorCallbackPointer,
                "Cannot register Dtor hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Dtor, callback, false);

            hooks.dtor = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedDtorCallbackDelegate
                : Pointers<TComponent>.UnmanagedDtorCallbackDelegate;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Dtor callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Dtor(delegate*<ref TComponent, TypeInfo, void> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.dtor == IntPtr.Zero ||
                hooks.dtor == Pointers<TComponent>.DefaultManagedDtorCallback ||
                hooks.dtor == Pointers<TComponent>.ManagedDtorCallbackDelegate ||
                hooks.dtor == Pointers<TComponent>.ManagedDtorCallbackPointer ||
                hooks.dtor == Pointers<TComponent>.UnmanagedDtorCallbackDelegate ||
                hooks.dtor == Pointers<TComponent>.UnmanagedDtorCallbackPointer,
                "Cannot register Dtor hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Dtor, (IntPtr)callback);

            hooks.dtor = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedDtorCallbackPointer
                : Pointers<TComponent>.UnmanagedDtorCallbackPointer;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Move callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Move(Ecs.MoveCallback<TComponent> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.move == IntPtr.Zero ||
                hooks.move == Pointers<TComponent>.DefaultManagedMoveCallback ||
                hooks.move == Pointers<TComponent>.ManagedMoveCallbackDelegate ||
                hooks.move == Pointers<TComponent>.ManagedMoveCallbackPointer ||
                hooks.move == Pointers<TComponent>.UnmanagedMoveCallbackDelegate ||
                hooks.move == Pointers<TComponent>.UnmanagedMoveCallbackPointer,
                "Cannot register Move hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Move, callback, false);

            hooks.move = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedMoveCallbackDelegate
                : Pointers<TComponent>.UnmanagedMoveCallbackDelegate;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Move callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Move(delegate*<ref TComponent, ref TComponent, TypeInfo, void> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.move == IntPtr.Zero ||
                hooks.move == Pointers<TComponent>.DefaultManagedMoveCallback ||
                hooks.move == Pointers<TComponent>.ManagedMoveCallbackDelegate ||
                hooks.move == Pointers<TComponent>.ManagedMoveCallbackPointer ||
                hooks.move == Pointers<TComponent>.UnmanagedMoveCallbackDelegate ||
                hooks.move == Pointers<TComponent>.UnmanagedMoveCallbackPointer,
                "Cannot register Move hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Move, (IntPtr)callback);

            hooks.move = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedMoveCallbackPointer
                : Pointers<TComponent>.UnmanagedMoveCallbackPointer;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Copy callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Copy(Ecs.CopyCallback<TComponent> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.copy == IntPtr.Zero ||
                hooks.copy == Pointers<TComponent>.DefaultManagedCopyCallback ||
                hooks.copy == Pointers<TComponent>.ManagedCopyCallbackDelegate ||
                hooks.copy == Pointers<TComponent>.ManagedCopyCallbackPointer ||
                hooks.copy == Pointers<TComponent>.UnmanagedCopyCallbackDelegate ||
                hooks.copy == Pointers<TComponent>.UnmanagedCopyCallbackPointer,
                "Cannot register Copy hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Copy, callback, false);

            hooks.copy = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedCopyCallbackDelegate
                : Pointers<TComponent>.UnmanagedCopyCallbackDelegate;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers a Copy callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> Copy(delegate*<ref TComponent, ref TComponent, TypeInfo, void> callback)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.copy == IntPtr.Zero ||
                hooks.copy == Pointers<TComponent>.DefaultManagedCopyCallback ||
                hooks.copy == Pointers<TComponent>.ManagedCopyCallbackDelegate ||
                hooks.copy == Pointers<TComponent>.ManagedCopyCallbackPointer ||
                hooks.copy == Pointers<TComponent>.UnmanagedCopyCallbackDelegate ||
                hooks.copy == Pointers<TComponent>.UnmanagedCopyCallbackPointer,
                "Cannot register Copy hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->Copy, (IntPtr)callback);

            hooks.copy = RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
                ? Pointers<TComponent>.ManagedCopyCallbackPointer
                : Pointers<TComponent>.UnmanagedCopyCallbackPointer;

            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(Ecs.IterFieldCallback<TComponent> callback)
        {
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddIterFieldCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(Ecs.IterSpanCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddIterSpanCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(Ecs.IterPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddIterPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(Ecs.EachRefCallback<TComponent> callback)
        {
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(Ecs.EachEntityRefCallback<TComponent> callback)
        {
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachEntityRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(Ecs.EachIterRefCallback<TComponent> callback)
        {
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachIterRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(Ecs.EachPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(Ecs.EachEntityPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachEntityPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(Ecs.EachIterPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachIterPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(Ecs.IterFieldCallback<TComponent> callback)
        {
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetIterFieldCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(Ecs.IterSpanCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetIterSpanCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(Ecs.IterPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetIterPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(Ecs.EachRefCallback<TComponent> callback)
        {
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(Ecs.EachEntityRefCallback<TComponent> callback)
        {
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachEntityRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(Ecs.EachIterRefCallback<TComponent> callback)
        {
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachIterRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(Ecs.EachPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(Ecs.EachEntityPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachEntityPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(Ecs.EachIterPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachIterPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(Ecs.IterFieldCallback<TComponent> callback)
        {
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveIterFieldCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(Ecs.IterSpanCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveIterSpanCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(Ecs.IterPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveIterPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(Ecs.EachRefCallback<TComponent> callback)
        {
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(Ecs.EachEntityRefCallback<TComponent> callback)
        {
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachEntityRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(Ecs.EachIterRefCallback<TComponent> callback)
        {
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachIterRefCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(Ecs.EachPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(Ecs.EachEntityPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachEntityPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(Ecs.EachIterPointerCallback<T> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachIterPointerCallbackDelegate);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(delegate*<Iter, Field<TComponent>, void> callback)
        {
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddIterFieldCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(delegate*<Iter, Span<T>, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddIterSpanCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(delegate*<Iter, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddIterPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(delegate*<ref TComponent, void> callback)
        {
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddEachRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(delegate*<Entity, ref TComponent, void> callback)
        {
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddEachEntityRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd(delegate*<Iter, int, ref TComponent, void> callback)
        {
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddEachIterRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(delegate*<T*, void>callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddEachPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(delegate*<Entity, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddEachEntityPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnAdd callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnAdd<T>(delegate*<Iter, int, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnAddCallback((IntPtr)callback, Pointers<TComponent>.OnAddEachIterPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(delegate*<Iter, Field<TComponent>, void> callback)
        {
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetIterFieldCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(delegate*<Iter, Span<T>, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetIterSpanCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(delegate*<Iter, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetIterPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(delegate*<ref TComponent, void> callback)
        {
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetEachRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(delegate*<Entity, ref TComponent, void> callback)
        {
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetEachEntityRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet(delegate*<Iter, int, ref TComponent, void> callback)
        {
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetEachIterRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(delegate*<T*, void>callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetEachPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(delegate*<Entity, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetEachEntityPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnSet callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnSet<T>(delegate*<Iter, int, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnSetCallback((IntPtr)callback, Pointers<TComponent>.OnSetEachIterPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(delegate*<Iter, Field<TComponent>, void> callback)
        {
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveIterFieldCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(delegate*<Iter, Span<T>, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveIterSpanCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(delegate*<Iter, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveIterPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(delegate*<ref TComponent, void> callback)
        {
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveEachRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(delegate*<Entity, ref TComponent, void> callback)
        {
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveEachEntityRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove(delegate*<Iter, int, ref TComponent, void> callback)
        {
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveEachIterRefCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(delegate*<T*, void>callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveEachPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(delegate*<Entity, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveEachEntityPointerCallbackPointer);
        }

        /// <summary>
        ///     Registers an OnRemove callback for this component.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref Component<TComponent> OnRemove<T>(delegate*<Iter, int, T*, void> callback) where T : unmanaged, TComponent
        {
            Ecs.Assert(typeof(T) == typeof(TComponent), "T must match TComponent type.");
            return ref SetOnRemoveCallback((IntPtr)callback, Pointers<TComponent>.OnRemoveEachIterPointerCallbackPointer);
        }

        private ref Component<TComponent> SetOnAddCallback<T>(T? callback, IntPtr invoker) where T : Delegate
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.on_add == IntPtr.Zero ||

                hooks.on_add == Pointers<TComponent>.OnAddIterFieldCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddIterSpanCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddIterPointerCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachRefCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityRefCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterRefCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachPointerCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityPointerCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterPointerCallbackDelegate ||

                hooks.on_add == Pointers<TComponent>.OnAddIterFieldCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddIterSpanCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddIterPointerCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachRefCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityRefCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterRefCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachPointerCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityPointerCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterPointerCallbackPointer,
                "Cannot register OnAdd hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->OnAdd, callback, false);
            hooks.on_add = invoker;
            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        private ref Component<TComponent> SetOnSetCallback<T>(T? callback, IntPtr invoker) where T : Delegate
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.on_set == IntPtr.Zero ||

                hooks.on_set == Pointers<TComponent>.OnSetIterFieldCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetIterSpanCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetIterPointerCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachRefCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityRefCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterRefCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachPointerCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityPointerCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterPointerCallbackDelegate ||

                hooks.on_set == Pointers<TComponent>.OnSetIterFieldCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetIterSpanCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetIterPointerCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachRefCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityRefCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterRefCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachPointerCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityPointerCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterPointerCallbackPointer,
                "Cannot register OnSet hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->OnSet, callback, false);
            hooks.on_set = invoker;
            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        private ref Component<TComponent> SetOnRemoveCallback<T>(T? callback, IntPtr invoker) where T : Delegate
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.on_remove == IntPtr.Zero ||

                hooks.on_remove == Pointers<TComponent>.OnRemoveIterFieldCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterSpanCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterPointerCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachRefCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityRefCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterRefCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachPointerCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityPointerCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterPointerCallbackDelegate ||

                hooks.on_remove == Pointers<TComponent>.OnRemoveIterFieldCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterSpanCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterPointerCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachRefCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityRefCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterRefCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachPointerCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityPointerCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterPointerCallbackPointer,
                "Cannot register OnRemove hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->OnRemove, callback, false);
            hooks.on_remove = invoker;
            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        private ref Component<TComponent> SetOnAddCallback(IntPtr callback, IntPtr invoker)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.on_add == IntPtr.Zero ||

                hooks.on_add == Pointers<TComponent>.OnAddIterFieldCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddIterSpanCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddIterPointerCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachRefCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityRefCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterRefCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachPointerCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityPointerCallbackDelegate ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterPointerCallbackDelegate ||

                hooks.on_add == Pointers<TComponent>.OnAddIterFieldCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddIterSpanCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddIterPointerCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachRefCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityRefCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterRefCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachPointerCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachEntityPointerCallbackPointer ||
                hooks.on_add == Pointers<TComponent>.OnAddEachIterPointerCallbackPointer,
                "Cannot register OnAdd hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->OnAdd, callback);
            hooks.on_add = invoker;
            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        private ref Component<TComponent> SetOnSetCallback(IntPtr callback, IntPtr invoker)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.on_set == IntPtr.Zero ||

                hooks.on_set == Pointers<TComponent>.OnSetIterFieldCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetIterSpanCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetIterPointerCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachRefCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityRefCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterRefCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachPointerCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityPointerCallbackDelegate ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterPointerCallbackDelegate ||

                hooks.on_set == Pointers<TComponent>.OnSetIterFieldCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetIterSpanCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetIterPointerCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachRefCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityRefCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterRefCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachPointerCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachEntityPointerCallbackPointer ||
                hooks.on_set == Pointers<TComponent>.OnSetEachIterPointerCallbackPointer,
                "Cannot register OnSet hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->OnSet, callback);
            hooks.on_set = invoker;
            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }

        private ref Component<TComponent> SetOnRemoveCallback(IntPtr callback, IntPtr invoker)
        {
            GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);

            Ecs.Assert(
                hooks.on_remove == IntPtr.Zero ||

                hooks.on_remove == Pointers<TComponent>.OnRemoveIterFieldCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterSpanCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterPointerCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachRefCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityRefCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterRefCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachPointerCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityPointerCallbackDelegate ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterPointerCallbackDelegate ||

                hooks.on_remove == Pointers<TComponent>.OnRemoveIterFieldCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterSpanCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveIterPointerCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachRefCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityRefCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterRefCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachPointerCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachEntityPointerCallbackPointer ||
                hooks.on_remove == Pointers<TComponent>.OnRemoveEachIterPointerCallbackPointer,
                "Cannot register OnRemove hook because it is already registered by a non Flecs.NET application.");

            Callback.Set(ref context->OnRemove, callback);
            hooks.on_remove = invoker;
            ecs_set_hooks_id(World, Id, &hooks);

            return ref this;
        }
    }
}
