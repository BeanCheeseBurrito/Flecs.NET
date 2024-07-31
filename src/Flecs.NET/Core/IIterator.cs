using System.Diagnostics.CodeAnalysis;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Interface for iterator structs.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
    public unsafe partial interface IIterator : IIterable
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

#if NET5_0_OR_GREATER
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
#endif
    }

    // Iterators
    public unsafe partial interface IIterator : IIterable
    {
        internal static void Iter<T>(ref T iterable, Ecs.IterCallback callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            while (iterable.GetNext(&iter))
                Invoker.Iter(&iter, callback);
        }

        internal static void Each<T>(ref T iterable, Ecs.EachEntityCallback callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            while (iterable.GetNext(&iter))
                Invoker.Each(&iter, callback);
        }

        internal static void Each<T>(ref T iterable, Ecs.EachIterCallback callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            while (iterable.GetNext(&iter))
                Invoker.Each(&iter, callback);
        }

        internal static void Run<T>(ref T iterable, Ecs.RunCallback callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            Invoker.Run(&iter, callback);
        }

#if NET5_0_OR_GREATER
        internal static void Iter<T>(ref T iterable, delegate*<Iter, void> callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            while (iterable.GetNext(&iter))
                Invoker.Iter(&iter, callback);
        }

        internal static void Each<T>(ref T iterable, delegate*<Entity, void> callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            while (iterable.GetNext(&iter))
                Invoker.Each(&iter, callback);
        }

        internal static void Each<T>(ref T iterable, delegate*<Iter, int, void> callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            while (iterable.GetNext(&iter))
                Invoker.Each(&iter, callback);
        }

        internal static void Run<T>(ref T iterable, delegate*<Iter, void> callback) where T : unmanaged, IIterable
        {
            ecs_iter_t iter = iterable.GetIter();
            Invoker.Run(&iter, callback);
        }
#endif
    }
}
