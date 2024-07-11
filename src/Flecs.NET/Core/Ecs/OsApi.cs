using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    public static partial class Ecs
    {
        /// <summary>
        ///     Original os api abort function pointer if overridden.
        /// </summary>
        public static IntPtr OsAbortNative { get; private set; }

        /// <summary>
        ///     Whether or not os api is overridden.
        /// </summary>
        public static bool IsOsApiOverridden { get; private set; }

        /// <summary>
        ///     Override the default os api abort function to log C# stack traces.
        /// </summary>
        internal static void OverrideOsAbort()
        {
            if (IsOsApiOverridden)
                return;

            ecs_os_set_api_defaults();
            ecs_os_api_t currentApi = ecs_os_get_api();
            OsAbortNative = currentApi.abort_;

            ecs_os_api.abort_ = BindingContext.OsApiAbortPointer;

            IsOsApiOverridden = true;
        }
    }
}
