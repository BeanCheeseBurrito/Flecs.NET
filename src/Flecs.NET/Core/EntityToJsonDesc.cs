using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_entity_to_json_desc_t"/>.
/// </summary>
public unsafe record struct EntityToJsonDesc()
{
    /// <summary>
    ///     The underlying <see cref="ecs_entity_to_json_desc_t"/> instance.
    /// </summary>
    public ecs_entity_to_json_desc_t Desc = new()
    {
        serialize_entity_id =  false,
        serialize_doc =        false,
        serialize_full_paths = true,
        serialize_inherited =  false,
        serialize_values =     true,
        serialize_builtin =    false,
        serialize_type_info =  false,
        serialize_alerts =     false,
        serialize_refs =       0,
        serialize_matches =    false
    };

    /// <summary>
    ///     Initializes a new instance of <see cref="EntityToJsonDesc"/> with the provided description.
    /// </summary>
    /// <param name="desc">The <see cref="ecs_entity_to_json_desc_t"/> instance to wrap.</param>
    public EntityToJsonDesc(ecs_entity_to_json_desc_t desc) : this()
    {
        Desc = desc;
    }

    /// <summary>
    ///     Serialize entity id.
    /// </summary>
    public ref bool SerializeEntityId => ref Desc.serialize_entity_id;

    /// <summary>
    ///     Serialize doc attributes.
    /// </summary>
    public ref bool SerializeDoc => ref Desc.serialize_doc;

    /// <summary>
    ///     Serialize full paths for tags, components and pairs.
    /// </summary>
    public ref bool SerializeFullPaths => ref Desc.serialize_full_paths;

    /// <summary>
    ///     Serialize base components.
    /// </summary>
    public ref bool SerializeInherited => ref Desc.serialize_inherited;

    /// <summary>
    ///     Serialize component values.
    /// </summary>
    public ref bool SerializeValues => ref Desc.serialize_values;

    /// <summary>
    ///     Serialize built-in data as components. (e.g. "name", "parent")
    /// </summary>
    public ref bool SerializeBuiltIn => ref Desc.serialize_builtin;

    /// <summary>
    ///     Serialize type info. (requires serialize_values)
    /// </summary>
    public ref bool SerializeTypeInfo => ref Desc.serialize_type_info;

    /// <summary>
    ///     Serialize active alerts for entity.
    /// </summary>
    public ref bool SerializeAlerts => ref Desc.serialize_alerts;

    /// <summary>
    ///     Serialize references (incoming edges) for relationship.
    /// </summary>
    public ref ulong SerializeRefs => ref Desc.serialize_refs;

    /// <summary>
    ///     Serialize which queries entity matches with.
    /// </summary>
    public ref bool SerializeMatches => ref Desc.serialize_matches;

    /// <summary>
    ///     Implicitly converts an <see cref="ecs_entity_to_json_desc_t"/> instance to a <see cref="EntityToJsonDesc"/>.
    /// </summary>
    /// <param name="desc">The <see cref="ecs_entity_to_json_desc_t"/> instance to convert.</param>
    /// <returns>A new <see cref="EntityToJsonDesc"/> instance initialized with the given description.</returns>
    public static implicit operator EntityToJsonDesc(ecs_entity_to_json_desc_t desc)
    {
        return new EntityToJsonDesc(desc);
    }

    /// <summary>
    ///     Implicitly converts an <see cref="EntityToJsonDesc"/> instance to an <see cref="ecs_entity_to_json_desc_t"/>.
    /// </summary>
    /// <param name="desc">The <see cref="EntityToJsonDesc"/> instance to convert.</param>
    /// <returns>A new <see cref="ecs_entity_to_json_desc_t"/> instance initialized with the given description.</returns>
    public static implicit operator ecs_entity_to_json_desc_t(EntityToJsonDesc desc)
    {
        return desc.Desc;
    }
}
