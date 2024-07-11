using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Interface for iterable structs.
    /// </summary>
    public unsafe interface IIterable
    {
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

        /// <summary>
        ///     Progress instanced iterator.
        /// </summary>
        /// <param name="it">The iterator.</param>
        /// <returns>The result.</returns>
        public bool GetNextInstanced(ecs_iter_t* it);

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
        ///     Return number of entities matched by iterable.
        /// </summary>
        /// <returns>The result.</returns>
        public int Count();

        /// <summary>
        ///     Return whether iterable has any matches.
        /// </summary>
        /// <returns>The result.</returns>
        public bool IsTrue();

        /// <summary>
        ///     Return first entity matched by iterable.
        /// </summary>
        /// <returns>The result.</returns>
        public Entity First();

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
}
