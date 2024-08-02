using System.Diagnostics.CodeAnalysis;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Interface for iterator structs.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
    public unsafe interface IIterator : IIterable
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
    }
}
