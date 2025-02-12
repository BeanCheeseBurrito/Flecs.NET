using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    /// <summary>
    ///     Static class for flecs logging functions.
    /// </summary>
    public static class Log
    {
        /// <summary>
        ///     Set log level.
        /// </summary>
        /// <param name="level"></param>
        public static void SetLevel(int level)
        {
            _ = ecs_log_set_level(level);
        }

        /// <summary>
        ///     Get log level.
        /// </summary>
        /// <returns></returns>
        public static int GetLevel()
        {
            return ecs_log_get_level();
        }

        /// <summary>
        ///     Enable colors in logging.
        /// </summary>
        /// <param name="enabled"></param>
        public static void EnableColors(bool enabled = true)
        {
            ecs_log_enable_colors(enabled);
        }

        /// <summary>
        ///     Enable timestamps in logging.
        /// </summary>
        /// <param name="enabled"></param>
        public static void EnableTimestamp(bool enabled = true)
        {
            ecs_log_enable_timestamp(enabled);
        }

        /// <summary>
        ///     Enable time delta in logging.
        /// </summary>
        /// <param name="enabled"></param>
        public static void EnableTimeDelta(bool enabled = true)
        {
            ecs_log_enable_timedelta(enabled);
        }

        /// <summary>
        ///     Debug trace (Level 1)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        public static void Dbg(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            using NativeString nativeMessage = (NativeString)message;
            using NativeString nativeFile = (NativeString)file;
            ecs_log_(1, nativeFile, line, nativeMessage);
        }

        /// <summary>
        ///     Trace (Level 0)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        public static void Trace(string message = "", [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            using NativeString nativeMessage = (NativeString)message;
            using NativeString nativeFile = (NativeString)file;
            ecs_log_(0, nativeFile, line, nativeMessage);
        }

        /// <summary>
        ///     Trace (Level -2)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        public static void Warn(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            using NativeString nativeMessage = (NativeString)message;
            using NativeString nativeFile = (NativeString)file;
            ecs_log_(-2, nativeFile, line, nativeMessage);
        }

        /// <summary>
        ///     Trace (Level -3)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        public static void Err(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            using NativeString nativeMessage = (NativeString)message;
            using NativeString nativeFile = (NativeString)file;
            ecs_log_(-3, nativeFile, line, nativeMessage);
        }

        /// <summary>
        ///     Trace (Level 0)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        public static void Push(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            using NativeString nativeMessage = (NativeString)message;
            using NativeString nativeFile = (NativeString)file;
            ecs_log_(1, nativeFile, line, nativeMessage);
            ecs_log_push_(0);
        }

        /// <summary>
        ///     Increase log indentation.
        /// </summary>
        public static void Push()
        {
            ecs_log_push_(0);
        }

        /// <summary>
        ///     Decrease log indentation.
        /// </summary>
        public static void Pop()
        {
            ecs_log_pop_(0);
        }
    }
}
