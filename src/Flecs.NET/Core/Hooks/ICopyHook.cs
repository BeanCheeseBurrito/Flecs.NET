namespace Flecs.NET.Core.Hooks;

/// <summary>
///     Interface for registering a copy hook callback with the provided type.
/// </summary>
/// <typeparam name="T">The component type.</typeparam>
public interface ICopyHook<T>
{
    /// <summary>
    ///     The copy hook callback
    /// </summary>
    /// <param name="dst">The reference to the component's copy destination.</param>
    /// <param name="src">The reference to the component's copy source.</param>
    /// <param name="typeInfo">The component's type info.</param>
    public static abstract void Copy(ref T dst, ref T src, TypeInfo typeInfo);

    internal static unsafe class FunctionPointer<TInterface> where TInterface : ICopyHook<T>
    {
        public static delegate*<ref T, ref T, TypeInfo, void> Get()
        {
            return &TInterface.Copy;
        }
    }
}
