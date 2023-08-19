using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core.Modules
{
    public unsafe struct Alerts : IFlecsModule
    {
        public struct Alert
        {
        }

        public struct Info
        {
        }

        public struct Warning
        {
        }

        public struct Err
        {
        }

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