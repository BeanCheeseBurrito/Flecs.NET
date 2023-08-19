using System.Diagnostics;

namespace Flecs.NET.Utilities
{
    public static class Assert
    {
        [Conditional("DEBUG")]
        public static void True(bool condition, string message = "")
        {
            Debug.Assert(condition, message);
        }

        [Conditional("DEBUG")]
        public static void False(bool condition, string message = "")
        {
            Debug.Assert(!condition, message);
        }
    }
}