// /_/src/Flecs.NET/Generated/PageIterable/PageIterable/T8.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/PageIterable.cs
#nullable enable

using System;
using System.Runtime.CompilerServices;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <inheritdoc cref="IterIterable"/>
/// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam>
public unsafe partial struct PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> : IEquatable<PageIterable<T0, T1, T2, T3, T4, T5, T6, T7>>
{
    /// <inheritdoc cref="IPageIterable.Underlying"/>
    public PageIterable Underlying;

    /// <inheritdoc cref="IPageIterable.Iterator"/>
    public ref ecs_iter_t Iterator => ref Underlying.Iterator;
    
    /// <inheritdoc cref="IPageIterable.Offset"/>
    public int Offset => Underlying.Offset;
    
    /// <inheritdoc cref="IPageIterable.Limit"/>
    public int Limit => Underlying.Limit;

    /// <summary>
    ///     Creates a page iterable.
    /// </summary>
    /// <param name="handle">The page iterable.</param>
    public PageIterable(PageIterable handle)
    {
        Underlying = handle;
    }

    /// <inheritdoc cref="PageIterable(ecs_iter_t, int, int)"/>
    public PageIterable(ecs_iter_t iter, int offset, int limit)
    {
        Underlying = new PageIterable(iter, offset, limit);
    }
    
    /// <inheritdoc cref="PageIterable.Equals(PageIterable)"/>
    public bool Equals(PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> other)
    {
        return Underlying.Equals(other.Underlying);
    }
    
    /// <inheritdoc cref="PageIterable.Equals(object)"/>
    public override bool Equals(object? obj)
    {
        return obj is PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> other && Equals(other);
    }
    
    /// <inheritdoc cref="PageIterable.GetHashCode()"/>
    public override int GetHashCode()
    {
        return Underlying.GetHashCode();
    }
    
    /// <inheritdoc cref="PageIterable.op_Equality"/>
    public static bool operator ==(PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> left, PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc cref="PageIterable.op_Inequality"/>
    public static bool operator !=(PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> left, PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> right)
    {
        return !(left == right);
    }
}

// IPageIterable Interface
public unsafe partial struct PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> : IPageIterable
{
    ref PageIterable IPageIterable.Underlying => ref Underlying;
}

// IIterableBase Interface
public unsafe partial struct PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> : IIterableBase
{
    /// <inheritdoc cref="IIterableBase.World"/>
    ecs_world_t* IIterableBase.World => Iterator.world;
    
    /// <inheritdoc cref="IIterableBase.GetIter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ecs_iter_t GetIter(World world = default)
    {
        return Underlying.GetIter(world);
    }
    
    /// <inheritdoc cref="IIterableBase.GetNext"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNext(Iter it)
    {
        return Underlying.GetNext(it);
    }
}

// IIterable<T0, T1, T2, T3, T4, T5, T6, T7> Interface
public unsafe partial struct PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> : IIterable<T0, T1, T2, T3, T4, T5, T6, T7>
{
    /// <inheritdoc cref="PageIterable.Page(int, int)"/>
    public PageIterable<T0, T1, T2, T3, T4, T5, T6, T7> Page(int offset, int limit)
    {
        return new PageIterable<T0, T1, T2, T3, T4, T5, T6, T7>(Underlying.Page(offset, limit));
    }
    
    /// <inheritdoc cref="PageIterable.Worker(int, int)"/>
    public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7> Worker(int index, int count)
    {
        return new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7>(Underlying.Worker(index, count));
    }

    /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.World)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> Iter(World world = default)
    {
        return new(Underlying.Iter(world));
    }
    
    /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.Iter)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> Iter(Iter it)
    {
        return new(Underlying.Iter(it));
    }
    
    /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.Entity)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> Iter(Entity entity)
    {
        return new(Underlying.Iter(entity));
    }
    
    /// <inheritdoc cref="PageIterable.Count()"/>
    public int Count()
    {
        return Underlying.Count();
    }
    
    /// <inheritdoc cref="PageIterable.IsTrue()"/>
    public bool IsTrue()
    {
        return Underlying.IsTrue();
    }
    
    /// <inheritdoc cref="PageIterable.First()"/>
    public Entity First()
    {
        return Underlying.First();
    }
    
    /// <inheritdoc cref="PageIterable.SetVar(int, ulong)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetVar(int varId, ulong value)
    {
        return new(Underlying.SetVar(varId, value));
    }
    
    /// <inheritdoc cref="PageIterable.SetVar(string, ulong)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetVar(string name, ulong value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="PageIterable.SetVar(string, ecs_table_t*)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetVar(string name, ecs_table_t* value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="PageIterable.SetVar(string, ecs_table_range_t)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetVar(string name, ecs_table_range_t value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="PageIterable.SetVar(string, Table)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetVar(string name, Table value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="PageIterable.SetGroup(ulong)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetGroup(ulong groupId)
    {
        return new(Underlying.SetGroup(groupId));
    }
    
    /// <inheritdoc cref="PageIterable.SetGroup{T}()"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7> SetGroup<T>()
    {
        return new(Underlying.SetGroup<T>()); 
    }
}