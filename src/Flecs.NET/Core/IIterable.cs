using System.Diagnostics.CodeAnalysis;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for iterable structs.
/// </summary>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public unsafe interface IIterable : IIterableBase
{
    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Iter(Ecs.IterCallback callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Each(Ecs.EachEntityCallback callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Each(Ecs.EachIterCallback callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Run(Ecs.RunCallback callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Iter(delegate*<Iter, void> callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Each(delegate*<Entity, void> callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Each(delegate*<Iter, int, void> callback);

    /// <summary>
    ///     Iterates the query using the provided callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public void Run(delegate*<Iter, void> callback);

    /// <summary>
    ///     Create an iterator object that can be modified before iterating.
    /// </summary>
    /// <returns>An iterable iter.</returns>
    public IterIterable Iter(World world = default);

    /// <summary>
    ///     Create an iterator object that can be modified before iterating.
    /// </summary>
    /// <returns>An iterable iter.</returns>
    public IterIterable Iter(Iter it);

    /// <summary>
    ///     Create an iterator object that can be modified before iterating.
    /// </summary>
    /// <returns>An iterable iter.</returns>
    public IterIterable Iter(Entity entity);

    /// <summary>
    ///     Create an iterator that limits the returned entities with offset/limit.
    /// </summary>
    /// <param name="offset">The number of entities to skip.</param>
    /// <param name="limit">The maximum number of entities to return.</param>
    /// <returns>Iterable that can be iterated with Each/Iter.</returns>
    public PageIterable Page(int offset, int limit);

    /// <summary>
    ///     Create an iterator that divides the number of matched entities across a number of resources.
    /// </summary>
    /// <param name="index">The index of the current resource.</param>
    /// <param name="count">The total number of resources to divide entities between.</param>
    /// <returns>Iterable that can be iterated with Each/Iter.</returns>
    public WorkerIterable Worker(int index, int count);

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="varId">The variable id.</param>
    /// <param name="value">The entity variable value.</param>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetVar(int varId, ulong value);

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The entity variable value.</param>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetVar(string name, ulong value);

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The table variable value.</param>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetVar(string name, ecs_table_t* value);

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The table variable value.</param>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetVar(string name, ecs_table_range_t value);

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The table variable value.</param>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetVar(string name, Table value);

    /// <summary>
    ///     Limit results to tables with specified group id (grouped queries only)
    /// </summary>
    /// <param name="groupId">The group id.</param>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetGroup(ulong groupId);

    /// <summary>
    ///     Limit results to tables with specified group id (grouped queries only)
    /// </summary>
    /// <typeparam name="T">The group type.</typeparam>
    /// <returns>An iterable iter.</returns>
    public IterIterable SetGroup<T>();
}
