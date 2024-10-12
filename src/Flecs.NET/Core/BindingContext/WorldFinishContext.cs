using System;

namespace Flecs.NET.Core.BindingContext;

internal struct WorldFinishContext : IDisposable
{
    public Callback Callback;

    public void Dispose()
    {
        Callback.Dispose();
    }
}
