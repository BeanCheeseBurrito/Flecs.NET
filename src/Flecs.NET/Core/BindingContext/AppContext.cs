using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct AppContext : IDisposable
{
    public Callback Init;

    public void Dispose()
    {
        Init.Dispose();
    }

    public static void Free(AppContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref AppContext context)
    {
        fixed (AppContext* ptr = &context)
            Free(ptr);
    }
}
