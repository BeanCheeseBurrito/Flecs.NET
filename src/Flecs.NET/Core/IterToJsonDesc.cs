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
                serialize_entity_ids =    Macros.False,
                serialize_values =        Macros.True,
                serialize_doc =           Macros.False,
                serialize_full_paths =    Macros.False,
                serialize_inherited =     Macros.False,
                serialize_table =         Macros.False,
                serialize_type_info =     Macros.False,
                serialize_field_info =    Macros.False,
                serialize_query_info =    Macros.False,
                serialize_query_plan =    Macros.False,
                serialize_query_profile = Macros.False,
                dont_serialize_results =  Macros.False,
                serialize_alerts =        Macros.False,
                serialize_refs =          Macros.False,
                serialize_matches =       Macros.False
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
            Desc.serialize_entity_ids = Macros.Bool(value);
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
            Desc.serialize_values = Macros.Bool(value);
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
            Desc.serialize_doc = Macros.Bool(value);
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
            Desc.serialize_var_labels = Macros.Bool(value);
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
            Desc.serialize_full_paths = Macros.Bool(value);
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
            Desc.serialize_inherited = Macros.Bool(value);
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
            Desc.serialize_table = Macros.Bool(value);
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
            Desc.serialize_type_info = Macros.Bool(value);
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
            Desc.serialize_field_info = Macros.Bool(value);
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
            Desc.serialize_query_info = Macros.Bool(value);
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
            Desc.serialize_query_plan = Macros.Bool(value);
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
            Desc.serialize_query_profile = Macros.Bool(value);
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
            Desc.dont_serialize_results = Macros.Bool(value);
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
            Desc.serialize_alerts = Macros.Bool(value);
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
            Desc.serialize_matches = Macros.Bool(value);
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
