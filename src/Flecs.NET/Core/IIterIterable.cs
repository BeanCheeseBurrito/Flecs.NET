using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for <see cref="IterIterable"/> types.
/// </summary>
public interface IIterIterable
{
    /// <summary>
    ///     The underlying <see cref="IterIterable"/> instance.
    /// </summary>
    public ref IterIterable Underlying { get; }

    /// <summary>
    ///     The iterator instance.
    /// </summary>
    public ref ecs_iter_t Iterator { get; }

    /// <summary>
    ///     The iterable type.
    /// </summary>
    public IterableType IterableType { get; }
}
