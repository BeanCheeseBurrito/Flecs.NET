using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Collections
{
    public unsafe struct UnsafeArray<T> : IDisposable
    {
        public void* Data { get; private set; }
        public int Length { get; }

        public readonly bool IsNull => Data == null;

        public UnsafeArray(int length, bool isZeroed = true)
        {
            if (isZeroed)
                Data = length > 0 ? Memory.AllocZeroed(length * Managed.ManagedSize<T>()) : null;
            else
                Data = length > 0 ? Memory.Alloc(length * Managed.ManagedSize<T>()) : null;

            Length = length;
        }

        public UnsafeArray(void* data, int length)
        {
            Data = data;
            Length = length;
        }

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

        public void Dispose()
        {
            if (Data == null)
                return;

            Memory.Free(Data);
            Data = null;
        }
    }
}