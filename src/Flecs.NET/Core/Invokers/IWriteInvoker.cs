namespace Flecs.NET.Core.Invokers;

internal unsafe interface IWriteInvoker
{
    static abstract void Invoke(void** pointers, InvokerCallback callback);
}
