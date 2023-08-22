using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe class Macros
    {
        public const byte False = 0;
        public const byte True = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Bool(bool value)
        {
            return value ? True : False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreSameReadOnlyRefs<T>(in T a, in T b)
        {
            return Unsafe.AreSame(ref Unsafe.AsRef(a), ref Unsafe.AsRef(b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullReadOnlyRef<T>(in T obj)
        {
#if NET5_0_OR_GREATER
            return Unsafe.IsNullRef(ref Unsafe.AsRef(obj));
#else
            return Unsafe.AsPointer(ref Unsafe.AsRef(obj)) == null;
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong EntityLow(ulong value)
        {
            return (uint)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong EntityHi(ulong value)
        {
            return (uint)(value >> 32);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong EntityCombine(ulong low, ulong hi)
        {
            return (hi << 32) + (uint)low;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Pair(ulong first, ulong second)
        {
            return ECS_PAIR | EntityCombine(second, first);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Pair<TFirst, TSecond>(ecs_world_t* world)
        {
            return Pair(Type<TFirst>.Id(world), Type<TSecond>.Id(world));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Pair<TFirst>(ulong second, ecs_world_t* world)
        {
            return Pair(Type<TFirst>.Id(world), second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PairSecond<TSecond>(ulong first, ecs_world_t* world)
        {
            return Pair(first, Type<TSecond>.Id(world));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PairFirst(ulong entity)
        {
            return EntityHi(entity & ECS_COMPONENT_MASK);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PairSecond(ulong entity)
        {
            return EntityLow(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong DependsOn(ulong entity)
        {
            return Pair(EcsDependsOn, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong EntityHasIdFlag(ulong entity, ulong flag)
        {
            return entity & flag;
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TableLock(ecs_world_t* world, ecs_table_t* table)
        {
            ecs_table_lock(world, table);
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TableUnlock(ecs_world_t* world, ecs_table_t* table)
        {
            ecs_table_unlock(world, table);
        }
    }
}