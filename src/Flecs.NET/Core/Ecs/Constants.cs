using Flecs.NET.Bindings;

namespace Flecs.NET.Core
{
    public static partial class Ecs
    {
        /// <summary>
        ///     Default path separator.
        /// </summary>
        public const string DefaultSeparator = ".";

        /// <summary>
        ///     Default path root.
        /// </summary>
        public const string DefaultRootSeparator = "::";

        /// <summary>
        ///     Native Flecs namespace.
        /// </summary>
        public static readonly string NativeNamespace = $"{nameof(Flecs)}.{nameof(NET)}.{nameof(Bindings)}.{nameof(Native)}+";
    }
}
