using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    /// <summary>
    /// Unsafe array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct UnsafeArray<T> : IDisposable
    {
        /// <summary>
        /// Data storage for the array.
        /// </summary>
        public void* Data { get; private set; }

        /// <summary>
        /// The length of the array.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Represents whether or not the array is null.
        /// </summary>
        public readonly bool IsNull => Data == null;

        /// <summary>
        /// Creates an unsafe array with the provided length.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="isZeroed"></param>
        public UnsafeArray(int length, bool isZeroed = true)
        {
            if (isZeroed)
                Data = length > 0 ? Memory.AllocZeroed(length * Managed.ManagedSize<T>()) : null;
            else
                Data = length > 0 ? Memory.Alloc(length * Managed.ManagedSize<T>()) : null;

            Length = length;
        }

        /// <summary>
        /// Creates an unsafe array from the provided pointer and length.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        public UnsafeArray(void* data, int length)
        {
            Data = data;
            Length = length;
        }

        /// <summary>
        /// Grabs a managed reference to an object at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentException"></exception>
        public readonly ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= (uint)Length)
                    throw new ArgumentException($"Unsafe array index {index} is out of range.", nameof(index));

                return ref Managed.GetTypeRef<T>(Data, index);
            }
        }

        /// <summary>
        /// Disposes the unsafe array and cleans up resources.
        /// </summary>
        public void Dispose()
        {
            if (Data == null)
                return;

            Memory.Free(Data);
            Data = null;
        }
    }
}
