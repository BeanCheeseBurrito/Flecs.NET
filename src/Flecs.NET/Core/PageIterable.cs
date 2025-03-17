using System;
using System.Runtime.CompilerServices;

namespace Flecs.NET.Core;

/// <summary>
///      An iterator that limits the returned entities with offset/limit.
/// </summary>
public partial struct PageIterable : IEquatable<PageIterable>
{
    /// <summary>
    ///     The iterator instance.
    /// </summary>
    public ecs_iter_t Iterator;

    /// <summary>
    ///     The number of entities to skip.
    /// </summary>
    public readonly int Offset;

    /// <summary>
    ///     The max number of entities to return.
    /// </summary>
    public readonly int Limit;

    /// <summary>
    ///     Creates a <see cref="PageIterable"/>.
    /// </summary>
    /// <param name="iterator">The source iterator.</param>
    /// <param name="offset">The number of entities to skip.</param>
    /// <param name="limit">The maximum number of entities to return.</param>
    public PageIterable(ecs_iter_t iterator, int offset, int limit)
    {
        Iterator = iterator;
        Offset = offset;
        Limit = limit;
    }

    /// <summary>
    ///     Checks if two <see cref="PageIterable"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(PageIterable other)
    {
        return Iterator == other.Iterator && Offset == other.Offset && Limit == other.Limit;
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
        return HashCode.Combine(Iterator, Offset, Limit);
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

// IPageIterable Interface
public unsafe partial struct PageIterable : IPageIterable
{
    ref PageIterable IPageIterable.Underlying => ref this;
    ref ecs_iter_t IPageIterable.Iterator => ref Iterator;
    int IPageIterable.Offset => Offset;
    int IPageIterable.Limit => Limit;
}

// IIterableBase Interface
public unsafe partial struct PageIterable: IIterableBase
{
    /// <inheritdoc cref="IIterableBase.World"/>
    ecs_world_t* IIterableBase.World => Iterator.world;

    /// <inheritdoc cref="IIterableBase.GetIter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ecs_iter_t GetIter(World world = default)
    {
        fixed (ecs_iter_t* ptr = &Iterator)
            return ecs_page_iter(ptr, Offset, Limit);
    }

    /// <inheritdoc cref="IIterableBase.GetNext"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNext(Iter it)
    {
        return ecs_page_next(it);
    }
}

// IIterable Interface
public unsafe partial struct PageIterable : IIterable
{
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
