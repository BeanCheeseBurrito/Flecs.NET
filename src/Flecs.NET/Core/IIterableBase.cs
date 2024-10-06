using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Base interface for iterable structs.
/// </summary>
public unsafe interface IIterableBase
{
    /// <summary>
    ///     Reference to the world.
    /// </summary>
    public ref ecs_world_t* World { get; }

    /// <summary>
    ///     Iterate a query.
    /// </summary>
    /// <returns>An iterator.</returns>
    public ecs_iter_t GetIter(ecs_world_t* world = null);

    /// <summary>
    ///     Progress iterator.
    /// </summary>
    /// <param name="it">The iterator.</param>
    /// <returns>The result.</returns>
    public bool GetNext(ecs_iter_t* it);
}
