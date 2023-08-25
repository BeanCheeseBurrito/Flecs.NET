using System.Runtime.CompilerServices;

namespace Flecs.NET.Utilities
{

    /// <summary>
    /// Static class for simple utility functions.
    /// </summary>
    public static unsafe class Utils
    {
        /// <summary>
        /// Gets the next power of 2 for the provided number.
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
