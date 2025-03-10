namespace Flecs.NET.Core.Hooks;

/// <summary>
///     Interface for registering a ctor hook callback with the provided type.
/// </summary>
/// <typeparam name="T">The component type.</typeparam>
public interface ICtorHook<T>
{
    /// <summary>
    ///     The ctor hook callback.
    /// </summary>
    /// <param name="data">The reference to the component.</param>
    /// <param name="typeInfo">The component's type info.</param>
    public static abstract void Ctor(ref T data, TypeInfo typeInfo);

    internal static unsafe class FunctionPointer<TInterface> where TInterface : ICtorHook<T>
    {
        public static delegate*<ref T, TypeInfo, void> Get()
        {
            return &TInterface.Ctor;
        }
    }
}
