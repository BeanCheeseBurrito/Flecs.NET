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

    #region Context Free Callbacks

    internal static readonly nint WorldContextFree = (nint)(delegate* unmanaged<WorldContext*, void>)&Functions.WorldContextFree;
    internal static readonly nint IteratorContextFree = (nint)(delegate* unmanaged<IteratorContext*, void>)&Functions.IteratorContextFree;
    internal static readonly nint RunContextFree = (nint)(delegate* unmanaged<RunContext*, void>)&Functions.RunContextFree;
    internal static readonly nint QueryContextFree = (nint)(delegate* unmanaged<QueryContext*, void>)&Functions.QueryContextFree;
    internal static readonly nint GroupByContextFree = (nint)(delegate* unmanaged<GroupByContext*, void>)&Functions.GroupByContextFree;
    internal static readonly nint TypeHooksContextFree = (nint)(delegate* unmanaged<TypeHooksContext*, void>)&Functions.TypeHooksContextFree;

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

    #region Post Frame Callbacks

    internal static readonly nint PostFrameCallback = (nint)(delegate* unmanaged<ecs_world_t*, void*, void>)&Functions.PostFrameCallback;
    internal static readonly nint PostFrameCallbackDelegate = (nint)(delegate*<ecs_world_t*, void*, void>)&Functions.PostFrameCallbackDelegate;
    internal static readonly nint PostFrameCallbackPointer = (nint)(delegate*<ecs_world_t*, void*, void>)&Functions.PostFrameCallbackPointer;

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

    #region Type Hook Callbacks

    internal static readonly nint CtorCallback = (nint)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&Functions.CtorCallback;
    internal static readonly nint DtorCallback = (nint)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&Functions.DtorCallback;
    internal static readonly nint MoveCallback = (nint)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&Functions.MoveCallback;
    internal static readonly nint CopyCallback = (nint)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&Functions.CopyCallback;

    internal static readonly nint OnAddCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.OnAddCallback;
    internal static readonly nint OnSetCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.OnSetCallback;
    internal static readonly nint OnRemoveCallback = (nint)(delegate* unmanaged<ecs_iter_t*, void>)&Functions.OnRemoveCallback;

    #endregion
}

