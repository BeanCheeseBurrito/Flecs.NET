using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct Callback : IDisposable, IEquatable<Callback>
{
    /// <summary>
    ///     Delegate of user callback.
    /// </summary>
    public GCHandle Delegate;

    /// <summary>
    ///     Pointer of user callback.
    /// </summary>
    public void* Pointer;

    /// <summary>
    ///     Function used to invoke user callback.
    /// </summary>
    public void* Invoker;

    public void Dispose()
    {
        Managed.FreeGcHandle(Delegate);
        Invoker = null;
        Pointer = null;
        Delegate = default;
    }

    internal void Set(InvokerCallback callback, void* invoker)
    {
        Dispose();
        Invoker = invoker;

        if (callback.Pointer == null)
            Delegate = GCHandle.Alloc(callback.Delegate);
        else
            Pointer = callback.Pointer;
    }

    public static implicit operator Delegate(Callback callback)
    {
        return (Delegate)callback.Delegate.Target!;
    }

    public static implicit operator void*(Callback callback)
    {
        return callback.Pointer;
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
        return HashCode.Combine(Delegate, (nint)Pointer, (nint)Invoker);
    }
}
