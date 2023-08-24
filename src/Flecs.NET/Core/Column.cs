using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;

using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public readonly unsafe struct Column<T> : IEquatable<Column<T>>
    {
        /// <summary>
        /// Pointer to column.
        /// </summary>
        public void* Data { get; }

        /// <summary>
        /// Length of the column.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Specifies if the column is shared.
        /// </summary>
        public bool IsShared { get; }

        /// <summary>
        /// Specifies if the column pointer is null.
        /// </summary>
        public bool IsNull => Data == null;

        /// <summary>
        /// Creates column.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <param name="isShared"></param>
        public Column(void* data, int length, bool isShared = false)
        {
            Data = data;
            Length = length;
            IsShared = isShared;
        }

        /// <summary>
        /// Gets a managed reference to the component at the specified index.
        /// </summary>
        /// <param name="index"></param>
        public ref T this[int index]
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Column<T> other)
        {
            return Data == other.Data;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Column<T> column && Equals(column);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((IntPtr)Data).GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Column<T> left, Column<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Column<T> left, Column<T> right)
        {
            return !(left == right);
        }
    }
}
