using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around table fields.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly unsafe struct Field<T> : IEquatable<Field<T>>
    {
        /// <summary>
        ///     Pointer to field.
        /// </summary>
        public void* Data { get; }

        /// <summary>
        ///     Length of the field.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Specifies if the field is shared.
        /// </summary>
        public bool IsShared { get; }

        /// <summary>
        ///     Specifies if the field pointer is null.
        /// </summary>
        public bool IsNull => Data == null;

        /// <summary>
        ///     Creates field.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <param name="isShared"></param>
        public Field(void* data, int length, bool isShared = false)
        {
            Data = data;
            Length = length;
            IsShared = isShared;
        }

        /// <summary>
        ///     Gets a managed reference to the component at the specified index.
        /// </summary>
        /// <param name="index"></param>
        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Ecs.Assert(index < Length, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
                Ecs.Assert(index == 0 || !IsShared, nameof(ECS_INVALID_PARAMETER));
                Ecs.Assert(Data != null, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
                return ref Managed.GetTypeRef<T>(Data, index);
            }
        }

        /// <summary>
        ///     Checks if two <see cref="Field{T}"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Field<T> other)
        {
            return Data == other.Data;
        }

        /// <summary>
        ///     Checks if two <see cref="Field{T}"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Field<T> field && Equals(field);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Entity"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((IntPtr)Data).GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Entity"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Field<T> left, Field<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Entity"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Field<T> left, Field<T> right)
        {
            return !(left == right);
        }
    }
}
