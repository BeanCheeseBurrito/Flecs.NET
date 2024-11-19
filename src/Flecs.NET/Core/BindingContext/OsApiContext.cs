using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct OsApiContext : IDisposable
{
    public Callback Abort;
    public Callback Log;

    public void Dispose()
    {
        Abort.Dispose();
        Log.Dispose();
    }

    public static void Free(OsApiContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref OsApiContext context)
    {
        fixed (OsApiContext* ptr = &context)
            Free(ptr);
    }
}
