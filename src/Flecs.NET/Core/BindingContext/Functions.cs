using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core.BindingContext;

/// <summary>
///     A static class for binding context functions.
/// </summary>
internal static unsafe class Functions
{
    #region  Context Free

    [UnmanagedCallersOnly]
    internal static void WorldContextFree(WorldContext* context)
    {
        context->Dispose();
        Memory.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void IteratorContextFree(IteratorContext* context)
    {
        context->Dispose();
        Memory.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void RunContextFree(RunContext* context)
    {
        context->Dispose();
        Memory.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void QueryContextFree(QueryContext* context)
    {
        context->Dispose();
        Memory.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void GroupByContextFree(GroupByContext* context)
    {
        context->Dispose();
        Memory.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void TypeHooksContextFree(TypeHooksContext* context)
    {
        context->Dispose();
        Memory.Free(context);
    }

    #endregion

    #region Iterator Callbacks

    [UnmanagedCallersOnly]
    internal static void IteratorCallback(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->Callback.Invoker)(iter);
    }

    internal static void ActionCallbackDelegate(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        ((Action)context->Callback.Delegate.Target!)();
    }

    internal static void ActionCallbackPointer(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        ((delegate*<void>)context->Callback.Pointer)();
    }

    internal static void IterCallbackDelegate(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterCallback)context->Callback.Delegate.Target!);
    }

    internal static void IterCallbackPointer(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, void>)context->Callback.Pointer);
    }

