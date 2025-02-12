using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

/// <summary>
///     A static class for binding context pointers.
/// </summary>
internal static unsafe class Pointers
{
    static Pointers()
    {
        AppDomain.CurrentDomain.ProcessExit += (object? _, EventArgs _) =>
        {
            Memory.Free(DefaultSeparator);
        };
    }

    #region Native Resources

    internal static readonly byte* DefaultSeparator = (byte*)Marshal.StringToHGlobalAnsi(Ecs.DefaultSeparator);

    #endregion
}
