using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_iter_to_json_desc_t"/>.
/// </summary>
public unsafe record struct IterToJsonDesc
{
    /// <summary>
    ///     The underlying <see cref="ecs_iter_to_json_desc_t"/> instance.
    /// </summary>
    public ecs_iter_to_json_desc_t Desc = new()
    {
        serialize_entity_ids =      false,
        serialize_values =          true,
        serialize_builtin =         false,
        serialize_doc =             false,
        serialize_full_paths =      true,
        serialize_fields =          true,
        serialize_inherited =       false,
        serialize_table =           false,
        serialize_type_info =       false,
        serialize_field_info =      false,
        serialize_query_info =      false,
        serialize_query_plan =      false,
        serialize_query_profile =   false,
        dont_serialize_results =    false,
        serialize_alerts =          false,
        serialize_refs =            0,
        serialize_matches =         false,
        query =                     null
    };

    /// <summary>
    ///     Initializes a new instance of <see cref="IterToJsonDesc"/> with the provided description.
    /// </summary>
    /// <param name="desc">The <see cref="ecs_iter_to_json_desc_t"/> instance to wrap.</param>
    public IterToJsonDesc(ecs_iter_to_json_desc_t desc)
    {
        Desc = desc;
    }

    /// <summary>
    ///     Serialize entity ids.
    /// </summary>
    public ref bool SerializeEntityIds => ref Desc.serialize_entity_ids;

    /// <summary>
    ///     Serialize component values.
    /// </summary>
    public ref bool SerializeValues => ref Desc.serialize_values;

    /// <summary>
    ///     Serialize builtin data as components. (e.g. "name", "parent")
    /// </summary>
    public ref bool SerializeBuiltIn => ref Desc.serialize_builtin;

    /// <summary>
    ///     Serialize doc attributes.
    /// </summary>
    public ref bool SerializeDoc => ref Desc.serialize_doc;

    /// <summary>
    ///     Serialize full paths for tags, components and pairs.
    /// </summary>
    public ref bool SerializeFullPaths => ref Desc.serialize_full_paths;

    /// <summary>
    ///     Serialize field data.
    /// </summary>
    public ref bool SerializeFields => ref Desc.serialize_fields;

    /// <summary>
    ///     Serialize inherited components.
    /// </summary>
    public ref bool SerializeInherited => ref Desc.serialize_inherited;

    /// <summary>
    ///     Serialize entire table vs. matched components.
    /// </summary>
    public ref bool SerializeTable => ref Desc.serialize_table;

    /// <summary>
    ///     Serialize type information.
    /// </summary>
    public ref bool SerializeTypeInfo => ref Desc.serialize_type_info;

    /// <summary>
    ///     Serialize metadata for fields returned by query.
    /// </summary>
    public ref bool SerializeFieldInfo => ref Desc.serialize_field_info;

    /// <summary>
    ///     Serialize query terms.
    /// </summary>
    public ref bool SerializeQueryInfo => ref Desc.serialize_query_info;

    /// <summary>
    ///     Serialize query plan.
    /// </summary>
    public ref bool SerializeQueryPlan => ref Desc.serialize_query_plan;

    /// <summary>
    ///     Profile query performance.
    /// </summary>
    public ref bool SerializeQueryProfile => ref Desc.serialize_query_profile;

    /// <summary>
    ///     If true, query won't be evaluated.
    /// </summary>
    public ref bool DontSerializeResults => ref Desc.dont_serialize_results;

    /// <summary>
    ///     Serialize active alerts for entity.
    /// </summary>
    public ref bool SerializeAlerts => ref Desc.serialize_alerts;

    /// <summary>
    ///     Serialize references (incoming edges) for relationship.
    /// </summary>
    public ref ulong SerializeRefs => ref Desc.serialize_refs;

    /// <summary>
    ///     Serialize which queries entity matches with
    /// </summary>
    public ref bool SerializeMatches => ref Desc.serialize_matches;

    /// <summary>
    ///     Query object (required for serialize_query_[plan|profile]).
    /// </summary>
    public ref void* Query => ref Desc.query;

    /// <summary>
    ///     Implicitly converts an <see cref="ecs_iter_to_json_desc_t"/> instance to a <see cref="IterToJsonDesc"/>.
    /// </summary>
    /// <param name="desc">The <see cref="ecs_iter_to_json_desc_t"/> instance to convert.</param>
    /// <returns>A new <see cref="IterToJsonDesc"/> instance initialized with the given description.</returns>
    public static implicit operator IterToJsonDesc(ecs_iter_to_json_desc_t desc)
    {
        return new IterToJsonDesc(desc);
    }

    /// <summary>
    ///     Implicitly converts an <see cref="IterToJsonDesc"/> instance to an <see cref="ecs_iter_to_json_desc_t"/>.
    /// </summary>
    /// <param name="desc">The <see cref="IterToJsonDesc"/> instance to convert.</param>
    /// <returns>A new <see cref="ecs_iter_to_json_desc_t"/> instance initialized with the given description.</returns>
    public static implicit operator ecs_iter_to_json_desc_t(IterToJsonDesc desc)
    {
        return desc.Desc;
    }
}
