using Flecs.NET.Bindings;

namespace Flecs.NET.Core
{
    public static partial class Ecs
    {
        /// <summary>
        ///     Meta module.
        /// </summary>
        public struct Meta : IFlecsModule
        {
            /// <summary>
            ///     Initializes meta module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                Type<bool>.SetSymbolName("bool");
                Type<byte>.SetSymbolName("u8");
                Type<ushort>.SetSymbolName("u16");
                Type<uint>.SetSymbolName("u32");
                Type<ulong>.SetSymbolName("u64");
                Type<sbyte>.SetSymbolName("i8");
                Type<short>.SetSymbolName("i16");
                Type<int>.SetSymbolName("i32");
                Type<long>.SetSymbolName("i64");
                Type<float>.SetSymbolName("f32");
                Type<double>.SetSymbolName("f64");

                world.Entity<bool>("::flecs::meta::bool");
                world.Entity<byte>("::flecs::meta::u8");
                world.Entity<ushort>("::flecs::meta::u16");
                world.Entity<uint>("::flecs::meta::u32");
                world.Entity<ulong>("::flecs::meta::u64");
                world.Entity<sbyte>("::flecs::meta::i8");
                world.Entity<short>("::flecs::meta::i16");
                world.Entity<int>("::flecs::meta::i32");
                world.Entity<long>("::flecs::meta::i64");
                world.Entity<float>("::flecs::meta::f32");
                world.Entity<double>("::flecs::meta::f64");

                // TODO: Add support for string, char and native sized integers
            }
        }
    }
}
