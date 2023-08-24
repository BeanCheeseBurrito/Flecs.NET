using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Entity : IEquatable<Entity>
    {
        private Id _id;

        /// <summary>
        /// Reference to id.
        /// </summary>
        public ref Id Id => ref _id;

        /// <summary>
        /// Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _id.World;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static Entity Null()
        {
            return default;
        }

        public static Entity Null(ecs_world_t* world)
        {
            return new Entity(world);
        }

        public Entity(ulong id)
        {
            _id = id;
        }

        public Entity(ecs_world_t* world)
        {
            _id = new Id(world, ecs_new_w_id(world, 0));
        }

        public Entity(ecs_world_t* world, ulong id)
        {
            _id = new Id(world, id);
        }

        public Entity(ecs_world_t* world, string name)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSeparator = (NativeString)"::";

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.sep = nativeSeparator;
            desc.root_sep = nativeSeparator;

            _id = new Id(world, ecs_entity_init(world, &desc));
        }

        public World CsWorld()
        {
            return new World(World, false);
        }

        public bool IsValid()
        {
            return World != null && ecs_is_valid(World, Id) == 1;
        }

        public bool IsAlive()
        {
            return World != null && ecs_is_alive(World, Id) == 1;
        }

        public string Name()
        {
            return NativeString.GetString(ecs_get_name(World, Id));
        }

        public string Symbol()
        {
            return NativeString.GetString(ecs_get_symbol(World, Id));
        }

        public string Path(string sep = "::", string initSep = "::")
        {
            return PathFrom(0, sep, initSep);
        }

        public string PathFrom(ulong parent, string sep = "::", string initSep = "::")
        {
            using NativeString nativeSep = (NativeString)sep;
            using NativeString nativeInitSep = (NativeString)initSep;

            return NativeString.GetStringAndFree(ecs_get_path_w_sep(World, parent, Id, nativeSep, nativeInitSep));
        }

        public string PathFrom<TParent>(string sep = "::", string initSep = "::")
        {
            return PathFrom(Type<TParent>.Id(World), sep, initSep);
        }

        public bool Enabled()
        {
            return ecs_has_id(World, Id, EcsDisabled) == 0;
        }

        public bool Enabled(ulong id)
        {
            return ecs_is_enabled_id(World, Id, id) == 1;
        }

        public bool Enabled(ulong first, ulong second)
        {
            return Enabled(Macros.Pair(first, second));
        }

        public bool Enabled<T>()
        {
            return Enabled(Type<T>.Id(World));
        }

        public bool Enabled<TFirst>(ulong second)
        {
            return Enabled(Macros.Pair<TFirst>(second, World));
        }

        public bool Enabled<TFirst, TSecond>()
        {
            return Enabled(Macros.Pair<TFirst, TSecond>(World));
        }

        public Types Types()
        {
            return new Types(World, ecs_get_type(World, Id));
        }

        public Table Table()
        {
            return new Table(World, ecs_get_table(World, Id));
        }

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

        public void Each(ulong relation, Ecs.EachEntityCallback func)
        {
            Each(relation, EcsWildcard, (Id id) => { func(id.Second()); });
        }

        public void Each<TFirst>(Ecs.EachEntityCallback func)
        {
            Each(Type<TFirst>.Id(World), func);
        }

        public void Children(ulong relation, Ecs.EachEntityCallback callback)
        {
            if (Id == EcsWildcard || Id == EcsAny)
                return;

            Span<ecs_term_t> terms = stackalloc ecs_term_t[2];
            ecs_filter_t filter = ECS_FILTER_INIT;
            filter.terms = (ecs_term_t*)&terms;
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
            Invoker.EachEntity(callback, ecs_filter_next_instanced, &it);
            ecs_filter_fini(&filter);
        }

        public void Children<TRel>(Ecs.EachEntityCallback callback)
        {
            Children(Type<TRel>.Id(World), callback);
        }

        public void Children(Ecs.EachEntityCallback callback)
        {
            Children(EcsChildOf, callback);
        }

        public readonly void* GetPtr(ulong compId)
        {
            return ecs_get_id(World, Id, compId);
        }

        public readonly void* GetPtr(ulong first, ulong second)
        {
            return GetPtr(Macros.Pair(first, second));
        }

        public readonly T* GetPtr<T>() where T : unmanaged
        {
            ulong componentId = Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (T*)ecs_get_id(World, Id, componentId);
        }

        public readonly TEnum* GetEnumPtr<TEnum>() where TEnum : unmanaged, Enum
        {
            ulong enumTypeId = Type<TEnum>.Id(World);
            ulong target = ecs_get_target(World, Id, enumTypeId, 0);

            if (target == 0)
                return (TEnum*)ecs_get_id(World, Id, enumTypeId);

            void* ptr = ecs_get_id(World, enumTypeId, target);
            Assert.True(ptr != null, "Missing enum constant value");
            return (TEnum*)ptr;
        }

        public readonly TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_id(World, Id, pair);
        }

        public readonly TFirst* GetPtr<TFirst, TSecondEnum>(TSecondEnum secondEnum)
            where TFirst : unmanaged
            where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, World);
            ulong pair = Macros.Pair<TFirst>(enumId, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_id(World, Id, pair);
        }

        public readonly TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_id(World, Id, pair);
        }

        public readonly TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_id(World, Id, pair);
        }

        public readonly TSecond* GetSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_id(World, Id, pair);
        }

        public ref readonly T Get<T>()
        {
            ulong id = Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<T>(ecs_get_id(World, Id, id));
        }

        public ref readonly TEnum GetEnum<TEnum>() where TEnum : Enum
        {
            ulong enumTypeId = Type<TEnum>.Id(World);
            ulong target = ecs_get_target(World, Id, enumTypeId, 0);

            if (target == 0)
                return ref Managed.GetTypeRef<TEnum>(ecs_get_id(World, Id, enumTypeId));

            void* ptr = ecs_get_id(World, enumTypeId, target);
            Assert.True(ptr != null, "Missing enum constant value");
            return ref Managed.GetTypeRef<TEnum>(ptr);
        }

        public ref readonly TFirst Get<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TFirst>(component);
        }

        public ref readonly TFirst Get<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            ulong pair = Macros.Pair<TFirst>(enumId, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TFirst>(component);
        }

        public ref readonly TFirst GetFirst<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TFirst>(component);
        }

        public ref readonly TSecond GetSecond<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TSecond>(component);
        }

        public ref readonly TSecond GetSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            void* component = ecs_get_id(World, Id, pair);
            return ref Managed.GetTypeRef<TSecond>(component);
        }

        public Entity Target(ulong relation, int index = 0)
        {
            return new Entity(World, ecs_get_target(World, Id, relation, index));
        }

        public Entity Target<T>(int index = 0) where T : unmanaged
        {
            return new Entity(World, ecs_get_target(World, Id, Type<T>.Id(World), index));
        }

        public Entity TargetFor(ulong relation, ulong id)
        {
            return new Entity(World, ecs_get_target_for_id(World, Id, relation, id));
        }

        public Entity TargetFor<T>(ulong relation)
        {
            return new Entity(World, TargetFor(relation, Type<T>.Id(World)));
        }

        public Entity TargetFor<TFirst, TSecond>(ulong relation)
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return new Entity(World, TargetFor(relation, pair));
        }

        public int Depth(ulong rel)
        {
            return ecs_get_depth(World, Id, rel);
        }

        public int Depth<T>() where T : unmanaged
        {
            return Depth(Type<T>.Id(World));
        }

        public Entity Parent()
        {
            return Target(EcsChildOf);
        }

        public Entity Lookup(string path)
        {
            Assert.True(Id != 0, "invalid lookup from null handle");
            using NativeString nativeSep = (NativeString)"::";
            using NativeString nativePath = (NativeString)path;
            ulong id = ecs_lookup_path_w_sep(World, Id, nativePath, nativeSep, nativeSep, Macros.False);
            return new Entity(World, id);
        }

        public bool Has(ulong id)
        {
            return ecs_has_id(World, Id, id) == 1;
        }

        public bool Has(ulong first, ulong second)
        {
            return Has(Macros.Pair(first, second));
        }

        public bool Has<T>()
        {
            ulong typeId = Type<T>.Id(World);
            bool result = ecs_has_id(World, Id, typeId) == 1;

            if (result)
                return result;

            return typeof(T).IsEnum && Has(typeId, EcsWildcard);
        }

        public bool Has<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return Has<TEnum>(enumId);
        }

        public bool Has<TFirst>(ulong second)
        {
            return Has(Macros.Pair<TFirst>(second, World));
        }

        public bool Has<TFirst, TSecond>()
        {
            return Has(Macros.Pair<TFirst, TSecond>(World));
        }

        public bool Has<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return Has<TFirst>(enumId);
        }

        public bool HasSecond<TSecond>(ulong first)
        {
            return Has(Macros.PairSecond<TSecond>(first, World));
        }

        public bool Owns(ulong id)
        {
            return ecs_owns_id(World, Id, id) == 1;
        }

        public bool Owns(ulong first, ulong second)
        {
            return Owns(Macros.Pair(first, second));
        }

        public bool Owns<T>()
        {
            return Owns(Type<T>.Id(World));
        }

        public bool Owns<TFirst>(ulong second)
        {
            return Owns(Macros.Pair<TFirst>(second, World));
        }

        public bool Owns<TFirst, TSecond>()
        {
            return Owns(Macros.Pair<TFirst, TSecond>(World));
        }

        public Entity Clone(bool cloneValue = true, ulong dstId = 0)
        {
            if (dstId == 0)
                dstId = ecs_new_id(World);

            Entity dst = new Entity(World, dstId);
            ecs_clone(World, dstId, Id, Macros.Bool(cloneValue));
            return dst;
        }

        public Entity Mut(ref World stage)
        {
            Assert.True(!stage.IsReadOnly(), "Cannot use readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(stage);
        }

        public Entity Mut(ref Iter it)
        {
            Assert.True(!it.World().IsReadOnly(),
                "Cannot use iterator created for readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(it.World());
        }

        public Entity Mut(ref Entity entity)
        {
            Assert.True(!entity.CsWorld().IsReadOnly(),
                "Cannot use entity created for readonly world/stage to create mutable handle");
            return new Entity(Id).SetStage(entity.World);
        }

        private Entity SetStage(ecs_world_t* stage)
        {
            return new Entity(stage, Id);
        }

        public string ToJson(ecs_entity_to_json_desc_t* desc = null)
        {
            return NativeString.GetStringAndFree(ecs_entity_to_json(World, Id, desc));
        }

        public string DocName()
        {
            return NativeString.GetString(ecs_doc_get_name(World, Id));
        }

        public string DocDetail()
        {
            return NativeString.GetString(ecs_doc_get_detail(World, Id));
        }

        public string DocLink()
        {
            return NativeString.GetString(ecs_doc_get_link(World, Id));
        }

        public string DocColor()
        {
            return NativeString.GetString(ecs_doc_get_color(World, Id));
        }

        public int AlertCount(ulong alert = 0)
        {
            return ecs_get_alert_count(World, Id, alert);
        }

        public TEnum ToConstant<TEnum>() where TEnum : unmanaged, Enum
        {
            TEnum* ptr = GetEnumPtr<TEnum>();
            Assert.True(ptr != null, "Entity is not a constant");
            return *ptr;
        }

        public ref Entity Add(ulong id)
        {
            ecs_add_id(World, Id, id);
            return ref this;
        }

        public ref Entity Add(ulong first, ulong second)
        {
            return ref Add(Macros.Pair(first, second));
        }

        public ref Entity Add<T>()
        {
            return ref Add(Type<T>.Id(World));
        }


        public ref Entity Add<TFirst>(ulong second)
        {
            return ref Add(Macros.Pair<TFirst>(second, World));
        }

        public ref Entity Add<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return ref Add<TEnum>(enumId);
        }

        public ref Entity Add<TFirst, TSecond>()
        {
            return ref Add<TFirst>(Type<TSecond>.Id(World));
        }

        public ref Entity Add<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Add<TFirst>(enumId);
        }

        public ref Entity AddSecond<TSecond>(ulong first)
        {
            return ref Add(Macros.PairSecond<TSecond>(first, World));
        }

        public ref Entity AddIf(bool cond, ulong id)
        {
            return ref cond ? ref Add(id) : ref Remove(id);
        }

        public ref Entity AddIf(bool cond, ulong first, ulong second)
        {
            if (cond)
                return ref Add(first, second);

            if (second == 0 || ecs_has_id(World, first, EcsExclusive) == 1)
                second = EcsWildcard;

            return ref Remove(first, second);
        }

        public ref Entity AddIf<T>(bool cond)
        {
            return ref cond ? ref Add<T>() : ref Remove<T>();
        }

        public ref Entity AddIf<TEnum>(bool cond, TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            return ref AddIf<TEnum>(cond, enumId);
        }

        public ref Entity AddIf<TFirst>(bool cond, ulong second)
        {
            return ref AddIf(cond, Type<TFirst>.Id(World), second);
        }

        public ref Entity AddIf<TFirst, TSecond>(bool cond)
        {
            return ref AddIf(cond, Macros.Pair<TFirst, TSecond>(World));
        }

        public ref Entity IsA(ulong id)
        {
            return ref Add(EcsIsA, id);
        }

        public ref Entity IsA<T>()
        {
            return ref Add(EcsIsA, Type<T>.Id(World));
        }

        public ref Entity ChildOf(ulong second)
        {
            return ref Add(EcsChildOf, second);
        }

        public ref Entity ChildOf<T>()
        {
            return ref Add(EcsChildOf, Type<T>.Id(World));
        }

        public ref Entity DependsOn(ulong second)
        {
            return ref Add(EcsDependsOn, second);
        }

        public ref Entity DependsOn<T>()
        {
            return ref Add(EcsDependsOn, Type<T>.Id(World));
        }

        public ref Entity SlotOf(ulong id)
        {
            return ref Add(EcsSlotOf, id);
        }

        public ref Entity SlotOf<T>()
        {
            return ref Add(EcsSlotOf, Type<T>.Id(World));
        }

        public ref Entity Slot()
        {
            Assert.True(ecs_get_target(World, Id, EcsChildOf, 0) != 0, "Add ChildOf pair before using slot()");
            return ref SlotOf(Target(EcsChildOf));
        }

        public ref Entity Remove(ulong id)
        {
            ecs_remove_id(World, Id, id);
            return ref this;
        }

        public ref Entity Remove(ulong first, ulong second)
        {
            return ref Remove(Macros.Pair(first, second));
        }

        public ref Entity Remove<T>()
        {
            return ref Remove(Type<T>.Id(World));
        }

        public ref Entity Remove<TEnum>(TEnum enumMember = default) where TEnum : Enum
        {
            return ref Remove<TEnum>(EcsWildcard);
        }

        public ref Entity Remove<TFirst>(ulong second)
        {
            return ref Remove(Macros.Pair<TFirst>(second, World));
        }

        public ref Entity Remove<TFirst, TSecond>()
        {
            return ref Remove(Macros.Pair<TFirst, TSecond>(World));
        }

        public ref Entity Remove<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Remove<TFirst>(enumId);
        }

        public ref Entity RemoveSecond<TSecond>(ulong first)
        {
            return ref Remove(Macros.PairSecond<TSecond>(first, World));
        }

        public ref Entity Override(ulong id)
        {
            ecs_add_id(World, Id, ECS_OVERRIDE | id);
            return ref this;
        }

        public ref Entity Override(ulong first, ulong second)
        {
            return ref Override(Macros.Pair(first, second));
        }

        public ref Entity Override<T>()
        {
            return ref Override(Type<T>.Id(World));
        }

        public ref Entity Override<TFirst>(ulong second)
        {
            return ref Override(Macros.Pair<TFirst>(second, World));
        }

        public ref Entity Override<TFirst, TSecond>()
        {
            return ref Override(Macros.Pair<TFirst, TSecond>(World));
        }

        public ref Entity OverrideSecond<TSecond>(ulong first)
        {
            return ref Override(Macros.PairSecond<TSecond>(first, World));
        }

        public ref Entity SetOverride<T>(T component)
        {
            return ref Override<T>().Set(component);
        }

        public ref Entity SetOverride<T>(ulong id, T component)
        {
            return ref Override<T>(id).Set(id, component);
        }

        public ref Entity SetOverrideFirst<TFirst, TSecond>(TFirst component)
        {
            return ref Override<TFirst, TSecond>().SetFirst<TFirst, TSecond>(component);
        }

        public ref Entity SetOverrideSecond<TFirst, TSecond>(TSecond component)
        {
            return ref Override<TFirst, TSecond>().SetSecond<TFirst, TSecond>(component);
        }

        public ref Entity SetOverrideSecond<TSecond>(ulong first, TSecond component)
        {
            return ref OverrideSecond<TSecond>(first).SetSecond(first, component);
        }

        public ref Entity Enable()
        {
            ecs_enable(World, Id, Macros.True);
            return ref this;
        }

        public ref Entity Enable(ulong id)
        {
            ecs_enable_id(World, Id, id, Macros.True);
            return ref this;
        }

        public ref Entity Enable(ulong first, ulong second)
        {
            return ref Enable(Macros.Pair(first, second));
        }

        public ref Entity Enable<T>()
        {
            return ref Enable(Type<T>.Id(World));
        }

        public ref Entity Enable<TFirst>(ulong second)
        {
            return ref Enable(Macros.Pair<TFirst>(second, World));
        }

        public ref Entity Enable<TFirst, TSecond>()
        {
            return ref Enable(Macros.Pair<TFirst, TSecond>(World));
        }

        public ref Entity Disable()
        {
            ecs_enable(World, Id, Macros.False);
            return ref this;
        }

        public ref Entity Disable(ulong id)
        {
            ecs_enable_id(World, Id, id, Macros.False);
            return ref this;
        }

        public ref Entity Disable(ulong first, ulong second)
        {
            return ref Disable(Macros.Pair(first, second));
        }

        public ref Entity Disable<T>()
        {
            return ref Disable(Type<T>.Id(World));
        }

        public ref Entity Disable<TFirst>(ulong second)
        {
            return ref Disable(Macros.Pair<TFirst>(second, World));
        }

        public ref Entity Disable<TFirst, TSecond>()
        {
            return ref Disable(Macros.Pair<TFirst, TSecond>(World));
        }

        public ref Entity SetPtr(ulong componentId, int size, void* data)
        {
            ecs_set_id(World, Id, componentId, (ulong)size, data);
            return ref this;
        }

        public ref Entity SetPtr(ulong componentId, ulong size, void* data)
        {
            ecs_set_id(World, Id, componentId, size, data);
            return ref this;
        }

        public ref Entity SetPtr(ulong componentId, void* data)
        {
            EcsComponent* ecsComponent = (EcsComponent*)ecs_get_id(World, componentId, FLECS_IDEcsComponentID_);
            Assert.True(ecsComponent != null, nameof(ECS_INVALID_PARAMETER));
            ecs_set_id(World, Id, componentId, (ulong)ecsComponent->size, data);
            return ref this;
        }

        public ref Entity Set<T>(T component)
        {
            return ref SetInternal(Type<T>.Id(World), ref component);
        }

        public ref Entity Set<T>(ref T component)
        {
            return ref Set(component);
        }

        public ref Entity Set<TFirst>(ulong second, TFirst component)
        {
            return ref Set(second, ref component);
        }

        public ref Entity Set<TFirst>(ulong second, ref TFirst component)
        {
            return ref SetInternal(Macros.Pair<TFirst>(second, World), ref component);
        }

        public ref Entity Set<TFirst, TSecondEnum>(TSecondEnum enumMember, ref TFirst component)
            where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Set(enumId, ref component);
        }

        public ref Entity Set<TFirst, TSecondEnum>(TSecondEnum enumMember, TFirst component) where TSecondEnum : Enum
        {
            return ref Set(enumMember, ref component);
        }

        public ref Entity SetFirst<TFirst, TSecond>(TFirst component)
        {
            return ref SetFirst<TFirst, TSecond>(ref component);
        }

        public ref Entity SetFirst<TFirst, TSecond>(ref TFirst component)
        {
            return ref SetInternal(Macros.Pair<TFirst, TSecond>(World), ref component);
        }

        public ref Entity SetSecond<TFirst, TSecond>(TSecond component)
        {
            return ref SetSecond<TFirst, TSecond>(ref component);
        }

        public ref Entity SetSecond<TFirst, TSecond>(ref TSecond component)
        {
            return ref SetInternal(Macros.Pair<TFirst, TSecond>(World), ref component);
        }

        public ref Entity SetSecond<TSecond>(ulong first, TSecond component)
        {
            return ref SetSecond(first, ref component);
        }

        public ref Entity SetSecond<TSecond>(ulong first, ref TSecond component)
        {
            return ref SetInternal(Macros.PairSecond<TSecond>(first, World), ref component);
        }

        public ref Entity With(Action func)
        {
            ulong prev = ecs_set_with(World, Id);
            func();
            ecs_set_with(World, prev);
            return ref this;
        }

        public ref Entity With(ulong first, Action func)
        {
            ulong prev = ecs_set_with(World, Macros.Pair(first, Id));
            func();
            ecs_set_with(World, prev);
            return ref this;
        }

        public ref Entity With<T>(Action func)
        {
            return ref With(Type<T>.Id(World), func);
        }

        public ref Entity Scope(Action func)
        {
            ulong prev = ecs_set_scope(World, Id);
            func();
            ecs_set_scope(World, prev);
            return ref this;
        }

        public ref Entity SetName(string name)
        {
            using NativeString nativeName = (NativeString)name;
            ecs_set_name(World, Id, nativeName);
            return ref this;
        }

        public ref Entity SetAlias(string alias)
        {
            using NativeString nativeAlias = (NativeString)alias;
            ecs_set_alias(World, Id, nativeAlias);
            return ref this;
        }

        public ref Entity SetDocName(string name)
        {
            using NativeString nativeName = (NativeString)name;
            ecs_doc_set_name(World, Id, nativeName);
            return ref this;
        }

        public ref Entity SetDocBrief(string brief)
        {
            using NativeString nativeBrief = (NativeString)brief;
            ecs_doc_set_brief(World, Id, nativeBrief);
            return ref this;
        }

        public ref Entity SetDoDetail(string detail)
        {
            using NativeString nativeDetail = (NativeString)detail;
            ecs_doc_set_detail(World, Id, nativeDetail);
            return ref this;
        }

        public ref Entity SetDocLink(string link)
        {
            using NativeString nativeLink = (NativeString)link;
            ecs_doc_set_link(World, Id, nativeLink);
            return ref this;
        }

        public ref Entity SetDocColor(string color)
        {
            using NativeString nativeColor = (NativeString)color;
            ecs_doc_set_color(World, Id, nativeColor);
            return ref this;
        }

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

        public ref Entity Quantity(ulong quantity)
        {
            ecs_add_id(World, Id, Macros.Pair(EcsQuantity, quantity));
            return ref this;
        }

        public ref Entity Quantity<T>()
        {
            return ref Quantity(Type<T>.Id(World));
        }

        public ref Entity Quantity()
        {
            ecs_add_id(World, Id, EcsQuantity);
            return ref this;
        }

        public void* GetMutPtr(ulong id)
        {
            return ecs_get_mut_id(World, Id, id);
        }

        public void* GetMutPtr(ulong first, ulong second)
        {
            return GetMutPtr(Macros.Pair(first, second));
        }

        public readonly T* GetMutPtr<T>() where T : unmanaged
        {
            ulong componentId = Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (T*)ecs_get_mut_id(World, Id, componentId);
        }

        public readonly TEnum* GetMutEnumPtr<TEnum>() where TEnum : unmanaged, Enum
        {
            ulong enumTypeId = Type<TEnum>.Id(World);
            ulong target = ecs_get_target(World, Id, enumTypeId, 0);

            if (target == 0)
                return (TEnum*)ecs_get_id(World, Id, enumTypeId);

            void* ptr = ecs_get_mut_id(World, enumTypeId, target);
            Assert.True(ptr != null, "Missing enum constant value");
            return (TEnum*)ptr;
        }

        public readonly TFirst* GetMutPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_mut_id(World, Id, pair);
        }

        public readonly TFirst* GetMutPtr<TFirst, TSecondEnum>(TSecondEnum secondEnum)
            where TFirst : unmanaged
            where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, World);
            ulong pair = Macros.Pair<TFirst>(enumId, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_mut_id(World, Id, pair);
        }

        public readonly TFirst* GetMutFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)ecs_get_mut_id(World, Id, pair);
        }

        public readonly TSecond* GetMutSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_mut_id(World, Id, pair);
        }

        public readonly TSecond* GetMutSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)ecs_get_mut_id(World, Id, pair);
        }

        public ref T GetMut<T>()
        {
            ulong compId = Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<T>(ecs_get_mut_id(World, Id, compId));
        }

        public ref TFirst GetMut<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(ecs_get_mut_id(World, Id, pair));
        }

        public ref TFirst GetMutFirst<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TFirst>(ecs_get_mut_id(World, Id, pair));
        }

        public ref TSecond GetMutSecond<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(ecs_get_mut_id(World, Id, pair));
        }

        public ref TSecond GetMutSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<TSecond>(ecs_get_mut_id(World, Id, pair));
        }

        public void Modified(ulong id)
        {
            ecs_modified_id(World, Id, id);
        }

        public void Modified(ulong first, ulong second)
        {
            Modified(Macros.Pair(first, second));
        }

        public void Modified<T>()
        {
            Modified(Type<T>.Id(World));
        }

        public void Modified<TFirst>(ulong second)
        {
            Modified(Macros.Pair<TFirst>(second, World));
        }

        public void Modified<TFirst, TSecond>()
        {
            Modified(Macros.Pair<TFirst, TSecond>(World));
        }

        public void ModifiedSecond<TSecond>(ulong first)
        {
            Modified(Macros.PairSecond<TSecond>(first, World));
        }

        public Ref<T> GetRef<T>()
        {
            Type<T>.Id(World);
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<T>(World, Id);
        }

        public Ref<TFirst> GetRef<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TFirst>(World, Id, pair);
        }

        public Ref<TFirst> GetRefFirst<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TFirst>(World, Id, pair);
        }

        public Ref<TSecond> GetRefSecond<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TSecond>(World, Id, pair);
        }

        public Ref<TSecond> GetRefSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Ref<TSecond>(World, Id, pair);
        }

        public void Flatten(ulong relation, ecs_flatten_desc_t* desc = null)
        {
            ecs_flatten(World, Macros.Pair(relation, Id), desc);
        }

        public void Clear()
        {
            ecs_clear(World, Id);
        }

        public void Destruct()
        {
            ecs_delete(World, Id);
        }

        public string FromJson(string json)
        {
            using NativeString nativeJson = (NativeString)json;
            return NativeString.GetString(ecs_entity_from_json(World, Id, nativeJson, null));
        }

        private ref Entity SetInternal<T>(ulong id, ref T component)
        {
            bool isRef = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            int size = isRef ? sizeof(IntPtr) : sizeof(T);

            fixed (void* data = &component)
            {
                IntPtr ptr = default;
                ecs_set_id(World, Id, id, (ulong)size, isRef ? Managed.AllocGcHandle(&ptr, ref component) : data);
                return ref this;
            }
        }

        public static implicit operator ulong(Entity entity)
        {
            return ToUInt64(entity);
        }

        public static ulong ToUInt64(Entity entity)
        {
            return entity.Id.Value;
        }

        public bool Equals(Entity other)
        {
            return Id.Value == other.Id.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity entity && Equals(entity);
        }

        public override int GetHashCode()
        {
            return Id.Value.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
