using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_type_info_t"/>*.
    /// </summary>
    public readonly unsafe struct TypeInfo : IEquatable<TypeInfo>
    {
        private readonly ecs_type_info_t* _handle;

        /// <summary>
        ///     The pointer to the <see cref="ecs_type_info_t"/> object.
        /// </summary>
        public ref readonly ecs_type_info_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a new <see cref="TypeInfo"/> instance with the provided <see cref="ecs_type_info_t"/> pointer.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public TypeInfo(ecs_type_info_t* handle)
        {
            _handle = handle;
        }

        /// <summary>
        ///     Size of type.
        /// </summary>
        public int Size => Handle->size;

        /// <summary>
        ///     Alignment of type.
        /// </summary>
        public int Alignment => Handle->size;

        /// <summary>
        ///     Handle to component.
        /// </summary>
        public ulong Component => Handle->component;

        /// <summary>
        ///     Type name.
        /// </summary>
        public string Name => NativeString.GetString(Handle->name);

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
