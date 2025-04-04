using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Flecs.NET.Collections;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Core.Hooks;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Static class that registers and stores information about types.
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
public static unsafe class Type<T>
{
    /// <summary>
    ///     The index that corresponds to its location in a world's type id cache.
    /// </summary>
    public static readonly int CacheIndex = Interlocked.Increment(ref Ecs.CacheIndexCount);

    /// <summary>
    ///     The full name of this type.
    /// </summary>
    public static readonly string FullName = GetFullName();

    /// <summary>
    ///     The name of this type.
    /// </summary>
    public static readonly string Name = GetName();

    /// <summary>
    ///     The size of the type.
    /// </summary>
    public static readonly int Size = SizeOf();

    /// <summary>
    ///     The alignment of the type.
    /// </summary>
    public static readonly int Alignment = AlignOf();

    /// <summary>
    ///     Whether the type is a tag.
    /// </summary>
    public static readonly bool IsTag = Size == 0 && Alignment == 0;

    /// <summary>
    ///     Whether the type is an enum.
    /// </summary>
    public static readonly bool IsEnum = typeof(T).IsEnum;

    /// <summary>
    ///     Whether the type is a native flecs type.
    /// </summary>
    public static readonly bool IsFlecsType = typeof(T).ToString().StartsWith(Ecs.NativeNamespace, StringComparison.Ordinal);

    /// <summary>
    ///     The underlying integer type if this type is an enum.
    /// </summary>
    public static readonly IntegerType UnderlyingType = GetUnderlyingType();

    /// <summary>
    ///     The underlying integer type id if this type is an enum.
    /// </summary>
    public static readonly ulong UnderlyingTypeId = GetUnderlyingTypeId();

    /// <summary>
    ///     The cache indexes of all enum members if this type is an enum.
    /// </summary>
    private static readonly NativeArray<EnumMember> Constants = InitEnumCacheIndexes();

    /// <summary>
    ///     Returns the id for this type with the provided world. Registers a new component id if it doesn't exist.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <returns>The id of the type in the world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Id(ecs_world_t* world)
    {
        ref ulong cachedId = ref LookupCacheIndex(world);
        return Unsafe.IsNullRef(ref cachedId) || !ecs_is_alive(world, cachedId)
            ? RegisterComponent(world, true, true, 0, "")
            : cachedId;
    }

    /// <summary>
    ///     Returns the id for this type with the provided world. Registers a new component id if it doesn't exist.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="ignoreScope">If true, the type will be registered in the root scope with it's full type name.</param>
    /// <param name="isComponent">If true, type will be created with full component registration. (size, alignment, enums, hooks)</param>
    /// <param name="id">If an existing entity is found with this id, attempt to alias it. Otherwise, register new entity with this id.</param>
    /// <returns>The id of the type in the world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Id(ecs_world_t* world, bool ignoreScope, bool isComponent, ulong id)
    {
        ref ulong cachedId = ref LookupCacheIndex(world);
        return Unsafe.IsNullRef(ref cachedId) || !ecs_is_alive(world, cachedId)
            ? RegisterComponent(world, ignoreScope, isComponent, id, "")
            : cachedId;
    }

    /// <summary>
    ///     Returns the id for this type with the provided world. Registers a new component id if it doesn't exist.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="ignoreScope">If true, the type will be registered in the root scope with it's full type name.</param>
    /// <param name="isComponent">If true, type will be created with full component registration. (size, alignment, enums, hooks)</param>
    /// <param name="id">If an existing entity is found with this id, attempt to alias it. Otherwise, register new entity with this id.</param>
    /// <param name="name">If an existing entity is found with this name, attempt to alias it. Otherwise, register new entity with this name.</param>
    /// <returns>The id of the type in the world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Id(ecs_world_t* world, bool ignoreScope, bool isComponent, ulong id, string name)
    {
        ref ulong cachedId = ref LookupCacheIndex(world);
        return Unsafe.IsNullRef(ref cachedId) || !ecs_is_alive(world, cachedId)
            ? RegisterComponent(world, ignoreScope, isComponent, id, name)
            : cachedId;
    }

