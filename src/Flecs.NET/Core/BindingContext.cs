using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class holding methods and types for binding contexts.
    /// </summary>
    public static unsafe class BindingContext
    {
        static BindingContext()
        {
            AppDomain.CurrentDomain.ProcessExit += (object? _, EventArgs _) =>
            {
                Memory.Free(DefaultSeparator);
            };
        }

        internal static readonly byte* DefaultSeparator = (byte*)Marshal.StringToHGlobalAnsi(Ecs.DefaultSeparator);

        internal static readonly IntPtr ActionCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&ActionCallback;
        internal static readonly IntPtr IterCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&IterCallback;
        internal static readonly IntPtr EachEntityCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&EachEntityCallback;
        internal static readonly IntPtr EachIterCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&EachIterCallback;
        internal static readonly IntPtr ObserveCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&ObserveCallback;
        internal static readonly IntPtr RunCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&RunCallback;
        internal static readonly IntPtr RunDelegateCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&RunDelegateCallback;
        internal static readonly IntPtr RunPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&RunPointerCallback;
        internal static readonly IntPtr GroupByCallbackPointer = (IntPtr)(delegate* <ecs_world_t*, ecs_table_t*, ulong, void*, ulong>)&GroupByCallback;

        internal static readonly IntPtr WorldContextFreePointer = (IntPtr)(delegate* <void*, void>)&WorldContextFree;
        internal static readonly IntPtr IteratorContextFreePointer = (IntPtr)(delegate* <void*, void>)&IteratorContextFree;
        internal static readonly IntPtr RunContextFreePointer = (IntPtr)(delegate* <void*, void>)&RunContextFree;
        internal static readonly IntPtr QueryContextFreePointer = (IntPtr)(delegate* <void*, void>)&QueryContextFree;
        internal static readonly IntPtr GroupByContextFreePointer = (IntPtr)(delegate* <void*, void>)&GroupByContextFree;
        internal static readonly IntPtr TypeHooksContextFreePointer = (IntPtr)(delegate* <void*, void>)&TypeHooksContextFree;

        internal static readonly IntPtr OsApiAbortPointer = (IntPtr)(delegate* <void>)&OsApiAbort;

        private static void ActionCallback(ecs_iter_t* iter)
        {
            IteratorContext* context = (IteratorContext*)iter->callback_ctx;

            if (context->Callback.Pointer != default)
                ((delegate*<void>)context->Callback.Pointer)();
            else
                ((Action)context->Callback.GcHandle.Target!)();
        }

        private static void IterCallback(ecs_iter_t* iter)
        {
            IteratorContext* context = (IteratorContext*)iter->callback_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Iter(iter, (delegate*<Iter, void>)context->Callback.Pointer);
            else
                Invoker.Iter(iter, (Ecs.IterCallback)context->Callback.GcHandle.Target!);
        }

        private static void EachEntityCallback(ecs_iter_t* iter)
        {
            IteratorContext* context = (IteratorContext*)iter->callback_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Each(iter, (delegate*<Entity, void>)context->Callback.Pointer);
            else
                Invoker.Each(iter, (Ecs.EachEntityCallback)context->Callback.GcHandle.Target!);
        }

        private static void EachIterCallback(ecs_iter_t* iter)
        {
            IteratorContext* context = (IteratorContext*)iter->callback_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Each(iter, (delegate*<Iter, int, void>)context->Callback.Pointer);
            else
                Invoker.Each(iter, (Ecs.EachIterCallback)context->Callback.GcHandle.Target!);
        }

        private static void ObserveCallback(ecs_iter_t* iter)
        {
            IteratorContext* context = (IteratorContext*)iter->callback_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Observe(iter, (delegate*<Entity, void>)context->Callback.Pointer);
            else
                Invoker.Observe(iter, (Ecs.EachEntityCallback)context->Callback.GcHandle.Target!);
        }

        private static void RunCallback(ecs_iter_t* iter)
        {
            RunContext* context = (RunContext*)iter->run_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Run(iter, (delegate*<Iter, void>)context->Callback.Pointer);
            else
                Invoker.Run(iter, (Ecs.RunCallback)context->Callback.GcHandle.Target!);
        }

        private static void RunDelegateCallback(ecs_iter_t* iter)
        {
            RunContext* context = (RunContext*)iter->run_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Run(iter, (delegate*<Iter, Action<Iter>, void>)context->Callback.Pointer);
            else
                Invoker.Run(iter, (Ecs.RunDelegateCallback)context->Callback.GcHandle.Target!);
        }

        private static void RunPointerCallback(ecs_iter_t* iter)
        {
            RunContext* context = (RunContext*)iter->run_ctx;

            if (context->Callback.Pointer != default)
                Invoker.Run(iter, (delegate*<Iter, delegate*<Iter, void>, void>)context->Callback.Pointer);
            else
                Invoker.Run(iter, (Ecs.RunPointerCallback)context->Callback.GcHandle.Target!);
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

        private static void IteratorContextFree(void* context)
        {
            IteratorContext* iteratorContext = (IteratorContext*)context;
            iteratorContext->Dispose();
            Memory.Free(context);
        }

        private static void RunContextFree(void* context)
        {
            RunContext* runContext = (RunContext*)context;
            runContext->Dispose();
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

            dest.Pointer = callback;
        }

        internal static void SetCallback<T>(ref Callback dest, T? callback, bool storePtr = true) where T : Delegate
        {
            if (dest.GcHandle != default)
                dest.Dispose();

            dest = AllocCallback(callback, storePtr);
        }

        internal struct Callback : IDisposable
        {
            public IntPtr Pointer;
            public GCHandle GcHandle;

            public Callback(IntPtr function, GCHandle gcHandle)
            {
                Pointer = function;
                GcHandle = gcHandle;
            }

            public void Dispose()
            {
                Managed.FreeGcHandle(GcHandle);
                Pointer = default;
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

        internal struct IteratorContext : IDisposable
        {
            public Callback Callback;

            public void Dispose()
            {
                Callback.Dispose();
            }
        }

        internal struct RunContext : IDisposable
        {
            public Callback Callback;

            public void Dispose()
            {
                Callback.Dispose();
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
            public int Header;

            public Callback Ctor;
            public Callback Dtor;
            public Callback Move;
            public Callback Copy;
            public Callback OnAdd;
            public Callback OnSet;
            public Callback OnRemove;
            public Callback ContextFree;

            public static readonly TypeHooksContext Default = new TypeHooksContext { Header = Ecs.Header };

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

        internal struct OsApiContext : IDisposable
        {
            public Callback Abort;
            public Callback Log;

            public void Dispose()
            {
                Abort.Dispose();
                Log.Dispose();
            }
        }
    }

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static unsafe partial class BindingContext<T0>
    {
        internal static readonly IntPtr EntityObserverEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&EntityObserverEachRefCallback;
        internal static readonly IntPtr EntityObserverEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&EntityObserverEachEntityRefCallback;

        internal static readonly IntPtr UnmanagedCtorCallbackPointer = (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&UnmanagedCtorCallback;
        internal static readonly IntPtr UnmanagedDtorCallbackPointer = (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&UnmanagedDtorCallback;
        internal static readonly IntPtr UnmanagedMoveCallbackPointer = (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&UnmanagedMoveCallback;
        internal static readonly IntPtr UnmanagedCopyCallbackPointer = (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&UnmanagedCopyCallback;

        internal static readonly IntPtr ManagedCtorCallbackPointer = (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&ManagedCtorCallback;
        internal static readonly IntPtr ManagedDtorCallbackPointer = (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&ManagedDtorCallback;
        internal static readonly IntPtr ManagedMoveCallbackPointer = (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&ManagedMoveCallback;
        internal static readonly IntPtr ManagedCopyCallbackPointer = (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&ManagedCopyCallback;

        internal static readonly IntPtr DefaultManagedCtorCallbackPointer = (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&DefaultManagedCtorCallback;
        internal static readonly IntPtr DefaultManagedDtorCallbackPointer = (IntPtr)(delegate* <void*, int, ecs_type_info_t*, void>)&DefaultManagedDtorCallback;
        internal static readonly IntPtr DefaultManagedMoveCallbackPointer = (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&DefaultManagedMoveCallback;
        internal static readonly IntPtr DefaultManagedCopyCallbackPointer = (IntPtr)(delegate* <void*, void*, int, ecs_type_info_t*, void>)&DefaultManagedCopyCallback;

        internal static readonly IntPtr OnAddIterFieldCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddIterFieldCallback;
        internal static readonly IntPtr OnSetIterFieldCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetIterFieldCallback;
        internal static readonly IntPtr OnRemoveIterFieldCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveIterFieldCallback;

        internal static readonly IntPtr OnAddIterSpanCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddIterSpanCallback;
        internal static readonly IntPtr OnSetIterSpanCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetIterSpanCallback;
        internal static readonly IntPtr OnRemoveIterSpanCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveIterSpanCallback;

        internal static readonly IntPtr OnAddIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddIterPointerCallback;
        internal static readonly IntPtr OnSetIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetIterPointerCallback;
        internal static readonly IntPtr OnRemoveIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveIterPointerCallback;

        internal static readonly IntPtr OnAddEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddEachRefCallback;
        internal static readonly IntPtr OnSetEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetEachRefCallback;
        internal static readonly IntPtr OnRemoveEachRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveEachRefCallback;

        internal static readonly IntPtr OnAddEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddEachEntityRefCallback;
        internal static readonly IntPtr OnSetEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetEachEntityRefCallback;
        internal static readonly IntPtr OnRemoveEachEntityRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveEachEntityRefCallback;

        internal static readonly IntPtr OnAddEachIterRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddEachIterRefCallback;
        internal static readonly IntPtr OnSetEachIterRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetEachIterRefCallback;
        internal static readonly IntPtr OnRemoveEachIterRefCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveEachIterRefCallback;

        internal static readonly IntPtr OnAddEachPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddEachPointerCallback;
        internal static readonly IntPtr OnSetEachPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetEachPointerCallback;
        internal static readonly IntPtr OnRemoveEachPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveEachPointerCallback;

        internal static readonly IntPtr OnAddEachEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddEachEntityPointerCallback;
        internal static readonly IntPtr OnSetEachEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetEachEntityPointerCallback;
        internal static readonly IntPtr OnRemoveEachEntityPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveEachEntityPointerCallback;

        internal static readonly IntPtr OnAddEachIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnAddEachIterPointerCallback;
        internal static readonly IntPtr OnSetEachIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnSetEachIterPointerCallback;
        internal static readonly IntPtr OnRemoveEachIterPointerCallbackPointer = (IntPtr)(delegate* <ecs_iter_t*, void>)&OnRemoveEachIterPointerCallback;

        private static void EntityObserverEachRefCallback(ecs_iter_t* iter)
        {
            BindingContext.IteratorContext* context = (BindingContext.IteratorContext*)iter->callback_ctx;
            Ecs.EachRefCallback<T0> callback = (Ecs.EachRefCallback<T0>)context->Callback.GcHandle.Target!;
            Invoker.Observe(iter, callback);
        }

        private static void EntityObserverEachEntityRefCallback(ecs_iter_t* iter)
        {
            BindingContext.IteratorContext* context = (BindingContext.IteratorContext*)iter->callback_ctx;
            Ecs.EachEntityRefCallback<T0> callback = (Ecs.EachEntityRefCallback<T0>)context->Callback.GcHandle.Target!;
            Invoker.Observe(iter, callback);
        }

        private static void UnmanagedCtorCallback(void* dataHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            T0* data = (T0*)dataHandle;

            if (context->Ctor.Pointer != default)
            {
                delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Ctor.Pointer;
                for (int i = 0; i < count; i++)
                    callback(ref data[i], new TypeInfo(typeInfoHandle));
            }
            else
            {
                Ecs.CtorCallback<T0> callback = (Ecs.CtorCallback<T0>)context->Ctor.GcHandle.Target!;
                for (int i = 0; i < count; i++)
                    callback(ref data[i], new TypeInfo(typeInfoHandle));
            }
        }

        private static void UnmanagedDtorCallback(void* dataHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            T0* data = (T0*)dataHandle;

            if (context->Dtor.Pointer != default)
            {
                delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Dtor.Pointer;
                for (int i = 0; i < count; i++)
                    callback(ref data[i], new TypeInfo(typeInfoHandle));
            }
            else
            {
                Ecs.DtorCallback<T0> callback = (Ecs.DtorCallback<T0>)context->Dtor.GcHandle.Target!;
                for (int i = 0; i < count; i++)
                    callback(ref data[i], new TypeInfo(typeInfoHandle));
            }
        }

        private static void UnmanagedMoveCallback(void* dstHandle, void* srcHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            T0* dst = (T0*)dstHandle;
            T0* src = (T0*)srcHandle;

            if (context->Move.Pointer != default)
            {
                delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Move.Pointer;
                for (int i = 0; i < count; i++)
                    callback(ref dst[i], ref src[i], new TypeInfo(typeInfoHandle));
            }
            else
            {
                Ecs.MoveCallback<T0> callback = (Ecs.MoveCallback<T0>)context->Move.GcHandle.Target!;
                for (int i = 0; i < count; i++)
                    callback(ref dst[i], ref src[i], new TypeInfo(typeInfoHandle));
            }
        }

        private static void UnmanagedCopyCallback(void* dstHandle, void* srcHandle, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            T0* dst = (T0*)dstHandle;
            T0* src = (T0*)srcHandle;

            if (context->Copy.Pointer != default)
            {
                delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Copy.Pointer;
                for (int i = 0; i < count; i++)
                    callback(ref dst[i], ref src[i], new TypeInfo(typeInfoHandle));
            }
            else
            {
                Ecs.CopyCallback<T0> callback = (Ecs.CopyCallback<T0>)context->Copy.GcHandle.Target!;
                for (int i = 0; i < count; i++)
                    callback(ref dst[i], ref src[i], new TypeInfo(typeInfoHandle));
            }
        }

        private static void ManagedCtorCallback(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            GCHandle* handles = (GCHandle*)data;

            if (context->Ctor.Pointer != default)
            {
                delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Ctor.Pointer;
                for (int i = 0; i < count; i++)
                {
                    BindingContext.Box<T0> box = new BindingContext.Box<T0>();
                    callback(ref box.Value!, new TypeInfo(typeInfoHandle));
                    handles[i] = GCHandle.Alloc(box);
                }
            }
            else
            {
                Ecs.CtorCallback<T0> callback = (Ecs.CtorCallback<T0>)context->Ctor.GcHandle.Target!;
                for (int i = 0; i < count; i++)
                {
                    BindingContext.Box<T0> box = new BindingContext.Box<T0>();
                    callback(ref box.Value!, new TypeInfo(typeInfoHandle));
                    handles[i] = GCHandle.Alloc(box);
                }
            }
        }

        private static void ManagedDtorCallback(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            GCHandle* handles = (GCHandle*)data;

            if (context->Dtor.Pointer != default)
            {
                delegate*<ref T0, TypeInfo, void> callback = (delegate*<ref T0, TypeInfo, void>)context->Dtor.Pointer;
                for (int i = 0; i < count; i++)
                {
                    BindingContext.Box<T0> box = (BindingContext.Box<T0>)handles[i].Target!;
                    callback(ref box.Value!, new TypeInfo(typeInfoHandle));
                    Managed.FreeGcHandle(handles[i]);
                    handles[i] = default;
                }
            }
            else
            {
                Ecs.DtorCallback<T0> callback = (Ecs.DtorCallback<T0>)context->Dtor.GcHandle.Target!;
                for (int i = 0; i < count; i++)
                {
                    BindingContext.Box<T0> box = (BindingContext.Box<T0>)handles[i].Target!;
                    callback(ref box.Value!, new TypeInfo(typeInfoHandle));
                    Managed.FreeGcHandle(handles[i]);
                    handles[i] = default;
                }
            }
        }

        private static void ManagedMoveCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            GCHandle* dstHandles = (GCHandle*)dst;
            GCHandle* srcHandles = (GCHandle*)src;

            if (context->Move.Pointer != default)
            {
                delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Move.Pointer;
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
            else
            {
                Ecs.MoveCallback<T0> callback = (Ecs.MoveCallback<T0>)context->Move.GcHandle.Target!;
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
        }

        private static void ManagedCopyCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
        {
            BindingContext.TypeHooksContext* context =
                (BindingContext.TypeHooksContext*)typeInfoHandle->hooks.binding_ctx;

            GCHandle* dstHandles = (GCHandle*)dst;
            GCHandle* srcHandles = (GCHandle*)src;

            if (context->Copy.Pointer != default)
            {
                delegate*<ref T0, ref T0, TypeInfo, void> callback = (delegate*<ref T0, ref T0, TypeInfo, void>)context->Copy.Pointer;
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
            else
            {
                Ecs.CopyCallback<T0> callback = (Ecs.CopyCallback<T0>)context->Copy.GcHandle.Target!;
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
        }

        private static void DefaultManagedCtorCallback(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            GCHandle* handles = (GCHandle*)data;

            for (int i = 0; i < count; i++)
                handles[i] = GCHandle.Alloc(new BindingContext.Box<T0>());
        }

        private static void DefaultManagedDtorCallback(void* data, int count, ecs_type_info_t* typeInfoHandle)
        {
            GCHandle* handles = (GCHandle*)data;

            for (int i = 0; i < count; i++)
            {
                Managed.FreeGcHandle(handles[i]);
                handles[i] = default;
            }
        }

        private static void DefaultManagedMoveCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
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

        private static void DefaultManagedCopyCallback(void* dst, void* src, int count, ecs_type_info_t* typeInfoHandle)
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

        private static void OnAddIterFieldCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Iter, Field<T0>, void> callback = (delegate*<Iter, Field<T0>, void>)context->OnAdd.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.Field<T0>(0));
            }
            else
            {
                Ecs.IterFieldCallback<T0> callback = (Ecs.IterFieldCallback<T0>)context->OnAdd.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.Field<T0>(0));
            }
        }

        private static void OnAddIterSpanCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Iter, Span<T0>, void> callback = (delegate*<Iter, Span<T0>, void>)context->OnAdd.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetSpan<T0>(0));
            }
            else
            {
                Ecs.IterSpanCallback<T0> callback = (Ecs.IterSpanCallback<T0>)context->OnAdd.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetSpan<T0>(0));
            }
        }

        private static void OnAddIterPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Iter, T0*, void> callback = (delegate*<Iter, T0*, void>)context->OnAdd.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetPointer<T0>(0));
            }
            else
            {
                Ecs.IterPointerCallback<T0> callback = (Ecs.IterPointerCallback<T0>)context->OnAdd.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetPointer<T0>(0));
            }
        }

        private static void OnAddEachRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<ref T0, void> callback = (delegate*<ref T0, void>)context->OnAdd.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachRefCallback<T0> callback = (Ecs.EachRefCallback<T0>)context->OnAdd.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnAddEachEntityRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Entity, ref T0, void> callback = (delegate*<Entity, ref T0, void>)context->OnAdd.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachEntityRefCallback<T0> callback = (Ecs.EachEntityRefCallback<T0>)context->OnAdd.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnAddEachIterRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Iter, int, ref T0, void> callback = (delegate*<Iter, int, ref T0, void>)context->OnAdd.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachIterRefCallback<T0> callback = (Ecs.EachIterRefCallback<T0>)context->OnAdd.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnAddEachPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<T0*, void> callback = (delegate*<T0*, void>)context->OnAdd.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(&pointer[i]);
            }
            else
            {
                Ecs.EachPointerCallback<T0> callback = (Ecs.EachPointerCallback<T0>)context->OnAdd.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(&pointer[i]);
            }
        }

        private static void OnAddEachEntityPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Entity, T0*, void> callback = (delegate*<Entity, T0*, void>)context->OnAdd.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), &pointer[i]);
            }
            else
            {
                Ecs.EachEntityPointerCallback<T0> callback = (Ecs.EachEntityPointerCallback<T0>)context->OnAdd.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), &pointer[i]);
            }
        }

        private static void OnAddEachIterPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnAdd.Pointer != default)
            {
                delegate*<Iter, int, T0*, void> callback = (delegate*<Iter, int, T0*, void>)context->OnAdd.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, &pointer[i]);
            }
            else
            {
                Ecs.EachIterPointerCallback<T0> callback = (Ecs.EachIterPointerCallback<T0>)context->OnAdd.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, &pointer[i]);
            }
        }

        private static void OnSetIterFieldCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Iter, Field<T0>, void> callback = (delegate*<Iter, Field<T0>, void>)context->OnSet.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.Field<T0>(0));
            }
            else
            {
                Ecs.IterFieldCallback<T0> callback = (Ecs.IterFieldCallback<T0>)context->OnSet.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.Field<T0>(0));
            }
        }

        private static void OnSetIterSpanCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Iter, Span<T0>, void> callback = (delegate*<Iter, Span<T0>, void>)context->OnSet.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetSpan<T0>(0));
            }
            else
            {
                Ecs.IterSpanCallback<T0> callback = (Ecs.IterSpanCallback<T0>)context->OnSet.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetSpan<T0>(0));
            }
        }

        private static void OnSetIterPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Iter, T0*, void> callback = (delegate*<Iter, T0*, void>)context->OnSet.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetPointer<T0>(0));
            }
            else
            {
                Ecs.IterPointerCallback<T0> callback = (Ecs.IterPointerCallback<T0>)context->OnSet.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetPointer<T0>(0));
            }
        }

        private static void OnSetEachRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<ref T0, void> callback = (delegate*<ref T0, void>)context->OnSet.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachRefCallback<T0> callback = (Ecs.EachRefCallback<T0>)context->OnSet.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnSetEachEntityRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Entity, ref T0, void> callback = (delegate*<Entity, ref T0, void>)context->OnSet.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachEntityRefCallback<T0> callback = (Ecs.EachEntityRefCallback<T0>)context->OnSet.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnSetEachIterRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Iter, int, ref T0, void> callback = (delegate*<Iter, int, ref T0, void>)context->OnSet.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachIterRefCallback<T0> callback = (Ecs.EachIterRefCallback<T0>)context->OnSet.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnSetEachPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<T0*, void> callback = (delegate*<T0*, void>)context->OnSet.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(&pointer[i]);
            }
            else
            {
                Ecs.EachPointerCallback<T0> callback = (Ecs.EachPointerCallback<T0>)context->OnSet.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(&pointer[i]);
            }
        }

        private static void OnSetEachEntityPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Entity, T0*, void> callback = (delegate*<Entity, T0*, void>)context->OnSet.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), &pointer[i]);
            }
            else
            {
                Ecs.EachEntityPointerCallback<T0> callback = (Ecs.EachEntityPointerCallback<T0>)context->OnSet.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), &pointer[i]);
            }
        }

        private static void OnSetEachIterPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnSet.Pointer != default)
            {
                delegate*<Iter, int, T0*, void> callback = (delegate*<Iter, int, T0*, void>)context->OnSet.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, &pointer[i]);
            }
            else
            {
                Ecs.EachIterPointerCallback<T0> callback = (Ecs.EachIterPointerCallback<T0>)context->OnSet.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, &pointer[i]);
            }
        }

        private static void OnRemoveIterFieldCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Iter, Field<T0>, void> callback = (delegate*<Iter, Field<T0>, void>)context->OnRemove.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.Field<T0>(0));
            }
            else
            {
                Ecs.IterFieldCallback<T0> callback = (Ecs.IterFieldCallback<T0>)context->OnRemove.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.Field<T0>(0));
            }
        }

        private static void OnRemoveIterSpanCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Iter, Span<T0>, void> callback = (delegate*<Iter, Span<T0>, void>)context->OnRemove.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetSpan<T0>(0));
            }
            else
            {
                Ecs.IterSpanCallback<T0> callback = (Ecs.IterSpanCallback<T0>)context->OnRemove.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetSpan<T0>(0));
            }
        }

        private static void OnRemoveIterPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Iter, T0*, void> callback = (delegate*<Iter, T0*, void>)context->OnRemove.Pointer;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetPointer<T0>(0));
            }
            else
            {
                Ecs.IterPointerCallback<T0> callback = (Ecs.IterPointerCallback<T0>)context->OnRemove.GcHandle.Target!;
                Iter it = new Iter(iter);
                for (int i = 0; i < iter->count; i++)
                    callback(it, it.GetPointer<T0>(0));
            }
        }

        private static void OnRemoveEachRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<ref T0, void> callback = (delegate*<ref T0, void>)context->OnRemove.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachRefCallback<T0> callback = (Ecs.EachRefCallback<T0>)context->OnRemove.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnRemoveEachEntityRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Entity, ref T0, void> callback = (delegate*<Entity, ref T0, void>)context->OnRemove.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachEntityRefCallback<T0> callback = (Ecs.EachEntityRefCallback<T0>)context->OnRemove.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnRemoveEachIterRefCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Iter, int, ref T0, void> callback = (delegate*<Iter, int, ref T0, void>)context->OnRemove.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
            else
            {
                Ecs.EachIterRefCallback<T0> callback = (Ecs.EachIterRefCallback<T0>)context->OnRemove.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, ref Managed.GetTypeRef<T0>(&pointer[i]));
            }
        }

        private static void OnRemoveEachPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<T0*, void> callback = (delegate*<T0*, void>)context->OnRemove.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(&pointer[i]);
            }
            else
            {
                Ecs.EachPointerCallback<T0> callback = (Ecs.EachPointerCallback<T0>)context->OnRemove.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(&pointer[i]);
            }
        }

        private static void OnRemoveEachEntityPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Entity, T0*, void> callback = (delegate*<Entity, T0*, void>)context->OnRemove.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), &pointer[i]);
            }
            else
            {
                Ecs.EachEntityPointerCallback<T0> callback = (Ecs.EachEntityPointerCallback<T0>)context->OnRemove.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Entity(iter->world, iter->entities[i]), &pointer[i]);
            }
        }

        private static void OnRemoveEachIterPointerCallback(ecs_iter_t* iter)
        {
            BindingContext.TypeHooksContext* context = (BindingContext.TypeHooksContext*)iter->callback_ctx;

            if (context->OnRemove.Pointer != default)
            {
                delegate*<Iter, int, T0*, void> callback = (delegate*<Iter, int, T0*, void>)context->OnRemove.Pointer;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, &pointer[i]);
            }
            else
            {
                Ecs.EachIterPointerCallback<T0> callback = (Ecs.EachIterPointerCallback<T0>)context->OnRemove.GcHandle.Target!;
                T0* pointer = (T0*)iter->ptrs[0];
                for (int i = 0; i < iter->count; i++)
                    callback(new Iter(iter), i, &pointer[i]);
            }
        }
    }
}
