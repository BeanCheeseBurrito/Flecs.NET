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
    ///     Checks if specific bit is set.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(int value, int index)
    {
        return (value & (1 << index)) != 0;
    }

    /// <summary>
    ///     Checks if specific bit is set.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(uint value, int index)
    {
        return (value & (1 << index)) != 0;
    }

    /// <summary>
    ///     Checks if specific bit is set.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(uint value, uint index)
    {
        return (value & (1 << (int)index)) != 0;
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
        OsFree((nint)data);
    }
}
