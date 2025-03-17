using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Flecs.NET.Core.Invokers;

[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
internal unsafe interface IFieldGetter
{
    static abstract T* Get<T>(in Fields fieldData, byte field, int row);

    internal struct Self : IFieldGetter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* Get<T>(in Fields fields, byte field, int row)
        {
            return (T*)&((byte*)fields.Pointers[field])[row * Type<T>.Size];
        }
    }

    internal struct Shared : IFieldGetter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* Get<T>(in Fields fields, byte field, int row)
        {
            return Utils.IsBitSet(fields.Shared, field)
                ? Self.Get<T>(in fields, field, 0)
                : Self.Get<T>(in fields, field, row);
        }
    }

    internal struct Sparse : IFieldGetter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* Get<T>(in Fields fieldData, byte field, int row)
        {
            return Utils.IsBitSet(fieldData.Sparse, field)
                ? (T*)ecs_field_at_w_size(fieldData.Iter, Type<T>.Size, field, row)
                : Self.Get<T>(in fieldData, field, row);
        }
    }

    internal struct SparseShared : IFieldGetter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* Get<T>(in Fields fieldData, byte field, int row)
        {
            return Utils.IsBitSet(fieldData.Sparse, field)
                ? (T*)ecs_field_at_w_size(fieldData.Iter, Type<T>.Size, field, row)
                : Shared.Get<T>(in fieldData, field, row);
        }
    }
}
