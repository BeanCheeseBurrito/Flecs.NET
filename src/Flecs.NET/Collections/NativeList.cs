using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    /// <summary>
    ///     An unmanaged alternative to <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct NativeList<T> : IEnumerable<T>, IDisposable, IEquatable<NativeList<T>> where T : unmanaged
    {
        /// <summary>
        ///     Data storage for the <see cref="NativeList{T}"/>.
        /// </summary>
        public T* Data { get; private set; }

        /// <summary>
        ///     The capacity of the <see cref="NativeList{T}"/>.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        ///     The current count of the <see cref="NativeList{T}"/>.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Represents whether or not the <see cref="NativeList{T}"/> is null.
        /// </summary>
        public readonly bool IsNull => Data == null;

        /// <summary>
        ///     Returns a span of the <see cref="NativeList{T}"/>.
        /// </summary>
        public Span<T> Span => new Span<T>(Data, Count);

        /// <summary>
        ///     Creates an <see cref="NativeList{T}"/> with the specified capacity.
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
        ///     Gets a managed reference to the object at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentException"></exception>
        public readonly ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= (uint)Count || index < 0)
                    throw new ArgumentException($"List index {index} is out of range.", nameof(index));

                return ref Data[index];
            }
        }

        /// <summary>
        ///     Disposes the <see cref="NativeList{T}"/> and frees resources.
        /// </summary>
        public void Dispose()
        {
            if (Data == null)
                return;

            Memory.Free(Data);
            Data = null;
        }

        /// <summary>
        ///     Adds an item to the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (Count == Capacity)
                EnsureCapacity(Count + 1);

            Data[Count++] = item;
        }

        /// <summary>
        ///     Adds the elements of the specified span to the end of the <see cref="NativeList{T}"/>
        /// </summary>
        /// <param name="span"></param>
        public void AddRange(Span<T> span)
        {
            int oldCount = Count;
            EnsureCapacity(Count += span.Length);
            span.CopyTo(MemoryMarshal.CreateSpan(ref Data[oldCount], span.Length));
        }

        /// <summary>
        ///     Adds the elements of the specified array to the end of the <see cref="NativeList{T}"/>
        /// </summary>
        /// <param name="array"></param>
        public void AddRange(T[] array)
        {
            Span<T> span = new Span<T>(array);
            AddRange(span);
        }

        /// <summary>
        ///     Adds the elements of the specified enumerable to the end of the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        public void AddRange(IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
                Add(item);
        }

        /// <summary>
        ///    Adds a span of items starting at the provided managed reference to the end of the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void AddRange(ref T start, int count)
        {
            AddRange(MemoryMarshal.CreateSpan(ref start, count));
        }

        /// <summary>
        ///    Adds a span of items starting at the pointer to the end of the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public void AddRange(T* start, int count)
        {
            AddRange(new Span<T>(start, count));
        }

        /// <summary>
        ///     Adds the provided item a repeated number of times to the end of the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="count"></param>
        public void AddRepeated(T item, int count)
        {
            EnsureCapacity(Capacity + count);

            for (int i = 0; i < count; i++)
                Data[Count++] = item;
        }

        /// <summary>
        ///     Sets the count of the list to 0.
        /// </summary>
        public void Clear()
        {
            Count = 0;
        }

        /// <summary>
        ///     Determines whether an element is in the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        ///     Ensures that the list has the specified capacity.
        /// </summary>
        /// <param name="newCapacity"></param>
        public void EnsureCapacity(int newCapacity)
        {
            if (Capacity >= newCapacity)
                return;

            int nextPowOf2 = Utils.NextPowOf2(newCapacity);
            Data = Memory.Realloc(Data, nextPowOf2);
            Capacity = nextPowOf2;
        }

        /// <summary>
        ///     Returns the zero-based index of the first occurrence of a value in the <see cref="NativeList{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (EqualityComparer<T>.Default.Equals(Data[i], item))
                    return i;

            return -1;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NativeList<T> other)
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
            return obj is NativeList<T> other && Equals(other);
        }

        /// <summary>
        ///     Tests whether two objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NativeList<T> left, NativeList<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Tests whether two objects are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NativeList<T> left, NativeList<T> right)
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

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="NativeList{T}"/>.
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
        ///     Enumerates the elements of a <see cref="NativeList{T}"/>.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly NativeList<T> _list;
            private readonly int _length;
            private int _currentIndex;

            internal Enumerator(NativeList<T> list)
            {
                _list = list;
                _length = list.Count;
                _currentIndex = -1;
            }

            object IEnumerator.Current => Current;

            /// <summary>
            ///     Current item.
            /// </summary>
            public T Current => _list.Data[_currentIndex];

            /// <summary>
            ///     Moves to the next index of the <see cref="NativeList{T}"/>.
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
    }
}
