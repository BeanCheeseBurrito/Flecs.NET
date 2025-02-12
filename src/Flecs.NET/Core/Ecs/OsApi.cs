using System;
using Flecs.NET.Core.BindingContext;
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

        private static void DefaultAbort()
        {
            throw new NativeException("Application aborted from native code.");
        }
    }
}
