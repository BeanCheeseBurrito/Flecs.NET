using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for an alert.
    /// </summary>
    public unsafe struct Alert : IEquatable<Alert>
    {
        private Entity _entity;

        /// <summary>
        ///     Reference to entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

        /// <summary>
        ///     Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _entity.World;

        /// <summary>
        ///     Creates an alert with the provided world and entity id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public Alert(ecs_world_t* world, ulong id)
        {
            _entity = new Entity(world, id);
        }

        /// <summary>
        ///     Creates an alert with the provided entity.
        /// </summary>
        /// <param name="entity"></param>
        public Alert(Entity entity)
        {
            _entity = entity;
        }

        /// <summary>
        ///     Returns the entity's name if it has one, otherwise return its id.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Entity.ToString();
        }

        /// <summary>
        ///     Checks if two <see cref="Alert"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Alert other)
        {
            return _entity == other._entity;
        }

        /// <summary>
        ///     Checks if two <see cref="Alert"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Alert other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Alert"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _entity.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Alert"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Alert left, Alert right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Alert"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Alert left, Alert right)
        {
            return !(left == right);
        }
    }
}
