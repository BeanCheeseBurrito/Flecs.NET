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

        internal ref FilterBuilder FilterBuilder => ref QueryBuilder.FilterBuilder;

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
        ///     Disposes the routine builder.
        /// </summary>
        public void Dispose()
        {
            RoutineContext.Dispose();
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
        public ref RoutineBuilder NoReadonly(bool value = true)
        {
            RoutineDesc.no_readonly = Macros.Bool(value);
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
            queryContext[0] = QueryBuilder.QueryContext;

            BindingContext.RoutineContext* routineContext = Memory.Alloc<BindingContext.RoutineContext>(1);
            routineContext[0] = RoutineContext;
            routineContext->QueryContext = QueryBuilder.QueryContext;
            BindingContext.SetCallback(ref routineContext->Iterator, userCallback, storeFunctionPointer);

            RoutineDesc.query = QueryBuilder.QueryDesc;
            RoutineDesc.query.filter = FilterBuilder.FilterDesc;
            RoutineDesc.query.filter.terms_buffer = FilterBuilder.Terms.Data;
            RoutineDesc.query.filter.terms_buffer_count = FilterBuilder.Terms.Count;
            RoutineDesc.query.binding_ctx = queryContext;
            RoutineDesc.query.binding_ctx_free = BindingContext.QueryContextFreePointer;
            RoutineDesc.binding_ctx = routineContext;
            RoutineDesc.binding_ctx_free = BindingContext.RoutineContextFreePointer;
            RoutineDesc.callback = internalCallback;

            fixed (ecs_system_desc_t* ptr = &RoutineDesc)
            {
                Entity entity = new Entity(World, ecs_system_init(World, ptr));
                FilterBuilder.Dispose();
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

    // FilterBuilder extensions.
    public unsafe partial struct RoutineBuilder
    {
        /// <inheritdoc cref="Core.FilterBuilder.Self"/>
        public ref RoutineBuilder Self()
        {
            FilterBuilder.Self();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Up"/>
        public ref RoutineBuilder Up(ulong traverse = 0)
        {
            FilterBuilder.Up(traverse);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Up{T}"/>
        public ref RoutineBuilder Up<T>()
        {
            FilterBuilder.Up<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Cascade"/>
        public ref RoutineBuilder Cascade(ulong traverse = 0)
        {
            FilterBuilder.Cascade(traverse);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Cascade{T}"/>
        public ref RoutineBuilder Cascade<T>()
        {
            FilterBuilder.Cascade<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Descend"/>
        public ref RoutineBuilder Descend()
        {
            FilterBuilder.Descend();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Parent"/>
        public ref RoutineBuilder Parent()
        {
            FilterBuilder.Parent();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Trav"/>
        public ref RoutineBuilder Trav(ulong traverse, uint flags = 0)
        {
            FilterBuilder.Trav(traverse, flags);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Id"/>
        public ref RoutineBuilder Id(ulong id)
        {
            FilterBuilder.Id(id);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Entity"/>
        public ref RoutineBuilder Entity(ulong entity)
        {
            FilterBuilder.Entity(entity);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Name"/>
        public ref RoutineBuilder Name(string name)
        {
            FilterBuilder.Name(name);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Var"/>
        public ref RoutineBuilder Var(string name)
        {
            FilterBuilder.Var(name);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Flags"/>
        public ref RoutineBuilder Flags(uint flags)
        {
            FilterBuilder.Flags(flags);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Src()"/>
        public ref RoutineBuilder Src()
        {
            FilterBuilder.Src();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.First()"/>
        public ref RoutineBuilder First()
        {
            FilterBuilder.First();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Second()"/>
        public ref RoutineBuilder Second()
        {
            FilterBuilder.Second();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Src(ulong)"/>
        public ref RoutineBuilder Src(ulong srcId)
        {
            FilterBuilder.Src(srcId);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Src{T}"/>
        public ref RoutineBuilder Src<T>()
        {
            FilterBuilder.Src<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Src(string)"/>
        public ref RoutineBuilder Src(string name)
        {
            FilterBuilder.Src(name);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.First(ulong)"/>
        public ref RoutineBuilder First(ulong firstId)
        {
            FilterBuilder.First(firstId);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.First{T}"/>
        public ref RoutineBuilder First<T>()
        {
            FilterBuilder.First<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.First(string)"/>
        public ref RoutineBuilder First(string name)
        {
            FilterBuilder.First(name);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Second(ulong)"/>
        public ref RoutineBuilder Second(ulong secondId)
        {
            FilterBuilder.Second(secondId);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Second{T}"/>
        public ref RoutineBuilder Second<T>()
        {
            FilterBuilder.Second<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Second(string)"/>
        public ref RoutineBuilder Second(string secondName)
        {
            FilterBuilder.Second(secondName);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Role"/>
        public ref RoutineBuilder Role(ulong role)
        {
            FilterBuilder.Role(role);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.InOut(Flecs.NET.Bindings.Native.ecs_inout_kind_t)"/>
        public ref RoutineBuilder InOut(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOut(inOut);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.InOutStage"/>
        public ref RoutineBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOutStage(inOut);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write()"/>
        public ref RoutineBuilder Write()
        {
            FilterBuilder.Write();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read()"/>
        public ref RoutineBuilder Read()
        {
            FilterBuilder.Read();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.ReadWrite"/>
        public ref RoutineBuilder ReadWrite()
        {
            FilterBuilder.ReadWrite();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.In"/>
        public ref RoutineBuilder In()
        {
            FilterBuilder.In();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Out"/>
        public ref RoutineBuilder Out()
        {
            FilterBuilder.Out();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.InOut()"/>
        public ref RoutineBuilder InOut()
        {
            FilterBuilder.InOut();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.InOutNone"/>
        public ref RoutineBuilder InOutNone()
        {
            FilterBuilder.InOutNone();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Oper"/>
        public ref RoutineBuilder Oper(ecs_oper_kind_t oper)
        {
            FilterBuilder.Oper(oper);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.And"/>
        public ref RoutineBuilder And()
        {
            FilterBuilder.And();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Or"/>
        public ref RoutineBuilder Or()
        {
            FilterBuilder.Or();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Not"/>
        public ref RoutineBuilder Not()
        {
            FilterBuilder.Not();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Optional"/>
        public ref RoutineBuilder Optional()
        {
            FilterBuilder.Optional();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.AndFrom"/>
        public ref RoutineBuilder AndFrom()
        {
            FilterBuilder.AndFrom();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.OrFrom"/>
        public ref RoutineBuilder OrFrom()
        {
            FilterBuilder.OrFrom();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.NotFrom"/>
        public ref RoutineBuilder NotFrom()
        {
            FilterBuilder.NotFrom();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Singleton"/>
        public ref RoutineBuilder Singleton()
        {
            FilterBuilder.Singleton();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Filter"/>
        public ref RoutineBuilder Filter()
        {
            FilterBuilder.Filter();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Instanced"/>
        public ref RoutineBuilder Instanced()
        {
            FilterBuilder.Instanced();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.FilterFlags"/>
        public ref RoutineBuilder FilterFlags(uint flags)
        {
            FilterBuilder.FilterFlags(flags);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Expr"/>
        public ref RoutineBuilder Expr(string expr)
        {
            FilterBuilder.Expr(expr);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With(ulong)"/>
        public ref RoutineBuilder With(ulong id)
        {
            FilterBuilder.With(id);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, ulong)"/>
        public ref RoutineBuilder With(ulong first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, string)"/>
        public ref RoutineBuilder With(ulong first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With(string, ulong)"/>
        public ref RoutineBuilder With(string first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With(string, string)"/>
        public ref RoutineBuilder With(string first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With{T}()"/>
        public ref RoutineBuilder With<T>()
        {
            FilterBuilder.With<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With{T}(T)"/>
        public ref RoutineBuilder With<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.With(enumMember);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With{T}(ulong)"/>
        public ref RoutineBuilder With<TFirst>(ulong second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With{T}(string)"/>
        public ref RoutineBuilder With<TFirst>(string second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}()"/>
        public ref RoutineBuilder With<TFirst, TSecond>()
        {
            FilterBuilder.With<TFirst, TSecond>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}(T2)"/>
        public ref RoutineBuilder With<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.With<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(ulong)"/>
        public ref RoutineBuilder WithSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(string)"/>
        public ref RoutineBuilder WithSecond<TSecond>(string first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong)"/>
        public ref RoutineBuilder Without(ulong id)
        {
            FilterBuilder.Without(id);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, ulong)"/>
        public ref RoutineBuilder Without(ulong first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, string)"/>
        public ref RoutineBuilder Without(ulong first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without(string, ulong)"/>
        public ref RoutineBuilder Without(string first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without(string, string)"/>
        public ref RoutineBuilder Without(string first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without{T}()"/>
        public ref RoutineBuilder Without<T>()
        {
            FilterBuilder.Without<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(T)"/>
        public ref RoutineBuilder Without<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Without(enumMember);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(ulong)"/>
        public ref RoutineBuilder Without<TFirst>(ulong second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(string)"/>
        public ref RoutineBuilder Without<TFirst>(string second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}()"/>
        public ref RoutineBuilder Without<TFirst, TSecond>()
        {
            FilterBuilder.Without<TFirst, TSecond>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}(T2)"/>
        public ref RoutineBuilder Without<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Without<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(ulong)"/>
        public ref RoutineBuilder WithoutSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(string)"/>
        public ref RoutineBuilder WithoutSecond<TSecond>(string first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong)"/>
        public ref RoutineBuilder Write(ulong id)
        {
            FilterBuilder.Write(id);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, ulong)"/>
        public ref RoutineBuilder Write(ulong first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, string)"/>
        public ref RoutineBuilder Write(ulong first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write(string, ulong)"/>
        public ref RoutineBuilder Write(string first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write(string, string)"/>
        public ref RoutineBuilder Write(string first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write{T}()"/>
        public ref RoutineBuilder Write<T>()
        {
            FilterBuilder.Write<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(T)"/>
        public ref RoutineBuilder Write<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Write(enumMember);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(ulong)"/>
        public ref RoutineBuilder Write<TFirst>(ulong second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(string)"/>
        public ref RoutineBuilder Write<TFirst>(string second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}()"/>
        public ref RoutineBuilder Write<TFirst, TSecond>()
        {
            FilterBuilder.Write<TFirst, TSecond>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}(T2)"/>
        public ref RoutineBuilder Write<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Write<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(ulong)"/>
        public ref RoutineBuilder WriteSecond<TSecond>(ulong first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(string)"/>
        public ref RoutineBuilder WriteSecond<TSecond>(string first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong)"/>
        public ref RoutineBuilder Read(ulong id)
        {
            FilterBuilder.Read(id);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, ulong)"/>
        public ref RoutineBuilder Read(ulong first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, string)"/>
        public ref RoutineBuilder Read(ulong first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read(string, ulong)"/>
        public ref RoutineBuilder Read(string first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read(string, string)"/>
        public ref RoutineBuilder Read(string first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read{T}()"/>
        public ref RoutineBuilder Read<T>()
        {
            FilterBuilder.Read<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(T)"/>
        public ref RoutineBuilder Read<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Read(enumMember);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(ulong)"/>
        public ref RoutineBuilder Read<TFirst>(ulong second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(string)"/>
        public ref RoutineBuilder Read<TFirst>(string second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}()"/>
        public ref RoutineBuilder Read<TFirst, TSecond>()
        {
            FilterBuilder.Read<TFirst, TSecond>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}(T2)"/>
        public ref RoutineBuilder Read<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Read<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(ulong)"/>
        public ref RoutineBuilder ReadSecond<TSecond>(ulong first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(string)"/>
        public ref RoutineBuilder ReadSecond<TSecond>(string first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.ScopeOpen"/>
        public ref RoutineBuilder ScopeOpen()
        {
            FilterBuilder.ScopeOpen();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.ScopeClose"/>
        public ref RoutineBuilder ScopeClose()
        {
            FilterBuilder.ScopeClose();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.IncrementTerm"/>
        public ref RoutineBuilder IncrementTerm()
        {
            FilterBuilder.IncrementTerm();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.TermAt"/>
        public ref RoutineBuilder TermAt(int termIndex)
        {
            FilterBuilder.TermAt(termIndex);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Arg"/>
        public ref RoutineBuilder Arg(int termIndex)
        {
            FilterBuilder.Arg(termIndex);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong)"/>
        public ref RoutineBuilder Term(ulong id)
        {
            FilterBuilder.Term(id);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term(string)"/>
        public ref RoutineBuilder Term(string name)
        {
            FilterBuilder.Term(name);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, ulong)"/>
        public ref RoutineBuilder Term(ulong first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, string)"/>
        public ref RoutineBuilder Term(ulong first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term(string, ulong)"/>
        public ref RoutineBuilder Term(string first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term(string, string)"/>
        public ref RoutineBuilder Term(string first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term{T}()"/>
        public ref RoutineBuilder Term<T>()
        {
            FilterBuilder.Term<T>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(T)"/>
        public ref RoutineBuilder Term<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Term(enumMember);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(ulong)"/>
        public ref RoutineBuilder Term<TFirst>(ulong second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(string)"/>
        public ref RoutineBuilder Term<TFirst>(string second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}()"/>
        public ref RoutineBuilder Term<TFirst, TSecond>()
        {
            FilterBuilder.Term<TFirst, TSecond>();
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}(T2)"/>
        public ref RoutineBuilder Term<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Term<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(ulong)"/>
        public ref RoutineBuilder TermSecond<TSecond>(ulong first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }
        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(string)"/>
        public ref RoutineBuilder TermSecond<TSecond>(string first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }
    }

    // QueryBuilder extensions.
    public unsafe partial struct RoutineBuilder
    {
        /// <inheritdoc cref="Core.QueryBuilder.OrderBy{T}"/>
        public ref RoutineBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy<T>(compare);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.OrderBy"/>
        public ref RoutineBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy(component, compare);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
        public ref RoutineBuilder GroupBy<T>(Ecs.GroupByAction groupByAction)
        {
            QueryBuilder.GroupBy<T>(groupByAction);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
        public ref RoutineBuilder GroupBy(ulong component, Ecs.GroupByAction groupByAction)
        {
            QueryBuilder.GroupBy(component, groupByAction);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.GroupBy{T}()"/>
        public ref RoutineBuilder GroupBy<T>()
        {
            QueryBuilder.GroupBy<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.GroupBy(ulong)"/>
        public ref RoutineBuilder GroupBy(ulong component)
        {
            QueryBuilder.GroupBy(component);
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

        /// <inheritdoc cref="Core.QueryBuilder.OnGroupCreate"/>
        public ref RoutineBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            QueryBuilder.OnGroupCreate(onGroupCreate);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.OnGroupDelete"/>
        public ref RoutineBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            QueryBuilder.OnGroupDelete(onGroupDelete);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.Observable(Query)"/>
        public ref RoutineBuilder Observable(Query parent)
        {
            QueryBuilder.Observable(parent);
            return ref this;
        }

        /// <inheritdoc cref="Core.QueryBuilder.Observable(ref Query)"/>
        public ref RoutineBuilder Observable(ref Query parent)
        {
            QueryBuilder.Observable(ref parent);
            return ref this;
        }
    }
}
