using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

internal unsafe struct FieldData<T>()
{
    public T* Pointer;
    public ecs_iter_t* Iter;
    public byte Index;    // Field index.
    public bool IsShared; // Is 0 indexed component.
    public bool IsSparse; // Is sparse component.

    public FieldData(ecs_iter_t* iter, byte index) : this()
    {
        Iter = iter;
        Index = index;

        if (Utils.IsBitSet(iter->row_fields, index))
        {
            IsSparse = true;
        }
        else
        {
            Pointer = (T*)ecs_field_w_size(iter, Type<T>.Size, index);
            IsShared = iter->sources[index] != 0 || !Utils.IsBitSet(iter->set_fields, index);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerUnmanaged(int row)
    {
        return &Pointer[row];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerSharedUnmanaged(int row)
    {
        return IsShared ? PointerUnmanaged(0) : PointerUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerSparseUnmanaged(int row)
    {
        return IsSparse ? (T*)ecs_field_at_w_size(Iter, Type<T>.Size, Index, row) : PointerUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerSparseSharedUnmanaged(int row)
    {
        return IsSparse ? (T*)ecs_field_at_w_size(Iter, Type<T>.Size, Index, row) : PointerSharedUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerManaged(int row)
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            return (T*)&((GCHandle*)Pointer)[row];

        return &Pointer[row];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerSharedManaged(int row)
    {
        return IsShared ? PointerManaged(0) : PointerManaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerSparseManaged(int row)
    {
        return IsSparse ? (T*)ecs_field_at_w_size(Iter, Type<T>.Size, Index, row) : PointerManaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* PointerSparseSharedManaged(int row)
    {
        return IsSparse ? (T*)ecs_field_at_w_size(Iter, Type<T>.Size, Index, row) : PointerSharedManaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefUnmanaged(int row)
    {
        return ref *PointerUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefSharedUnmanaged(int row)
    {
        return ref *PointerSharedUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefSparseUnmanaged(int row)
    {
        return ref *PointerSparseUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefSparseSharedUnmanaged(int row)
    {
        return ref *PointerSparseSharedUnmanaged(row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefManaged(int row)
    {
        return ref Managed.GetTypeRef<T>(PointerManaged(row));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefSharedManaged(int row)
    {
        return ref Managed.GetTypeRef<T>(PointerSharedManaged(row));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefSparseManaged(int row)
    {
        return ref Managed.GetTypeRef<T>(PointerSparseManaged(row));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T RefSparseSharedManaged(int row)
    {
        return ref Managed.GetTypeRef<T>(PointerSparseSharedManaged(row));
    }
}
