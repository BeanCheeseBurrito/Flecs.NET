using System.Runtime.CompilerServices;

namespace Flecs.NET.Utilities
{
    /// <summary>
    ///     Static class for simple utility functions.
    /// </summary>
    internal static unsafe class Utils
    {
        /// <summary>
        ///     Checks if 2 native strings are equal.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool StringEqual(byte* s1, byte* s2)
        {
            while (*s1 == *s2++)
                if (*s1++ == 0)
                    return true;

            return *s1 - *--s1 == 0;
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
    }
}
