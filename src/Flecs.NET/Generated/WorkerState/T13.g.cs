// /_/src/Flecs.NET/Generated/WorkerState/T13.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/WorkerState.cs
using System;
using System.Threading;

namespace Flecs.NET.Core;

public static unsafe partial class WorkerState<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
{
    public struct RunCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.RunCallback Callback;
    }

    public struct RunCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Iter, void> Callback;
    }

    public struct IterFieldCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.IterFieldCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct IterSpanCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.IterSpanCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct IterPointerCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.IterPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct IterFieldCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, Field<T4>, Field<T5>, Field<T6>, Field<T7>, Field<T8>, Field<T9>, Field<T10>, Field<T11>, Field<T12>, Field<T13>, void> Callback;
    }

    public struct IterSpanCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>, Span<T10>, Span<T11>, Span<T12>, Span<T13>, void> Callback;
    }

    public struct IterPointerCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Iter, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, void> Callback;
    }

    public struct EachRefCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.EachRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct EachEntityRefCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.EachEntityRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct EachIterRefCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.EachIterRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct EachRefCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, void> Callback;
    }

    public struct EachEntityRefCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Entity, ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, void> Callback;
    }

    public struct EachIterRefCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Iter, int, ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, void> Callback;
    }

    public struct EachPointerCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.EachPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct EachEntityPointerCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.EachEntityPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct EachIterPointerCallbackDelegate
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public Ecs.EachIterPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Callback;
    }

    public struct EachPointerCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, void> Callback;
    }

    public struct EachEntityPointerCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Entity, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, void> Callback;
    }

    public struct EachIterPointerCallbackPointer
    {
        public CountdownEvent Countdown;
        public WorkerIterable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Worker;
        public delegate*<Iter, int, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, void> Callback;
    }
}