namespace Flecs.NET.Core.Invokers;

internal interface IRunInvoker : IJobInvoker
{
    static abstract void Invoke(Iter it, InvokerCallback callback);
}
