using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core.BindingContext;

internal unsafe delegate int OpaqueSerializeThunk(ecs_serializer_t* serializer, void* ptr);
internal unsafe delegate int OpaqueSerializeMemberThunk(ecs_serializer_t* serializer, void* ptr, byte* member);
internal unsafe delegate int OpaqueSerializeElementThunk(ecs_serializer_t* serializer, void* ptr, nuint index);
internal unsafe delegate void OpaqueAssignBoolThunk(void* ptr, bool value);
internal unsafe delegate void OpaqueAssignCharThunk(void* ptr, byte value);
internal unsafe delegate void OpaqueAssignIntThunk(void* ptr, long value);
internal unsafe delegate void OpaqueAssignUIntThunk(void* ptr, ulong value);
internal unsafe delegate void OpaqueAssignFloatThunk(void* ptr, double value);
internal unsafe delegate void OpaqueAssignStringThunk(void* ptr, byte* value);
internal unsafe delegate void OpaqueAssignEntityThunk(void* ptr, ecs_world_t* world, ulong value);
internal unsafe delegate void OpaqueAssignIdThunk(void* ptr, ecs_world_t* world, ulong value);
internal unsafe delegate void OpaqueAssignNullThunk(void* ptr);
internal unsafe delegate void OpaqueClearThunk(void* ptr);
internal unsafe delegate void* OpaqueEnsureMemberThunk(void* ptr, byte* member);
internal unsafe delegate nint OpaqueCountThunk(void* ptr);
internal unsafe delegate void OpaqueResizeThunk(void* ptr, nint size);
internal unsafe delegate void* OpaqueEnsureElementThunk(void* ptr, nint index);

[SuppressMessage("Design", "CA1031:Do not catch general exception types",
    Justification = "Exceptions must never escape into native code.")]
