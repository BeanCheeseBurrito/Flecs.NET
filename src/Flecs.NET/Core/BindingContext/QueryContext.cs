using System;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal struct QueryContext : IDisposable
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
}
