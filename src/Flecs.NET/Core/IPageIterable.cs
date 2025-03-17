using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for <see cref="PageIterable"/> types.
/// </summary>
public interface IPageIterable
{
    /// <summary>
    ///     The underlying <see cref="PageIterable"/> instance.
    /// </summary>
    public ref PageIterable Underlying { get; }

    /// <summary>
    ///     The iterator instance.
    /// </summary>
    public ref ecs_iter_t Iterator { get; }

    /// <summary>
    ///     The number of entities to skip.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    ///     The max number of entities to return.
    /// </summary>
    public int Limit { get; }
}
