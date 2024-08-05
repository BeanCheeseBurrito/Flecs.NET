using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///      An iterator that limits the returned entities with offset/limit.
/// </summary>
public partial struct PageIterable : IEquatable<PageIterable>, IIterable
{
    private ecs_iter_t _iter;
    private readonly int _offset;
    private readonly int _limit;

    /// <summary>
    ///     Creates a <see cref="PageIterable"/>.
    /// </summary>
    /// <param name="iter">The source iterator.</param>
    /// <param name="offset">The number of entities to skip.</param>
    /// <param name="limit">The maximum number of entities to return.</param>
    public PageIterable(ecs_iter_t iter, int offset, int limit)
    {
        _iter = iter;
        _offset = offset;
        _limit = limit;
    }

    /// <summary>
    ///     Checks if two <see cref="PageIterable"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(PageIterable other)
    {
        return Equals(_iter, other._iter) && Equals(_offset, other._offset) && Equals(_limit, other._limit);
    }

    /// <summary>
    ///     Checks if two <see cref="PageIterable"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is PageIterable other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="PageIterable"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(_iter.GetHashCode(), _offset, _limit);
    }

    /// <summary>
    ///     Checks if two <see cref="PageIterable"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(PageIterable left, PageIterable right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="PageIterable"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(PageIterable left, PageIterable right)
    {
        return !(left == right);
    }
}

// IIterableBase Interface
public unsafe partial struct PageIterable
{
    /// <inheritdoc cref="IIterableBase.GetIter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ecs_iter_t GetIter(ecs_world_t* world = null)
    {
        fixed (ecs_iter_t* ptr = &_iter)
            return ecs_page_iter(ptr, _offset, _limit);
    }

    /// <inheritdoc cref="IIterableBase.GetNext"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNext(ecs_iter_t* it)
    {
        return Utils.Bool(ecs_page_next(it));
    }

    /// <inheritdoc cref="IIterableBase.Count()"/>
    public int Count()
    {
        return Iter().Count();
    }

    /// <inheritdoc cref="IIterableBase.IsTrue()"/>
    public bool IsTrue()
    {
        return Iter().IsTrue();
    }

    /// <inheritdoc cref="IIterableBase.First()"/>
    public Entity First()
    {
        return Iter().First();
    }
}

// IIterable Interface
public unsafe partial struct PageIterable
{
    /// <inheritdoc cref="IIterable.Run(Ecs.RunCallback)"/>
    public void Run(Ecs.RunCallback callback)
    {
        Invoker.Run(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Run(Ecs.RunCallback)"/>
    public void Run(delegate*<Iter, void> callback)
    {
        Invoker.Run(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Iter(Ecs.IterCallback)"/>
    public void Iter(Ecs.IterCallback callback)
    {
        Invoker.Iter(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Iter(Ecs.IterCallback)"/>
    public void Iter(delegate*<Iter, void> callback)
    {
        Invoker.Iter(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Each(Ecs.EachEntityCallback)"/>
    public void Each(Ecs.EachEntityCallback callback)
    {
        Invoker.Each(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Each(Ecs.EachEntityCallback)"/>
    public void Each(delegate*<Entity, void> callback)
    {
        Invoker.Each(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Each(Ecs.EachIterCallback)"/>
    public void Each(Ecs.EachIterCallback callback)
    {
        Invoker.Each(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Each(Ecs.EachIterCallback)"/>
    public void Each(delegate*<Iter, int, void> callback)
    {
        Invoker.Each(ref this, callback);
    }

    /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.World)"/>
    public IterIterable Iter(World world = default)
    {
        return new IterIterable(GetIter(world), IterableType.Page);
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

    /// <inheritdoc cref="IIterable.Page(int, int)"/>
    public PageIterable Page(int offset, int limit)
    {
        return new PageIterable(GetIter(), offset, limit);
    }

    /// <inheritdoc cref="IIterable.Worker(int, int)"/>
    public WorkerIterable Worker(int index, int count)
    {
        return new WorkerIterable(GetIter(), index, count);
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
