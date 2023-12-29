using System;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Metric builder interface.
    /// </summary>
    public unsafe struct MetricBuilder : IDisposable, IEquatable<MetricBuilder>
    {
        private ecs_world_t* _world;
        private ecs_metric_desc_t _desc;
        private NativeList<NativeString> _strings;

        /// <summary>
        ///     The world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the desc struct.
        /// </summary>
        public ref ecs_metric_desc_t Desc => ref _desc;

        /// <summary>
        ///     Creates a metric builder using the provided entity.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        public MetricBuilder(ecs_world_t* world, ulong entity)
        {
            _world = world;
            _desc = default;
            _strings = default;
            Desc.entity = entity;
        }

        /// <summary>
        ///     Disposes resources.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < _strings.Count; i++)
                _strings[i].Dispose();

            _strings.Dispose();
        }

        /// <summary>
        ///     Set member.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ref MetricBuilder Member(ulong entity)
        {
            Desc.member = entity;
            return ref this;
        }

        /// <summary>
        ///     Set member.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref MetricBuilder Member(string name)
        {
            Entity m = Desc.id == 0
                ? new World(World).Lookup(name)
                : new Entity(World, ecs_get_typeid(World, Desc.id)).Lookup(name);

            if (m == 0)
                Ecs.Log.Err("member '%s' not found", name);

            return ref Member(m);
        }

        /// <summary>
        ///     Set member.
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
        ///     Set dot member.
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public ref MetricBuilder DotMember(string expr)
        {
            NativeString nativeExpr = (NativeString)expr;
            Desc.dotmember = nativeExpr;
            _strings.Add(nativeExpr);
            return ref this;
        }

        /// <summary>
        ///     Set dot member.
        /// </summary>
        /// <param name="expr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder DotMember<T>(string expr)
        {
            NativeString nativeExpr = (NativeString)expr;
            Desc.dotmember = nativeExpr;
            Desc.id = Type<T>.Id(World);
            _strings.Add(nativeExpr);
            return ref this;
        }

        /// <summary>
        ///     Set id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref MetricBuilder Id(ulong id)
        {
            Desc.id = id;
            return ref this;
        }

        /// <summary>
        ///     Set id.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref MetricBuilder Id(ulong first, ulong second)
        {
            return ref Id(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Set id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Id<T>()
        {
            return ref Id(Type<T>.Id(World));
        }

        /// <summary>
        ///     Set id.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Id<TFirst>(ulong second)
        {
            return ref Id(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Set id.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Id<TFirst, TSecond>()
        {
            return ref Id(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Set id.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder IdSecond<TSecond>(ulong first)
        {
            return ref Id(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Set target.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref MetricBuilder Targets(bool value = true)
        {
            Desc.targets = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Set kind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public ref MetricBuilder Kind(ulong kind)
        {
            Desc.kind = kind;
            return ref this;
        }

        /// <summary>
        ///     Set kind.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref MetricBuilder Kind<T>()
        {
            return ref Kind(Type<T>.Id(World));
        }

        /// <summary>
        ///     Set doc brief.
        /// </summary>
        /// <param name="brief"></param>
        /// <returns></returns>
        public ref MetricBuilder Brief(string brief)
        {
            using NativeString nativeBrief = (NativeString)brief;
            Desc.brief = nativeBrief;
            return ref this;
        }

        /// <summary>
        ///     Checks if two <see cref="MetricBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MetricBuilder other)
        {
            return Desc == other.Desc;
        }

        /// <summary>
        ///     Checks if two <see cref="MetricBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is MetricBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="MetricBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="MetricBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(MetricBuilder left, MetricBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="MetricBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(MetricBuilder left, MetricBuilder right)
        {
            return !(left == right);
        }
    }
}
