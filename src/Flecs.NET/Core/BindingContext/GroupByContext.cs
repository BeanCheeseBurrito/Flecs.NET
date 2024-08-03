using System;

namespace Flecs.NET.Core.BindingContext;

internal struct GroupByContext : IDisposable
{
    public Callback GroupBy;

    public void Dispose()
    {
        GroupBy.Dispose();
    }
}