    /// <summary>
    ///     Returns the id for this enum member in the provided world. Registers a new id if it doesn't exist.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="constant">The enum member.</param>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <returns>The id of the enum member in the world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Id<TEnum>(ecs_world_t* world, TEnum constant) where TEnum : Enum, T
    {
        Id(world); // Ensures that component ids are registered for enum members.
        return LookupCacheIndex(world, GetEnumCacheIndex(constant));
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
        WorldContext* context = (WorldContext*)ecs_get_binding_ctx_fast(world);
        Ecs.Assert(context != null, "World pointer must be created or passed into World.Create() to initialize binding context.");
        ref NativeList<ulong> cache = ref context->TypeCache;
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
        WorldContext* context = (WorldContext*)ecs_get_binding_ctx_fast(world);
        Ecs.Assert(context != null, "World pointer must be created or passed into World.Create() to initialize binding context.");
        ref NativeList<ulong> cache = ref context->TypeCache;
        return ref index >= cache.Count || cache.Data[index] == 0
            ? ref Unsafe.NullRef<ulong>()
            : ref cache.Data[index];
    }

    /// <summary>
    ///     Checks if the type is registered in the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <returns>True if the type is registered in the world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRegistered(ecs_world_t* world)
    {
        ref ulong cachedId = ref LookupCacheIndex(world);
        return !Unsafe.IsNullRef(ref cachedId) && cachedId != 0 && ecs_is_alive(world, cachedId);
    }

    /// <summary>
    ///     Checks if the type is registered in the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="id">The id of the type if registered, else 0.</param>
    /// <returns>True if the type was registered.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRegistered(ecs_world_t* world, out ulong id)
    {
        ref ulong cachedId = ref LookupCacheIndex(world);

        id = !Unsafe.IsNullRef(ref cachedId) && cachedId != 0 && ecs_is_alive(world, cachedId)
            ? cachedId
            : 0;

        return id != 0;
    }

    /// <summary>
    ///     Checks if the type is registered in the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="id">The id of the type if registered, else 0.</param>
    /// <returns>True if the type was registered.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRegistered(ecs_world_t* world, out Entity id)
    {
        ref ulong cachedId = ref LookupCacheIndex(world);

        id = !Unsafe.IsNullRef(ref cachedId) && cachedId != 0 && ecs_is_alive(world, cachedId)
            ? new Entity(world, cachedId)
            : default;

        return id != 0;
    }

