namespace Flecs.NET.Core.Invokers;

internal interface IIterInvoker : IJobInvoker
{
    static abstract void Invoke(Iter it, InvokerCallback callback);
}
