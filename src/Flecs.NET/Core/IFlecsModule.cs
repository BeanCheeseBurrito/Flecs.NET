namespace Flecs.NET.Core
{
    /// <summary>
    /// Interface for flecs modules.
    /// </summary>
    public interface IFlecsModule
    {
        /// <summary>
        /// Register entities, components, and systems.
        /// </summary>
        /// <param name="world"></param>
        public void InitModule(ref World world);
    }
}
