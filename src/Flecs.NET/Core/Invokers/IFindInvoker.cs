namespace Flecs.NET.Core.Invokers;

internal interface IFindInvoker
{
    static abstract Entity Invoke<TFieldGetter>(in Fields fieldData, int count, InvokerCallback callback) where TFieldGetter : IFieldGetter;
}
