using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_system_desc_t.
    /// </summary>
    public unsafe partial struct RoutineBuilder : IDisposable, IEquatable<RoutineBuilder>
    {
        private ecs_world_t* _world;

        internal ecs_system_desc_t RoutineDesc;
        internal QueryBuilder QueryBuilder;
        internal BindingContext.RoutineContext RoutineContext;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the routine description.
        /// </summary>
        public ref ecs_system_desc_t Desc => ref RoutineDesc;

        /// <summary>
        ///     Creates a routine builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public RoutineBuilder(ecs_world_t* world, string? name = null)
        {
            QueryBuilder = new QueryBuilder(world);
            RoutineDesc = default;
            RoutineContext = default;
            _world = world;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;

            RoutineDesc.entity = ecs_entity_init(world, &entityDesc);

            ecs_add_id(world, RoutineDesc.entity, Macros.DependsOn(EcsOnUpdate));
            ecs_add_id(world, RoutineDesc.entity, EcsOnUpdate);
        }

        /// <summary>
        ///     Disposes the routine builder. This should be called if the routine builder
        ///     will be discarded and .Iter()/Each() isn't called.
        /// </summary>
        public void Dispose()
        {
            RoutineContext.Dispose();
            QueryBuilder.Dispose();
        }

        /// <summary>
        ///     Specify in which phase the system should run.
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public ref RoutineBuilder Kind(ulong phase)
        {
            ulong currentPhase = ecs_get_target(World, RoutineDesc.entity, EcsDependsOn, 0);

            if (currentPhase != 0)
            {
                ecs_remove_id(World, RoutineDesc.entity, Macros.DependsOn(currentPhase));
                ecs_remove_id(World, RoutineDesc.entity, currentPhase);
            }

            if (phase != 0)
            {
                ecs_add_id(World, RoutineDesc.entity, Macros.DependsOn(phase));
                ecs_add_id(World, RoutineDesc.entity, phase);
            }

            return ref this;
        }

        /// <summary>
        ///     Specify in which phase the system should run.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref RoutineBuilder Kind<T>()
        {
            return ref Kind(Type<T>.Id(World));
        }

        /// <summary>
        ///     Specify in which phase the system should run.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref RoutineBuilder Kind<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Kind(EnumType<TEnum>.Id(enumMember, World));
        }

        /// <summary>
        ///     Specify whether system can run on multiple threads.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref RoutineBuilder MultiThreaded(bool value = true)
        {
            RoutineDesc.multi_threaded = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Specify whether system should be ran in staged context.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref RoutineBuilder Immediate(bool value = true)
        {
            RoutineDesc.immediate = Macros.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Set system interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public ref RoutineBuilder Interval(float interval)
        {
            RoutineDesc.interval = interval;
            return ref this;
        }

        /// <summary>
        ///     Set system rate.
        /// </summary>
        /// <param name="tickSource"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ref RoutineBuilder Rate(ulong tickSource, int rate)
        {
            RoutineDesc.rate = rate;
            RoutineDesc.tick_source = tickSource;
            return ref this;
        }

        /// <summary>
        ///     Set system rate.
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ref RoutineBuilder Rate(int rate)
        {
            RoutineDesc.rate = rate;
            return ref this;
        }

        /// <summary>
        ///     Set tick source.
        /// </summary>
        /// <param name="tickSource"></param>
        /// <returns></returns>
        public ref RoutineBuilder TickSource(ulong tickSource)
        {
            RoutineDesc.tick_source = tickSource;
            return ref this;
        }

        /// <summary>
        ///     Set tick source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref RoutineBuilder TickSource<T>()
        {
            RoutineDesc.tick_source = Type<T>.Id(World);
            return ref this;
        }

        /// <summary>
        ///     Set system context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ref RoutineBuilder Ctx(void* ctx)
        {
            RoutineDesc.ctx = ctx;
            return ref this;
        }

        /// <summary>
        ///     Set system run callback.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ref RoutineBuilder Run(Ecs.IterAction action)
        {
            BindingContext.SetCallback(ref RoutineContext.Run, action);
            RoutineDesc.run = RoutineContext.Run.Function;
            return ref this;
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Routine Iter(Action callback)
        {
            return Build(ref callback, BindingContext.RoutineActionPointer, false);
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Routine Iter(Ecs.IterCallback callback)
        {
            return Build(ref callback, BindingContext.RoutineIterPointer, false);
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Routine Each(Ecs.EachEntityCallback callback)
        {
            return Build(ref callback, BindingContext.RoutineEachEntityPointer, false);
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Routine Each(Ecs.EachIndexCallback callback)
        {
            return Build(ref callback, BindingContext.RoutineEachIndexPointer, false);
        }

        private Routine Build<T>(ref T userCallback, IntPtr internalCallback, bool storeFunctionPointer)
            where T : Delegate
        {
            BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
            queryContext[0] = QueryBuilder.Context;

            BindingContext.RoutineContext* routineContext = Memory.Alloc<BindingContext.RoutineContext>(1);
            routineContext[0] = RoutineContext;
            BindingContext.SetCallback(ref routineContext->Iterator, userCallback, storeFunctionPointer);

            RoutineDesc.query = QueryBuilder.Desc;
            RoutineDesc.query.binding_ctx = queryContext;
            RoutineDesc.query.binding_ctx_free = BindingContext.QueryContextFreePointer;
            RoutineDesc.binding_ctx = routineContext;
            RoutineDesc.binding_ctx_free = BindingContext.RoutineContextFreePointer;
            RoutineDesc.callback = internalCallback;

            fixed (ecs_system_desc_t* ptr = &RoutineDesc)
            {
                Entity entity = new Entity(World, ecs_system_init(World, ptr));
                return new Routine(entity);
            }
        }

        /// <summary>
        ///     Checks if two <see cref="RoutineBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RoutineBuilder other)
        {
            return Desc == other.Desc && QueryBuilder == other.QueryBuilder;
        }

        /// <summary>
        ///     Checks if two <see cref="RoutineBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is RoutineBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="RoutineBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(RoutineDesc.GetHashCode(), QueryBuilder.GetHashCode());
        }

        /// <summary>
        ///     Checks if two <see cref="RoutineBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(RoutineBuilder left, RoutineBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="RoutineBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(RoutineBuilder left, RoutineBuilder right)
        {
            return !(left == right);
        }
    }

    // QueryBuilder Extensions
    public unsafe partial struct RoutineBuilder
    {
        /// <inheritdoc cref="QueryBuilder.Self()"/>
        public ref RoutineBuilder Self()
        {
            QueryBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
        public ref RoutineBuilder Id(ulong id)
        {
            QueryBuilder.Id(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
        public ref RoutineBuilder Entity(ulong entity)
        {
            QueryBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Name(string)"/>
        public ref RoutineBuilder Name(string name)
        {
            QueryBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Var(string)"/>
        public ref RoutineBuilder Var(string name)
        {
            QueryBuilder.Var(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Flags(uint)"/>
        public ref RoutineBuilder Flags(uint flags)
        {
            QueryBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
        public ref RoutineBuilder Term(ulong id)
        {
            QueryBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src()"/>
        public ref RoutineBuilder Src()
        {
            QueryBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First()"/>
        public ref RoutineBuilder First()
        {
            QueryBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second()"/>
        public ref RoutineBuilder Second()
        {
            QueryBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
        public ref RoutineBuilder Src(ulong srcId)
        {
            QueryBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
        public ref RoutineBuilder Src<T>()
        {
            QueryBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
        public ref RoutineBuilder Src<T>(T value) where T : Enum
        {
            QueryBuilder.Src(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(string)"/>
        public ref RoutineBuilder Src(string name)
        {
            QueryBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
        public ref RoutineBuilder First(ulong firstId)
        {
            QueryBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}()"/>
        public ref RoutineBuilder First<T>()
        {
            QueryBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
        public ref RoutineBuilder First<T>(T value) where T : Enum
        {
            QueryBuilder.First(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(string)"/>
        public ref RoutineBuilder First(string name)
        {
            QueryBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
        public ref RoutineBuilder Second(ulong secondId)
        {
            QueryBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
        public ref RoutineBuilder Second<T>()
        {
            QueryBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
        public ref RoutineBuilder Second<T>(T value) where T : Enum
        {
            QueryBuilder.Second(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(string)"/>
        public ref RoutineBuilder Second(string secondName)
        {
            QueryBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
        public ref RoutineBuilder Up(ulong traverse = 0)
        {
            QueryBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
        public ref RoutineBuilder Up<T>()
        {
            QueryBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
        public ref RoutineBuilder Up<T>(T value) where T : Enum
        {
            QueryBuilder.Up(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
        public ref RoutineBuilder Cascade(ulong traverse = 0)
        {
            QueryBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
        public ref RoutineBuilder Cascade<T>()
        {
            QueryBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
        public ref RoutineBuilder Cascade<T>(T value) where T : Enum
        {
            QueryBuilder.Cascade(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Descend()"/>
        public ref RoutineBuilder Descend()
        {
            QueryBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Parent()"/>
        public ref RoutineBuilder Parent()
        {
            QueryBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
        public ref RoutineBuilder Trav(ulong traverse, uint flags = 0)
        {
            QueryBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
        public ref RoutineBuilder Trav<T>(uint flags = 0)
        {
            QueryBuilder.Trav<T>(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
        public ref RoutineBuilder Trav<T>(T value, uint flags = 0) where T : Enum
        {
            QueryBuilder.Trav(value, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.IdFlags(ulong)"/>
        public ref RoutineBuilder IdFlags(ulong flags)
        {
            QueryBuilder.IdFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
        public ref RoutineBuilder InOut(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
        public ref RoutineBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write()"/>
        public ref RoutineBuilder Write()
        {
            QueryBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read()"/>
        public ref RoutineBuilder Read()
        {
            QueryBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
        public ref RoutineBuilder ReadWrite()
        {
            QueryBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.In()"/>
        public ref RoutineBuilder In()
        {
            QueryBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Out()"/>
        public ref RoutineBuilder Out()
        {
            QueryBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut()"/>
        public ref RoutineBuilder InOut()
        {
            QueryBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
        public ref RoutineBuilder InOutNone()
        {
            QueryBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
        public ref RoutineBuilder Oper(ecs_oper_kind_t oper)
        {
            QueryBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.And()"/>
        public ref RoutineBuilder And()
        {
            QueryBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Or()"/>
        public ref RoutineBuilder Or()
        {
            QueryBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Not()"/>
        public ref RoutineBuilder Not()
        {
            QueryBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Optional()"/>
        public ref RoutineBuilder Optional()
        {
            QueryBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
        public ref RoutineBuilder AndFrom()
        {
            QueryBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
        public ref RoutineBuilder OrFrom()
        {
            QueryBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
        public ref RoutineBuilder NotFrom()
        {
            QueryBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Singleton()"/>
        public ref RoutineBuilder Singleton()
        {
            QueryBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Filter()"/>
        public ref RoutineBuilder Filter()
        {
            QueryBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Instanced()"/>
        public ref RoutineBuilder Instanced()
        {
            QueryBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.QueryFlags(uint)"/>
        public ref RoutineBuilder QueryFlags(uint flags)
        {
            QueryBuilder.QueryFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
        public ref RoutineBuilder CacheKind(ecs_query_cache_kind_t kind)
        {
            QueryBuilder.CacheKind(kind);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cached()"/>
        public ref RoutineBuilder Cached()
        {
            QueryBuilder.Cached();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
        public ref RoutineBuilder Expr(string expr)
        {
            QueryBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
        public ref RoutineBuilder With(Term term)
        {
            QueryBuilder.With(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
        public ref RoutineBuilder With(ulong id)
        {
            QueryBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string)"/>
        public ref RoutineBuilder With(string name)
        {
            QueryBuilder.With(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
        public ref RoutineBuilder With(ulong first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
        public ref RoutineBuilder With(ulong first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
        public ref RoutineBuilder With(string first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
        public ref RoutineBuilder With(string first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}()"/>
        public ref RoutineBuilder With<T>()
        {
            QueryBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
        public ref RoutineBuilder With<T>(T value) where T : Enum
        {
            QueryBuilder.With(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
        public ref RoutineBuilder With<TFirst>(ulong second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
        public ref RoutineBuilder With<TFirst>(string second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
        public ref RoutineBuilder With<TFirst, TSecond>()
        {
            QueryBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
        public ref RoutineBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
        public ref RoutineBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
        public ref RoutineBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
        public ref RoutineBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
        public ref RoutineBuilder WithSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
        public ref RoutineBuilder WithSecond<TSecond>(string first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
        public ref RoutineBuilder Without(Term term)
        {
            QueryBuilder.Without(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
        public ref RoutineBuilder Without(ulong id)
        {
            QueryBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string)"/>
        public ref RoutineBuilder Without(string name)
        {
            QueryBuilder.Without(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
        public ref RoutineBuilder Without(ulong first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
        public ref RoutineBuilder Without(ulong first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
        public ref RoutineBuilder Without(string first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
        public ref RoutineBuilder Without(string first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
        public ref RoutineBuilder Without<T>()
        {
            QueryBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
        public ref RoutineBuilder Without<T>(T value) where T : Enum
        {
            QueryBuilder.Without(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
        public ref RoutineBuilder Without<TFirst>(ulong second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
        public ref RoutineBuilder Without<TFirst>(string second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
        public ref RoutineBuilder Without<TFirst, TSecond>()
        {
            QueryBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
        public ref RoutineBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
        public ref RoutineBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
        public ref RoutineBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
        public ref RoutineBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
        public ref RoutineBuilder WithoutSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
        public ref RoutineBuilder WithoutSecond<TSecond>(string first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
        public ref RoutineBuilder Write(Term term)
        {
            QueryBuilder.Write(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
        public ref RoutineBuilder Write(ulong id)
        {
            QueryBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string)"/>
        public ref RoutineBuilder Write(string name)
        {
            QueryBuilder.Write(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
        public ref RoutineBuilder Write(ulong first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
        public ref RoutineBuilder Write(ulong first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
        public ref RoutineBuilder Write(string first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
        public ref RoutineBuilder Write(string first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
        public ref RoutineBuilder Write<T>()
        {
            QueryBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
        public ref RoutineBuilder Write<T>(T value) where T : Enum
        {
            QueryBuilder.Write(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
        public ref RoutineBuilder Write<TFirst>(ulong second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
        public ref RoutineBuilder Write<TFirst>(string second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
        public ref RoutineBuilder Write<TFirst, TSecond>()
        {
            QueryBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
        public ref RoutineBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
        public ref RoutineBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
        public ref RoutineBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
        public ref RoutineBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
        public ref RoutineBuilder WriteSecond<TSecond>(ulong first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
        public ref RoutineBuilder WriteSecond<TSecond>(string first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
        public ref RoutineBuilder Read(Term term)
        {
            QueryBuilder.Read(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
        public ref RoutineBuilder Read(ulong id)
        {
            QueryBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string)"/>
        public ref RoutineBuilder Read(string name)
        {
            QueryBuilder.Read(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
        public ref RoutineBuilder Read(ulong first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
        public ref RoutineBuilder Read(ulong first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
        public ref RoutineBuilder Read(string first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
        public ref RoutineBuilder Read(string first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
        public ref RoutineBuilder Read<T>()
        {
            QueryBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
        public ref RoutineBuilder Read<T>(T value) where T : Enum
        {
            QueryBuilder.Read(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
        public ref RoutineBuilder Read<TFirst>(ulong second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
        public ref RoutineBuilder Read<TFirst>(string second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
        public ref RoutineBuilder Read<TFirst, TSecond>()
        {
            QueryBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
        public ref RoutineBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
        public ref RoutineBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
        public ref RoutineBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
        public ref RoutineBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
        public ref RoutineBuilder ReadSecond<TSecond>(ulong first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
        public ref RoutineBuilder ReadSecond<TSecond>(string first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
        public ref RoutineBuilder ScopeOpen()
        {
            QueryBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
        public ref RoutineBuilder ScopeClose()
        {
            QueryBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term()"/>
        public ref RoutineBuilder Term()
        {
            QueryBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
        public ref RoutineBuilder TermAt(int termIndex)
        {
            QueryBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
        public ref RoutineBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy(component, compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
        public ref RoutineBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy<T>(compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
        public ref RoutineBuilder GroupBy(ulong component)
        {
            QueryBuilder.GroupBy(component);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
        public ref RoutineBuilder GroupBy<T>()
        {
            QueryBuilder.GroupBy<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
        public ref RoutineBuilder GroupBy(ulong component, Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
        public ref RoutineBuilder GroupBy<T>(Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
        public ref RoutineBuilder GroupBy(ulong component, Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
        public ref RoutineBuilder GroupBy<T>(Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        ///
        public ref RoutineBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            QueryBuilder.GroupByCtx(ctx, contextFree);
            return ref this;
        }

        ///
        public ref RoutineBuilder GroupByCtx(void* ctx)
        {
            QueryBuilder.GroupByCtx(ctx);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
        public ref RoutineBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            QueryBuilder.OnGroupCreate(onGroupCreate);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
        public ref RoutineBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            QueryBuilder.OnGroupDelete(onGroupDelete);
            return ref this;
        }
    }
}