    internal static void EachEntityCallbackDelegate(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityCallback)context->Callback.Delegate.Target!);
    }

    internal static void EachEntityCallbackPointer(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, void>)context->Callback.Pointer);
    }

    internal static void EachIterCallbackDelegate(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterCallback)context->Callback.Delegate.Target!);
    }

    internal static void EachIterCallbackPointer(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, void>)context->Callback.Pointer);
    }

    internal static void ObserveEntityCallbackDelegate(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (Ecs.ObserveEntityCallback)context->Callback.Delegate.Target!);
    }

    internal static void ObserveEntityCallbackPointer(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (delegate*<Entity, void>)context->Callback.Pointer);
    }

    internal static void ObserveRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (Ecs.ObserveRefCallback<T>)context->Callback.Delegate.Target!);
    }

    internal static void ObserveRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (delegate*<ref T, void>)context->Callback.Pointer);
    }

    internal static void ObservePointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (Ecs.ObservePointerCallback<T>)context->Callback.Delegate.Target!);
    }

    internal static void ObservePointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (delegate*<T*, void>)context->Callback.Pointer);
    }

    internal static void ObserveEntityRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (Ecs.ObserveEntityRefCallback<T>)context->Callback.Delegate.Target!);
    }

    internal static void ObserveEntityRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (delegate*<Entity, ref T, void>)context->Callback.Pointer);
    }

    internal static void ObserveEntityPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (Ecs.ObserveEntityPointerCallback<T>)context->Callback.Delegate.Target!);
    }

    internal static void ObserveEntityPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Observe(iter, (delegate*<Entity, T*, void>)context->Callback.Pointer);
    }

    #endregion

    #region Run Callbacks

    [UnmanagedCallersOnly]
    internal static void RunCallback(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        ((delegate*<ecs_iter_t*, void>)context->Callback.Invoker)(iter);
    }

    internal static void RunCallbackDelegate(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (Ecs.RunCallback)context->Callback.Delegate.Target!);
    }

    internal static void RunCallbackPointer(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (delegate*<Iter, void>)context->Callback.Pointer);
    }

    internal static void RunDelegateCallbackDelegate(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (Ecs.RunDelegateCallback)context->Callback.Delegate.Target!);
    }

    internal static void RunDelegateCallbackPointer(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (delegate*<Iter, Action<Iter>, void>)context->Callback.Pointer);
    }

    internal static void RunPointerCallbackDelegate(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (Ecs.RunPointerCallback)context->Callback.Delegate.Target!);
    }

    internal static void RunPointerCallbackPointer(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (delegate*<Iter, delegate*<Iter, void>, void>)context->Callback.Pointer);
    }

    #endregion

    #region Group By Callbacks

    [UnmanagedCallersOnly]
    internal static ulong GroupByCallback(ecs_world_t* world, ecs_table_t* table, ulong id, GroupByContext* context)
    {
        return ((delegate*<ecs_world_t*, ecs_table_t*, ulong, GroupByContext*, ulong>)context->GroupBy.Invoker)(world, table, id, context);
    }

    internal static ulong GroupByCallbackDelegate(ecs_world_t* world, ecs_table_t* table, ulong id, GroupByContext* context)
    {
        Ecs.GroupByCallback callback = (Ecs.GroupByCallback)context->GroupBy.Delegate.Target!;
        return callback(new World(world), new Table(world, table), id);
    }

    internal static ulong GroupByCallbackPointer(ecs_world_t* world, ecs_table_t* table, ulong id, GroupByContext* context)
    {
        delegate*<World, Table, ulong, ulong> callback = (delegate*<World, Table, ulong, ulong>)context->GroupBy.Pointer;
        return callback(new World(world), new Table(world, table), id);
    }

    #endregion

    #region Group Create Callbacks

    [UnmanagedCallersOnly]
    internal static void* GroupCreateCallback(ecs_world_t* world, ulong id, GroupByContext* context)
    {
        return ((delegate*<ecs_world_t*, ulong, GroupByContext*, void*>)context->GroupCreate.Invoker)(world, id, context);
    }

    internal static void* GroupCreateCallbackDelegate(ecs_world_t* world, ulong id, GroupByContext* context)
    {
        ((Ecs.GroupCreateCallback)context->GroupCreate.Delegate.Target!)(world, id);
        return null;
    }

    internal static void* GroupCreateCallbackPointer(ecs_world_t* world, ulong id, GroupByContext* context)
    {
        ((delegate*<World, ulong, void>)context->GroupCreate.Pointer)(world, id);
        return null;
    }

    internal static void* GroupCreateCallbackDelegate<T>(ecs_world_t* world, ulong id, GroupByContext* context)
    {
        ((Ecs.GroupCreateCallback<T>)context->GroupCreate.Delegate.Target!)(world, id, out T userContext);
        return UserContext.Alloc(ref userContext);
    }

    internal static void* GroupCreateCallbackPointer<T>(ecs_world_t* world, ulong id, GroupByContext* context)
    {
        ((delegate*<World, ulong, out T, void>)context->GroupCreate.Pointer)(world, id, out T userContext);
        return UserContext.Alloc(ref userContext);
    }

    #endregion

    #region Group Delete Callbacks

    [UnmanagedCallersOnly]
    internal static void GroupDeleteCallback(ecs_world_t* world, ulong id, UserContext* userContext, GroupByContext* context)
    {
        ((delegate*<ecs_world_t*, ulong, UserContext*, GroupByContext*, void>)context->GroupDelete.Invoker)(world, id, userContext, context);
    }

    internal static void GroupDeleteCallbackDelegate(ecs_world_t* world, ulong id, UserContext* userContext, GroupByContext* context)
    {
        ((Ecs.GroupDeleteCallback)context->GroupDelete.Delegate.Target!)(world, id);

        if (userContext == null)
            return;

        userContext->Dispose();
        Memory.Free(userContext);
    }

    internal static void GroupDeleteCallbackPointer(ecs_world_t* world, ulong id, UserContext* userContext, GroupByContext* context)
    {
        ((delegate*<World, ulong, void>)context->GroupDelete.Pointer)(world, id);

        if (userContext == null)
            return;

        userContext->Dispose();
        Memory.Free(userContext);
    }

    internal static void GroupDeleteCallbackDelegate<T>(ecs_world_t* world, ulong id, UserContext* userContext, GroupByContext* context)
    {
        ((Ecs.GroupDeleteCallback<T>)context->GroupDelete.Delegate.Target!)(world, id, ref userContext->Get<T>());

        if (userContext == null)
            return;

        userContext->Dispose();
        Memory.Free(userContext);
    }

    internal static void GroupDeleteCallbackPointer<T>(ecs_world_t* world, ulong id, UserContext* userContext, GroupByContext* context)
    {
        ((delegate*<World, ulong, ref T, void>)context->GroupDelete.Pointer)(world, id, ref userContext->Get<T>());

        if (userContext == null)
            return;

        userContext->Dispose();
        Memory.Free(userContext);
    }

    #endregion

    #region Type Hook Callbacks

    [UnmanagedCallersOnly]
    internal static void CtorCallback(void* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        ((delegate*<void*, int, ecs_type_info_t*, void>)context->Ctor.Invoker)(data, count, typeInfo);
    }

    [UnmanagedCallersOnly]
    internal static void DtorCallback(void* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        ((delegate*<void*, int, ecs_type_info_t*, void>)context->Dtor.Invoker)(data, count, typeInfo);
    }

    [UnmanagedCallersOnly]
    internal static void MoveCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        ((delegate*<void*, void*, int, ecs_type_info_t*, void>)context->Move.Invoker)(dst, src, count, typeInfo);
    }

    [UnmanagedCallersOnly]
    internal static void CopyCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        ((delegate*<void*, void*, int, ecs_type_info_t*, void>)context->Copy.Invoker)(dst, src, count, typeInfo);
    }

    [UnmanagedCallersOnly]
    internal static void OnAddCallback(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->OnAdd.Invoker)(iter);
    }

    [UnmanagedCallersOnly]
    internal static void OnSetCallback(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->OnSet.Invoker)(iter);
    }

    [UnmanagedCallersOnly]
    internal static void OnRemoveCallback(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->OnRemove.Invoker)(iter);
    }

    #endregion

    #region Post Frame Callbacks

    [UnmanagedCallersOnly]
    internal static void PostFrameCallback(void* ctx)
    {
        PostFrameContext* context = (PostFrameContext*)ctx;
        ((delegate*<void*, void>)context->Callback.Invoker)(ctx);
    }

    internal static void PostFrameCallbackDelegate(void* ctx)
    {
        PostFrameContext* context = (PostFrameContext*)ctx;
        ((Ecs.PostFrameCallback)context->Callback.Delegate.Target!)();
    }

    internal static void PostFrameCallbackPointer(void* ctx)
    {
        PostFrameContext* context = (PostFrameContext*)ctx;
        ((delegate*<void>)context->Callback.Pointer)();
    }

    #endregion

    #region World Finish Callback

    [UnmanagedCallersOnly]
    internal static void WorldFinishCallback(ecs_world_t* world, void* ctx)
    {
        WorldFinishContext* context = (WorldFinishContext*)ctx;
        ((delegate*<ecs_world_t*, void*, void>)context->Callback.Invoker)(world, ctx);
    }

    internal static void WorldFinishCallbackDelegate(ecs_world_t* world, void* ctx)
    {
        WorldFinishContext* context = (WorldFinishContext*)ctx;
        ((Ecs.WorldFinishCallback)context->Callback.Delegate.Target!)(world);
    }

    internal static void WorldFinishCallbackPointer(ecs_world_t* world, void* ctx)
    {
        WorldFinishContext* context = (WorldFinishContext*)ctx;
        ((delegate*<World, void>)context->Callback.Pointer)(world);
    }

    #endregion

    #region Os Api

    [UnmanagedCallersOnly]
    internal static void AbortCallback()
    {
        ((delegate*<void>)Ecs.Os.Context.Abort.Invoker)();
    }

    internal static void AbortCallbackDelegate()
    {
        ((Action)Ecs.Os.Context.Abort.Delegate.Target!)();
    }

    internal static void AbortCallbackPointer()
    {
        ((delegate*<void>)Ecs.Os.Context.Abort.Pointer)();
    }

    [UnmanagedCallersOnly]
    internal static void LogCallback(int level, byte* file, int line, byte* message)
    {
        ((delegate*<int, byte*, int, byte*, void>)Ecs.Os.Context.Log.Invoker)(level, file, line, message);
    }

    internal static void LogCallbackDelegate(int level, byte* file, int line, byte* message)
    {
        ((Ecs.LogCallback)Ecs.Os.Context.Log.Delegate.Target!)(level, NativeString.GetString(file), line, NativeString.GetString(message));
    }

    internal static void LogCallbackPointer(int level, byte* file, int line, byte* message)
    {
        ((delegate*<int, string, int, string, void>)Ecs.Os.Context.Log.Pointer)(level, NativeString.GetString(file), line, NativeString.GetString(message));
    }

    #endregion
}