/// <summary>
///     A static class for binding context pointers.
/// </summary>
[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
internal static unsafe partial class Pointers<T0>
{
    #region Context Free Callbacks

    internal static readonly nint UserContextFinishDelegate = (nint)(delegate*<ref UserContext, void>)&Functions.UserContextFinishDelegate<T0>;
    internal static readonly nint UserContextFinishPointer = (nint)(delegate*<ref UserContext, void>)&Functions.UserContextFinishPointer<T0>;

    #endregion

    #region Group Callbacks

    internal static readonly nint GroupByCallbackDelegate = (nint)(delegate*<ecs_world_t*, ecs_table_t*, ulong, GroupByContext*, ulong>)&Functions.GroupByCallbackDelegate<T0>;
    internal static readonly nint GroupByCallbackPointer = (nint)(delegate*<ecs_world_t*, ecs_table_t*, ulong, GroupByContext*, ulong>)&Functions.GroupByCallbackPointer<T0>;

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

    #region Resource Hook Callbacks

    internal static readonly nint DefaultManagedCtorCallback = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedCtorCallback<T0>;
    internal static readonly nint DefaultManagedDtorCallback = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedDtorCallback<T0>;
    internal static readonly nint DefaultManagedMoveCallback = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedMoveCallback<T0>;
    internal static readonly nint DefaultManagedCopyCallback = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedCopyCallback<T0>;

    internal static readonly nint ManagedCtorCallbackDelegate = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCtorCallbackDelegate<T0>;
    internal static readonly nint ManagedDtorCallbackDelegate = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedDtorCallbackDelegate<T0>;
    internal static readonly nint ManagedMoveCallbackDelegate = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedMoveCallbackDelegate<T0>;
    internal static readonly nint ManagedCopyCallbackDelegate = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCopyCallbackDelegate<T0>;

    internal static readonly nint ManagedCtorCallbackPointer = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCtorCallbackPointer<T0>;
    internal static readonly nint ManagedDtorCallbackPointer = (nint)(delegate* <GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedDtorCallbackPointer<T0>;
    internal static readonly nint ManagedMoveCallbackPointer = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedMoveCallbackPointer<T0>;
    internal static readonly nint ManagedCopyCallbackPointer = (nint)(delegate* <GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCopyCallbackPointer<T0>;

    internal static readonly nint UnmanagedCtorCallbackDelegate = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCtorCallbackDelegate<T0>;
    internal static readonly nint UnmanagedDtorCallbackDelegate = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedDtorCallbackDelegate<T0>;
    internal static readonly nint UnmanagedMoveCallbackDelegate = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedMoveCallbackDelegate<T0>;
    internal static readonly nint UnmanagedCopyCallbackDelegate = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCopyCallbackDelegate<T0>;

    internal static readonly nint UnmanagedCtorCallbackPointer = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCtorCallbackPointer<T0>;
    internal static readonly nint UnmanagedDtorCallbackPointer = (nint)(delegate* <T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedDtorCallbackPointer<T0>;
    internal static readonly nint UnmanagedMoveCallbackPointer = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedMoveCallbackPointer<T0>;
    internal static readonly nint UnmanagedCopyCallbackPointer = (nint)(delegate* <T0*, T0*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCopyCallbackPointer<T0>;

    #endregion

    #region OnAdd Hook Callbacks

    internal static readonly nint OnAddIterFieldCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddIterFieldCallbackDelegate<T0>;
    internal static readonly nint OnAddIterSpanCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddIterSpanCallbackDelegate<T0>;
    internal static readonly nint OnAddIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddIterPointerCallbackDelegate<T0>;
    internal static readonly nint OnAddEachRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachRefCallbackDelegate<T0>;
    internal static readonly nint OnAddEachEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachEntityRefCallbackDelegate<T0>;
    internal static readonly nint OnAddEachIterRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachIterRefCallbackDelegate<T0>;
    internal static readonly nint OnAddEachPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachPointerCallbackDelegate<T0>;
    internal static readonly nint OnAddEachEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachEntityPointerCallbackDelegate<T0>;
    internal static readonly nint OnAddEachIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachIterPointerCallbackDelegate<T0>;

    internal static readonly nint OnAddIterFieldCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddIterFieldCallbackPointer<T0>;
    internal static readonly nint OnAddIterSpanCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddIterSpanCallbackPointer<T0>;
    internal static readonly nint OnAddIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddIterPointerCallbackPointer<T0>;
    internal static readonly nint OnAddEachRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachRefCallbackPointer<T0>;
    internal static readonly nint OnAddEachEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachEntityRefCallbackPointer<T0>;
    internal static readonly nint OnAddEachIterRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachIterRefCallbackPointer<T0>;
    internal static readonly nint OnAddEachPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachPointerCallbackPointer<T0>;
    internal static readonly nint OnAddEachEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachEntityPointerCallbackPointer<T0>;
    internal static readonly nint OnAddEachIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnAddEachIterPointerCallbackPointer<T0>;

    #endregion

    #region OnSet Hook Callbacks

    internal static readonly nint OnSetIterFieldCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetIterFieldCallbackDelegate<T0>;
    internal static readonly nint OnSetIterSpanCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetIterSpanCallbackDelegate<T0>;
    internal static readonly nint OnSetIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetIterPointerCallbackDelegate<T0>;
    internal static readonly nint OnSetEachRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachRefCallbackDelegate<T0>;
    internal static readonly nint OnSetEachEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachEntityRefCallbackDelegate<T0>;
    internal static readonly nint OnSetEachIterRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachIterRefCallbackDelegate<T0>;
    internal static readonly nint OnSetEachPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachPointerCallbackDelegate<T0>;
    internal static readonly nint OnSetEachEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachEntityPointerCallbackDelegate<T0>;
    internal static readonly nint OnSetEachIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachIterPointerCallbackDelegate<T0>;

    internal static readonly nint OnSetIterFieldCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetIterFieldCallbackPointer<T0>;
    internal static readonly nint OnSetIterSpanCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetIterSpanCallbackPointer<T0>;
    internal static readonly nint OnSetIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetIterPointerCallbackPointer<T0>;
    internal static readonly nint OnSetEachRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachRefCallbackPointer<T0>;
    internal static readonly nint OnSetEachEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachEntityRefCallbackPointer<T0>;
    internal static readonly nint OnSetEachIterRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachIterRefCallbackPointer<T0>;
    internal static readonly nint OnSetEachPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachPointerCallbackPointer<T0>;
    internal static readonly nint OnSetEachEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachEntityPointerCallbackPointer<T0>;
    internal static readonly nint OnSetEachIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnSetEachIterPointerCallbackPointer<T0>;

    #endregion

    #region OnRemove Hook Callbacks

    internal static readonly nint OnRemoveIterFieldCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveIterFieldCallbackDelegate<T0>;
    internal static readonly nint OnRemoveIterSpanCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveIterSpanCallbackDelegate<T0>;
    internal static readonly nint OnRemoveIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveIterPointerCallbackDelegate<T0>;
    internal static readonly nint OnRemoveEachRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachRefCallbackDelegate<T0>;
    internal static readonly nint OnRemoveEachEntityRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachEntityRefCallbackDelegate<T0>;
    internal static readonly nint OnRemoveEachIterRefCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachIterRefCallbackDelegate<T0>;
    internal static readonly nint OnRemoveEachPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachPointerCallbackDelegate<T0>;
    internal static readonly nint OnRemoveEachEntityPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachEntityPointerCallbackDelegate<T0>;
    internal static readonly nint OnRemoveEachIterPointerCallbackDelegate = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachIterPointerCallbackDelegate<T0>;

    internal static readonly nint OnRemoveIterFieldCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveIterFieldCallbackPointer<T0>;
    internal static readonly nint OnRemoveIterSpanCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveIterSpanCallbackPointer<T0>;
    internal static readonly nint OnRemoveIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveIterPointerCallbackPointer<T0>;
    internal static readonly nint OnRemoveEachRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachRefCallbackPointer<T0>;
    internal static readonly nint OnRemoveEachEntityRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachEntityRefCallbackPointer<T0>;
    internal static readonly nint OnRemoveEachIterRefCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachIterRefCallbackPointer<T0>;
    internal static readonly nint OnRemoveEachPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachPointerCallbackPointer<T0>;
    internal static readonly nint OnRemoveEachEntityPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachEntityPointerCallbackPointer<T0>;
    internal static readonly nint OnRemoveEachIterPointerCallbackPointer = (nint)(delegate* <ecs_iter_t*, void>)&Functions.OnRemoveEachIterPointerCallbackPointer<T0>;

    #endregion
}
