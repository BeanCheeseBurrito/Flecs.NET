using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static partial class Ecs
{
    /// <summary>
    ///     Utilities for documenting entities, components and systems.
    /// </summary>
    public static class Doc
    {
        /// <summary>
        ///     Reference to <see cref="EcsDocBrief"/>.
        /// </summary>
        public static ref ulong Brief => ref EcsDocBrief;

        /// <summary>
        ///     Reference to <see cref="EcsDocDetail"/>.
        /// </summary>
        public static ref ulong Detail => ref EcsDocDetail;

        /// <summary>
        ///     Reference to <see cref="EcsDocLink"/>.
        /// </summary>
        public static ref ulong Link => ref EcsDocLink;

        /// <summary>
        ///     Reference to <see cref="EcsDocColor"/>.
        /// </summary>
        public static ref ulong Color => ref EcsDocColor;
    }
}