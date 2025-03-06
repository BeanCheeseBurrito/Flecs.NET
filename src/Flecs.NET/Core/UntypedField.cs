using System;
using System.Runtime.CompilerServices;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around an untyped field.
/// </summary>
public readonly unsafe struct UntypedField : IEquatable<UntypedField>
{
    /// <summary>
    ///     The field pointer.
    /// </summary>
    public readonly void* Data;

    /// <summary>
    ///     The size of each field element.
    /// </summary>
    public readonly int Size;

    /// <summary>
    ///     The number of elements in the field.
    /// </summary>
    public readonly int Length;

    /// <summary>
    ///     Initializes an untyped field with the provided pointer, size, and length.
    /// </summary>
    /// <param name="data">The field pointer.</param>
    /// <param name="size">The size of each field element.</param>
    /// <param name="length">The number of elements in the field.</param>
    public UntypedField(void* data, int size, int length)
    {
        Data = data;
        Size = size;
        Length = length;
    }

    /// <summary>
    ///     Whether the field is null.
    /// </summary>
    public bool IsNull => Data == null;

    /// <summary>
    ///     Returns a pointer to the element at the specified index in the column.
    /// </summary>
    /// <param name="index">The element index.</param>
    public void* this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Ecs.Assert(index >= 0 && index < Length, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
            Ecs.Assert(Data != null, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
            return &((byte*)Data)[index * Size];
        }
    }

    /// <summary>
    ///     Checks if two <see cref="UntypedField"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(UntypedField other)
    {
        return Data == other.Data;
    }

    /// <summary>
    ///     Checks if two <see cref="UntypedField"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is UntypedField field && Equals(field);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="UntypedField"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return ((IntPtr)Data).GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="UntypedField"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(UntypedField left, UntypedField right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="UntypedField"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(UntypedField left, UntypedField right)
    {
        return !(left == right);
    }
}
