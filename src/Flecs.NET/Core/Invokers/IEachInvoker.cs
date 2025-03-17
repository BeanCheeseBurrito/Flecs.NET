namespace Flecs.NET.Core.Invokers;

internal interface IEachInvoker : IJobInvoker
{
    static abstract void Invoke<TFieldGetter>(in Fields fieldData, int count, InvokerCallback callback) where TFieldGetter : IFieldGetter;
}
