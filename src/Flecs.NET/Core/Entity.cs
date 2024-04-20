using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for working with entities.
    /// </summary>
    public unsafe partial struct Entity : IEquatable<Entity>
    {
        private Id _id;

        /// <summary>
        ///     Reference to id.
        /// </summary>
        public ref Id Id => ref _id;

        /// <summary>
        ///     Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _id.World;

        /// <summary>
        ///     Returns a null entity.
        /// </summary>
        /// <returns></returns>
        public static Entity Null()
        {
            return default;
        }

        /// <summary>
        ///     Returns a null entity for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public static Entity Null(ecs_world_t* world)
        {
            return new Entity(world, 0);
        }

        /// <summary>
        ///     Creates an entity with the provided id.
        /// </summary>
        /// <param name="id"></param>
        public Entity(ulong id)
        {
            _id = id;
        }

        /// <summary>
        ///     Creates an entity for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public Entity(ecs_world_t* world)
        {
            if (ecs_get_scope(world) == 0 && ecs_get_with(world) == 0)
            {
                _id = new Id(world, ecs_new(world));
            }
            else
            {
                ecs_entity_desc_t desc = default;
                _id = new Id(world, ecs_entity_init(world, &desc));
            }
        }

        /// <summary>
        ///     Creates an entity from the provided world and id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public Entity(ecs_world_t* world, ulong id)
        {
            _id = new Id(world, id);
        }

        /// <summary>
        ///     Creates an entity from the provided world and name.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public Entity(ecs_world_t* world, string name)
        {
            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.sep = BindingContext.DefaultSeparator;
            desc.root_sep = BindingContext.DefaultSeparator;

            _id = new Id(world, ecs_entity_init(world, &desc));
        }

        /// <inheritdoc cref="Id.IsPair()"/>
        public bool IsPair()
        {
            return Id.IsPair();
        }

        /// <inheritdoc cref="Id.IsWildCard()"/>
        public bool IsWildCard()
        {
            return Id.IsWildCard();
        }

        /// <inheritdoc cref="Id.IsEntity()"/>
        public bool IsEntity()
        {
            return Id.IsEntity();
        }

        /// <inheritdoc cref="Id.AddFlags(ulong)"/>
        public Entity AddFlags(ulong flags)
        {
            return Id.AddFlags(flags);
        }

        /// <inheritdoc cref="Id.RemoveFlags(ulong)"/>
        public Entity RemoveFlags(ulong flags)
        {
            return Id.RemoveFlags(flags);
        }

        /// <inheritdoc cref="Id.RemoveFlags()"/>
        public Entity RemoveFlags()
        {
            return Id.RemoveFlags();
        }

        /// <inheritdoc cref="Id.RemoveGeneration()"/>
        public Entity RemoveGeneration()
        {
            return Id.RemoveGeneration();
        }

        /// <inheritdoc cref="Id.TypeId()"/>
        public Entity TypeId()
        {
            return Id.TypeId();
        }

        /// <inheritdoc cref="Id.HasFlags(ulong)"/>
        public bool HasFlags(ulong flags)
        {
            return Id.HasFlags(flags);
        }

        /// <inheritdoc cref="Id.HasFlags()"/>
        public bool HasFlags()
        {
            return Id.HasFlags();
        }

        /// <inheritdoc cref="Id.Flags()"/>
        public Entity Flags()
        {
            return Id.Flags();
        }

        /// <inheritdoc cref="Id.HasRelation(ulong)"/>
        public bool HasRelation(ulong first)
        {
            return Id.HasRelation(first);
        }

        /// <inheritdoc cref="Id.First()"/>
        public Entity First()
        {
            return Id.First();
        }

        /// <inheritdoc cref="Id.Second()"/>
        public Entity Second()
        {
            return Id.Second();
        }

        /// <inheritdoc cref="Id.Str()"/>
        public string Str()
        {
            return Id.Str();
        }

        /// <inheritdoc cref="Id.FlagsStr()"/>
        public string FlagsStr()
        {
            return Id.FlagsStr();
        }

        /// <inheritdoc cref="Id.CsWorld()"/>
        public World CsWorld()
        {
            return Id.CsWorld();
        }

        /// <summary>
        ///     Check if entity is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return World != null && ecs_is_valid(World, Id) == 1;
        }

        /// <summary>
        ///     Check if entity is alive.
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return World != null && ecs_is_alive(World, Id) == 1;
        }

        /// <summary>
        ///     Return the entity name.
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return NativeString.GetString(ecs_get_name(World, Id));
        }

        /// <summary>
        ///     Return the entity symbol.
        /// </summary>
        /// <returns></returns>
        public string Symbol()
        {
            return NativeString.GetString(ecs_get_symbol(World, Id));
        }

        /// <summary>
        ///     Return the entity path.
        /// </summary>
        /// <param name="sep"></param>
        /// <param name="initSep"></param>
        /// <returns></returns>
        public string Path(string sep = Ecs.DefaultSeparator, string initSep = Ecs.DefaultSeparator)
        {
            return PathFrom(0, sep, initSep);
        }

        /// <summary>
        ///     Return the entity path relative to a parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="sep"></param>
        /// <param name="initSep"></param>
        /// <returns></returns>
        public string PathFrom(ulong parent, string sep = Ecs.DefaultSeparator, string initSep = Ecs.DefaultSeparator)
        {
            using NativeString nativeSep = (NativeString)sep;
            using NativeString nativeInitSep = (NativeString)initSep;

            return NativeString.GetStringAndFree(ecs_get_path_w_sep(World, parent, Id, nativeSep, nativeInitSep));
        }

        /// <summary>
        ///     Return the entity path relative to a parent.
        /// </summary>
        /// <param name="sep"></param>
        /// <param name="initSep"></param>
        /// <typeparam name="TParent"></typeparam>
        /// <returns></returns>
        public string PathFrom<TParent>(string sep = Ecs.DefaultSeparator, string initSep = Ecs.DefaultSeparator)
        {
            return PathFrom(Type<TParent>.Id(World), sep, initSep);
        }

        /// <summary>
        ///     Check if entity is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled()
        {
            return ecs_has_id(World, Id, EcsDisabled) == 0;
        }

        /// <summary>
        ///     Checks if id is enabled.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Enabled(ulong id)
        {
            return ecs_is_enabled_id(World, Id, id) == 1;
        }

        /// <summary>
        ///     Checks if pair is enabled.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool Enabled(ulong first, ulong second)
        {
            return Enabled(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Checks if type is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Enabled<T>()
        {
            return Enabled(Type<T>.Id(World));
        }

        /// <summary>
        ///     Checks if type is enabled.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Enabled<T>(T value) where T : Enum
        {
            return Enabled<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Checks if pair is enabled.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public bool Enabled<TFirst>(ulong second)
        {
            return Enabled(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Checks if pair is enabled.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Enabled<TFirst, TSecond>()
        {
            return Enabled(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Checks if pair is enabled.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Enabled<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Enabled<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Checks if pair is enabled.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Enabled<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return EnabledSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Checks if pair is enabled.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool EnabledSecond<TSecond>(ulong first)
        {
            return Enabled(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Get the entity's type.
        /// </summary>
        /// <returns></returns>
        public Types Type()
        {
            return new Types(World, ecs_get_type(World, Id));
        }

        /// <summary>
        ///     Get the entity's table.
        /// </summary>
        /// <returns></returns>
        public Table Table()
        {
            return new Table(World, ecs_get_table(World, Id));
        }

        /// <summary>
        ///     Get table range for the entity.
        /// </summary>
        /// <returns></returns>
        public Table Range()
        {
            ecs_record_t* r = ecs_record_find(World, Id);
            return r != null ? new Table(World, r->table, Macros.RecordToRow(r->row), 1) : new Table();
        }

        /// <summary>
        ///     Iterate (component) ids of an entity.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachIdCallback func)
        {
            ecs_type_t* type = ecs_get_type(World, Id);

            if (type == null)
                return;

            ulong* ids = type->array;
            int count = type->count;

            for (int i = 0; i < count; i++)
                func(new Id(World, ids[i]));
        }

        /// <summary>
        ///     Iterate matching pair ids of an entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="func"></param>
        public void Each(ulong first, ulong second, Ecs.EachIdCallback func)
        {
            ecs_world_t* realWorld = ecs_get_world(World);
            ecs_table_t* table = ecs_get_table(World, Id);

            if (table == null)
                return;

            ecs_type_t* type = ecs_table_get_type(table);

            if (type == null)
                return;

            ulong pattern = first;

            if (second != 0)
                pattern = Macros.Pair(first, second);

            int cur = 0;
            ulong* ids = type->array;

            while (-1 != (cur = ecs_search_offset(realWorld, table, cur, pattern, null)))
            {
                func(new Id(World, ids[cur]));
                cur++;
            }
        }

        /// <summary>
        ///     Iterate targets for a given relationship.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="func"></param>
        public void Each(ulong relation, Ecs.EachEntityCallback func)
        {
            Each(relation, EcsWildcard, id => { func(id.Second()); });
        }

        /// <summary>
        ///     Iterate targets for a given relationship.
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Each<TFirst>(Ecs.EachEntityCallback func)
        {
            Each(Type<TFirst>.Id(World), func);
        }

        /// <summary>
        ///     Iterate targets for a given relationship.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="callback"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Each<TFirst>(TFirst relation, Ecs.EachEntityCallback callback) where TFirst : Enum
        {
            Each(Type<TFirst>.Id(World, relation), callback);
        }

        /// <summary>
        ///     Iterate children for entity.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="callback"></param>
        public void Children(ulong relation, Ecs.EachEntityCallback callback)
        {
            if (Id == EcsWildcard || Id == EcsAny)
                return;

            ecs_iter_t it = ecs_each_id(World, Macros.Pair(relation, Id));
            while (Macros.Bool(ecs_each_next(&it)))
                Invoker.Each(&it, callback);
        }

        /// <summary>
        ///     Iterate children for entity.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="TRel"></typeparam>
        public void Children<TRel>(Ecs.EachEntityCallback callback)
        {
            Children(Type<TRel>.Id(World), callback);
        }

        /// <summary>
        ///     Iterate children for entity.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="callback"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Children<TFirst>(TFirst relation, Ecs.EachEntityCallback callback) where TFirst : Enum
        {
            Children(Type<TFirst>.Id(World, relation), callback);
        }

        /// <summary>
        ///     Iterate children for entity.
        /// </summary>
        /// <param name="callback"></param>
        public void Children(Ecs.EachEntityCallback callback)
        {
            Children(EcsChildOf, callback);
        }

        /// <summary>
        ///     Get pointer to component value.
        /// </summary>
        /// <param name="compId"></param>
        /// <returns></returns>
        public void* GetPtr(ulong compId)
        {
            return ecs_get_id(World, Id, compId);
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public void* GetPtr(ulong first, ulong second)
        {
            return GetPtr(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Get pointer to component value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* GetPtr<T>() where T : unmanaged
        {
            ulong componentId = Type<T>.Id(World);

            if (!typeof(T).IsEnum)
            {
                Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
                return (T*)ecs_get_id(World, Id, componentId);
            }

            ulong target = ecs_get_target(World, Id, componentId, 0);

            if (target == 0)
                return (T*)ecs_get_id(World, Id, componentId);

            void* ptr = ecs_get_id(World, target, componentId);
            Ecs.Assert(ptr != null, "Missing enum constant value");
            return (T*)ptr;
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetPtr(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst, TSecond>(TSecond second)
            where TFirst : unmanaged
            where TSecond : Enum
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetPtr(Macros.Pair<TFirst, TSecond>(second, World));
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetPtr<TFirst, TSecond>(TFirst first)
            where TFirst : Enum
            where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetPtr(Macros.Pair<TFirst, TSecond>(first, World));
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetPtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetPtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetPtr(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Get managed reference to component value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref readonly T Get<T>()
        {
            ulong componentId = Type<T>.Id(World);

            if (!typeof(T).IsEnum)
            {
                Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
                return ref Managed.GetTypeRef<T>(ecs_get_id(World, Id, componentId));
            }

            ulong target = ecs_get_target(World, Id, componentId, 0);

            if (target == 0)
                return ref Managed.GetTypeRef<T>(ecs_get_id(World, Id, componentId));

            void* ptr = ecs_get_id(World, target, componentId);
            Ecs.Assert(ptr != null, "Missing enum constant value");
            return ref Managed.GetTypeRef<T>(ptr);
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst Get<TFirst>(ulong second)
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(GetPtr(Macros.Pair<TFirst>(second, World)));
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst Get<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(GetPtr(Macros.Pair<TFirst, TSecond>(second, World)));
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TSecond Get<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(GetPtr(Macros.Pair<TFirst, TSecond>(first, World)));
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst GetFirst<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(GetPtr(Macros.Pair<TFirst, TSecond>(World)));
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TSecond GetSecond<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(GetPtr(Macros.Pair<TFirst, TSecond>(World)));
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TSecond GetSecond<TSecond>(ulong first)
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(GetPtr(Macros.PairSecond<TSecond>(first, World)));
        }

        /// <summary>
        ///     Get mutable component value (untyped).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void* GetMutPtr(ulong id)
        {
            return ecs_get_mut_id(World, Id, id);
        }

        /// <summary>
        ///     Get mutable pointer for a pair (untyped).
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public void* GetMutPtr(ulong first, ulong second)
        {
            return GetMutPtr(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Get mutable component value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* GetMutPtr<T>() where T : unmanaged
        {
            ulong componentId = Type<T>.Id(World);

            if (!typeof(T).IsEnum)
            {
                Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
                return (T*)ecs_get_mut_id(World, Id, componentId);
            }

            ulong target = ecs_get_target(World, Id, componentId, 0);

            if (target == 0)
                return (T*)ecs_get_mut_id(World, Id, componentId);

            void* ptr = ecs_get_mut_id(World, target, componentId);
            Ecs.Assert(ptr != null, "Missing enum constant value");
            return (T*)ptr;
        }

        /// <summary>
        ///     Get mutable pointer for the first element of a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetMutPtr(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutPtr<TFirst, TSecond>(TSecond second)
            where TFirst : unmanaged
            where TSecond : Enum
        {
            return GetMutPtr<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetMutPtr<TFirst, TSecond>(TFirst first)
            where TFirst : Enum
            where TSecond : unmanaged
        {
            return GetMutSecondPtr<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetMutPtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetMutSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetMutPtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetMutSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetMutPtr(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Get mutable managed reference for component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref T GetMut<T>()
        {
            ulong componentId = Type<T>.Id(World);

            if (!typeof(T).IsEnum)
            {
                Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
                return ref Managed.GetTypeRef<T>(ecs_get_mut_id(World, Id, componentId));
            }

            ulong target = ecs_get_target(World, Id, componentId, 0);

            if (target == 0)
                return ref Managed.GetTypeRef<T>(ecs_get_mut_id(World, Id, componentId));

            void* ptr = ecs_get_mut_id(World, target, componentId);
            Ecs.Assert(ptr != null, "Missing enum constant value");
            return ref Managed.GetTypeRef<T>(ptr);
        }

        /// <summary>
        ///     Get mutable managed reference for the first element of a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMut<TFirst>(ulong second)
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(GetMutPtr(Macros.Pair<TFirst>(second, World)));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMut<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref GetMut<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond GetMut<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref GetMutSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMutFirst<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(GetMutPtr(Macros.Pair<TFirst, TSecond>(World)));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond GetMutSecond<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(GetMutPtr(Macros.Pair<TFirst, TSecond>(World)));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond GetMutSecond<TSecond>(ulong first)
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(GetMutPtr(Macros.PairSecond<TSecond>(first, World)));
        }

        /// <summary>
        ///     Get target for a given pair.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Entity Target(ulong relation, int index = 0)
        {
            return new Entity(World, ecs_get_target(World, Id, relation, index));
        }

        /// <summary>
        ///     Get target for a given pair.
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Target<T>(int index = 0) where T : unmanaged
        {
            return new Entity(World, ecs_get_target(World, Id, Type<T>.Id(World), index));
        }

        /// <summary>
        ///     Get the target of a pair for a given relationship id.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity TargetFor(ulong relation, ulong id)
        {
            return new Entity(World, ecs_get_target_for_id(World, Id, relation, id));
        }

        /// <summary>
        ///     Get the target of a pair for a given relationship id.
        /// </summary>
        /// <param name="relation"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity TargetFor<T>(ulong relation)
        {
            return new Entity(World, TargetFor(relation, Type<T>.Id(World)));
        }

        /// <summary>
        ///     Get the target of a pair for a given relationship id.
        /// </summary>
        /// <param name="relation"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Entity TargetFor<TFirst, TSecond>(ulong relation)
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return new Entity(World, TargetFor(relation, pair));
        }

        /// <summary>
        ///     Get depth for given relationship.
        /// </summary>
        /// <param name="rel"></param>
        /// <returns></returns>
        public int Depth(ulong rel)
        {
            return ecs_get_depth(World, Id, rel);
        }

        /// <summary>
        ///     Get depth for given relationship.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Depth<T>()
        {
            return Depth(Type<T>.Id(World));
        }

        /// <summary>
        ///     Get depth for given relationship.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Depth<T>(T value) where T : Enum
        {
            return Depth(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Get parent of entity.
        /// </summary>
        /// <returns></returns>
        public Entity Parent()
        {
            return Target(EcsChildOf);
        }

        /// <summary>
        ///     Lookup an entity from a path.
        /// </summary>
        /// <param name="path">The path to resolve.</param>
        /// <param name="recursive">Recursively traverse up the tree until entity is found.</param>
        /// <returns>True if the entity was found, else false.</returns>
        public Entity Lookup(string path, bool recursive = false)
        {
            Ecs.Assert(Id != 0, "Invalid lookup from null handle.");
            using NativeString nativePath = (NativeString)path;
            ulong id = ecs_lookup_path_w_sep(World, Id, nativePath,
                BindingContext.DefaultSeparator, BindingContext.DefaultSeparator, Macros.Bool(recursive));
            return new Entity(World, id);
        }

        /// <summary>
        ///     Check if entity has the provided entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Has(ulong id)
        {
            return Macros.Bool(ecs_has_id(World, Id, id));
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool Has(ulong first, ulong second)
        {
            return Has(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Check if entity has the provided component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>()
        {
            ulong typeId = Type<T>.Id(World);
            bool result = Macros.Bool(ecs_has_id(World, Id, typeId));

            if (result)
                return result;

            return typeof(T).IsEnum && Has(typeId, EcsWildcard);
        }

        /// <summary>
        ///     Check if entity has the provided enum constant.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>(T value) where T : Enum
        {
            return Has<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst>(ulong second)
        {
            return Has(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>()
        {
            return Has(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Has<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return HasSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool HasSecond<TSecond>(ulong first)
        {
            return Has(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Check if entity owns the provided entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Owns(ulong id)
        {
            return ecs_owns_id(World, Id, id) == 1;
        }

        /// <summary>
        ///     Check if entity owns the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool Owns(ulong first, ulong second)
        {
            return Owns(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Check if entity owns the provided component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Owns<T>()
        {
            return Owns(Type<T>.Id(World));
        }

        /// <summary>
        ///     Check if entity owns the provided component.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Owns<T>(T value) where T : Enum
        {
            return Owns<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Check if entity owns the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public bool Owns<TFirst>(ulong second)
        {
            return Owns(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Check if entity owns the provided pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Owns<TFirst, TSecond>()
        {
            return Owns(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Check if entity owns the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Owns<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Owns<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Check if entity owns the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Owns<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return OwnsSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Check if entity owns the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool OwnsSecond<TSecond>(ulong first)
        {
            return Owns(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Clones the entity.
        /// </summary>
        /// <param name="cloneValue"></param>
        /// <param name="dstId"></param>
        /// <returns></returns>
        public Entity Clone(bool cloneValue = true, ulong dstId = 0)
        {
            if (dstId == 0)
                dstId = ecs_new(World);

            Entity dst = new Entity(World, dstId);
            ecs_clone(World, dstId, Id, Macros.Bool(cloneValue));
            return dst;
        }

        /// <summary>
        ///     Return mutable entity handle for current stage.
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public Entity Mut(ref World stage)
        {
            Ecs.Assert(!stage.IsReadOnly(), "Cannot use readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(stage);
        }

        /// <summary>
        ///     Return mutable entity handle for current iter.
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public Entity Mut(ref Iter it)
        {
            Ecs.Assert(!it.World().IsReadOnly(),
                "Cannot use iterator created for readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(it.World());
        }

        /// <summary>
        ///     Return mutable entity handle for current entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Mut(ref Entity entity)
        {
            Ecs.Assert(!entity.CsWorld().IsReadOnly(),
                "Cannot use entity created for readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(entity.World);
        }

        /// <summary>
        ///     Return mutable entity handle for current stage.
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public Entity Mut(World stage)
        {
            return Mut(ref stage);
        }

        /// <summary>
        ///     Return mutable entity handle for current iter.
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public Entity Mut(Iter it)
        {
            return Mut(ref it);
        }

        /// <summary>
        ///     Return mutable entity handle for current entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Mut(Entity entity)
        {
            return Mut(ref entity);
        }

        private Entity SetStage(ecs_world_t* stage)
        {
            return new Entity(stage, Id);
        }

        /// <summary>
        ///     Serialize entity to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(ecs_entity_to_json_desc_t* desc)
        {
            return NativeString.GetStringAndFree(ecs_entity_to_json(World, Id, desc));
        }

        /// <summary>
        ///     Serialize entity to JSON.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return ToJson(null);
        }

        /// <summary>
        ///     Serialize entity to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(ref EntityToJsonDesc desc)
        {
            fixed (ecs_entity_to_json_desc_t* ptr = &desc.Desc)
            {
                return ToJson(ptr);
            }
        }

        /// <summary>
        ///     Serialize entity to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(EntityToJsonDesc desc)
        {
            return ToJson(ref desc);
        }

        /// <summary>
        ///     Returns the entity's doc name.
        /// </summary>
        /// <returns></returns>
        public string DocName()
        {
            return NativeString.GetString(ecs_doc_get_name(World, Id));
        }

        /// <summary>
        ///     Returns the entity's doc brief.
        /// </summary>
        /// <returns></returns>
        public string DocBrief()
        {
            return NativeString.GetString(ecs_doc_get_brief(World, Id));
        }

        /// <summary>
        ///     Returns the entity's doc detail.
        /// </summary>
        /// <returns></returns>
        public string DocDetail()
        {
            return NativeString.GetString(ecs_doc_get_detail(World, Id));
        }

        /// <summary>
        ///     Returns the entity's doc detail.
        /// </summary>
        /// <returns></returns>
        public string DocLink()
        {
            return NativeString.GetString(ecs_doc_get_link(World, Id));
        }

        /// <summary>
        ///     Returns the entity's doc color.
        /// </summary>
        /// <returns></returns>
        public string DocColor()
        {
            return NativeString.GetString(ecs_doc_get_color(World, Id));
        }

        /// <summary>
        ///     Return number of alerts for entity.
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        public int AlertCount(ulong alert = 0)
        {
            return ecs_get_alert_count(World, Id, alert);
        }

        /// <summary>
        ///     Convert entity to enum constant.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ToConstant<T>() where T : unmanaged, Enum
        {
            T* ptr = GetPtr<T>();
            Ecs.Assert(ptr != null, "Entity is not a constant");
            return *ptr;
        }

        /// <summary>
        ///     Emits an event for this entity.
        /// </summary>
        /// <param name="eventId"></param>
        public void Emit(ulong eventId)
        {
            new World(World)
                .Event(eventId)
                .Entity(Id)
                .Emit();
        }

        /// <summary>
        ///     Emits an event for this entity.
        /// </summary>
        /// <param name="eventId"></param>
        public void Emit(Entity eventId)
        {
            Emit(eventId.Id.Value);
        }

        /// <summary>
        ///     Emits an event for this entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Emit<T>()
        {
            Emit(Type<T>.Id(World));
        }

        /// <summary>
        ///     Emits an event for this entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Emit<T>(T payload) where T : unmanaged
        {
            new World(World)
                .Event(Type<T>.Id(World))
                .Entity(Id)
                .Ctx(&payload)
                .Emit();
        }

        /// <summary>
        ///     Emits an event for this entity.
        /// </summary>
        /// <param name="payload"></param>
        /// <typeparam name="T"></typeparam>
        public void Emit<T>(ref T payload)
        {
            fixed (T* ptr = &payload)
            {
                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                {
                    // TODO: Get rid of GC handle here and just use raw pointer since the object is pinned.
                    IntPtr gcHandle;
                    Managed.AllocGcHandle(&gcHandle, ref payload);

                    new World(World)
                        .Event(Type<T>.Id(World))
                        .Entity(Id)
                        .Ctx(&gcHandle)
                        .Emit();
                }
                else
                {
                    new World(World)
                        .Event(Type<T>.Id(World))
                        .Entity(Id)
                        .Ctx(ptr)
                        .Emit();
                }
            }
        }

        /// <summary>
        ///     Enqueues an event for this entity.
        /// </summary>
        /// <param name="eventId"></param>
        public void Enqueue(ulong eventId)
        {
            new World(World)
                .Event(eventId)
                .Entity(Id)
                .Enqueue();
        }

        /// <summary>
        ///     Enqueues an event for this entity.
        /// </summary>
        /// <param name="eventId"></param>
        public void Enqueue(Entity eventId)
        {
            Enqueue(eventId.Id.Value);
        }

        /// <summary>
        ///     Enqueues an event for this entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Enqueue<T>()
        {
            Enqueue(Type<T>.Id(World));
        }

        /// <summary>
        ///     Enqueues an event for this entity.
        /// </summary>
        /// <param name="payload"></param>
        /// <typeparam name="T"></typeparam>
        public void Enqueue<T>(T payload) where T : unmanaged
        {
            new World(World)
                .Event(Type<T>.Id(World))
                .Entity(Id)
                .Ctx(&payload)
                .Enqueue();
        }

        /// <summary>
        ///     Enqueues an event for this entity.
        /// </summary>
        /// <param name="payload"></param>
        /// <typeparam name="T"></typeparam>
        public void Enqueue<T>(ref T payload)
        {
            fixed (T* ptr = &payload)
            {
                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                {
                    IntPtr gcHandle = default;

                    new World(World)
                        .Event(Type<T>.Id(World))
                        .Entity(Id)
                        .Ctx(Managed.AllocGcHandle(&gcHandle, ref payload))
                        .Enqueue();
                }
                else
                {
                    new World(World)
                        .Event(Type<T>.Id(World))
                        .Entity(Id)
                        .Ctx(ptr)
                        .Enqueue();
                }
            }
        }

        /// <summary>
        ///     Short for Has(EcsChildOf, entity).
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsChildOf(ulong entity)
        {
            Ecs.Assert(!Macros.IsPair(entity), "Cannot use pairs as an argument.");
            return Has(EcsChildOf, entity);
        }

        /// <summary>
        ///     Short for Has(EcsChildOf, entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsChildOf<T>()
        {
            return IsChildOf(Type<T>.Id(World));
        }

        /// <summary>
        ///     Short for Has(EcsChildOf, entity).
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsChildOf<T>(T value) where T : Enum
        {
            return IsChildOf(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Add an entity to entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity Add(ulong id)
        {
            Ecs.Assert(ecs_id_is_valid(World, id) == Macros.True, nameof(ECS_INVALID_OPERATION));
            ecs_add_id(World, Id, id);
            return ref this;
        }

        /// <summary>
        ///     Add pair to entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity Add(ulong first, ulong second)
        {
            return ref Add(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Add a component to entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Add<T>()
        {
            return ref Add(Type<T>.Id(World));
        }

        /// <summary>
        ///     Add a pair to entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Add<TFirst>(ulong second)
        {
            return ref Add(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Add an enum to entity.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Add<T>(T value) where T : Enum
        {
            return ref Add<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Add a pair to entity.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Add<TFirst, TSecond>()
        {
            return ref Add<TFirst>(Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Add a pair to entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Add<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Add<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Add a pair to entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Add<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref AddSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Add a pair to entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity AddSecond<TSecond>(ulong first)
        {
            return ref Add(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Conditionally adds an entity to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity AddIf(bool cond, ulong id)
        {
            return ref cond ? ref Add(id) : ref Remove(id);
        }

        /// <summary>
        ///     Conditionally adds a pair to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity AddIf(bool cond, ulong first, ulong second)
        {
            if (cond)
                return ref Add(first, second);

            if (second == 0 || ecs_has_id(World, first, EcsExclusive) == 1)
                second = EcsWildcard;

            return ref Remove(first, second);
        }

        /// <summary>
        ///     Conditionally adds a component to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<T>(bool cond)
        {
            return ref cond ? ref Add<T>() : ref Remove<T>();
        }

        /// <summary>
        ///     Conditionally adds an enum to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<T>(bool cond, T value) where T : Enum
        {
            return ref AddIf<T>(cond, Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Conditionally adds a pair to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<TFirst>(bool cond, ulong second)
        {
            return ref AddIf(cond, Type<TFirst>.Id(World), second);
        }

        /// <summary>
        ///     Conditionally adds a pair to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<TFirst, TSecond>(bool cond)
        {
            return ref AddIf<TFirst>(cond, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Conditionally adds a pair to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<TFirst, TSecond>(bool cond, TSecond second) where TSecond : Enum
        {
            return ref AddIf<TFirst>(cond, Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Conditionally adds a pair to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<TFirst, TSecond>(bool cond, TFirst first) where TFirst : Enum
        {
            return ref AddIfSecond<TSecond>(cond, Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Conditionally adds a pair to entity.
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity AddIfSecond<TSecond>(bool cond, ulong first)
        {
            return ref AddIf(cond, first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Shortcut for Add(EcsIsA, entity).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity IsA(ulong id)
        {
            return ref Add(EcsIsA, id);
        }

        /// <summary>
        ///     Shortcut for Add(EcsIsA, entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity IsA<T>()
        {
            return ref Add(EcsIsA, Type<T>.Id(World));
        }

        /// <summary>
        ///     Shortcut for Add(EcsIsA, entity).
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity IsA<T>(T value) where T : Enum
        {
            return ref IsA(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Shortcut for Add(EcsChildOf, entity).
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity ChildOf(ulong second)
        {
            return ref Add(EcsChildOf, second);
        }

        /// <summary>
        ///     Shortcut for Add(EcsChildOf, entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity ChildOf<T>()
        {
            return ref Add(EcsChildOf, Type<T>.Id(World));
        }

        /// <summary>
        ///     Shortcut for Add(EcsChildOf, entity).
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity ChildOf<T>(T value) where T : Enum
        {
            return ref ChildOf(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Shortcut for Add(EcDependsOn, entity).
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity DependsOn(ulong second)
        {
            return ref Add(EcsDependsOn, second);
        }

        /// <summary>
        ///     Shortcut for Add(EcDependsOn, entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity DependsOn<T>()
        {
            return ref DependsOn(Type<T>.Id(World));
        }

        /// <summary>
        ///     Shortcut for Add(EcDependsOn, entity).
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity DependsOn<T>(T value) where T : Enum
        {
            return ref DependsOn(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Shortcut for Add(EcsSlotOf, entity).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity SlotOf(ulong id)
        {
            return ref Add(EcsSlotOf, id);
        }

        /// <summary>
        ///     Shortcut for Add(EcsSlotOf, entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity SlotOf<T>()
        {
            return ref Add(EcsSlotOf, Type<T>.Id(World));
        }

        /// <summary>
        ///     Shortcut for Add(EcsSlotOf, entity).
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity SlotOf<T>(T value) where T : Enum
        {
            return ref SlotOf(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Shortcut for Add(EcsSlotOf, Target(EcsChildOf)).
        /// </summary>
        /// <returns></returns>
        public ref Entity Slot()
        {
            Ecs.Assert(ecs_get_target(World, Id, EcsChildOf, 0) != 0, "Add ChildOf pair before using slot()");
            return ref SlotOf(Target(EcsChildOf));
        }

        /// <summary>
        ///     Remove an entity from entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity Remove(ulong id)
        {
            ecs_remove_id(World, Id, id);
            return ref this;
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity Remove(ulong first, ulong second)
        {
            return ref Remove(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Remove a component from entity. If the provided type argument is an enum,
        ///     this operation will remove any (Enum, *) pair from the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<T>()
        {
            if (typeof(T).IsEnum)
                return ref Remove<T>(EcsWildcard);

            return ref Remove(Type<T>.Id(World));
        }

        /// <summary>
        ///     Remove an enum from entity.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<T>(T value) where T : Enum
        {
            return ref Remove<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<TFirst>(ulong second)
        {
            return ref Remove(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<TFirst, TSecond>()
        {
            return ref Remove(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Remove<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref RemoveSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity RemoveSecond<TSecond>(ulong first)
        {
            return ref Remove(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Mark id for auto-overriding.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity Override(ulong id)
        {
            ecs_add_id(World, Id, ECS_OVERRIDE | id);
            return ref this;
        }

        /// <summary>
        ///     Mark a pair for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity Override(ulong first, ulong second)
        {
            return ref Override(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Mark a component or auto-overriding.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Override<T>()
        {
            return ref Override(Type<T>.Id(World));
        }

        /// <summary>
        ///     Mark a component or auto-overriding.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Override<T>(T value) where T : Enum
        {
            return ref Override<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Mark a pair for auto-overriding.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Override<TFirst>(ulong second)
        {
            return ref Override(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Mark a pair for auto-overriding.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Override<TFirst, TSecond>()
        {
            return ref Override(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Mark a pair for auto-overriding.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Override<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Override<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Mark a pair for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Override<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref OverrideSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Mark a pair for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity OverrideSecond<TSecond>(ulong first)
        {
            return ref Override(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Set component, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<T>(T component)
        {
            return ref SetOverride(ref component);
        }

        /// <summary>
        ///     Set component, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<T>(ref T component)
        {
            return ref Override<T>().Set(ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst>(ulong second, TFirst component)
        {
            return ref SetOverride(second, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst>(ulong second, ref TFirst component)
        {
            return ref Override<TFirst>(second).Set(second, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(TFirst component)
        {
            return ref SetOverride<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(ref TFirst component)
        {
            return ref Override<TFirst, TSecond>().Set<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(TSecond component)
        {
            return ref SetOverride<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(ref TSecond component)
        {
            return ref Override<TFirst, TSecond>().Set<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(TSecond second, TFirst component) where TSecond : Enum
        {
            return ref SetOverride<TFirst, TSecond>(second, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(TSecond second, ref TFirst component) where TSecond : Enum
        {
            ulong secondId = Type<TSecond>.Id(World, second);
            return ref Override<TFirst>(secondId).Set(secondId, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(TFirst first, TSecond component) where TFirst : Enum
        {
            return ref SetOverride<TFirst, TSecond>(first, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<TFirst, TSecond>(TFirst first, ref TSecond component) where TFirst : Enum
        {
            ulong firstId = Type<TFirst>.Id(World, first);
            return ref OverrideSecond<TSecond>(firstId).SetSecond(firstId, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverrideSecond<TSecond>(ulong first, TSecond component)
        {
            return ref SetOverrideSecond(first, ref component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverrideSecond<TSecond>(ulong first, ref TSecond component)
        {
            return ref OverrideSecond<TSecond>(first).SetSecond(first, ref component);
        }

        /// <summary>
        ///     Enable this entity.
        /// </summary>
        /// <returns></returns>
        public ref Entity Enable()
        {
            ecs_enable(World, Id, Macros.True);
            return ref this;
        }

        /// <summary>
        ///     Enable an id for entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity Enable(ulong id)
        {
            ecs_enable_id(World, Id, id, Macros.True);
            return ref this;
        }

        /// <summary>
        ///     Enable pair for entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity Enable(ulong first, ulong second)
        {
            return ref Enable(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Enable component for entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Enable<T>()
        {
            return ref Enable(Type<T>.Id(World));
        }

        /// <summary>
        ///     Enable component for entity.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Enable<T>(T value) where T : Enum
        {
            return ref Enable<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Enable pair for entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Enable<TFirst>(ulong second)
        {
            return ref Enable(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Enable pair for entity.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Enable<TFirst, TSecond>()
        {
            return ref Enable(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Enable pair for entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Enable<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Enable<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Enable pair for entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Enable<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref EnableSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Enable pair for entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity EnableSecond<TSecond>(ulong first)
        {
            return ref Enable(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Disable this entity.
        /// </summary>
        /// <returns></returns>
        public ref Entity Disable()
        {
            ecs_enable(World, Id, Macros.False);
            return ref this;
        }

        /// <summary>
        ///     Disable an id for entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity Disable(ulong id)
        {
            ecs_enable_id(World, Id, id, Macros.False);
            return ref this;
        }

        /// <summary>
        ///     Disable pair for entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref Entity Disable(ulong first, ulong second)
        {
            return ref Disable(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Disable component for entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Disable<T>()
        {
            return ref Disable(Type<T>.Id(World));
        }

        /// <summary>
        ///     Disable component for entity.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Disable<T>(T value) where T : Enum
        {
            return ref Disable<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Disable pair for entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Disable<TFirst>(ulong second)
        {
            return ref Disable(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Disable pair for entity.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Disable<TFirst, TSecond>()
        {
            return ref Disable(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Disable pair for entity.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Disable<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Disable<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Disable pair for entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Disable<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref DisableSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Disable pair for entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity DisableSecond<TSecond>(ulong first)
        {
            return ref Disable(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Set the value of a component.
        /// </summary>
        /// <param name="id">The id of the component to set.</param>
        /// <param name="size">The size of the pointed-to value.</param>
        /// <param name="data">The pointer to the value.</param>
        /// <returns>Reference to self.</returns>
        public ref Entity SetPtr(ulong id, int size, void* data)
        {
            ecs_set_id(World, Id, id, (IntPtr)size, data);
            return ref this;
        }

        /// <summary>
        ///     Set the value of a component.
        /// </summary>
        /// <param name="id">The id of the component to set.</param>
        /// <param name="data">The pointer to the value.</param>
        /// <returns>Reference to self.</returns>
        public ref Entity SetPtr(ulong id, void* data)
        {
            EcsComponent* component = (EcsComponent*)ecs_get_id(World, id, FLECS_IDEcsComponentID_);
            Ecs.Assert(component != null, nameof(ECS_INVALID_PARAMETER));
            ecs_set_id(World, Id, id, (IntPtr)component->size, data);
            return ref this;
        }

        /// <summary>
        ///     Set the value of a pair component.
        /// </summary>
        /// <param name="first">The first id of the pair.</param>
        /// <param name="second">The second id of the pair.</param>
        /// <param name="size">The size of the pointed-to value.</param>
        /// <param name="data">The pointer to the value.</param>
        /// <returns>Reference to self.</returns>
        public ref Entity SetPtr(ulong first, ulong second, int size, void* data)
        {
            ecs_set_id(World, Id, Macros.Pair(first, second), (IntPtr)size, data);
            return ref this;
        }

        /// <summary>
        ///     Sets data for component.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Set<T>(T component)
        {
            return ref Set(ref component);
        }

        /// <summary>
        ///     Sets data for component.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Set<T>(ref T component)
        {
            return ref SetInternal(Type<T>.Id(World), ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst>(ulong second, TFirst component)
        {
            return ref Set(second, ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst>(ulong second, ref TFirst component)
        {
            return ref SetInternal(Macros.Pair<TFirst>(second, World), ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(TSecond component)
        {
            return ref Set<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(ref TSecond component)
        {
            return ref SetInternal(Macros.Pair<TFirst, TSecond>(World), ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(TFirst component)
        {
            return ref Set<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(ref TFirst component)
        {
            return ref SetInternal(Macros.Pair<TFirst, TSecond>(World), ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(TSecond second, TFirst component) where TSecond : Enum
        {
            return ref Set<TFirst, TSecond>(second, ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(TSecond second, ref TFirst component) where TSecond : Enum
        {
            return ref Set(Type<TSecond>.Id(World, second), ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(TFirst first, TSecond component) where TFirst : Enum
        {
            return ref Set<TFirst, TSecond>(first, ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecond>(TFirst first, ref TSecond component) where TFirst : Enum
        {
            return ref SetSecond(Type<TFirst>.Id(World, first), ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetSecond<TSecond>(ulong first, TSecond component)
        {
            return ref SetSecond(first, ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="component"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetSecond<TSecond>(ulong first, ref TSecond component)
        {
            return ref SetInternal(Macros.PairSecond<TSecond>(first, World), ref component);
        }

        /// <summary>
        ///     Entities created in function will have the current entity.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public ref Entity With(Action func)
        {
            ulong prev = ecs_set_with(World, Id);
            func();
            ecs_set_with(World, prev);
            return ref this;
        }

        /// <summary>
        ///     Entities created in function will have (First, this).
        /// </summary>
        /// <param name="first"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public ref Entity With(ulong first, Action func)
        {
            ulong prev = ecs_set_with(World, Macros.Pair(first, Id));
            func();
            ecs_set_with(World, prev);
            return ref this;
        }

        /// <summary>
        ///     Entities created in function will have (TFirst, this).
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity With<TFirst>(Action func)
        {
            return ref With(Type<TFirst>.Id(World), func);
        }

        /// <summary>
        ///     The function will be ran with the scope set to the current entity.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public ref Entity Scope(Action func)
        {
            ulong prev = ecs_set_scope(World, Id);
            func();
            ecs_set_scope(World, prev);
            return ref this;
        }

        /// <summary>
        ///     Set the entity name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref Entity SetName(string name)
        {
            using NativeString nativeName = (NativeString)name;
            ecs_set_name(World, Id, nativeName);
            return ref this;
        }

        /// <summary>
        ///     Set entity alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public ref Entity SetAlias(string alias)
        {
            using NativeString nativeAlias = (NativeString)alias;
            ecs_set_alias(World, Id, nativeAlias);
            return ref this;
        }

        /// <summary>
        ///     Set doc name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref Entity SetDocName(string name)
        {
            using NativeString nativeName = (NativeString)name;
            ecs_doc_set_name(World, Id, nativeName);
            return ref this;
        }

        /// <summary>
        ///     Set doc brief.
        /// </summary>
        /// <param name="brief"></param>
        /// <returns></returns>
        public ref Entity SetDocBrief(string brief)
        {
            using NativeString nativeBrief = (NativeString)brief;
            ecs_doc_set_brief(World, Id, nativeBrief);
            return ref this;
        }

        /// <summary>
        ///     Set doc detailed description.
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public ref Entity SetDocDetail(string detail)
        {
            using NativeString nativeDetail = (NativeString)detail;
            ecs_doc_set_detail(World, Id, nativeDetail);
            return ref this;
        }

        /// <summary>
        ///     Set doc link.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public ref Entity SetDocLink(string link)
        {
            using NativeString nativeLink = (NativeString)link;
            ecs_doc_set_link(World, Id, nativeLink);
            return ref this;
        }

        /// <summary>
        ///     Set doc color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public ref Entity SetDocColor(string color)
        {
            using NativeString nativeColor = (NativeString)color;
            ecs_doc_set_color(World, Id, nativeColor);
            return ref this;
        }

        /// <summary>
        ///     Make entity a unit.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="prefix"></param>
        /// <param name="base"></param>
        /// <param name="over"></param>
        /// <param name="factor"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public ref Entity Unit(
            string symbol,
            ulong prefix = 0,
            ulong @base = 0,
            ulong over = 0,
            int factor = 0,
            int power = 0)
        {
            using NativeString nativeSymbol = (NativeString)symbol;

            ecs_unit_desc_t desc = default;
            desc.symbol = nativeSymbol;
            desc.entity = Id;
            desc.@base = @base;
            desc.over = over;
            desc.prefix = prefix;
            desc.translation.factor = factor;
            desc.translation.power = power;
            ecs_unit_init(World, &desc);

            return ref this;
        }

        /// <summary>
        ///     Make entity a derived unit.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="base"></param>
        /// <param name="over"></param>
        /// <param name="factor"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public ref Entity Unit(
            ulong prefix = 0,
            ulong @base = 0,
            ulong over = 0,
            int factor = 0,
            int power = 0)
        {
            ecs_unit_desc_t desc = default;
            desc.entity = Id;
            desc.@base = @base;
            desc.over = over;
            desc.prefix = prefix;
            desc.translation.factor = factor;
            desc.translation.power = power;
            ecs_unit_init(World, &desc);

            return ref this;
        }

        /// <summary>
        ///     Make unit a prefix.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="factor"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public ref Entity UnitPrefix(string symbol, int factor = 0, int power = 0)
        {
            using NativeString nativeSymbol = (NativeString)symbol;

            ecs_unit_prefix_desc_t desc = default;
            desc.entity = Id;
            desc.symbol = nativeSymbol;
            desc.translation.factor = factor;
            desc.translation.power = power;
            ecs_unit_prefix_init(World, &desc);

            return ref this;
        }

        /// <summary>
        ///     Add quantity to unit.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public ref Entity Quantity(ulong quantity)
        {
            ecs_add_id(World, Id, Macros.Pair(EcsQuantity, quantity));
            return ref this;
        }

        /// <summary>
        ///     Make entity a quantity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Quantity<T>()
        {
            return ref Quantity(Type<T>.Id(World));
        }

        /// <summary>
        ///     Make entity a quantity.
        /// </summary>
        /// <returns></returns>
        public ref Entity Quantity()
        {
            ecs_add_id(World, Id, EcsQuantity);
            return ref this;
        }

        /// <summary>
        ///     Set component from JSON.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public ref Entity SetJson(ulong e, string json, ecs_from_json_desc_t* desc = null)
        {
            ulong type = ecs_get_typeid(World, e);

            if (type == 0)
            {
                Ecs.Error("Id is not a type");
                return ref this;
            }

            void* ptr = ecs_ensure_id(World, Id, e);
            Ecs.Assert(ptr != null, nameof(ECS_INTERNAL_ERROR));

            using NativeString nativeJson = (NativeString)json;

            ecs_ptr_from_json(World, type, ptr, nativeJson, desc);
            ecs_modified_id(World, Id, e);

            return ref this;
        }

        /// <summary>
        ///     Set pair from JSON.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public ref Entity SetJson(ulong first, ulong second, string json, ecs_from_json_desc_t* desc = null)
        {
            return ref SetJson(Macros.Pair(first, second), json, desc);
        }

        /// <summary>
        ///     Set component from JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity SetJson<T>(string json, ecs_from_json_desc_t* desc = null)
        {
            return ref SetJson(Type<T>.Id(World), json, desc);
        }

        /// <summary>
        ///     Set pair from JSON.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref Entity SetJson<TFirst>(ulong second, string json, ecs_from_json_desc_t* desc = null)
        {
            return ref SetJson(Macros.Pair<TFirst>(second, World), json, desc);
        }

        /// <summary>
        ///     Set pair from JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetJson<TFirst, TSecond>(string json, ecs_from_json_desc_t* desc = null)
        {
            return ref SetJson(Macros.Pair<TFirst, TSecond>(World), json, desc);
        }

        /// <summary>
        ///     Set pair from JSON.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetJson<TFirst, TSecond>(TSecond second, string json, ecs_from_json_desc_t* desc = null)
            where TSecond : Enum
        {
            return ref SetJson<TFirst>(Type<TSecond>.Id(World, second), json, desc);
        }

        /// <summary>
        ///     Set pair from JSON.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetJson<TFirst, TSecond>(TFirst first, string json, ecs_from_json_desc_t* desc = null)
            where TFirst : Enum
        {
            return ref SetJsonSecond<TSecond>(Type<TFirst>.Id(World, first), json, desc);
        }

        /// <summary>
        ///     Set pair from JSON.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetJsonSecond<TSecond>(ulong first, string json, ecs_from_json_desc_t* desc = null)
        {
            return ref SetJson(first, Type<TSecond>.Id(World), json, desc);
        }

        /// <summary>
        ///     Observe an event on this entity.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ref Entity Observe(ulong eventId, Action callback)
        {
            return ref ObserveInternal(eventId, callback, BindingContext.ActionCallbackPointer);
        }

        /// <summary>
        ///     Observe an event on this entity.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ref Entity Observe(ulong eventId, Ecs.EachEntityCallback callback)
        {
            return ref ObserveInternal(eventId, callback, BindingContext.ObserveCallbackPointer);
        }

        /// <summary>
        ///     Observe an event on this entity.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Observe<T>(Action callback)
        {
            return ref Observe(Type<T>.Id(World), callback);
        }

        /// <summary>
        ///     Observe an event on this entity.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Observe<T>(Ecs.EachEntityCallback callback)
        {
            return ref Observe(Type<T>.Id(World), callback);
        }

        /// <summary>
        ///     Observe an event on this entity.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Observe<T>(Ecs.EachCallback<T> callback)
        {
            return ref ObserveInternal(Type<T>.Id(World), callback, BindingContext<T>.EntityObserverEachPointer);
        }

        /// <summary>
        ///     Observe an event on this entity.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Observe<T>(Ecs.EachEntityCallback<T> callback)
        {
            return ref ObserveInternal(Type<T>.Id(World), callback, BindingContext<T>.EntityObserverEachEntityPointer);
        }

        /// <summary>
        ///     Get mutable component value (untyped).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void* EnsurePtr(ulong id)
        {
            return ecs_ensure_id(World, Id, id);
        }

        /// <summary>
        ///     Get mutable pointer for a pair (untyped).
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public void* EnsurePtr(ulong first, ulong second)
        {
            return EnsurePtr(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Get mutable component value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* EnsurePtr<T>() where T : unmanaged
        {
            ulong componentId = Type<T>.Id(World);

            if (!typeof(T).IsEnum)
            {
                Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
                return (T*)ecs_ensure_id(World, Id, componentId);
            }

            ulong target = ecs_get_target(World, Id, componentId, 0);

            if (target == 0)
                return  (T*)ecs_ensure_id(World, Id, componentId);

            T* ptr = (T*)ecs_ensure_id(World, target, componentId);
            Ecs.Assert(ptr != null, "Missing enum constant value");
            return ptr;
        }

        /// <summary>
        ///     Get mutable pointer for the first element of a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* EnsurePtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)EnsurePtr(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* EnsurePtr<TFirst, TSecond>(TSecond second)
            where TFirst : unmanaged
            where TSecond : Enum
        {
            return EnsurePtr<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* EnsurePtr<TFirst, TSecond>(TFirst first)
            where TFirst : Enum
            where TSecond : unmanaged
        {
            return EnsureSecondPtr<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* EnsureFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)EnsurePtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* EnsureSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)EnsurePtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* EnsureSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)EnsurePtr(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Get mutable managed reference for component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref T Ensure<T>()
        {
            ulong componentId = Type<T>.Id(World);

            if (!typeof(T).IsEnum)
            {
                Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
                return ref Managed.GetTypeRef<T>(ecs_ensure_id(World, Id, componentId));
            }

            ulong target = ecs_get_target(World, Id, componentId, 0);

            if (target == 0)
                return ref Managed.GetTypeRef<T>(ecs_ensure_id(World, Id, componentId));

            void* ptr = ecs_ensure_id(World, target, componentId);
            Ecs.Assert(ptr != null, "Missing enum constant value");
            return ref Managed.GetTypeRef<T>(ptr);
        }

        /// <summary>
        ///     Get mutable managed reference for the first element of a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref TFirst Ensure<TFirst>(ulong second)
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(EnsurePtr(Macros.Pair<TFirst>(second, World)));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TFirst Ensure<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Ensure<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond Ensure<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref EnsureSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TFirst EnsureFirst<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(EnsurePtr(Macros.Pair<TFirst, TSecond>(World)));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond EnsureSecond<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(EnsurePtr(Macros.Pair<TFirst, TSecond>(World)));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond EnsureSecond<TSecond>(ulong first)
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(EnsurePtr(Macros.PairSecond<TSecond>(first, World)));
        }

        /// <summary>
        ///     Signal that component was modified.
        /// </summary>
        /// <param name="id"></param>
        public void Modified(ulong id)
        {
            ecs_modified_id(World, Id, id);
        }

        /// <summary>
        ///     Signal that pair was modified.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public void Modified(ulong first, ulong second)
        {
            Modified(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Signal that component was modified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Modified<T>()
        {
            Modified(Type<T>.Id(World));
        }

        /// <summary>
        ///     Signal that component was modified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Modified<T>(T value) where T : Enum
        {
            Modified<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Signal that a pair was modified.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Modified<TFirst>(ulong second)
        {
            Modified(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Signal that a pair was modified.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Modified<TFirst, TSecond>()
        {
            Modified(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Signal that a pair was modified.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Modified<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            Modified<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Signal that a pair was modified.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Modified<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            ModifiedSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Signal that a pair was modified.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        public void ModifiedSecond<TSecond>(ulong first)
        {
            Modified(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Get reference to component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Ref<T> GetRef<T>()
        {
            return new Ref<T>(World, Id);
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Ref<TFirst> GetRef<TFirst>(ulong second)
        {
            return new Ref<TFirst>(World, Id, Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TFirst> GetRef<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return GetRef<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TSecond> GetRef<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return GetRefSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TFirst> GetRefFirst<TFirst, TSecond>()
        {
            return new Ref<TFirst>(World, Id, Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TSecond> GetRefSecond<TFirst, TSecond>()
        {
            return new Ref<TSecond>(World, Id, Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TSecond> GetRefSecond<TSecond>(ulong first)
        {
            return new Ref<TSecond>(World, Id, Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Clear an entity.
        /// </summary>
        public void Clear()
        {
            ecs_clear(World, Id);
        }

        /// <summary>
        ///     Delete an entity.
        /// </summary>
        public void Destruct()
        {
            ecs_delete(World, Id);
        }

        /// <summary>
        ///     Deserialize entity to JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string FromJson(string json)
        {
            using NativeString nativeJson = (NativeString)json;
            return NativeString.GetString(ecs_entity_from_json(World, Id, nativeJson, null));
        }

        private ref Entity SetInternal<T>(ulong id, ref T component)
        {
            Ecs.Assert(Type<T>.Size != 0,
                "Empty structs can't be used as components. Use .Add() to add them as tags instead.");

            bool isRef = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            int size = isRef ? sizeof(IntPtr) : sizeof(T);

            fixed (void* data = &component)
            {
                IntPtr ptr = default;
                ecs_set_id(World, Id, id, (IntPtr)size, isRef ? Managed.AllocGcHandle(&ptr, ref component) : data);
                return ref this;
            }
        }

        private ref Entity ObserveInternal<T>(ulong eventId, T callback, IntPtr bindingContextCallback)
            where T : Delegate
        {
            BindingContext.RunIterContext* observerContext = Memory.AllocZeroed<BindingContext.RunIterContext>(1);
            BindingContext.SetCallback(ref observerContext->Iterator, callback, false);

            ecs_observer_desc_t desc = default;
            desc.events[0] = eventId;
            desc.query.terms[0].id = EcsAny;
            desc.query.terms[0].src.id = Id;
            desc.callback = bindingContextCallback;
            desc.binding_ctx = observerContext;
            desc.binding_ctx_free = BindingContext.RunIterContextFreePointer;

            ulong observer = ecs_observer_init(World, &desc);
            ecs_add_id(World, observer, Macros.Pair(EcsChildOf, Id));

            return ref this;
        }

        /// <summary>
        ///     Converts an <see cref="Entity"/> instance to its integer id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static implicit operator ulong(Entity entity)
        {
            return ToUInt64(entity);
        }

        /// <summary>
        ///     Converts an <see cref="Entity"/> instance to its id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static implicit operator Id(Entity entity)
        {
            return ToId(entity);
        }

        /// <summary>
        ///     Converts an <see cref="Entity"/> instance to its integer id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ulong ToUInt64(Entity entity)
        {
            return entity.Id.Value;
        }

        /// <summary>
        ///     Converts an <see cref="Entity"/> instance to its id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Id ToId(Entity entity)
        {
            return entity.Id;
        }

        /// <summary>
        ///     Checks if two <see cref="Entity"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Entity other)
        {
            return Id.Value == other.Id.Value;
        }

        /// <summary>
        ///     Checks if two <see cref="Entity"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Entity entity && Equals(entity);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Entity"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.Value.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Entity"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity left, Entity right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Entity"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Returns the entity's name if it has one, otherwise return its id.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Id.ToString();
        }
    }

    // Flecs.NET Extensions
    public unsafe partial struct Entity
    {
        /// <summary>
        ///     Lookup an entity from a path.
        /// </summary>
        /// <param name="path">The path to resolve.</param>
        /// <param name="entity">The entity if found, else 0.</param>
        /// <returns>True if the entity was found, else false.</returns>
        public bool TryLookup(string path, out Entity entity)
        {
            return TryLookup(path, false, out entity);
        }

        /// <summary>
        ///     Lookup an entity from a path.
        /// </summary>
        /// <param name="path">The path to resolve.</param>
        /// <param name="recursive">Recursively traverse up the tree until entity is found.</param>
        /// <param name="entity">The entity if found, else 0.</param>
        /// <returns>True if the entity was found, else false.</returns>
        public bool TryLookup(string path, bool recursive, out Entity entity)
        {
            return (entity = Lookup(path, recursive)) != 0;
        }

        /// <summary>
        ///     Lookup an entity from a path.
        /// </summary>
        /// <param name="path">The path to resolve.</param>
        /// <param name="entity">The entity if found, else 0.</param>
        /// <returns>True if the entity was found, else false.</returns>
        public bool TryLookup(string path, out ulong entity)
        {
            return TryLookup(path, false, out entity);
        }

        /// <summary>
        ///     Lookup an entity from a path.
        /// </summary>
        /// <param name="path">The path to resolve.</param>
        /// <param name="recursive">Recursively traverse up the tree until entity is found.</param>
        /// <param name="entity">The entity if found, else 0.</param>
        /// <returns>True if the entity was found, else false.</returns>
        public bool TryLookup(string path, bool recursive, out ulong entity)
        {
            return (entity = Lookup(path, recursive)) != 0;
        }
    }
}
