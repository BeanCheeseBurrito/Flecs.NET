using System;
using System.Runtime.CompilerServices;

namespace Flecs.NET.Core;

/// <summary>
///     An iterator that divides the number of matched entities across a number of resources.
/// </summary>
public partial struct WorkerIterable : IEquatable<WorkerIterable>
{
    /// <summary>
    ///     The iterator instance.
    /// </summary>
    public ecs_iter_t Iterator;

    /// <summary>
    ///     The current thread index for this iterator.
    /// </summary>
    public readonly int ThreadIndex;

    /// <summary>
    ///     The total number of threads to divide entities between.
    /// </summary>
    public readonly int ThreadCount;

    /// <summary>
    ///     Creates a <see cref="WorkerIterable"/>.
    /// </summary>
    /// <param name="iterator">The source iterator.</param>
    /// <param name="threadIndex">The current thread index for this iterator.</param>
    /// <param name="threadCount">The total number of threads to divide entities between.</param>
    public WorkerIterable(ecs_iter_t iterator, int threadIndex, int threadCount)
    {
        Iterator = iterator;
        ThreadIndex = threadIndex;
        ThreadCount = threadCount;
    }

    /// <summary>
    ///     Checks if two <see cref="WorkerIterable"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(WorkerIterable other)
    {
        return Iterator == other.Iterator && ThreadIndex == other.ThreadIndex && ThreadCount == other.ThreadCount;
    }

    /// <summary>
    ///     Checks if two <see cref="WorkerIterable"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is WorkerIterable other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="WorkerIterable"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Iterator.GetHashCode(), ThreadIndex, ThreadCount);
    }

    /// <summary>
    ///     Checks if two <see cref="WorkerIterable"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(WorkerIterable left, WorkerIterable right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="WorkerIterable"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(WorkerIterable left, WorkerIterable right)
    {
        return !(left == right);
    }
}

// IWorkerIterable Interface
public unsafe partial struct WorkerIterable : IWorkerIterable
{
    ref WorkerIterable IWorkerIterable.Underlying => ref this;
    ref ecs_iter_t IWorkerIterable.Iterator => ref Iterator;
    int IWorkerIterable.ThreadIndex => ThreadIndex;
    int IWorkerIterable.ThreadCount => ThreadCount;
}

// IIterableBase Interface
public unsafe partial struct WorkerIterable : IIterableBase
{
    /// <inheritdoc cref="IIterableBase.World"/>
    ecs_world_t* IIterableBase.World => Iterator.world;

    /// <inheritdoc cref="IIterableBase.GetIter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ecs_iter_t GetIter(World world = default)
    {
        fixed (ecs_iter_t* ptr = &Iterator)
            return ecs_worker_iter(ptr, ThreadIndex, ThreadCount);
    }

    /// <inheritdoc cref="IIterableBase.GetNext"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNext(Iter it)
    {
        return ecs_worker_next(it);
    }
}

// IIterable Interface
public unsafe partial struct WorkerIterable : IIterable
{
    /// <inheritdoc cref="IIterable.Page(int, int)"/>
    public PageIterable Page(int offset, int limit)
    {
        return new PageIterable(this.GetIter(), offset, limit);
    }

    /// <inheritdoc cref="IIterable.Worker(int, int)"/>
    public WorkerIterable Worker(int index, int count)
    {
        return new WorkerIterable(this.GetIter(), index, count);
    }

    /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.World)"/>
    public IterIterable Iter(World world = default)
    {
        return new IterIterable(this.GetIter(world), IterableType.Worker);
    }

    /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.Iter)"/>
    public IterIterable Iter(Iter it)
    {
        return Iter(it.World());
    }

    /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.Entity)"/>
    public IterIterable Iter(Entity entity)
    {
        return Iter(entity.CsWorld());
    }

    /// <inheritdoc cref="IIterable.Count()"/>
    public int Count()
    {
        return Iter().Count();
    }

    /// <inheritdoc cref="IIterable.IsTrue()"/>
    public bool IsTrue()
    {
        return Iter().IsTrue();
    }

    /// <inheritdoc cref="IIterable.First()"/>
    public Entity First()
    {
        return Iter().First();
    }

    /// <inheritdoc cref="IIterable.SetVar(int, ulong)"/>
    public IterIterable SetVar(int varId, ulong value)
    {
        return Iter().SetVar(varId, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, ulong)"/>
    public IterIterable SetVar(string name, ulong value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, ecs_table_t*)"/>
    public IterIterable SetVar(string name, ecs_table_t* value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, ecs_table_range_t)"/>
    public IterIterable SetVar(string name, ecs_table_range_t value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, Table)"/>
    public IterIterable SetVar(string name, Table value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetGroup(ulong)"/>
    public IterIterable SetGroup(ulong groupId)
    {
        return Iter().SetGroup(groupId);
    }

    /// <inheritdoc cref="IIterable.SetGroup{T}()"/>
    public IterIterable SetGroup<T>()
    {
        return Iter().SetGroup<T>();
    }
}
