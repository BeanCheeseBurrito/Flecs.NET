using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Alerts module
        /// </summary>
        public struct Alerts : IFlecsModule, IEquatable<Alerts>
        {
            /// <summary>
            ///     Initializes the alerts module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(World world)
            {
                FlecsAlertsImport(world);

                world.Entity<Alert>(".flecs.alerts.Alert");
                world.Entity<Info>(".flecs.alerts.Info");
                world.Entity<Warning>(".flecs.alerts.Warning");
                world.Entity<Err>(".flecs.alerts.Error");
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

            /// <summary>
            ///     Checks if two <see cref="Alerts"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Alerts other)
            {
                return true;
            }

            /// <summary>
            ///     Checks if two <see cref="Alerts"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Alerts;
            }

            /// <summary>
            ///     Returns the hash code of the <see cref="Alerts"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 0;
            }

            /// <summary>
            ///     Checks if two <see cref="Alerts"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Alerts left, Alerts right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Alerts"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Alerts left, Alerts right)
            {
                return !(left == right);
            }
        }
    }
}
