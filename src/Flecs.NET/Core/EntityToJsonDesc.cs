using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper of ecs_entity_to_json_desc_t.
    /// </summary>
    public unsafe struct EntityToJsonDesc : System.IEquatable<EntityToJsonDesc>
    {
        private ecs_entity_to_json_desc_t _desc;

        /// <summary>
        ///     Reference to desc.
        /// </summary>
        public ref ecs_entity_to_json_desc_t Desc => ref _desc;

        /// <summary>
        ///     Serialize full path name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Path(bool value = true)
        {
            Desc.serialize_path = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Label(bool value = true)
        {
            Desc.serialize_label = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize brief doc description.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Brief(bool value = true)
        {
            Desc.serialize_brief = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc link (URL).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Link(bool value = true)
        {
            Desc.serialize_link = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc color.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Color(bool value = true)
        {
            Desc.serialize_color = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize (component) ids.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Ids(bool value = true)
        {
            Desc.serialize_ids = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize labels of (component) ids.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc IdLabels(bool value = true)
        {
            Desc.serialize_id_labels = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize base components.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Base(bool value = true)
        {
            Desc.serialize_base = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize private components.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Private(bool value = true)
        {
            Desc.serialize_private = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize ids hidden by override.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Hidden(bool value = true)
        {
            Desc.serialize_hidden = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize component values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Values(bool value = true)
        {
            Desc.serialize_values = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize type info. (requires Values() to be set to true)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc TypeInfo(bool value = true)
        {
            Desc.serialize_type_info = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize active alerts for entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Alerts(bool value = true)
        {
            Desc.serialize_alerts = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize references (incoming edges) for relationship.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Refs(ulong value)
        {
            Desc.serialize_refs = value;
            return ref this;
        }

        /// <summary>
        ///     Serialize which queries this entity matches with.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref EntityToJsonDesc Matches(bool value = true)
        {
            Desc.serialize_matches = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Checks if two <see cref="EntityToJsonDesc"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EntityToJsonDesc other)
        {
            return Desc == other.Desc;
        }

        /// <summary>
        ///     Checks if two <see cref="EntityToJsonDesc"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public readonly override bool Equals(object? obj)
        {
            return obj is EventBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="EventBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="EntityToJsonDesc"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(EntityToJsonDesc left, EntityToJsonDesc right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="EntityToJsonDesc"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(EntityToJsonDesc left, EntityToJsonDesc right)
        {
            return !(left == right);
        }
    }
}
