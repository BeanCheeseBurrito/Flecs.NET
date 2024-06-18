using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_term_t"/>.
    /// </summary>
    public unsafe struct Term : IEquatable<Term>
    {
        private ecs_world_t* _world;
        private ecs_term_t _value;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the value.
        /// </summary>
        public ref ecs_term_t Value => ref _value;

        /// <summary>
        ///     Creates a term with the provided world and default value.
        /// </summary>
        /// <param name="world"></param>
        public Term(ecs_world_t* world)
        {
            _world = world;
            _value = default;
        }

        /// <summary>
        ///     Creates a term with the provided world and value.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="value"></param>
        public Term(ecs_world_t* world, ecs_term_t value)
        {
            _world = world;
            _value = value;
        }

        /// <summary>
        ///     Creates a term with the provided world and id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public Term(ecs_world_t* world, ulong id)
        {
            _world = world;
            _value = default;

            if ((id & ECS_ID_FLAGS_MASK) == 0)
                Value.first.id = id;
            else
                Value.id = id;
        }

        /// <summary>
        ///     Creates a term with the provided world and pair.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public Term(ecs_world_t* world, ulong first, ulong second)
        {
            _world = world;
            _value = default;
            Value.id = Macros.Pair(first, second);
        }

        /// <summary>
        ///     Creates a term with the provided id.
        /// </summary>
        /// <param name="id"></param>
        public Term(ulong id) : this(null, id)
        {
        }

        /// <summary>
        ///     Creates a term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public Term(ulong first, ulong second) : this(null, first, second)
        {
        }

        /// <summary>
        ///     Returns this term's id.
        /// </summary>
        /// <returns></returns>
        public Id Id()
        {
            return new Id(World, Value.id);
        }

        /// <summary>
        ///     Returns this term's inout value.
        /// </summary>
        /// <returns></returns>
        public ecs_inout_kind_t InOut()
        {
            return (ecs_inout_kind_t)Value.inout;
        }

        /// <summary>
        ///     Returns this term's oper value.
        /// </summary>
        /// <returns></returns>
        public ecs_oper_kind_t Oper()
        {
            return (ecs_oper_kind_t)Value.oper;
        }

        /// <summary>
        ///     Returns this term's source.
        /// </summary>
        /// <returns></returns>
        public Entity GetSrc()
        {
            return new Entity(World, Macros.TermRefId(ref Value.src));
        }

        /// <summary>
        ///     Returns this term's first id.
        /// </summary>
        /// <returns></returns>
        public Entity GetFirst()
        {
            return new Entity(World, Macros.TermRefId(ref Value.first));
        }

        /// <summary>
        ///     Returns this term's second id.
        /// </summary>
        /// <returns></returns>
        public Entity GetSecond()
        {
            return new Entity(World, Macros.TermRefId(ref Value.second));
        }

        /// <summary>
        ///     Checks if two <see cref="Term"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Term other)
        {
            return Value == other.Value;
        }

        /// <summary>
        ///     Checks if two <see cref="Term"/> instance equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Term other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Term"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Term"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Term left, Term right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Term"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Term left, Term right)
        {
            return !(left == right);
        }
    }
}
