using System;
using System.Runtime.CompilerServices;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Utilities;

/// <summary>
///     Helper macros for working with flecs.
/// </summary>
public static unsafe class Utils
{
    /// <summary>
    ///     False.
    /// </summary>
    public const byte False = 0;

    /// <summary>
    ///     True.
    /// </summary>
    public const byte True = 1;

    /// <summary>
    ///     Converts a boolean to a byte.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Bool(bool value)
    {
        return value ? True : False;
    }

    /// <summary>
    ///     Converts a byte to a boolean.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Bool(byte value)
    {
        return value != 0;
    }

    /// <summary>
    ///     Gets the next power of 2 for the provided number.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int NextPowOf2(int num)
    {
        num--;
        num |= num >> 1;
        num |= num >> 2;
        num |= num >> 4;
        num |= num >> 8;
        num |= num >> 16;
        num++;

        return num;
    }

    /// <summary>
    ///     Tests if 2 readonly refs point to the same object.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreSameReadOnlyRefs<T>(in T a, in T b)
    {
        return Unsafe.AreSame(ref Unsafe.AsRef(in a), ref Unsafe.AsRef(in b));
    }

    /// <summary>
    ///     Tests if a readonly ref is null.
    /// </summary>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullReadOnlyRef<T>(in T obj)
    {
        return Unsafe.IsNullRef(ref Unsafe.AsRef(in obj));
    }

    /// <summary>
    ///     Calls the os api free function.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OsFree(IntPtr data)
    {
        ((delegate* unmanaged<IntPtr, void>)ecs_os_api.free_)(data);
    }

    /// <summary>
    ///     Calls the os api free function.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OsFree(void* data)
    {
        OsFree((IntPtr)data);
    }
}