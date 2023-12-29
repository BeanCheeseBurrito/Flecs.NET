using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Wrapper around system.
    /// </summary>
    public unsafe struct Routine : IEquatable<Routine>
    {
        private Entity _entity;

        /// <summary>
        ///     A reference to the entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

        /// <summary>
        ///     A reference to the entity.
        /// </summary>
        public ref Id Id => ref _entity.Id;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _entity.World;

        /// <summary>
        ///     Creates a routine from world and id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        public Routine(ecs_world_t* world, ulong entity)
        {
            _entity = new Entity(world, entity);
        }

        /// <summary>
        ///     Creates a routine from an entity.
        /// </summary>
        /// <param name="entity"></param>
        public Routine(Entity entity)
        {
            _entity = entity;
        }

        /// <summary>
        ///     Destructs the routine.
        /// </summary>
        public void Destruct()
        {
            Entity.Destruct();
        }

        /// <summary>
        ///     Sets the context for the routine.
        /// </summary>
        /// <param name="ctx"></param>
        public void Ctx(void* ctx)
        {
            ecs_system_desc_t desc = default;
            desc.entity = Entity;
            desc.ctx = ctx;
            ecs_system_init(World, &desc);
        }

        /// <summary>
        ///     Returns the context for the routine.
        /// </summary>
        /// <returns></returns>
        public void* Ctx()
        {
            return ecs_system_get_ctx(World, Entity);
        }

        /// <summary>
        ///     Returns the query for the routine.
        /// </summary>
        /// <returns></returns>
        public Query Query()
        {
            return new Query(World, ecs_system_get_query(World, Entity));
        }

        /// <summary>
        ///     Run the routine.
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public void Run(
            float deltaTime = 0,
            int offset = 0,
            int limit = 0)
        {
            RunWithParam(deltaTime, null, offset, limit);
        }

        /// <summary>
        ///     Run the routine with a param.
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="param"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public void RunWithParam(
            float deltaTime = 0,
            void* param = null,
            int offset = 0,
            int limit = 0)
        {
            ecs_run_w_filter(World, Id, deltaTime, offset, limit, param);
        }

        /// <summary>
        ///     Run the routine.
        /// </summary>
        /// <param name="stageCurrent"></param>
        /// <param name="stageCount"></param>
        /// <param name="deltaTime"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public void RunWorker(
            int stageCurrent,
            int stageCount,
            float deltaTime = 0,
            int offset = 0,
            int limit = 0)
        {
            RunWorkerWithParam(stageCurrent, stageCount, deltaTime, null, offset, limit);
        }

        /// <summary>
        ///     Run the routine with a param.
        /// </summary>
        /// <param name="stageCurrent"></param>
        /// <param name="stageCount"></param>
        /// <param name="deltaTime"></param>
        /// <param name="param"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public void RunWorkerWithParam(
            int stageCurrent,
            int stageCount,
            float deltaTime = 0,
            void* param = null,
            int offset = 0,
            int limit = 0)
        {
            if (stageCount != 0)
                ecs_run_worker(World, Id, stageCurrent, stageCount, deltaTime, param);
            else
                ecs_run_w_filter(World, Id, deltaTime, offset, limit, param);
        }

        /// <summary>
        ///     Sets the interval for the routine.
        /// </summary>
        /// <param name="interval"></param>
        public void Interval(float interval)
        {
            ecs_set_interval(World, Entity, interval);
        }

        /// <summary>
        ///     Returns the interval for the routine.
        /// </summary>
        /// <returns></returns>
        public float Interval()
        {
            return ecs_get_interval(World, Entity);
        }

        /// <summary>
        ///     Sets the timeout for the routine.
        /// </summary>
        /// <param name="timeout"></param>
        public void Timeout(float timeout)
        {
            ecs_set_timeout(World, Entity, timeout);
        }

        /// <summary>
        ///     Gets the timeout for the routine.
        /// </summary>
        /// <returns></returns>
        public float Timeout()
        {
            return ecs_get_timeout(World, Entity);
        }

        /// <summary>
        ///     Sets the rate for the routine.
        /// </summary>
        /// <param name="rate"></param>
        public void Rate(int rate)
        {
            ecs_set_rate(World, Entity, rate, 0);
        }

        /// <summary>
        ///     Starts the timer.
        /// </summary>
        public void Start()
        {
            ecs_start_timer(World, Entity);
        }

        /// <summary>
        ///     Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            ecs_stop_timer(World, Entity);
        }

        /// <summary>
        ///     Sets the external tick source.
        /// </summary>
        /// <param name="entity"></param>
        public void SetTickSource(ulong entity)
        {
            ecs_set_tick_source(World, Entity, entity);
        }

        /// <summary>
        ///     Sets the external tick source.
        /// </summary>
        /// <param name="timer"></param>
        public void SetTickSource(Timer timer)
        {
            ecs_set_tick_source(World, Entity, timer.Entity);
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
        ///     Checks if two <see cref="Routine"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Routine other)
        {
            return Entity == other.Entity;
        }

        /// <summary>
        ///     Checks if two <see cref="Routine"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Routine routine && Equals(routine);
        }

        /// <summary>
        ///     Return the hash code of the <see cref="Routine"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Entity.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Routine"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Routine left, Routine right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Routine"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Routine left, Routine right)
        {
            return !(left == right);
        }
    }
}
