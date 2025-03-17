// /_/src/Flecs.NET/Generated/Invoker/IterJob/Iterable/T4.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Invoker.cs
using System;
using System.Runtime.CompilerServices;
using System.Threading;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Invoker
{
    public static void IterJob<T, T0, T1, T2, T3>(ref T iterable, Ecs.IterFieldCallback<T0, T1, T2, T3> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3>.IterFieldCallbackDelegate
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3>.IterFieldCallbackDelegate state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3>(ref T iterable, Ecs.IterSpanCallback<T0, T1, T2, T3> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3>.IterSpanCallbackDelegate
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3>.IterSpanCallbackDelegate state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3>(ref T iterable, Ecs.IterPointerCallback<T0, T1, T2, T3> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3>.IterPointerCallbackDelegate
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3>.IterPointerCallbackDelegate state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3>(ref T iterable, delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, void> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3>.IterFieldCallbackPointer
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3>.IterFieldCallbackPointer state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3>(ref T iterable, delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, void> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3>.IterSpanCallbackPointer
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3>.IterSpanCallbackPointer state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }

    public static void IterJob<T, T0, T1, T2, T3>(ref T iterable, delegate*<Iter, T0*, T1*, T2*, T3*, void> callback) where T : unmanaged, IIterableBase
    {
        World world = iterable.World;
    
        Ecs.Assert(!world.IsDeferred() && !world.IsReadOnly(), "Cannot run multi-threaded query when world is already in deferred or readonly mode.");
    
        ecs_readonly_begin(world, true);
    
        int stageCount = world.GetStageCount();
    
        using CountdownEvent countdown = new(stageCount);
    
        for (int i = 0; i < stageCount; i++)
        {
            ThreadPool.QueueUserWorkItem(Work, new WorkerState<T0, T1, T2, T3>.IterPointerCallbackPointer
            {
                Countdown = countdown,
                Worker = new WorkerIterable<T0, T1, T2, T3>(iterable.GetIter(world.GetStage(i)), i, stageCount),
                Callback = callback
            }, true);
        }
    
        countdown.Wait();
    
        ecs_readonly_end(world);
    
        return;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Work(WorkerState<T0, T1, T2, T3>.IterPointerCallbackPointer state)
        {
            state.Worker.Iter(state.Callback);
            state.Countdown.Signal();
        }
    }
}