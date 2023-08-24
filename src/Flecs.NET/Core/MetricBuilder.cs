using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    /// Event builder interface.
    /// </summary>
    public unsafe struct MetricBuilder
    {
        private ecs_world_t* _world;
        private ecs_metric_desc_t _desc;

        /// <summary>
        /// The world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        /// A reference to the desc struct.
        /// </summary>
        public ref ecs_metric_desc_t Desc => ref _desc;

        /// <summary>
        /// Creates a metric builder using the provided entity.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        public MetricBuilder(ecs_world_t* world, ulong entity)
        {
            _desc = default;
            _world = world;
            Desc.entity = entity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ref MetricBuilder Member(ulong entity)
        {
            Desc.member = entity;
            return ref this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref MetricBuilder Member(string name)
        {
            return ref Member(new World(World).Lookup(name));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Member<T>(string name)
        {
            Entity entity = new Entity(World, Type<T>.Id(World));
            ulong member = entity.Lookup(name);

            if (member != 0)
                return ref this;

            return ref Member(member);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref MetricBuilder Id(ulong id)
        {
            Desc.id = id;
            return ref this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref MetricBuilder Id(ulong first, ulong second)
        {
            return ref Id(Macros.Pair(first, second));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Id<T>()
        {
            return ref Id(Type<T>.Id(World));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Id<TFirst>(ulong second)
        {
            return ref Id(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Id<TFirst, TSecond>()
        {
            return ref Id(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder IdSecond<TSecond>(ulong first)
        {
            return ref Id(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref MetricBuilder Targets(bool value = true)
        {
            Desc.targets = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public ref MetricBuilder Kind(ulong kind)
        {
            Desc.kind = kind;
            return ref this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Kind<T>()
        {
            return ref Kind(Type<T>.Id(World));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="brief"></param>
        /// <returns></returns>
        public ref MetricBuilder Brief(string brief)
        {
            using NativeString nativeBrief = (NativeString)brief;
            Desc.brief = nativeBrief;
            return ref this;
        }
    }
}
