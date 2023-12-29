namespace Flecs.NET.Core
{
    /// <summary>
    ///     Class containing type hook delegates and context.
    /// </summary>
    public unsafe class TypeHooks<T>
    {
        /// <summary>
        ///     Ctor callback delegate.
        /// </summary>
        public Ecs.CtorCallback<T>? Ctor { get; set; }

        /// <summary>
        ///     Dtor callback delegate.
        /// </summary>
        public Ecs.DtorCallback<T>? Dtor { get; set; }

        /// <summary>
        ///     Move callback delegate.
        /// </summary>
        public Ecs.MoveCallback<T>? Move { get; set; }

        /// <summary>
        ///     Copy callback delegate.
        /// </summary>
        public Ecs.CopyCallback<T>? Copy { get; set; }

        /// <summary>
        ///     On add callback delegate.
        /// </summary>
        public Ecs.IterCallback<T>? OnAdd { get; set; }

        /// <summary>
        ///     On set callback delegate.
        /// </summary>
        public Ecs.IterCallback<T>? OnSet { get; set; }

        /// <summary>
        ///     On remove callback delegate.
        /// </summary>
        public Ecs.IterCallback<T>? OnRemove { get; set; }

        /// <summary>
        ///     Context free callback delegate.
        /// </summary>
        public Ecs.ContextFree? ContextFree { get; set; }

        /// <summary>
        ///     Context pointer.
        /// </summary>
        public void* Context { get; set; }
    }
}
