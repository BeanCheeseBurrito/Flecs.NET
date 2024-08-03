using System;

namespace Flecs.NET.Core.BindingContext;

internal struct TypeHooksContext : IDisposable
{
    public int Header;

    public Callback Ctor;
    public Callback Dtor;
    public Callback Move;
    public Callback Copy;
    public Callback OnAdd;
    public Callback OnSet;
    public Callback OnRemove;
    public Callback ContextFree;

    public static readonly TypeHooksContext Default = new() { Header = Ecs.Header };

    public void Dispose()
    {
        Ctor.Dispose();
        Dtor.Dispose();
        Move.Dispose();
        Copy.Dispose();
        OnAdd.Dispose();
        OnSet.Dispose();
        OnRemove.Dispose();
        ContextFree.Dispose();
    }
}
