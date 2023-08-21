using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Flecs.NET.Polyfill;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe class BindingContext
    {
#if NET5_0_OR_GREATER
        internal static readonly IntPtr RoutineIterPointer =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&RoutineIter;

        internal static readonly IntPtr ObserverIterPointer =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&ObserverIter;

        internal static readonly IntPtr ObserverContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&ObserverContextFree;

        internal static readonly IntPtr RoutineContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&RoutineContextFree;

        internal static readonly IntPtr QueryContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&QueryContextFree;

        internal static readonly IntPtr TypeHooksContextFreePointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&TypeHooksContextFree;
#else
        internal static readonly IntPtr ObserverIterPointer;
        internal static readonly IntPtr RoutineIterPointer;

        internal static readonly IntPtr ObserverContextFreePointer;
        internal static readonly IntPtr RoutineContextFreePointer;
        internal static readonly IntPtr QueryContextFreePointer;

        internal static readonly IntPtr TypeHooksContextFreePointer;

        private static readonly Ecs.IterAction ObserverIterReference = ObserverIter;
        private static readonly Ecs.IterAction RoutineIterReference = RoutineIter;

        private static readonly Ecs.ContextFree ObserverContextFreeReference = ObserverContextFree;
        private static readonly Ecs.ContextFree RoutineContextFreeReference = RoutineContextFree;
        private static readonly Ecs.ContextFree QueryContextFreeReference = QueryContextFree;

        private static readonly Ecs.ContextFree TypeHooksContextFreeReference = TypeHooksContextFree;

        [SuppressMessage("Usage", "CA1810")]
        static BindingContext()
        {
            ObserverIterPointer = Marshal.GetFunctionPointerForDelegate(ObserverIterReference);
            RoutineIterPointer = Marshal.GetFunctionPointerForDelegate(RoutineIterReference);

            ObserverContextFreePointer = Marshal.GetFunctionPointerForDelegate(ObserverContextFreeReference);
            RoutineContextFreePointer = Marshal.GetFunctionPointerForDelegate(RoutineContextFreeReference);
            QueryContextFreePointer = Marshal.GetFunctionPointerForDelegate(QueryContextFreeReference);

            TypeHooksContextFreePointer = Marshal.GetFunctionPointerForDelegate(TypeHooksContextFreeReference);
        }
#endif

        [UnmanagedCallersOnly]
        public static void ObserverIter(ecs_iter_t* iter)
        {
            ObserverContext* context = (ObserverContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            ((delegate* unmanaged<Iter, void>)context->Iter.Function)(new Iter(iter));
#else
            Marshal.GetDelegateForFunctionPointer<Ecs.IterCallback>(context->Iter.Function)(new Iter(iter));
#endif
        }

        [UnmanagedCallersOnly]
        public static void RoutineIter(ecs_iter_t* iter)
        {
            RoutineContext* context = (RoutineContext*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            ((delegate* unmanaged<Iter, void>)context->Iter.Function)(new Iter(iter));
#else
            Marshal.GetDelegateForFunctionPointer<Ecs.IterCallback>(context->Iter.Function)(new Iter(iter));
#endif
        }

        [UnmanagedCallersOnly]
        private static void ObserverContextFree(void* context)
        {
            ObserverContext* observerContext = (ObserverContext*)context;

            FreeCallback(ref observerContext->Iter);
            FreeCallback(ref observerContext->Run);

            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void RoutineContextFree(void* context)
        {
            RoutineContext* routineContext = (RoutineContext*)context;

            FreeCallback(ref routineContext->QueryContext.OrderByAction);
            FreeCallback(ref routineContext->QueryContext.GroupByAction);
            FreeCallback(ref routineContext->QueryContext.ContextFree);
            FreeCallback(ref routineContext->QueryContext.GroupCreateAction);
            FreeCallback(ref routineContext->QueryContext.GroupDeleteAction);

            FreeCallback(ref routineContext->Iter);
            FreeCallback(ref routineContext->Run);

            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void QueryContextFree(void* context)
        {
            QueryContext* queryContext = (QueryContext*)context;

            FreeCallback(ref queryContext->OrderByAction);
            FreeCallback(ref queryContext->GroupByAction);
            FreeCallback(ref queryContext->ContextFree);
            FreeCallback(ref queryContext->GroupCreateAction);
            FreeCallback(ref queryContext->GroupDeleteAction);

            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void TypeHooksContextFree(void* context)
        {
            TypeHooksContext* typeHooks = (TypeHooksContext*)context;
            FreeCallback(ref typeHooks->Ctor);
            FreeCallback(ref typeHooks->Dtor);
            FreeCallback(ref typeHooks->Move);
            FreeCallback(ref typeHooks->Copy);
            FreeCallback(ref typeHooks->OnAdd);
            FreeCallback(ref typeHooks->OnSet);
            FreeCallback(ref typeHooks->OnRemove);
            FreeCallback(ref typeHooks->ContextFree);
            Memory.Free(context);
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

        internal struct ObserverContext
        {
            public Callback Iter;
            public Callback Run;
        }

        internal struct RoutineContext
        {
            public QueryContext QueryContext;
            public Callback Iter;
            public Callback Run;
        }

        internal struct QueryContext
        {
            public Callback OrderByAction;
            public Callback GroupByAction;
            public Callback ContextFree;
            public Callback GroupCreateAction;
            public Callback GroupDeleteAction;
        }

        internal struct TypeHooksContext
        {
            public Callback Ctor;
            public Callback Dtor;
            public Callback Move;
            public Callback Copy;
            public Callback OnAdd;
            public Callback OnSet;
            public Callback OnRemove;
            public Callback ContextFree;
        }
    }
}
