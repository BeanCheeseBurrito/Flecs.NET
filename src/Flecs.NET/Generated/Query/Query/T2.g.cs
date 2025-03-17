// /_/src/Flecs.NET/Generated/Query/Query/T2.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Query.cs
#nullable enable

using System;
using System.Runtime.CompilerServices;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A type-safe wrapper around <see cref="Query"/> that takes 2 type arguments.
/// </summary>
/// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
public unsafe partial struct Query<T0, T1> : IDisposable, IEquatable<Query<T0, T1>>
{
    /// <inheritdoc cref="IQuery.Underlying"/>
    public Query Underlying;

    /// <inheritdoc cref="Query.Handle"/>
    public ecs_query_t* Handle => Underlying.Handle;

    /// <inheritdoc cref="Query(ecs_query_t*)"/>
    public Query(ecs_query_t* query)
    {
        TypeHelper<T0, T1>.AssertNoTags();
        Underlying = new Query(query);
    }

    /// <inheritdoc cref="Query(ecs_world_t*, ulong)"/>
    public Query(ecs_world_t* world, ulong entity)
    {
        TypeHelper<T0, T1>.AssertNoTags();
        Underlying = new Query(world, entity);
    }

    /// <inheritdoc cref="Query(Core.Entity)"/>
    public Query(Entity entity)
    {
        TypeHelper<T0, T1>.AssertNoTags();
        Underlying = new Query(entity);
    }

    /// <inheritdoc cref="Query.Dispose()"/>
    public void Dispose()
    {
        Underlying.Dispose();
    }

    /// <inheritdoc cref="Query.Destruct()"/>
    public void Destruct()
    {
        Underlying.Destruct();
    }

    /// <inheritdoc cref="Query.Entity()"/>
    public Entity Entity()
    {
        return Underlying.Entity();
    }

    /// <inheritdoc cref="Query.CPtr()"/>
    public ecs_query_t* CPtr()
    {
        return Underlying.CPtr();
    }

    /// <inheritdoc cref="Query.Changed()"/>
    public bool Changed()
    {
        return Underlying.Changed();
    }

    /// <inheritdoc cref="Query.GroupInfo(ulong)"/>
    public ecs_query_group_info_t* GroupInfo(ulong groupId)
    {
        return Underlying.GroupInfo(groupId);
    }

    /// <inheritdoc cref="Query.GroupCtx{T}(ulong)"/>
    public ref T GroupCtx<T>(ulong group)
    {
        return ref Underlying.GroupCtx<T>(group);
    }

    /// <inheritdoc cref="Query.EachTerm(Ecs.TermCallback)"/>
    public void EachTerm(Ecs.TermCallback callback)
    {
        Underlying.EachTerm(callback);
    }

    /// <inheritdoc cref="Query.Term(int)"/>
    public Term Term(int index)
    {
        return Underlying.Term(index);
    }

    /// <inheritdoc cref="Query.TermCount()"/>
    public int TermCount()
    {
        return Underlying.TermCount();
    }

    /// <inheritdoc cref="Query.FieldCount()"/>
    public int FieldCount()
    {
        return Underlying.FieldCount();
    }

    /// <inheritdoc cref="Query.FindVar(string)"/>
    public int FindVar(string name)
    {
        return Underlying.FindVar(name);
    }

    /// <inheritdoc cref="Query.Str()"/>
    public string Str()
    {
        return Underlying.Str();
    }

    /// <inheritdoc cref="Query.Plan()"/>
    public string Plan()
    {
        return Underlying.Plan();
    }

    /// <inheritdoc cref="Query.To(Query)"/>
    public static ecs_query_t* To(Query<T0, T1> query)
    {
        return query.Handle;
    }

    /// <inheritdoc cref="Query.ToBoolean(Query)"/>
    public static bool ToBoolean(Query<T0, T1> query)
    {
        return query.Handle != null;
    }

    /// <inheritdoc cref="Query.To(Query)"/>
    public static implicit operator ecs_query_t*(Query<T0, T1> query)
    {
        return To(query);
    }

    /// <inheritdoc cref="Query.ToBoolean(Query)"/>
    public static implicit operator bool(Query<T0, T1> query)
    {
        return ToBoolean(query);
    }

    /// <inheritdoc cref="Query.Equals(Query)"/>
    public bool Equals(Query<T0, T1> other)
    {
        return Underlying.Equals(other.Underlying);
    }

