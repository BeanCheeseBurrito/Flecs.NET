using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    public unsafe struct UnsafeList<T> : IDisposable
    {
        public void* Data { get; private set; }
        public int Capacity { get; private set; }
        public int Count { get; private set; }

        public readonly bool IsNull => Data == null;
        public readonly bool IsManaged => RuntimeHelpers.IsReferenceOrContainsReferences<T>();

        public UnsafeList(int capacity)
        {
            if (capacity <= 0)
            {
                Data = null;
                Capacity = default;
                Count = default;
            }

            Data = Memory.AllocZeroed(capacity * Managed.ManagedSize<T>());
            Capacity = capacity;
            Count = 0;
        }

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

        public void Add(T item)
        {
            if (Count == Capacity)
            {
                int newCapacity = Utils.NextPowOf2(Count + 1);
                Data = Memory.Realloc(Data, newCapacity * Managed.ManagedSize<T>());
                Capacity = newCapacity;
            }

            Managed.SetTypeRef(Data, item, Count++);
        }

        public void Dispose()
        {
            if (Data == null)
                return;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                for (int i = 0; i < Count; i++)
                    Managed.FreeGcHandle(Data, i);

            Memory.Free(Data);
            Data = null;
        }
    }
}