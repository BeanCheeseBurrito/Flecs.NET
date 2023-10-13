using System;
using Flecs.NET.Utilities;
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
        ///     Creates an alert.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="alertBuilder"></param>
        public Alert(ecs_world_t* world, string name = "", FilterBuilder filterBuilder = default,
            AlertBuilder alertBuilder = default)
        {
            ecs_alert_desc_t* alertDesc = &alertBuilder.AlertDesc;
            alertDesc->filter = filterBuilder.Desc;
            alertDesc->filter.terms_buffer = filterBuilder.Terms.Data;
            alertDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;

            if (!string.IsNullOrEmpty(name))
            {
                using NativeString nativeName = (NativeString)name;

                ecs_entity_desc_t entityDesc = default;
                entityDesc.name = nativeName;
                entityDesc.sep = BindingContext.DefaultSeparator;
                entityDesc.root_sep = BindingContext.DefaultRootSeparator;
                alertDesc->entity = ecs_entity_init(world, &entityDesc);
            }

            _entity = new Entity(world, ecs_alert_init(world, alertDesc));

            filterBuilder.Dispose();
            alertBuilder.Dispose();
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
