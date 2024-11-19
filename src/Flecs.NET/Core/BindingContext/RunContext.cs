using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct RunContext : IDisposable
{
    public Callback Callback;

    public void Dispose()
    {
        Callback.Dispose();
    }

    public static void Free(RunContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref RunContext context)
    {
        fixed (RunContext* ptr = &context)
            Free(ptr);
    }
}
