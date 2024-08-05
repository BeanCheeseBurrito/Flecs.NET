using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Flecs.NET.Utilities;

/// <summary>
///     Static class for allocating and freeing memory.
/// </summary>
public static unsafe class Memory
{
    /// <summary>
    ///     Allocates memory for type.
    /// </summary>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* Alloc<T>(int count) where T : unmanaged
    {
        return (T*)Alloc(count * sizeof(T));
    }

    /// <summary>
    ///     Allocates memory for type.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* Alloc<T>(T value) where T : unmanaged
    {
        T* ptr = Alloc<T>(1);
        ptr[0] = value;
        return ptr;
    }

    /// <summary>
    ///     Allocates zeroed memory for type.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* AllocZeroed<T>(T value) where T : unmanaged
    {
        T* ptr = AllocZeroed<T>(1);
        ptr[0] = value;
        return ptr;
    }

    /// <summary>
    ///     Allocates zeroed memory for type.
    /// </summary>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* AllocZeroed<T>(int count) where T : unmanaged
    {
        return (T*)AllocZeroed(count * sizeof(T));
    }

    /// <summary>
    ///     Reallocate memory for type.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="count"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T* Realloc<T>(T* data, int count) where T : unmanaged
    {
        return (T*)Realloc((void*)data, count * sizeof(T));
    }

    /// <summary>
    ///     Free memory.
    /// </summary>
    /// <param name="data"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Free(void* data)
    {
        NativeMemory.Free(data);
    }

    /// <summary>
    ///     Allocate specified amount of bytes.
    /// </summary>
    /// <param name="byteCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* Alloc(int byteCount)
    {
        return NativeMemory.Alloc((nuint)byteCount);
    }

    /// <summary>
    ///     Allocate zeroed specified amount of bytes.
    /// </summary>
    /// <param name="byteCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* AllocZeroed(int byteCount)
    {
        return NativeMemory.AllocZeroed((nuint)byteCount);;
    }

    /// <summary>
    ///     Reallocate specified amount of bytes.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="byteCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* Realloc(void* data, int byteCount)
    {
        return NativeMemory.Realloc(data, (nuint)byteCount);
    }
}