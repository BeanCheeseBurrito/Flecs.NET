using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for working with entities.
    /// </summary>
    public unsafe struct Entity : IEquatable<Entity>
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
            return new Entity(world);
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
            _id = new Id(world, ecs_new_w_id(world, 0));
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
            desc.root_sep = BindingContext.DefaultRootSeparator;

            _id = new Id(world, ecs_entity_init(world, &desc));
        }

        /// <summary>
        ///     Returns the C# world for this entity.
        /// </summary>
        /// <returns></returns>
        public World CsWorld()
        {
            return new World(World, false);
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
        public string Path(string sep = ".", string initSep = "")
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
        public string PathFrom(ulong parent, string sep = ".", string initSep = "")
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
        public string PathFrom<TParent>(string sep = ".", string initSep = "")
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
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool EnabledSecond<TSecond>(ulong first)
        {
            return Enabled(Macros.PairSecond<TSecond>(first, World));
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
            ecs_record_t *r = ecs_record_find(World, Id);
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
            {
                ulong id = ids[i];
                Id entity = new Id(World, id);
                func(entity);

                if (Macros.PairFirst(id) != EcsUnion)
                    continue;

                entity = new Id(
                    World,
                    Macros.PairSecond(id),
                    ecs_get_target(World, Id, Macros.PairSecond(id), 0)
                );

                func(entity);
            }
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
        ///     Iterate children for entity.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="callback"></param>
        public void Children(ulong relation, Ecs.EachEntityCallback callback)
        {
            if (Id == EcsWildcard || Id == EcsAny)
                return;

            Span<ecs_term_t> terms = stackalloc ecs_term_t[2];

            ecs_filter_t filter = ECS_FILTER_INIT;
            filter.terms = (ecs_term_t*)Unsafe.AsPointer(ref terms[0]);
            filter.term_count = 2;

            ecs_filter_desc_t desc = default;
            desc.terms[0].first.id = relation;
            desc.terms[0].second.id = Id;
            desc.terms[0].second.flags = EcsIsEntity;
            desc.terms[1].id = EcsPrefab;
            desc.terms[1].oper = EcsOptional;
            desc.storage = &filter;

            if (ecs_filter_init(World, &desc) == null)
                return;

            ecs_iter_t it = ecs_filter_iter(World, &filter);
            while (ecs_filter_next_instanced(&it) == 1)
                Invoker.Each(&it, callback);
            ecs_filter_fini(&filter);
        }

        /// <summary>
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
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (T*)ecs_get_id(World, Id, componentId);
        }

        /// <summary>
        ///     Get pointer to component value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public TEnum* GetEnumPtr<TEnum>() where TEnum : unmanaged, Enum
        {
            ulong enumTypeId = Type<TEnum>.Id(World);
            ulong target = ecs_get_target(World, Id, enumTypeId, 0);

            if (target == 0)
                return (TEnum*)ecs_get_id(World, Id, enumTypeId);

            void* ptr = ecs_get_id(World, enumTypeId, target);
            Assert.True(ptr != null, "Missing enum constant value");
            return (TEnum*)ptr;
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_id(World, Id, pair);
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst, TSecondEnum>(TSecondEnum secondEnum)
            where TFirst : unmanaged
            where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, World);
            ulong pair = Macros.Pair<TFirst>(enumId, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_id(World, Id, pair);
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_id(World, Id, pair);
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_id(World, Id, pair);
        }

        /// <summary>
        ///     Get pointer to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_id(World, Id, pair);
        }

        /// <summary>
        ///     Get managed reference to component value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref readonly T Get<T>()
        {
            ulong id = Type<T>.Id(World);
            Assert.True(!typeof(T).IsEnum, "Cannot use .Get<T>() on an enum type. Use .GetEnum<T>() instead.");
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<T>(ecs_get_id(World, Id, id));
        }

        /// <summary>
        ///     Get managed reference to component value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref readonly TEnum GetEnum<TEnum>() where TEnum : Enum
        {
            ulong r = Type<TEnum>.Id(World);
            ulong c = ecs_get_target(World, Id, r, 0);

            if (c == 0)
                return ref Managed.GetTypeRef<TEnum>(ecs_get_id(World, Id, r));

            void* ptr = ecs_get_id(World, c, r);
            Assert.True(ptr != null, "Missing enum constant value");
            return ref Managed.GetTypeRef<TEnum>(ptr);
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst Get<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TFirst>(component);
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst Get<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            ulong pair = Macros.Pair<TFirst>(enumId, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TFirst>(component);
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst GetFirst<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TFirst>(component);
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TSecond GetSecond<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TSecond>(component);
        }

        /// <summary>
        ///     Get managed reference to component value from pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TSecond GetSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TSecond>(component);
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
        public int Depth<T>() where T : unmanaged
        {
            return Depth(Type<T>.Id(World));
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
        ///     Lookup an entity by name.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Entity Lookup(string path)
        {
            Assert.True(Id != 0, "invalid lookup from null handle");
            using NativeString nativePath = (NativeString)path;
            ulong id = ecs_lookup_path_w_sep(World, Id, nativePath,
                BindingContext.DefaultSeparator, BindingContext.DefaultRootSeparator, Macros.False);
            return new Entity(World, id);
        }

        /// <summary>
        ///     Check if entity has the provided entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Has(ulong id)
        {
            return ecs_has_id(World, Id, id) == 1;
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
            bool result = ecs_has_id(World, Id, typeId) == 1;

            if (result)
                return result;

            return typeof(T).IsEnum && Has(typeId, EcsWildcard);
        }

        /// <summary>
        ///     Check if entity has the provided enum constant.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public bool Has<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return Has<TEnum>(enumId);
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
        /// <param name="enumMember"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return Has<TFirst>(enumId);
        }

        /// <summary>
        ///     Check if entity has the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool HasSecond<TSecond>(ulong first)
        {
            return Has(Macros.PairSecond<TSecond>(first, World));
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
        ///     Clones the entity.
        /// </summary>
        /// <param name="cloneValue"></param>
        /// <param name="dstId"></param>
        /// <returns></returns>
        public Entity Clone(bool cloneValue = true, ulong dstId = 0)
        {
            if (dstId == 0)
                dstId = ecs_new_id(World);

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
            Assert.True(!stage.IsReadOnly(), "Cannot use readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(stage);
        }

        /// <summary>
        ///     Return mutable entity handle for current iter.
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public Entity Mut(ref Iter it)
        {
            Assert.True(!it.World().IsReadOnly(),
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
            Assert.True(!entity.CsWorld().IsReadOnly(),
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
        public string ToJson(ecs_entity_to_json_desc_t* desc = null)
        {
            return NativeString.GetStringAndFree(ecs_entity_to_json(World, Id, desc));
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
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public TEnum ToConstant<TEnum>() where TEnum : unmanaged, Enum
        {
            TEnum* ptr = GetEnumPtr<TEnum>();
            Assert.True(ptr != null, "Entity is not a constant");
            return *ptr;
        }

        /// <summary>
        ///     Short for Has(EcsChildOf, entity).
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsChildOf(ulong entity)
        {
            Assert.True(!Macros.IsPair(entity), "Cannot use pairs as an argument.");
            return Has(EcsChildOf, entity);
        }

        /// <summary>
        ///    Short for Has(EcsChildOf, entity).
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
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public bool IsChildOf<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return IsChildOf(enumId);
        }

        /// <summary>
        ///     Add an entity to entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref Entity Add(ulong id)
        {
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
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref Entity Add<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return ref Add<TEnum>(enumId);
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
        /// <param name="enumMember"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref Entity Add<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Add<TFirst>(enumId);
        }

        /// <summary>
        ///     Add a pair to entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity AddSecond<TSecond>(ulong first)
        {
            return ref Add(Macros.PairSecond<TSecond>(first, World));
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
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref Entity AddIf<TEnum>(bool cond, TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return ref AddIf<TEnum>(cond, enumId);
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
            return ref AddIf(cond, Macros.Pair<TFirst, TSecond>(World));
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
            return ref Add(EcsDependsOn, Type<T>.Id(World));
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
        ///     Shortcut for Add(EcsSlotOf, Target(EcsChildOf)).
        /// </summary>
        /// <returns></returns>
        public ref Entity Slot()
        {
            Assert.True(ecs_get_target(World, Id, EcsChildOf, 0) != 0, "Add ChildOf pair before using slot()");
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
        ///     Remove a component from entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<T>()
        {
            return ref Remove(Type<T>.Id(World));
        }

        /// <summary>
        ///     Remove an enum from entity.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Remove<TEnum>(EcsWildcard);
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
        /// <param name="enumMember"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref Entity Remove<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Remove<TFirst>(enumId);
        }

        /// <summary>
        ///     Remove a pair from entity.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity RemoveSecond<TSecond>(ulong first)
        {
            return ref Remove(Macros.PairSecond<TSecond>(first, World));
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
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity OverrideSecond<TSecond>(ulong first)
        {
            return ref Override(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Set component, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverride<T>(T component)
        {
            return ref Override<T>().Set(component);
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
            return ref Override<TFirst>(second).Set(second, component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverrideFirst<TFirst, TSecond>(TFirst component)
        {
            return ref Override<TFirst, TSecond>().SetFirst<TFirst, TSecond>(component);
        }

        /// <summary>
        ///     Set pair, mark component for auto-overriding.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetOverrideSecond<TFirst, TSecond>(TSecond component)
        {
            return ref Override<TFirst, TSecond>().SetSecond<TFirst, TSecond>(component);
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
            return ref OverrideSecond<TSecond>(first).SetSecond(first, component);
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
        ///     Sets data for id.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ref Entity SetPtr(ulong componentId, int size, void* data)
        {
            ecs_set_id(World, Id, componentId, (IntPtr)size, data);
            return ref this;
        }

        /// <summary>
        ///     Sets data for id.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ref Entity SetPtr(ulong componentId, ulong size, void* data)
        {
            ecs_set_id(World, Id, componentId, (IntPtr)size, data);
            return ref this;
        }

        /// <summary>
        ///     Sets data for id.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ref Entity SetPtr(ulong componentId, void* data)
        {
            EcsComponent* ecsComponent = (EcsComponent*)ecs_get_id(World, componentId, FLECS_IDEcsComponentID_);
            Assert.True(ecsComponent != null, nameof(ECS_INVALID_PARAMETER));
            ecs_set_id(World, Id, componentId, (IntPtr)ecsComponent->size, data);
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
            return ref SetInternal(Type<T>.Id(World), ref component);
        }

        /// <summary>
        ///     Sets data for component.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref Entity Set<T>(ref T component)
        {
            return ref Set(component);
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
        /// <param name="enumMember"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecondEnum>(TSecondEnum enumMember, ref TFirst component)
            where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Set(enumId, ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref Entity Set<TFirst, TSecondEnum>(TSecondEnum enumMember, TFirst component) where TSecondEnum : Enum
        {
            return ref Set(enumMember, ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetFirst<TFirst, TSecond>(TFirst component)
        {
            return ref SetFirst<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetFirst<TFirst, TSecond>(ref TFirst component)
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
        public ref Entity SetSecond<TFirst, TSecond>(TSecond component)
        {
            return ref SetSecond<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Sets data for pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref Entity SetSecond<TFirst, TSecond>(ref TSecond component)
        {
            return ref SetInternal(Macros.Pair<TFirst, TSecond>(World), ref component);
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
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (T*)ecs_get_mut_id(World, Id, componentId);
        }

        /// <summary>
        ///     Get a mutable enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public TEnum* GetMutEnumPtr<TEnum>() where TEnum : unmanaged, Enum
        {
            ulong enumTypeId = Type<TEnum>.Id(World);
            ulong target = ecs_get_target(World, Id, enumTypeId, 0);

            if (target == 0)
                return (TEnum*)ecs_get_id(World, Id, enumTypeId);

            void* ptr = ecs_get_mut_id(World, enumTypeId, target);
            Assert.True(ptr != null, "Missing enum constant value");
            return (TEnum*)ptr;
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_mut_id(World, Id, pair);
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutPtr<TFirst, TSecondEnum>(TSecondEnum secondEnum)
            where TFirst : unmanaged
            where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, World);
            ulong pair = Macros.Pair<TFirst>(enumId, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_mut_id(World, Id, pair);
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_mut_id(World, Id, pair);
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetMutSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_mut_id(World, Id, pair);
        }

        /// <summary>
        ///     Get mutable pointer for a pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetMutSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_mut_id(World, Id, pair);
        }

        /// <summary>
        ///     Get mutable managed reference for component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref T GetMut<T>()
        {
            ulong compId = Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<T>(ecs_get_mut_id(World, Id, compId));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMut<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(ecs_get_mut_id(World, Id, pair));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMutFirst<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(ecs_get_mut_id(World, Id, pair));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond GetMutSecond<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(ecs_get_mut_id(World, Id, pair));
        }

        /// <summary>
        ///     Get mutable managed reference for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond GetMutSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(ecs_get_mut_id(World, Id, pair));
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
        ///     Signal that first element of pair was modified.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Modified<TFirst>(ulong second)
        {
            Modified(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Signal that first element of pair was modified.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Modified<TFirst, TSecond>()
        {
            Modified(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Signal that first element of pair was modified.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        public void ModifiedSecond<TSecond>(ulong first)
        {
            Modified(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Get reference to component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Ref<T> GetRef<T>()
        {
            Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
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
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TFirst>(World, Id, pair);
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TFirst> GetRefFirst<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TFirst>(World, Id, pair);
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TSecond> GetRefSecond<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TSecond>(World, Id, pair);
        }

        /// <summary>
        ///     Get ref to pair component.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TSecond> GetRefSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TSecond>(World, Id, pair);
        }

        /// <summary>
        ///     Recursively flatten relationship.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="desc"></param>
        public void Flatten(ulong relation, ecs_flatten_desc_t* desc = null)
        {
            ecs_flatten(World, Macros.Pair(relation, Id), desc);
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
            Assert.True(Type<T>.GetSize() != 0, "Zero-sized types can't be used as components. Use .Add() to add them as tags instead.");

            bool isRef = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            int size = isRef ? sizeof(IntPtr) : sizeof(T);

            fixed (void* data = &component)
            {
                IntPtr ptr = default;
                ecs_set_id(World, Id, id, (IntPtr)size, isRef ? Managed.AllocGcHandle(&ptr, ref component) : data);
                return ref this;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static implicit operator ulong(Entity entity)
        {
            return ToUInt64(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ulong ToUInt64(Entity entity)
        {
            return entity.Id.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Entity other)
        {
            return Id.Value == other.Id.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Entity entity && Equals(entity);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.Value.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity left, Entity right)
        {
            return left.Equals(right);
        }

        /// <summary>
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
            string name = Name();
            return string.IsNullOrEmpty(name) ? Id.Value.ToString(CultureInfo.InvariantCulture) : name;
        }
    }
}
