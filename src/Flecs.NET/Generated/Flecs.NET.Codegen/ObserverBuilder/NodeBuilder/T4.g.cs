﻿// NodeBuilder/T4.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/ObserverBuilder.cs
using System;
using Flecs.NET.Core.BindingContext;

namespace Flecs.NET.Core;

public unsafe partial struct ObserverBuilder
{
    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Iter<T0, T1, T2, T3>(Ecs.IterFieldCallback<T0, T1, T2, T3> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.IterFieldCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Iter<T0, T1, T2, T3>(Ecs.IterSpanCallback<T0, T1, T2, T3> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.IterSpanCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Iter<T0, T1, T2, T3>(Ecs.IterPointerCallback<T0, T1, T2, T3> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.IterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Iter<T0, T1, T2, T3>(delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, Field<T3>, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.IterFieldCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Iter<T0, T1, T2, T3>(delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, Span<T3>, void> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.IterSpanCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Iter<T0, T1, T2, T3>(delegate*<Iter, T0*, T1*, T2*, T3*, void> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.IterPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(Ecs.EachRefCallback<T0, T1, T2, T3> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(Ecs.EachEntityRefCallback<T0, T1, T2, T3> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachEntityRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(Ecs.EachIterRefCallback<T0, T1, T2, T3> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachIterRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(delegate*<ref T0, ref T1, ref T2, ref T3, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(delegate*<Entity, ref T0, ref T1, ref T2, ref T3, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachEntityRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(delegate*<Iter, int, ref T0, ref T1, ref T2, ref T3, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachIterRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(Ecs.EachPointerCallback<T0, T1, T2, T3> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(Ecs.EachEntityPointerCallback<T0, T1, T2, T3> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachEntityPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(Ecs.EachIterPointerCallback<T0, T1, T2, T3> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1, T2, T3>.EachIterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(delegate*<T0*, T1*, T2*, T3*, void> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(delegate*<Entity, T0*, T1*, T2*, T3*, void> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachEntityPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Observer"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam>
    public Observer Each<T0, T1, T2, T3>(delegate*<Iter, int, T0*, T1*, T2*, T3*, void> callback) where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1, T2, T3>.EachIterPointerCallbackPointer).Build();
    }
}