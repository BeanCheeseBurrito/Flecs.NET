using System;

namespace Flecs.NET.Core.BindingContext;

internal struct PostFrameContext : IDisposable
{
    public Callback Callback;

    public void Dispose()
    {
        Callback.Dispose();
    }
}
