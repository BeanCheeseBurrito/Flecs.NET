using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Alerts module
        /// </summary>
        public struct Alerts : IFlecsModule
        {
            /// <summary>
            ///     Initializes the alerts module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                FlecsAlertsImport(world);

                world.Entity<Alert>("global::flecs.alerts.Alert");
                world.Entity<Info>("global::flecs.alerts.Info");
                world.Entity<Warning>("global::flecs.alerts.Warning");
                world.Entity<Err>("global::flecs.alerts.Error");
            }

            /// <summary>
            ///     Alert tag.
            /// </summary>
            public struct Alert
            {
            }

            /// <summary>
            ///     Info tag.
            /// </summary>
            public struct Info
            {
            }

            /// <summary>
            ///     Warning tag.
            /// </summary>
            public struct Warning
            {
            }

            /// <summary>
            ///     Error tag.
            /// </summary>
            public struct Err
            {
            }
        }
    }
}
