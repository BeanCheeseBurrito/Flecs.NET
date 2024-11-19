using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct ObserverContext : IDisposable
{
    public UserContext UserContext;

    public void Dispose()
    {
        UserContext.Dispose();
    }

    public static void Free(ObserverContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref ObserverContext context)
    {
        fixed (ObserverContext* ptr = &context)
            Free(ptr);
    }
}
