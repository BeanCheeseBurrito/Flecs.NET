using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Struct used to register components and component metadata.
/// </summary>
/// <typeparam name="TComponent"></typeparam>
public unsafe partial struct Component<TComponent> : IEquatable<Component<TComponent>>, IEquatable<ulong>, IEntity<Component<TComponent>>
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
            hooks.binding_ctx_free = &Functions.TypeHooksContextFree;
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
        hooks.ctor = &Functions.CtorCallback;
        context->Ctor.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCtorCallbackDelegate<TComponent>
            : (delegate*<TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCtorCallbackDelegate<TComponent>);

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
        hooks.ctor = &Functions.CtorCallback;
        context->Ctor.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCtorCallbackPointer<TComponent>
            : (delegate*<TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCtorCallbackPointer<TComponent>);

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
        hooks.dtor = &Functions.DtorCallback;
        context->Dtor.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedDtorCallbackDelegate<TComponent>
            : (delegate*<TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedDtorCallbackDelegate<TComponent>);

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
        hooks.dtor = &Functions.DtorCallback;
        context->Dtor.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedDtorCallbackPointer<TComponent>
            : (delegate*<TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedDtorCallbackPointer<TComponent>);

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
        hooks.move = &Functions.MoveCallback;
        context->Move.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedMoveCallbackDelegate<TComponent>
            : (delegate*<TComponent*, TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedMoveCallbackDelegate<TComponent>);

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
        hooks.move = &Functions.MoveCallback;
        context->Move.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedMoveCallbackPointer<TComponent>
            : (delegate*<TComponent*, TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedMoveCallbackPointer<TComponent>);

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
        hooks.copy = &Functions.CopyCallback;
        context->Copy.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCopyCallbackDelegate<TComponent>
            : (delegate*<TComponent*, TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCopyCallbackDelegate<TComponent>);

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
        hooks.copy = &Functions.CopyCallback;
        context->Copy.Set(callback, RuntimeHelpers.IsReferenceOrContainsReferences<TComponent>()
            ? (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCopyCallbackPointer<TComponent>
            : (delegate*<TComponent*, TComponent*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCopyCallbackPointer<TComponent>);

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
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddIterFieldCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.IterSpanCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddIterSpanCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.IterPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddIterPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachRefCallback<TComponent> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachEntityRefCallback<TComponent> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachEntityRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachIterRefCallback<TComponent> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachIterRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachEntityPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachEntityPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(Ecs.EachIterPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachIterPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.IterFieldCallback<TComponent> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetIterFieldCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.IterSpanCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetIterSpanCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.IterPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetIterPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachRefCallback<TComponent> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachEntityRefCallback<TComponent> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachEntityRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachIterRefCallback<TComponent> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachIterRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachEntityPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachEntityPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(Ecs.EachIterPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachIterPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.IterFieldCallback<TComponent> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveIterFieldCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.IterSpanCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveIterSpanCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.IterPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveIterPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachRefCallback<TComponent> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachEntityRefCallback<TComponent> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachEntityRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachIterRefCallback<TComponent> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachIterRefCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachEntityPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachEntityPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(Ecs.EachIterPointerCallback<TComponent> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachIterPointerCallbackDelegate<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, Field<TComponent>, void> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddIterFieldCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, Span<TComponent>, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddIterSpanCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddIterPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<ref TComponent, void> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Entity, ref TComponent, void> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachEntityRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, int, ref TComponent, void> callback)
    {
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachIterRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<TComponent*, void>callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Entity, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachEntityPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnAdd callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnAdd(delegate*<Iter, int, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnAddCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachIterPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, Field<TComponent>, void> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetIterFieldCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, Span<TComponent>, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetIterSpanCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetIterPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<ref TComponent, void> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Entity, ref TComponent, void> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachEntityRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, int, ref TComponent, void> callback)
    {
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachIterRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<TComponent*, void>callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Entity, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachEntityPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnSet callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnSet(delegate*<Iter, int, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnSetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachIterPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, Field<TComponent>, void> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveIterFieldCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, Span<TComponent>, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveIterSpanCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveIterPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<ref TComponent, void> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Entity, ref TComponent, void> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachEntityRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, int, ref TComponent, void> callback)
    {
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachIterRefCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<TComponent*, void>callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Entity, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachEntityPointerCallbackPointer<TComponent>);
    }

    /// <summary>
    ///     Registers an OnRemove callback for this component.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>Reference to self.</returns>
    public ref Component<TComponent> OnRemove(delegate*<Iter, int, TComponent*, void> callback)
    {
        Types<TComponent, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _>.AssertReferenceTypes(false);
        return ref SetOnRemoveCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachIterPointerCallbackPointer<TComponent>);
    }

    private ref Component<TComponent> SetOnAddCallback<T>(T callback, void* invoker) where T : Delegate
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_add = &Functions.OnAddCallback;
        context->OnAdd.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnSetCallback<T>(T callback, void* invoker) where T : Delegate
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_set = &Functions.OnSetCallback;
        context->OnSet.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnRemoveCallback<T>(T callback, void* invoker) where T : Delegate
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_remove = &Functions.OnRemoveCallback;
        context->OnRemove.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnAddCallback(void* callback, void* invoker)
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_add = &Functions.OnAddCallback;
        context->OnAdd.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnSetCallback(void* callback, void* invoker)
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_set = &Functions.OnSetCallback;
        context->OnSet.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }

    private ref Component<TComponent> SetOnRemoveCallback(void* callback, void* invoker)
    {
        GetHooksAndContext(out ecs_type_hooks_t hooks, out TypeHooksContext* context);
        hooks.on_remove = &Functions.OnRemoveCallback;
        context->OnRemove.Set(callback, invoker);
        ecs_set_hooks_id(World, Id, &hooks);
        return ref this;
    }
}
