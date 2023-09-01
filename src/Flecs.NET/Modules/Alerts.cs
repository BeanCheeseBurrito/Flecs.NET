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
            ///     Alert
            /// </summary>
            public struct Alert
            {
            }

            /// <summary>
            ///     Info
            /// </summary>
            public struct Info
            {
            }

            /// <summary>
            ///     Warning
            /// </summary>
            public struct Warning
            {
            }

            /// <summary>
            ///     Error
            /// </summary>
            public struct Err
            {
            }

            /// <summary>
            ///     Initializes the alerts module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                FlecsAlertsImport(world);

                world.Entity<Alert>("::flecs::alerts::Alert");
                world.Entity<Info>("::flecs::alerts::Info");
                world.Entity<Warning>("::flecs::alerts::Warning");
                world.Entity<Err>("::flecs::alerts::Error");
            }
        }
    }
}
