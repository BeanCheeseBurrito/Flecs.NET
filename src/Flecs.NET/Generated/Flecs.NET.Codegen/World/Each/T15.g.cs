﻿// Each/T15.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/World.cs
namespace Flecs.NET.Core;
             
public unsafe partial struct World
{
    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Ecs.EachRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Ecs.EachEntityRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Ecs.EachIterRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(delegate*<ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, ref T14, void> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(delegate*<Entity, ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, ref T14, void> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(delegate*<Iter, int, ref T0, ref T1, ref T2, ref T3, ref T4, ref T5, ref T6, ref T7, ref T8, ref T9, ref T10, ref T11, ref T12, ref T13, ref T14, void> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Ecs.EachPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Ecs.EachEntityPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Ecs.EachIterPointerCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(delegate*<T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, void> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(delegate*<Entity, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, void> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }

    /// <summary>
    ///     Iterates over the world using the provided .Each callback.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam> <typeparam name="T13">The T13 component type.</typeparam> <typeparam name="T14">The T14 component type.</typeparam>
    public void Each<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(delegate*<Iter, int, T0*, T1*, T2*, T3*, T4*, T5*, T6*, T7*, T8*, T9*, T10*, T11*, T12*, T13*, T14*, void> callback)
    {
        using Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> query = Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        query.Each(callback);   
    }
}