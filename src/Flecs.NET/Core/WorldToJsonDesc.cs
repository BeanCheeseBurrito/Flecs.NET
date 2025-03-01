using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_world_to_json_desc_t"/>.
/// </summary>
public unsafe record struct WorldToJsonDesc
{
    /// <summary>
    ///     The underlying <see cref="ecs_world_to_json_desc_t"/> instance.
    /// </summary>
    public ecs_world_to_json_desc_t Desc;

    /// <summary>
    ///     Initializes a new instance of <see cref="WorldToJsonDesc"/> with the provided description.
    /// </summary>
    /// <param name="desc">The <see cref="ecs_world_to_json_desc_t"/> instance to wrap.</param>
    public WorldToJsonDesc(ecs_world_to_json_desc_t desc)
    {
        Desc = desc;
    }

    /// <summary>
    ///     Whether to include built-in flecs modules and contents.
    /// </summary>
    public ref bool BuiltIn => ref Desc.serialize_builtin;

    /// <summary>
    ///     Whether to include modules and contents.
    /// </summary>
    public ref bool Modules => ref Desc.serialize_modules;

    /// <summary>
    ///     Implicitly converts an <see cref="ecs_world_to_json_desc_t"/> instance to a <see cref="WorldToJsonDesc"/>.
    /// </summary>
    /// <param name="desc">The <see cref="ecs_world_to_json_desc_t"/> instance to convert.</param>
    /// <returns>A new <see cref="WorldToJsonDesc"/> instance initialized with the given description.</returns>
    public static implicit operator WorldToJsonDesc(ecs_world_to_json_desc_t desc)
    {
        return new WorldToJsonDesc(desc);
    }

    /// <summary>
    ///     Implicitly converts a <see cref="WorldToJsonDesc"/> instance to an <see cref="ecs_world_to_json_desc_t"/>.
    /// </summary>
    /// <param name="desc">The <see cref="WorldToJsonDesc"/> instance to convert.</param>
    /// <returns>A new <see cref="ecs_world_to_json_desc_t"/> instance initialized with the given description.</returns>
    public static implicit operator ecs_world_to_json_desc_t(WorldToJsonDesc desc)
    {
        return desc.Desc;
    }
}
