// /_/src/Flecs.NET/Generated/Invoker/FetchComponents/T5.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Invoker.cs
using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Invoker
{
    /// <summary>
    ///     Invokes the provided Read callback.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The entity.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam>
    /// <returns>True if the entity has the specified components.</returns>
    public static bool Read<T0, T1, T2, T3, T4>(ecs_world_t* world, ulong entity, Ecs.ReadRefCallback<T0, T1, T2, T3, T4> callback)
    {
        ecs_record_t* record = ecs_read_begin(world, entity);

        if (record == null)
            return false;

        ecs_table_t *table = record->table;

        if (table == null)
            return false;

        void** pointers = stackalloc void*[5];
        bool hasComponents = Ecs.GetPointers<T0, T1, T2, T3, T4>(world, entity, record, table, pointers);

        if (hasComponents)
            callback(ref Managed.GetTypeRef<T0>(pointers[0]), ref Managed.GetTypeRef<T1>(pointers[1]), ref Managed.GetTypeRef<T2>(pointers[2]), ref Managed.GetTypeRef<T3>(pointers[3]), ref Managed.GetTypeRef<T4>(pointers[4]));

        ecs_read_end(record);

        return hasComponents;
    }

    /// <summary>
    ///     Invokes the provided Write callback.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The entity.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam>
    /// <returns>True if the entity has the specified components.</returns>
    public static bool Write<T0, T1, T2, T3, T4>(ecs_world_t* world, ulong entity, Ecs.WriteRefCallback<T0, T1, T2, T3, T4> callback)
    {
        ecs_record_t* record = ecs_write_begin(world, entity);

        if (record == null)
            return false;

        ecs_table_t *table = record->table;

        if (table == null)
            return false;

        void** pointers = stackalloc void*[5];
        bool hasComponents = Ecs.GetPointers<T0, T1, T2, T3, T4>(world, entity, record, table, pointers);

        if (hasComponents)
            callback(ref Managed.GetTypeRef<T0>(pointers[0]), ref Managed.GetTypeRef<T1>(pointers[1]), ref Managed.GetTypeRef<T2>(pointers[2]), ref Managed.GetTypeRef<T3>(pointers[3]), ref Managed.GetTypeRef<T4>(pointers[4]));

        ecs_write_end(record);

        return hasComponents;
    }

    /// <summary>
    ///     Invokes the provided Insert callback.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The entity.</param>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam>
    /// <returns></returns>
    public static bool Insert<T0, T1, T2, T3, T4>(World world, ulong entity, Ecs.InsertRefCallback<T0, T1, T2, T3, T4> callback)
    {
        ulong* ids = stackalloc ulong[] { Type<T0>.Id(world), Type<T1>.Id(world), Type<T2>.Id(world), Type<T3>.Id(world), Type<T4>.Id(world) };
        void** pointers = stackalloc void*[5];
        
        ecs_table_t* table = null;
    
        if (!world.IsDeferred())
        {
            Ecs.Assert(!world.IsStage(), nameof(ECS_INVALID_PARAMETER));
    
            ecs_record_t* record = ecs_record_find(world, entity);
            
            if (record != null)
                table = record->table;
    
            ecs_table_t* prev = table;
            ecs_table_t* next = null;
            
            ulong* added = stackalloc ulong[5];
            int addedCount = 0;
            
            for (int i = 0; i < 5; i++)
            {
                next = ecs_table_add_id(world, prev, ids[i]);
                
                if (prev != next)
                    added[addedCount++] = ids[i];
                    
                prev = next;
            }

            if (table != next)
            {
                ecs_type_t type = default;
                type.array = added;
                type.count = addedCount;
                ecs_commit(world, entity, record, next, &type, null);
                table = next;
            }
    
            if (!Ecs.GetPointers<T0, T1, T2, T3, T4>(world, entity, record, table, pointers))
                Ecs.Error(nameof(ECS_INTERNAL_ERROR));
    
            Ecs.TableLock(world, table);
        }
        else
        {
            Ecs.EnsurePointers<T0, T1, T2, T3, T4>(world, entity, pointers);
        }
    
        callback(ref Managed.GetTypeRef<T0>(pointers[0]), ref Managed.GetTypeRef<T1>(pointers[1]), ref Managed.GetTypeRef<T2>(pointers[2]), ref Managed.GetTypeRef<T3>(pointers[3]), ref Managed.GetTypeRef<T4>(pointers[4]));
    
        if (!world.IsDeferred())
            Ecs.TableUnlock(world, table);
    
        ecs_modified_id(world, entity, ids[0]); ecs_modified_id(world, entity, ids[1]); ecs_modified_id(world, entity, ids[2]); ecs_modified_id(world, entity, ids[3]); ecs_modified_id(world, entity, ids[4]);
    
        return true;
    }
}