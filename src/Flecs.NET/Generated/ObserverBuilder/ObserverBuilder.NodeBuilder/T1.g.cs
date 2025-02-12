// /_/src/Flecs.NET/Generated/ObserverBuilder/ObserverBuilder.NodeBuilder/T1.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/ObserverBuilder.cs
using System;
using Flecs.NET.Core.BindingContext;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public unsafe partial struct ObserverBuilder<T0>
{
    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Run(Ecs.RunCallback callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetRun(callback, (delegate*<ecs_iter_t*, void>)&Functions.RunCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Run(delegate*<Iter, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetRun(callback, (delegate*<ecs_iter_t*, void>)&Functions.RunCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Iter(Ecs.IterFieldCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, false);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.IterFieldCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Iter(Ecs.IterSpanCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, false);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.IterSpanCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Iter(Ecs.IterPointerCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, false);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.IterPointerCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Iter(delegate*<Iter, Field<T0>, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, false);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.IterFieldCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Iter(delegate*<Iter, Span<T0>, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, false);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.IterSpanCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Iter(delegate*<Iter, T0*, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, false);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.IterPointerCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(Ecs.EachRefCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachRefCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(Ecs.EachEntityRefCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachEntityRefCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(Ecs.EachIterRefCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachIterRefCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(delegate*<ref T0, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachRefCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(delegate*<Entity, ref T0, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachEntityRefCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(delegate*<Iter, int, ref T0, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(true);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachIterRefCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(Ecs.EachPointerCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachPointerCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(Ecs.EachEntityPointerCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachEntityPointerCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(Ecs.EachIterPointerCallback<T0> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachIterPointerCallbackDelegate<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(delegate*<T0*, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachPointerCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(delegate*<Entity, T0*, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachEntityPointerCallbackPointer<T0>).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0> Each(delegate*<Iter, int, T0*, void> callback)
    {
        TypeHelper<T0>.AssertReferenceTypes(false);
        TypeHelper<T0>.AssertSparseTypes(World, true);
        return SetCallback(callback, (delegate*<ecs_iter_t*, void>)&Functions.EachIterPointerCallbackPointer<T0>).Build();
    }
}