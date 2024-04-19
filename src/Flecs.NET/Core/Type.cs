using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Static class that registers and stores information about types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static unsafe class Type<T>
    {
        /// <summary>
        ///     The index that corresponds to its location in a world's component id cache.
        /// </summary>
        public static readonly int CacheIndex = Interlocked.Increment(ref Ecs.CacheIndexCount);

        /// <summary>
        ///     The full name of this type.
        /// </summary>
        public static readonly string FullName = Macros.FullName<T>();

        /// <summary>
        ///     The name of this type.
        /// </summary>
        public static readonly string Name = Macros.Name<T>();

        /// <summary>
        ///     The symbol name of the type.
        /// </summary>
        public static readonly string SymbolName = Macros.FullName<T>();

        /// <summary>
        ///     The size of the type.
        /// </summary>
        public static readonly int Size = SizeOf();

        /// <summary>
        ///     The alignment of the type.
        /// </summary>
        public static readonly int Alignment = AlignOf();

        /// <summary>
        ///     Whether or not the type is a tag.
        /// </summary>
        public static readonly bool IsTag = Size == 0 && Alignment == 0;

        /// <summary>
        ///     Returns the id for this type with the provided world. Registers a new component id if it doesn't exist.
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Id(ecs_world_t* world)
        {
            ref ulong cachedId = ref LookupCacheIndex(world);
            return Unsafe.IsNullRef(ref cachedId)
                ? RegisterComponent(world, true, true, 0, "")
                : cachedId;
        }

        /// <summary>
        ///     Returns the id for this type with the provided world. Registers a new component id if it doesn't exist.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="ignoreScope"></param>
        /// <param name="isComponent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Id(ecs_world_t* world, bool ignoreScope, bool isComponent, ulong id)
        {
            ref ulong cachedId = ref LookupCacheIndex(world);
            return Unsafe.IsNullRef(ref cachedId)
                ? RegisterComponent(world, ignoreScope, isComponent, id, "")
                : cachedId;
        }

        /// <summary>
        ///     Returns the id for this type with the provided world. Registers a new component id if it doesn't exist.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="ignoreScope"></param>
        /// <param name="isComponent"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Id(ecs_world_t* world, bool ignoreScope, bool isComponent, ulong id, string name)
        {
            ref ulong cachedId = ref LookupCacheIndex(world);
            return Unsafe.IsNullRef(ref cachedId)
                ? RegisterComponent(world, ignoreScope, isComponent, id, name)
                : cachedId;
        }

        /// <summary>
        ///     Checks if the type is registered in the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRegistered(ecs_world_t* world)
        {
            ref ulong cachedId = ref LookupCacheIndex(world);
            return !Unsafe.IsNullRef(ref cachedId) && cachedId != 0;
        }

        /// <summary>
        ///     Ensures that the world has memory allocated at the provided cache index.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>Reference to the id at the provided cache index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ulong EnsureCacheIndex(ecs_world_t* world)
        {
            return ref EnsureCacheIndex(world, CacheIndex);
        }

        /// <summary>
        ///     Ensures that the world has memory allocated at the provided cache index.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="index">The type cache index.</param>
        /// <returns>Reference to the id at the provided cache index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ulong EnsureCacheIndex(ecs_world_t* world, int index)
        {
            ref NativeList<ulong> cache = ref ((BindingContext.WorldContext*)ecs_get_binding_ctx_fast(world))->TypeCache;
            cache.EnsureCount(index + 1);
            return ref cache.Data[index];
        }

        /// <summary>
        ///     Gets a reference to the id occupying the provided cache index for this world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <returns>Reference to the id at the provided cache index. Returns a null reference if the index does not have a registered id yet.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ulong LookupCacheIndex(ecs_world_t* world)
        {
            return ref LookupCacheIndex(world, CacheIndex);
        }

        /// <summary>
        ///     Gets a reference to the id occupying the provided cache index for this world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="index">The type cache index.</param>
        /// <returns>Reference to the id at the provided cache index. Returns a null reference if the index does not have a registered id yet.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ulong LookupCacheIndex(ecs_world_t* world, int index)
        {
            ref NativeList<ulong> cache = ref ((BindingContext.WorldContext*)ecs_get_binding_ctx_fast(world))->TypeCache;
            return ref index >= cache.Count || cache.Data[index] == 0
                ? ref Unsafe.NullRef<ulong>()
                : ref cache.Data[index];
        }

        /// <summary>
        ///     Registers this type with the provided world.
        /// </summary>
        /// <param name="world">The ECS world.</param>
        /// <param name="ignoreScope">If true, the type will be registered in the root scope with it's full type name.</param>
        /// <param name="isComponent">If true, type will be created with full component registration. (size, alignment, enums, hooks)</param>
        /// <param name="id">If an existing entity is found with this id, attempt to alias it. Otherwise, register new entity with this id.</param>
        /// <param name="name">If an existing entity is found with this name, attempt to alias it. Otherwise, register new entity with this name.</param>
        /// <returns></returns>
        public static ulong RegisterComponent(World world, bool ignoreScope, bool isComponent, ulong id, string name)
        {
            // If a name or id is provided, the type is being used to alias an already existing entity.
            Entity e = id != 0 ? new Entity(world, id) : world.Lookup(name, false);

            // If an existing entity is found, ensure that the size and alignment match the entity and return its id.
            if (isComponent && e != 0)
            {
                if (!e.Has<EcsComponent>())
                {
                    Ecs.Assert(IsTag, $"Cannot alias '{e.Path()}' with a component. '{FullName}' must be a zero-sized struct");
                    return e;
                }

                ref readonly EcsComponent info = ref e.Get<EcsComponent>();
                Ecs.Assert(info.size == Size, $"Size of type '{FullName}' ({Size}) does not match currently registered size ({info.size})");
                Ecs.Assert(info.alignment == Alignment, $"Alignment of type '{FullName}' ({Alignment}) does not match currently registered alignment ({info.alignment})");

                return e;
            }

            Entity prevScope = world.SetScope(ignoreScope ? 0ul : world.GetScope());
            Entity prevWith = world.SetWith(ignoreScope ? 0ul : world.GetWith());

            // Check if an entity exists with the same symbol as this type. This is normally used
            // for pairing Flecs.NET.Bindings.Native types with their C counterparts.
            if (world.TryLookupSymbol(FullName, out Entity symbol))
            {
                id = symbol;
                name = symbol.Path();
            }

            using NativeString nativeSymbol = (NativeString)FullName;
            using NativeString nativeName = string.IsNullOrEmpty(name)
                ? (NativeString)GetTrimmedTypeName(world)
                : (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.id = id;
            entityDesc.use_low_id = Macros.True;
            entityDesc.name = nativeName;
            entityDesc.symbol = nativeSymbol;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultSeparator;
            ulong entity = ecs_entity_init(world, &entityDesc);
            Ecs.Assert(entity != 0, $"Failed to register entity for type '{FullName}'");

            EnsureCacheIndex(world) = entity;

            if (!isComponent)
                return entity;

            ecs_component_desc_t componentDesc = default;
            componentDesc.entity = entity;
            componentDesc.type.size = Size;
            componentDesc.type.alignment = Alignment;
            ulong component = ecs_component_init(world, &componentDesc);
            Ecs.Assert(component != 0, $"Failed to register component for type '{FullName}'");

            world.SetWith(prevWith);
            world.SetScope(prevScope);

            if (typeof(T).IsEnum)
                EnumType<T>.Init(world, entity);

            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return component;

            ecs_type_hooks_t hooksDesc = default;
            hooksDesc.ctor = BindingContext<T>.DefaultManagedCtorPointer;
            hooksDesc.dtor = BindingContext<T>.DefaultManagedDtorPointer;
            hooksDesc.move = BindingContext<T>.DefaultManagedMovePointer;
            hooksDesc.copy = BindingContext<T>.DefaultManagedCopyPointer;
            ecs_set_hooks_id(world, component, &hooksDesc);

            return component;
        }

        /// <summary>
        ///     Returns a trimmed version of this type's full name with respect to the current scope of the world.
        /// </summary>
        /// <param name="world"></param>
        /// <typeparam name="T"></typeparam>
        public static string GetTrimmedTypeName(World world)
        {
            Entity scope = world.GetScope();

            if (scope == 0)
                return FullName;

            string scopePath = scope.Path(initSep: "");

            // If the the start of the type's full name matches the current scope's path, trim the scope's path
            // from the full type name and return. Otherwise return only the name of the type.
            return FullName.StartsWith(scopePath, StringComparison.Ordinal)
                ? FullName[(scopePath.Length + 1)..]
                : Name;
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
