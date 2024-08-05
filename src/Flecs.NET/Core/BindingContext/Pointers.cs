using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core.BindingContext;

/// <summary>
///     A static class for binding context pointers.
/// </summary>
internal static unsafe class Pointers
{
    static Pointers()
    {
        AppDomain.CurrentDomain.ProcessExit += (object? _, EventArgs _) =>
        {
            Memory.Free(DefaultSeparator);
        };
    }

    #region Native Resources

    internal static readonly byte* DefaultSeparator = (byte*)Marshal.StringToHGlobalAnsi(Ecs.DefaultSeparator);

    #endregion

    #region Callback Handlers

    internal static readonly IntPtr ActionCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.ActionCallbackDelegate;
    internal static readonly IntPtr RunCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.RunCallbackDelegate;
    internal static readonly IntPtr RunDelegateCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.RunDelegateCallbackDelegate;
    internal static readonly IntPtr RunPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.RunPointerCallbackDelegate;
    internal static readonly IntPtr IterCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.IterCallbackDelegate;
    internal static readonly IntPtr EachEntityCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.EachEntityCallbackDelegate;
    internal static readonly IntPtr EachIterCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.EachIterCallbackDelegate;
    internal static readonly IntPtr ObserveEntityCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.ObserveEntityCallbackDelegate;
    internal static readonly IntPtr GroupByCallbackDelegate = (IntPtr)(delegate* <ecs_world_t*, ecs_table_t*, ulong, void*, ulong>)&Functions.GroupByCallbackDelegate;

    internal static readonly IntPtr ActionCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.ActionCallbackPointer;
    internal static readonly IntPtr RunCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.RunCallbackPointer;
    internal static readonly IntPtr RunDelegateCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.RunDelegateCallbackPointer;
    internal static readonly IntPtr RunPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.RunPointerCallbackPointer;
    internal static readonly IntPtr IterCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.IterCallbackPointer;
    internal static readonly IntPtr EachEntityCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.EachEntityCallbackPointer;
    internal static readonly IntPtr EachIterCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.EachIterCallbackPointer;
    internal static readonly IntPtr ObserveEntityCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions.ObserveEntityCallbackPointer;
    internal static readonly IntPtr GroupByCallbackPointer = (IntPtr)(delegate* <ecs_world_t*, ecs_table_t*, ulong, void*, ulong>)&Functions.GroupByCallbackPointer;

    #endregion

    #region Context Free

    internal static readonly IntPtr WorldContextFree = (IntPtr)(delegate* <WorldContext*, void>)&Functions.WorldContextFree;
    internal static readonly IntPtr IteratorContextFree = (IntPtr)(delegate* <IteratorContext*, void>)&Functions.IteratorContextFree;
    internal static readonly IntPtr RunContextFree = (IntPtr)(delegate* <RunContext*, void>)&Functions.RunContextFree;
    internal static readonly IntPtr QueryContextFree = (IntPtr)(delegate* <QueryContext*, void>)&Functions.QueryContextFree;
    internal static readonly IntPtr GroupByContextFree = (IntPtr)(delegate* <GroupByContext*, void>)&Functions.GroupByContextFree;
    internal static readonly IntPtr TypeHooksContextFree = (IntPtr)(delegate* <TypeHooksContext*, void>)&Functions.TypeHooksContextFree;

    #endregion

    #region Os Api

    internal static readonly IntPtr OsApiAbort = (IntPtr)(delegate* <void>)&Functions.OsApiAbort;

    #endregion
}

