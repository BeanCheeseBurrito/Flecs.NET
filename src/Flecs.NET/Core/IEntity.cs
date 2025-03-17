using System;
using System.Diagnostics.CodeAnalysis;

namespace Flecs.NET.Core;

/// <summary>
///     Interface for entity objects.
/// </summary>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public unsafe partial interface IEntity<TEntity> : IId
{
    /// <summary>
    ///     A reference to the entity.
    /// </summary>
    public ref Entity Entity { get; }

    /// <inheritdoc cref="Entity.IsValid()"/>
    public bool IsValid();

    /// <inheritdoc cref="Entity.IsAlive()"/>
    public bool IsAlive();

    /// <inheritdoc cref="Entity.Name()"/>
    public string Name();

    /// <inheritdoc cref="Entity.Symbol()"/>
    public string Symbol();

    /// <inheritdoc cref="Entity.Path(string, string)"/>
    public string Path(string sep = Ecs.DefaultSeparator, string initSep = Ecs.DefaultSeparator);

    /// <inheritdoc cref="Entity.PathFrom(ulong, string, string)"/>
    public string PathFrom(ulong parent, string sep = Ecs.DefaultSeparator, string initSep = Ecs.DefaultSeparator);

    /// <inheritdoc cref="Entity.PathFrom{TParent}(string, string)"/>
    public string PathFrom<TParent>(string sep = Ecs.DefaultSeparator, string initSep = Ecs.DefaultSeparator);

    /// <inheritdoc cref="Entity.Enabled()"/>
    public bool Enabled();

    /// <inheritdoc cref="Entity.Enabled(ulong)"/>
    public bool Enabled(ulong id);

    /// <inheritdoc cref="Entity.Enabled(ulong, ulong)"/>
    public bool Enabled(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Enabled{T}()"/>
    public bool Enabled<T>();

    /// <inheritdoc cref="Entity.Enabled{T}(T)"/>
    public bool Enabled<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Enabled{TFirst}(ulong)"/>
    public bool Enabled<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Enabled{TFirst, TSecond}()"/>
    public bool Enabled<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Enabled{TFirst, TSecond}(TSecond)"/>
    public bool Enabled<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Enabled{TFirst, TSecond}(TFirst)"/>
    public bool Enabled<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.EnabledSecond{TSecond}(ulong)"/>
    public bool EnabledSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Type()"/>
    public FlecsType Type();

    /// <inheritdoc cref="Entity.Table()"/>
    public Table Table();

    /// <inheritdoc cref="Entity.Range()"/>
    public Table Range();

    /// <inheritdoc cref="Entity.Each(Ecs.EachIdCallback)"/>
    public void Each(Ecs.EachIdCallback func);

    /// <inheritdoc cref="Entity.Each(ulong, ulong, Ecs.EachIdCallback)"/>
    public void Each(ulong first, ulong second, Ecs.EachIdCallback func);

    /// <inheritdoc cref="Entity.Each(ulong, Ecs.EachEntityCallback)"/>
    public void Each(ulong relation, Ecs.EachEntityCallback func);

    /// <inheritdoc cref="Entity.Each{TFirst}(Ecs.EachEntityCallback)"/>
    public void Each<TFirst>(Ecs.EachEntityCallback func);

    /// <inheritdoc cref="Entity.Each{TFirst}(TFirst, Ecs.EachEntityCallback)"/>
    public void Each<TFirst>(TFirst relation, Ecs.EachEntityCallback callback) where TFirst : Enum;

    /// <inheritdoc cref="Entity.Children(ulong, Ecs.EachEntityCallback)"/>
    public void Children(ulong relation, Ecs.EachEntityCallback callback);

    /// <inheritdoc cref="Entity.Children{TRel}(Ecs.EachEntityCallback)"/>
    public void Children<TRel>(Ecs.EachEntityCallback callback);

    /// <inheritdoc cref="Entity.Children{TFirst}(TFirst, Ecs.EachEntityCallback)"/>
    public void Children<TFirst>(TFirst relation, Ecs.EachEntityCallback callback) where TFirst : Enum;

    /// <inheritdoc cref="Entity.Children(Ecs.EachEntityCallback)"/>
    public void Children(Ecs.EachEntityCallback callback);

    /// <inheritdoc cref="Entity.GetPtr(ulong)"/>
    public void* GetPtr(ulong compId);

    /// <inheritdoc cref="Entity.GetPtr(ulong, ulong)"/>
    public void* GetPtr(ulong first, ulong second);

    /// <inheritdoc cref="Entity.GetPtr{T}()"/>
    public T* GetPtr<T>() where T : unmanaged;

    /// <inheritdoc cref="Entity.GetPtr{TFirst}(ulong)"/>
    public TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged;

    /// <inheritdoc cref="Entity.GetPtr{TFirst, TSecond}(TSecond)"/>
    public TFirst* GetPtr<TFirst, TSecond>(TSecond second) where TFirst : unmanaged where TSecond : Enum;

    /// <inheritdoc cref="Entity.GetPtr{TFirst, TSecond}(TFirst)"/>
    public TSecond* GetPtr<TFirst, TSecond>(TFirst first) where TFirst : Enum where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.GetFirstPtr{TFirst, TSecond}()"/>
    public TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged;

    /// <inheritdoc cref="Entity.GetSecondPtr{TFirst, TSecond}()"/>
    public TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.GetSecondPtr{TSecond}(ulong)"/>
    public TSecond* GetSecondPtr<TSecond>(ulong first) where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.Get{T}()"/>
    public ref readonly T Get<T>();

    /// <inheritdoc cref="Entity.Get{TFirst}(ulong)"/>
    public ref readonly TFirst Get<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Get{TFirst, TSecond}(TSecond)"/>
    public ref readonly TFirst Get<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Get{TFirst, TSecond}(TFirst)"/>
    public ref readonly TSecond Get<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.GetFirst{TFirst, TSecond}()"/>
    public ref readonly TFirst GetFirst<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.GetSecond{TFirst, TSecond}()"/>
    public ref readonly TSecond GetSecond<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.GetSecond{TSecond}(ulong)"/>
    public ref readonly TSecond GetSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.GetMutPtr(ulong)"/>
    public void* GetMutPtr(ulong id);

    /// <inheritdoc cref="Entity.GetMutPtr(ulong, ulong)"/>
    public void* GetMutPtr(ulong first, ulong second);

    /// <inheritdoc cref="Entity.GetMutPtr{T}()"/>
    public T* GetMutPtr<T>() where T : unmanaged;

    /// <inheritdoc cref="Entity.GetMutPtr{TFirst}(ulong)"/>
    public TFirst* GetMutPtr<TFirst>(ulong second) where TFirst : unmanaged;

    /// <inheritdoc cref="Entity.GetMutPtr{TFirst, TSecond}(TSecond)"/>
    public TFirst* GetMutPtr<TFirst, TSecond>(TSecond second) where TFirst : unmanaged where TSecond : Enum;

    /// <inheritdoc cref="Entity.GetMutPtr{TFirst, TSecond}(TFirst)"/>
    public TSecond* GetMutPtr<TFirst, TSecond>(TFirst first) where TFirst : Enum where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.GetMutFirstPtr{TFirst, TSecond}()"/>
    public TFirst* GetMutFirstPtr<TFirst, TSecond>() where TFirst : unmanaged;

    /// <inheritdoc cref="Entity.GetMutSecondPtr{TFirst, TSecond}()"/>
    public TSecond* GetMutSecondPtr<TFirst, TSecond>() where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.GetMutSecondPtr{TSecond}(ulong)"/>
    public TSecond* GetMutSecondPtr<TSecond>(ulong first) where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.GetMut{T}()"/>
    public ref T GetMut<T>();

    /// <inheritdoc cref="Entity.GetMut{TFirst}(ulong)"/>
    public ref TFirst GetMut<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.GetMut{TFirst, TSecond}(TSecond)"/>
    public ref TFirst GetMut<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.GetMut{TFirst, TSecond}(TFirst)"/>
    public ref TSecond GetMut<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.GetMutFirst{TFirst, TSecond}()"/>
    public ref TFirst GetMutFirst<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.GetMutSecond{TFirst, TSecond}()"/>
    public ref TSecond GetMutSecond<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.GetMutSecond{TSecond}(ulong)"/>
    public ref TSecond GetMutSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Target(ulong, int)"/>
    public Entity Target(ulong relation, int index = 0);

    /// <inheritdoc cref="Entity.Target{T}(int)"/>
    public Entity Target<T>(int index = 0);

    /// <inheritdoc cref="Entity.TargetFor(ulong, ulong)"/>
    public Entity TargetFor(ulong relation, ulong id);

    /// <inheritdoc cref="Entity.TargetFor{T}(ulong)"/>
    public Entity TargetFor<T>(ulong relation);

    /// <inheritdoc cref="Entity.TargetFor{TFirst, TSecond}(ulong)"/>
    public Entity TargetFor<TFirst, TSecond>(ulong relation);

    /// <inheritdoc cref="Entity.Depth(ulong)"/>
    public int Depth(ulong rel);

    /// <inheritdoc cref="Entity.Depth{T}()"/>
    public int Depth<T>();

    /// <inheritdoc cref="Entity.Depth{T}(T)"/>
    public int Depth<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Parent()"/>
    public Entity Parent();

    /// <inheritdoc cref="Entity.Lookup(string, bool)"/>
    public Entity Lookup(string path, bool recursive = false);

    /// <inheritdoc cref="Entity.TryLookup(string, out Core.Entity)"/>
    public bool TryLookup(string path, out Entity entity);

    /// <inheritdoc cref="Entity.TryLookup(string, bool, out Core.Entity)"/>
    public bool TryLookup(string path, bool recursive, out Entity entity);

    /// <inheritdoc cref="Entity.TryLookup(string, out ulong)"/>
    public bool TryLookup(string path, out ulong entity);

    /// <inheritdoc cref="Entity.TryLookup(string, bool, out ulong)"/>
    public bool TryLookup(string path, bool recursive, out ulong entity);

    /// <inheritdoc cref="Entity.Has(ulong)"/>
    public bool Has(ulong id);

    /// <inheritdoc cref="Entity.Has(ulong, ulong)"/>
    public bool Has(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Has{T}()"/>
    public bool Has<T>();

    /// <inheritdoc cref="Entity.Has{T}(T)"/>
    public bool Has<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Has{TFirst}(ulong)"/>
    public bool Has<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Has{TFirst, TSecond}()"/>
    public bool Has<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Has{TFirst, TSecond}(TSecond)"/>
    public bool Has<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Has{TFirst, TSecond}(TFirst)"/>
    public bool Has<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.HasSecond{TSecond}(ulong)"/>
    public bool HasSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Owns(ulong)"/>
    public bool Owns(ulong id);

    /// <inheritdoc cref="Entity.Owns(ulong, ulong)"/>
    public bool Owns(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Owns{T}()"/>
    public bool Owns<T>();

    /// <inheritdoc cref="Entity.Owns{T}(T)"/>
    public bool Owns<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Owns{TFirst}(ulong)"/>
    public bool Owns<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Owns{TFirst, TSecond}()"/>
    public bool Owns<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Owns{TFirst, TSecond}(TSecond)"/>
    public bool Owns<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Owns{TFirst, TSecond}(TFirst)"/>
    public bool Owns<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.OwnsSecond{TSecond}(ulong)"/>
    public bool OwnsSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Clone(bool, ulong)"/>
    public Entity Clone(bool cloneValue = true, ulong dstId = 0);

    /// <inheritdoc cref="Entity.Mut(ref Core.World)"/>
    public Entity Mut(ref World stage);

    /// <inheritdoc cref="Entity.Mut(ref Iter)"/>
    public Entity Mut(ref Iter it);

    /// <inheritdoc cref="Entity.Mut(ref Core.Entity)"/>
    public Entity Mut(ref Entity entity);

    /// <inheritdoc cref="Entity.Mut(Core.World)"/>
    public Entity Mut(World stage);

    /// <inheritdoc cref="Entity.Mut(Iter)"/>
    public Entity Mut(Iter it);

    /// <inheritdoc cref="Entity.Mut(Core.Entity)"/>
    public Entity Mut(Entity entity);

    /// <inheritdoc cref="Entity.ToJson(in EntityToJsonDesc)"/>
    public string ToJson(in EntityToJsonDesc desc);

    /// <inheritdoc cref="Entity.ToJson()"/>
    public string ToJson();

    /// <inheritdoc cref="Entity.DocName()"/>
    public string DocName();

    /// <inheritdoc cref="Entity.DocBrief()"/>
    public string DocBrief();

    /// <inheritdoc cref="Entity.DocDetail()"/>
    public string DocDetail();

    /// <inheritdoc cref="Entity.DocLink()"/>
    public string DocLink();

    /// <inheritdoc cref="Entity.DocColor()"/>
    public string DocColor();

    /// <inheritdoc cref="Entity.DocUuid()"/>
    public string DocUuid();

    /// <inheritdoc cref="Entity.AlertCount(ulong)"/>
    public int AlertCount(ulong alert = 0);

    /// <inheritdoc cref="Entity.ToConstant{T}()"/>
    public T ToConstant<T>() where T : unmanaged, Enum;

    /// <inheritdoc cref="Entity.Emit(ulong)"/>
    public void Emit(ulong eventId);

    /// <inheritdoc cref="Entity.Emit(Core.Entity)"/>
    public void Emit(Entity eventId);

    /// <inheritdoc cref="Entity.Emit{T}()"/>
    public void Emit<T>();

    /// <inheritdoc cref="Entity.Emit{T}(T)"/>
    public void Emit<T>(T payload) where T : unmanaged;

    /// <inheritdoc cref="Entity.Emit{T}(ref T)"/>
    public void Emit<T>(ref T payload);

    /// <inheritdoc cref="Entity.Enqueue(ulong)"/>
    public void Enqueue(ulong eventId);

    /// <inheritdoc cref="Entity.Enqueue(Core.Entity)"/>
    public void Enqueue(Entity eventId);

    /// <inheritdoc cref="Entity.Enqueue{T}()"/>
    public void Enqueue<T>();

    /// <inheritdoc cref="Entity.Enqueue{T}(T)"/>
    public void Enqueue<T>(T payload) where T : unmanaged;

    /// <inheritdoc cref="Entity.Enqueue{T}(ref T)"/>
    public void Enqueue<T>(ref T payload);

    /// <inheritdoc cref="Entity.IsChildOf(ulong)"/>
    public bool IsChildOf(ulong entity);

    /// <inheritdoc cref="Entity.IsChildOf{T}()"/>
    public bool IsChildOf<T>();

    /// <inheritdoc cref="Entity.IsChildOf{T}(T)"/>
    public bool IsChildOf<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Add(ulong)"/>
    public ref TEntity Add(ulong id);

    /// <inheritdoc cref="Entity.Add(ulong, ulong)"/>
    public ref TEntity Add(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Add{T}()"/>
    public ref TEntity Add<T>();

    /// <inheritdoc cref="Entity.Add{TFirst}(ulong)"/>
    public ref TEntity Add<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Add{T}(T)"/>
    public ref TEntity Add<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Add{TFirst, TSecond}()"/>
    public ref TEntity Add<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Add{TFirst, TSecond}(TSecond)"/>
    public ref TEntity Add<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Add{TFirst, TSecond}(TFirst)"/>
    public ref TEntity Add<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.AddSecond{TSecond}(ulong)"/>
    public ref TEntity AddSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.AddIf(bool, ulong)"/>
    public ref TEntity AddIf(bool cond, ulong id);

    /// <inheritdoc cref="Entity.AddIf(bool, ulong, ulong)"/>
    public ref TEntity AddIf(bool cond, ulong first, ulong second);

    /// <inheritdoc cref="Entity.AddIf{T}(bool)"/>
    public ref TEntity AddIf<T>(bool cond);

    /// <inheritdoc cref="Entity.AddIf{T}(bool, T)"/>
    public ref TEntity AddIf<T>(bool cond, T value) where T : Enum;

    /// <inheritdoc cref="Entity.AddIf{TFirst}(bool, ulong)"/>
    public ref TEntity AddIf<TFirst>(bool cond, ulong second);

    /// <inheritdoc cref="Entity.AddIf{TFirst, TSecond}(bool)"/>
    public ref TEntity AddIf<TFirst, TSecond>(bool cond);

    /// <inheritdoc cref="Entity.AddIf{TFirst, TSecond}(bool, TSecond)"/>
    public ref TEntity AddIf<TFirst, TSecond>(bool cond, TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.AddIf{TFirst, TSecond}(bool, TFirst)"/>
    public ref TEntity AddIf<TFirst, TSecond>(bool cond, TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.AddIfSecond{TSecond}(bool, ulong)"/>
    public ref TEntity AddIfSecond<TSecond>(bool cond, ulong first);

    /// <inheritdoc cref="Entity.IsA(ulong)"/>
    public ref TEntity IsA(ulong id);

    /// <inheritdoc cref="Entity.IsA{T}()"/>
    public ref TEntity IsA<T>();

    /// <inheritdoc cref="Entity.IsA{T}(T)"/>
    public ref TEntity IsA<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.ChildOf(ulong)"/>
    public ref TEntity ChildOf(ulong second);

    /// <inheritdoc cref="Entity.ChildOf{T}()"/>
    public ref TEntity ChildOf<T>();

    /// <inheritdoc cref="Entity.ChildOf{T}(T)"/>
    public ref TEntity ChildOf<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.DependsOn(ulong)"/>
    public ref TEntity DependsOn(ulong second);

    /// <inheritdoc cref="Entity.DependsOn{T}()"/>
    public ref TEntity DependsOn<T>();

    /// <inheritdoc cref="Entity.DependsOn{T}(T)"/>
    public ref TEntity DependsOn<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.SlotOf(ulong)"/>
    public ref TEntity SlotOf(ulong id);

    /// <inheritdoc cref="Entity.SlotOf{T}()"/>
    public ref TEntity SlotOf<T>();

    /// <inheritdoc cref="Entity.SlotOf{T}(T)"/>
    public ref TEntity SlotOf<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Slot()"/>
    public ref TEntity Slot();

    /// <inheritdoc cref="Entity.Remove(ulong)"/>
    public ref TEntity Remove(ulong id);

    /// <inheritdoc cref="Entity.Remove(ulong, ulong)"/>
    public ref TEntity Remove(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Remove{T}()"/>
    public ref TEntity Remove<T>();

    /// <inheritdoc cref="Entity.Remove{T}(T)"/>
    public ref TEntity Remove<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Remove{TFirst}(ulong)"/>
    public ref TEntity Remove<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Remove{TFirst, TSecond}()"/>
    public ref TEntity Remove<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Remove{TFirst, TSecond}(TSecond)"/>
    public ref TEntity Remove<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Remove{TFirst, TSecond}(TFirst)"/>
    public ref TEntity Remove<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.RemoveSecond{TSecond}(ulong)"/>
    public ref TEntity RemoveSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.AutoOverride(ulong)"/>
    public ref TEntity AutoOverride(ulong id);

    /// <inheritdoc cref="Entity.AutoOverride(ulong, ulong)"/>
    public ref TEntity AutoOverride(ulong first, ulong second);

    /// <inheritdoc cref="Entity.AutoOverride{T}()"/>
    public ref TEntity AutoOverride<T>();

    /// <inheritdoc cref="Entity.AutoOverride{T}(T)"/>
    public ref TEntity AutoOverride<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.AutoOverride{TFirst}(ulong)"/>
    public ref TEntity AutoOverride<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.AutoOverride{TFirst, TSecond}()"/>
    public ref TEntity AutoOverride<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.AutoOverride{TFirst, TSecond}(TSecond)"/>
    public ref TEntity AutoOverride<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.AutoOverride{TFirst, TSecond}(TFirst)"/>
    public ref TEntity AutoOverride<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.AutoOverrideSecond{TSecond}(ulong)"/>
    public ref TEntity AutoOverrideSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.SetAutoOverride{T}(in T)"/>
    public ref TEntity SetAutoOverride<T>(in T component);

    /// <inheritdoc cref="Entity.SetAutoOverride{TFirst}(ulong, in TFirst)"/>
    public ref TEntity SetAutoOverride<TFirst>(ulong second, in TFirst component);

    /// <inheritdoc cref="Entity.SetAutoOverride{TFirst, TSecond}(in TFirst)"/>
    public ref TEntity SetAutoOverride<TFirst, TSecond>(in TFirst component);

    /// <inheritdoc cref="Entity.SetAutoOverride{TFirst, TSecond}(in TSecond)"/>
    public ref TEntity SetAutoOverride<TFirst, TSecond>(in TSecond component);

    /// <inheritdoc cref="Entity.SetAutoOverride{TFirst, TSecond}(TSecond, in TFirst)"/>
    public ref TEntity SetAutoOverride<TFirst, TSecond>(TSecond second, in TFirst component) where TSecond : Enum;

    /// <inheritdoc cref="Entity.SetAutoOverride{TFirst, TSecond}(TFirst, in TSecond)"/>
    public ref TEntity SetAutoOverride<TFirst, TSecond>(TFirst first, in TSecond component) where TFirst : Enum;

    /// <inheritdoc cref="Entity.SetAutoOverrideSecond{TSecond}(ulong, in TSecond)"/>
    public ref TEntity SetAutoOverrideSecond<TSecond>(ulong first, in TSecond component);

    /// <inheritdoc cref="Entity.Enable()"/>
    public ref TEntity Enable();

    /// <inheritdoc cref="Entity.Enable(ulong)"/>
    public ref TEntity Enable(ulong id);

    /// <inheritdoc cref="Entity.Enable(ulong, ulong)"/>
    public ref TEntity Enable(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Enable{T}()"/>
    public ref TEntity Enable<T>();

    /// <inheritdoc cref="Entity.Enable{T}(T)"/>
    public ref TEntity Enable<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Enable{TFirst}(ulong)"/>
    public ref TEntity Enable<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Enable{TFirst, TSecond}()"/>
    public ref TEntity Enable<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Enable{TFirst, TSecond}(TSecond)"/>
    public ref TEntity Enable<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Enable{TFirst, TSecond}(TFirst)"/>
    public ref TEntity Enable<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.EnableSecond{TSecond}(ulong)"/>
    public ref TEntity EnableSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Disable()"/>
    public ref TEntity Disable();

    /// <inheritdoc cref="Entity.Disable(ulong)"/>
    public ref TEntity Disable(ulong id);

    /// <inheritdoc cref="Entity.Disable(ulong, ulong)"/>
    public ref TEntity Disable(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Disable{T}()"/>
    public ref TEntity Disable<T>();

    /// <inheritdoc cref="Entity.Disable{T}(T)"/>
    public ref TEntity Disable<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Disable{TFirst}(ulong)"/>
    public ref TEntity Disable<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Disable{TFirst, TSecond}()"/>
    public ref TEntity Disable<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Disable{TFirst, TSecond}(TSecond)"/>
    public ref TEntity Disable<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Disable{TFirst, TSecond}(TFirst)"/>
    public ref TEntity Disable<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.DisableSecond{TSecond}(ulong)"/>
    public ref TEntity DisableSecond<TSecond>(ulong first);

    ///
    public ref TEntity SetUntyped(ulong id, int size, void* data);

    ///
    public ref TEntity SetUntyped(ulong id, void* data);

    ///
    public ref TEntity SetUntyped(ulong first, ulong second, int size, void* data);

    /// <inheritdoc cref="Entity.SetPtr{T}(T*)"/>
    public ref TEntity SetPtr<T>(T* data);

    /// <inheritdoc cref="Entity.SetPtr{TFirst}(ulong, TFirst*)"/>
    public ref TEntity SetPtr<TFirst>(ulong second, TFirst* data);

    /// <inheritdoc cref="Entity.SetPtr{TFirst, TSecond}(TSecond*)"/>
    public ref TEntity SetPtr<TFirst, TSecond>(TSecond* data);

    /// <inheritdoc cref="Entity.SetPtr{TFirst, TSecond}(TFirst*)"/>
    public ref TEntity SetPtr<TFirst, TSecond>(TFirst* data);

    /// <inheritdoc cref="Entity.SetPtr{TFirst, TSecond}(TSecond, TFirst*)"/>
    public ref TEntity SetPtr<TFirst, TSecond>(TSecond second, TFirst* data) where TSecond : Enum;

    /// <inheritdoc cref="Entity.SetPtr{TFirst, TSecond}(TFirst, TSecond*)"/>
    public ref TEntity SetPtr<TFirst, TSecond>(TFirst first, TSecond* data) where TFirst : Enum;

    /// <inheritdoc cref="Entity.SetPtrSecond{TSecond}(ulong, TSecond*)"/>
    public ref TEntity SetPtrSecond<TSecond>(ulong first, TSecond* data);

    /// <inheritdoc cref="Entity.Set{T}(in T)"/>
    public ref TEntity Set<T>(in T data);

    /// <inheritdoc cref="Entity.Set{TFirst}(ulong, in TFirst)"/>
    public ref TEntity Set<TFirst>(ulong second, in TFirst data);

    /// <inheritdoc cref="Entity.Set{TFirst, TSecond}(in TSecond)"/>
    public ref TEntity Set<TFirst, TSecond>(in TSecond data);

    /// <inheritdoc cref="Entity.Set{TFirst, TSecond}(in TFirst)"/>
    public ref TEntity Set<TFirst, TSecond>(in TFirst data);

    /// <inheritdoc cref="Entity.Set{TFirst, TSecond}(TSecond, in TFirst)"/>
    public ref TEntity Set<TFirst, TSecond>(TSecond second, in TFirst data) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Set{TFirst, TSecond}(TFirst, in TSecond)"/>
    public ref TEntity Set<TFirst, TSecond>(TFirst first, in TSecond data) where TFirst : Enum;

    /// <inheritdoc cref="Entity.SetSecond{TSecond}(ulong, in TSecond)"/>
    public ref TEntity SetSecond<TSecond>(ulong first, in TSecond data);

    /// <inheritdoc cref="Entity.With(Action)"/>
    public ref TEntity With(Action callback);

    /// <inheritdoc cref="Entity.With(ulong, Action)"/>
    public ref TEntity With(ulong first, Action callback);

    /// <inheritdoc cref="Entity.With{TFirst}(Action)"/>
    public ref TEntity With<TFirst>(Action callback);

    /// <inheritdoc cref="Entity.With(Ecs.WorldCallback)"/>
    public ref TEntity With(Ecs.WorldCallback callback);

    /// <inheritdoc cref="Entity.With(ulong, Ecs.WorldCallback)"/>
    public ref TEntity With(ulong first, Ecs.WorldCallback callback);

    /// <inheritdoc cref="Entity.With{TFirst}(Ecs.WorldCallback)"/>
    public ref TEntity With<TFirst>(Ecs.WorldCallback callback);

    /// <inheritdoc cref="Entity.Scope(Action)"/>
    public ref TEntity Scope(Action callback);

    /// <inheritdoc cref="Entity.Scope(Ecs.WorldCallback)"/>
    public ref TEntity Scope(Ecs.WorldCallback callback);

    /// <inheritdoc cref="Entity.SetName(string)"/>
    public ref TEntity SetName(string name);

    /// <inheritdoc cref="Entity.SetAlias(string)"/>
    public ref TEntity SetAlias(string alias);

    /// <inheritdoc cref="Entity.SetDocName(string)"/>
    public ref TEntity SetDocName(string name);

    /// <inheritdoc cref="Entity.SetDocBrief(string)"/>
    public ref TEntity SetDocBrief(string brief);

    /// <inheritdoc cref="Entity.SetDocDetail(string)"/>
    public ref TEntity SetDocDetail(string detail);

    /// <inheritdoc cref="Entity.SetDocLink(string)"/>
    public ref TEntity SetDocLink(string link);

    /// <inheritdoc cref="Entity.SetDocColor(string)"/>
    public ref TEntity SetDocColor(string color);

    /// <inheritdoc cref="Entity.SetDocUuid(string)"/>
    public ref TEntity SetDocUuid(string uuid);

    /// <inheritdoc cref="Entity.Unit(string, ulong, ulong, ulong, int, int)"/>
    public ref TEntity Unit(
        string symbol,
        ulong prefix = 0,
        ulong @base = 0,
        ulong over = 0,
        int factor = 0,
        int power = 0);

    /// <inheritdoc cref="Entity.Unit(ulong, ulong, ulong, int, int)"/>
    public ref TEntity Unit(
        ulong prefix = 0,
        ulong @base = 0,
        ulong over = 0,
        int factor = 0,
        int power = 0);

    /// <inheritdoc cref="Entity.UnitPrefix(string, int, int)"/>
    public ref TEntity UnitPrefix(string symbol, int factor = 0, int power = 0);

    /// <inheritdoc cref="Entity.Quantity(ulong)"/>
    public ref TEntity Quantity(ulong quantity);

    /// <inheritdoc cref="Entity.Quantity{T}()"/>
    public ref TEntity Quantity<T>();

    /// <inheritdoc cref="Entity.Quantity()"/>
    public ref TEntity Quantity();

    /// <inheritdoc cref="Entity.SetJson(ulong, string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson(ulong e, string json, ecs_from_json_desc_t* desc = null);

    /// <inheritdoc cref="Entity.SetJson(ulong, ulong, string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson(ulong first, ulong second, string json, ecs_from_json_desc_t* desc = null);

    /// <inheritdoc cref="Entity.SetJson{T}(string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson<T>(string json, ecs_from_json_desc_t* desc = null);

    /// <inheritdoc cref="Entity.SetJson{TFirst}(ulong, string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson<TFirst>(ulong second, string json, ecs_from_json_desc_t* desc = null);

    /// <inheritdoc cref="Entity.SetJson{TFirst, TSecond}(string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson<TFirst, TSecond>(string json, ecs_from_json_desc_t* desc = null);

    /// <inheritdoc cref="Entity.SetJson{TFirst, TSecond}(TSecond, string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson<TFirst, TSecond>(TSecond second, string json, ecs_from_json_desc_t* desc = null) where TSecond : Enum;

    /// <inheritdoc cref="Entity.SetJson{TFirst, TSecond}(TFirst, string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJson<TFirst, TSecond>(TFirst first, string json, ecs_from_json_desc_t* desc = null) where TFirst : Enum;

    /// <inheritdoc cref="Entity.SetJsonSecond{TSecond}(ulong, string, ecs_from_json_desc_t*)"/>
    public ref TEntity SetJsonSecond<TSecond>(ulong first, string json, ecs_from_json_desc_t* desc = null);

    /// <inheritdoc cref="Entity.EnsurePtr(ulong)"/>
    public void* EnsurePtr(ulong id);

    /// <inheritdoc cref="Entity.EnsurePtr(ulong, ulong)"/>
    public void* EnsurePtr(ulong first, ulong second);

    /// <inheritdoc cref="Entity.EnsurePtr{T}()"/>
    public T* EnsurePtr<T>() where T : unmanaged;

    /// <inheritdoc cref="Entity.EnsurePtr{TFirst}(ulong)"/>
    public TFirst* EnsurePtr<TFirst>(ulong second) where TFirst : unmanaged;

    /// <inheritdoc cref="Entity.EnsurePtr{TFirst, TSecond}(TSecond)"/>
    public TFirst* EnsurePtr<TFirst, TSecond>(TSecond second) where TFirst : unmanaged where TSecond : Enum;

    /// <inheritdoc cref="Entity.EnsurePtr{TFirst, TSecond}(TFirst)"/>
    public TSecond* EnsurePtr<TFirst, TSecond>(TFirst first) where TFirst : Enum where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.EnsureFirstPtr{TFirst, TSecond}()"/>
    public TFirst* EnsureFirstPtr<TFirst, TSecond>() where TFirst : unmanaged;

    /// <inheritdoc cref="Entity.EnsureSecondPtr{TFirst, TSecond}()"/>
    public TSecond* EnsureSecondPtr<TFirst, TSecond>() where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.EnsureSecondPtr{TSecond}(ulong)"/>
    public TSecond* EnsureSecondPtr<TSecond>(ulong first) where TSecond : unmanaged;

    /// <inheritdoc cref="Entity.Ensure{T}()"/>
    public ref T Ensure<T>();

    /// <inheritdoc cref="Entity.Ensure{TFirst}(ulong)"/>
    public ref TFirst Ensure<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Ensure{TFirst, TSecond}(TSecond)"/>
    public ref TFirst Ensure<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Ensure{TFirst, TSecond}(TFirst)"/>
    public ref TSecond Ensure<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.EnsureFirst{TFirst, TSecond}()"/>
    public ref TFirst EnsureFirst<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.EnsureSecond{TFirst, TSecond}()"/>
    public ref TSecond EnsureSecond<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.EnsureSecond{TSecond}(ulong)"/>
    public ref TSecond EnsureSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Modified(ulong)"/>
    public void Modified(ulong id);

    /// <inheritdoc cref="Entity.Modified(ulong, ulong)"/>
    public void Modified(ulong first, ulong second);

    /// <inheritdoc cref="Entity.Modified{T}()"/>
    public void Modified<T>();

    /// <inheritdoc cref="Entity.Modified{T}(T)"/>
    public void Modified<T>(T value) where T : Enum;

    /// <inheritdoc cref="Entity.Modified{TFirst}(ulong)"/>
    public void Modified<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.Modified{TFirst, TSecond}()"/>
    public void Modified<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.Modified{TFirst, TSecond}(TSecond)"/>
    public void Modified<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.Modified{TFirst, TSecond}(TFirst)"/>
    public void Modified<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.ModifiedSecond{TSecond}(ulong)"/>
    public void ModifiedSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.GetRef{T}()"/>
    public Ref<T> GetRef<T>();

    /// <inheritdoc cref="Entity.GetRef{TFirst}(ulong)"/>
    public Ref<TFirst> GetRef<TFirst>(ulong second);

    /// <inheritdoc cref="Entity.GetRef{TFirst, TSecond}(TSecond)"/>
    public Ref<TFirst> GetRef<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="Entity.GetRef{TFirst, TSecond}(TFirst)"/>
    public Ref<TSecond> GetRef<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="Entity.GetRefFirst{TFirst, TSecond}()"/>
    public Ref<TFirst> GetRefFirst<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.GetRefSecond{TFirst, TSecond}()"/>
    public Ref<TSecond> GetRefSecond<TFirst, TSecond>();

    /// <inheritdoc cref="Entity.GetRefSecond{TSecond}(ulong)"/>
    public Ref<TSecond> GetRefSecond<TSecond>(ulong first);

    /// <inheritdoc cref="Entity.Clear()"/>
    public void Clear();

    /// <inheritdoc cref="Entity.Destruct()"/>
    public void Destruct();

    /// <inheritdoc cref="Entity.FromJson(string)"/>
    public string FromJson(string json);
}
