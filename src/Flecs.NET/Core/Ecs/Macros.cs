using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Returns information about the current Flecs build.
        /// </summary>
        /// <returns>A struct with information about the current Flecs build.</returns>
        public static BuildInfo GetBuildInfo()
        {
            return new BuildInfo(ecs_get_build_info());
        }
    }
}
