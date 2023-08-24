using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public readonly unsafe struct Column<T> : IEquatable<Column<T>>
    {
        public void* Data { get; }
        public int Length { get; }
        public bool IsShared { get; }

        public bool IsNull => Data == null;

        public Column(void* data, int length, bool isShared = false)
        {
            Data = data;
            Length = length;
            IsShared = isShared;
        }

        public readonly ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Assert.True(index < Length, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
                Assert.True(index == 0 || !IsShared, nameof(ECS_INVALID_PARAMETER));
                Assert.True(Data != null, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
                return ref Managed.GetTypeRef<T>(Data, index);
            }
        }

        public bool Equals(Column<T> other)
        {
            return Data == other.Data;
        }

        public override bool Equals(object obj)
        {
            return obj is Column<T> column && Equals(column);
        }

        public override int GetHashCode()
        {
            return ((IntPtr)Data).GetHashCode();
        }

        public static bool operator ==(Column<T> left, Column<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Column<T> left, Column<T> right)
        {
            return !(left == right);
        }
    }
}
