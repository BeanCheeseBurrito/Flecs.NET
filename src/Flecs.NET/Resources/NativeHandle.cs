using System;
using System.Runtime.InteropServices;
using Flecs.NET.Core;
using Flecs.NET.Utilities;

namespace Flecs.NET.Resources
{
    /// <summary>
    ///     A handle for storing references to managed objects from inside an unmanaged struct.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct NativeHandle<T> : IDisposable, IEquatable<NativeHandle<T>>
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
            return new NativeHandle<T> { _handle = GCHandle.Alloc(new BindingContext.Box<T>()) };
        }

        /// <summary>
        ///     Allocates a native handle that references the provided object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static NativeHandle<T> Alloc(T obj)
        {
            return new NativeHandle<T> { _handle = GCHandle.Alloc(new BindingContext.Box<T>(obj)) };
        }

        /// <summary>
        ///     Checks if two <see cref="NativeHandle{T}"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NativeHandle<T> other)
        {
            return _handle == other._handle;
        }

        /// <summary>
        ///     Checks if two <see cref="NativeHandle{T}"/> instance are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is NativeHandle<T> other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="NativeHandle{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _handle.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="NativeHandle{T}"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NativeHandle<T> left, NativeHandle<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="NativeHandle{T}"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NativeHandle<T> left, NativeHandle<T> right)
        {
            return !(left == right);
        }
    }
}
