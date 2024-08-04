using System;
using Flecs.NET.Collections;

namespace Flecs.NET.Core.BindingContext;

internal struct WorldContext : IDisposable
{
    public Callback AtFini;
    public Callback RunPostFrame;
    public Callback ContextFree;
    public NativeList<ulong> TypeCache;

    public void Dispose()
    {
        AtFini.Dispose();
        RunPostFrame.Dispose();
        ContextFree.Dispose();
        TypeCache.Dispose();
    }
}
