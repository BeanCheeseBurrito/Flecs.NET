using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Base interface for iterable structs.
    /// </summary>
    public unsafe interface IIterableBase
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
    }
}
