using System.Diagnostics;

namespace Flecs.NET.Utilities
{
    /// <summary>
    ///     Static class for assertions.
    /// </summary>
    public static class Assert
    {
        /// <summary>
        ///     Asserts that a condition is true.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        [Conditional("DEBUG")]
        public static void True(bool condition, string message = "")
        {
            Debug.Assert(condition, "[Flecs.NET Assertion]: " + message);
        }

        /// <summary>
        ///     Asserts that a condition is false.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        [Conditional("DEBUG")]
        public static void False(bool condition, string message = "")
        {
            Debug.Assert(!condition, message);
        }
    }
}