internal static unsafe partial class Functions<T0>
{
    internal static void DefaultManagedCtorCallback(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
            data[i] = GCHandle.Alloc(new Box<T0>());
    }

    internal static void DefaultManagedDtorCallback(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
        {
            Managed.FreeGcHandle(data[i]);
            data[i] = default;
        }
    }

    internal static void DefaultManagedMoveCallback(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
        {
            Box<T0> dstBox = (Box<T0>)dst[i].Target!;
            Box<T0> srcBox = (Box<T0>)src[i].Target!;

            dstBox.Value = srcBox.Value!;

            // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
            if (srcBox.ShouldFree)
                Managed.FreeGcHandle(src[i]);
        }
    }

    internal static void DefaultManagedCopyCallback(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
        {
            Box<T0> dstBox = (Box<T0>)dst[i].Target!;
            Box<T0> srcBox = (Box<T0>)src[i].Target!;

            dstBox.Value = srcBox.Value!;

            // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
            if (srcBox.ShouldFree)
                Managed.FreeGcHandle(src[i]);
        }
    }

    internal static void UnmanagedCtorCallbackDelegate(T0* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CtorCallback<T0> callback = (Ecs.CtorCallback<T0>)context->Ctor.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedCtorCallbackPointer(T0* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Ctor.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedDtorCallbackDelegate(T0* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.DtorCallback<T0> callback = (Ecs.DtorCallback<T0>)context->Dtor.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedDtorCallbackPointer(T0* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Dtor.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedMoveCallbackDelegate(T0* dst, T0* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.MoveCallback<T0> callback = (Ecs.MoveCallback<T0>)context->Move.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedMoveCallbackPointer(T0* dst, T0* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Move.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedCopyCallbackDelegate(T0* dst, T0* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CopyCallback<T0> callback = (Ecs.CopyCallback<T0>)context->Copy.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedCopyCallbackPointer(T0* dst, T0* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Copy.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void ManagedCtorCallbackDelegate(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CtorCallback<T0> callback = (Ecs.CtorCallback<T0>)context->Ctor.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            Box<T0> box = new();
            callback(ref box.Value!, new TypeInfo(typeInfo));
            data[i] = GCHandle.Alloc(box);
        }
    }

    internal static void ManagedCtorCallbackPointer(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Ctor.Pointer;

        for (int i = 0; i < count; i++)
        {
            Box<T0> box = new();
            callback(ref box.Value!, new TypeInfo(typeInfo));
            data[i] = GCHandle.Alloc(box);
        }
    }

    internal static void ManagedDtorCallbackDelegate(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.DtorCallback<T0> callback = (Ecs.DtorCallback<T0>)context->Dtor.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            Box<T0> box = (Box<T0>)data[i].Target!;
            callback(ref box.Value!, new TypeInfo(typeInfo));
            Managed.FreeGcHandle(data[i]);
            data[i] = default;
        }
    }

    internal static void ManagedDtorCallbackPointer(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Dtor.Pointer;

        for (int i = 0; i < count; i++)
        {
            Box<T0> box = (Box<T0>)data[i].Target!;
            callback(ref box.Value!, new TypeInfo(typeInfo));
            Managed.FreeGcHandle(data[i]);
            data[i] = default;
        }
    }

    internal static void ManagedMoveCallbackDelegate(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.MoveCallback<T0> callback = (Ecs.MoveCallback<T0>)context->Move.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            Box<T0> dstBox = (Box<T0>)dst[i].Target!;
            Box<T0> srcBox = (Box<T0>)src[i].Target!;

            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));

            // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
            if (srcBox.ShouldFree)
                Managed.FreeGcHandle(src[i]);
        }
    }

    internal static void ManagedMoveCallbackPointer(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Move.Pointer;

        for (int i = 0; i < count; i++)
        {
            Box<T0> dstBox = (Box<T0>)dst[i].Target!;
            Box<T0> srcBox = (Box<T0>)src[i].Target!;

            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));

            // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
            if (srcBox.ShouldFree)
                Managed.FreeGcHandle(src[i]);
        }
    }

    internal static void ManagedCopyCallbackDelegate(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CopyCallback<T0> callback = (Ecs.CopyCallback<T0>)context->Copy.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            Box<T0> dstBox = (Box<T0>)dst[i].Target!;
            Box<T0> srcBox = (Box<T0>)src[i].Target!;

            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));

            // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
            if (srcBox.ShouldFree)
                Managed.FreeGcHandle(src[i]);
        }
    }

    internal static void ManagedCopyCallbackPointer(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Copy.Pointer;

        for (int i = 0; i < count; i++)
        {
            Box<T0> dstBox = (Box<T0>)dst[i].Target!;
            Box<T0> srcBox = (Box<T0>)src[i].Target!;

            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));

            // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
            if (srcBox.ShouldFree)
                Managed.FreeGcHandle(src[i]);
        }
    }

    internal static void OnAddIterFieldCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddIterFieldCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T0>, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddIterSpanCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddIterSpanCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T0>, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddIterPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddIterPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T0*, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T0>)context->OnAdd.Delegate.Target!);

    }

    internal static void OnAddEachRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T0, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachEntityRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachEntityRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T0, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachIterRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachIterRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T0, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T0*, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachEntityPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachEntityPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T0*, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachIterPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T0>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachIterPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T0*, void>)context->OnAdd.Pointer);
    }

    internal static void OnSetIterFieldCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetIterFieldCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T0>, void>)context->OnSet.Pointer);
    }

    internal static void OnSetIterSpanCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetIterSpanCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T0>, void>)context->OnSet.Pointer);
    }

    internal static void OnSetIterPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetIterPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T0*, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T0, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachEntityRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachEntityRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T0, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachIterRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachIterRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T0, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T0*, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachEntityPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachEntityPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T0*, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachIterPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T0>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachIterPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T0*, void>)context->OnSet.Pointer);
    }

    internal static void OnRemoveIterFieldCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveIterFieldCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T0>, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveIterSpanCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveIterSpanCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T0>, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveIterPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveIterPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T0*, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T0, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachEntityRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachEntityRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T0, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachIterRefCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachIterRefCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T0, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T0*, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachEntityPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachEntityPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T0*, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachIterPointerCallbackDelegate(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T0>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachIterPointerCallbackPointer(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T0*, void>)context->OnRemove.Pointer);
    }
}
