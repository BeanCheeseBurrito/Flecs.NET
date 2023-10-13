using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Reference to a component from a specific entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe struct Ref<T> : IEquatable<Ref<T>>
    {
        private ecs_world_t* _world;
        private ecs_ref_t _ref;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Creates a ref.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public Ref(ecs_world_t* world, ulong entity, ulong id = 0)
        {
            _world = world == null ? null : ecs_get_world(world);

            if (id == 0)
                id = Type<T>.Id(world);

            Ecs.Assert(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));

            _ref = ecs_ref_init_id(world, entity, id);
        }

        /// <summary>
        ///     Gets a pointer to the ref component.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T* GetPtr()
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                throw new InvalidOperationException("Can't use GetPtr on managed types");

            fixed (ecs_ref_t* refPtr = &_ref)
            {
                return (T*)ecs_ref_get_id(World, refPtr, _ref.id);
            }
        }

        /// <summary>
        ///     Gets a reference to the ref component.
        /// </summary>
        /// <returns></returns>
        public ref T Get()
        {
            fixed (ecs_ref_t* refPtr = &_ref)
            {
                void* data = ecs_ref_get_id(World, refPtr, _ref.id);
                return ref Managed.GetTypeRef<T>(data);
            }
        }

        /// <summary>
        ///     Returns the entity associated with the ref.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, _ref.entity);
        }

        /// <summary>
        ///     Checks if two <see cref="Ref{T}"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Ref<T> other)
        {
            return Equals(_ref, other._ref);
        }

        /// <summary>
        ///     Checks if two <see cref="Ref{T}"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Ref<T> other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Ref{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _ref.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Ref{T}"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Ref<T> left, Ref<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Ref{T}"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Ref<T> left, Ref<T> right)
        {
            return !(left == right);
        }
    }
}
