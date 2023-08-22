using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Timer
    {
        public Entity Entity { get; }

        public Timer(ulong id)
        {
            Entity = new Entity(id);
        }

        public Timer(ecs_world_t* world)
        {
            Entity = new Entity(world);
        }

        public Timer(ecs_world_t* world, ulong id)
        {
            Entity = new Entity(world, id);
        }

        public Timer(ecs_world_t* world, string name)
        {
            Entity = new Entity(world, name);
        }

        public ref Timer Interval(float interval)
        {
            ecs_set_interval(Entity.World, Entity, interval);
            return ref this;
        }

        public float Interval()
        {
            return ecs_get_interval(Entity.World, Entity);
        }

        public ref Timer Timeout(float timeout)
        {
            ecs_set_timeout(Entity.World, Entity, timeout);
            return ref this;
        }

        public float Timeout()
        {
            return ecs_get_timeout(Entity.World, Entity);
        }

        public ref Timer Rate(int rate, ulong tickSource = 0)
        {
            ecs_set_rate(Entity.World, Entity, rate, tickSource);
            return ref this;
        }

        public void Start()
        {
            ecs_start_timer(Entity.World, Entity);
        }

        public void Stop()
        {
            ecs_stop_timer(Entity.World, Entity);
        }
    }
}
