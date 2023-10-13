using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_observer_desc_t.
    /// </summary>
    public unsafe struct ObserverBuilder : IDisposable, IEquatable<ObserverBuilder>
    {
        private ecs_world_t* _world;

        internal ecs_observer_desc_t ObserverDesc;
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
        public ObserverBuilder(ecs_world_t* world)
        {
            ObserverDesc = default;
            ObserverContext = default;
            EventCount = default;
            _world = world;
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
        /// <exception cref="InvalidOperationException"></exception>
        public ref ObserverBuilder Event(ulong @event)
        {
            if (EventCount >= 8)
                throw new InvalidOperationException();

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
        ///     Checks if two <see cref="ObserverBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ObserverBuilder other)
        {
            return Equals(Desc, other.Desc);
        }

        /// <summary>
        ///     Checks if two <see cref="ObserverBuilder"/> instance are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool Equals(object? obj)
        {
            return obj is ObserverBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="ObserverBuilder"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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
}
