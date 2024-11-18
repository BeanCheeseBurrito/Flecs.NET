// /_/src/Flecs.NET/Generated/Entity/Entity.ComponentCallbacks/T13.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Entity.cs
using System;

namespace Flecs.NET.Core;

public unsafe partial struct Entity
{
    /// <summary>
    ///     Read 13 components using the provided callback. <br/><br/>
    /// 
    ///     This operation accepts a callback with as arguments the components to
    ///     retrieve. The callback will only be invoked when the entity has all
    ///     the components. <br/><br/>
    ///     
    ///     This operation is faster than individually calling get for each component
    ///     as it only obtains entity metadata once.  <br/><br/>
    ///     
    ///     While the callback is invoked the table in which the components are
    ///     stored is locked, which prevents mutations that could cause invalidation
    ///     of the component references. Note that this is not an actual lock: 
    ///     invalid access causes a runtime panic and so it is still up to the 
    ///     application to ensure access is protected.  <br/><br/>
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam>
    /// <returns>True if the entity has the specified components.</returns>
    public bool Read<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Ecs.ReadRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        return Invoker.Read(World, Id, callback);
    }

    /// <summary>
    ///     Write 13 components using the provided callback. <br/><br/>
    /// 
    ///     This operation accepts a callback with as arguments the components to
    ///     retrieve. The callback will only be invoked when the entity has all
    ///     the components. <br/><br/>
    ///     
    ///     This operation is faster than individually calling get for each component
    ///     as it only obtains entity metadata once.  <br/><br/>
    ///     
    ///     While the callback is invoked the table in which the components are
    ///     stored is locked, which prevents mutations that could cause invalidation
    ///     of the component references. Note that this is not an actual lock: 
    ///     invalid access causes a runtime panic and so it is still up to the 
    ///     application to ensure access is protected.  <br/><br/>
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam>
    /// <returns>True if the entity has the specified components.</returns>
    public bool Write<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Ecs.WriteRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        return Invoker.Write(World, Id, callback);
    }

    /// <summary>
    ///     Ensures 13 components using the provided callback.<br/><br/>
    /// 
    ///     This operation accepts a callback with as arguments the components to
    ///     set. If the entity does not have all of the provided components, they
    ///     will be added. <br/><br/>
    ///
    ///     This operation is faster than individually calling ensure for each component
    ///     as it only obtains entity metadata once. When this operation is called
    ///     while deferred, its performance is equivalent to that of calling ensure
    ///     for each component separately. <br/><br/>
    ///
    ///     The operation will invoke modified for each component after the callback
    ///     has been invoked.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam> <typeparam name="T3">The T3 component type.</typeparam> <typeparam name="T4">The T4 component type.</typeparam> <typeparam name="T5">The T5 component type.</typeparam> <typeparam name="T6">The T6 component type.</typeparam> <typeparam name="T7">The T7 component type.</typeparam> <typeparam name="T8">The T8 component type.</typeparam> <typeparam name="T9">The T9 component type.</typeparam> <typeparam name="T10">The T10 component type.</typeparam> <typeparam name="T11">The T11 component type.</typeparam> <typeparam name="T12">The T12 component type.</typeparam>
    /// <returns>Reference to self.</returns>
    public ref Entity Insert<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Ecs.InsertRefCallback<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        Invoker.Insert(World, Id, callback);
        return ref this;
    }
}