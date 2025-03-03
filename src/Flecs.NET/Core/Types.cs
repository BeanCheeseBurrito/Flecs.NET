using System;
using System.Collections;
using System.Collections.Generic;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_type_t"/>*.
/// </summary>
public unsafe struct Types : IEnumerable<Id>, IEquatable<Types>
{
    /// <summary>
    ///     The world.
    /// </summary>
    public World World;

    /// <summary>
    ///     The underlying <see cref="ecs_type_t"/>* pointer.
    /// </summary>
    public ecs_type_t* Handle;

    /// <summary>
    ///     Initializes a new instance of <see cref="Types"/> with the provided world and handle.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="handle">The <see cref="ecs_type_t"/>* pointer to wrap. </param>
    public Types(World world, ecs_type_t* handle)
    {
        World = world;
        Handle = handle;
    }

    /// <summary>
    ///     Convert type to comma-separated string
    /// </summary>
    /// <returns></returns>
    public string Str()
    {
        return NativeString.GetStringAndFree(ecs_type_str(World, Handle));
    }

    /// <summary>
    ///     Return number of ids in type
    /// </summary>
    /// <returns></returns>
    public int Count()
    {
        return Handle == null ? 0 : Handle->count;
    }

    /// <summary>
    ///     Return pointer to array.
    /// </summary>
    /// <returns></returns>
    public ulong* Array()
    {
        return Handle == null ? null : Handle->array;
    }

    /// <summary>
    ///     Gets the type id at provided index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The type id at the provided index.</returns>
    public Id Get(int index)
    {
        if (Handle == null)
            return default;
        Ecs.Assert(Handle->count > index, nameof(ECS_OUT_OF_RANGE));
        return new Id(World, Handle->array[index]);
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the entity's types.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the entity's types.</returns>
    public IEnumerator<Id> GetEnumerator()
    {
        for (int i = 0; i < Count(); i++)
            yield return Get(i);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static implicit operator ecs_type_t*(Types types)
    {
        return To(types);
    }

    /// <summary>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static ecs_type_t* To(Types types)
    {
        return types.Handle;
    }

    /// <summary>
    ///     Checks if two <see cref="Types"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Types other)
    {
        return Handle == other.Handle;
    }

    /// <summary>
    ///     Checks if two <see cref="Types"/> instances are equals.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is Types types && Equals(types);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="Types"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Handle->GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="Types"/> are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Types left, Types right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="Types"/> are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Types left, Types right)
    {
        return !(left == right);
    }

    /// <summary>
    ///     Returns comma separated string of type names.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Ecs.IsStageOrWorld(World) ? Str() : string.Empty;
    }
}