internal unsafe sealed class OpaqueContext<TComponent, TElement>
    where TElement : unmanaged
{
    private readonly World _world;
    private readonly GCHandle _handle;

    internal Ecs.OpaqueSerializeCallback<TComponent>? Serialize;
    internal Ecs.OpaqueSerializeMemberCallback<TComponent>? SerializeMember;
    internal Ecs.OpaqueSerializeElementCallback<TComponent>? SerializeElement;
    internal Ecs.OpaqueAssignBoolCallback<TComponent>? AssignBool;
    internal Ecs.OpaqueAssignCharCallback<TComponent>? AssignChar;
    internal Ecs.OpaqueAssignIntCallback<TComponent>? AssignInt;
    internal Ecs.OpaqueAssignUIntCallback<TComponent>? AssignUInt;
    internal Ecs.OpaqueAssignFloatCallback<TComponent>? AssignFloat;
    internal Ecs.OpaqueAssignStringCallback<TComponent>? AssignString;
    internal Ecs.OpaqueAssignEntityCallback<TComponent>? AssignEntity;
    internal Ecs.OpaqueAssignIdCallback<TComponent>? AssignId;
    internal Ecs.OpaqueAssignNullCallback<TComponent>? AssignNull;
    internal Ecs.OpaqueClearCallback<TComponent>? Clear;
    internal Ecs.OpaqueEnsureMemberCallback<TComponent>? EnsureMember;
    internal Ecs.OpaqueCountCallback<TComponent>? Count;
    internal Ecs.OpaqueResizeCallback<TComponent>? Resize;
    internal Ecs.OpaqueEnsureElementCallback<TComponent, TElement>? EnsureElement;

    private readonly OpaqueSerializeThunk _serializeInvoker;
    private readonly OpaqueSerializeMemberThunk _serializeMemberInvoker;
    private readonly OpaqueSerializeElementThunk _serializeElementInvoker;
    private readonly OpaqueAssignBoolThunk _assignBoolInvoker;
    private readonly OpaqueAssignCharThunk _assignCharInvoker;
    private readonly OpaqueAssignIntThunk _assignIntInvoker;
    private readonly OpaqueAssignUIntThunk _assignUIntInvoker;
    private readonly OpaqueAssignFloatThunk _assignFloatInvoker;
    private readonly OpaqueAssignStringThunk _assignStringInvoker;
    private readonly OpaqueAssignEntityThunk _assignEntityInvoker;
    private readonly OpaqueAssignIdThunk _assignIdInvoker;
    private readonly OpaqueAssignNullThunk _assignNullInvoker;
    private readonly OpaqueClearThunk _clearInvoker;
    private readonly OpaqueEnsureMemberThunk _ensureMemberInvoker;
    private readonly OpaqueCountThunk _countInvoker;
    private readonly OpaqueResizeThunk _resizeInvoker;
    private readonly OpaqueEnsureElementThunk _ensureElementInvoker;

    internal readonly delegate* unmanaged<ecs_serializer_t*, void*, int> SerializePointer;
    internal readonly delegate* unmanaged<ecs_serializer_t*, void*, byte*, int> SerializeMemberPointer;
    internal readonly delegate* unmanaged<ecs_serializer_t*, void*, nuint, int> SerializeElementPointer;
    internal readonly delegate* unmanaged<void*, bool, void> AssignBoolPointer;
    internal readonly delegate* unmanaged<void*, byte, void> AssignCharPointer;
    internal readonly delegate* unmanaged<void*, long, void> AssignIntPointer;
    internal readonly delegate* unmanaged<void*, ulong, void> AssignUIntPointer;
    internal readonly delegate* unmanaged<void*, double, void> AssignFloatPointer;
    internal readonly delegate* unmanaged<void*, byte*, void> AssignStringPointer;
    internal readonly delegate* unmanaged<void*, ecs_world_t*, ulong, void> AssignEntityPointer;
    internal readonly delegate* unmanaged<void*, ecs_world_t*, ulong, void> AssignIdPointer;
    internal readonly delegate* unmanaged<void*, void> AssignNullPointer;
    internal readonly delegate* unmanaged<void*, void> ClearPointer;
    internal readonly delegate* unmanaged<void*, byte*, void*> EnsureMemberPointer;
    internal readonly delegate* unmanaged<void*, nint> CountPointer;
    internal readonly delegate* unmanaged<void*, nint, void> ResizePointer;
    internal readonly delegate* unmanaged<void*, nint, void*> EnsureElementPointer;

    internal OpaqueContext(World world)
    {
        _world = world;
        _serializeInvoker = SerializeCallback;
        _serializeMemberInvoker = SerializeMemberCallback;
        _serializeElementInvoker = SerializeElementCallback;
        _assignBoolInvoker = AssignBoolCallback;
        _assignCharInvoker = AssignCharCallback;
        _assignIntInvoker = AssignIntCallback;
        _assignUIntInvoker = AssignUIntCallback;
        _assignFloatInvoker = AssignFloatCallback;
        _assignStringInvoker = AssignStringCallback;
        _assignEntityInvoker = AssignEntityCallback;
        _assignIdInvoker = AssignIdCallback;
        _assignNullInvoker = AssignNullCallback;
        _clearInvoker = ClearCallback;
        _ensureMemberInvoker = EnsureMemberCallback;
        _countInvoker = CountCallback;
        _resizeInvoker = ResizeCallback;
        _ensureElementInvoker = EnsureElementCallback;

        SerializePointer = (delegate* unmanaged<ecs_serializer_t*, void*, int>)Marshal.GetFunctionPointerForDelegate(_serializeInvoker);
        SerializeMemberPointer = (delegate* unmanaged<ecs_serializer_t*, void*, byte*, int>)Marshal.GetFunctionPointerForDelegate(_serializeMemberInvoker);
        SerializeElementPointer = (delegate* unmanaged<ecs_serializer_t*, void*, nuint, int>)Marshal.GetFunctionPointerForDelegate(_serializeElementInvoker);
        AssignBoolPointer = (delegate* unmanaged<void*, bool, void>)Marshal.GetFunctionPointerForDelegate(_assignBoolInvoker);
        AssignCharPointer = (delegate* unmanaged<void*, byte, void>)Marshal.GetFunctionPointerForDelegate(_assignCharInvoker);
        AssignIntPointer = (delegate* unmanaged<void*, long, void>)Marshal.GetFunctionPointerForDelegate(_assignIntInvoker);
        AssignUIntPointer = (delegate* unmanaged<void*, ulong, void>)Marshal.GetFunctionPointerForDelegate(_assignUIntInvoker);
        AssignFloatPointer = (delegate* unmanaged<void*, double, void>)Marshal.GetFunctionPointerForDelegate(_assignFloatInvoker);
        AssignStringPointer = (delegate* unmanaged<void*, byte*, void>)Marshal.GetFunctionPointerForDelegate(_assignStringInvoker);
        AssignEntityPointer = (delegate* unmanaged<void*, ecs_world_t*, ulong, void>)Marshal.GetFunctionPointerForDelegate(_assignEntityInvoker);
        AssignIdPointer = (delegate* unmanaged<void*, ecs_world_t*, ulong, void>)Marshal.GetFunctionPointerForDelegate(_assignIdInvoker);
        AssignNullPointer = (delegate* unmanaged<void*, void>)Marshal.GetFunctionPointerForDelegate(_assignNullInvoker);
        ClearPointer = (delegate* unmanaged<void*, void>)Marshal.GetFunctionPointerForDelegate(_clearInvoker);
        EnsureMemberPointer = (delegate* unmanaged<void*, byte*, void*>)Marshal.GetFunctionPointerForDelegate(_ensureMemberInvoker);
        CountPointer = (delegate* unmanaged<void*, nint>)Marshal.GetFunctionPointerForDelegate(_countInvoker);
        ResizePointer = (delegate* unmanaged<void*, nint, void>)Marshal.GetFunctionPointerForDelegate(_resizeInvoker);
        EnsureElementPointer = (delegate* unmanaged<void*, nint, void*>)Marshal.GetFunctionPointerForDelegate(_ensureElementInvoker);

        _handle = GCHandle.Alloc(this);
        _world.WorldContext.OpaqueContexts.Add((nint)GCHandle.ToIntPtr(_handle));
    }

    private int SerializeCallback(ecs_serializer_t* serializer, void* ptr)
    {
        try
        {
            Ecs.Serializer value = new(serializer);
            return Serialize!(ref value, in Managed.GetTypeRef<TComponent>(ptr));
        }
        catch (Exception exception)
        {
            LogCallbackException(nameof(Serialize), exception);
            return -1;
        }
    }

    private int SerializeMemberCallback(ecs_serializer_t* serializer, void* ptr, byte* member)
    {
        try
        {
            Ecs.Serializer value = new(serializer);
            return SerializeMember!(ref value, in Managed.GetTypeRef<TComponent>(ptr), NativeString.GetString(member));
        }
        catch (Exception exception)
        {
            LogCallbackException(nameof(SerializeMember), exception);
            return -1;
        }
    }

    private int SerializeElementCallback(ecs_serializer_t* serializer, void* ptr, nuint index)
    {
        try
        {
            Ecs.Serializer value = new(serializer);
            return SerializeElement!(ref value, in Managed.GetTypeRef<TComponent>(ptr), index);
        }
        catch (Exception exception)
        {
            LogCallbackException(nameof(SerializeElement), exception);
            return -1;
        }
    }

    private void AssignBoolCallback(void* ptr, bool value) => Invoke(nameof(AssignBool), () => AssignBool!(ref Managed.GetTypeRef<TComponent>(ptr), value));
    private void AssignCharCallback(void* ptr, byte value) => Invoke(nameof(AssignChar), () => AssignChar!(ref Managed.GetTypeRef<TComponent>(ptr), (char)value));
    private void AssignIntCallback(void* ptr, long value) => Invoke(nameof(AssignInt), () => AssignInt!(ref Managed.GetTypeRef<TComponent>(ptr), value));
    private void AssignUIntCallback(void* ptr, ulong value) => Invoke(nameof(AssignUInt), () => AssignUInt!(ref Managed.GetTypeRef<TComponent>(ptr), value));
    private void AssignFloatCallback(void* ptr, double value) => Invoke(nameof(AssignFloat), () => AssignFloat!(ref Managed.GetTypeRef<TComponent>(ptr), value));
    private void AssignStringCallback(void* ptr, byte* value) => Invoke(nameof(AssignString), () => AssignString!(ref Managed.GetTypeRef<TComponent>(ptr), NativeString.GetString(value)));
    private void AssignEntityCallback(void* ptr, ecs_world_t* world, ulong value) => Invoke(nameof(AssignEntity), () => AssignEntity!(ref Managed.GetTypeRef<TComponent>(ptr), new World(world), value));
    private void AssignIdCallback(void* ptr, ecs_world_t* world, ulong value) => Invoke(nameof(AssignId), () => AssignId!(ref Managed.GetTypeRef<TComponent>(ptr), new World(world), value));
    private void AssignNullCallback(void* ptr) => Invoke(nameof(AssignNull), () => AssignNull!(ref Managed.GetTypeRef<TComponent>(ptr)));
    private void ClearCallback(void* ptr) => Invoke(nameof(Clear), () => Clear!(ref Managed.GetTypeRef<TComponent>(ptr)));

    private void* EnsureMemberCallback(void* ptr, byte* member)
    {
        try { return Unsafe.AsPointer(ref EnsureMember!(ref Managed.GetTypeRef<TComponent>(ptr), NativeString.GetString(member))); }
        catch (Exception exception) { LogCallbackException(nameof(EnsureMember), exception); return null; }
    }

    private nint CountCallback(void* ptr)
    {
        try { return Count!(Managed.GetTypeRef<TComponent>(ptr)); }
        catch (Exception exception) { LogCallbackException(nameof(Count), exception); return 0; }
    }

    private void ResizeCallback(void* ptr, nint size) => Invoke(nameof(Resize), () => Resize!(ref Managed.GetTypeRef<TComponent>(ptr), (nuint)size));

    private void* EnsureElementCallback(void* ptr, nint index)
    {
        try { return Unsafe.AsPointer(ref EnsureElement!(ref Managed.GetTypeRef<TComponent>(ptr), (nuint)index)); }
        catch (Exception exception) { LogCallbackException(nameof(EnsureElement), exception); return null; }
    }

    private static void Invoke(string name, Action callback)
    {
        try { callback(); }
        catch (Exception exception) { LogCallbackException(name, exception); }
    }

    private static void LogCallbackException(string callback, Exception exception)
        => Ecs.Log.Err($"Exception thrown from opaque '{callback}' callback of type '{typeof(TComponent)}': {exception.Message}");
}
