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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FreeGcHandle(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
                GCHandle.FromIntPtr(handle).Free();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FreeGcHandle(GCHandle handle)
        {
            if ((IntPtr)handle != IntPtr.Zero)
                handle.Free();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FreeGcHandle(void* data, int index = 0)
        {
            IntPtr handle = ((IntPtr*)data)[index];
            FreeGcHandle(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void* AllocGcHandle<T>(void* data, T comp, int index = 0)
        {
            return AllocGcHandle(data, ref comp, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void* AllocGcHandle<T>(void* data, ref T comp, int index = 0)
        {
            GCHandle handle = GCHandle.Alloc(new BindingContext.Box<T>(comp, true));
            ((IntPtr*)data)[index] = GCHandle.ToIntPtr(handle);
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetTypeRef<T>(void* data, T item, int index = 0)
        {
            SetTypeRef(data, ref item, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetTypeRef<T>(void* data, ref T item, int index = 0)
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                AllocGcHandle((IntPtr*)data, ref item, index);
                return;
            }

            ((T*)data)[index] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetTypeRef<T>(GCHandle handle, ref T item)
        {
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            box.Value = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref T GetTypeRef<T>(void* data)
        {
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return ref Unsafe.AsRef<T>(data);

            GCHandle handle = GCHandle.FromIntPtr(*(IntPtr*)data);
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            return ref box.Value!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref T GetTypeRef<T>(void* data, int index)
        {
            if (data == null)
                return ref Unsafe.NullRef<T>();

            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return ref ((T*)data)[index];

            GCHandle handle = GCHandle.FromIntPtr(((IntPtr*)data)[index]);
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            return ref box.Value!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref T GetTypeRef<T>(IntPtr data)
        {
            return ref GetTypeRef<T>((void*)data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref T GetTypeRef<T>(IntPtr data, int index)
        {
            return ref GetTypeRef<T>((void*)data, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref T GetTypeRef<T>(GCHandle handle)
        {
            BindingContext.Box<T> box = (BindingContext.Box<T>)handle.Target!;
            return ref box.Value!;
        }
    }
}
