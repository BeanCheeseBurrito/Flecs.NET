using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    /// <summary>
    ///     Static class for overriding the os api.
    /// </summary>
    public static class Os
    {
        private static bool _initialized;

        internal static OsApiContext Context;

        /// <summary>
        ///     Determines whether the abort function can be overriden by Flecs.NET. If set to false,
        ///     the abort function provided by flecs will be used. This should be set before the first call
        ///     to World.Create();
        /// </summary>
        public static bool OverrideAbort { get; set; } = true;

        /// <summary>
        ///     Determines whether the log function can be overriden by Flecs.NET. If set to false,
        ///     the log provided by flecs will be used. This should be set before the first call
        ///     to World.Create();
        /// </summary>
        public static bool OverrideLog { get; set; } = true;

        /// <summary>
        ///     Determines whether the trace push function can be overriden by Flecs.NET. If set to false,
        ///     the trace push function provided by flecs will be used. This should be set before the first call
        ///     to World.Create();
        /// </summary>
        public static bool OverrideTracePush { get; set; } = true;

        /// <summary>
        ///     Determines whether the trace pop function can be overriden by Flecs.NET. If set to false,
        ///     the trace pop function provided by flecs will be used. This should be set before the first call
        ///     to World.Create();
        /// </summary>
        public static bool OverrideTracePop { get; set; } = true;

        /// <summary>
        ///     Override the default os api.
        /// </summary>
        internal static void OverrideOsApi()
        {
            if (_initialized)
                return;

            ecs_os_init();

            if (OverrideAbort)
            {
                if (Context.Abort == default)
                    SetAbort(&DefaultAbort);

                ecs_os_api.abort_ = &Functions.AbortCallback;
            }

            if (OverrideLog)
                ecs_os_api.log_ = Context.Log == default ? ecs_os_api.log_ : &Functions.LogCallback;

            if (OverrideTracePush)
                ecs_os_api.perf_trace_push_ = Context.TracePush == default ? ecs_os_api.perf_trace_push_ : &Functions.TracePushCallback;

            if (OverrideTracePop)
                ecs_os_api.perf_trace_pop_ = Context.TracePop == default ? ecs_os_api.perf_trace_pop_ : &Functions.TracePopCallback;

            _initialized = true;
        }

        /// <summary>
        ///     Sets os api abort callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetAbort(Action callback)
        {
            Context.Abort.Set(callback, (delegate*<void>)&Functions.AbortCallbackDelegate);
        }

        /// <summary>
        ///     Sets os api abort callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetAbort(delegate*<void> callback)
        {
            Context.Abort.Set(callback, (delegate*<void>)&Functions.AbortCallbackPointer);
        }

        /// <summary>
        ///     Sets the os api log callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetLog(LogCallback callback)
        {
            Context.Log.Set(callback, (delegate*<int, byte*, int, byte*, void>)&Functions.LogCallbackDelegate);
        }

        /// <summary>
        ///     Sets the os api log callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetLog(delegate*<int, string, int, string, void> callback)
        {
            Context.Log.Set(callback, (delegate*<int, byte*, int, byte*, void>)&Functions.LogCallbackPointer);
        }

        /// <summary>
        ///     Sets the os api trace push callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetTracePush(TraceCallback callback)
        {
            Context.TracePush.Set(callback, (delegate*<byte*, ulong, byte*, void>)&Functions.TracePushCallbackDelegate);
        }

        /// <summary>
        ///     Sets the os api trace push callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetTracePush(delegate*<string, ulong, string, void> callback)
        {
            Context.TracePush.Set(callback, (delegate*<byte*, ulong, byte*, void>)&Functions.TracePushCallbackPointer);
        }

        /// <summary>
        ///     Sets the os api trace pop callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetTracePop(TraceCallback callback)
        {
            Context.TracePop.Set(callback, (delegate*<byte*, ulong, byte*, void>)&Functions.TracePopCallbackDelegate);
        }

        /// <summary>
        ///     Sets the os api trace pop callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void SetTracePop(delegate*<string, ulong, string, void> callback)
        {
            Context.TracePop.Set(callback, (delegate*<byte*, ulong, byte*, void>)&Functions.TracePopCallbackPointer);
        }

        private static void DefaultAbort()
        {
            int error = ecs_log_last_error();
            string description = error != 0
                ? NativeString.GetString(ecs_strerror(error))
                : "no error code was logged";

            Environment.FailFast(
                $"Application aborted from native code. " +
                $"Flecs error code: {error} ({description}).");
        }
    }
}
