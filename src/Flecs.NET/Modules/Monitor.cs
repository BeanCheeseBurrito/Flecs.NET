using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Monitor module.
        /// </summary>
        public unsafe struct Monitor : IFlecsModule
        {
            /// <summary>
            ///     Initializes monitor module.
            /// </summary>
            /// <param name="world"></param>
            public void InitModule(ref World world)
            {
                FlecsMonitorImport(world);
            }
        }
    }
}
