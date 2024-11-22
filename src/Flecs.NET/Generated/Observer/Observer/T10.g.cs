// /_/src/Flecs.NET/Generated/Observer/Observer/T10.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Observer.cs
#nullable enable

using System;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A type-safe wrapper around <see cref="Observer"/> that takes 16 type arguments.
/// </summary>
/// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam>
public unsafe partial struct Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IDisposable, IEquatable<Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>, IEntity<Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>
{
    private Observer _observer;

    /// <inheritdoc cref="Observer.Entity"/>
    public ref Entity Entity => ref _observer.Entity;

    /// <inheritdoc cref="Observer.Id"/>
    public ref Id Id => ref _observer.Id;

    /// <inheritdoc cref="Observer.World"/>
    public ref ecs_world_t* World => ref _observer.World;
    
    /// <summary>
    ///     Creates an observer with the provided observer.
    /// </summary>
    /// <param name="observer">The observer.</param>
    public Observer(Observer observer)
    {
        TypeHelper<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.AssertNoTags();
        _observer = observer;
    }

    /// <inheritdoc cref="Observer(ecs_world_t*, ulong)"/>
    public Observer(ecs_world_t* world, ulong entity)
    {
        TypeHelper<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.AssertNoTags();
        _observer = new Observer(world, entity);
    }

    /// <inheritdoc cref="Observer(Core.Entity)"/>
    public Observer(Entity entity)
    {
        TypeHelper<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.AssertNoTags();
        _observer = new Observer(entity);
    }

    /// <inheritdoc cref="Observer.Dispose"/>
    public void Dispose()
    {
        _observer.Dispose();
    }

    /// <inheritdoc cref="Observer.Ctx{T}(T)"/>
    public void Ctx<T>(T value)
    {
        _observer.Ctx(ref value);
    }
    
    /// <inheritdoc cref="Observer.Ctx{T}(T, Ecs.UserContextFinish{T})"/>
    public void Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
    {
        _observer.Ctx(ref value, callback);
    }
    
    /// <inheritdoc cref="Observer.Ctx{T}(T, Ecs.UserContextFinish{T})"/>
    public void Ctx<T>(T value, delegate*<ref T, void> callback)
    {
        _observer.Ctx(ref value, callback);
    }
    
    /// <inheritdoc cref="Observer.Ctx{T}(ref T)"/>
    public void Ctx<T>(ref T value)
    {
        _observer.Ctx(ref value);
    }
    
    /// <inheritdoc cref="Observer.Ctx{T}(ref T, Ecs.UserContextFinish{T})"/>
    public void Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
    {
        _observer.Ctx(ref value, callback);
    }
    
    /// <inheritdoc cref="Observer.Ctx{T}(ref T, Ecs.UserContextFinish{T})"/>
    public void Ctx<T>(ref T value, delegate*<ref T, void> callback)
    {
        _observer.Ctx(ref value, callback);
    }
    
    /// <inheritdoc cref="Observer.Ctx{T}()"/>
    public ref T Ctx<T>()
    {
        return ref _observer.Ctx<T>();
    }

    /// <inheritdoc cref="Observer.Query()"/>
    public Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Query()
    {
        return new Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(_observer.Query());
    }

    /// <inheritdoc cref="Observer.ToUInt64"/>
    public static implicit operator ulong(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> observer)
    {
        return ToUInt64(observer);
    }

    /// <inheritdoc cref="Observer.ToId"/>
    public static implicit operator Id(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> observer)
    {
        return ToId(observer);
    }

    /// <inheritdoc cref="Observer.ToEntity(Observer)"/>
    public static implicit operator Entity(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> observer)
    {
        return ToEntity(observer);
    }

    /// <inheritdoc cref="Observer.ToUInt64"/>
    public static ulong ToUInt64(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> observer)
    {
        return observer.Entity;
    }

    /// <inheritdoc cref="Observer.ToId"/>
    public static Id ToId(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> observer)
    {
        return observer.Id;
    }

    /// <inheritdoc cref="Observer.ToEntity(Observer)"/>
    public static Entity ToEntity(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> observer)
    {
        return observer.Entity;
    }

    /// <inheritdoc cref="Observer.Equals(Observer)"/>
    public bool Equals(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> other)
    {
        return _observer == other._observer;
    }

    /// <inheritdoc cref="Observer.Equals(object)"/>
    public override bool Equals(object? obj)
    {
        return obj is Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> other && Equals(other);
    }

    /// <inheritdoc cref="Observer.GetHashCode()"/>
    public override int GetHashCode()
    {
        return _observer.GetHashCode();
    }

    /// <inheritdoc cref="Observer.op_Equality"/>
    public static bool operator ==(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> left, Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc cref="Observer.op_Inequality"/>
    public static bool operator !=(Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> left, Observer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> right)
    {
        return !(left == right);
    }

    /// <inheritdoc cref="Observer.ToString"/>
    public override string ToString()
    {
        return _observer.ToString();
    }
}