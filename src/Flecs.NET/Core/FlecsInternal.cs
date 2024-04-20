using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for holding flecs internals.
    /// </summary>
    public static unsafe class FlecsInternal
    {
        internal static IntPtr OsAbortNative;

        /// <summary>
        ///     Determines whether or not to strip the GUID prefix the from beginning of file-local type names when
        ///     registering components. This is will cause name clashing if file-local types in different files
        ///     have the same name. This is primarily used in Flecs.NET.Examples to reduce output noise.
        /// </summary>
        public static bool StripFileLocalTypeNameGuid { get; set; }

        /// <summary>
        ///     Tests whether or not the os api is initialized.
        /// </summary>
        public static bool IsOsApiOverridden { get; private set; }

        /// <summary>
        ///     Current reset count.
        /// </summary>
        public static int ResetCount { get; private set; }

        /// <summary>
        ///     Override the default os api abort function to log C# stack traces.
        /// </summary>
        public static void OverrideOsAbort()
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
