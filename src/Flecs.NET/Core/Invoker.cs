using System;
using System.Threading;

namespace Flecs.NET.Core;

/// <summary>
///     A static class for invoking user callbacks.
/// </summary>
internal static unsafe class Invoker<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
    private static readonly int TypeCount = Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Count;

    internal static void Job<TIterable, TInvoker>(ref TIterable iterable, InvokerCallback callback)
        where TIterable : IIterableBase
        where TInvoker : IJobInvoker
    {
        World world = ecs_get_world(iterable.World);

        bool isReadonly = world.IsReadOnly();

        Ecs.Assert(isReadonly || !world.IsDeferred(), "Cannot run multi-threaded query when world is already in deferred mode.");

        if (!isReadonly)
            ecs_readonly_begin(world, true);

        int stageCount = world.GetStageCount();

        using CountdownEvent countdown = new(stageCount);

        for (int i = 0; i < stageCount; i++)
            ThreadPool.QueueUserWorkItem(ExecuteJob, new JobState(callback, countdown, new WorkerIterable(iterable.GetIter(world.GetStage(i)), i, stageCount)), true);

        countdown.Wait();

        if (!isReadonly)
            ecs_readonly_end(world);

        if (!Ecs.Exceptions.IsEmpty)
        {
            AggregateException exception = Ecs.Exception(Ecs.Exceptions, "One or more threads have raised an exception while iterating this query.");
            Ecs.Exceptions.Clear();
            throw exception;
        }

        return;

        static void ExecuteJob(JobState state)
        {
            try
            {
                TInvoker.Invoke(state);
            }
            catch (Exception exception)
            {
                Ecs.Exceptions.Enqueue(exception);
            }
            finally
            {
                state.Countdown.Signal();
            }
        }
    }

    internal static void Run<TInvoker>(Iter it, InvokerCallback callback)
        where TInvoker : IRunInvoker
    {
        it.Handle->flags &= ~EcsIterIsValid;

        try
        {
            TInvoker.Invoke(it, callback);
        }
        catch (Exception)
        {
            it.Fini();
            throw;
        }
    }

    internal static void Run<TIterable, TInvoker>(ref TIterable iterable, InvokerCallback callback)
        where TIterable : IIterableBase
        where TInvoker : IRunInvoker
    {
        ecs_iter_t iter = iterable.GetIter();
        Run<TInvoker>(&iter, callback);
    }

    internal static void Iter<TInvoker>(Iter it, InvokerCallback callback)
        where TInvoker : IIterInvoker
    {
        it.AssertFields<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        Ecs.TableLock(it);

        try
        {
            TInvoker.Invoke(it, callback);
        }
        catch (Exception)
        {
            it.Fini();
            throw;
        }

        Ecs.TableUnlock(it);
    }

    internal static void Iter<TIterable, TInvoker>(ref TIterable iterable, InvokerCallback callback)
        where TIterable : IIterableBase
        where TInvoker : IIterInvoker
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Iter<TInvoker>(&iter, callback);
    }

    internal static void Each<TInvoker>(Iter it, InvokerCallback callback)
        where TInvoker : IEachInvoker
    {
        it.AssertFields<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        IterationTechnique flags = it.GetIterationTechnique(TypeCount);

        Ecs.TableLock(it);

        int count = it.Handle->entities == null ? 1 : it.Handle->count;

        ulong empty = 0;
        if (it.Handle->entities == null)
            it.Handle->entities = &empty;

        try
        {
            if (flags == IterationTechnique.None)
                TInvoker.Invoke<IFieldGetter.Self>(it.GetFields(TypeCount), count, callback);
            else if (flags == IterationTechnique.Shared)
                TInvoker.Invoke<IFieldGetter.Shared>(it.GetFields(TypeCount), count, callback);
            else if (flags == IterationTechnique.Sparse)
                TInvoker.Invoke<IFieldGetter.Sparse>(it.GetFields(TypeCount), count, callback);
            else if (flags == (IterationTechnique.Sparse | IterationTechnique.Shared))
                TInvoker.Invoke<IFieldGetter.SparseShared>(it.GetFields(TypeCount), count, callback);
        }
        catch (Exception)
        {
            it.Fini();
            throw;
        }

        Ecs.TableUnlock(it);
    }

    internal static void Each<TIterable, TInvoker>(ref TIterable iterable, InvokerCallback callback)
        where TIterable : IIterableBase
        where TInvoker : IEachInvoker
    {
        ecs_iter_t iter = iterable.GetIter();
        while (iterable.GetNext(&iter))
            Each<TInvoker>(&iter, callback);
    }

    internal static Entity Find<TInvoker>(Iter it, InvokerCallback callback)
        where TInvoker : IFindInvoker
    {
        it.AssertFields<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        IterationTechnique flags = it.GetIterationTechnique(TypeCount);

        Ecs.TableLock(it);

        int count = it.Handle->entities == null ? 1 : it.Handle->count;

        ulong empty = 0;
        if (it.Handle->entities == null)
            it.Handle->entities = &empty;

        Entity result = default;

        try
        {
            if (flags == IterationTechnique.None)
                result = TInvoker.Invoke<IFieldGetter.Self>(it.GetFields(TypeCount), count, callback);
            else if (flags == IterationTechnique.Shared)
                result = TInvoker.Invoke<IFieldGetter.Shared>(it.GetFields(TypeCount), count, callback);
            else if (flags == IterationTechnique.Sparse)
                result = TInvoker.Invoke<IFieldGetter.Sparse>(it.GetFields(TypeCount), count, callback);
            else if (flags == (IterationTechnique.Sparse | IterationTechnique.Shared))
                result = TInvoker.Invoke<IFieldGetter.SparseShared>(it.GetFields(TypeCount), count, callback);
        }
        catch (Exception)
        {
            it.Fini();
            throw;
        }

        Ecs.TableUnlock(it);

        return result;
    }

    internal static Entity Find<TIterable, TInvoker>(ref TIterable iterable, InvokerCallback callback)
        where TIterable : IIterableBase
        where TInvoker : IFindInvoker
    {
        Entity result = default;

        ecs_iter_t iter = iterable.GetIter();
        while (result == 0 && iterable.GetNext(&iter))
            result = Find<TInvoker>(&iter, callback);

        if (result != 0)
            ecs_iter_fini(&iter);

        return result;
    }

    internal static void Observe<TInvoker>(Iter it, InvokerCallback callback)
        where TInvoker : IObserveInvoker
    {
        TInvoker.Invoke(it, callback);
    }

    internal static bool Read<TInvoker>(Entity entity, InvokerCallback callback)
        where TInvoker : IReadInvoker
    {
        ecs_record_t* record = ecs_read_begin(entity.World, entity);

        if (record == null)
            return false;

        ecs_table_t *table = record->table;

        if (table == null)
            return false;

        void** pointers = stackalloc void*[TypeCount];
        bool hasComponents = Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Get(entity, record, table, pointers);

        if (hasComponents)
            TInvoker.Invoke(pointers, callback);

        ecs_read_end(record);

        return hasComponents;
    }

    internal static bool Write<TInvoker>(Entity entity, InvokerCallback callback)
        where TInvoker : IWriteInvoker
    {
        ecs_record_t* record = ecs_write_begin(entity.World, entity);

        if (record == null)
            return false;

        ecs_table_t *table = record->table;

        if (table == null)
            return false;

        void** pointers = stackalloc void*[TypeCount];
        bool hasComponents = Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Get(entity, record, table, pointers);

        if (hasComponents)
            TInvoker.Invoke(pointers, callback);

        ecs_write_end(record);

        return hasComponents;
    }

    internal static bool Insert<TInvoker>(Entity entity, InvokerCallback callback)
        where TInvoker : IInsertInvoker
    {
        World world = entity.World;

        ulong* ids = stackalloc ulong[TypeCount];
        void** pointers = stackalloc void*[TypeCount];

        Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Ids(world, ids);

        ecs_table_t* table = null;

        if (!world.IsDeferred())
        {
            Ecs.Assert(!world.IsStage(), nameof(ECS_INVALID_PARAMETER));

            ecs_record_t* record = ecs_record_find(world, entity);

            if (record != null)
                table = record->table;

            ecs_table_t* prev = table;
            ecs_table_t* next = null;

            ulong* added = stackalloc ulong[TypeCount];
            int addedCount = 0;

            for (int i = 0; i < TypeCount; i++)
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

            if (!Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Get(entity, record, table, pointers))
                Ecs.Error(nameof(ECS_INTERNAL_ERROR));

            Ecs.TableLock(world, table);
        }
        else
        {
            Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Ensure(entity, pointers);
        }

        TInvoker.Invoke(pointers, callback);

        if (!world.IsDeferred())
            Ecs.TableUnlock(world, table);

        Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Modified(entity, ids);

        return true;
    }
}
