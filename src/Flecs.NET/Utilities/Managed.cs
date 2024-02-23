using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Core;

namespace Flecs.NET.Utilities
{
    /// <summary>
    ///     Static class for working with managed memory in flecs.
    /// </summary>
    public static unsafe class Managed
    {
        /// <summary>
        ///     Gets the managed size of a type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int ManagedSize<T>()
        {
            return RuntimeHelpers.IsReferenceOrContainsReferences<T>() ? sizeof(IntPtr) : sizeof(T);
        }

        internal static void FreeGcHandle(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
                GCHandle.FromIntPtr(handle).Free();
        }

        internal static void FreeGcHandle(GCHandle handle)
        {
            if ((IntPtr)handle != IntPtr.Zero)
                handle.Free();
        }

        internal static void FreeGcHandle(void* data, int index = 0)
        {
            IntPtr handle = ((IntPtr*)data)[index];
            FreeGcHandle(handle);
        }

        internal static void* AllocGcHandle<T>(void* data, T comp, int index = 0)
        {
            return AllocGcHandle(data, ref comp, index);
        }

        internal static void* AllocGcHandle<T>(void* data, ref T comp, int index = 0)
        {
            GCHandle handle = GCHandle.Alloc(new BindingContext.Box<T>(comp, true));
            ((IntPtr*)data)[index] = GCHandle.ToIntPtr(handle);
            return data;
        }

        internal static void SetTypeRef<T>(void* data, T item, int index = 0)
        {
            SetTypeRef(data, ref item, index);
        }

        internal static void SetTypeRef<T>(void* data, ref T item, int index = 0)
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                AllocGcHandle((IntPtr*)data, ref item, index);
                return;
            }

            ((T*)data)[index] = item;
        }

        internal static void SetTypeRef<T>(GCHandle handle, ref T item)
        {
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            box.Value = item;
        }

        internal static ref T GetTypeRef<T>(void* data, int index = 0)
        {
            Ecs.Assert(data != null, "Provided pointer is null.");

            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return ref ((T*)data)[index];

            GCHandle handle = GCHandle.FromIntPtr(((IntPtr*)data)[index]);
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            return ref box.Value!;
        }

        internal static ref T GetTypeRef<T>(IntPtr data, int index = 0)
        {
            return ref GetTypeRef<T>((void*)data, index);
        }

        internal static ref T GetTypeRef<T>(GCHandle handle)
        {
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            return ref box.Value!;
        }
    }
}
