namespace Flecs.NET.Core.Invokers;

internal unsafe interface IReadInvoker
{
    static abstract void Invoke(void** pointers, InvokerCallback callback);
}
