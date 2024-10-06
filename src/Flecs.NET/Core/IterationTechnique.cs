using System;

namespace Flecs.NET.Core;

[Flags]
internal enum IterationTechnique
{
    /// <summary>
    ///     Indicates default enum value.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Indicates that query contains shared components.
    /// </summary>
    Shared = 1,

    /// <summary>
    ///     Indicates that query contains sparse components.
    /// </summary>
    Sparse = 2
}
