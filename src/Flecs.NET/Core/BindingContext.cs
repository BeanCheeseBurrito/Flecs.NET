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
        internal static readonly IntPtr IterPointer; =
            (IntPtr)(delegate* unmanaged<ecs_iter_t*, void>)&RoutineIter;

        internal static readonly IntPtr FreeIterPointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&RoutineFree;

        internal static readonly IntPtr FreeTypeHooksPointer =
            (IntPtr)(delegate* unmanaged<void*, void>)&FreeTypeHooks;
#else
        internal static readonly IntPtr IterPointer;
        internal static readonly IntPtr FreeIterPointer;

        internal static readonly IntPtr FreeTypeHooksPointer;

        private static readonly Ecs.IterAction IterReference = Iter;
        private static readonly Ecs.ContextFree FreeIterReference = FreeIter;

        private static readonly Ecs.ContextFree FreeTypeHooksReference = FreeTypeHooks;

        [SuppressMessage("Usage", "CA1810")]
        static BindingContext()
        {
            IterPointer = Marshal.GetFunctionPointerForDelegate(IterReference);
            FreeIterPointer = Marshal.GetFunctionPointerForDelegate(FreeIterReference);

            FreeTypeHooksPointer = Marshal.GetFunctionPointerForDelegate(FreeTypeHooksReference);
        }
#endif

        [UnmanagedCallersOnly]
        public static void Iter(ecs_iter_t* iter)
        {
            Callback* context = (Callback*)iter->binding_ctx;

#if NET5_0_OR_GREATER
            ((delegate* unmanaged<Iter, void>)context->Function)(new Iter(iter));
#else
            Marshal.GetDelegateForFunctionPointer<Ecs.IterCallback>(context->Function)(new Iter(iter));
#endif
        }

        [UnmanagedCallersOnly]
        private static void FreeIter(void* context)
        {
            FreeCallback(context);
            Memory.Free(context);
        }

        [UnmanagedCallersOnly]
        private static void FreeTypeHooks(void* context)
        {
            TypeHooks* typeHooks = (TypeHooks*)context;
            FreeCallback(&typeHooks->Ctor);
            FreeCallback(&typeHooks->Dtor);
            FreeCallback(&typeHooks->Move);
            FreeCallback(&typeHooks->Copy);
            FreeCallback(&typeHooks->OnAdd);
            FreeCallback(&typeHooks->OnSet);
            FreeCallback(&typeHooks->OnRemove);
            FreeCallback(&typeHooks->ContextFree);
            Memory.Free(context);
        }

        private static void FreeCallback(void* context)
        {
            Callback* callback = (Callback*)context;

            if (callback->GcHandle != IntPtr.Zero)
                GCHandle.FromIntPtr(callback->GcHandle).Free();
        }

        internal static Callback AllocCallback<T>(T? callback) where T : Delegate
        {
            return callback == null
                ? new Callback(IntPtr.Zero, IntPtr.Zero)
                : new Callback(Marshal.GetFunctionPointerForDelegate(callback), (IntPtr)GCHandle.Alloc(callback));
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

        internal struct TypeHooks
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
