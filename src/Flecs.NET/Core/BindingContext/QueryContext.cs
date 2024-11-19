using System;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct QueryContext : IDisposable
{
    public Callback OrderBy;
    public Callback ContextFree;

    public NativeList<NativeString> Strings;

    public void Dispose()
    {
        OrderBy.Dispose();
        ContextFree.Dispose();

        if (Strings == default)
            return;

        for (int i = 0; i < Strings.Count; i++)
            Strings[i].Dispose();

        Strings.Dispose();
    }

    public static void Free(QueryContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref QueryContext context)
    {
        fixed (QueryContext* ptr = &context)
            Free(ptr);
    }
}