    /// <inheritdoc cref="Query.Equals(object)"/>
    public override bool Equals(object? obj)
    {
        return obj is Query<T0, T1> other && Equals(other);
    }

    /// <inheritdoc cref="Query.GetHashCode()"/>
    public override int GetHashCode()
    {
        return Underlying.GetHashCode();
    }

    /// <inheritdoc cref="Query.op_Equality"/>
    public static bool operator ==(Query<T0, T1> left, Query<T0, T1> right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc cref="Query.op_Inequality"/>
    public static bool operator !=(Query<T0, T1> left, Query<T0, T1> right)
    {
        return !(left == right);
    }
}

// Flecs.NET Extensions
public unsafe partial struct Query<T0, T1>
{
    /// <inheritdoc cref="Query.World()"/>
    public World World()
    {
        return Underlying.World();
    }

    /// <inheritdoc cref="Query.RealWorld()"/>
    public World RealWorld()
    {
        return Underlying.RealWorld();
    }
}

// IPageIterable Interface
public unsafe partial struct Query<T0, T1> : IQuery
{
    ref Query IQuery.Underlying => ref Underlying;
}

// IIterableBase Interface
public unsafe partial struct Query<T0, T1> : IIterableBase
{
    /// <inheritdoc cref="IIterableBase.World"/>
    ecs_world_t* IIterableBase.World => Handle->world;
    
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

// IIterable<T0, T1> Interface
public unsafe partial struct Query<T0, T1> : IIterable<T0, T1>
{
    /// <inheritdoc cref="Query.Page(int, int)"/>
    public PageIterable<T0, T1> Page(int offset, int limit)
    {
        return new PageIterable<T0, T1>(Underlying.Page(offset, limit));
    }
    
    /// <inheritdoc cref="Query.Worker(int, int)"/>
    public WorkerIterable<T0, T1> Worker(int index, int count)
    {
        return new WorkerIterable<T0, T1>(Underlying.Worker(index, count));
    }

    /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.World)"/>
    public IterIterable<T0, T1> Iter(World world = default)
    {
        return new IterIterable<T0, T1>(Underlying.Iter(world));
    }
    
    /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.Iter)"/>
    public IterIterable<T0, T1> Iter(Iter it)
    {
        return new IterIterable<T0, T1>(Underlying.Iter(it));
    }
    
    /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.Entity)"/>
    public IterIterable<T0, T1> Iter(Entity entity)
    {
        return new IterIterable<T0, T1>(Underlying.Iter(entity));
    }
    
    /// <inheritdoc cref="Query.Count()"/>
    public int Count()
    {
        return Underlying.Count();
    }
    
    /// <inheritdoc cref="Query.IsTrue()"/>
    public bool IsTrue()
    {
        return Underlying.IsTrue();
    }
    
    /// <inheritdoc cref="Query.First()"/>
    public Entity First()
    {
        return Underlying.First();
    }
    
    /// <inheritdoc cref="Query.SetVar(int, ulong)"/>
    public IterIterable<T0, T1> SetVar(int varId, ulong value)
    {
        return new IterIterable<T0, T1>(Underlying.SetVar(varId, value));
    }
    
    /// <inheritdoc cref="Query.SetVar(string, ulong)"/>
    public IterIterable<T0, T1> SetVar(string name, ulong value)
    {
        return new IterIterable<T0, T1>(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="Query.SetVar(string, ecs_table_t*)"/>
    public IterIterable<T0, T1> SetVar(string name, ecs_table_t* value)
    {
        return new IterIterable<T0, T1>(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="Query.SetVar(string, ecs_table_range_t)"/>
    public IterIterable<T0, T1> SetVar(string name, ecs_table_range_t value)
    {
        return new IterIterable<T0, T1>(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="Query.SetVar(string, Table)"/>
    public IterIterable<T0, T1> SetVar(string name, Table value)
    {
        return new IterIterable<T0, T1>(Underlying.SetVar(name, value));
    }
    
    /// <inheritdoc cref="Query.SetGroup(ulong)"/>
    public IterIterable<T0, T1> SetGroup(ulong groupId)
    {
        return new IterIterable<T0, T1>(Underlying.SetGroup(groupId));
    }
    
    /// <inheritdoc cref="Query.SetGroup{T}()"/>
    public IterIterable<T0, T1> SetGroup<T>()
    {
        return new IterIterable<T0, T1>(Underlying.SetGroup<T>());
    }
}