using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct GroupByContext : IDisposable
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

    public static void Free(GroupByContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref GroupByContext context)
    {
        fixed (GroupByContext* ptr = &context)
            Free(ptr);
    }
}
