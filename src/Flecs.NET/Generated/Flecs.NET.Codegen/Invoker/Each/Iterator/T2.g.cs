﻿// Each/Iterator/T2.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Invoker.cs
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Invoker
{
    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, Ecs.EachRefCallback<T0, T1> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]));
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]));
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, Ecs.EachEntityRefCallback<T0, T1> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]));
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]));
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, Ecs.EachIterRefCallback<T0, T1> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]));
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]));
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, delegate*<ref T0, ref T1, void> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]));
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]));
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, delegate*<Entity, ref T0, ref T1, void> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]));
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]));
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, delegate*<Iter, int, ref T0, ref T1, void> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]));
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]));
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, Ecs.EachPointerCallback<T0, T1> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(&pointer0[i], &pointer1[i]);
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(&pointer0[i * step0], &pointer1[i * step1]);
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, Ecs.EachEntityPointerCallback<T0, T1> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i], &pointer1[i]);
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i * step0], &pointer1[i * step1]);
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, Ecs.EachIterPointerCallback<T0, T1> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(it, i, &pointer0[i], &pointer1[i]);
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(it, i, &pointer0[i * step0], &pointer1[i * step1]);
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, delegate*<T0*, T1*, void> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(&pointer0[i], &pointer1[i]);
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(&pointer0[i * step0], &pointer1[i * step1]);
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, delegate*<Entity, T0*, T1*, void> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i], &pointer1[i]);
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i * step0], &pointer1[i * step1]);
        }
            
        Ecs.TableUnlock(it);
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Each callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public static void Each<T0, T1>(Iter it, delegate*<Iter, int, T0*, T1*, void> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);
            
        Ecs.TableLock(it);
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
                callback(it, i, &pointer0[i], &pointer1[i]);
        }
        else
        {
            int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);
            for (int i = 0; i < count; i++)
                callback(it, i, &pointer0[i * step0], &pointer1[i * step1]);
        }
            
        Ecs.TableUnlock(it);
    }
}