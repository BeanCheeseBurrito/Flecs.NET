using BenchmarkDotNet.Attributes;
using Flecs.NET.Core;

namespace Flecs.NET.Benchmarks.Core
{
    /// <summary>
    ///     Measures the overhead of implicit type id lookups vs directly using an integer id.
    /// </summary>
    public class ImplicitTypeIdOverhead
    {
        [Params(100000)]
        public int EntityCount;

        public World World;
        public Entity[] Entities;

        public ulong Id1;
        public ulong Id2;
        public ulong Id3;
        public ulong Id4;
        public ulong Id5;
        public ulong Id6;
        public ulong Id7;
        public ulong Id8;
        public ulong Id9;
        public ulong Id10;
        public ulong Id11;
        public ulong Id12;
        public ulong Id13;
        public ulong Id14;
        public ulong Id15;
        public ulong Id16;

        public struct Tag1 { }
        public struct Tag2 { }
        public struct Tag3 { }
        public struct Tag4 { }
        public struct Tag5 { }
        public struct Tag6 { }
        public struct Tag7 { }
        public struct Tag8 { }
        public struct Tag9 { }
        public struct Tag10 { }
        public struct Tag11 { }
        public struct Tag12 { }
        public struct Tag13 { }
        public struct Tag14 { }
        public struct Tag15 { }
        public struct Tag16 { }


        [GlobalSetup]
        public void Setup()
        {
            World = World.Create();
            Entities = new Entity[EntityCount];

            Id1 =  World.Component<Tag1>();
            Id2 =  World.Component<Tag2>();
            Id3 =  World.Component<Tag3>();
            Id4 =  World.Component<Tag4>();
            Id5 =  World.Component<Tag5>();
            Id6 =  World.Component<Tag6>();
            Id7 =  World.Component<Tag7>();
            Id8 =  World.Component<Tag8>();
            Id9 =  World.Component<Tag9>();
            Id10 = World.Component<Tag10>();
            Id11 = World.Component<Tag11>();
            Id12 = World.Component<Tag12>();
            Id13 = World.Component<Tag13>();
            Id14 = World.Component<Tag14>();
            Id15 = World.Component<Tag15>();
            Id16 = World.Component<Tag16>();

            for (int i = 0; i < EntityCount; i++)
                Entities[i] = World.Entity();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            World.Dispose();
        }

        [Benchmark]
        public void Add1Tag()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e].Add<Tag1>();
        }

        [Benchmark]
        public void Add1Id()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e].Add(Id1);
        }

        [Benchmark]
        public void Add2Tags()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e]
                    .Add<Tag1>()
                    .Add<Tag2>();
        }

        [Benchmark]
        public void Add2Ids()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e]
                    .Add(Id1)
                    .Add(Id2);
        }

        [Benchmark]
        public void Add8Tags()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e]
                    .Add<Tag1>()
                    .Add<Tag2>()
                    .Add<Tag3>()
                    .Add<Tag4>()
                    .Add<Tag5>()
                    .Add<Tag6>()
                    .Add<Tag7>()
                    .Add<Tag8>();
        }

        [Benchmark]
        public void Add8Ids()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e]
                    .Add(Id1)
                    .Add(Id2)
                    .Add(Id3)
                    .Add(Id4)
                    .Add(Id5)
                    .Add(Id6)
                    .Add(Id7)
                    .Add(Id8);
        }

        [Benchmark]
        public void Add16Tags()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e]
                    .Add<Tag1>()
                    .Add<Tag2>()
                    .Add<Tag3>()
                    .Add<Tag4>()
                    .Add<Tag5>()
                    .Add<Tag6>()
                    .Add<Tag7>()
                    .Add<Tag8>()
                    .Add<Tag9>()
                    .Add<Tag10>()
                    .Add<Tag11>()
                    .Add<Tag12>()
                    .Add<Tag13>()
                    .Add<Tag14>()
                    .Add<Tag15>()
                    .Add<Tag16>();
        }

        [Benchmark]
        public void Add16Ids()
        {
            for (int e = 0; e < EntityCount; e++)
                Entities[e]
                    .Add(Id1)
                    .Add(Id2)
                    .Add(Id3)
                    .Add(Id4)
                    .Add(Id5)
                    .Add(Id6)
                    .Add(Id7)
                    .Add(Id8)
                    .Add(Id9)
                    .Add(Id10)
                    .Add(Id11)
                    .Add(Id12)
                    .Add(Id13)
                    .Add(Id14)
                    .Add(Id15)
                    .Add(Id16);
        }
    }
}
