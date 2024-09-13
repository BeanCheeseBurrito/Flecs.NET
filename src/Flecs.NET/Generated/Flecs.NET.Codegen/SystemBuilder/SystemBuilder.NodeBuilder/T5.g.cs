﻿// SystemBuilder.NodeBuilder/T5.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/SystemBuilder.cs
using System;
using Flecs.NET.Core.BindingContext;

namespace Flecs.NET.Core;

public unsafe partial struct SystemBuilder<T0, T1, T2, T3, T4>
{
    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Run(Ecs.RunCallback callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetRun(callback, Pointers<T0, T1, T2, T3, T4>.RunCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Run callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Run(delegate*<Iter, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetRun((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.RunCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Iter(Ecs.IterFieldCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.IterFieldCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Iter(Ecs.IterSpanCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.IterSpanCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Iter(Ecs.IterPointerCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.IterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Iter(delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, Field<T4>, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.IterFieldCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Iter(delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, Span<T4>, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.IterSpanCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Iter(delegate*<Iter, T0*, T1*, T2*, T3*, T4*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.IterPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(Ecs.EachRefCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.EachRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(Ecs.EachEntityRefCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.EachEntityRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(Ecs.EachIterRefCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.EachIterRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(delegate*<ref T0, ref T1, ref T2, ref T3, ref T4, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.EachRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(delegate*<Entity, ref T0, ref T1, ref T2, ref T3, ref T4, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.EachEntityRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(delegate*<Iter, int, ref T0, ref T1, ref T2, ref T3, ref T4, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(true);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.EachIterRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(Ecs.EachPointerCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.EachPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(Ecs.EachEntityPointerCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.EachEntityPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(Ecs.EachIterPointerCallback<T0, T1, T2, T3, T4> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback(callback, Pointers<T0, T1, T2, T3, T4>.EachIterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(delegate*<T0*, T1*, T2*, T3*, T4*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.EachPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(delegate*<Entity, T0*, T1*, T2*, T3*, T4*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.EachEntityPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="System"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    public System<T0, T1, T2, T3, T4> Each(delegate*<Iter, int, T0*, T1*, T2*, T3*, T4*, void> callback)
    {
        TypeHelper<T0, T1, T2, T3, T4>.AssertReferenceTypes(false);
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3, T4>.EachIterPointerCallbackPointer).Build();
    }
}