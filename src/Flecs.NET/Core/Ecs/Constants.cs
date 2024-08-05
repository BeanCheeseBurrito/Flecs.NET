using Flecs.NET.Bindings;

namespace Flecs.NET.Core;

public static partial class Ecs
{
    /// <summary>
    ///     Default path separator.
    /// </summary>
    public const string DefaultSeparator = ".";

    /// <summary>
    ///     Native Flecs namespace.
    /// </summary>
    public static readonly string NativeNamespace = $"{nameof(Flecs)}.{nameof(NET)}.{nameof(Bindings)}.{nameof(flecs)}+";

    /// <summary>
    ///     Number that identifies a Flecs.NET binding context object.
    /// </summary>
    public const int Header = 0x2E6E6574;
}