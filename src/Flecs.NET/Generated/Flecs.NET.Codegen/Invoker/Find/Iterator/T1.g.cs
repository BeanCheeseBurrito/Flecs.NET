﻿// Find/Iterator/T1.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Invoker.cs
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Invoker
{
    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, Ecs.FindRefCallback<T0> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(ref Managed.GetTypeRef<T0>(&pointer0[i])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(ref Managed.GetTypeRef<T0>(&pointer0[i * step0])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, Ecs.FindEntityRefCallback<T0> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i * step0])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, Ecs.FindIterRefCallback<T0> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i * step0])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, delegate*<ref T0, bool> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(ref Managed.GetTypeRef<T0>(&pointer0[i])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(ref Managed.GetTypeRef<T0>(&pointer0[i * step0])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, delegate*<Entity, ref T0, bool> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i * step0])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, delegate*<Iter, int, ref T0, bool> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, ref Managed.GetTypeRef<T0>(&pointer0[i * step0])))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, Ecs.FindPointerCallback<T0> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(&pointer0[i]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(&pointer0[i * step0]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, Ecs.FindEntityPointerCallback<T0> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i * step0]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, Ecs.FindIterPointerCallback<T0> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, &pointer0[i]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, &pointer0[i * step0]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, delegate*<T0*, bool> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(&pointer0[i]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(&pointer0[i * step0]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, delegate*<Entity, T0*, bool> callback)
    {
        int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i * step0]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }

    /// <summary>
    ///     Iterates over an Iter object using the provided .Find callback.
    /// </summary>
    /// <param name="it">The iter object.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam>
    public static Entity Find<T0>(Iter it, delegate*<Iter, int, T0*, bool> callback)
    {
        int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;
        
        T0* pointer0 = it.GetPointer<T0>(0);
            
        Ecs.TableLock(it);
        
        Entity result = default;
            
        if (it.IsLinear())
        {
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, &pointer0[i]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
        else
        {
            int step0 = it.Step<T0>(0);
            for (int i = 0; i < count; i++)
            {
                if (!callback(it, i, &pointer0[i * step0]))
                    continue;
                    
                result = new Entity(it.Handle->world, it.Handle->entities[i]);
                break;
            }
        }
            
        Ecs.TableUnlock(it);
        
        return result;
    }
}