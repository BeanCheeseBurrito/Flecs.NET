using System;
using Flecs.NET.Utilities;

namespace Flecs.NET.Core.BindingContext;

internal unsafe struct TypeHooksContext : IDisposable
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

    public static void Free(TypeHooksContext* context)
    {
        if (context == null)
            return;
        context->Dispose();
        Memory.Free(context);
    }

    public static void Free(ref TypeHooksContext context)
    {
        fixed (TypeHooksContext* ptr = &context)
            Free(ptr);
    }
}
