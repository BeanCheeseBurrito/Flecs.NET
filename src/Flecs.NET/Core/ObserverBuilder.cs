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
        internal FilterBuilder FilterBuilder;
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
            FilterBuilder = new FilterBuilder(world);
            _world = world;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;

            ObserverDesc.entity = ecs_entity_init(world, &entityDesc);
        }

        /// <summary>
        ///     Disposes the observer builder.
        /// </summary>
        public void Dispose()
        {
            ObserverContext.Dispose();
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
            BindingContext.ObserverContext* observerContext = Memory.Alloc<BindingContext.ObserverContext>(1);
            observerContext[0] = ObserverContext;
            BindingContext.SetCallback(ref observerContext->Iterator, userCallback, storeFunctionPointer);

            Ecs.Assert(FilterBuilder.Terms.Count > 0 || FilterBuilder.Desc.expr != null, "Observers require at least 1 term.");
            Ecs.Assert(EventCount != 0,
                "Observer cannot have zero events. Use ObserverBuilder.Event() to add events.");

            ObserverDesc.filter = FilterBuilder.Desc;
            ObserverDesc.filter.terms_buffer = FilterBuilder.Terms.Data;
            ObserverDesc.filter.terms_buffer_count = FilterBuilder.Terms.Count;
            ObserverDesc.binding_ctx = observerContext;
            ObserverDesc.binding_ctx_free = BindingContext.ObserverContextFreePointer;
            ObserverDesc.callback = internalCallback;

            fixed (ecs_observer_desc_t* ptr = &ObserverDesc)
            {
                Entity entity = new Entity(World, ecs_observer_init(World, ptr));
                FilterBuilder.Dispose();
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
            return Desc == other.Desc && FilterBuilder == other.FilterBuilder;
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

    public unsafe partial struct ObserverBuilder
    {
        /// <inheritdoc cref="Core.FilterBuilder.Self"/>
        public ref ObserverBuilder Self()
        {
            FilterBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up"/>
        public ref ObserverBuilder Up(ulong traverse = 0)
        {
            FilterBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up{T}"/>
        public ref ObserverBuilder Up<T>()
        {
            FilterBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade"/>
        public ref ObserverBuilder Cascade(ulong traverse = 0)
        {
            FilterBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade{T}"/>
        public ref ObserverBuilder Cascade<T>()
        {
            FilterBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Descend"/>
        public ref ObserverBuilder Descend()
        {
            FilterBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Parent"/>
        public ref ObserverBuilder Parent()
        {
            FilterBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Trav"/>
        public ref ObserverBuilder Trav(ulong traverse, uint flags = 0)
        {
            FilterBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Id"/>
        public ref ObserverBuilder Id(ulong id)
        {
            FilterBuilder.Id(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Entity"/>
        public ref ObserverBuilder Entity(ulong entity)
        {
            FilterBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Name"/>
        public ref ObserverBuilder Name(string name)
        {
            FilterBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Var"/>
        public ref ObserverBuilder Var(string name)
        {
            FilterBuilder.Var(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Flags"/>
        public ref ObserverBuilder Flags(uint flags)
        {
            FilterBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src()"/>
        public ref ObserverBuilder Src()
        {
            FilterBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First()"/>
        public ref ObserverBuilder First()
        {
            FilterBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second()"/>
        public ref ObserverBuilder Second()
        {
            FilterBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(ulong)"/>
        public ref ObserverBuilder Src(ulong srcId)
        {
            FilterBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src{T}"/>
        public ref ObserverBuilder Src<T>()
        {
            FilterBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(string)"/>
        public ref ObserverBuilder Src(string name)
        {
            FilterBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(ulong)"/>
        public ref ObserverBuilder First(ulong firstId)
        {
            FilterBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First{T}"/>
        public ref ObserverBuilder First<T>()
        {
            FilterBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(string)"/>
        public ref ObserverBuilder First(string name)
        {
            FilterBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(ulong)"/>
        public ref ObserverBuilder Second(ulong secondId)
        {
            FilterBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second{T}"/>
        public ref ObserverBuilder Second<T>()
        {
            FilterBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(string)"/>
        public ref ObserverBuilder Second(string secondName)
        {
            FilterBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Role"/>
        public ref ObserverBuilder Role(ulong role)
        {
            FilterBuilder.Role(role);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut(Flecs.NET.Bindings.Native.ecs_inout_kind_t)"/>
        public ref ObserverBuilder InOut(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutStage"/>
        public ref ObserverBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write()"/>
        public ref ObserverBuilder Write()
        {
            FilterBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read()"/>
        public ref ObserverBuilder Read()
        {
            FilterBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadWrite"/>
        public ref ObserverBuilder ReadWrite()
        {
            FilterBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.In"/>
        public ref ObserverBuilder In()
        {
            FilterBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Out"/>
        public ref ObserverBuilder Out()
        {
            FilterBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut()"/>
        public ref ObserverBuilder InOut()
        {
            FilterBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutNone"/>
        public ref ObserverBuilder InOutNone()
        {
            FilterBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Oper"/>
        public ref ObserverBuilder Oper(ecs_oper_kind_t oper)
        {
            FilterBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.And"/>
        public ref ObserverBuilder And()
        {
            FilterBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Or"/>
        public ref ObserverBuilder Or()
        {
            FilterBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Not"/>
        public ref ObserverBuilder Not()
        {
            FilterBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Optional"/>
        public ref ObserverBuilder Optional()
        {
            FilterBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.AndFrom"/>
        public ref ObserverBuilder AndFrom()
        {
            FilterBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.OrFrom"/>
        public ref ObserverBuilder OrFrom()
        {
            FilterBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.NotFrom"/>
        public ref ObserverBuilder NotFrom()
        {
            FilterBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Singleton"/>
        public ref ObserverBuilder Singleton()
        {
            FilterBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Filter"/>
        public ref ObserverBuilder Filter()
        {
            FilterBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Instanced"/>
        public ref ObserverBuilder Instanced()
        {
            FilterBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.FilterFlags"/>
        public ref ObserverBuilder FilterFlags(uint flags)
        {
            FilterBuilder.FilterFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Expr"/>
        public ref ObserverBuilder Expr(string expr)
        {
            FilterBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong)"/>
        public ref ObserverBuilder With(ulong id)
        {
            FilterBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, ulong)"/>
        public ref ObserverBuilder With(ulong first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, string)"/>
        public ref ObserverBuilder With(ulong first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, ulong)"/>
        public ref ObserverBuilder With(string first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, string)"/>
        public ref ObserverBuilder With(string first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}()"/>
        public ref ObserverBuilder With<T>()
        {
            FilterBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(T)"/>
        public ref ObserverBuilder With<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.With(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(ulong)"/>
        public ref ObserverBuilder With<TFirst>(ulong second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(string)"/>
        public ref ObserverBuilder With<TFirst>(string second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}()"/>
        public ref ObserverBuilder With<TFirst, TSecond>()
        {
            FilterBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}(T2)"/>
        public ref ObserverBuilder With<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.With<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(ulong)"/>
        public ref ObserverBuilder WithSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(string)"/>
        public ref ObserverBuilder WithSecond<TSecond>(string first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong)"/>
        public ref ObserverBuilder Without(ulong id)
        {
            FilterBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, ulong)"/>
        public ref ObserverBuilder Without(ulong first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, string)"/>
        public ref ObserverBuilder Without(ulong first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, ulong)"/>
        public ref ObserverBuilder Without(string first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, string)"/>
        public ref ObserverBuilder Without(string first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}()"/>
        public ref ObserverBuilder Without<T>()
        {
            FilterBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(T)"/>
        public ref ObserverBuilder Without<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Without(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(ulong)"/>
        public ref ObserverBuilder Without<TFirst>(ulong second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(string)"/>
        public ref ObserverBuilder Without<TFirst>(string second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}()"/>
        public ref ObserverBuilder Without<TFirst, TSecond>()
        {
            FilterBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}(T2)"/>
        public ref ObserverBuilder Without<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Without<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(ulong)"/>
        public ref ObserverBuilder WithoutSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(string)"/>
        public ref ObserverBuilder WithoutSecond<TSecond>(string first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong)"/>
        public ref ObserverBuilder Write(ulong id)
        {
            FilterBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, ulong)"/>
        public ref ObserverBuilder Write(ulong first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, string)"/>
        public ref ObserverBuilder Write(ulong first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, ulong)"/>
        public ref ObserverBuilder Write(string first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, string)"/>
        public ref ObserverBuilder Write(string first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}()"/>
        public ref ObserverBuilder Write<T>()
        {
            FilterBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(T)"/>
        public ref ObserverBuilder Write<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Write(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(ulong)"/>
        public ref ObserverBuilder Write<TFirst>(ulong second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(string)"/>
        public ref ObserverBuilder Write<TFirst>(string second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}()"/>
        public ref ObserverBuilder Write<TFirst, TSecond>()
        {
            FilterBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}(T2)"/>
        public ref ObserverBuilder Write<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Write<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(ulong)"/>
        public ref ObserverBuilder WriteSecond<TSecond>(ulong first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(string)"/>
        public ref ObserverBuilder WriteSecond<TSecond>(string first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong)"/>
        public ref ObserverBuilder Read(ulong id)
        {
            FilterBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, ulong)"/>
        public ref ObserverBuilder Read(ulong first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, string)"/>
        public ref ObserverBuilder Read(ulong first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, ulong)"/>
        public ref ObserverBuilder Read(string first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, string)"/>
        public ref ObserverBuilder Read(string first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}()"/>
        public ref ObserverBuilder Read<T>()
        {
            FilterBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(T)"/>
        public ref ObserverBuilder Read<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Read(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(ulong)"/>
        public ref ObserverBuilder Read<TFirst>(ulong second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(string)"/>
        public ref ObserverBuilder Read<TFirst>(string second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}()"/>
        public ref ObserverBuilder Read<TFirst, TSecond>()
        {
            FilterBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}(T2)"/>
        public ref ObserverBuilder Read<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Read<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(ulong)"/>
        public ref ObserverBuilder ReadSecond<TSecond>(ulong first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(string)"/>
        public ref ObserverBuilder ReadSecond<TSecond>(string first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeOpen"/>
        public ref ObserverBuilder ScopeOpen()
        {
            FilterBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeClose"/>
        public ref ObserverBuilder ScopeClose()
        {
            FilterBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.IncrementTerm"/>
        public ref ObserverBuilder IncrementTerm()
        {
            FilterBuilder.IncrementTerm();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermAt"/>
        public ref ObserverBuilder TermAt(int termIndex)
        {
            FilterBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Arg"/>
        public ref ObserverBuilder Arg(int termIndex)
        {
            FilterBuilder.Arg(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term()"/>
        public ref ObserverBuilder Term()
        {
            FilterBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(Core.Term)"/>
        public ref ObserverBuilder Term(Term term)
        {
            FilterBuilder.Term(term);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong)"/>
        public ref ObserverBuilder Term(ulong id)
        {
            FilterBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string)"/>
        public ref ObserverBuilder Term(string name)
        {
            FilterBuilder.Term(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, ulong)"/>
        public ref ObserverBuilder Term(ulong first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, string)"/>
        public ref ObserverBuilder Term(ulong first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, ulong)"/>
        public ref ObserverBuilder Term(string first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, string)"/>
        public ref ObserverBuilder Term(string first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}()"/>
        public ref ObserverBuilder Term<T>()
        {
            FilterBuilder.Term<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(T)"/>
        public ref ObserverBuilder Term<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Term(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(ulong)"/>
        public ref ObserverBuilder Term<TFirst>(ulong second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(string)"/>
        public ref ObserverBuilder Term<TFirst>(string second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}()"/>
        public ref ObserverBuilder Term<TFirst, TSecond>()
        {
            FilterBuilder.Term<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}(T2)"/>
        public ref ObserverBuilder Term<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Term<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(ulong)"/>
        public ref ObserverBuilder TermSecond<TSecond>(ulong first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(string)"/>
        public ref ObserverBuilder TermSecond<TSecond>(string first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }
    }
}
