using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Flecs.NET.Polyfill;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class holding methods and types for binding contexts.
    /// </summary>
    public static unsafe class BindingContext
    {
#if NET5_0_OR_GREATER
        internal static readonly IntPtr ObserverIterPointer =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&ObserverIter;

        internal static readonly IntPtr RoutineIterPointer =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&RoutineIter;

        internal static readonly IntPtr ObserverEachEntityPointer =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&ObserverEachEntity;

        internal static readonly IntPtr RoutineEachEntityPointer =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&RoutineEachEntity;

        internal static readonly IntPtr WorldContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&WorldContextFree;

        internal static readonly IntPtr ObserverContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&ObserverContextFree;

        internal static readonly IntPtr RoutineContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&RoutineContextFree;

        internal static readonly IntPtr QueryContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&QueryContextFree;

        internal static readonly IntPtr TypeHooksContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&TypeHooksContextFree;

        internal static readonly IntPtr OsApiAbortPointer =
            (IntPtr)(delegate* unmanaged<void>)&OsApiAbort;
#else
        internal static readonly IntPtr ObserverIterPointer;
        internal static readonly IntPtr RoutineIterPointer;

        internal static readonly IntPtr ObserverEachEntityPointer;
        internal static readonly IntPtr RoutineEachEntityPointer;

        internal static readonly IntPtr WorldContextFreePointer;
        internal static readonly IntPtr ObserverContextFreePointer;
        internal static readonly IntPtr RoutineContextFreePointer;
        internal static readonly IntPtr QueryContextFreePointer;

        internal static readonly IntPtr TypeHooksContextFreePointer;

        internal static readonly IntPtr OsApiAbortPointer;

        private static readonly Ecs.IterAction ObserverIterReference = ObserverIter;
        private static readonly Ecs.IterAction RoutineIterReference = RoutineIter;

        private static readonly Ecs.IterAction ObserverEachEntityReference = ObserverEachEntity;
        private static readonly Ecs.IterAction RoutineEachEntityReference = RoutineEachEntity;

        private static readonly Ecs.ContextFree WorldContextFreeReference = WorldContextFree;
        private static readonly Ecs.ContextFree ObserverContextFreeReference = ObserverContextFree;
        private static readonly Ecs.ContextFree RoutineContextFreeReference = RoutineContextFree;
        private static readonly Ecs.ContextFree QueryContextFreeReference = QueryContextFree;

        private static readonly Ecs.ContextFree TypeHooksContextFreeReference = TypeHooksContextFree;

        private static readonly Action OsApiAbortReference = OsApiAbort;

        [SuppressMessage("Usage", "CA1810")]
        static BindingContext()
        {
            ObserverIterPointer = Marshal.GetFunctionPointerForDelegate(ObserverIterReference);
            RoutineIterPointer = Marshal.GetFunctionPointerForDelegate(RoutineIterReference);

            ObserverEachEntityPointer = Marshal.GetFunctionPointerForDelegate(ObserverEachEntityReference);
            RoutineEachEntityPointer = Marshal.GetFunctionPointerForDelegate(RoutineEachEntityReference);

            WorldContextFreePointer = Marshal.GetFunctionPointerForDelegate(WorldContextFreeReference);
            ObserverContextFreePointer = Marshal.GetFunctionPointerForDelegate(ObserverContextFreeReference);
            RoutineContextFreePointer = Marshal.GetFunctionPointerForDelegate(RoutineContextFreeReference);
            QueryContextFreePointer = Marshal.GetFunctionPointerForDelegate(QueryContextFreeReference);

            TypeHooksContextFreePointer = Marshal.GetFunctionPointerForDelegate(TypeHooksContextFreeReference);

            OsApiAbortPointer = Marshal.GetFunctionPointerForDelegate(OsApiAbortReference);
        }
#endif

        [UnmanagedCallersOnly]
        private static void ObserverIter(ecs_iter_t* iter)
        {
            ObserverContext* context = (ObserverContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            delegate* unmanaged<Iter, void> callback = (delegate* unmanaged<Iter, void>)context->Iterator.Function;
#else
            Ecs.IterCallback callback =
                Marshal.GetDelegateForFunctionPointer<Ecs.IterCallback>(context->Iterator.Function);
#endif

            Invoker.Iter(iter, callback);
        }

        [UnmanagedCallersOnly]
        private static void RoutineIter(ecs_iter_t* iter)
        {
            RoutineContext* context = (RoutineContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            delegate* unmanaged<Iter, void> callback = (delegate* unmanaged<Iter, void>)context->Iterator.Function;
#else
            Ecs.IterCallback callback =
                Marshal.GetDelegateForFunctionPointer<Ecs.IterCallback>(context->Iterator.Function);
#endif

            Invoker.Iter(iter, callback);
        }

        [UnmanagedCallersOnly]
        private static void ObserverEachEntity(ecs_iter_t* iter)
        {
            ObserverContext* context = (ObserverContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            delegate* unmanaged<Entity, void> callback = (delegate* unmanaged<Entity, void>)context->Iterator.Function;
#else
            Ecs.EachEntityCallback callback =
                Marshal.GetDelegateForFunctionPointer<Ecs.EachEntityCallback>(context->Iterator.Function);
#endif

            Invoker.EachEntity(iter, callback);
        }

        [UnmanagedCallersOnly]
        private static void RoutineEachEntity(ecs_iter_t* iter)
        {
            RoutineContext* context = (RoutineContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            delegate* unmanaged<Entity, void> callback = (delegate* unmanaged<Entity, void>)context->Iterator.Function;
#else
            Ecs.EachEntityCallback callback =
                Marshal.GetDelegateForFunctionPointer<Ecs.EachEntityCallback>(context->Iterator.Function);
#endif

            Invoker.EachEntity(iter, callback);
        }

        [UnmanagedCallersOnly]
        private static void WorldContextFree(void* context)
        {
            WorldContext* worldContext = (WorldContext*)context;
            worldContext->Dispose();
            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void ObserverContextFree(void* context)
        {
            ObserverContext* observerContext = (ObserverContext*)context;
            observerContext->Dispose();
            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void RoutineContextFree(void* context)
        {
            RoutineContext* routineContext = (RoutineContext*)context;
            routineContext->Dispose();
            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void QueryContextFree(void* context)
        {
            QueryContext* queryContext = (QueryContext*)context;
            queryContext->Dispose();
            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void TypeHooksContextFree(void* context)
        {
            TypeHooksContext* typeHooks = (TypeHooksContext*)context;
            typeHooks->Dispose();
            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void OsApiAbort()
        {
            Console.WriteLine(Environment.StackTrace);

#if NET5_0_OR_GREATER
            ((delegate* unmanaged<void>)FlecsInternal.OsAbortNative)();
#else
            Marshal.GetDelegateForFunctionPointer<Action>(FlecsInternal.OsAbortNative)();
#endif
        }

        private static void FreeCallback(ref Callback dest)
        {
            Managed.FreeGcHandle(dest.GcHandle);
            dest = default;
        }

        internal static Callback AllocCallback<T>(T? callback) where T : Delegate
        {
            return callback == null
                ? new Callback(IntPtr.Zero, IntPtr.Zero)
                : new Callback(Marshal.GetFunctionPointerForDelegate(callback), (IntPtr)GCHandle.Alloc(callback));
        }

        internal static void SetCallback<T>(ref Callback dest, T? callback) where T : Delegate
        {
            if (dest.GcHandle != IntPtr.Zero)
                FreeCallback(ref dest);

            dest = AllocCallback(callback);
        }

        internal struct Callback
        {
            public IntPtr Function;
            public IntPtr GcHandle;

            public Callback(IntPtr function, IntPtr gcHandle)
            {
                Function = function;
                GcHandle = gcHandle;
            }
        }

        internal struct WorldContext : IDisposable
        {
            public Callback AtFini;
            public Callback RunPostFrame;
            public Callback ContextFree;

            public void Dispose()
            {
                FreeCallback(ref AtFini);
                FreeCallback(ref RunPostFrame);
                FreeCallback(ref ContextFree);
            }
        }

        internal struct ObserverContext : IDisposable
        {
            public Callback Iterator;
            public Callback Run;

            public void Dispose()
            {
                FreeCallback(ref Iterator);
                FreeCallback(ref Run);
            }
        }

        internal struct RoutineContext : IDisposable
        {
            public Callback Iterator;
            public Callback Run;

            public QueryContext QueryContext;

            public void Dispose()
            {
                FreeCallback(ref Iterator);
                FreeCallback(ref Run);

                QueryContext.Dispose();
            }
        }

        internal struct QueryContext : IDisposable
        {
            public Callback OrderByAction;
            public Callback GroupByAction;
            public Callback ContextFree;
            public Callback GroupCreateAction;
            public Callback GroupDeleteAction;

            public void Dispose()
            {
                FreeCallback(ref OrderByAction);
                FreeCallback(ref GroupByAction);
                FreeCallback(ref ContextFree);
                FreeCallback(ref GroupCreateAction);
                FreeCallback(ref GroupDeleteAction);
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
                FreeCallback(ref Ctor);
                FreeCallback(ref Dtor);
                FreeCallback(ref Move);
                FreeCallback(ref Copy);
                FreeCallback(ref OnAdd);
                FreeCallback(ref OnSet);
                FreeCallback(ref OnRemove);
                FreeCallback(ref ContextFree);
            }
        }
    }
}
