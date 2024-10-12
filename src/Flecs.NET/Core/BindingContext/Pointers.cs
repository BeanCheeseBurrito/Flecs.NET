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

    #region Iterator Callbacks

    internal static readonly nint IteratorCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.IteratorCallback;

    internal static readonly nint ActionCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.ActionCallbackDelegate;
    internal static readonly nint IterCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.IterCallbackDelegate;
    internal static readonly nint EachEntityCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.EachEntityCallbackDelegate;
    internal static readonly nint EachIterCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.EachIterCallbackDelegate;
    internal static readonly nint ObserveEntityCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.ObserveEntityCallbackDelegate;

    internal static readonly nint ActionCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.ActionCallbackPointer;
    internal static readonly nint IterCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.IterCallbackPointer;
    internal static readonly nint EachEntityCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.EachEntityCallbackPointer;
    internal static readonly nint EachIterCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.EachIterCallbackPointer;
    internal static readonly nint ObserveEntityCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.ObserveEntityCallbackPointer;

    #endregion

    #region Run Callbacks

    internal static readonly nint RunCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.RunCallback;

    internal static readonly nint RunCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.RunCallbackDelegate;
    internal static readonly nint RunDelegateCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.RunDelegateCallbackDelegate;
    internal static readonly nint RunPointerCallbackDelegate = (nint)(delegate*<ecs_iter_t*, void>)&Functions.RunPointerCallbackDelegate;

    internal static readonly nint RunCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.RunCallbackPointer;
    internal static readonly nint RunDelegateCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.RunDelegateCallbackPointer;
    internal static readonly nint RunPointerCallbackPointer = (nint)(delegate*<ecs_iter_t*, void>)&Functions.RunPointerCallbackPointer;

    #endregion

    #region Group By Callbacks

    internal static readonly nint GroupByCallback = (nint)(delegate* unmanaged<ecs_world_t*, ecs_table_t*, ulong, GroupByContext*, ulong>)&Functions.GroupByCallback;
    internal static readonly nint GroupByCallbackDelegate = (nint)(delegate*<ecs_world_t*, ecs_table_t*, ulong, GroupByContext*, ulong>)&Functions.GroupByCallbackDelegate;
    internal static readonly nint GroupByCallbackPointer = (nint)(delegate*<ecs_world_t*, ecs_table_t*, ulong, GroupByContext*, ulong>)&Functions.GroupByCallbackPointer;

    internal static readonly nint GroupCreateCallback = (nint)(delegate* unmanaged<ecs_world_t*, ulong, GroupByContext*, void*>)&Functions.GroupCreateCallback;
    internal static readonly nint GroupCreateCallbackDelegate = (nint)(delegate* <ecs_world_t*, ulong, GroupByContext*, void*>)&Functions.GroupCreateCallbackDelegate;
    internal static readonly nint GroupCreateCallbackPointer = (nint)(delegate* <ecs_world_t*, ulong, GroupByContext*, void*>)&Functions.GroupCreateCallbackPointer;

    internal static readonly nint GroupDeleteCallback = (nint)(delegate* unmanaged<ecs_world_t*, ulong, UserContext*, GroupByContext*, void>)&Functions.GroupDeleteCallback;
    internal static readonly nint GroupDeleteCallbackDelegate = (nint)(delegate*<ecs_world_t*, ulong, UserContext*, GroupByContext*, void>)&Functions.GroupDeleteCallbackDelegate;
    internal static readonly nint GroupDeleteCallbackPointer = (nint)(delegate*<ecs_world_t*, ulong, UserContext*, GroupByContext*, void>)&Functions.GroupDeleteCallbackPointer;

    #endregion

    #region Type Hook Callbacks

    internal static readonly nint CtorCallback = (nint)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&Functions.CtorCallback;
    internal static readonly nint DtorCallback = (nint)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&Functions.DtorCallback;
    internal static readonly nint MoveCallback = (nint)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&Functions.MoveCallback;
    internal static readonly nint CopyCallback = (nint)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&Functions.CopyCallback;

    internal static readonly nint OnAddCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.OnAddCallback;
    internal static readonly nint OnSetCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.OnSetCallback;
    internal static readonly nint OnRemoveCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.OnRemoveCallback;

    #endregion

    #region Post Frame Callbacks

    internal static readonly nint PostFrameCallback = (nint)(delegate* unmanaged<void*, void>)&Functions.PostFrameCallback;
    internal static readonly nint PostFrameCallbackDelegate = (nint)(delegate*<void*, void>)&Functions.PostFrameCallbackDelegate;
    internal static readonly nint PostFrameCallbackPointer = (nint)(delegate*<void*, void>)&Functions.PostFrameCallbackPointer;

    #endregion

    #region World Finish Callbacks

    internal static readonly nint WorldFinishCallback = (nint)(delegate* unmanaged<ecs_world_t*, void*, void>)&Functions.WorldFinishCallback;
    internal static readonly nint WorldFinishCallbackDelegate = (nint)(delegate*<ecs_world_t*, void*, void>)&Functions.WorldFinishCallbackDelegate;
    internal static readonly nint WorldFinishCallbackPointer = (nint)(delegate*<ecs_world_t*, void*, void>)&Functions.WorldFinishCallbackPointer;

    #endregion

    #region Os Api Callbacks

    internal static readonly nint AbortCallback = (nint)(delegate* unmanaged<void>)&Functions.AbortCallback;
    internal static readonly nint AbortCallbackDelegate = (nint)(delegate*<void>)&Functions.AbortCallbackDelegate;
    internal static readonly nint AbortCallbackPointer = (nint)(delegate*<void>)&Functions.AbortCallbackPointer;

    internal static readonly nint LogCallback = (nint)(delegate* unmanaged<int, byte*, int, byte*, void>)&Functions.LogCallback;
    internal static readonly nint LogCallbackDelegate = (nint)(delegate*<int, byte*, int, byte*, void>)&Functions.LogCallbackDelegate;
    internal static readonly nint LogCallbackPointer = (nint)(delegate*<int, byte*, int, byte*, void>)&Functions.LogCallbackPointer;

    #endregion

    #region Context Free Callbacks

    internal static readonly nint WorldContextFree = (nint)(delegate* unmanaged<WorldContext*, void>)&Functions.WorldContextFree;
    internal static readonly nint IteratorContextFree = (nint)(delegate* unmanaged<IteratorContext*, void>)&Functions.IteratorContextFree;
    internal static readonly nint RunContextFree = (nint)(delegate* unmanaged<RunContext*, void>)&Functions.RunContextFree;
    internal static readonly nint QueryContextFree = (nint)(delegate* unmanaged<QueryContext*, void>)&Functions.QueryContextFree;
    internal static readonly nint GroupByContextFree = (nint)(delegate* unmanaged<GroupByContext*, void>)&Functions.GroupByContextFree;
    internal static readonly nint TypeHooksContextFree = (nint)(delegate* unmanaged<TypeHooksContext*, void>)&Functions.TypeHooksContextFree;

    #endregion
}

