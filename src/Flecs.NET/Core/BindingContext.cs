using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class holding methods and types for binding contexts.
    /// </summary>
    public static unsafe partial class BindingContext
    {
        [SuppressMessage("Usage", "CA1823")]
        private static readonly BindingContextCleanup _cleanup = new BindingContextCleanup();

        internal static readonly byte* DefaultSeparator = (byte*)Marshal.StringToHGlobalAnsi(Ecs.DefaultSeparator);

#if NET5_0_OR_GREATER
        internal static readonly IntPtr ActionCallbackPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&ActionCallback;

        internal static readonly IntPtr IterCallbackPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&IterCallback;

        internal static readonly IntPtr EachEntityCallbackPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&EachEntityCallback;

        internal static readonly IntPtr EachIndexCallbackPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&EachIndexCallback;

        internal static readonly IntPtr ObserveCallbackPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&ObserveCallback;

        internal static readonly IntPtr GroupByCallbackPointer =
            (IntPtr)(delegate* <ecs_world_t*, ecs_table_t*, ulong, void*, ulong>)&GroupByCallback;

        internal static readonly IntPtr WorldContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&WorldContextFree;

        internal static readonly IntPtr RunIterContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&RunIterContextFree;

        internal static readonly IntPtr QueryContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&QueryContextFree;

        internal static readonly IntPtr GroupByContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&GroupByContextFree;

        internal static readonly IntPtr TypeHooksContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&TypeHooksContextFree;

        internal static readonly IntPtr OsApiAbortPointer =
            (IntPtr)(delegate* <void>)&OsApiAbort;
#else
        internal static readonly IntPtr ActionCallbackPointer =
            Marshal.GetFunctionPointerForDelegate(ActionCallbackReference = ActionCallback);

        internal static readonly IntPtr IterCallbackPointer =
            Marshal.GetFunctionPointerForDelegate(IterCallbackReference = IterCallback);

        internal static readonly IntPtr EachEntityCallbackPointer =
            Marshal.GetFunctionPointerForDelegate(EachEntityCallbackReference = EachEntityCallback);

        internal static readonly IntPtr EachIndexCallbackPointer =
            Marshal.GetFunctionPointerForDelegate(EachIndexCallbackReference = EachIndexCallback);

        internal static readonly IntPtr ObserveCallbackPointer =
            Marshal.GetFunctionPointerForDelegate(ObserveCallbackReference = ObserveCallback);

        internal static readonly IntPtr GroupByCallbackPointer =
            Marshal.GetFunctionPointerForDelegate(GroupByCallbackReference = GroupByCallback);

        internal static readonly IntPtr WorldContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(WorldContextFreeReference = WorldContextFree);

        internal static readonly IntPtr RunIterContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(RunIterContextFreeReference = RunIterContextFree);

        internal static readonly IntPtr QueryContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(QueryContextFreeReference = QueryContextFree);

        internal static readonly IntPtr GroupByContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(GroupByContextFreeReference = GroupByContextFree);

        internal static readonly IntPtr TypeHooksContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(TypeHooksContextFreeReference = TypeHooksContextFree);

        internal static readonly IntPtr OsApiAbortPointer =
            Marshal.GetFunctionPointerForDelegate(OsApiAbortReference = OsApiAbort);

        private static readonly Ecs.IterAction ActionCallbackReference;
        private static readonly Ecs.IterAction IterCallbackReference;
        private static readonly Ecs.IterAction EachEntityCallbackReference;
        private static readonly Ecs.IterAction EachIndexCallbackReference;
        private static readonly Ecs.IterAction ObserveCallbackReference;
        private static readonly Ecs.GroupByAction GroupByCallbackReference;

        private static readonly Ecs.ContextFree WorldContextFreeReference;
        private static readonly Ecs.ContextFree RunIterContextFreeReference;
        private static readonly Ecs.ContextFree QueryContextFreeReference;
        private static readonly Ecs.ContextFree GroupByContextFreeReference;
        private static readonly Ecs.ContextFree TypeHooksContextFreeReference;

        private static readonly Action OsApiAbortReference;
#endif

        private static void ActionCallback(ecs_iter_t* iter)
        {
            RunIterContext* context = (RunIterContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            if (context->Iterator.GcHandle == default)
            {
                ((delegate*<void>)context->Iterator.Function)();
                return;
            }
#endif

            Action callback = (Action)context->Iterator.GcHandle.Target!;
            callback();
        }

        private static void IterCallback(ecs_iter_t* iter)
        {
            RunIterContext* context = (RunIterContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            if (context->Iterator.GcHandle == default)
            {
                Invoker.Iter(iter, (delegate*<Iter, void>)context->Iterator.Function);
                return;
            }
#endif

            Ecs.IterCallback callback = (Ecs.IterCallback)context->Iterator.GcHandle.Target!;
            Invoker.Iter(iter, callback);
        }

        private static void EachEntityCallback(ecs_iter_t* iter)
        {
            RunIterContext* context = (RunIterContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            if (context->Iterator.GcHandle == default)
            {
                Invoker.Each(iter, (delegate*<Entity, void>)context->Iterator.Function);
                return;
            }
#endif

            Ecs.EachEntityCallback callback = (Ecs.EachEntityCallback)context->Iterator.GcHandle.Target!;
            Invoker.Each(iter, callback);
        }

        private static void EachIndexCallback(ecs_iter_t* iter)
        {
            RunIterContext* context = (RunIterContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            if (context->Iterator.GcHandle == default)
            {
                Invoker.Each(iter, (delegate*<Iter, int, void>)context->Iterator.Function);
                return;
            }
#endif

            Ecs.EachIndexCallback callback = (Ecs.EachIndexCallback)context->Iterator.GcHandle.Target!;
            Invoker.Each(iter, callback);
        }

        private static void ObserveCallback(ecs_iter_t* iter)
        {
            RunIterContext* context = (RunIterContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            if (context->Iterator.GcHandle == default)
            {
                Invoker.Each(iter, (delegate*<Entity, void>)context->Iterator.Function);
                return;
            }
#endif

            Ecs.EachEntityCallback callback = (Ecs.EachEntityCallback)context->Iterator.GcHandle.Target!;
            Invoker.Observe(iter, callback);
        }

        private static ulong GroupByCallback(ecs_world_t* world, ecs_table_t* table, ulong id, void* ctx)
        {
            GroupByContext* context = (GroupByContext*)ctx;
            Ecs.GroupByCallback callback = (Ecs.GroupByCallback)context->GroupBy.GcHandle.Target!;
            return callback(new World(world), new Table(world, table), new Entity(world, id));
        }

        private static void WorldContextFree(void* context)
        {
            WorldContext* worldContext = (WorldContext*)context;
            worldContext->Dispose();
            Memory.Free(context);
        }

        private static void RunIterContextFree(void* context)
        {
            RunIterContext* runIterContext = (RunIterContext*)context;
            runIterContext->Dispose();
            Memory.Free(context);
        }
        private static void QueryContextFree(void* context)
        {
            QueryContext* queryContext = (QueryContext*)context;
            queryContext->Dispose();
            Memory.Free(context);
        }

        private static void GroupByContextFree(void* context)
        {
            GroupByContext* queryGroupByContext = (GroupByContext*)context;
            queryGroupByContext->Dispose();
            Memory.Free(context);
        }

        private static void TypeHooksContextFree(void* context)
        {
            TypeHooksContext* typeHooks = (TypeHooksContext*)context;
            typeHooks->Dispose();
            Memory.Free(context);
        }

        private static void OsApiAbort()
        {
            throw new Ecs.NativeException("Application aborted from native code.");
        }

        internal static Callback AllocCallback<T>(T? callback, bool storePtr = true) where T : Delegate
        {
            if (callback == null)
                return default;

            IntPtr funcPtr = storePtr ? Marshal.GetFunctionPointerForDelegate(callback) : IntPtr.Zero;
            return new Callback(funcPtr, GCHandle.Alloc(callback));
        }

        internal static void SetCallback(ref Callback dest, IntPtr callback)
        {
            if (dest.GcHandle != default)
                dest.Dispose();

            dest.Function = callback;
        }

        internal static void SetCallback<T>(ref Callback dest, T? callback, bool storePtr = true) where T : Delegate
        {
            if (dest.GcHandle != default)
                dest.Dispose();

            dest = AllocCallback(callback, storePtr);
        }

        internal struct Callback : IDisposable
        {
            public IntPtr Function;
            public GCHandle GcHandle;

            public Callback(IntPtr function, GCHandle gcHandle)
            {
                Function = function;
                GcHandle = gcHandle;
            }

            public void Dispose()
            {
                Managed.FreeGcHandle(GcHandle);
                Function = default;
                GcHandle = default;
            }
        }

        internal class Box<T>
        {
            public bool ShouldFree;
            [MaybeNull] public T Value = default!;

            public Box()
            {
            }

            public Box(T value, bool shouldFree = false)
            {
                Value = value;
                ShouldFree = shouldFree;
            }
        }

        internal struct WorldContext : IDisposable
        {
            public Callback AtFini;
            public Callback RunPostFrame;
            public Callback ContextFree;
            public NativeList<ulong> TypeCache;

            public void Dispose()
            {
                AtFini.Dispose();
                RunPostFrame.Dispose();
                ContextFree.Dispose();
                TypeCache.Dispose();
            }
        }

        internal struct RunIterContext : IDisposable
        {
            public Callback Run;
            public Callback Iterator;

            public void Dispose()
            {
                Run.Dispose();
                Iterator.Dispose();
            }
        }

        internal struct QueryContext : IDisposable
        {
            public Callback OrderByAction;
            public Callback GroupByAction;
            public Callback ContextFree;
            public Callback GroupCreateAction;
            public Callback GroupDeleteAction;

            public NativeList<NativeString> Strings;

            public void Dispose()
            {
                OrderByAction.Dispose();
                GroupByAction.Dispose();
                ContextFree.Dispose();
                GroupCreateAction.Dispose();
                GroupDeleteAction.Dispose();

                if (Strings == default)
                    return;

                for (int i = 0; i < Strings.Count; i++)
                    Strings[i].Dispose();

                Strings.Dispose();
            }
        }

        internal struct TypeHooksContext : IDisposable
        {
            public Callback Ctor;
            public Callback Dtor;
            public Callback Move;
            public Callback Copy;
            public Callback OnAdd;
            public Callback OnSet;
            public Callback OnRemove;
            public Callback ContextFree;

            public void Dispose()
            {
                Ctor.Dispose();
                Dtor.Dispose();
                Move.Dispose();
                Copy.Dispose();
                OnAdd.Dispose();
                OnSet.Dispose();
                OnRemove.Dispose();
                ContextFree.Dispose();
            }
        }

        internal struct GroupByContext : IDisposable
        {
            public Callback GroupBy;

            public void Dispose()
            {
                GroupBy.Dispose();
            }
        }

        // Free native resources on program exit
        // TODO: This isn't guaranteed to be called at program exit?
        private class BindingContextCleanup
        {
            ~BindingContextCleanup()
            {
                Memory.Free(DefaultSeparator);
            }
        }
    }

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static unsafe partial class BindingContext<T0>
    {
#if NET5_0_OR_GREATER
        internal static readonly IntPtr EntityObserverEachPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&EntityObserverEach;

        internal static readonly IntPtr EntityObserverEachEntityPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&EntityObserverEachEntity;

        internal static readonly IntPtr UnmanagedCtorPointer =
            (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&UnmanagedCtor;

        internal static readonly IntPtr UnmanagedDtorPointer =
            (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&UnmanagedDtor;

        internal static readonly IntPtr UnmanagedMovePointer =
            (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&UnmanagedMove;

        internal static readonly IntPtr UnmanagedCopyPointer =
            (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&UnmanagedCopy;

        internal static readonly IntPtr ManagedCtorPointer =
            (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&ManagedCtor;

        internal static readonly IntPtr ManagedDtorPointer =
            (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&ManagedDtor;

        internal static readonly IntPtr ManagedMovePointer =
            (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&ManagedMove;

        internal static readonly IntPtr ManagedCopyPointer =
            (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&ManagedCopy;

        internal static readonly IntPtr DefaultManagedCtorPointer =
            (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&DefaultManagedCtor;

        internal static readonly IntPtr DefaultManagedDtorPointer =
            (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&DefaultManagedDtor;

        internal static readonly IntPtr DefaultManagedMovePointer =
            (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&DefaultManagedMove;

        internal static readonly IntPtr DefaultManagedCopyPointer =
            (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&DefaultManagedCopy;

        internal static readonly IntPtr OnAddHookPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddHook;

        internal static readonly IntPtr OnSetHookPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetHook;

        internal static readonly IntPtr OnRemoveHookPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveHook;
#else
        internal static readonly IntPtr EntityObserverEachPointer =
            Marshal.GetFunctionPointerForDelegate(EntityObserverEachReference = EntityObserverEach);

        internal static readonly IntPtr EntityObserverEachEntityPointer =
            Marshal.GetFunctionPointerForDelegate(EntityObserverEachEntityReference = EntityObserverEachEntity);

        internal static readonly IntPtr UnmanagedCtorPointer =
            Marshal.GetFunctionPointerForDelegate(UnmanagedCtorReference = UnmanagedCtor);

        internal static readonly IntPtr UnmanagedDtorPointer =
            Marshal.GetFunctionPointerForDelegate(UnmanagedDtorReference = UnmanagedDtor);

        internal static readonly IntPtr UnmanagedMovePointer =
            Marshal.GetFunctionPointerForDelegate(UnmanagedMoveReference = UnmanagedMove);

        internal static readonly IntPtr UnmanagedCopyPointer =
            Marshal.GetFunctionPointerForDelegate(UnmanagedCopyReference = UnmanagedCopy);

        internal static readonly IntPtr ManagedCtorPointer =
            Marshal.GetFunctionPointerForDelegate(ManagedCtorReference = ManagedCtor);

        internal static readonly IntPtr ManagedDtorPointer =
            Marshal.GetFunctionPointerForDelegate(ManagedDtorReference = ManagedDtor);

        internal static readonly IntPtr ManagedMovePointer =
            Marshal.GetFunctionPointerForDelegate(ManagedMoveReference = ManagedMove);

        internal static readonly IntPtr ManagedCopyPointer =
            Marshal.GetFunctionPointerForDelegate(ManagedCopyReference = ManagedCopy);

        internal static readonly IntPtr DefaultManagedCtorPointer =
            Marshal.GetFunctionPointerForDelegate(DefaultManagedCtorReference = DefaultManagedCtor);

        internal static readonly IntPtr DefaultManagedDtorPointer =
            Marshal.GetFunctionPointerForDelegate(DefaultManagedDtorReference = DefaultManagedDtor);

        internal static readonly IntPtr DefaultManagedMovePointer =
            Marshal.GetFunctionPointerForDelegate(DefaultManagedMoveReference = DefaultManagedMove);

        internal static readonly IntPtr DefaultManagedCopyPointer =
            Marshal.GetFunctionPointerForDelegate(DefaultManagedCopyReference = DefaultManagedCopy);

        internal static readonly IntPtr OnAddHookPointer =
            Marshal.GetFunctionPointerForDelegate(OnAddHookReference = OnAddHook);

        internal static readonly IntPtr OnSetHookPointer =
            Marshal.GetFunctionPointerForDelegate(OnSetHookReference = OnSetHook);

        internal static readonly IntPtr OnRemoveHookPointer =
            Marshal.GetFunctionPointerForDelegate(OnRemoveHookCopyReference = OnRemoveHook);

        private static readonly Ecs.IterAction EntityObserverEachReference;
        private static readonly Ecs.IterAction EntityObserverEachEntityReference;

        private static readonly Ecs.CtorCallback UnmanagedCtorReference;
        private static readonly Ecs.DtorCallback UnmanagedDtorReference;
        private static readonly Ecs.MoveCallback UnmanagedMoveReference;
        private static readonly Ecs.CopyCallback UnmanagedCopyReference;

        private static readonly Ecs.CtorCallback ManagedCtorReference;
        private static readonly Ecs.DtorCallback ManagedDtorReference;
        private static readonly Ecs.MoveCallback ManagedMoveReference;
        private static readonly Ecs.CopyCallback ManagedCopyReference;

        private static readonly Ecs.CtorCallback DefaultManagedCtorReference;
        private static readonly Ecs.DtorCallback DefaultManagedDtorReference;
        private static readonly Ecs.MoveCallback DefaultManagedMoveReference;
        private static readonly Ecs.CopyCallback DefaultManagedCopyReference;

        private static readonly Ecs.IterAction OnAddHookReference;
        private static readonly Ecs.IterAction OnSetHookReference;
        private static readonly Ecs.IterAction OnRemoveHookCopyReference;
#endif

        private static void EntityObserverEach(ecs_iter_t* iter)
        {
            BindingContext.RunIterContext* context = (BindingContext.RunIterContext*)iter->binding_ctx;
            Ecs.EachCallback<T0> callback = (Ecs.EachCallback<T0>)context->Iterator.GcHandle.Target!;
            Invoker.Observe(iter, callback);
        }

        private static void EntityObserverEachEntity(ecs_iter_t* iter)
        {
            BindingContext.RunIterContext* context = (BindingContext.RunIterContext*)iter->binding_ctx;
            Ecs.EachEntityCallback<T0> callback = (Ecs.EachEntityCallback<T0>)context->Iterator.GcHandle.Target!;
            Invoker.Observe(iter, callback);
        }

        private static void UnmanagedCtor(void* dataHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            TypeInfo typeInfo = new TypeInfo(typeInfoHandle);

            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            Ecs.CtorCallback<T0> callback = (Ecs.CtorCallback<T0>)context->Ctor.GcHandle.Target!;

            T0* data = (T0*)dataHandle;

            for (int i = 0; i < count; i++)
                callback(ref data[i], typeInfo);
        }

        private static void UnmanagedDtor(void* dataHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            TypeInfo typeInfo = new TypeInfo(typeInfoHandle);

            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            Ecs.DtorCallback<T0> callback = (Ecs.DtorCallback<T0>)context->Dtor.GcHandle.Target!;

            T0* data = (T0*)dataHandle;

            for (int i = 0; i < count; i++)
                callback(ref data[i], typeInfo);
        }

        private static void UnmanagedMove(void* dstHandle, void* srcHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            TypeInfo typeInfo = new TypeInfo(typeInfoHandle);

            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            Ecs.MoveCallback<T0> callback = (Ecs.MoveCallback<T0>)context->Move.GcHandle.Target!;

            T0* dst = (T0*)dstHandle;
            T0* src = (T0*)srcHandle;

            for (int i = 0; i < count; i++)
                callback(ref dst[i], ref src[i], typeInfo);
        }

        private static void UnmanagedCopy(void* dstHandle, void* srcHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            TypeInfo typeInfo = new TypeInfo(typeInfoHandle);

            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            Ecs.CopyCallback<T0> callback = (Ecs.CopyCallback<T0>)context->Copy.GcHandle.Target!;

            T0* dst = (T0*)dstHandle;
            T0* src = (T0*)srcHandle;

            for (int i = 0; i < count; i++)
                callback(ref dst[i], ref src[i], typeInfo);
        }

        private static void ManagedCtor(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;
            Ecs.CtorCallback<T0> callback = (Ecs.CtorCallback<T0>)context->Ctor.GcHandle.Target!;

            GCHandle* handles = (GCHandle*)data;

            for (int i = 0; i < count; i++)
            {
                BindingContext.Box<T0> box = new BindingContext.Box<T0>();

                callback(ref box.Value!, new TypeInfo(typeInfoHandle));

                handles[i] = GCHandle.Alloc(box);
            }
        }

        private static void ManagedDtor(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;
            Ecs.DtorCallback<T0> callback = (Ecs.DtorCallback<T0>)context->Dtor.GcHandle.Target!;

            GCHandle* handles = (GCHandle*)data;

            for (int i = 0; i < count; i++)
            {
                BindingContext.Box<T0> box = (BindingContext.Box<T0>)handles[i].Target!;

                callback(ref box.Value!, new TypeInfo(typeInfoHandle));

                Managed.FreeGcHandle(handles[i]);
                handles[i] = default;
            }
        }

        private static void ManagedMove(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;
            Ecs.MoveCallback<T0> callback = (Ecs.MoveCallback<T0>)context->Move.GcHandle.Target!;

            GCHandle* dstHandles = (GCHandle*)dst;
            GCHandle* srcHandles = (GCHandle*)src;

            for (int i = 0; i < count; i++)
            {
                BindingContext.Box<T0> dstBox = (BindingContext.Box<T0>)dstHandles[i].Target!;
                BindingContext.Box<T0> srcBox = (BindingContext.Box<T0>)srcHandles[i].Target!;

                callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfoHandle));

                // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
                if (srcBox.ShouldFree)
                    Managed.FreeGcHandle(srcHandles[i]);
            }
        }

        private static void ManagedCopy(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;
            Ecs.CopyCallback<T0> callback = (Ecs.CopyCallback<T0>)context->Copy.GcHandle.Target!;

            GCHandle* dstHandles = (GCHandle*)dst;
            GCHandle* srcHandles = (GCHandle*)src;

            for (int i = 0; i < count; i++)
            {
                BindingContext.Box<T0> dstBox = (BindingContext.Box<T0>)dstHandles[i].Target!;
                BindingContext.Box<T0> srcBox = (BindingContext.Box<T0>)srcHandles[i].Target!;

                callback(ref dstBox.Value!, ref srcBox.Value!, new TypeInfo(typeInfoHandle));

                // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
                if (srcBox.ShouldFree)
                    Managed.FreeGcHandle(srcHandles[i]);
            }
        }

        // For managed types, create a strong box and attempt to set it's value with Activator.CreateInstance<T0>().
        // If no public parameterless constructor exists, the strong box will point to a null value until
        // .Set is called.
        private static void DefaultManagedCtor(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            GCHandle* handles = (GCHandle*)data;

            for (int i = 0; i < count; i++)
                handles[i] = GCHandle.Alloc(new BindingContext.Box<T0>());
        }

        private static void DefaultManagedDtor(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            GCHandle* handles = (GCHandle*)data;

            for (int i = 0; i < count; i++)
            {
                Managed.FreeGcHandle(handles[i]);
                handles[i] = default;
            }
        }

        private static void DefaultManagedMove(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
        {
            GCHandle* dstHandles = (GCHandle*)dst;
            GCHandle* srcHandles = (GCHandle*)src;

            for (int i = 0; i < count; i++)
            {
                BindingContext.Box<T0> dstBox = (BindingContext.Box<T0>)dstHandles[i].Target!;
                BindingContext.Box<T0> srcBox = (BindingContext.Box<T0>)srcHandles[i].Target!;

                dstBox.Value = srcBox.Value!;

                // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
                if (srcBox.ShouldFree)
                    Managed.FreeGcHandle(srcHandles[i]);
            }
        }

        private static void DefaultManagedCopy(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
        {
            GCHandle* dstHandles = (GCHandle*)dst;
            GCHandle* srcHandles = (GCHandle*)src;

            for (int i = 0; i < count; i++)
            {
                BindingContext.Box<T0> dstBox = (BindingContext.Box<T0>)dstHandles[i].Target!;
                BindingContext.Box<T0> srcBox = (BindingContext.Box<T0>)srcHandles[i].Target!;

                dstBox.Value = srcBox.Value!;

                // Free the gc handle if it comes from a .Set call, otherwise let the Dtor hook handle it.
                if (srcBox.ShouldFree)
                    Managed.FreeGcHandle(srcHandles[i]);
            }
        }

        private static void OnAddHook(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->binding_ctx;
            Ecs.IterCallback<T0> callback = (Ecs.IterCallback<T0>)context->OnAdd.GcHandle.Target!;

            Iter it = new Iter(iter);

            for (int i = 0; i < iter->count; i++)
                callback(it, it.Field<T0>(0));
        }

        private static void OnSetHook(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->binding_ctx;
            Ecs.IterCallback<T0> callback = (Ecs.IterCallback<T0>)context->OnSet.GcHandle.Target!;

            Iter it = new Iter(iter);

            for (int i = 0; i < iter->count; i++)
                callback(it, it.Field<T0>(0));
        }

        private static void OnRemoveHook(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->binding_ctx;
            Ecs.IterCallback<T0> callback = (Ecs.IterCallback<T0>)context->OnRemove.GcHandle.Target!;

            Iter it = new Iter(iter);

            for (int i = 0; i < iter->count; i++)
                callback(it, it.Field<T0>(0));
        }
    }
}
