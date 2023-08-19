#if !NET5_0_OR_GREATER

using System;

namespace Flecs.NET.Polyfill
{
    public class UnmanagedCallersOnlyAttribute : Attribute
    {
    }
}

#endif