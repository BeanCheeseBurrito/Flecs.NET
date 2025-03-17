using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for <see cref="Query"/> types.
/// </summary>
public unsafe interface IQuery
{
    /// <summary>
    ///     The underlying <see cref="Query"/> instance.
    /// </summary>
    public ref Query Underlying { get; }

    /// <summary>
    ///     The underlying <see cref="ecs_query_t"/>* handle.
    /// </summary>
    public ecs_query_t* Handle { get; }
}
