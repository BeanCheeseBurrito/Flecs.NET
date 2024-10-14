using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct UserContext : IDisposable, IEquatable<UserContext>
{
    public GCHandle Object;

    public void Dispose()
    {
        Managed.FreeGcHandle(Object);
    }

    public ref T Get<T>()
    {
        return ref Managed.GetTypeRef<T>(Object);
    }

    public static void Set<T>(ref UserContext dest, ref T value)
    {
        if (dest != default)
            dest.Dispose();

        dest.Object = GCHandle.Alloc(new StrongBox<T>(value));
    }

    public static void Set<T>(ref UserContext dest, T value)
    {
        Set(ref dest, ref value);
    }

    public static UserContext* Alloc<T>(ref T value)
    {
        return Memory.Alloc(new UserContext { Object = GCHandle.Alloc(new StrongBox<T>(value)) });
    }

    public static UserContext* Alloc<T>(T value)
    {
        return Alloc(ref value);
    }

    public bool Equals(UserContext other)
    {
        return Object.Equals(other.Object);
    }

    public override bool Equals(object? obj)
    {
        return obj is UserContext other && Equals(other);
    }

    public static bool operator ==(UserContext left, UserContext right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UserContext left, UserContext right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Object.GetHashCode();
    }
}
