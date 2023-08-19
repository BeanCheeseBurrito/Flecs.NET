using System.Runtime.CompilerServices;

namespace Flecs.NET.Utilities
{
    public static unsafe class Utils
    {
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