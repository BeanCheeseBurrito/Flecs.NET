using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for <see cref="WorkerIterable"/> types.
/// </summary>
public interface IWorkerIterable
{
    /// <summary>
    ///     The underlying <see cref="WorkerIterable"/> instance.
    /// </summary>
    public ref WorkerIterable Underlying { get; }

    /// <summary>
    ///     The iterator instance.
    /// </summary>
    public ref ecs_iter_t Iterator { get; }

    /// <summary>
    ///     The current thread index for this iterator.
    /// </summary>
    public int ThreadIndex { get; }

    /// <summary>
    ///     The total number of threads to divide entities between.
    /// </summary>
    public int ThreadCount { get; }
}
