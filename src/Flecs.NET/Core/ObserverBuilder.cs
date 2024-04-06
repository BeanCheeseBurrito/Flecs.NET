using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_observer_desc_t.
    /// </summary>
    public unsafe partial struct ObserverBuilder : IDisposable, IEquatable<ObserverBuilder>
    {
        private ecs_world_t* _world;

        internal ecs_observer_desc_t ObserverDesc;
        internal QueryBuilder QueryBuilder;
        internal BindingContext.ObserverContext ObserverContext;
        internal int EventCount;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the observer description.
        /// </summary>
        public ref ecs_observer_desc_t Desc => ref ObserverDesc;

        /// <summary>
        ///     Creates an observer builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public ObserverBuilder(ecs_world_t* world, string? name = null)
        {
            ObserverDesc = default;
            ObserverContext = default;
            EventCount = default;
            QueryBuilder = new QueryBuilder(world);
            _world = world;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;

            ObserverDesc.entity = ecs_entity_init(world, &entityDesc);
        }

        /// <summary>
        ///     Disposes the observer builder. This should be called if the observer builder
        ///     will be discarded and .Iter()/Each() isn't called.
        /// </summary>
        public void Dispose()
        {
            ObserverContext.Dispose();
            QueryBuilder.Dispose();
        }

        /// <summary>
        ///     Specify the event(s) for when the observer should run.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public ref ObserverBuilder Event(ulong @event)
        {
            if (EventCount >= 8)
                Ecs.Error("Can't create an observer with more than 8 events.");

            ObserverDesc.events[EventCount++] = @event;
            return ref this;
        }

        /// <summary>
        ///     Specify the event(s) for when the observer should run.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref ObserverBuilder Event<T>()
        {
            return ref Event(Type<T>.Id(World));
        }

        /// <summary>
        ///     Invoke observer for anything that matches its filter on creation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref ObserverBuilder YieldExisting(bool value = true)
        {
            ObserverDesc.yield_existing = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Set observer context.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ref ObserverBuilder Ctx(void* data)
        {
            ObserverDesc.ctx = data;
            return ref this;
        }

        /// <summary>
        ///     Set observer run callback.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ref ObserverBuilder Run(Ecs.IterAction action)
        {
            BindingContext.SetCallback(ref ObserverContext.Run, action);
            ObserverDesc.run = ObserverContext.Run.Function;
            return ref this;
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Observer Iter(Action callback)
        {
            return Build(ref callback, BindingContext.RoutineActionPointer, false);
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Observer Iter(Ecs.IterCallback callback)
        {
            return Build(ref callback, BindingContext.RoutineIterPointer, false);
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Observer Each(Ecs.EachEntityCallback callback)
        {
            return Build(ref callback, BindingContext.RoutineEachEntityPointer, false);
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Observer Each(Ecs.EachIndexCallback callback)
        {
            return Build(ref callback, BindingContext.RoutineEachIndexPointer, false);
        }

        private Observer Build<T>(ref T userCallback, IntPtr internalCallback, bool storeFunctionPointer)
            where T : Delegate
        {
            BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
            queryContext[0] = QueryBuilder.Context;

            BindingContext.ObserverContext* observerContext = Memory.Alloc<BindingContext.ObserverContext>(1);
            observerContext[0] = ObserverContext;
            BindingContext.SetCallback(ref observerContext->Iterator, userCallback, storeFunctionPointer);

            ObserverDesc.query = QueryBuilder.Desc;
            ObserverDesc.query.binding_ctx = queryContext;
            ObserverDesc.query.binding_ctx_free = BindingContext.QueryContextFreePointer;
            ObserverDesc.binding_ctx = observerContext;
            ObserverDesc.binding_ctx_free = BindingContext.ObserverContextFreePointer;
            ObserverDesc.callback = internalCallback;

            fixed (ecs_observer_desc_t* ptr = &ObserverDesc)
            {
                Ecs.Assert(EventCount != 0, "Observer cannot have zero events. Use ObserverBuilder.Event() to add events.");
                Ecs.Assert(ptr->query.terms[0] != default || ptr->query.expr != null, "Observers require at least 1 term.");

                Entity entity = new Entity(World, ecs_observer_init(World, ptr));
                return new Observer(entity);
            }
        }

        /// <summary>
        ///     Checks if two <see cref="ObserverBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ObserverBuilder other)
        {
            return Desc == other.Desc && QueryBuilder == other.QueryBuilder;
        }

        /// <summary>
        ///     Checks if two <see cref="ObserverBuilder"/> instance are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is ObserverBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="ObserverBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="ObserverBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ObserverBuilder left, ObserverBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="ObserverBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ObserverBuilder left, ObserverBuilder right)
        {
            return !(left == right);
        }
    }

    // QueryBuilder Extensions
    public unsafe partial struct ObserverBuilder
    {
        /// <inheritdoc cref="QueryBuilder.Self()"/>
        public ref ObserverBuilder Self()
        {
            QueryBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
        public ref ObserverBuilder Id(ulong id)
        {
            QueryBuilder.Id(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
        public ref ObserverBuilder Entity(ulong entity)
        {
            QueryBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Name(string)"/>
        public ref ObserverBuilder Name(string name)
        {
            QueryBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Var(string)"/>
        public ref ObserverBuilder Var(string name)
        {
            QueryBuilder.Var(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Flags(uint)"/>
        public ref ObserverBuilder Flags(uint flags)
        {
            QueryBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
        public ref ObserverBuilder Term(ulong id)
        {
            QueryBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src()"/>
        public ref ObserverBuilder Src()
        {
            QueryBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First()"/>
        public ref ObserverBuilder First()
        {
            QueryBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second()"/>
        public ref ObserverBuilder Second()
        {
            QueryBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
        public ref ObserverBuilder Src(ulong srcId)
        {
            QueryBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
        public ref ObserverBuilder Src<T>()
        {
            QueryBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
        public ref ObserverBuilder Src<T>(T value) where T : Enum
        {
            QueryBuilder.Src(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(string)"/>
        public ref ObserverBuilder Src(string name)
        {
            QueryBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
        public ref ObserverBuilder First(ulong firstId)
        {
            QueryBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}()"/>
        public ref ObserverBuilder First<T>()
        {
            QueryBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
        public ref ObserverBuilder First<T>(T value) where T : Enum
        {
            QueryBuilder.First(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(string)"/>
        public ref ObserverBuilder First(string name)
        {
            QueryBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
        public ref ObserverBuilder Second(ulong secondId)
        {
            QueryBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
        public ref ObserverBuilder Second<T>()
        {
            QueryBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
        public ref ObserverBuilder Second<T>(T value) where T : Enum
        {
            QueryBuilder.Second(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(string)"/>
        public ref ObserverBuilder Second(string secondName)
        {
            QueryBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
        public ref ObserverBuilder Up(ulong traverse = 0)
        {
            QueryBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
        public ref ObserverBuilder Up<T>()
        {
            QueryBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
        public ref ObserverBuilder Up<T>(T value) where T : Enum
        {
            QueryBuilder.Up(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
        public ref ObserverBuilder Cascade(ulong traverse = 0)
        {
            QueryBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
        public ref ObserverBuilder Cascade<T>()
        {
            QueryBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
        public ref ObserverBuilder Cascade<T>(T value) where T : Enum
        {
            QueryBuilder.Cascade(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Descend()"/>
        public ref ObserverBuilder Descend()
        {
            QueryBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Parent()"/>
        public ref ObserverBuilder Parent()
        {
            QueryBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
        public ref ObserverBuilder Trav(ulong traverse, uint flags = 0)
        {
            QueryBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
        public ref ObserverBuilder Trav<T>(uint flags = 0)
        {
            QueryBuilder.Trav<T>(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
        public ref ObserverBuilder Trav<T>(T value, uint flags = 0) where T : Enum
        {
            QueryBuilder.Trav(value, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.IdFlags(ulong)"/>
        public ref ObserverBuilder IdFlags(ulong flags)
        {
            QueryBuilder.IdFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
        public ref ObserverBuilder InOut(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
        public ref ObserverBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write()"/>
        public ref ObserverBuilder Write()
        {
            QueryBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read()"/>
        public ref ObserverBuilder Read()
        {
            QueryBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
        public ref ObserverBuilder ReadWrite()
        {
            QueryBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.In()"/>
        public ref ObserverBuilder In()
        {
            QueryBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Out()"/>
        public ref ObserverBuilder Out()
        {
            QueryBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut()"/>
        public ref ObserverBuilder InOut()
        {
            QueryBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
        public ref ObserverBuilder InOutNone()
        {
            QueryBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
        public ref ObserverBuilder Oper(ecs_oper_kind_t oper)
        {
            QueryBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.And()"/>
        public ref ObserverBuilder And()
        {
            QueryBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Or()"/>
        public ref ObserverBuilder Or()
        {
            QueryBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Not()"/>
        public ref ObserverBuilder Not()
        {
            QueryBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Optional()"/>
        public ref ObserverBuilder Optional()
        {
            QueryBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
        public ref ObserverBuilder AndFrom()
        {
            QueryBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
        public ref ObserverBuilder OrFrom()
        {
            QueryBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
        public ref ObserverBuilder NotFrom()
        {
            QueryBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Singleton()"/>
        public ref ObserverBuilder Singleton()
        {
            QueryBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Filter()"/>
        public ref ObserverBuilder Filter()
        {
            QueryBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Instanced()"/>
        public ref ObserverBuilder Instanced()
        {
            QueryBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.QueryFlags(uint)"/>
        public ref ObserverBuilder QueryFlags(uint flags)
        {
            QueryBuilder.QueryFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
        public ref ObserverBuilder CacheKind(ecs_query_cache_kind_t kind)
        {
            QueryBuilder.CacheKind(kind);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cached()"/>
        public ref ObserverBuilder Cached()
        {
            QueryBuilder.Cached();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
        public ref ObserverBuilder Expr(string expr)
        {
            QueryBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
        public ref ObserverBuilder With(Term term)
        {
            QueryBuilder.With(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
        public ref ObserverBuilder With(ulong id)
        {
            QueryBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string)"/>
        public ref ObserverBuilder With(string name)
        {
            QueryBuilder.With(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
        public ref ObserverBuilder With(ulong first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
        public ref ObserverBuilder With(ulong first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
        public ref ObserverBuilder With(string first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
        public ref ObserverBuilder With(string first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}()"/>
        public ref ObserverBuilder With<T>()
        {
            QueryBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
        public ref ObserverBuilder With<T>(T value) where T : Enum
        {
            QueryBuilder.With(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
        public ref ObserverBuilder With<TFirst>(ulong second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
        public ref ObserverBuilder With<TFirst>(string second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
        public ref ObserverBuilder With<TFirst, TSecond>()
        {
            QueryBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
        public ref ObserverBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
        public ref ObserverBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
        public ref ObserverBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
        public ref ObserverBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
        public ref ObserverBuilder WithSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
        public ref ObserverBuilder WithSecond<TSecond>(string first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
        public ref ObserverBuilder Without(Term term)
        {
            QueryBuilder.Without(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
        public ref ObserverBuilder Without(ulong id)
        {
            QueryBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string)"/>
        public ref ObserverBuilder Without(string name)
        {
            QueryBuilder.Without(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
        public ref ObserverBuilder Without(ulong first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
        public ref ObserverBuilder Without(ulong first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
        public ref ObserverBuilder Without(string first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
        public ref ObserverBuilder Without(string first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
        public ref ObserverBuilder Without<T>()
        {
            QueryBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
        public ref ObserverBuilder Without<T>(T value) where T : Enum
        {
            QueryBuilder.Without(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
        public ref ObserverBuilder Without<TFirst>(ulong second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
        public ref ObserverBuilder Without<TFirst>(string second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
        public ref ObserverBuilder Without<TFirst, TSecond>()
        {
            QueryBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
        public ref ObserverBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
        public ref ObserverBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
        public ref ObserverBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
        public ref ObserverBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
        public ref ObserverBuilder WithoutSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
        public ref ObserverBuilder WithoutSecond<TSecond>(string first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
        public ref ObserverBuilder Write(Term term)
        {
            QueryBuilder.Write(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
        public ref ObserverBuilder Write(ulong id)
        {
            QueryBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string)"/>
        public ref ObserverBuilder Write(string name)
        {
            QueryBuilder.Write(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
        public ref ObserverBuilder Write(ulong first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
        public ref ObserverBuilder Write(ulong first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
        public ref ObserverBuilder Write(string first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
        public ref ObserverBuilder Write(string first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
        public ref ObserverBuilder Write<T>()
        {
            QueryBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
        public ref ObserverBuilder Write<T>(T value) where T : Enum
        {
            QueryBuilder.Write(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
        public ref ObserverBuilder Write<TFirst>(ulong second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
        public ref ObserverBuilder Write<TFirst>(string second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
        public ref ObserverBuilder Write<TFirst, TSecond>()
        {
            QueryBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
        public ref ObserverBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
        public ref ObserverBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
        public ref ObserverBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
        public ref ObserverBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
        public ref ObserverBuilder WriteSecond<TSecond>(ulong first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
        public ref ObserverBuilder WriteSecond<TSecond>(string first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
        public ref ObserverBuilder Read(Term term)
        {
            QueryBuilder.Read(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
        public ref ObserverBuilder Read(ulong id)
        {
            QueryBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string)"/>
        public ref ObserverBuilder Read(string name)
        {
            QueryBuilder.Read(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
        public ref ObserverBuilder Read(ulong first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
        public ref ObserverBuilder Read(ulong first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
        public ref ObserverBuilder Read(string first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
        public ref ObserverBuilder Read(string first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
        public ref ObserverBuilder Read<T>()
        {
            QueryBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
        public ref ObserverBuilder Read<T>(T value) where T : Enum
        {
            QueryBuilder.Read(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
        public ref ObserverBuilder Read<TFirst>(ulong second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
        public ref ObserverBuilder Read<TFirst>(string second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
        public ref ObserverBuilder Read<TFirst, TSecond>()
        {
            QueryBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
        public ref ObserverBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
        public ref ObserverBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
        public ref ObserverBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
        public ref ObserverBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
        public ref ObserverBuilder ReadSecond<TSecond>(ulong first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
        public ref ObserverBuilder ReadSecond<TSecond>(string first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
        public ref ObserverBuilder ScopeOpen()
        {
            QueryBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
        public ref ObserverBuilder ScopeClose()
        {
            QueryBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term()"/>
        public ref ObserverBuilder Term()
        {
            QueryBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
        public ref ObserverBuilder TermAt(int termIndex)
        {
            QueryBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
        public ref ObserverBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy(component, compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
        public ref ObserverBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy<T>(compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
        public ref ObserverBuilder GroupBy(ulong component)
        {
            QueryBuilder.GroupBy(component);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
        public ref ObserverBuilder GroupBy<T>()
        {
            QueryBuilder.GroupBy<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
        public ref ObserverBuilder GroupBy(ulong component, Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
        public ref ObserverBuilder GroupBy<T>(Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
        public ref ObserverBuilder GroupBy(ulong component, Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
        public ref ObserverBuilder GroupBy<T>(Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        ///
        public ref ObserverBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            QueryBuilder.GroupByCtx(ctx, contextFree);
            return ref this;
        }

        ///
        public ref ObserverBuilder GroupByCtx(void* ctx)
        {
            QueryBuilder.GroupByCtx(ctx);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
        public ref ObserverBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            QueryBuilder.OnGroupCreate(onGroupCreate);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
        public ref ObserverBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            QueryBuilder.OnGroupDelete(onGroupDelete);
            return ref this;
        }
    }
}
