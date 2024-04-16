using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Static class that registers and stores information about types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SuppressMessage("Usage", "CA1721")]
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static unsafe class Type<T>
    {
        /// <summary>
        ///     The index that corresponds to its location in a world's component id cache.
        /// </summary>
        public static readonly int TypeIndex = Interlocked.Increment(ref Ecs.CacheIndexCount);

        /// <summary>
        ///     The size of the type.
        /// </summary>
        public static readonly int Size = SizeOf();

        /// <summary>
        ///     The alignment of the type.
        /// </summary>
        public static readonly int Alignment = AlignOf();

        /// <summary>
        ///     The type name of the type.
        /// </summary>
        public static readonly string TypeName = "::" + Macros.GetSymbolName<T>();

        /// <summary>
        ///     The symbol name of the type.
        /// </summary>
        public static readonly string SymbolName = Macros.GetSymbolName<T>();

        /// <summary>
        ///     Registers a type and returns it's id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="isComponent"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        public static ulong IdExplicit(ecs_world_t* world, string? name = null,
            ulong id = default, bool isComponent = true, bool* existing = null)
        {
            World w = new World(world);

            ref ulong cachedId = ref w.LookupComponentIndex(TypeIndex);

            if (!Unsafe.IsNullRef(ref cachedId))
                return cachedId;

            string symbol = id == 0 ? SymbolName : NativeString.GetString(ecs_get_symbol(world, id));

            using NativeString nativeName = (NativeString)name;
            using NativeString nativeTypeName = (NativeString)TypeName;
            using NativeString nativeSymbolName = (NativeString)symbol;

            NativeLayout(out int size, out int alignment);

            ulong entity = FlecsInternal.ComponentRegisterExplicit(
                world, 0, id,
                nativeName, nativeTypeName, nativeSymbolName,
                size, alignment,
                Macros.Bool(isComponent), (byte*)existing
            );

            w.EnsureComponentIndex(TypeIndex) = entity;

            if (typeof(T).IsEnum)
                EnumType<T>.Init(world, entity);

            return entity;
        }

        /// <summary>
        ///     Registers a type and returns it's id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ulong Id(ecs_world_t* world, string? name = null)
        {
            ref ulong cachedId = ref new World(world).LookupComponentIndex(TypeIndex);

            if (!Unsafe.IsNullRef(ref cachedId))
                return cachedId;

            ulong prevScope = ecs_set_scope(world, 0);
            ulong prevWith = ecs_set_with(world, 0);

            bool existing = false;
            ulong entity = IdExplicit(world, name, 0, true, &existing);

            if (Size != 0 && !existing && RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                ecs_type_hooks_t typeHooksDesc = default;
                typeHooksDesc.ctor = BindingContext<T>.DefaultManagedCtorPointer;
                typeHooksDesc.dtor = BindingContext<T>.DefaultManagedDtorPointer;
                typeHooksDesc.move = BindingContext<T>.DefaultManagedMovePointer;
                typeHooksDesc.copy = BindingContext<T>.DefaultManagedCopyPointer;
                ecs_set_hooks_id(world, entity, &typeHooksDesc);
            }

            if (prevWith != 0)
                ecs_set_with(world, prevWith);

            if (prevScope != 0)
                ecs_set_scope(world, prevScope);

            return entity;
        }

        /// <summary>
        ///     Tests if the type is registered.
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public static bool IsRegistered(ecs_world_t* world)
        {
            return Lookup(world) != 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public static ulong Lookup(ecs_world_t* world)
        {
            ref ulong cachedId = ref new World(world).LookupComponentIndex(TypeIndex);
            return Unsafe.IsNullRef(ref cachedId) ? 0 : cachedId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool TryLookup(ecs_world_t* world, out ulong entity)
        {
            return (entity = Lookup(world)) != 0;
        }

        [SuppressMessage("Usage", "CA1508")]
        private static void NativeLayout(out int size, out int alignment)
        {
            Type type = typeof(T);
            StructLayoutAttribute attribute = type.StructLayoutAttribute!;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                size = sizeof(GCHandle);
                alignment = sizeof(GCHandle);
                return;
            }

            if (attribute.Value == LayoutKind.Explicit)
            {
                size = attribute.Size == 0 ? sizeof(T) : attribute.Size;
                alignment = attribute.Pack == 0 ? sizeof(AlignOfHelper) - sizeof(T) : attribute.Pack;
            }
            else
            {
                size = sizeof(T);
                alignment = sizeof(AlignOfHelper) - sizeof(T);
            }

            if (RuntimeFeature.IsDynamicCodeSupported)
            {
                FieldInfo[] fields =
                    type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                size = fields.Length == 0 ? 0 : size;
                alignment = size == 0 ? 0 : alignment;
            }
            else if (sizeof(T) == 1)
            {
                // Structs containing zero non-static fields will always return true when compared using .Equals().
                // Test for tags by changing the underlying byte and checking equality.
                // If the structs always return true, it's likely that the struct is a tag.

                T aInstance = default!;
                T bInstance = default!;

                for (byte i = 0; i < 16; i++)
                {
                    *(byte*)&bInstance = i;
                    if (!Equals(aInstance, bInstance))
                        return;
                }

                size = 0;
                alignment = 0;
            }
        }

        private static int SizeOf()
        {
            NativeLayout(out int size, out int _);
            return size;
        }

        private static int AlignOf()
        {
            NativeLayout(out int _, out int alignment);
            return alignment;
        }

        [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
        private readonly struct AlignOfHelper
        {
            private readonly byte _dummy;
            private readonly T _data;

            [SuppressMessage("ReSharper", "UnusedMember.Local")]
            public AlignOfHelper(byte dummy, T data)
            {
                _dummy = dummy;
                _data = data;
                _ = _dummy;
                _ = _data;
            }
        }
    }
}
