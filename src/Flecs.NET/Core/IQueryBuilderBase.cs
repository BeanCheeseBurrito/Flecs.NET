namespace Flecs.NET.Core;

/// <summary>
///     Base interface for query builders.
/// </summary>
public interface IQueryBuilderBase
{
    /// <summary>
    ///     A reference to the query builder.
    /// </summary>
    public ref QueryBuilder QueryBuilder { get; }
}
