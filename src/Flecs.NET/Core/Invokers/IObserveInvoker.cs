namespace Flecs.NET.Core.Invokers;

internal interface IObserveInvoker
{
    static abstract void Invoke(Iter it, InvokerCallback callback);
}

internal unsafe struct Test : IObserveInvoker
{
    public static void Invoke(Iter it, InvokerCallback callback)
    {
        Ecs.ObserveEntityCallback invoke = (Ecs.ObserveEntityCallback)callback;
        invoke(new Entity(it.Handle->world, it.Handle->sources[0]));
    }
}
