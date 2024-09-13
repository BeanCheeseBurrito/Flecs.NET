﻿// ObserverBuilder.NodeBuilder/T4.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/ObserverBuilder.cs
using System;
using Flecs.NET.Core.BindingContext;

namespace Flecs.NET.Core;

public unsafe partial struct ObserverBuilder<T0, T1, T2, T3>
{
    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Run(Ecs.RunCallback callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetRun(callback, Pointers<T0, T1, T2, T3>.RunCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Run(delegate*<Iter, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetRun((IntPtr)callback, Pointers<T0, T1, T2, T3>.RunCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Iter(Ecs.IterFieldCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.IterFieldCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Iter(Ecs.IterSpanCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.IterSpanCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Iter(Ecs.IterPointerCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.IterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Iter(delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.IterFieldCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Iter(delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.IterSpanCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Iter(delegate*<Iter, T0*, T1*, T2*, T3*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.IterPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(Ecs.EachRefCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(Ecs.EachEntityRefCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachEntityRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(Ecs.EachIterRefCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachIterRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(delegate*<ref T0, ref T1, ref T2, ref T3, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(delegate*<Entity, ref T0, ref T1, ref T2, ref T3, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachEntityRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(delegate*<Iter, int, ref T0, ref T1, ref T2, ref T3, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachIterRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(Ecs.EachPointerCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(Ecs.EachEntityPointerCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachEntityPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(Ecs.EachIterPointerCallback<T0, T1, T2, T3> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachIterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(delegate*<T0*, T1*, T2*, T3*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(delegate*<Entity, T0*, T1*, T2*, T3*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachEntityPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public Observer<T0, T1, T2, T3> Each(delegate*<Iter, int, T0*, T1*, T2*, T3*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachIterPointerCallbackPointer).Build();
    }
}