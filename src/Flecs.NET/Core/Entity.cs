using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    [SuppressMessage("Usage", "CS9087")]
    public unsafe struct Entity : IEquatable<Entity>
    {
        public ecs_world_t* World { get; set; }
        public Id Id { get; set; }

        public Entity(ulong id)
        {
            World = null;
            Id = id;
        }

        public Entity(ecs_world_t* world)
        {
            World = world;
            Id = ecs_new_w_id(world, 0);
        }

        public Entity(ecs_world_t* world, ulong id)
        {
            World = world;
            Id = id;
        }

        public Entity(ecs_world_t* world, string name)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSeparator = (NativeString)"::";

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.sep = nativeSeparator;
            desc.root_sep = nativeSeparator;

            Id = ecs_entity_init(world, &desc);
            World = world;
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
            using NativeString nativeSep = (NativeString)sep;
            using NativeString nativeInitSep = (NativeString)initSep;
            using NativeString nativePath = (NativeString)ecs_get_path_w_sep(World, 0, Id, nativeSep, nativeInitSep);
            return (string)nativePath;
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

        public bool Enabled<TFirst>(ulong id)
        {
            return Enabled(Macros.Pair(Type<TFirst>.Id(World), id));
        }

        public bool Enabled<TFirst, TSecond>()
        {
            return Enabled(Macros.Pair(Type<TFirst>.Id(World), Type<TSecond>.Id(World)));
        }

        public Types Types()
        {
            return new Types(World, ecs_get_type(World, Id));
        }

        public readonly void* GetPtr(ulong compId)
        {
            return ecs_get_id(World, Id, compId);
        }

        public readonly void* GetPtr(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ecs_get_id(World, Id, pair);
        }

        public ref readonly T Get<T>()
        {
            void* component = ecs_get_id(World, Id, Type<T>.Id(World));
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<T>(component);
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
            ulong pair = Macros.Pair(first, second);
            return Has(pair);
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
            ulong pair = Macros.Pair<TFirst>(second, World);
            return Has(pair);
        }

        public bool Has<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return Has(pair);
        }

        public bool Hash<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return Has<TFirst>(enumId);
        }

        public bool HasSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return Has(pair);
        }

        public bool Owns(ulong id)
        {
            return ecs_owns_id(World, Id, id) == 1;
        }

        public bool Owns(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return Owns(pair);
        }

        public bool Owns<T>()
        {
            return Owns(Type<T>.Id(World));
        }

        public bool Owns<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return Owns(pair);
        }

        public bool Owns<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return Owns(pair);
        }

        public ref Entity SetName(string name)
        {
            using NativeString nativeName = (NativeString)name;
            ecs_set_name(World, Id, nativeName);
            return ref this;
        }

        public ref Entity Add(ulong id)
        {
            ecs_add_id(World, Id, id);
            return ref this;
        }

        public ref Entity Add(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ref Add(pair);
        }

        public ref Entity Add<T>()
        {
            return ref Add(Type<T>.Id(World));
        }


        public ref Entity Add<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ref Add(pair);
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
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref Add(pair);
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
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref AddIf(cond, pair);
        }

        public ref Entity Remove(ulong id)
        {
            ecs_remove_id(World, Id, id);
            return ref this;
        }

        public ref Entity Remove(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ref Remove(pair);
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
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ref Remove(pair);
        }

        public ref Entity Remove<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref Remove(pair);
        }

        public ref Entity Remove<TFirst, TSecondEnum>(TSecondEnum enumMember) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(enumMember, World);
            return ref Remove<TFirst>(enumId);
        }

        public ref Entity RemoveSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref Remove(pair);
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
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ref SetInternal(pair, ref component);
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
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref SetInternal(pair, ref component);
        }

        public ref Entity SetSecond<TFirst, TSecond>(TSecond component)
        {
            return ref SetSecond<TFirst, TSecond>(ref component);
        }

        public ref Entity SetSecond<TFirst, TSecond>(ref TSecond component)
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref SetInternal(pair, ref component);
        }

        public ref Entity SetSecond<TSecond>(ulong first, TSecond component)
        {
            return ref SetSecond(first, ref component);
        }

        public ref Entity SetSecond<TSecond>(ulong first, ref TSecond component)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref SetInternal(pair, ref component);
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

        public ref Entity Override(ulong id)
        {
            ecs_add_id(World, Id, ECS_OVERRIDE | id);
            return ref this;
        }

        public ref Entity Override(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ref Override(pair);
        }

        public ref Entity Override<T>()
        {
            return ref Override(Type<T>.Id(World));
        }

        public ref Entity Override<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ref Override(pair);
        }

        public ref Entity Override<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref Override(pair);
        }

        public ref Entity OverrideSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref Override(pair);
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
            ulong pair = Macros.Pair(first, second);
            return ref Enable(pair);
        }

        public ref Entity Enable<T>()
        {
            return ref Enable(Type<T>.Id(World));
        }

        public ref Entity Enable<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ref Enable(pair);
        }

        public ref Entity Enable<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref Enable(pair);
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
            ulong pair = Macros.Pair(first, second);
            return ref Disable(pair);
        }

        public ref Entity Disable<T>()
        {
            return ref Disable(Type<T>.Id(World));
        }

        public ref Entity Disable<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ref Disable(pair);
        }

        public ref Entity Disable<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref Disable(pair);
        }

        public ref Entity Scope(Action func)
        {
            ulong prev = ecs_set_scope(World, Id);
            func();
            ecs_set_scope(World, prev);
            return ref this;
        }

        public ref Entity With(Action func)
        {
            ulong prev = ecs_set_with(World, Id);
            func();
            ecs_set_with(World, prev);
            return ref this;
        }

        public void* GetMutPtr(ulong id)
        {
            return ecs_get_mut_id(World, Id, id);
        }

        public void* GetMutPtr(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ecs_get_mut_id(World, Id, pair);
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
            ulong pair = Macros.Pair(first, second);
            Modified(pair);
        }

        public void Modified<T>()
        {
            Modified(Type<T>.Id(World));
        }

        public void Modified<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            Modified(pair);
        }

        public void Modified<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            Modified(pair);
        }

        public void ModifiedSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            Modified(pair);
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

        public void Clear()
        {
            ecs_clear(World, Id);
        }

        public void Destruct()
        {
            ecs_delete(World, Id);
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