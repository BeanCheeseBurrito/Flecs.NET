using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

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
        public static IterToJsonDesc Default => default(IterToJsonDesc)
            .TermIds()
            .Ids()
            .Sources()
            .Variables()
            .IsSet()
            .Values()
            .Entities();

        /// <summary>
        ///     Serialize query term component ids.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc TermIds(bool value = true)
        {
            Desc.serialize_term_ids = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize query term component id labels.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc TermLabels(bool value = true)
        {
            Desc.serialize_term_labels = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize actual (matched) component ids.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Ids(bool value = true)
        {
            Desc.serialize_ids = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize actual (matched) component id labels.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc IdLabels(bool value = true)
        {
            Desc.serialize_id_labels = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize sources.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Sources(bool value = true)
        {
            Desc.serialize_sources = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize variables.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Variables(bool value = true)
        {
            Desc.serialize_variables = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize IsSet. (for optional terms)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc IsSet(bool value = true)
        {
            Desc.serialize_is_set = Macros.Bool(value);
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
        ///     Serialize private component values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Private(bool value = true)
        {
            Desc.serialize_private = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize entities. (for This terms)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Entities(bool value = true)
        {
            Desc.serialize_entities = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc name for entities.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc EntityLabels(bool value = true)
        {
            Desc.serialize_entity_labels = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize numerical ids for entities.
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
        ///     Serialize names (not paths) for entities.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc EntityNames(bool value = true)
        {
            Desc.serialize_entity_names = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc name for variables.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc VariableLabels(bool value = true)
        {
            Desc.serialize_variable_labels = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize numerical ids for variables.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc VariableIds(bool value = true)
        {
            Desc.serialize_variable_ids = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize doc color for entities.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc Colors(bool value = true)
        {
            Desc.serialize_colors = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Serialize evaluation duration.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref IterToJsonDesc MeasureEvalDuration(bool value = true)
        {
            Desc.measure_eval_duration = Macros.Bool(value);
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
