using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    /// <summary>
    ///     Unsafe array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct NativeArray<T> : IDisposable where T : unmanaged
    {
        /// <summary>
        ///     Data storage for the array.
        /// </summary>
        public T* Data { get; private set; }

        /// <summary>
        ///     The length of the array.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Represents whether or not the array is null.
        /// </summary>
        public readonly bool IsNull => Data == null;

        /// <summary>
        ///     Creates an unsafe array with the provided length.
        /// </summary>
        /// <param name="length"></param>
        public NativeArray(int length)
        {
            Data = length > 0 ? Memory.AllocZeroed<T>(length) : null;
            Length = length;
        }

        /// <summary>
        ///     Creates an unsafe array from the provided pointer and length.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        public NativeArray(T* data, int length)
        {
            Data = data;
            Length = length;
        }

        /// <summary>
        ///     Grabs a managed reference to an object at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentException"></exception>
        public readonly T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Length)
                    throw new ArgumentException($"Array index \"{index}\" is out of range.", nameof(index));

                return Data[index];
            }
            set
            {
                if ((uint)index >= (uint)Length)
                    throw new ArgumentException($"Array index \"{index}\" is out of range.", nameof(index));

                Data[index] = value;
            }
        }

        /// <summary>
        ///     Disposes the unsafe array and cleans up resources.
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
