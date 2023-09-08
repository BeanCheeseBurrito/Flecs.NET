using BenchmarkDotNet.Attributes;
using Flecs.NET.Core;

namespace Flecs.NET.Benchmarks.Core
{
    public class AddRemoveTag
    {
        [Params(100000)]
        public int EntityCount;

        [Params(1, 2, 16, 32)]
        public int TagCount;

        public World World;
        public Entity[] Entities;
        public Entity[] Tags;

        [GlobalSetup]
        public void Setup()
        {
            World = World.Create();
            Entities = new Entity[EntityCount];
            Tags = new Entity[TagCount];

            for (int i = 0; i < EntityCount; i++)
                Entities[i] = World.Entity();

            for (int i = 0; i < TagCount; i++)
                Tags[i] = World.Entity();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            World.Dispose();
        }

        [Benchmark]
        public void Benchmark()
        {
            for (int e = 0; e < EntityCount; e++)
            {
                for (int tag = 0; tag < TagCount; tag++)
                    Entities[e].Add(Tags[tag]);

                for (int tag = 0; tag < TagCount; tag++)
                    Entities[e].Remove(Tags[tag]);
            }
        }
    }
}
