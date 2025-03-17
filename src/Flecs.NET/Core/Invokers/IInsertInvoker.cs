namespace Flecs.NET.Core.Invokers;

internal unsafe interface IInsertInvoker
{
    static abstract void Invoke(void** pointers, InvokerCallback callback);
}
