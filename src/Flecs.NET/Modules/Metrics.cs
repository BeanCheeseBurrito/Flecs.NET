using System;
using System.Diagnostics.CodeAnalysis;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Metrics module.
        /// </summary>
        [SuppressMessage("Usage", "CA1724")]
        public struct Metrics : IFlecsModule, IEquatable<Metrics>
        {
            /// <summary>
            ///     Initializes metrics module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                world.Import<Units>();

                FlecsMetricsImport(world);

                world.Entity<Instance>("::flecs.metrics.Instance");
                world.Entity<Metric>("::flecs.metrics.Metric");
                world.Entity<Counter>("::flecs.metrics.Metric.Counter");
                world.Entity<CounterId>("::flecs.metrics.Metric.CounterId");
                world.Entity<CounterIncrement>("::flecs.metrics.Metric.CounterIncrement");
                world.Entity<Gauge>("::flecs.metrics.Metric.Gauge");
            }

            /// <summary>
            ///     Instance tag.
            /// </summary>
            public struct Instance
            {
            }

            /// <summary>
            ///     Metric tag.
            /// </summary>
            public struct Metric
            {
            }

            /// <summary>
            ///     Counter tag.
            /// </summary>
            public struct Counter
            {
            }

            /// <summary>
            ///     CounterIncrement tag.
            /// </summary>
            public struct CounterIncrement
            {
            }

            /// <summary>
            ///     CounterId tag.
            /// </summary>
            public struct CounterId
            {
            }

            /// <summary>
            ///     Gauge tag.
            /// </summary>
            public struct Gauge
            {
            }

            /// <summary>
            ///     Checks if two <see cref="Metrics"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Metrics other)
            {
                return true;
            }

            /// <summary>
            ///     Checks if two <see cref="Metrics"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Metrics;
            }

            /// <summary>
            ///     Returns the hash code of the <see cref="Metrics"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 0;
            }

            /// <summary>
            ///     Checks if two <see cref="Metrics"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Metrics left, Metrics right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Metrics"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Metrics left, Metrics right)
            {
                return !(left == right);
            }
        }
    }
}
