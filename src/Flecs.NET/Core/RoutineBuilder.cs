using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_system_desc_t"/>.
    /// </summary>
    public unsafe partial struct RoutineBuilder : IDisposable, IEquatable<RoutineBuilder>, IQueryBuilder<RoutineBuilder>
    {
        private ecs_world_t* _world;
        private ecs_system_desc_t _desc;
        private QueryBuilder _queryBuilder;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the routine description.
        /// </summary>
        public ref ecs_system_desc_t Desc => ref _desc;

        /// <summary>
        ///     A reference to the query builder.
        /// </summary>
        public ref QueryBuilder QueryBuilder => ref _queryBuilder;

        /// <summary>
        ///     Creates a routine builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        public RoutineBuilder(ecs_world_t* world)
        {
            _world = world;
            _desc = default;
            _queryBuilder = new QueryBuilder(world);

            ecs_entity_desc_t entityDesc = default;
            Desc.entity = ecs_entity_init(world, &entityDesc);
            ecs_add_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsOnUpdate));
            ecs_add_id(world, Desc.entity, EcsOnUpdate);
        }

        /// <summary>
        ///     Creates a routine builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="name">The routine name.</param>
        public RoutineBuilder(ecs_world_t* world, string name)
        {
            _world = world;
            _desc = default;
            _queryBuilder = new QueryBuilder(world);

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultSeparator;

            Desc.entity = ecs_entity_init(world, &entityDesc);
            ecs_add_id(world, Desc.entity, Ecs.Pair(EcsDependsOn, EcsOnUpdate));
            ecs_add_id(world, Desc.entity, EcsOnUpdate);
        }

        /// <summary>
        ///     Disposes the routine builder. This should be called if the routine builder
        ///     will be discarded and .Iter()/.Each()/.Run() isn't called.
        /// </summary>
        public void Dispose()
        {
            QueryBuilder.Dispose();
            FreeRun();
            FreeCallback();
        }

        /// <summary>
        ///     Specify in which phase the system should run.
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public ref RoutineBuilder Kind(ulong phase)
        {
            ulong currentPhase = ecs_get_target(World, Desc.entity, EcsDependsOn, 0);

            if (currentPhase != 0)
            {
                ecs_remove_id(World, Desc.entity, Ecs.Pair(EcsDependsOn, currentPhase));
                ecs_remove_id(World, Desc.entity, currentPhase);
            }

            if (phase != 0)
            {
                ecs_add_id(World, Desc.entity, Ecs.Pair(EcsDependsOn, phase));
                ecs_add_id(World, Desc.entity, phase);
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
            return ref Kind(Type<TEnum>.Id(World, enumMember));
        }

        /// <summary>
        ///     Specify whether system can run on multiple threads.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref RoutineBuilder MultiThreaded(bool value = true)
        {
            Desc.multi_threaded = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Specify whether system should be ran in staged context.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref RoutineBuilder Immediate(bool value = true)
        {
            Desc.immediate = Utils.Bool(value);
            return ref this;
        }

        /// <summary>
        ///     Set system interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public ref RoutineBuilder Interval(float interval)
        {
            Desc.interval = interval;
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
            Desc.rate = rate;
            Desc.tick_source = tickSource;
            return ref this;
        }

        /// <summary>
        ///     Set system rate.
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public ref RoutineBuilder Rate(int rate)
        {
            Desc.rate = rate;
            return ref this;
        }

        /// <summary>
        ///     Set tick source.
        /// </summary>
        /// <param name="tickSource"></param>
        /// <returns></returns>
        public ref RoutineBuilder TickSource(ulong tickSource)
        {
            Desc.tick_source = tickSource;
            return ref this;
        }

        /// <summary>
        ///     Set tick source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref RoutineBuilder TickSource<T>()
        {
            Desc.tick_source = Type<T>.Id(World);
            return ref this;
        }

        /// <summary>
        ///     Set system context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ref RoutineBuilder Ctx(void* ctx)
        {
            Desc.ctx = ctx;
            return ref this;
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Iter(Action callback)
        {
            return SetCallback(callback, BindingContext.ActionCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Iter(Ecs.IterCallback callback)
        {
            return SetCallback(callback, BindingContext.IterCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Each(Action callback)
        {
            return SetCallback(callback, BindingContext.ActionCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Each(Ecs.EachEntityCallback callback)
        {
            return SetCallback(callback, BindingContext.EachEntityCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Each(Ecs.EachIterCallback callback)
        {
            return SetCallback(callback, BindingContext.EachIterCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Run callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Run(Action callback)
        {
            return SetCallback(callback, BindingContext.ActionCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Run callback.
        /// </summary>
        /// <param name="run">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Run(Ecs.RunCallback run)
        {
            return SetRun(run, BindingContext.RunCallbackPointer).Build();
        }

        /// <summary>
        ///     Sets a run callback. .Iter() or .Each() must be called after this to build the routine.
        /// </summary>
        /// <param name="run">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref RoutineBuilder Run(Ecs.RunDelegateCallback run)
        {
            return ref SetRun(run, BindingContext.RunDelegateCallbackPointer);
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Iter(delegate*<void> callback)
        {
            return SetCallback((IntPtr)callback, BindingContext.ActionCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Iter callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Iter(delegate*<Iter, void> callback)
        {
            return SetCallback((IntPtr)callback, BindingContext.IterCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Each(delegate*<void> callback)
        {
            return SetCallback((IntPtr)callback, BindingContext.ActionCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Each(delegate*<Entity, void> callback)
        {
            return SetCallback((IntPtr)callback, BindingContext.EachEntityCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Each callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Each(delegate*<Iter, int, void> callback)
        {
            return SetCallback((IntPtr)callback, BindingContext.EachIterCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Run callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Run(delegate*<void> callback)
        {
            return SetCallback((IntPtr)callback, BindingContext.ActionCallbackPointer).Build();
        }

        /// <summary>
        ///     Creates a routine with the provided Run callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The created routine.</returns>
        public Routine Run(delegate*<Iter, void> callback)
        {
            return SetRun((IntPtr)callback, BindingContext.RunCallbackPointer).Build();
        }

        /// <summary>
        ///     Sets a run callback. .Iter() or .Each() must be called after this to build the routine.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref RoutineBuilder Run(delegate*<Iter, Action<Iter>, void> callback)
        {
            return ref SetRun((IntPtr)callback, BindingContext.RunDelegateCallbackPointer);
        }

        /// <summary>
        ///     Sets a run callback. .Iter() or .Each() must be called after this to build the routine.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref RoutineBuilder Run(delegate*<Iter, delegate*<Iter, void>, void> callback)
        {
            return ref SetRun((IntPtr)callback, BindingContext.RunPointerCallbackPointer);
        }

        /// <summary>
        ///     Sets a run callback. .Iter() or .Each() must be called after this to build the routine.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public ref RoutineBuilder Run(Ecs.RunPointerCallback callback)
        {
            return ref SetRun(callback, BindingContext.RunPointerCallbackPointer);
        }

        private ref RoutineBuilder SetCallback<T>(T callback, IntPtr invoker) where T : Delegate
        {
            FreeCallback();
            BindingContext.IteratorContext context = default;
            BindingContext.SetCallback(ref context.Callback, callback, false);
            Desc.callback = invoker;
            Desc.callback_ctx = Memory.Alloc(context);
            Desc.callback_ctx_free = BindingContext.IteratorContextFreePointer;
            return ref this;
        }

        private ref RoutineBuilder SetCallback(IntPtr callback, IntPtr invoker)
        {
            FreeCallback();
            BindingContext.IteratorContext context = default;
            BindingContext.SetCallback(ref context.Callback, callback);
            Desc.callback = invoker;
            Desc.callback_ctx = Memory.Alloc(context);
            Desc.callback_ctx_free = BindingContext.IteratorContextFreePointer;
            return ref this;
        }

        private ref RoutineBuilder SetRun<T>(T callback, IntPtr invoker) where T : Delegate
        {
            FreeRun();
            BindingContext.RunContext context = default;
            BindingContext.SetCallback(ref context.Callback, callback, false);
            Desc.run = invoker;
            Desc.run_ctx = Memory.Alloc(context);
            Desc.run_ctx_free = BindingContext.RunContextFreePointer;
            return ref this;
        }

        private ref RoutineBuilder SetRun(IntPtr callback, IntPtr invoker)
        {
            FreeRun();
            BindingContext.RunContext context = default;
            BindingContext.SetCallback(ref context.Callback, callback);
            Desc.run = invoker;
            Desc.run_ctx = Memory.Alloc(context);
            Desc.run_ctx_free = BindingContext.RunContextFreePointer;
            return ref this;
        }

        private Routine Build()
        {
            Desc.query = QueryBuilder.Desc;
            Desc.query.binding_ctx = Memory.Alloc(QueryBuilder.Context);
            Desc.query.binding_ctx_free = BindingContext.QueryContextFreePointer;

            fixed (ecs_system_desc_t* ptr = &Desc)
                return new Routine(World, ecs_system_init(World, ptr));
        }

        private void FreeRun()
        {
            if (Desc.run == IntPtr.Zero)
                return;

            ((delegate*<void*, void>)Desc.run_ctx_free)(Desc.run_ctx);
        }

        private void FreeCallback()
        {
            if (Desc.callback == IntPtr.Zero)
                return;

            ((delegate*<void*, void>)Desc.callback_ctx_free)(Desc.callback_ctx);
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
            return HashCode.Combine(Desc.GetHashCode(), QueryBuilder.GetHashCode());
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
}