/// <summary>
///     A static class for binding context pointers.
/// </summary>
[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
internal static unsafe partial class Pointers<T0>
{
    #region Group Callbacks

    internal static readonly nint GroupCreateCallbackDelegate = (nint)(delegate*<ecs_world_t*, ulong, GroupByContext*, void*>)&Functions.GroupCreateCallbackDelegate<T0>;
    internal static readonly nint GroupCreateCallbackPointer = (nint)(delegate*<ecs_world_t*, ulong, GroupByContext*, void*>)&Functions.GroupCreateCallbackPointer<T0>;

    internal static readonly nint GroupDeleteCallbackDelegate = (nint)(delegate*<ecs_world_t*, ulong, UserContext*, GroupByContext*, void>)&Functions.GroupDeleteCallbackDelegate<T0>;
    internal static readonly nint GroupDeleteCallbackPointer = (nint)(delegate*<ecs_world_t*, ulong, UserContext*, GroupByContext*, void>)&Functions.GroupDeleteCallbackPointer<T0>;

    #endregion

    #region Observe Callbacks

    internal static readonly nint ObserveRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObserveRefCallbackDelegate<T0>;
    internal static readonly nint ObservePointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObservePointerCallbackDelegate<T0>;
    internal static readonly nint ObserveEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObserveEntityRefCallbackDelegate<T0>;
    internal static readonly nint ObserveEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObserveEntityPointerCallbackDelegate<T0>;

    internal static readonly nint ObserveRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObserveRefCallbackPointer<T0>;
    internal static readonly nint ObservePointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObservePointerCallbackPointer<T0>;
    internal static readonly nint ObserveEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObserveEntityRefCallbackPointer<T0>;
    internal static readonly nint ObserveEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.ObserveEntityPointerCallbackPointer<T0>;

    #endregion

    #region Type Hook Callbacks

    internal static readonly nint DefaultManagedCtorCallback = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedCtorCallback;
    internal static readonly nint DefaultManagedDtorCallback = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedDtorCallback;
    internal static readonly nint DefaultManagedMoveCallback = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedMoveCallback;
    internal static readonly nint DefaultManagedCopyCallback = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.DefaultManagedCopyCallback;

    internal static readonly nint ManagedCtorCallbackDelegate = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCtorCallbackDelegate;
    internal static readonly nint ManagedDtorCallbackDelegate = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedDtorCallbackDelegate;
    internal static readonly nint ManagedMoveCallbackDelegate = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedMoveCallbackDelegate;
    internal static readonly nint ManagedCopyCallbackDelegate = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCopyCallbackDelegate;

    internal static readonly nint ManagedCtorCallbackPointer = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCtorCallbackPointer;
    internal static readonly nint ManagedDtorCallbackPointer = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedDtorCallbackPointer;
    internal static readonly nint ManagedMoveCallbackPointer = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedMoveCallbackPointer;
    internal static readonly nint ManagedCopyCallbackPointer = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions<T0>.ManagedCopyCallbackPointer;

    internal static readonly nint UnmanagedCtorCallbackDelegate = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCtorCallbackDelegate;
    internal static readonly nint UnmanagedDtorCallbackDelegate = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedDtorCallbackDelegate;
    internal static readonly nint UnmanagedMoveCallbackDelegate = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedMoveCallbackDelegate;
    internal static readonly nint UnmanagedCopyCallbackDelegate = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCopyCallbackDelegate;

    internal static readonly nint UnmanagedCtorCallbackPointer = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCtorCallbackPointer;
    internal static readonly nint UnmanagedDtorCallbackPointer = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedDtorCallbackPointer;
    internal static readonly nint UnmanagedMoveCallbackPointer = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedMoveCallbackPointer;
    internal static readonly nint UnmanagedCopyCallbackPointer = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions<T0>.UnmanagedCopyCallbackPointer;

    internal static readonly nint OnAddIterFieldCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterFieldCallbackDelegate;
    internal static readonly nint OnSetIterFieldCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterFieldCallbackDelegate;
    internal static readonly nint OnRemoveIterFieldCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterFieldCallbackDelegate;

    internal static readonly nint OnAddIterFieldCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterFieldCallbackPointer;
    internal static readonly nint OnSetIterFieldCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterFieldCallbackPointer;
    internal static readonly nint OnRemoveIterFieldCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterFieldCallbackPointer;

    internal static readonly nint OnAddIterSpanCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterSpanCallbackDelegate;
    internal static readonly nint OnSetIterSpanCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterSpanCallbackDelegate;
    internal static readonly nint OnRemoveIterSpanCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterSpanCallbackDelegate;

    internal static readonly nint OnAddIterSpanCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterSpanCallbackPointer;
    internal static readonly nint OnSetIterSpanCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterSpanCallbackPointer;
    internal static readonly nint OnRemoveIterSpanCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterSpanCallbackPointer;

    internal static readonly nint OnAddIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterPointerCallbackDelegate;
    internal static readonly nint OnSetIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterPointerCallbackDelegate;
    internal static readonly nint OnRemoveIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterPointerCallbackDelegate;

    internal static readonly nint OnAddIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddIterPointerCallbackPointer;
    internal static readonly nint OnSetIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetIterPointerCallbackPointer;
    internal static readonly nint OnRemoveIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveIterPointerCallbackPointer;

    internal static readonly nint OnAddEachRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachRefCallbackDelegate;
    internal static readonly nint OnSetEachRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachRefCallbackDelegate;
    internal static readonly nint OnRemoveEachRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachRefCallbackDelegate;

    internal static readonly nint OnAddEachRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachRefCallbackPointer;
    internal static readonly nint OnSetEachRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachRefCallbackPointer;
    internal static readonly nint OnRemoveEachRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachRefCallbackPointer;

    internal static readonly nint OnAddEachEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityRefCallbackDelegate;
    internal static readonly nint OnSetEachEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityRefCallbackDelegate;
    internal static readonly nint OnRemoveEachEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityRefCallbackDelegate;

    internal static readonly nint OnAddEachEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityRefCallbackPointer;
    internal static readonly nint OnSetEachEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityRefCallbackPointer;
    internal static readonly nint OnRemoveEachEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityRefCallbackPointer;

    internal static readonly nint OnAddEachIterRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterRefCallbackDelegate;
    internal static readonly nint OnSetEachIterRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterRefCallbackDelegate;
    internal static readonly nint OnRemoveEachIterRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterRefCallbackDelegate;

    internal static readonly nint OnAddEachIterRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterRefCallbackPointer;
    internal static readonly nint OnSetEachIterRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterRefCallbackPointer;
    internal static readonly nint OnRemoveEachIterRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterRefCallbackPointer;

    internal static readonly nint OnAddEachPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachPointerCallbackDelegate;
    internal static readonly nint OnSetEachPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachPointerCallbackDelegate;
    internal static readonly nint OnRemoveEachPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachPointerCallbackDelegate;

    internal static readonly nint OnAddEachPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachPointerCallbackPointer;
    internal static readonly nint OnSetEachPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachPointerCallbackPointer;
    internal static readonly nint OnRemoveEachPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachPointerCallbackPointer;

    internal static readonly nint OnAddEachEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityPointerCallbackDelegate;
    internal static readonly nint OnSetEachEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityPointerCallbackDelegate;
    internal static readonly nint OnRemoveEachEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityPointerCallbackDelegate;

    internal static readonly nint OnAddEachEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachEntityPointerCallbackPointer;
    internal static readonly nint OnSetEachEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachEntityPointerCallbackPointer;
    internal static readonly nint OnRemoveEachEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachEntityPointerCallbackPointer;

    internal static readonly nint OnAddEachIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterPointerCallbackDelegate;
    internal static readonly nint OnSetEachIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterPointerCallbackDelegate;
    internal static readonly nint OnRemoveEachIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterPointerCallbackDelegate;

    internal static readonly nint OnAddEachIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnAddEachIterPointerCallbackPointer;
    internal static readonly nint OnSetEachIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnSetEachIterPointerCallbackPointer;
    internal static readonly nint OnRemoveEachIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions<T0>.OnRemoveEachIterPointerCallbackPointer;

    #endregion
}
