namespace Flecs.NET.Core.Hooks;

/// <summary>
///     Interface for registering an on add hook callback with the provided type.
/// </summary>
/// <typeparam name="T">The component type.</typeparam>
public interface IOnAddHook<T>
{
    /// <summary>
    ///     The on add hook callback.
    /// </summary>
    /// <param name="it">The iterator.</param>
    /// <param name="i">The entity row.</param>
    /// <param name="data">The reference to the component.</param>
    public static abstract void OnAdd(Iter it, int i, ref T data);

    internal static unsafe class FunctionPointer<TInterface> where TInterface : IOnAddHook<T>
    {
        public static delegate*<Iter, int, ref T, void> Get()
        {
            return &TInterface.OnAdd;
        }
    }
}
