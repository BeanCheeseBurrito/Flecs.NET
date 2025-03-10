// /_/src/Flecs.NET/Generated/World/Builders/T3.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/World.cs
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Flecs.NET.Core;

public unsafe partial struct World
{
    public AlertBuilder AlertBuilder<T0, T1, T2>()
    {
        return new AlertBuilder(Handle).With<T0>().With<T1>().With<T2>();
    }

    public AlertBuilder AlertBuilder<T0, T1, T2>(string name)
    {
        return new AlertBuilder(Handle, name).With<T0>().With<T1>().With<T2>();
    }

    public AlertBuilder AlertBuilder<T0, T1, T2>(ulong entity)
    {
        return new AlertBuilder(Handle, entity).With<T0>().With<T1>().With<T2>();
    }

    public Alert Alert<T0, T1, T2>()
    {
        return AlertBuilder<T0, T1, T2>().Build();
    }

    public Alert Alert<T0, T1, T2>(string name)
    {
        return AlertBuilder<T0, T1, T2>(name).Build();
    }

    public Alert Alert<T0, T1, T2>(ulong entity)
    {
        return AlertBuilder<T0, T1, T2>(entity).Build();
    }
    
    public QueryBuilder<T0, T1, T2> QueryBuilder<T0, T1, T2>()
    {
        return new QueryBuilder<T0, T1, T2>(Handle);
    }

    public QueryBuilder<T0, T1, T2> QueryBuilder<T0, T1, T2>(string name)
    {
        return new QueryBuilder<T0, T1, T2>(Handle, name);
    }

    public QueryBuilder<T0, T1, T2> QueryBuilder<T0, T1, T2>(ulong entity)
    {
        return new QueryBuilder<T0, T1, T2>(Handle, entity);
    }

    public Query<T0, T1, T2> Query<T0, T1, T2>()
    {
        return new QueryBuilder<T0, T1, T2>(Handle).Build();
    }

    public Query<T0, T1, T2> Query<T0, T1, T2>(string name)
    {
        return new QueryBuilder<T0, T1, T2>(Handle, name).Build();
    }

    public Query<T0, T1, T2> Query<T0, T1, T2>(ulong entity)
    {
        return new QueryBuilder<T0, T1, T2>(Handle, entity).Build();
    }

    public SystemBuilder<T0, T1, T2> System<T0, T1, T2>()
    {
        return new SystemBuilder<T0, T1, T2>(Handle);
    }

    public SystemBuilder<T0, T1, T2> System<T0, T1, T2>(string name)
    {
        return new SystemBuilder<T0, T1, T2>(Handle, name);
    }

    public ObserverBuilder<T0, T1, T2> Observer<T0, T1, T2>()
    {
        return new ObserverBuilder<T0, T1, T2>(Handle);
    }

    public ObserverBuilder<T0, T1, T2> Observer<T0, T1, T2>(string name)
    {
        return new ObserverBuilder<T0, T1, T2>(Handle, name);
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member