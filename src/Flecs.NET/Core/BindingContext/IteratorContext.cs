using System;

namespace Flecs.NET.Core.BindingContext;

internal struct IteratorContext : IDisposable
{
    public Callback Callback;

    public void Dispose()
    {
        Callback.Dispose();
    }
}
