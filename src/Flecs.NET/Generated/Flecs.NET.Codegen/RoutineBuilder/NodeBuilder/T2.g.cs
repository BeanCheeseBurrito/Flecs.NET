﻿// NodeBuilder/T2.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/RoutineBuilder.cs
using System;
using Flecs.NET.Core.BindingContext;

namespace Flecs.NET.Core;

public unsafe partial struct RoutineBuilder
{
    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Iter<T0, T1>(Ecs.IterFieldCallback<T0, T1> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1>.IterFieldCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Iter<T0, T1>(Ecs.IterSpanCallback<T0, T1> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1>.IterSpanCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Iter<T0, T1>(Ecs.IterPointerCallback<T0, T1> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1>.IterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Iter<T0, T1>(delegate*<Iter, Field<T0>, Field<T1>, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.IterFieldCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Iter<T0, T1>(delegate*<Iter, Span<T0>, Span<T1>, void> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.IterSpanCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Iter callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Iter<T0, T1>(delegate*<Iter, T0*, T1*, void> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.IterPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(Ecs.EachRefCallback<T0, T1> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1>.EachRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(Ecs.EachEntityRefCallback<T0, T1> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1>.EachEntityRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(Ecs.EachIterRefCallback<T0, T1> callback) 
    {
        return SetCallback(callback, Pointers<T0, T1>.EachIterRefCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(delegate*<ref T0, ref T1, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.EachRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(delegate*<Entity, ref T0, ref T1, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.EachEntityRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(delegate*<Iter, int, ref T0, ref T1, void> callback) 
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.EachIterRefCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(Ecs.EachPointerCallback<T0, T1> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1>.EachPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(Ecs.EachEntityPointerCallback<T0, T1> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1>.EachEntityPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(Ecs.EachIterPointerCallback<T0, T1> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback(callback, Pointers<T0, T1>.EachIterPointerCallbackDelegate).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(delegate*<T0*, T1*, void> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.EachPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(delegate*<Entity, T0*, T1*, void> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.EachEntityPointerCallbackPointer).Build();
    }

    /// <summary>
    ///     Creates <see cref="Routine"/> with the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam>
    public Routine Each<T0, T1>(delegate*<Iter, int, T0*, T1*, void> callback) where T0 : unmanaged where T1 : unmanaged
    {
        return SetCallback((IntPtr)callback, Pointers<T0, T1>.EachIterPointerCallbackPointer).Build();
    }
}