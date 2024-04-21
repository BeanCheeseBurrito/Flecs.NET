using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Monitor module.
        /// </summary>
        public struct Monitor : IFlecsModule, IEquatable<Monitor>
        {
            /// <summary>
            ///     Initializes monitor module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(World world)
            {
                FlecsMonitorImport(world);
            }

            /// <summary>
            ///     Checks if two <see cref="Monitor"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Monitor other)
            {
                return true;
            }

            /// <summary>
            ///     Checks if two <see cref="Monitor"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Monitor;
            }

            /// <summary>
            ///     Returns the hash code of the <see cref="Monitor"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 0;
            }

            /// <summary>
            ///     Checks if two <see cref="Monitor"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Monitor left, Monitor right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Monitor"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Monitor left, Monitor right)
            {
                return !(left == right);
            }
        }
    }
}
