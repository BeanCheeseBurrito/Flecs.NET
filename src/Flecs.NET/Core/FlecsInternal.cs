namespace Flecs.NET.Core
{
    public class FlecsInternal
    {
        /// <summary>
        /// Current reset count.
        /// </summary>
        public static int ResetCount { get; private set; }

        /// <summary>
        /// Resets all type ids.
        /// </summary>
        public static void Reset()
        {
            ResetCount++;
        }
    }
}
