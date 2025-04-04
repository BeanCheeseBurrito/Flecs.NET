using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     An iterator object that can be modified before iterating.
/// </summary>
public unsafe partial struct IterIterable : IEquatable<IterIterable>
{
    /// <summary>
    ///     The iterator instance.
    /// </summary>
    public ecs_iter_t Iterator;

    /// <summary>
    ///     The iterable type.
    /// </summary>
    public readonly IterableType IterableType;

    /// <summary>
    ///     Creates an iter iterable.
    /// </summary>
    /// <param name="iterator">The iterator source.</param>
    /// <param name="iterableType">The iterator type.</param>
    public IterIterable(ecs_iter_t iterator, IterableType iterableType)
    {
        Iterator = iterator;
        IterableType = iterableType;
    }

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="varId">The variable id.</param>
    /// <param name="value">The entity variable value.</param>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetVar(int varId, ulong value)
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
            ecs_iter_set_var(it, varId, value);
            return ref this;
        }
    }

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The entity variable value.</param>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetVar(string name, ulong value)
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            using NativeString nativeName = (NativeString)name;

            ecs_query_iter_t* iter = &it->priv_.iter.query;
            int varId = ecs_query_find_var(iter->query, nativeName);

            Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
            ecs_iter_set_var(it, varId, value);

            return ref this;
        }
    }

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The table variable value.</param>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetVar(string name, ecs_table_t* value)
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            using NativeString nativeName = (NativeString)name;

            ecs_query_iter_t* iter = &it->priv_.iter.query;
            int varId = ecs_query_find_var(iter->query, nativeName);

            Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
            ecs_iter_set_var_as_table(it, varId, value);

            return ref this;
        }
    }

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The table variable value.</param>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetVar(string name, ecs_table_range_t value)
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            using NativeString nativeName = (NativeString)name;

            ecs_query_iter_t* iter = &it->priv_.iter.query;
            int varId = ecs_query_find_var(iter->query, nativeName);

            Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
            ecs_iter_set_var_as_range(it, varId, &value);

            return ref this;
        }
    }

    /// <summary>
    ///     Set value for iterator variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The table variable value.</param>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetVar(string name, Table value)
    {
        ecs_table_range_t range;
        range.table = value.GetTable();
        range.offset = value.Offset;
        range.count = value.Count;
        return ref SetVar(name, range);
    }

    /// <summary>
    ///     Serializes the iterator to a JSON string using the provided description.
    /// </summary>
    /// <param name="desc">The description settings for JSON serialization.</param>
    /// <returns>A JSON string with the serialized iterator data, or an empty string if failed.</returns>
    public string ToJson(in IterToJsonDesc desc)
    {
        fixed (ecs_iter_t* it = &Iterator)
        fixed (ecs_iter_to_json_desc_t* ptr = &desc.Desc)
        {
            return NativeString.GetStringAndFree(ecs_iter_to_json(it, ptr));
        }
    }

    /// <summary>
    ///     Serializes the iterator to a JSON string.
    /// </summary>
    /// <returns>A JSON string with the serialized iterator data, or an empty string if failed.</returns>
    public string ToJson()
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            return NativeString.GetStringAndFree(ecs_iter_to_json(it, null));
        }
    }

    /// <summary>
    ///     Returns total number of entities in result.
    /// </summary>
    /// <returns></returns>
    public int Count()
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            int result = 0;
            while (this.GetNext(it))
                result += Iterator.count;
            return result;
        }
    }

    /// <summary>
    ///     Returns true if iterator yields at least once result.
    /// </summary>
    /// <returns></returns>
    public bool IsTrue()
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            bool result = this.GetNext(it);
            if (result)
                ecs_iter_fini(it);
            return result;
        }
    }

    /// <summary>
    ///     Return first matching entity.
    /// </summary>
    /// <returns></returns>
    public Entity First()
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            Entity result = default;

            if (this.GetNext(it) && it->count != 0)
            {
                result = new Entity(it->world, it->entities[0]);
                ecs_iter_fini(it);
            }

            return result;
        }
    }

    /// <summary>
    ///     Limit results to tables with specified group id (grouped queries only)
    /// </summary>
    /// <param name="groupId">The group id.</param>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetGroup(ulong groupId)
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            ecs_iter_set_group(it, groupId);
            return ref this;
        }
    }

    /// <summary>
    ///     Limit results to tables with specified group id (grouped queries only)
    /// </summary>
    /// <typeparam name="T">The group type.</typeparam>
    /// <returns>Reference to self.</returns>
    public ref IterIterable SetGroup<T>()
    {
        fixed (ecs_iter_t* it = &Iterator)
        {
            ecs_iter_set_group(it, Type<T>.Id(it->real_world));
            return ref this;
        }
    }

    /// <summary>
    ///     Checks if two <see cref="IterIterable"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(IterIterable other)
    {
        return Iterator == other.Iterator && IterableType == other.IterableType;
    }

    /// <summary>
    ///     Checks if two <see cref="IterIterable"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is IterIterable other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="IterIterable"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Iterator.GetHashCode(), IterableType.GetHashCode());
    }

    /// <summary>
    ///     Checks if two <see cref="IterIterable"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(IterIterable left, IterIterable right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="IterIterable"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(IterIterable left, IterIterable right)
    {
        return !(left == right);
    }
}

// IIterIterable Interface
public unsafe partial struct IterIterable : IIterIterable
{
    ref IterIterable IIterIterable.Underlying => ref this;
    ref ecs_iter_t IIterIterable.Iterator => ref Iterator;
    IterableType IIterIterable.IterableType => IterableType;
}

// IIterableBase Interface
public unsafe partial struct IterIterable : IIterableBase
{
    /// <inheritdoc cref="IIterableBase.World"/>
    ecs_world_t* IIterableBase.World => Iterator.world;

    /// <inheritdoc cref="IIterableBase.GetIter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ecs_iter_t GetIter(World world = default)
    {
        if (world == null)
            return Iterator;

        return world == null
            ? Iterator
            : Iterator with { world = world };
    }

    /// <inheritdoc cref="IIterableBase.GetNext"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNext(Iter it)
    {
        return IterableType switch
        {
            IterableType.Query => ecs_query_next(it),
            IterableType.Worker => ecs_worker_next(it),
            IterableType.Page => ecs_page_next(it),
            _ => throw new Ecs.ErrorException()
        };
    }
}

// IIterable Interface
public unsafe partial struct IterIterable : IIterable
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
        return new IterIterable(GetIter(world), IterableType);
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

    /// <inheritdoc cref="IIterable.SetVar(int, ulong)"/>
    IterIterable IIterable.SetVar(int varId, ulong value)
    {
        return Iter().SetVar(varId, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, ulong)"/>
    IterIterable IIterable.SetVar(string name, ulong value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, ecs_table_t*)"/>
    IterIterable IIterable.SetVar(string name, ecs_table_t* value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, ecs_table_range_t)"/>
    IterIterable IIterable.SetVar(string name, ecs_table_range_t value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetVar(string, Table)"/>
    IterIterable IIterable.SetVar(string name, Table value)
    {
        return Iter().SetVar(name, value);
    }

    /// <inheritdoc cref="IIterable.SetGroup(ulong)"/>
    IterIterable IIterable.SetGroup(ulong groupId)
    {
        return Iter().SetGroup(groupId);
    }

    /// <inheritdoc cref="IIterable.SetGroup{T}()"/>
    IterIterable IIterable.SetGroup<T>()
    {
        return Iter().SetGroup<T>();
    }
}
