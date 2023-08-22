#if !NET5_0_OR_GREATER

using System;

namespace Flecs.NET.Polyfill
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class UnmanagedCallersOnlyAttribute : Attribute
    {
    }
}

#endif
