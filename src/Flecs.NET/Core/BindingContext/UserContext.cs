using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct UserContext : IDisposable, IEquatable<UserContext>
{
    public GCHandle Object; // User context object
    public Callback Callback; // User context finish callback

    public void Dispose()
    {
        if (Callback != default)
            ((delegate*<ref UserContext, void>)Callback.Invoker)(ref this);

        Managed.FreeGcHandle(Object);
        Callback.Dispose();

        Object = default;
        Callback = default;
    }

    public ref T Get<T>()
    {
        Ecs.Assert(Object.IsAllocated, "User context object is empty.");
        Ecs.Assert(Object.Target is StrongBox<T>, "User context type does not match the given type argument 'T'.");
        return ref Managed.GetTypeRef<T>(Object);
    }

    public void Set<T>(ref T value)
    {
        Dispose();
        Object = GCHandle.Alloc(new StrongBox<T>(value));
    }

    public void Set<T>(ref T value, Ecs.UserContextFinish<T> callback)
    {
        Dispose();
        Object = GCHandle.Alloc(new StrongBox<T>(value));
        Callback.Set(callback, (delegate*<ref UserContext, void>)&Functions.UserContextFinishDelegate<T>);
    }

    public void Set<T>(ref T value, delegate*<ref T, void> callback)
    {
        Dispose();
        Object = GCHandle.Alloc(new StrongBox<T>(value));
        Callback.Set(callback, (delegate*<ref UserContext, void>)&Functions.UserContextFinishPointer<T>);
    }

    public void Set<T>(T value)
    {
        Set(ref value);
    }

    public void Set<T>(T value, Ecs.UserContextFinish<T> callback)
    {
        Set(ref value, callback);
    }

    public void Set<T>(T value, delegate*<ref T, void> callback)
    {
        Set(ref value, callback);
    }

    public static UserContext* Alloc<T>(ref T value)
    {
        return Memory.AllocZeroed(new UserContext { Object = GCHandle.Alloc(new StrongBox<T>(value)) });
    }

    public static UserContext* Alloc<T>(T value)
    {
        return Alloc(ref value);
    }

    public static void Free(UserContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref UserContext context)
    {
        fixed (UserContext* ptr = &context)
            Free(ptr);
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