    /// <summary>
    ///     Registers this type with the provided world.
    /// </summary>
    /// <param name="world">The ECS world.</param>
    /// <param name="ignoreScope">If true, the type will be registered in the root scope with it's full type name.</param>
    /// <param name="isComponent">If true, type will be created with full component registration. (size, alignment, enums, hooks)</param>
    /// <param name="id">If an existing entity is found with this id, attempt to alias it. Otherwise, register new entity with this id.</param>
    /// <param name="name">If an existing entity is found with this name, attempt to alias it. Otherwise, register new entity with this name.</param>
    /// <returns>The registered id of this type.</returns>
    public static ulong RegisterComponent(World world, bool ignoreScope, bool isComponent, ulong id, string name)
    {
        Ecs.Assert(!world.GetWorld().IsReadOnly() || world.GetWorld().GetStageCount() <= 1,
            "Cannot register component while multithreaded world is progressing/readonly.");

        // If a name or id is provided, the type is being used to alias an already existing entity.
        Entity existingEntity = id != 0 ? new Entity(world, id) : world.Lookup(name, false);

        // If an existing entity is found, ensure that the size and alignment match the entity and return its id.
        if (isComponent && existingEntity != 0 || world.TryLookupSymbol(FullName, out existingEntity))
        {
            if (IsFlecsType)
                return EnsureCacheIndex(world) = existingEntity;

            ref EcsComponent info = ref existingEntity.GetMut<EcsComponent>();

            if (Unsafe.IsNullRef(ref info))
                Ecs.Assert(IsTag, $"Cannot alias '{existingEntity.Path()}' with a '{FullName}'. '{FullName}' must be a zero-sized struct");
            else
                Ecs.Assert(info.size == Size && info.alignment == Alignment, $"Layout of type '{FullName}' (Size: {Size}, Alignment: {Alignment}) does not match currently registered layout (Size: {info.size}, Alignment: {info.alignment})");

            return EnsureCacheIndex(world) = existingEntity;
        }

        Entity prevScope = world.SetScope(ignoreScope ? 0ul : world.GetScope());
        Entity prevWith = world.SetWith(ignoreScope ? 0ul : world.GetWith());

        using NativeString nativeSymbol = (NativeString)FullName;
        using NativeString nativeName = string.IsNullOrEmpty(name)
            ? (NativeString)GetTrimmedTypeName(world)
            : (NativeString)name;

        ecs_entity_desc_t entityDesc = default;
        entityDesc.id = id;
        entityDesc.use_low_id = true;
        entityDesc.name = nativeName;
        entityDesc.symbol = nativeSymbol;
        entityDesc.sep = Pointers.DefaultSeparator;
        entityDesc.root_sep = Pointers.DefaultSeparator;
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

        // Store the type in the component.
        if (typeof(T) != typeof(Type))
            RegisterDotnetType(world, world.Entity(component));

        if (typeof(T).IsEnum)
            RegisterConstants(world, world.Entity(component));

        if (IsTag)
            return component;

        // Register type hooks.
        TypeHooksContext hooksContext = TypeHooksContext.Default;

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            hooksContext.Ctor.Invoker = (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedCtorCallback<T>;
            hooksContext.Dtor.Invoker = (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedDtorCallback<T>;
            hooksContext.Copy.Invoker = (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedCopyCallback<T>;
            hooksContext.Move.Invoker = (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.DefaultManagedMoveCallback<T>;
        }

        if (typeof(ICtorHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.Ctor.Invoker = RuntimeHelpers.IsReferenceOrContainsReferences<T>()
                ? (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCtorCallbackPointer<T>
                : (delegate*<T*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCtorCallbackPointer<T>;

            hooksContext.Ctor.Pointer = (void*)(nint)typeof(ICtorHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        if (typeof(IDtorHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.Dtor.Invoker = RuntimeHelpers.IsReferenceOrContainsReferences<T>()
                ? (delegate*<GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedDtorCallbackPointer<T>
                : (delegate*<T*, int, ecs_type_info_t*, void>)&Functions.UnmanagedDtorCallbackPointer<T>;

            hooksContext.Dtor.Pointer = (void*)(nint)typeof(IDtorHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        if (typeof(ICopyHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.Copy.Invoker = RuntimeHelpers.IsReferenceOrContainsReferences<T>()
                ? (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedCopyCallbackPointer<T>
                : (delegate*<T*, T*, int, ecs_type_info_t*, void>)&Functions.UnmanagedCopyCallbackPointer<T>;

            hooksContext.Copy.Pointer = (void*)(nint)typeof(ICopyHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        if (typeof(IMoveHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.Move.Invoker = RuntimeHelpers.IsReferenceOrContainsReferences<T>()
                ? (delegate*<GCHandle*, GCHandle*, int, ecs_type_info_t*, void>)&Functions.ManagedMoveCallbackPointer<T>
                : (delegate*<T*, T*, int, ecs_type_info_t*, void>)&Functions.UnmanagedMoveCallbackPointer<T>;

            hooksContext.Move.Pointer = (void*)(nint)typeof(IMoveHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        if (typeof(IOnAddHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.OnAdd.Invoker = (delegate*<ecs_iter_t*, void>)&Functions.OnAddEachIterRefCallbackPointer<T>;
            hooksContext.OnAdd.Pointer = (void*)(nint)typeof(IOnAddHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        if (typeof(IOnRemoveHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.OnRemove.Invoker = (delegate*<ecs_iter_t*, void>)&Functions.OnRemoveEachIterRefCallbackPointer<T>;
            hooksContext.OnRemove.Pointer = (void*)(nint)typeof(IOnRemoveHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        if (typeof(IOnSetHook<T>).IsAssignableFrom(typeof(T)))
        {
            hooksContext.OnSet.Invoker = (delegate*<ecs_iter_t*, void>)&Functions.OnSetEachIterRefCallbackPointer<T>;
            hooksContext.OnSet.Pointer = (void*)(nint)typeof(IOnSetHook<>.FunctionPointer<>)
                .MakeGenericType(typeof(T), typeof(T))
                .GetMethod("Get")!
                .Invoke(null, null)!;
        }

        ecs_type_hooks_t hooks = default;
        hooks.ctor = hooksContext.Ctor == default ? default : &Functions.CtorCallback;
        hooks.dtor = hooksContext.Dtor == default ? default : &Functions.DtorCallback;
        hooks.copy = hooksContext.Copy == default ? default : &Functions.CopyCallback;
        hooks.move = hooksContext.Move == default ? default : &Functions.MoveCallback;
        hooks.on_add = hooksContext.OnAdd == default ? default : &Functions.OnAddCallback;
        hooks.on_remove = hooksContext.OnRemove == default ? default : &Functions.OnRemoveCallback;
        hooks.on_set = hooksContext.OnSet == default ? default : &Functions.OnSetCallback;
        hooks.binding_ctx = Memory.Alloc(hooksContext);
        hooks.binding_ctx_free = &Functions.TypeHooksContextFree;

        ecs_set_hooks_id(world, component, &hooks);

        return component;
    }

    /// <summary>
    ///     Returns a trimmed version of this type's full name with respect to the current scope of the world.
    /// </summary>
    /// <param name="world">The world.</param>
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

    private static NativeArray<EnumMember> InitEnumCacheIndexes()
    {
        if (!IsEnum)
            return default;

        Array values = typeof(T).GetEnumValuesAsUnderlyingType();
        NativeArray<EnumMember> constants = new NativeArray<EnumMember>(values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            long value = UnderlyingType switch
            {
                IntegerType.SByte => (sbyte)values.GetValue(i)!,
                IntegerType.Byte => (byte)values.GetValue(i)!,
                IntegerType.Int16 => (short)values.GetValue(i)!,
                IntegerType.UInt16 => (ushort)values.GetValue(i)!,
                IntegerType.Int32 => (int)values.GetValue(i)!,
                IntegerType.UInt32 => (uint)values.GetValue(i)!,
                IntegerType.Int64 => (long)values.GetValue(i)!,
                IntegerType.UInt64 => (long)(ulong)values.GetValue(i)!,
                _ => throw new Ecs.ErrorException("Type is not an enum.")
            };
            constants[i] = new EnumMember(value, Interlocked.Increment(ref Ecs.CacheIndexCount));
        }

        return constants;
    }

    private static void RegisterDotnetType(World world, Entity type)
    {
        ecs_suspend_readonly_state_t state = default;
        world = flecs_suspend_readonly(world, &state);
        type.Set(typeof(T));
        flecs_resume_readonly(world, &state);
    }

    private static void RegisterConstants(World world, Entity type)
    {
        ecs_suspend_readonly_state_t state = default;
        world = flecs_suspend_readonly(world, &state);

        type.Set(new EcsEnum { underlying_type = UnderlyingTypeId });

        Entity prevScope = world.SetScope(type);

        for (int i = 0; i < Constants.Length; i++)
        {
            long value = Constants[i].Value;
            string name = Unsafe.As<long, T>(ref value)!.ToString()!;
            EnsureCacheIndex(world, Constants[i].CacheIndex) =
                world.Entity(name).SetUntyped(EcsConstant, UnderlyingTypeId, sizeof(T), &value);
        }

        world.SetScope(prevScope);

        flecs_resume_readonly(world, &state);
    }

    // Binary search enum list for enum constant's cache index.
    private static int GetEnumCacheIndex<TEnum>(TEnum constant) where TEnum : Enum, T
    {
        long value = GetEnumConstantAsLong(constant);

        int left = 0;
        int right = Constants.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            ref EnumMember data = ref Constants.Data[mid];

            if (data.Value == value)
                return data.CacheIndex;

            if (data.Value < value)
                left = ++mid;
            else
                right = --mid;
        }

        Ecs.Assert(false, $"No id registered for '{constant}'");
        return 0;
    }

    private static long GetEnumConstantAsLong<TEnum>(TEnum constant) where TEnum : Enum, T
    {
        return UnderlyingType switch
        {
            IntegerType.SByte => Unsafe.As<TEnum, sbyte>(ref constant),
            IntegerType.Byte => Unsafe.As<TEnum, byte>(ref constant),
            IntegerType.Int16 => Unsafe.As<TEnum, short>(ref constant),
            IntegerType.UInt16 => Unsafe.As<TEnum, ushort>(ref constant),
            IntegerType.Int32 => Unsafe.As<TEnum, int>(ref constant),
            IntegerType.UInt32 => Unsafe.As<TEnum, uint>(ref constant),
            IntegerType.Int64 => Unsafe.As<TEnum, long>(ref constant),
            IntegerType.UInt64 => (long)Unsafe.As<TEnum, ulong>(ref constant),
            _ => throw new Ecs.ErrorException("Type is not an enum.")
        };
    }

    private static IntegerType GetUnderlyingType()
    {
        if (!IsEnum)
            return IntegerType.None;

        Type underlyingType = typeof(T).GetEnumUnderlyingType();

        if (underlyingType == typeof(sbyte))
            return IntegerType.SByte;

        if (underlyingType == typeof(byte))
            return IntegerType.Byte;

        if (underlyingType == typeof(short))
            return IntegerType.Int16;

        if (underlyingType == typeof(ushort))
            return IntegerType.UInt16;

        if (underlyingType == typeof(int))
            return IntegerType.Int32;

        if (underlyingType == typeof(uint))
            return IntegerType.UInt32;

        if (underlyingType == typeof(long))
            return IntegerType.Int64;

        if (underlyingType == typeof(ulong))
            return IntegerType.UInt64;

        return IntegerType.None;
    }

    private static ulong GetUnderlyingTypeId()
    {
        return UnderlyingType switch
        {
            IntegerType.SByte => FLECS_IDecs_i8_tID_,
            IntegerType.Byte => FLECS_IDecs_u8_tID_,
            IntegerType.Int16 => FLECS_IDecs_i16_tID_,
            IntegerType.UInt16 => FLECS_IDecs_u16_tID_,
            IntegerType.Int32 => FLECS_IDecs_i32_tID_,
            IntegerType.UInt32 => FLECS_IDecs_u32_tID_,
            IntegerType.Int64 => FLECS_IDecs_i64_tID_,
            IntegerType.UInt64 => FLECS_IDecs_u64_tID_,
            _ => 0
        };
    }

    private static string GetName()
    {
        string fullname = GetFullName();

        int trimEnd;

        if (fullname.Contains('<', StringComparison.Ordinal))
            trimEnd = fullname.LastIndexOf('.', fullname.IndexOf('<', StringComparison.Ordinal)) + 1;
        else
            trimEnd = fullname.LastIndexOf('.') + 1;

        return fullname[trimEnd..];
    }

    private static string GetFullName()
    {
        string name = typeof(T).ToString();

        // File-local types are prefixed with a file name + GUID.
        if (Ecs.StripFileLocalTypeNameGuid)
        {
            int start = 0;
            bool skip = false;

            StringBuilder stringBuilder = new StringBuilder();

            for (int current = 0; current < name.Length;)
            {
                char c = name[current];

                if (skip && c == '_')
                {
                    skip = false;
                    start = current + 2;
                }
                else if (!skip && c == '<')
                {
                    skip = true;
                    stringBuilder.Append(name.AsSpan(start, current - start));
                    current = name.IndexOf('>', current) + 1;
                    continue;
                }

                current++;
            }

            stringBuilder.Append(name.AsSpan(start));
            name = stringBuilder.ToString();
        }

        {
            name = name
                .Replace(Ecs.NativeNamespace, string.Empty, StringComparison.Ordinal)
                .Replace('+', '.')
                .Replace('[', '<')
                .Replace(']', '>');

            int start = 0;
            int current = 0;
            bool skip = false;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in name)
            {
                if (skip && (c == '<' || c == '.'))
                {
                    start = current;
                    skip = false;
                }
                else if (!skip && c == '`')
                {
                    stringBuilder.Append(name.AsSpan(start, current - start));
                    skip = true;
                }

                current++;
            }

            return stringBuilder.Append(name.AsSpan(start)).ToString();
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
    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Local")]
    private readonly record struct AlignOfHelper(byte Dummy, T Data);

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    private readonly record struct EnumMember(long Value, int CacheIndex);

    /// <summary>
    ///     Represents the underlying integer type of an enum.
    /// </summary>
    public enum IntegerType
    {
        /// <summary>
        ///     This type is not an enum.
        /// </summary>
        None,

        /// <summary>
        ///     <see cref="sbyte"/>
        /// </summary>
        SByte,

        /// <summary>
        ///     <see cref="byte"/>
        /// </summary>
        Byte,

        /// <summary>
        ///     <see cref="short"/>
        /// </summary>
        Int16,

        /// <summary>
        ///     <see cref="ushort"/>
        /// </summary>
        UInt16,

        /// <summary>
        ///     <see cref="int"/>
        /// </summary>
        Int32,

        /// <summary>
        ///     <see cref="uint"/>
        /// </summary>
        UInt32,

        /// <summary>
        ///     <see cref="long"/>
        /// </summary>
        Int64,

        /// <summary>
        ///     <see cref="ulong"/>
        /// </summary>
        UInt64
    }
}
