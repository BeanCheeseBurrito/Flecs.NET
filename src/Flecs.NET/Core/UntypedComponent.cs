using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     An untyped component.
    /// </summary>
    public unsafe struct UntypedComponent
    {
        private Entity _entity;

        /// <summary>
        ///     Reference to id.
        /// </summary>
        public ref Id Id => ref _entity.Id;

        /// <summary>
        ///     Reference to entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

        /// <summary>
        ///     Reference to world pointer.
        /// </summary>
        public ref ecs_world_t* World => ref _entity.World;

        /// <summary>
        ///     Constructs component from entity id.
        /// </summary>
        /// <param name="id"></param>
        public UntypedComponent(ulong id)
        {
            _entity = new Entity(id);
        }

        /// <summary>
        ///     Constructs component from new id using the provided world.
        /// </summary>
        /// <param name="world"></param>
        public UntypedComponent(ecs_world_t* world)
        {
            _entity = new Entity(world);
        }

        /// <summary>
        ///     Constructs component with the provided world and entity id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public UntypedComponent(ecs_world_t* world, ulong id)
        {
            _entity = new Entity(world, id);
        }

        /// <summary>
        ///     Constructs component wit hthe provided world and entity name.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public UntypedComponent(ecs_world_t* world, string name)
        {
            _entity = new Entity(world, name);
        }

        /// <summary>
        ///     Add member with unit.
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ref UntypedComponent Member(ulong typeId, ulong unit, string name, int count, int offset = 0)
        {
            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.add[0] = Macros.Pair(EcsChildOf, Entity);
            ulong id = ecs_entity_init(World, &desc);
            Assert.True(id != 0, nameof(ECS_INTERNAL_ERROR));

            Entity entity = new Entity(World, id);

            EcsMember member = default;
            member.type = typeId;
            member.unit = unit;
            member.count = count;
            member.offset = offset;
            entity.Set(member);

            return ref this;
        }

        /// <summary>
        ///     Add member.
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ref UntypedComponent Member(ulong typeId, string name, int count = 0, int offset = 0)
        {
            return ref Member(typeId, 0, name, count, offset);
        }

        /// <summary>
        ///     Add member.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref UntypedComponent Member<T>(string name, int count = 0, int offset = 0)
        {
            return ref Member(Type<T>.Id(World), name, count, offset);
        }

        /// <summary>
        ///     Add member with unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref UntypedComponent Member<T>(ulong unit, string name, int count = 0, int offset = 0)
        {
            return ref Member(Type<T>.Id(World), unit, name, count, offset);
        }

        /// <summary>
        ///     Add member with unit.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TUnit"></typeparam>
        /// <returns></returns>
        public ref UntypedComponent Member<T, TUnit>(string name, int count = 0, int offset = 0)
        {
            return ref Member(Type<T>.Id(World), Type<TUnit>.Id(World), name, count, offset);
        }

        /// <summary>
        ///     Add constant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref UntypedComponent Constant(string name, int value)
        {
            ecs_add_id(World, Id, Type<EcsEnum>.Id(World));

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.add[0] = Macros.Pair(EcsChildOf, Id);

            ulong id = ecs_entity_init(World, &desc);
            Assert.True(id != 0, nameof(ECS_INTERNAL_ERROR));

            ecs_set_id(World, id, Macros.Pair(EcsConstant, FLECS_IDecs_i32_tID_), sizeof(int), &value);
            return ref this;
        }

        /// <summary>
        ///     Add bitmask constant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref UntypedComponent Bit(string name, uint value)
        {
            ecs_add_id(World, Id, Type<EcsBitmask>.Id(World));

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.add[0] = Macros.Pair(EcsChildOf, Id);

            ulong id = ecs_entity_init(World, &desc);
            Assert.True(id != 0, nameof(ECS_INTERNAL_ERROR));

            ecs_set_id(World, id, Macros.Pair(EcsConstant, FLECS_IDecs_u32_tID_), sizeof(uint), &value);
            return ref this;
        }

        /// <summary>
        ///     Add member value range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public ref UntypedComponent Range(double min, double max)
        {
            ecs_member_t* m = ecs_cpp_last_member(World, Entity);

            if (m == null)
                return ref this;

            World w = new World(World);
            Entity me = w.Entity(m->member);
            ref EcsMemberRanges mr = ref me.GetMut<EcsMemberRanges>();
            mr.value.min = min;
            mr.value.max = max;
            me.Modified<EcsMemberRanges>();
            return ref this;
        }

        /// <summary>
        ///     Add member warning range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public ref UntypedComponent WarningRange(double min, double max)
        {
            ecs_member_t* m = ecs_cpp_last_member(World, Entity);

            if (m == null)
                return ref this;

            World w = new World(World);
            Entity me = w.Entity(m->member);
            ref EcsMemberRanges mr = ref me.GetMut<EcsMemberRanges>();
            mr.warning.min = min;
            mr.warning.max = max;
            me.Modified<EcsMemberRanges>();
            return ref this;
        }

        /// <summary>
        ///     Add member error range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public ref UntypedComponent ErrorRange(double min, double max)
        {
            ecs_member_t* m = ecs_cpp_last_member(World, Entity);

            if (m == null)
                return ref this;

            World w = new World(World);
            Entity me = w.Entity(m->member);
            ref EcsMemberRanges mr = ref me.GetMut<EcsMemberRanges>();
            mr.error.min = min;
            mr.error.max = max;
            me.Modified<EcsMemberRanges>();
            return ref this;
        }

        /// <summary>
        ///     Register member as metric.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="brief"></param>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref UntypedComponent Metric<T>(ulong parent = 0, string brief = "", string name = "")
        {
            World world = new World(World);

            ecs_member_t* m = ecs_cpp_last_member(World, Id);

            if (m == null)
                return ref this;

            Entity me = world.Entity(m->member);
            Entity metricEntity = me;

            if (parent != 0)
            {
                string componentName = Entity.Name();

                if (string.IsNullOrEmpty(name))
                {
                    string mName = NativeString.GetString(m->name);

                    if (mName == "value" || string.IsNullOrEmpty(name))
                    {
                        using ScopedWorld _ = world.Scope(parent);
                        metricEntity = world.Entity(mName);
                    }
                    else
                    {
                        using NativeString nativeComponentName = (NativeString)componentName;
                        string snakeName = NativeString.GetStringAndFree(flecs_to_snake_case(nativeComponentName));

                        using ScopedWorld _ = world.Scope(componentName);
                        metricEntity = world.Entity(snakeName);
                    }
                }
                else
                {
                    using ScopedWorld _ = world.Scope(parent);
                    metricEntity = world.Entity(name);
                }
            }

            world.Metric(metricEntity).Member(me).Kind<T>().Brief(brief);

            return ref this;
        }
    }
}
