// /_/src/Flecs.NET/Generated/BindingContext/Functions/T15.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/BindingContext.cs
using System;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core.BindingContext;

internal static unsafe partial class Functions
{
    internal static void RunCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (Ecs.RunCallback)context->Callback.Delegate.Target!);
    }

    internal static void RunCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        RunContext* context = (RunContext*)iter->run_ctx;
        Invoker.Run(iter, (delegate*<Iter, void>)context->Callback.Pointer);
    }

    internal static void IterFieldCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterFieldCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void IterSpanCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterSpanCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void IterPointerCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (Ecs.IterPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void IterFieldCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, Field<T4>, Field<T5>, Field<T6>, Field<T7>, Field<T8>, Field<T9>, Field<T10>, Field<T11>, Field<T12>, Field<T13>, Field<T14>, Field<T15>, void>)context->Callback.Pointer);
    }

    internal static void IterSpanCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, Span<T5>, Span<T6>, Span<T7>, Span<T8>, Span<T9>, Span<T10>, Span<T11>, Span<T12>, Span<T13>, Span<T14>, Span<T15>, void>)context->Callback.Pointer);
    }

    internal static void IterPointerCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Iter(iter, (delegate*<Iter, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, T15*, void>)context->Callback.Pointer);
    }

    internal static void EachRefCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void EachEntityRefCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void EachIterRefCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void EachRefCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, ref T14, ref T15, void>)context->Callback.Pointer);
    }

    internal static void EachEntityRefCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, ref T14, ref T15, void>)context->Callback.Pointer);
    }

    internal static void EachIterRefCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, ref T14, ref T15, void>)context->Callback.Pointer);
    }

    internal static void EachPointerCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void EachEntityPointerCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachEntityPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void EachIterPointerCallbackDelegate<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (Ecs.EachIterPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)context->Callback.Delegate.Target!);
    }

    internal static void EachPointerCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, T15*, void>)context->Callback.Pointer);
    }

    internal static void EachEntityPointerCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Entity, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, T15*, void>)context->Callback.Pointer);
    }

    internal static void EachIterPointerCallbackPointer<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ecs_iter_t* iter)
    {
        IteratorContext* context = (IteratorContext*)iter->callback_ctx;
        Invoker.Each(iter, (delegate*<Iter, int, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, T15*, void>)context->Callback.Pointer);
    }
}