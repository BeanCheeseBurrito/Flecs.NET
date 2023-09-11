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

                world.Entity<bool>("global::flecs.meta.bool");
                world.Entity<byte>("global::flecs.meta.u8");
                world.Entity<ushort>("global::flecs.meta.u16");
                world.Entity<uint>("global::flecs.meta.u32");
                world.Entity<ulong>("global::flecs.meta.u64");
                world.Entity<sbyte>("global::flecs.meta.i8");
                world.Entity<short>("global::flecs.meta.i16");
                world.Entity<int>("global::flecs.meta.i32");
                world.Entity<long>("global::flecs.meta.i64");
                world.Entity<float>("global::flecs.meta.f32");
                world.Entity<double>("global::flecs.meta.f64");

                // TODO: Add support for string, char and native sized integers
            }
        }
    }
}
