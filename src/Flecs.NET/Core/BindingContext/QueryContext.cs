using System;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal struct QueryContext : IDisposable
{
    public Callback OrderByAction;
    public Callback GroupByAction;
    public Callback ContextFree;
    public Callback GroupCreateAction;
    public Callback GroupDeleteAction;

    public NativeList<NativeString> Strings;

    public void Dispose()
    {
        OrderByAction.Dispose();
        GroupByAction.Dispose();
        ContextFree.Dispose();
        GroupCreateAction.Dispose();
        GroupDeleteAction.Dispose();

        if (Strings == default)
            return;

        for (int i = 0; i < Strings.Count; i++)
            Strings[i].Dispose();

        Strings.Dispose();
    }
}
