namespace Flecs.NET.Core;

public static partial class Ecs
{
    /// <summary>
    ///     Determines whether or not to strip the GUID prefix the from beginning of file-local type names when
    ///     registering components. This is will cause name clashing if file-local types in different files
    ///     have the same name. This is primarily used in Flecs.NET.Examples to reduce output noise.
    /// </summary>
    public static bool StripFileLocalTypeNameGuid { get; set; }
}