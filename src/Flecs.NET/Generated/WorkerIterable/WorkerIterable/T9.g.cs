// /_/src/Flecs.NET/Generated/WorkerIterable/WorkerIterable/T9.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/WorkerIterable.cs
#nullable enable

using System;
using System.Runtime.CompilerServices;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <inheritdoc cref="IterIterable"/>
/// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam>
public unsafe partial struct WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> : IEquatable<WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8>>, IIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
    /// <inheritdoc cref="IWorkerIterable.Underlying"/>
    public WorkerIterable Underlying;
    
    /// <inheritdoc cref="IWorkerIterable.Iterator"/>
    public ref ecs_iter_t Iterator => ref Underlying.Iterator;
    
    /// <inheritdoc cref="IWorkerIterable.ThreadIndex"/>
    public int ThreadIndex => Underlying.ThreadIndex;
    
    /// <inheritdoc cref="IWorkerIterable.ThreadCount"/>
    public int ThreadCount => Underlying.ThreadCount;

    /// <summary>
    ///     Creates a worker iterable.
    /// </summary>
    /// <param name="handle">The worker iterable.</param>
    public WorkerIterable(WorkerIterable handle)
    {
        Underlying = handle;
    }

    /// <inheritdoc cref="WorkerIterable(ecs_iter_t, int, int)"/>
    public WorkerIterable(ecs_iter_t iter, int index, int count)
    {
        Underlying = new WorkerIterable(iter, index, count);
    }
    
    /// <inheritdoc cref="WorkerIterable.Equals(WorkerIterable)"/>
    public bool Equals(WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> other)
    {
        return Underlying.Equals(other.Underlying);
    }
    
    /// <inheritdoc cref="WorkerIterable.Equals(object)"/>
    public override bool Equals(object? obj)
    {
        return obj is WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> other && Equals(other);
    }
    
    /// <inheritdoc cref="WorkerIterable.GetHashCode()"/>
    public override int GetHashCode()
    {
        return Underlying.GetHashCode();
    }
    
    /// <inheritdoc cref="WorkerIterable.op_Equality"/>
    public static bool operator ==(WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> left, WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc cref="WorkerIterable.op_Inequality"/>
    public static bool operator !=(WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> left, WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> right)
    {
        return !(left == right);
    }
}

// IWorkerIterable Interface
public unsafe partial struct WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> : IWorkerIterable
{
    ref WorkerIterable IWorkerIterable.Underlying => ref Underlying;
}

// IIterableBase Interface
public unsafe partial struct WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> : IIterableBase
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

// IIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> Interface
public unsafe partial struct WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> : IIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
    /// <inheritdoc cref="WorkerIterable.Page(int, int)"/>
    public PageIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> Page(int offset, int limit)
    {
        return new PageIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Underlying.Page(offset, limit));
    }
    
    /// <inheritdoc cref="WorkerIterable.Worker(int, int)"/>
    public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> Worker(int index, int count)
    {
        return new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Underlying.Worker(index, count));
    }
    
    /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.World)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> Iter(World world = default)
    {
        return new(Underlying.Iter(world));
    }
    
    /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.Iter)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> Iter(Iter it)
    {
        return new(Underlying.Iter(it));
    }
    
    /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.Entity)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> Iter(Entity entity)
    {
        return new(Underlying.Iter(entity));
    }
    
    /// <inheritdoc cref="WorkerIterable.Count()"/>
    public int Count()
    {
        return Underlying.Count();
    }
    
    /// <inheritdoc cref="WorkerIterable.IsTrue()"/>
    public bool IsTrue()
    {
        return Underlying.IsTrue();
    }
    
    /// <inheritdoc cref="WorkerIterable.First()"/>
    public Entity First()
    {
        return Underlying.First();
    }
    
    /// <inheritdoc cref="WorkerIterable.SetVar(int, ulong)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetVar(int varId, ulong value)
    {
        return new(Underlying.SetVar(varId, value));
    }
    
    /// <inheritdoc cref="WorkerIterable.SetVar(string, ulong)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetVar(string name, ulong value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="WorkerIterable.SetVar(string, ecs_table_t*)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetVar(string name, ecs_table_t* value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="WorkerIterable.SetVar(string, ecs_table_range_t)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetVar(string name, ecs_table_range_t value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="WorkerIterable.SetVar(string, Table)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetVar(string name, Table value)
    {
        return new(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="WorkerIterable.SetGroup(ulong)"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetGroup(ulong groupId)
    {
        return new(Underlying.SetGroup(groupId));
    }
    
    /// <inheritdoc cref="WorkerIterable.SetGroup{T}()"/>
    public IterIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8> SetGroup<T>()
    {
        return new(Underlying.SetGroup<T>()); 
    }
}