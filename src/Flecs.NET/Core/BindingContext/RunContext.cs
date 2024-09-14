using System;

namespace Flecs.NET.Core.BindingContext;

internal struct RunContext : IDisposable
{
    public Callback Callback;

    public void Dispose()
    {
        Callback.Dispose();
    }
}
