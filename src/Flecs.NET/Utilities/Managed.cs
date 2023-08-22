using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Flecs.NET.Utilities
{
    public static unsafe class Managed
    {
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
            GCHandle handle = GCHandle.Alloc(new StrongBox<T>(comp));
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

        internal static ref T GetTypeRef<T>(void* data, int index = 0)
        {
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return ref ((T*)data)[index];

            GCHandle handle = GCHandle.FromIntPtr(((IntPtr*)data)[index]);
            StrongBox<T> obj = (StrongBox<T>)handle.Target!;
            return ref obj.Value;
        }
    }
}