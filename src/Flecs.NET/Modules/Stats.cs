using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Stats module.
        /// </summary>
        public struct Stats : IFlecsModule, IEquatable<Stats>
        {
            /// <summary>
            ///     Initializes stats module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(World world)
            {
                FlecsStatsImport(world);
            }

            /// <summary>
            ///     Checks if two <see cref="Stats"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Stats other)
            {
                return true;
            }

            /// <summary>
            ///     Checks if two <see cref="Stats"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Stats;
            }

            /// <summary>
            ///     Returns the hash code of the <see cref="Stats"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 0;
            }

            /// <summary>
            ///     Checks if two <see cref="Stats"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Stats left, Stats right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Stats"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Stats left, Stats right)
            {
                return !(left == right);
            }
        }
    }
}
