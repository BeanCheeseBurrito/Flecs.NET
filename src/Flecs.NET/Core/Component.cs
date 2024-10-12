using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

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

    internal void GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context)
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
        hooks.ctor = Pointers.CtorCallback;
        context->Ctor.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedCtorCallbackDelegate
            : Pointers<TComponent>.UnmanagedCtorCallbackDelegate);

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
        hooks.ctor = Pointers.CtorCallback;
        context->Ctor.Set((nint)callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedCtorCallbackPointer
            : Pointers<TComponent>.UnmanagedCtorCallbackPointer);

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
        hooks.dtor = Pointers.DtorCallback;
        context->Dtor.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedDtorCallbackDelegate
            : Pointers<TComponent>.UnmanagedDtorCallbackDelegate);

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
        hooks.dtor = Pointers.DtorCallback;
        context->Dtor.Set((nint)callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedDtorCallbackPointer
            : Pointers<TComponent>.UnmanagedDtorCallbackPointer);

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
        hooks.move = Pointers.MoveCallback;
        context->Move.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedMoveCallbackDelegate
            : Pointers<TComponent>.UnmanagedMoveCallbackDelegate);

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
        hooks.move = Pointers.MoveCallback;
        context->Move.Set((nint)callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedMoveCallbackPointer
            : Pointers<TComponent>.UnmanagedMoveCallbackPointer);

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
        hooks.copy = Pointers.CopyCallback;
        context->Copy.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedCopyCallbackDelegate
            : Pointers<TComponent>.UnmanagedCopyCallbackDelegate);

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
        hooks.copy = Pointers.CopyCallback;
        context->Copy.Set((nint)callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? Pointers<TComponent>.ManagedCopyCallbackPointer
            : Pointers<TComponent>.UnmanagedCopyCallbackPointer);

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
    public ref Component<TComponent> OnAdd(Ecs.IterSpanCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddIterSpanCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.IterPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
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
    public ref Component<TComponent> OnAdd(Ecs.EachPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachEntityPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, Pointers<TComponent>.OnAddEachEntityPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachIterPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
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
    public ref Component<TComponent> OnSet(Ecs.IterSpanCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetIterSpanCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.IterPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
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
    public ref Component<TComponent> OnSet(Ecs.EachPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachEntityPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, Pointers<TComponent>.OnSetEachEntityPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachIterPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
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
    public ref Component<TComponent> OnRemove(Ecs.IterSpanCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveIterSpanCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.IterPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
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
    public ref Component<TComponent> OnRemove(Ecs.EachPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachEntityPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachEntityPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachIterPointerCallback<TComponent> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, Pointers<TComponent>.OnRemoveEachIterPointerCallbackDelegate);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, Field<TComponent>, void> callback)
    {
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddIterFieldCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, Span<TComponent>, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddIterSpanCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddIterPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<ref TComponent, void> callback)
    {
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddEachRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Entity, ref TComponent, void> callback)
    {
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddEachEntityRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, int, ref TComponent, void> callback)
    {
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddEachIterRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<TComponent*, void>callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddEachPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Entity, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddEachEntityPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, int, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnAddCallback((nint)callback, Pointers<TComponent>.OnAddEachIterPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, Field<TComponent>, void> callback)
    {
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetIterFieldCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, Span<TComponent>, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetIterSpanCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetIterPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<ref TComponent, void> callback)
    {
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetEachRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Entity, ref TComponent, void> callback)
    {
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetEachEntityRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, int, ref TComponent, void> callback)
    {
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetEachIterRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<TComponent*, void>callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetEachPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Entity, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetEachEntityPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, int, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnSetCallback((nint)callback, Pointers<TComponent>.OnSetEachIterPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, Field<TComponent>, void> callback)
    {
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveIterFieldCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, Span<TComponent>, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveIterSpanCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveIterPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<ref TComponent, void> callback)
    {
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveEachRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Entity, ref TComponent, void> callback)
    {
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveEachEntityRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, int, ref TComponent, void> callback)
    {
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveEachIterRefCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<TComponent*, void>callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveEachPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Entity, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveEachEntityPointerCallbackPointer);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, int, TComponent*, void> callback)
    {
        TypeHelper<TComponent>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback((nint)callback, Pointers<TComponent>.OnRemoveEachIterPointerCallbackPointer);
    }

    private ref Component<TComponent> SetOnAddCallback<T>(T callback, nint invoker) where T : Delegate
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_add = Pointers.OnAddCallback;
        context->OnAdd.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnSetCallback<T>(T callback, nint invoker) where T : Delegate
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_set = Pointers.OnSetCallback;
        context->OnSet.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnRemoveCallback<T>(T callback, nint invoker) where T : Delegate
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_remove = Pointers.OnRemoveCallback;
        context->OnRemove.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnAddCallback(nint callback, nint invoker)
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_add = Pointers.OnAddCallback;
        context->OnAdd.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnSetCallback(nint callback, nint invoker)
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_set = Pointers.OnSetCallback;
        context->OnSet.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnRemoveCallback(nint callback, nint invoker)
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_remove = Pointers.OnRemoveCallback;
        context->OnRemove.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }
}
