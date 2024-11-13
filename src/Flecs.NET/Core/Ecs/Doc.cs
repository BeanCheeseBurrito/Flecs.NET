using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static partial class Ecs
{
    /// <summary>
    ///     Utilities for documenting entities, components and systems.
    /// </summary>
    [SuppressMessage("Naming", "CA1721:Property names should not match get methods")]
    public class Doc : IFlecsModule
    {
        /// <summary>
        ///     Init doc module.
        /// </summary>
        /// <param name="world"></param>
        public void InitModule(World world)
        {

        }

        /// <summary>
        ///     Reference to <see cref="EcsDocUuid"/>.
        /// </summary>
        public static ref ulong Uuid => ref EcsDocUuid;

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

        /// <summary>
        ///     Get doc uuid for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The uuid string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetUuid(Entity entity)
        {
            return entity.DocUuid();
        }

        /// <summary>
        ///     Get human-readable name for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The uuid string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetName(Entity entity)
        {
            return entity.DocName();
        }

        /// <summary>
        ///     Get brief description for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The brief string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetBrief(Entity entity)
        {
            return entity.DocBrief();
        }

        /// <summary>
        ///     Get detailed description for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The detail string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetDetail(Entity entity)
        {
            return entity.DocDetail();
        }

        /// <summary>
        ///     Get link to external documentation for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The link string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLink(Entity entity)
        {
            return entity.DocLink();
        }

        /// <summary>
        ///     Get color for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The color string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetColor(Entity entity)
        {
            return entity.DocColor();
        }

        /// <summary>
        ///     Set uuid for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="uuid">The uuid string.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetUuid(Entity entity, string uuid)
        {
            entity.SetDocUuid(uuid);
        }

        /// <summary>
        ///     Set human-readable name for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="name">The name string.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetName(Entity entity, string name)
        {
            entity.SetDocName(name);
        }

        /// <summary>
        ///     Set brief description for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="brief">The brief string</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBrief(Entity entity, string brief)
        {
            entity.SetDocBrief(brief);
        }

        /// <summary>
        ///     Set detailed description for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="detail">The detail string.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDetail(Entity entity, string detail)
        {
            entity.SetDocDetail(detail);
        }

        /// <summary>
        ///     Set link to external documentation for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="link">The link string.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLink(Entity entity, string link)
        {
            entity.SetDocLink(link);
        }

        /// <summary>
        ///     Set color for an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="color">The color string.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColor(Entity entity, string color)
        {
            entity.SetDocColor(color);
        }
    }
}
