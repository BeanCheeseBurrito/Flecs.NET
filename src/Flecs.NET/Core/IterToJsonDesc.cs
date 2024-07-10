using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_iter_to_json_desc_t.
    /// </summary>
    public unsafe struct IterToJsonDesc : IEquatable<IterToJsonDesc>
    {
        private ecs_iter_to_json_desc_t _desc;

        /// <summary>
        ///     Reference to desc.
        /// </summary>
        public ref ecs_iter_to_json_desc_t Desc => ref _desc;

        /// <summary>
        ///     Default serialization configuration.
        /// </summary>
        public static IterToJsonDesc Default => new IterToJsonDesc()
        {
            Desc = new ecs_iter_to_json_desc_t
            {
                serialize_entity_ids =    Utils.False,
                serialize_values =        Utils.True,
                serialize_doc =           Utils.False,
                serialize_full_paths =    Utils.False,
                serialize_fields =        Utils.True,
                serialize_inherited =     Utils.False,
                serialize_table =         Utils.False,
                serialize_type_info =     Utils.False,
                serialize_field_info =    Utils.False,
                serialize_query_info =    Utils.False,
                serialize_query_plan =    Utils.False,
                serialize_query_profile = Utils.False,
                dont_serialize_results =  Utils.False,
                serialize_alerts =        Utils.False,
                serialize_refs =          Utils.False,
                serialize_matches =       Utils.False
            }
        };

        /// <summary>
        ///     Serialize entity ids.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc EntityIds(bool value = true)
        {
            Desc.serialize_entity_ids = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize component values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Values(bool value = true)
        {
            Desc.serialize_values = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc attributes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Doc(bool value = true)
        {
            Desc.serialize_doc = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc names of matched variables.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc VarLabels(bool value = true)
        {
            Desc.serialize_var_labels = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize full paths for tags, components and pairs.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc FullPaths(bool value = true)
        {
            Desc.serialize_full_paths = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize field data.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Fields(bool value = true)
        {
            Desc.serialize_fields = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize inherited components.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Inherited(bool value = true)
        {
            Desc.serialize_inherited = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize entire table vs. matched components.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Table(bool value = true)
        {
            Desc.serialize_table = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize type information.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc TypeInfo(bool value = true)
        {
            Desc.serialize_type_info = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize metadata for fields returned by query.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc FieldInfo(bool value = true)
        {
            Desc.serialize_field_info = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize query terms.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc QueryInfo(bool value = true)
        {
            Desc.serialize_query_info = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize query plan.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc QueryPlan(bool value = true)
        {
            Desc.serialize_query_plan = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Profile query performance.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc QueryProfile(bool value = true)
        {
            Desc.serialize_query_profile = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     If true, query won't be evaluated.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc DontSerializeResults(bool value = true)
        {
            Desc.dont_serialize_results = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize active alerts for entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc SerializeAlerts(bool value = true)
        {
            Desc.serialize_alerts = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize references (incoming edges) for relationship.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc SerializeRefs(ulong value)
        {
            Desc.serialize_refs = value;
            return ref this;
        }

        /// <summary>
        ///     Serialize which queries entity matches with
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc SerializeMatches(bool value = true)
        {
            Desc.serialize_matches = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Query object (required for serialize_query_[plan|profile]).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Query(void* value)
        {
            Desc.query = value;
            return ref this;
        }

        /// <summary>
        ///     Checks if two <see cref="IterToJsonDesc"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IterToJsonDesc other)
        {
            return Desc == other.Desc;
        }

        /// <summary>
        ///     Checks if two <see cref="IterToJsonDesc"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is IterToJsonDesc other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="IterToJsonDesc"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="IterToJsonDesc"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(IterToJsonDesc left, IterToJsonDesc right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="IterToJsonDesc"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(IterToJsonDesc left, IterToJsonDesc right)
        {
            return !(left == right);
        }
    }
}
