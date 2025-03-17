using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around ecs_query_t.
/// </summary>
public unsafe partial struct Query : IEquatable<Query>, IDisposable
{
    /// <summary>
    ///     The underlying <see cref="ecs_query_t"/>* handle.
    /// </summary>
    public ecs_query_t* Handle;

    /// <summary>
    ///     Creates a query from a handle.
    /// </summary>
    /// <param name="query">The query pointer.</param>
    public Query(ecs_query_t* query)
    {
        Handle = query;
    }

    /// <summary>
    ///     Creates a query from an entity.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The query entity.</param>
    public Query(ecs_world_t* world, ulong entity) : this (new Entity(world, entity))
    {
    }

    /// <summary>
    ///     Creates a query from an entity.
    /// </summary>
    /// <param name="entity">The query entity.</param>
    public Query(Entity entity)
    {
        if (entity != 0)
        {
            EcsPoly* poly = entity.GetPtr<EcsPoly>(EcsQuery);

            if (poly != null)
            {
                Handle = (ecs_query_t*)poly->poly;
                return;
            }
        }

        ecs_query_desc_t desc = default;
        Handle = ecs_query_init(entity.World, &desc);
    }

    /// <summary>
    ///     Disposes query.
    /// </summary>
    public void Dispose()
    {
        Destruct();
    }

    /// <summary>
    ///     Destructs query and cleans up resources.
    /// </summary>
    public void Destruct()
    {
        if (Handle == null)
            return;

        ecs_query_fini(Handle);
        Handle = null;
    }

    /// <summary>
    ///     Returns the entity associated with the query.
    /// </summary>
    /// <returns></returns>
    public Entity Entity()
    {
        return new Entity(Handle->world, Handle->entity);
    }

    /// <summary>
    ///     Returns the query handle.
    /// </summary>
    /// <returns></returns>
    public ecs_query_t* CPtr()
    {
        return Handle;
    }

    /// <summary>
    ///     Returns whether the query data changed since the last iteration.
    /// </summary>
    /// <returns></returns>
    public bool Changed()
    {
        return ecs_query_changed(Handle);
    }

    /// <summary>
    ///     Get info for group.
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public ecs_query_group_info_t* GroupInfo(ulong groupId)
    {
        return ecs_query_get_group_info(Handle, groupId);
    }

    /// <summary>
    ///     Get context for group.
    /// </summary>
    /// <param name="group">The group id.</param>
    /// <returns></returns>
    public ref T GroupCtx<T>(ulong group)
    {
        ecs_query_group_info_t* groupInfo = GroupInfo(group);
        return ref groupInfo == null || groupInfo->ctx == null
            ? ref Unsafe.NullRef<T>()
            : ref ((UserContext*)groupInfo->ctx)->Get<T>();
    }

    /// <summary>
    ///     Iterates terms with the provided callback.
    /// </summary>
    /// <param name="callback"></param>
    public void EachTerm(Ecs.TermCallback callback)
    {
        for (int i = 0; i < Handle->term_count; i++)
        {
            Term term = new Term(Handle->world, Handle->terms[i]);
            callback(ref term);
        }
    }

    /// <summary>
    ///     Gets term at provided index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Term Term(int index)
    {
        Ecs.Assert(index < Handle->term_count, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
        return new Term(Handle->world, Handle->terms[index]);
    }

    /// <summary>
    ///     Gets term count.
    /// </summary>
    /// <returns></returns>
    public int TermCount()
    {
        return Handle->term_count;
    }

    /// <summary>
    ///     Gets field count.
    /// </summary>
    /// <returns></returns>
    public int FieldCount()
    {
        return Handle->field_count;
    }

    /// <summary>
    ///     Searches for a variable by name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int FindVar(string name)
    {
        using NativeString nativeName = (NativeString)name;
        return ecs_query_find_var(Handle, nativeName);
    }

    /// <summary>
    ///     Returns the string of the query.
    /// </summary>
    /// <returns></returns>
    public string Str()
    {
        return NativeString.GetStringAndFree(ecs_query_str(Handle));
    }

    /// <summary>
    ///     Returns a string representing the query plan.
    /// </summary>
    /// <returns></returns>
    public string Plan()
    {
        return NativeString.GetStringAndFree(ecs_query_plan(Handle));
    }

    /// <summary>
    ///     Converts a <see cref="Query"/> instance to an <see cref="ecs_query_t"/>*.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static ecs_query_t* To(Query query)
    {
        return query.Handle;
    }

    /// <summary>
    ///     Returns true if query handle is not a null pointer, otherwise return false.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static bool ToBoolean(Query query)
    {
        return query.Handle != null;
    }

    /// <summary>
    ///     Converts a <see cref="Query"/> instance to an <see cref="ecs_query_t"/>*.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static implicit operator ecs_query_t*(Query query)
    {
        return To(query);
    }

    /// <summary>
    ///     Returns true if query handle is not a null pointer, otherwise return false.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static implicit operator bool(Query query)
    {
        return ToBoolean(query);
    }

    /// <summary>
    ///     Checks if two <see cref="Query"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Query other)
    {
        return Handle == other.Handle;
    }

    /// <summary>
    ///     Checks if two <see cref="Query"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is Query other && Equals(other);
    }

    /// <summary>
    ///     Returns the hash code of the <see cref="Query"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Handle->GetHashCode();
    }

    /// <summary>
    ///     Checks if two <see cref="Query"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Query left, Query right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="Query"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Query left, Query right)
    {
        return !(left == right);
    }
}

// Flecs.NET Extensions
public unsafe partial struct Query
{
    /// <summary>
    ///     Gets the query's world or stage.
    /// </summary>
    /// <returns></returns>
    public World World()
    {
        return Handle->world;
    }

    /// <summary>
    ///     Gets the query's actual world.
    /// </summary>
    /// <returns></returns>
    public World RealWorld()
    {
        return Handle->real_world;
    }
}

// IQuery Interface
public unsafe partial struct Query : IQuery
{
    ref Query IQuery.Underlying => ref this;
    ecs_query_t* IQuery.Handle => Handle;
}


// IIterableBase Interface
public unsafe partial struct Query : IIterableBase
{
    /// <inheritdoc cref="IIterableBase.World"/>
    ecs_world_t* IIterableBase.World => Handle->world;

    /// <inheritdoc cref="IIterableBase.GetIter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ecs_iter_t GetIter(World world = default)
    {
        Ecs.Assert(Handle != null, "Cannot iterate invalid query.");

        if (world == null)
            world = Handle->world;

        return ecs_query_iter(world, Handle);
    }

    /// <inheritdoc cref="IIterableBase.GetNext"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNext(Iter it)
    {
        return ecs_query_next(it);
    }
}

// IIterable Interface
public unsafe partial struct Query : IIterable
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
        return new IterIterable(GetIter(world), IterableType.Query);
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
