using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core.BindingContext;

/// <summary>
///     A static class for binding context functions.
/// </summary>
internal static unsafe class Functions
{
    #region Context Free

    [UnmanagedCallersOnly]
    internal static void WorldContextFree(WorldContext* context)
    {
        WorldContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void IteratorContextFree(IteratorContext* context)
    {
        IteratorContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void RunContextFree(RunContext* context)
    {
        RunContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void QueryContextFree(QueryContext* context)
    {
        QueryContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void GroupByContextFree(GroupByContext* context)
    {
        GroupByContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void SystemContextFree(SystemContext* context)
    {
        SystemContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void ObserverContextFree(ObserverContext* context)
    {
        ObserverContext.Free(context);
    }

    [UnmanagedCallersOnly]
    internal static void TypeHooksContextFree(TypeHooksContext* context)
    {
        TypeHooksContext.Free(context);
    }

    internal static void UserContextFinishDelegate<T>(ref UserContext context)
    {
        ((Ecs.UserContextFinish<T>)context.Callback.Delegate.Target!)(ref context.Get<T>());
    }

    internal static void UserContextFinishPointer<T>(ref UserContext context)
    {
        ((delegate*<ref T, void>)context.Callback.Pointer)(ref context.Get<T>());
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
        return ((Ecs.GroupByCallback)context->GroupBy.Delegate.Target!)(world, new Table(world, table), id);
    }

    internal static ulong GroupByCallbackPointer(ecs_world_t* world, ecs_table_t* table, ulong id, GroupByContext* context)
    {
        return ((delegate*<World, Table, ulong, ulong>)context->GroupBy.Pointer)(world, new Table(world, table), id);
    }

    internal static ulong GroupByCallbackDelegate<T>(ecs_world_t* world, ecs_table_t* table, ulong id, GroupByContext* context)
    {
        return ((Ecs.GroupByCallback<T>)context->GroupBy.Delegate.Target!)(world, new Table(world, table), id, ref context->GroupByUserContext.Get<T>());
    }

    internal static ulong GroupByCallbackPointer<T>(ecs_world_t* world, ecs_table_t* table, ulong id, GroupByContext* context)
    {
        return ((delegate*<World, Table, ulong, ref T, ulong>)context->GroupBy.Pointer)(world, new Table(world, table), id, ref context->GroupByUserContext.Get<T>());
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
        ((Ecs.GroupCreateCallback<T>)context->GroupCreate.Delegate.Target!)(world, id, out T groupContext);
        return UserContext.Alloc(ref groupContext);
    }

    internal static void* GroupCreateCallbackPointer<T>(ecs_world_t* world, ulong id, GroupByContext* context)
    {
        ((delegate*<World, ulong, out T, void>)context->GroupCreate.Pointer)(world, id, out T groupContext);
        return UserContext.Alloc(ref groupContext);
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

    #region Post Frame Callbacks

    [UnmanagedCallersOnly]
    internal static void PostFrameCallback(ecs_world_t* world, void* ctx)
    {
        PostFrameContext* context = (PostFrameContext*)ctx;
        ((delegate*<ecs_world_t*, void*, void>)context->Callback.Invoker)(world, ctx);
    }

    internal static void PostFrameCallbackDelegate(ecs_world_t* world, void* ctx)
    {
        PostFrameContext* context = (PostFrameContext*)ctx;
        ((Ecs.PostFrameCallback)context->Callback.Delegate.Target!)(world);
    }

    internal static void PostFrameCallbackPointer(ecs_world_t* world, void* ctx)
    {
        PostFrameContext* context = (PostFrameContext*)ctx;
        ((delegate*<World, void>)context->Callback.Pointer)(world);
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

    #region Os Api Callbacks

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

    #region Resource Hook Callbacks

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

    internal static void DefaultManagedCtorCallback<T>(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
            data[i] = GCHandle.Alloc(new StrongBox<T>());
    }

    internal static void DefaultManagedDtorCallback<T>(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
            data[i].Free();
    }

    internal static void DefaultManagedMoveCallback<T>(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
        {
            StrongBox<T> dstBox = (StrongBox<T>)dst[i].Target!;
            StrongBox<T> srcBox = (StrongBox<T>)src[i].Target!;
            dstBox.Value = srcBox.Value!;
        }
    }

    internal static void DefaultManagedCopyCallback<T>(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        for (int i = 0; i < count; i++)
        {
            StrongBox<T> dstBox = (StrongBox<T>)dst[i].Target!;
            StrongBox<T> srcBox = (StrongBox<T>)src[i].Target!;
            dstBox.Value = srcBox.Value!;
        }
    }

    internal static void ManagedCtorCallbackDelegate<T>(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CtorCallback<T> callback = (Ecs.CtorCallback<T>)context->Ctor.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> box = new();
            callback(ref box.Value!, new TypeInfo(typeInfo));
            data[i] = GCHandle.Alloc(box);
        }
    }

    internal static void ManagedCtorCallbackPointer<T>(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, TypeInfo, void> callback = (delegate*<ref T, TypeInfo, void>)context->Ctor.Pointer;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> box = new();
            callback(ref box.Value!, new TypeInfo(typeInfo));
            data[i] = GCHandle.Alloc(box);
        }
    }

    internal static void ManagedDtorCallbackDelegate<T>(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.DtorCallback<T> callback = (Ecs.DtorCallback<T>)context->Dtor.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> box = (StrongBox<T>)data[i].Target!;
            callback(ref box.Value!, new TypeInfo(typeInfo));
            data[i].Free();
        }
    }

    internal static void ManagedDtorCallbackPointer<T>(GCHandle* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, TypeInfo, void> callback = (delegate*<ref T, TypeInfo, void>)context->Dtor.Pointer;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> box = (StrongBox<T>)data[i].Target!;
            callback(ref box.Value!, new TypeInfo(typeInfo));
            data[i].Free();
        }
    }

    internal static void ManagedMoveCallbackDelegate<T>(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.MoveCallback<T> callback = (Ecs.MoveCallback<T>)context->Move.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> dstBox = (StrongBox<T>)dst[i].Target!;
            StrongBox<T> srcBox = (StrongBox<T>)src[i].Target!;
            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));
        }
    }

    internal static void ManagedMoveCallbackPointer<T>(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, ref T, TypeInfo, void> callback = (delegate*<ref T, ref T, TypeInfo, void>)context->Move.Pointer;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> dstBox = (StrongBox<T>)dst[i].Target!;
            StrongBox<T> srcBox = (StrongBox<T>)src[i].Target!;
            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));
        }
    }

    internal static void ManagedCopyCallbackDelegate<T>(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CopyCallback<T> callback = (Ecs.CopyCallback<T>)context->Copy.Delegate.Target!;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> dstBox = (StrongBox<T>)dst[i].Target!;
            StrongBox<T> srcBox = (StrongBox<T>)src[i].Target!;
            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));
        }
    }

    internal static void ManagedCopyCallbackPointer<T>(GCHandle* dst, GCHandle* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, ref T, TypeInfo, void> callback = (delegate*<ref T, ref T, TypeInfo, void>)context->Copy.Pointer;

        for (int i = 0; i < count; i++)
        {
            StrongBox<T> dstBox = (StrongBox<T>)dst[i].Target!;
            StrongBox<T> srcBox = (StrongBox<T>)src[i].Target!;
            callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfo));
        }
    }

    internal static void UnmanagedCtorCallbackDelegate<T>(T* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CtorCallback<T> callback = (Ecs.CtorCallback<T>)context->Ctor.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedCtorCallbackPointer<T>(T* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, TypeInfo, void> callback = (delegate*<ref T, TypeInfo, void>)context->Ctor.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedDtorCallbackDelegate<T>(T* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.DtorCallback<T> callback = (Ecs.DtorCallback<T>)context->Dtor.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedDtorCallbackPointer<T>(T* data, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, TypeInfo, void> callback = (delegate*<ref T, TypeInfo, void>)context->Dtor.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref data[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedMoveCallbackDelegate<T>(T* dst, T* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.MoveCallback<T> callback = (Ecs.MoveCallback<T>)context->Move.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedMoveCallbackPointer<T>(T* dst, T* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, ref T, TypeInfo, void> callback = (delegate*<ref T, ref T, TypeInfo, void>)context->Move.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedCopyCallbackDelegate<T>(T* dst, T* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        Ecs.CopyCallback<T> callback = (Ecs.CopyCallback<T>)context->Copy.Delegate.Target!;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    internal static void UnmanagedCopyCallbackPointer<T>(T* dst, T* src, int count, ecs_type_info_t* typeInfo)
    {
        TypeHooksContext* context = (TypeHooksContext*)typeInfo->hooks.binding_ctx;
        delegate*<ref T, ref T, TypeInfo, void> callback = (delegate*<ref T, ref T, TypeInfo, void>)context->Copy.Pointer;

        for (int i = 0; i < count; i++)
            callback(ref dst[i], ref src[i], new TypeInfo(typeInfo));
    }

    #endregion

    #region OnAdd Hook Callbacks

    [UnmanagedCallersOnly]
    internal static void OnAddCallback(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->OnAdd.Invoker)(iter);
    }

    internal static void OnAddIterFieldCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddIterFieldCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T>, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddIterSpanCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddIterSpanCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T>, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddIterPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddIterPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T*, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachEntityRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachEntityRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachIterRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachIterRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T*, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachEntityPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachEntityPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T*, void>)context->OnAdd.Pointer);
    }

    internal static void OnAddEachIterPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T>)context->OnAdd.Delegate.Target!);
    }

    internal static void OnAddEachIterPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T*, void>)context->OnAdd.Pointer);
    }

    #endregion

    #region OnSet Hook Callbacks

    [UnmanagedCallersOnly]
    internal static void OnSetCallback(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->OnSet.Invoker)(iter);
    }

    internal static void OnSetIterFieldCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetIterFieldCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T>, void>)context->OnSet.Pointer);
    }

    internal static void OnSetIterSpanCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetIterSpanCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T>, void>)context->OnSet.Pointer);
    }

    internal static void OnSetIterPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetIterPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T*, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachEntityRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachEntityRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachIterRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachIterRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T*, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachEntityPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachEntityPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T*, void>)context->OnSet.Pointer);
    }

    internal static void OnSetEachIterPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T>)context->OnSet.Delegate.Target!);
    }

    internal static void OnSetEachIterPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T*, void>)context->OnSet.Pointer);
    }

    #endregion

    #region OnRemove Hook Callbacks

    [UnmanagedCallersOnly]
    internal static void OnRemoveCallback(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        ((delegate*<ecs_iter_t*, void>)context->OnRemove.Invoker)(iter);
    }

    internal static void OnRemoveIterFieldCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveIterFieldCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T>, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveIterSpanCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveIterSpanCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T>, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveIterPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveIterPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T*, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachEntityRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachEntityRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachIterRefCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachIterRefCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T*, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachEntityPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachEntityPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T*, void>)context->OnRemove.Pointer);
    }

    internal static void OnRemoveEachIterPointerCallbackDelegate<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T>)context->OnRemove.Delegate.Target!);
    }

    internal static void OnRemoveEachIterPointerCallbackPointer<T>(ecs_iter_t* iter)
    {
        TypeHooksContext* context = (TypeHooksContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T*, void>)context->OnRemove.Pointer);
    }

    #endregion
}
