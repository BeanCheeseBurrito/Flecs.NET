using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for registering enum types and it's members.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static unsafe class EnumType<T> // TODO: Remove EnumType and merge enum code in Type<T> class.
    {
        private static NativeArray<EnumMember> _data;

        [SuppressMessage("Performance", "CA1810:Initialize reference type static fields inline")]
        static EnumType()
        {
            Type type = typeof(T);

            if (!typeof(T).IsEnum)
                return;

            Array values = type.GetEnumValues();
            _data = new NativeArray<EnumMember>(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                object obj = values.GetValue(i)!;
                int value = Convert.ToInt32(obj, CultureInfo.InvariantCulture);
                _data[i] = new EnumMember(value, Interlocked.Increment(ref Ecs.CacheIndexCount));
            }
        }

        internal static void Init(ecs_world_t* world, ulong id)
        {
            ecs_cpp_enum_init(world, id);

            for (int i = 0; i < _data.Length; i++)
            {
                T member = (T)(object)_data[i].Value;
                using NativeString nativeName = (NativeString)member.ToString();
                Type<T>.EnsureCacheIndex(world, _data[i].CacheIndex) =
                    ecs_cpp_enum_constant_register(world, id, 0, nativeName, _data[i].Value);
            }
        }

        /// <summary>
        ///     Gets an id for an enum member.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="world"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static ulong Id(T member, ecs_world_t* world)
        {
            Type<T>.Id(world);

            int value = Convert.ToInt32(member, CultureInfo.InvariantCulture);

            for (int i = 0; i < _data.Length; i++)
            {
                EnumMember data = _data[i];
                if (data.Value == value)
                    return Type<T>.LookupCacheIndex(world, data.CacheIndex);
            }

            Ecs.Error("Failed to find entity associated with enum member.");
            return default;
        }

        private struct EnumMember
        {
            public int Value;
            public int CacheIndex;

            public EnumMember(int value, int cacheIndex)
            {
                Value = value;
                CacheIndex = cacheIndex;
            }
        }
    }
}
