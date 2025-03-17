// /_/src/Flecs.NET/Generated/Invoker/IterJob/Iterable/T14.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Invoker.cs
using System;
using System.Runtime.CompilerServices;
using System.Threading;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Invoker
{
    public static void IterJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref T iterable, Ecs.IterFieldCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterFieldCallbackDelegate
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterFieldCallbackDelegate state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref T iterable, Ecs.IterSpanCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterSpanCallbackDelegate
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterSpanCallbackDelegate state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref T iterable, Ecs.IterPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterPointerCallbackDelegate
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterPointerCallbackDelegate state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref T iterable, delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, Field<T4>, Field<T5>, Field<T6>, Field<T7>, Field<T8>, Field<T9>, Field<T10>, Field<T11>, Field<T12>, Field<T13>, void> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterFieldCallbackPointer
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterFieldCallbackPointer state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref T iterable, delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>, Span<T10>, Span<T11>, Span<T12>, Span<T13>, void> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterSpanCallbackPointer
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterSpanCallbackPointer state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref T iterable, delegate*<Iter, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, void> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterPointerCallbackPointer
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.IterPointerCallbackPointer state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }
}