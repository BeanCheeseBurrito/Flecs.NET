using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

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
        public readonly void InitModule(World world)
        {
            world.Component<bool>().Set(new EcsPrimitive { kind = EcsBool });
            world.Component<char>().Set(new EcsPrimitive { kind = EcsChar });
            world.Component<byte>().Set(new EcsPrimitive { kind = EcsU8 });
            world.Component<ushort>().Set(new EcsPrimitive { kind = EcsU16 });
            world.Component<uint>().Set(new EcsPrimitive { kind = EcsU32 });
            world.Component<ulong>().Set(new EcsPrimitive { kind = EcsU64 });
            world.Component<sbyte>().Set(new EcsPrimitive { kind = EcsI8 });
            world.Component<short>().Set(new EcsPrimitive { kind = EcsI16 });
            world.Component<int>().Set(new EcsPrimitive { kind = EcsI32 });
            world.Component<long>().Set(new EcsPrimitive { kind = EcsI64 });
            world.Component<float>().Set(new EcsPrimitive { kind = EcsF32 });
            world.Component<double>().Set(new EcsPrimitive { kind = EcsF64 });

            world.Component<UIntPtr>().Set(new EcsPrimitive { kind = EcsUPtr });
            world.Component<IntPtr>().Set(new EcsPrimitive { kind = EcsIPtr });

            // TODO: Add support for string.
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
