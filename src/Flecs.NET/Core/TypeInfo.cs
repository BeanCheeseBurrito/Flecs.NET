using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_type_info_t.
    /// </summary>
    public unsafe struct TypeInfo : IEquatable<TypeInfo>
    {
        private ecs_type_info_t* _handle;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_type_info_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a type info from the provided handle.
        /// </summary>
        /// <param name="typeInfo"></param>
        public TypeInfo(ecs_type_info_t* typeInfo)
        {
            _handle = typeInfo;
        }

        /// <summary>
        ///     Checks if two <see cref="TypeInfo"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TypeInfo other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="TypeInfo"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is TypeInfo other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="TypeInfo"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="TypeInfo"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TypeInfo left, TypeInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="TypeInfo"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TypeInfo left, TypeInfo right)
        {
            return !(left == right);
        }
    }
}
