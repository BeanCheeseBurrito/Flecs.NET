using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct SystemContext : IDisposable
{
    public UserContext UserContext;

    public void Dispose()
    {
        UserContext.Dispose();
    }

    public static void Free(SystemContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref SystemContext context)
    {
        fixed (SystemContext* ptr = &context)
            Free(ptr);
    }
}
