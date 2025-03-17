using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Base interface for iterable structs.
/// </summary>
public unsafe interface IIterableBase
{
    /// <summary>
    ///     The world.
    /// </summary>
    ecs_world_t* World { get; }

    /// <summary>
    ///     Retrieves an iterator for this iterable object.
    /// </summary>
    /// <param name="world">The world or stage to use with the iterator.</param>
    /// <returns>An iterator.</returns>
    public ecs_iter_t GetIter(World world = default);

    /// <summary>
    ///     Progresses the iterator object and returns true if the iterator has more results.
    /// </summary>
    /// <param name="it">The iterator object.</param>
    /// <returns>True if the iterator has more results.</returns>
    public bool GetNext(Iter it);
}
