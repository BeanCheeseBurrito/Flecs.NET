using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Timer struct.
    /// </summary>
    public unsafe struct Timer
    {
        private Entity _entity;

        /// <summary>
        ///     A reference to the entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

        /// <summary>
        ///     Creates a timer from the entity id.
        /// </summary>
        /// <param name="id"></param>
        public Timer(ulong id)
        {
            _entity = new Entity(id);
        }

        /// <summary>
        ///     Creates a timer for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public Timer(ecs_world_t* world)
        {
            _entity = new Entity(world);
        }

        /// <summary>
        ///     Creates a timer from the provided world and id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="id"></param>
        public Timer(ecs_world_t* world, ulong id)
        {
            _entity = new Entity(world, id);
        }

        /// <summary>
        ///     Creates a timer from the provided world and entity name.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public Timer(ecs_world_t* world, string name)
        {
            _entity = new Entity(world, name);
        }

        /// <summary>
        ///     Sets the interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public ref Timer Interval(float interval)
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
        public ref Timer Timeout(float timeout)
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
        public ref Timer Rate(int rate, ulong tickSource = 0)
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
        ///      Returns the entity's name if it has one, otherwise return its id.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Entity.ToString();
        }
    }
}
