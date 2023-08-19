using System.Runtime.CompilerServices;

namespace Flecs.NET.Utilities
{
    public unsafe struct ManagedMemory<T>
    {
        public void* Data { get; set; }

        public ManagedMemory(void* data)
        {
            Data = default;
        }

        public readonly ref T this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref Managed.GetTypeRef<T>(Data, i);
        }
    }
}