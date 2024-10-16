using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal struct Callback : IDisposable, IEquatable<Callback>
{
    /// <summary>
    ///     Delegate of user callback.
    /// </summary>
    public GCHandle Delegate;

    /// <summary>
    ///     Pointer of user callback.
    /// </summary>
    public nint Pointer;

    /// <summary>
    ///     Function used to invoke user callback.
    /// </summary>
    public nint Invoker;

    public void Dispose()
    {
        Managed.FreeGcHandle(Delegate);
        Invoker = default;
        Pointer = default;
        Delegate = default;
    }

    internal void Set(nint callback, nint invoker)
    {
        Dispose();
        Invoker = invoker;
        Pointer = callback;
    }

    internal void Set<T>(T callback, nint invoker) where T : Delegate
    {
        Dispose();
        Invoker = invoker;
        Delegate = GCHandle.Alloc(callback);
    }

    public bool Equals(Callback other)
    {
        return Delegate.Equals(other.Delegate) && Pointer == other.Pointer && Invoker == other.Invoker;
    }

    public override bool Equals(object? obj)
    {
        return obj is Callback other && Equals(other);
    }

    public static bool operator ==(Callback left, Callback right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Callback left, Callback right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Delegate, Pointer, Invoker);
    }
}
