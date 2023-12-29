using System;
using System.Collections;
using System.Collections.Generic;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    /// <summary>
    ///     A unmanaged alternative to arrays.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct NativeArray<T> : IEnumerable<T>, IDisposable, IEquatable<NativeArray<T>> where T : unmanaged
    {
        /// <summary>
        ///     Data storage for the <see cref="NativeArray{T}"/>.
        /// </summary>
        public T* Data { get; private set; }

        /// <summary>
        ///     The length of the <see cref="NativeArray{T}"/>.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Represents whether or not the <see cref="NativeArray{T}"/> is null.
        /// </summary>
        public readonly bool IsNull => Data == null;

        /// <summary>
        ///     Returns a span of the <see cref="NativeArray{T}"/>.
        /// </summary>
        public Span<T> Span => new Span<T>(Data, Length);

        /// <summary>
        ///     Creates an <see cref="NativeArray{T}"/> with the provided length.
        /// </summary>
        /// <param name="length"></param>
        public NativeArray(int length)
        {
            Data = length > 0 ? Memory.AllocZeroed<T>(length) : null;
            Length = length;
        }

        /// <summary>
        ///     Creates an <see cref="NativeArray{T}"/> from the provided pointer and length.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        public NativeArray(T* data, int length)
        {
            Data = data;
            Length = length;
        }

        /// <summary>
        ///     Disposes the <see cref="NativeArray{T}"/> and cleans up resources.
        /// </summary>
        public void Dispose()
        {
            if (Data == null)
                return;

            Memory.Free(Data);
            Data = null;
        }

        /// <summary>
        ///     Grabs a managed reference to the object at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentException"></exception>
        public readonly ref T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Length)
                    throw new ArgumentException($"Array index \"{index}\" is out of range.", nameof(index));

                return ref Data[index];
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="NativeArray{T}"/>.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Enumerates the elements of a <see cref="NativeArray{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly NativeArray<T> _array;
            private readonly int _length;
            private int _currentIndex;

            internal Enumerator(NativeArray<T> array)
            {
                _array = array;
                _length = array.Length;
                _currentIndex = -1;
            }

            object IEnumerator.Current => Current;

            /// <summary>
            ///     Current item.
            /// </summary>
            public T Current => _array.Data[_currentIndex];

            /// <summary>
            ///     Moves to the next index of the <see cref="NativeArray{T}"/>.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                _currentIndex++;
                return _currentIndex < _length;
            }

            /// <summary>
            ///     Resets the index of the enumerator.
            /// </summary>
            public void Reset()
            {
                _currentIndex = -1;
            }

            /// <summary>
            /// </summary>
            public void Dispose()
            {
            }
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NativeArray<T> other)
        {
            return Data == other.Data;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is NativeArray<T> other && Equals(other);
        }

        /// <summary>
        ///     Tests whether two objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Tests whether two objects are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Data->GetHashCode();
        }
    }
}
