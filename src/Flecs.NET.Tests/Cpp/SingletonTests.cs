using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class SingletonTests
    {
        [Fact]
        private void SetGetSingleton()
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            Position* p = world.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void EnsureSingleton()
        {
            using World world = World.Create();

            Position* pMut = world.EnsurePtr<Position>();
            pMut->X = 10;
            pMut->Y = 20;

            Position* p = world.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void GetMutSingleton()
        {
            using World world = World.Create();

            Position* p = world.GetMutPtr<Position>();
            Assert.True(p == null);

            world.Set(new Position(10, 20));
            p = world.GetMutPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void ModifiedSingleton()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((ref Position p) => { invoked++; });

            Entity e = world.Entity();
            e.Ensure<Position>();
            Assert.Equal(0, invoked);

            e.Modified<Position>();
            Assert.Equal(1, invoked);
        }

        [Fact]
        private void AddSingleton()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer<Position>()
                .Event(Ecs.OnAdd)
                .Each((ref Position p) => { invoked++; });

            world.Add<Position>();

            Assert.Equal(1, invoked);
        }


        [Fact]
        private void RemoveSingleton()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer<Position>()
                .Event(Ecs.OnRemove)
                .Each((ref Position p) => { invoked++; });

            world.Ensure<Position>();
            Assert.Equal(0, invoked);

            world.Remove<Position>();
            Assert.Equal(1, invoked);
        }

        [Fact]
        private void HasSingleton()
        {
            using World world = World.Create();

            Assert.True(!world.Has<Position>());

            world.Set(new Position(10, 20));

            Assert.True(world.Has<Position>());
        }

        [Fact]
        private void SingletonSystem()
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            world.System()
                .Expr("[inout] Position($)")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Assert.Equal(10, p[0].X);
                        Assert.Equal(20, p[0].Y);

                        p[0].X++;
                        p[0].Y++;
                    }
                });

            world.Progress();

            Position* p = world.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(11, p->X);
            Assert.Equal(21, p->Y);
        }

        [Fact]
        private void GetSingleton()
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            Entity s = world.Singleton<Position>();
            Assert.True(s.Has<Position>());
            Assert.True(s == world.Id<Position>());

            Position* p = s.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void TypeIdFromWorld()
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            ulong id = world.Id<Position>();
            Assert.True(id == world.Id<Position>());

            Entity s = world.Singleton<Position>();
            Assert.True(s == world.Id<Position>());
            Assert.True(s == world.Id<Position>());
        }

        [Fact]
        private void SetLambda()
        {
            using World world = World.Create();

            world.Insert((ref Position p) =>
            {
                p.X = 10;
                p.Y = 20;
            });

            Position* p = world.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            world.Insert((ref Position p) =>
            {
                p.X++;
                p.Y++;
            });

            p = world.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(21, p->Y);
        }

        [Fact]
        private void GetLambda()
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            int count = 0;
            world.Read((ref readonly Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void GetWriteLambda()
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            int count = 0;
            world.Write((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                p.X++;
                p.Y++;
                count++;
            });

            Assert.Equal(1, count);

            Position* p = world.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(21, p->Y);
        }

        [Fact]
        private void GetSetSingletonPairRT()
        {
            using World world = World.Create();

            world.Set<Position, Tag>(new Position(10, 20));

            Position* p = world.GetFirstPtr<Position, Tag>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void GetSetSingletonPairRt()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            world.Set(tgt, new Position(10, 20));

            Position* p = world.GetPtr<Position>(tgt);
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void AddRemoveSingletonPairRT()
        {
            using World world = World.Create();

            world.Add<Position, Tag>();
            Assert.True(world.Has<Position, Tag>());
            world.Remove<Position, Tag>();
            Assert.True(!world.Has<Position, Tag>());
        }

        [Fact]
        private void AddRemoveSingletonPairRt()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            world.Add<Position>(tgt);
            Assert.True(world.Has<Position>(tgt));
            world.Remove<Position>(tgt);
            Assert.True(!world.Has<Position>(tgt));
        }

        [Fact]
        private void AddRemoveSingletonPairrt()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgt = world.Entity();

            world.Add(rel, tgt);
            Assert.True(world.Has(rel, tgt));
            world.Remove(rel, tgt);
            Assert.True(!world.Has(rel, tgt));
        }

        [Fact]
        private void GetTarget()
        {
            using World world = World.Create();

            Entity rel = world.Singleton<Tag>();

            Entity obj1 = world.Entity()
                .Add<Position>();

            Entity obj2 = world.Entity()
                .Add<Velocity>();

            Entity obj3 = world.Entity()
                .Add<Mass>();

            Entity* entities = stackalloc[] { obj1, obj2, obj3 };

            world.Add<Tag>(obj1);
            world.Add<Tag>(obj2);
            world.Add(rel, obj3);

            Entity p = world.Target<Tag>();
            Assert.True(p != 0);
            Assert.True(p == obj1);

            p = world.Target<Tag>(rel);
            Assert.True(p != 0);
            Assert.True(p == obj1);

            p = world.Target(rel);
            Assert.True(p != 0);
            Assert.True(p == obj1);

            for (int i = 0; i < 3; i++)
            {
                p = world.Target<Tag>(i);
                Assert.True(p != 0);
                Assert.True(p == entities[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                p = world.Target<Tag>(rel, i);
                Assert.True(p != 0);
                Assert.True(p == entities[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                p = world.Target(rel, i);
                Assert.True(p != 0);
                Assert.True(p == entities[i]);
            }
        }
    }
}
