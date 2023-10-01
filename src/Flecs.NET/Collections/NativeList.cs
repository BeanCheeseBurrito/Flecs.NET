using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    /// <summary>
    ///     Unsafe list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct NativeList<T> : IDisposable, IEquatable<NativeList<T>> where T : unmanaged
    {
        /// <summary>
        ///     Data storage for the unsafe list.
        /// </summary>
        public T* Data { get; private set; }

        /// <summary>
        ///     The capacity of the unsafe list.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        ///     The current count of the unsafe list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Represents whether or not the unsafe list is null.
        /// </summary>
        public readonly bool IsNull => Data == null;

        /// <summary>
        ///     Creates an unsafe list with the specified capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public NativeList(int capacity)
        {
            if (capacity <= 0)
            {
                Data = null;
                Capacity = default;
                Count = default;
            }

            Data = Memory.AllocZeroed<T>(capacity);
            Capacity = capacity;
            Count = 0;
        }

        /// <summary>
        ///     Disposes the unsafe list and frees resources.
        /// </summary>
        public void Dispose()
        {
            if (Data == null)
                return;

            Memory.Free(Data);
            Data = null;
        }

        /// <summary>
        ///     Gets a managed reference to the object at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentException"></exception>
        public readonly ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= (uint)Count)
                    throw new ArgumentException($"List index {index} is out of range.", nameof(index));

                return ref Managed.GetTypeRef<T>(Data, index);
            }
        }

        /// <summary>
        ///     Adds an item to the unsafe list.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (Count == Capacity)
            {
                int newCapacity = Utils.NextPowOf2(Count + 1);
                Data = Memory.Realloc(Data, newCapacity);
                Capacity = newCapacity;
            }

            Data[Count++] = item;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NativeList<T> other)
        {
            return Data == other.Data;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is NativeList<T> other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for this list.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Data->GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NativeList<T> left, NativeList<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NativeList<T> left, NativeList<T> right)
        {
            return !(left == right);
        }
    }
}
