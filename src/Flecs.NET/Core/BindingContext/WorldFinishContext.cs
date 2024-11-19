using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct WorldFinishContext : IDisposable
{
    public Callback Callback;

    public void Dispose()
    {
        Callback.Dispose();
    }

    public static void Free(WorldFinishContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref WorldFinishContext context)
    {
        fixed (WorldFinishContext* ptr = &context)
            Free(ptr);
    }
}
