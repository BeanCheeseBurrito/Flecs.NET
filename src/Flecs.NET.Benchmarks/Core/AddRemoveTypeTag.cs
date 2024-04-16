using BenchmarkDotNet.Attributes;
using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Benchmarks.Core
{
    public unsafe class AddRemoveTypeTag
    {
        [Params(100000)] public int EntityCount;

        public World World;
        public Entity[] Entities;

        public ulong TagId;

        public struct Tag { }

        [GlobalSetup]
        public void Setup()
        {
            World = World.Create();
            Entities = new Entity[EntityCount];

            World.Component<Tag>();
            TagId = World.Component<Tag>();

            for (int i = 0; i < EntityCount; i++)
                Entities[i] = World.Entity();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            World.Dispose();
        }

        [Benchmark]
        public void TypeTag()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e].Add<Tag>();

            for (int e = 0; e < EntityCount; e++)
                Entities[e].Remove<Tag>();
        }

        [Benchmark]
        public void RawTag()
        {
            for (int e = 0; e < EntityCount; e++)
                ecs_add_id(World, Entities[e], TagId);

            for (int e = 0; e < EntityCount; e++)
                ecs_remove_id(World, Entities[e], TagId);
        }
    }
}
