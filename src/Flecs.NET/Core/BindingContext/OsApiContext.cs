using System;

namespace Flecs.NET.Core.BindingContext;

internal struct OsApiContext : IDisposable
{
    public Callback Abort;
    public Callback Log;

    public void Dispose()
    {
        Abort.Dispose();
        Log.Dispose();
    }
}
