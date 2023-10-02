using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Resources
{
    /// <summary>
    ///     A handle for storing references to managed objects from inside an unmanaged struct.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct NativeHandle<T> : IDisposable
    {
        private GCHandle _handle;

        /// <summary>
        ///     Represents the object that the native handle references.
        /// </summary>
        public ref T Target => ref Managed.GetTypeRef<T>(_handle);

        /// <summary>
        ///     Frees the native handle.
        /// </summary>
        public void Dispose()
        {
            Managed.FreeGcHandle(_handle);
        }

        /// <summary>
        ///     Allocates a native handle that references nothing.
        /// </summary>
        /// <returns></returns>
        public static NativeHandle<T> Alloc()
        {
            return new NativeHandle<T> { _handle = GCHandle.Alloc(new StrongBox<T>()) };
        }

        /// <summary>
        ///     Allocates a native handle that references the provided object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static NativeHandle<T> Alloc(T obj)
        {
            return new NativeHandle<T> { _handle = GCHandle.Alloc(new StrongBox<T>(obj)) };
        }
    }
}
