using System;

namespace Flecs.NET.Core.BindingContext;

internal struct GroupByContext : IDisposable
{
    public Callback GroupBy;
    public Callback GroupCreate;
    public Callback GroupDelete;

    public void Dispose()
    {
        GroupBy.Dispose();
        GroupCreate.Dispose();
        GroupDelete.Dispose();
    }
}
