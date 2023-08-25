namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for holding flecs internals.
    /// </summary>
    public class FlecsInternal
    {
        /// <summary>
        ///     Current reset count.
        /// </summary>
        public static int ResetCount { get; private set; }

        /// <summary>
        ///     Resets all type ids.
        /// </summary>
        public static void Reset()
        {
            ResetCount++;
        }
    }
}
