using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Flecs.NET.Utilities
{
    /// <summary>
    /// Static class for allocating and freeing memory.
    /// </summary>
    public static unsafe class Memory
    {
        /// <summary>
        /// Allocates memory for type.
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
        /// Allocates zeroed memory for type.
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
        /// Reallocate memory for type.
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
        /// Free memory.
        /// </summary>
        /// <param name="data"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free(void* data)
        {
#if NET6_0_OR_GREATER
            NativeMemory.Free(data);
#else
            Marshal.FreeHGlobal((IntPtr)data);
#endif
        }

        /// <summary>
        /// Allocate specified amount of bytes.
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Alloc(int byteCount)
        {
#if NET6_0_OR_GREATER
            void* pointer = NativeMemory.Alloc((nuint)byteCount);
#else
            void* pointer = (void*)Marshal.AllocHGlobal((IntPtr)byteCount);
#endif
            return pointer;
        }

        /// <summary>
        /// Allocate zeroed specified amount of bytes.
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* AllocZeroed(int byteCount)
        {
#if NET6_0_OR_GREATER
            void* pointer = NativeMemory.AllocZeroed((nuint)byteCount);
#else
            void* pointer = (void*)Marshal.AllocHGlobal((IntPtr)byteCount);
            Unsafe.InitBlock(pointer, 0, (uint)byteCount);
#endif
            return pointer;
        }

        /// <summary>
        /// Reallocate specified amount of bytes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Realloc(void* data, int byteCount)
        {
#if NET6_0_OR_GREATER
            void* pointer = NativeMemory.Realloc(data, (nuint)byteCount);
#else
            void* pointer = data == null
                ? Alloc(byteCount)
                : (void*)Marshal.ReAllocHGlobal((IntPtr)data, (IntPtr)byteCount);
#endif
            return pointer;
        }
    }
}
