using System;
using System.Runtime.InteropServices;
using Flecs.NET.Polyfill;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class holding methods and types for binding contexts.
    /// </summary>
    public static unsafe partial class BindingContext
    {
        private static readonly BindingContextCleanup _ = new BindingContextCleanup();

        internal static readonly byte* DefaultSeparator = (byte*)Marshal.StringToHGlobalAnsi(".");
        internal static readonly byte* DefaultRootSeparator = (byte*)Marshal.StringToHGlobalAnsi("::");

#if NET5_0_OR_GREATER
        internal static readonly IntPtr ObserverIterPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&ObserverIter;

        internal static readonly IntPtr RoutineIterPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&RoutineIter;

        internal static readonly IntPtr ObserverEachEntityPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&ObserverEachEntity;

        internal static readonly IntPtr RoutineEachEntityPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&RoutineEachEntity;

        internal static readonly IntPtr ObserverEachIndexPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&ObserverEachIndex;

        internal static readonly IntPtr RoutineEachIndexPointer =
            (IntPtr)(delegate* <ecs_iter_t*, void>)&RoutineEachIndex;

        internal static readonly IntPtr WorldContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&WorldContextFree;

        internal static readonly IntPtr ObserverContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&ObserverContextFree;

        internal static readonly IntPtr RoutineContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&RoutineContextFree;

        internal static readonly IntPtr QueryContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&QueryContextFree;

        internal static readonly IntPtr TypeHooksContextFreePointer =
            (IntPtr)(delegate* <void*, void>)&TypeHooksContextFree;

        internal static readonly IntPtr OsApiAbortPointer =
            (IntPtr)(delegate* <void>)&OsApiAbort;
#else
        internal static readonly IntPtr ObserverIterPointer =
            Marshal.GetFunctionPointerForDelegate(ObserverIterReference = ObserverIter);

        internal static readonly IntPtr RoutineIterPointer =
            Marshal.GetFunctionPointerForDelegate(RoutineIterReference = RoutineIter);

        internal static readonly IntPtr ObserverEachEntityPointer =
            Marshal.GetFunctionPointerForDelegate(ObserverEachEntityReference = ObserverEachEntity);

        internal static readonly IntPtr RoutineEachEntityPointer =
            Marshal.GetFunctionPointerForDelegate(RoutineEachEntityReference = RoutineEachEntity);

        internal static readonly IntPtr ObserverEachIndexPointer =
            Marshal.GetFunctionPointerForDelegate(ObserverEachIndexReference = ObserverEachIndex);

        internal static readonly IntPtr RoutineEachIndexPointer =
            Marshal.GetFunctionPointerForDelegate(RoutineEachIndexReference = RoutineEachIndex);

        internal static readonly IntPtr WorldContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(WorldContextFreeReference = WorldContextFree);

        internal static readonly IntPtr ObserverContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(ObserverContextFreeReference = ObserverContextFree);

        internal static readonly IntPtr RoutineContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(RoutineContextFreeReference = RoutineContextFree);

        internal static readonly IntPtr QueryContextFreePointer =
            Marshal.GetFunctionPointerForDelegate(QueryContextFreeReference = QueryContextFree);

        internal static readonly IntPtr TypeHooksContextFreePointer=
            Marshal.GetFunctionPointerForDelegate(TypeHooksContextFreeReference = TypeHooksContextFree);

        internal static readonly IntPtr OsApiAbortPointer =
            Marshal.GetFunctionPointerForDelegate(OsApiAbortReference = OsApiAbort);

        private static readonly Ecs.IterAction ObserverIterReference;
        private static readonly Ecs.IterAction RoutineIterReference;

        private static readonly Ecs.IterAction ObserverEachEntityReference;
        private static readonly Ecs.IterAction RoutineEachEntityReference;

        private static readonly Ecs.IterAction ObserverEachIndexReference;
        private static readonly Ecs.IterAction RoutineEachIndexReference;

        private static readonly Ecs.ContextFree WorldContextFreeReference;
        private static readonly Ecs.ContextFree ObserverContextFreeReference;
        private static readonly Ecs.ContextFree RoutineContextFreeReference;
        private static readonly Ecs.ContextFree QueryContextFreeReference;

        private static readonly Ecs.ContextFree TypeHooksContextFreeReference;

        private static readonly Action OsApiAbortReference;
#endif

        private static void ObserverIter(ecs_iter_t* iter)
        {
            ObserverContext* context = (ObserverContext*)iter->binding_ctx;
            Ecs.IterCallback callback = (Ecs.IterCallback)context->Iterator.GcHandle.Target!;
            Invoker.Iter(iter, callback);
        }

        private static void RoutineIter(ecs_iter_t* iter)
        {
            RoutineContext* context = (RoutineContext*)iter->binding_ctx;
            Ecs.IterCallback callback = (Ecs.IterCallback)context->Iterator.GcHandle.Target!;
            Invoker.Iter(iter, callback);
        }

        private static void ObserverEachEntity(ecs_iter_t* iter)
        {
            ObserverContext* context = (ObserverContext*)iter->binding_ctx;
            Ecs.EachEntityCallback callback = (Ecs.EachEntityCallback)context->Iterator.GcHandle.Target!;
            Invoker.Each(iter, callback);
        }

        private static void RoutineEachEntity(ecs_iter_t* iter)
        {
            RoutineContext* context = (RoutineContext*)iter->binding_ctx;
            Ecs.EachEntityCallback callback = (Ecs.EachEntityCallback)context->Iterator.GcHandle.Target!;
            Invoker.Each(iter, callback);
        }

        private static void ObserverEachIndex(ecs_iter_t* iter)
        {
            ObserverContext* context = (ObserverContext*)iter->binding_ctx;
            Ecs.EachIndexCallback callback = (Ecs.EachIndexCallback)context->Iterator.GcHandle.Target!;
            Invoker.Each(iter, callback);
        }

        private static void RoutineEachIndex(ecs_iter_t* iter)
        {
            RoutineContext* context = (RoutineContext*)iter->binding_ctx;
            Ecs.EachIndexCallback callback = (Ecs.EachIndexCallback)context->Iterator.GcHandle.Target!;
            Invoker.Each(iter, callback);
        }

        private static void WorldContextFree(void* context)
        {
            WorldContext* worldContext = (WorldContext*)context;
            worldContext->Dispose();
            Memory.Free(context);
        }

        private static void ObserverContextFree(void* context)
        {
            ObserverContext* observerContext = (ObserverContext*)context;
            observerContext->Dispose();
            Memory.Free(context);
        }

        private static void RoutineContextFree(void* context)
        {
            RoutineContext* routineContext = (RoutineContext*)context;
            routineContext->Dispose();
            Memory.Free(context);
        }

        private static void QueryContextFree(void* context)
        {
            QueryContext* queryContext = (QueryContext*)context;
            queryContext->Dispose();
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
            Console.WriteLine(Environment.StackTrace);
            Marshal.GetDelegateForFunctionPointer<Action>(FlecsInternal.OsAbortNative)();
        }

        private static void FreeCallback(ref Callback dest)
        {
            Managed.FreeGcHandle(dest.GcHandle);
            dest = default;
        }

        internal static Callback AllocCallback<T>(T? callback, bool storePtr = true) where T : Delegate
        {
            if (callback == null)
                return default;

            IntPtr funcPtr = storePtr ? Marshal.GetFunctionPointerForDelegate(callback) : IntPtr.Zero;
            return new Callback(funcPtr, GCHandle.Alloc(callback));
        }

        internal static void SetCallback<T>(ref Callback dest, T? callback, bool storePtr = true) where T : Delegate
        {
            if (dest.GcHandle != default)
                FreeCallback(ref dest);

            dest = AllocCallback(callback, storePtr);
        }

        internal struct Callback
        {
            public IntPtr Function;
            public GCHandle GcHandle;

            public Callback(IntPtr function, GCHandle gcHandle)
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

        // Free native resources on program exit
        private class BindingContextCleanup
        {
            ~BindingContextCleanup()
            {
                Memory.Free(DefaultSeparator);
                Memory.Free(DefaultRootSeparator);
            }
        }
    }
}
