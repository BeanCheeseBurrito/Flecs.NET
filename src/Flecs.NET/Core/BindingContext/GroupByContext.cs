using System;

namespace Flecs.NET.Core.BindingContext;

internal struct GroupByContext : IDisposable
{
    public Callback GroupBy;
    public Callback GroupCreate;
    public Callback GroupDelete;
    public UserContext GroupByUserContext;

    public void Dispose()
    {
        GroupBy.Dispose();
        GroupCreate.Dispose();
        GroupDelete.Dispose();
        GroupByUserContext.Dispose();
    }
}
