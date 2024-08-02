using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Static class for overriding the os api.
        /// </summary>
        public static class Os
        {
            internal static BindingContext.OsApiContext Context = new BindingContext.OsApiContext
            {
                Abort = new BindingContext.Callback(BindingContext.OsApiAbortPointer, default)
            };

            internal static bool IsOsApiOverridden { get; private set; }

            /// <summary>
            ///     Override the default os api abort function to log C# stack traces.
            /// </summary>
            internal static void OverrideOsApi()
            {
                if (IsOsApiOverridden)
                    return;

                ecs_set_os_api_impl();
                ecs_os_init();

                ecs_os_api.abort_ = Context.Abort.Pointer == IntPtr.Zero ? ecs_os_api.abort_ : Context.Abort.Pointer;
                ecs_os_api.log_ = Context.Log.Pointer == IntPtr.Zero ? ecs_os_api.log_ : Context.Log.Pointer;

                IsOsApiOverridden = true;
            }

            /// <summary>
            ///     Sets os api abort callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            public static void SetAbort(Action callback)
            {
                BindingContext.SetCallback(ref Context.Abort, callback, true);
            }

            /// <summary>
            ///     Sets the os api log callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            public static void SetLog(OsApiLog callback)
            {
                BindingContext.SetCallback(ref Context.Log, callback, true);
            }

            /// <summary>
            ///     Sets os api abort callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            public static void SetAbort(IntPtr callback)
            {
                BindingContext.SetCallback(ref Context.Abort, callback);
            }

            /// <summary>
            ///     Sets the os api log callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            public static void SetLog(IntPtr callback)
            {
                BindingContext.SetCallback(ref Context.Log, callback);
            }

            /// <summary>
            ///     Sets os api abort callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            public static void SetAbort(delegate*<void> callback)
            {
                BindingContext.SetCallback(ref Context.Abort, (IntPtr)callback);
            }

            /// <summary>
            ///     Sets the os api log callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            public static void SetLog(delegate*<int, byte*, int, byte*, void> callback)
            {
                BindingContext.SetCallback(ref Context.Log, (IntPtr)callback);
            }
        }
    }
}
