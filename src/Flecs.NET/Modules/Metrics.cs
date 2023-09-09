using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Metrics module.
        /// </summary>
        public struct Metrics : IFlecsModule
        {
            /// <summary>
            ///     Initializes metrics module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                world.Import<Units>();

                FlecsMetricsImport(world);

                world.Entity<Instance>("::flecs::metrics::Instance");
                world.Entity<Metric>("::flecs::metrics::Metric");
                world.Entity<Counter>("::flecs::metrics::Metric::Counter");
                world.Entity<CounterId>("::flecs::metrics::Metric::CounterId");
                world.Entity<CounterIncrement>("::flecs::metrics::Metric::CounterIncrement");
                world.Entity<Gauge>("::flecs::metrics::Metric::Gauge");
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
        }
    }
}
