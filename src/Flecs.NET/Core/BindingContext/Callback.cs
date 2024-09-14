using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal struct Callback : IDisposable
{
    public IntPtr Pointer;
    public GCHandle GcHandle;

    public Callback(IntPtr function, GCHandle gcHandle)
    {
        Pointer = function;
        GcHandle = gcHandle;
    }

    public void Dispose()
    {
        Managed.FreeGcHandle(GcHandle);
        Pointer = default;
        GcHandle = default;
    }

    internal static Callback Allocate<T>(T? callback, bool storePtr = true) where T : Delegate
    {
        if (callback == null)
            return default;

        IntPtr funcPtr = storePtr ? Marshal.GetFunctionPointerForDelegate(callback) : IntPtr.Zero;
        return new Callback(funcPtr, GCHandle.Alloc(callback));
    }

    internal static void Set(ref Callback dest, IntPtr callback)
    {
        if (dest.GcHandle != default)
            dest.Dispose();

        dest.Pointer = callback;
    }

    internal static void Set<T>(ref Callback dest, T? callback, bool storePtr = true) where T : Delegate
    {
        if (dest.GcHandle != default)
            dest.Dispose();

        dest = Allocate(callback, storePtr);
    }
}