/// <summary>
///     A static class for binding context pointers.
/// </summary>
[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
internal static unsafe partial class Pointers<T0>
{
    #region Observe Callback Handlers

    internal static readonly IntPtr ObserveRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObserveRefCallbackDelegate;
    internal static readonly IntPtr ObservePointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObservePointerCallbackDelegate;
    internal static readonly IntPtr ObserveEntityRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObserveEntityRefCallbackDelegate;
    internal static readonly IntPtr ObserveEntityPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObserveEntityPointerCallbackDelegate;

    internal static readonly IntPtr ObserveRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObserveRefCallbackPointer;
    internal static readonly IntPtr ObservePointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObservePointerCallbackPointer;
    internal static readonly IntPtr ObserveEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObserveEntityRefCallbackPointer;
    internal static readonly IntPtr ObserveEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.ObserveEntityPointerCallbackPointer;

    #endregion

    #region Type Hook Callback Handlers

    internal static readonly IntPtr DefaultManagedCtorCallback = (IntPtr)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedCtorCallback;
    internal static readonly IntPtr DefaultManagedDtorCallback = (IntPtr)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedDtorCallback;
    internal static readonly IntPtr DefaultManagedMoveCallback = (IntPtr)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedMoveCallback;
    internal static readonly IntPtr DefaultManagedCopyCallback = (IntPtr)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedCopyCallback;

    internal static readonly IntPtr ManagedCtorCallbackDelegate = (IntPtr)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCtorCallbackDelegate;
    internal static readonly IntPtr ManagedDtorCallbackDelegate = (IntPtr)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedDtorCallbackDelegate;
    internal static readonly IntPtr ManagedMoveCallbackDelegate = (IntPtr)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedMoveCallbackDelegate;
    internal static readonly IntPtr ManagedCopyCallbackDelegate = (IntPtr)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCopyCallbackDelegate;

    internal static readonly IntPtr ManagedCtorCallbackPointer = (IntPtr)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCtorCallbackPointer;
    internal static readonly IntPtr ManagedDtorCallbackPointer = (IntPtr)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedDtorCallbackPointer;
    internal static readonly IntPtr ManagedMoveCallbackPointer = (IntPtr)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedMoveCallbackPointer;
    internal static readonly IntPtr ManagedCopyCallbackPointer = (IntPtr)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCopyCallbackPointer;

    internal static readonly IntPtr UnmanagedCtorCallbackDelegate = (IntPtr)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCtorCallbackDelegate;
    internal static readonly IntPtr UnmanagedDtorCallbackDelegate = (IntPtr)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedDtorCallbackDelegate;
    internal static readonly IntPtr UnmanagedMoveCallbackDelegate = (IntPtr)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedMoveCallbackDelegate;
    internal static readonly IntPtr UnmanagedCopyCallbackDelegate = (IntPtr)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCopyCallbackDelegate;

    internal static readonly IntPtr UnmanagedCtorCallbackPointer = (IntPtr)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCtorCallbackPointer;
    internal static readonly IntPtr UnmanagedDtorCallbackPointer = (IntPtr)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedDtorCallbackPointer;
    internal static readonly IntPtr UnmanagedMoveCallbackPointer = (IntPtr)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedMoveCallbackPointer;
    internal static readonly IntPtr UnmanagedCopyCallbackPointer = (IntPtr)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCopyCallbackPointer;

    internal static readonly IntPtr OnAddIterFieldCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterFieldCallbackDelegate;
    internal static readonly IntPtr OnSetIterFieldCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterFieldCallbackDelegate;
    internal static readonly IntPtr OnRemoveIterFieldCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterFieldCallbackDelegate;

    internal static readonly IntPtr OnAddIterFieldCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterFieldCallbackPointer;
    internal static readonly IntPtr OnSetIterFieldCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterFieldCallbackPointer;
    internal static readonly IntPtr OnRemoveIterFieldCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterFieldCallbackPointer;

    internal static readonly IntPtr OnAddIterSpanCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterSpanCallbackDelegate;
    internal static readonly IntPtr OnSetIterSpanCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterSpanCallbackDelegate;
    internal static readonly IntPtr OnRemoveIterSpanCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterSpanCallbackDelegate;

    internal static readonly IntPtr OnAddIterSpanCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterSpanCallbackPointer;
    internal static readonly IntPtr OnSetIterSpanCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterSpanCallbackPointer;
    internal static readonly IntPtr OnRemoveIterSpanCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterSpanCallbackPointer;

    internal static readonly IntPtr OnAddIterPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterPointerCallbackDelegate;
    internal static readonly IntPtr OnSetIterPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterPointerCallbackDelegate;
    internal static readonly IntPtr OnRemoveIterPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterPointerCallbackDelegate;

    internal static readonly IntPtr OnAddIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterPointerCallbackPointer;
    internal static readonly IntPtr OnSetIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterPointerCallbackPointer;
    internal static readonly IntPtr OnRemoveIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterPointerCallbackPointer;

    internal static readonly IntPtr OnAddEachRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachRefCallbackDelegate;
    internal static readonly IntPtr OnSetEachRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachRefCallbackDelegate;
    internal static readonly IntPtr OnRemoveEachRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachRefCallbackDelegate;

    internal static readonly IntPtr OnAddEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachRefCallbackPointer;
    internal static readonly IntPtr OnSetEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachRefCallbackPointer;
    internal static readonly IntPtr OnRemoveEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachRefCallbackPointer;

    internal static readonly IntPtr OnAddEachEntityRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityRefCallbackDelegate;
    internal static readonly IntPtr OnSetEachEntityRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityRefCallbackDelegate;
    internal static readonly IntPtr OnRemoveEachEntityRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityRefCallbackDelegate;

    internal static readonly IntPtr OnAddEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityRefCallbackPointer;
    internal static readonly IntPtr OnSetEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityRefCallbackPointer;
    internal static readonly IntPtr OnRemoveEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityRefCallbackPointer;

    internal static readonly IntPtr OnAddEachIterRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterRefCallbackDelegate;
    internal static readonly IntPtr OnSetEachIterRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterRefCallbackDelegate;
    internal static readonly IntPtr OnRemoveEachIterRefCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterRefCallbackDelegate;

    internal static readonly IntPtr OnAddEachIterRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterRefCallbackPointer;
    internal static readonly IntPtr OnSetEachIterRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterRefCallbackPointer;
    internal static readonly IntPtr OnRemoveEachIterRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterRefCallbackPointer;

    internal static readonly IntPtr OnAddEachPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachPointerCallbackDelegate;
    internal static readonly IntPtr OnSetEachPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachPointerCallbackDelegate;
    internal static readonly IntPtr OnRemoveEachPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachPointerCallbackDelegate;

    internal static readonly IntPtr OnAddEachPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachPointerCallbackPointer;
    internal static readonly IntPtr OnSetEachPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachPointerCallbackPointer;
    internal static readonly IntPtr OnRemoveEachPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachPointerCallbackPointer;

    internal static readonly IntPtr OnAddEachEntityPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityPointerCallbackDelegate;
    internal static readonly IntPtr OnSetEachEntityPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityPointerCallbackDelegate;
    internal static readonly IntPtr OnRemoveEachEntityPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityPointerCallbackDelegate;

    internal static readonly IntPtr OnAddEachEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityPointerCallbackPointer;
    internal static readonly IntPtr OnSetEachEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityPointerCallbackPointer;
    internal static readonly IntPtr OnRemoveEachEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityPointerCallbackPointer;

    internal static readonly IntPtr OnAddEachIterPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterPointerCallbackDelegate;
    internal static readonly IntPtr OnSetEachIterPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterPointerCallbackDelegate;
    internal static readonly IntPtr OnRemoveEachIterPointerCallbackDelegate = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterPointerCallbackDelegate;

    internal static readonly IntPtr OnAddEachIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterPointerCallbackPointer;
    internal static readonly IntPtr OnSetEachIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterPointerCallbackPointer;
    internal static readonly IntPtr OnRemoveEachIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterPointerCallbackPointer;

    #endregion
}
