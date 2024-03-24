using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Timer struct.
    /// </summary>
    public unsafe struct TimerEntity : IEquatable<TimerEntity>
    {
        private Entity _entity;

        /// <summary>
        ///     A reference to the entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

        /// <summary>
        ///     A reference to the id.
        /// </summary>
        public ref Id Id => ref _entity.Id;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _entity.World;

        /// <summary>
        ///     Creates a timer from the entity id.
        /// </summary>
        /// <param name="id"></param>
        public TimerEntity(ulong id)
        {
            _entity = new Entity(id);
        }

        /// <summary>
        ///     Creates a timer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public TimerEntity(ecs_world_t* world)
        {
            _entity = new Entity(world);
        }

        /// <summary>
        ///     Creates a timer from the provided world and id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public TimerEntity(ecs_world_t* world, ulong id)
        {
            _entity = new Entity(world, id);
        }

        /// <summary>
        ///     Creates a timer from the provided world and entity name.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public TimerEntity(ecs_world_t* world, string name)
        {
            _entity = new Entity(world, name);
        }

        /// <summary>
        ///     Sets the interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public ref TimerEntity Interval(float interval)
        {
            ecs_set_interval(Entity.World, Entity, interval);
            return ref this;
        }

        /// <summary>
        ///     Gets the interval.
        /// </summary>
        /// <returns></returns>
        public float Interval()
        {
            return ecs_get_interval(Entity.World, Entity);
        }

        /// <summary>
        ///     Sets the timeout.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public ref TimerEntity Timeout(float timeout)
        {
            ecs_set_timeout(Entity.World, Entity, timeout);
            return ref this;
        }

        /// <summary>
        ///     Gets the timeout.
        /// </summary>
        /// <returns></returns>
        public float Timeout()
        {
            return ecs_get_timeout(Entity.World, Entity);
        }

        /// <summary>
        ///     Sets the rate.
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="tickSource"></param>
        /// <returns></returns>
        public ref TimerEntity Rate(int rate, ulong tickSource = 0)
        {
            ecs_set_rate(Entity.World, Entity, rate, tickSource);
            return ref this;
        }

        /// <summary>
        ///     Starts the timer.
        /// </summary>
        public void Start()
        {
            ecs_start_timer(Entity.World, Entity);
        }

        /// <summary>
        ///     Stops the timer.
        /// </summary>
        public void Stop()
        {
            ecs_stop_timer(Entity.World, Entity);
        }

        /// <summary>
        ///     Converts a <see cref="TimerEntity"/> instance to its integer id.
        /// </summary>
        /// <param name="timerEntity"></param>
        /// <returns></returns>
        public static implicit operator ulong(TimerEntity timerEntity)
        {
            return ToUInt64(timerEntity);
        }

        /// <summary>
        ///     Converts a <see cref="TimerEntity"/> instance to its id.
        /// </summary>
        /// <param name="timerEntity"></param>
        /// <returns></returns>
        public static implicit operator Id(TimerEntity timerEntity)
        {
            return ToId(timerEntity);
        }

        /// <summary>
        ///     Converts a <see cref="TimerEntity"/> instance to its entity.
        /// </summary>
        /// <param name="timerEntity"></param>
        /// <returns></returns>
        public static implicit operator Entity(TimerEntity timerEntity)
        {
            return ToEntity(timerEntity);
        }

        /// <summary>
        ///     Converts a <see cref="TimerEntity"/> instance to its integer id.
        /// </summary>
        /// <param name="timerEntity"></param>
        /// <returns></returns>
        public static ulong ToUInt64(TimerEntity timerEntity)
        {
            return timerEntity.Entity;
        }

        /// <summary>
        ///     Converts a <see cref="TimerEntity"/> instance to its id.
        /// </summary>
        /// <param name="timerEntity"></param>
        /// <returns></returns>
        public static Id ToId(TimerEntity timerEntity)
        {
            return timerEntity.Id;
        }

        /// <summary>
        ///     Converts a <see cref="TimerEntity"/> instance to its entity.
        /// </summary>
        /// <param name="timerEntity"></param>
        /// <returns></returns>
        public static Entity ToEntity(TimerEntity timerEntity)
        {
            return timerEntity.Entity;
        }

        /// <summary>
        ///     Returns the entity's name if it has one, otherwise return its id.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Entity.ToString();
        }

        /// <summary>
        ///     Checks if two <see cref="TimerEntity"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TimerEntity other)
        {
            return Entity == other.Entity;
        }

        /// <summary>
        ///     Checks if two <see cref="TimerEntity"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is TimerEntity other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="TimerEntity"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Entity.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="TimerEntity"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TimerEntity left, TimerEntity right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="TimerEntity"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TimerEntity left, TimerEntity right)
        {
            return !(left == right);
        }
    }
}
