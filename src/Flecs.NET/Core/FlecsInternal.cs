namespace Flecs.NET.Core
{
    public class FlecsInternal
    {
        public static int ResetCount { get; private set; }

        public static void Reset()
        {
            ResetCount++;
        }
    }
}