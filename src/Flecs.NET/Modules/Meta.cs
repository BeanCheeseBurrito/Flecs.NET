using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static partial class Ecs
    {
        /// <summary>
        ///     Meta module.
        /// </summary>
        public struct Meta : IEquatable<Meta>, IFlecsModule
        {
            /// <summary>
            ///     Initializes meta module.
            /// </summary>
            /// <param name="world"></param>
            public readonly void InitModule(ref World world)
            {
                world.Component<bool>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsBool });
                world.Component<byte>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsU8 });
                world.Component<ushort>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsU16 });
                world.Component<uint>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsU32 });
                world.Component<ulong>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsU64 });
                world.Component<sbyte>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsI8 });
                world.Component<short>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsI16 });
                world.Component<int>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsI32 });
                world.Component<long>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsI64 });
                world.Component<float>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsF32 });
                world.Component<double>().Entity.Set(new EcsPrimitive { kind = ecs_primitive_kind_t.EcsF64 });

                // TODO: Add support for string, char and native sized integers
            }

            /// <summary>
            ///     Checks if two <see cref="Meta"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Meta other)
            {
                return true;
            }

            /// <summary>
            ///     Checks if two <see cref="Meta"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Meta;
            }

            /// <summary>
            ///     Returns the hash code of the <see cref="Meta"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 0;
            }

            /// <summary>
            ///     Checks if two <see cref="Meta"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Meta left, Meta right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Meta"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Meta left, Meta right)
            {
                return !(left == right);
            }
        }
    }
}
