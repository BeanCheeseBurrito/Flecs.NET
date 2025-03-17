using System;

namespace Flecs.NET.Utilities;

internal unsafe struct InvokerCallback
{
    public void* Pointer;
    public Delegate Delegate;

    public static implicit operator InvokerCallback(Delegate callback)
    {
        return new InvokerCallback { Delegate = callback };
    }

    public static implicit operator InvokerCallback(void* callback)
    {
        return new InvokerCallback { Pointer = callback };
    }

    public static implicit operator Delegate(InvokerCallback callback)
    {
        return callback.Delegate;
    }

    public static implicit operator void*(InvokerCallback callback)
    {
        return callback.Pointer;
    }
}
