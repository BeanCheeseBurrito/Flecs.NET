using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for entity objects.
/// </summary>
public unsafe interface IEntity
{
    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World { get; }

    /// <summary>
    ///     A reference to the id.
    /// </summary>
    ///
    public ref Id Id { get; }

    /// <summary>
    ///     A reference to the entity.
    /// </summary>
    public ref Entity Entity { get; }
}
