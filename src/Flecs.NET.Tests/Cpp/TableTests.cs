using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public unsafe class TableTests
    {
        public TableTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void Each()
        {
            using World world = World.Create();

            world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();

            world.Filter<Position>()
                .Each((Entity e, ref Position p) => { e2.Add<Mass>(); });

            Assert.True(e2.Has<Mass>());
        }

        [Fact]
        private void EachWithoutEntity()
        {
            using World world = World.Create();

            world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();

            world.Filter<Position>()
                .Each((ref Position p) => { e2.Add<Mass>(); });

            Assert.True(e2.Has<Mass>());
        }

        [Fact]
        private void Iter()
        {
            using World world = World.Create();

            world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();

            world.Filter<Position>()
                .Iter((Iter it, Field<Position> p) => { e2.Add<Mass>(); });

            Assert.True(e2.Has<Mass>());
        }

        [Fact]
        private void IterWithoutComponents()
        {
            using World world = World.Create();

            world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();

            world.Filter<Position>()
                .Iter((Iter it) => { e2.Add<Mass>(); });

            Assert.True(e2.Has<Mass>());
        }

        [Fact]
        private void MultiGet()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            Entity e2 = world.Entity().Add<Position>();

            Assert.True(e1.Read((in Position p, in Velocity v) => { e2.Add<Mass>(); }));

            Assert.True(e2.Has<Mass>());
        }

        [Fact]
        private void MultiSet()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            Entity e2 = world.Entity().Add<Position>();

            e1.Ensure((ref Position p, ref Velocity v) => { e2.Add<Mass>(); });

            Assert.True(e2.Has<Mass>());
        }

        [Fact]
        private void Count()
        {
            using World world = World.Create();

            Entity e = world.Entity().Set(new Position(10, 20));
            world.Entity().Set(new Position(20, 30));
            world.Entity().Set(new Position(30, 40));

            Table table = e.Table();
            Assert.Equal(3, table.Count);
        }

        [Fact]
        private void HasId()
        {
            using World world = World.Create();

            Entity t1 = world.Entity();
            Entity t2 = world.Entity();
            Entity t3 = world.Entity();

            Entity e = world.Entity()
                .Add(t1)
                .Add(t2);
            world.Entity()
                .Add(t1)
                .Add(t2);
            world.Entity()
                .Add(t1)
                .Add(t2);

            Table table = e.Table();
            Assert.True(table.Has(t1));
            Assert.True(table.Has(t2));
            Assert.True(!table.Has(t3));
        }

        [Fact]
        private void HasT()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));
            world.Entity()
                .Set(new Position(20, 30))
                .Set(new Velocity(2, 3));
            world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4));

            Table table = e.Table();

            Assert.True(table.Has<Position>());
            Assert.True(table.Has<Velocity>());
            Assert.True(!table.Has<Mass>());
        }

        [Fact]
        private void HasPairrt()
        {
            using World world = World.Create();

            Entity r = world.Entity();
            Entity t1 = world.Entity();
            Entity t2 = world.Entity();
            Entity t3 = world.Entity();

            Entity e = world.Entity()
                .Add(r, t1)
                .Add(r, t2);
            world.Entity()
                .Add(r, t1)
                .Add(r, t2);
            world.Entity()
                .Add(r, t1)
                .Add(r, t2);

            Table table = e.Table();
            Assert.True(table.Has(r, t1));
            Assert.True(table.Has(r, t2));
            Assert.True(!table.Has(r, t3));
        }

        [Fact]
        private void HasPairRt()
        {
            using World world = World.Create();

            Entity t1 = world.Entity();
            Entity t2 = world.Entity();
            Entity t3 = world.Entity();

            Entity e = world.Entity()
                .Add<R>(t1)
                .Add<R>(t2);
            world.Entity()
                .Add<R>(t1)
                .Add<R>(t2);
            world.Entity()
                .Add<R>(t1)
                .Add<R>(t2);

            Table table = e.Table();
            Assert.True(table.Has<R>(t1));
            Assert.True(table.Has<R>(t2));
            Assert.True(!table.Has<R>(t3));
        }

        [Fact]
        private void HasPairRT()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<R, T1>()
                .Add<R, T2>();
            world.Entity()
                .Add<R, T1>()
                .Add<R, T2>();
            world.Entity()
                .Add<R, T1>()
                .Add<R, T2>();

            Table table = e.Table();
            Assert.True(table.Has<R, T1>());
            Assert.True(table.Has<R, T2>());
            Assert.True(!table.Has<R, T3>());
        }

        [Fact]
        private void GetId()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));
            world.Entity()
                .Set(new Position(20, 30))
                .Set(new Velocity(2, 3));
            world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4));

            Table table = e.Table();
            void* ptr = table.GetPtr(world.Id<Position>());
            Position* p = (Position*)ptr;
            Assert.True(p != null);
            Assert.Equal(10, p![0].X);
            Assert.Equal(20, p[0].Y);
            Assert.Equal(20, p[1].X);
            Assert.Equal(30, p[1].Y);
            Assert.Equal(30, p[2].X);
            Assert.Equal(40, p[2].Y);

            ptr = table.GetPtr(world.Id<Velocity>());
            Velocity* v = (Velocity*)ptr;
            Assert.True(v != null);
            Assert.True(p != null);
            Assert.Equal(1, v![0].X);
            Assert.Equal(2, v[0].Y);
            Assert.Equal(2, v[1].X);
            Assert.Equal(3, v[1].Y);
            Assert.Equal(3, v[2].X);
            Assert.Equal(4, v[2].Y);
        }

        [Fact]
        private void GetT()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));
            world.Entity()
                .Set(new Position(20, 30))
                .Set(new Velocity(2, 3));
            world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4));

            Table table = e.Table();
            Position* p = table.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p![0].X);
            Assert.Equal(20, p[0].Y);
            Assert.Equal(20, p[1].X);
            Assert.Equal(30, p[1].Y);
            Assert.Equal(30, p[2].X);
            Assert.Equal(40, p[2].Y);

            Velocity* v = table.GetPtr<Velocity>();
            Assert.True(v != null);
            Assert.Equal(1, v![0].X);
            Assert.Equal(2, v[0].Y);
            Assert.Equal(2, v[1].X);
            Assert.Equal(3, v[1].Y);
            Assert.Equal(3, v[2].X);
            Assert.Equal(4, v[2].Y);
        }

        [Fact]
        private void GetPairrt()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set<Position, Tgt>(new Position(10, 20))
                .Set<Velocity, Tgt>(new Velocity(1, 2));
            world.Entity()
                .Set<Position, Tgt>(new Position(20, 30))
                .Set<Velocity, Tgt>(new Velocity(2, 3));
            world.Entity()
                .Set<Position, Tgt>(new Position(30, 40))
                .Set<Velocity, Tgt>(new Velocity(3, 4));

            Table table = e.Table();
            void* ptr = table.GetPtr(world.Id<Position>(), world.Id<Tgt>());
            Position* p = (Position*)ptr;
            Assert.True(p != null);
            Assert.Equal(10, p![0].X);
            Assert.Equal(20, p[0].Y);
            Assert.Equal(20, p[1].X);
            Assert.Equal(30, p[1].Y);
            Assert.Equal(30, p[2].X);
            Assert.Equal(40, p[2].Y);

            ptr = table.GetPtr(world.Id<Velocity>(), world.Id<Tgt>());
            Velocity* v = (Velocity*)ptr;
            Assert.True(v != null);
            Assert.Equal(1, v![0].X);
            Assert.Equal(2, v[0].Y);
            Assert.Equal(2, v[1].X);
            Assert.Equal(3, v[1].Y);
            Assert.Equal(3, v[2].X);
            Assert.Equal(4, v[2].Y);
        }

        [Fact]
        private void GetPairRt()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set<Position, Tgt>(new Position(10, 20))
                .Set<Velocity, Tgt>(new Velocity(1, 2));
            world.Entity()
                .Set<Position, Tgt>(new Position(20, 30))
                .Set<Velocity, Tgt>(new Velocity(2, 3));
            world.Entity()
                .Set<Position, Tgt>(new Position(30, 40))
                .Set<Velocity, Tgt>(new Velocity(3, 4));

            Table table = e.Table();
            void* ptr = table.GetPtr<Position>(world.Id<Tgt>());
            Position* p = (Position*)ptr;
            Assert.True(p != null);
            Assert.Equal(10, p![0].X);
            Assert.Equal(20, p[0].Y);
            Assert.Equal(20, p[1].X);
            Assert.Equal(30, p[1].Y);
            Assert.Equal(30, p[2].X);
            Assert.Equal(40, p[2].Y);

            ptr = table.GetPtr<Velocity>(world.Id<Tgt>());
            Velocity* v = (Velocity*)ptr;
            Assert.True(v != null);
            Assert.Equal(1, v![0].X);
            Assert.Equal(2, v[0].Y);
            Assert.Equal(2, v[1].X);
            Assert.Equal(3, v[1].Y);
            Assert.Equal(3, v[2].X);
            Assert.Equal(4, v[2].Y);
        }

        [Fact]
        private void GetPairRT()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set<Position, Tgt>(new Position(10, 20))
                .Set<Velocity, Tgt>(new Velocity(1, 2));
            world.Entity()
                .Set<Position, Tgt>(new Position(20, 30))
                .Set<Velocity, Tgt>(new Velocity(2, 3));
            world.Entity()
                .Set<Position, Tgt>(new Position(30, 40))
                .Set<Velocity, Tgt>(new Velocity(3, 4));

            Table table = e.Table();
            void* ptr = table.GetFirstPtr<Position, Tgt>();
            Position* p = (Position*)ptr;
            Assert.True(p != null);
            Assert.Equal(10, p![0].X);
            Assert.Equal(20, p[0].Y);
            Assert.Equal(20, p[1].X);
            Assert.Equal(30, p[1].Y);
            Assert.Equal(30, p[2].X);
            Assert.Equal(40, p[2].Y);

            ptr = table.GetFirstPtr<Velocity, Tgt>();
            Velocity* v = (Velocity*)ptr;
            Assert.True(v != null);
            Assert.Equal(1, v![0].X);
            Assert.Equal(2, v[0].Y);
            Assert.Equal(2, v[1].X);
            Assert.Equal(3, v[1].Y);
            Assert.Equal(3, v[2].X);
            Assert.Equal(4, v[2].Y);
        }

        [Fact]
        private void RangeGetId()
        {
            using World world = World.Create();

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));
            Entity e = world.Entity()
                .Set(new Position(20, 30))
                .Set(new Velocity(2, 3));
            world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4));

            Table table = e.Range();
            void* ptr = table.GetPtr(world.Id<Position>());
            Position* p = (Position*)ptr;
            Assert.True(p != null);
            Assert.Equal(20, p![0].X);
            Assert.Equal(30, p[0].Y);

            ptr = table.GetPtr(world.Id<Velocity>());
            Velocity* v = (Velocity*)ptr;
            Assert.True(v != null);
            Assert.Equal(2, v![0].X);
            Assert.Equal(3, v[0].Y);
        }

        [Fact]
        private void RangeGetT()
        {
            using World world = World.Create();

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));
            Entity e = world.Entity()
                .Set(new Position(20, 30))
                .Set(new Velocity(2, 3));
            world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4));

            Table table = e.Range();
            Position* p = table.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(20, p![0].X);
            Assert.Equal(30, p[0].Y);

            Velocity* v = table.GetPtr<Velocity>();
            Assert.True(v != null);
            Assert.Equal(2, v![0].X);
            Assert.Equal(3, v[0].Y);
        }

        [Fact]
        private void RangeGetPairrt()
        {
            using World world = World.Create();

            world.Entity()
                .Set<Position, Tgt>(new Position(10, 20))
                .Set<Velocity, Tgt>(new Velocity(1, 2));
            Entity e = world.Entity()
                .Set<Position, Tgt>(new Position(20, 30))
                .Set<Velocity, Tgt>(new Velocity(2, 3));
            world.Entity()
                .Set<Position, Tgt>(new Position(30, 40))
                .Set<Velocity, Tgt>(new Velocity(3, 4));

            Table table = e.Range();
            void* ptr = table.GetPtr(world.Id<Position>(), world.Id<Tgt>());
            Position* p = (Position*)ptr;
            Assert.True(p != null);
            Assert.Equal(20, p![0].X);
            Assert.Equal(30, p[0].Y);

            ptr = table.GetPtr(world.Id<Velocity>(), world.Id<Tgt>());
            Velocity* v = (Velocity*)ptr;
            Assert.True(v != null);
            Assert.Equal(2, v![0].X);
            Assert.Equal(3, v[0].Y);
        }

        [Fact]
        private void RangeGetPairRt()
        {
            using World world = World.Create();

            world.Entity()
                .Set<Position, Tgt>(new Position(10, 20))
                .Set<Velocity, Tgt>(new Velocity(1, 2));
            Entity e = world.Entity()
                .Set<Position, Tgt>(new Position(20, 30))
                .Set<Velocity, Tgt>(new Velocity(2, 3));
            world.Entity()
                .Set<Position, Tgt>(new Position(30, 40))
                .Set<Velocity, Tgt>(new Velocity(3, 4));

            Table table = e.Range();
            Position* p = table.GetPtr<Position>(world.Id<Tgt>());
            Assert.True(p != null);
            Assert.Equal(20, p![0].X);
            Assert.Equal(30, p[0].Y);

            Velocity* v = table.GetPtr<Velocity>(world.Id<Tgt>());
            Assert.True(v != null);
            Assert.Equal(2, v![0].X);
            Assert.Equal(3, v[0].Y);
        }

        [Fact]
        private void RangeGetPairRT()
        {
            using World world = World.Create();

            world.Entity()
                .Set<Position, Tgt>(new Position(10, 20))
                .Set<Velocity, Tgt>(new Velocity(1, 2));
            Entity e = world.Entity()
                .Set<Position, Tgt>(new Position(20, 30))
                .Set<Velocity, Tgt>(new Velocity(2, 3));
            world.Entity()
                .Set<Position, Tgt>(new Position(30, 40))
                .Set<Velocity, Tgt>(new Velocity(3, 4));

            Table table = e.Range();
            Position* p = table.GetFirstPtr<Position, Tgt>();
            Assert.True(p != null);
            Assert.Equal(20, p![0].X);
            Assert.Equal(30, p[0].Y);

            Velocity* v = table.GetFirstPtr<Velocity, Tgt>();
            Assert.True(v != null);
            Assert.Equal(2, v![0].X);
            Assert.Equal(3, v[0].Y);
        }

        [Fact]
        private void GetDepth()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity().ChildOf(e1);
            Entity e3 = world.Entity().ChildOf(e2);
            Entity e4 = world.Entity().ChildOf(e3);

            Assert.Equal(1, e2.Table().Depth(Ecs.ChildOf));
            Assert.Equal(2, e3.Table().Depth(Ecs.ChildOf));
            Assert.Equal(3, e4.Table().Depth(Ecs.ChildOf));
        }

        [Fact]
        private void GetDepthWithType()
        {
            using World world = World.Create();

            world.Component<Rel>().Entity.Add(Ecs.Traversable);

            Entity e1 = world.Entity();
            Entity e2 = world.Entity().Add<Rel>(e1);
            Entity e3 = world.Entity().Add<Rel>(e2);
            Entity e4 = world.Entity().Add<Rel>(e3);

            Assert.Equal(1, e2.Table().Depth<Rel>());
            Assert.Equal(2, e3.Table().Depth<Rel>());
            Assert.Equal(3, e4.Table().Depth<Rel>());
        }

        [Fact]
        private void IterType()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position>()
                .Add<Velocity>();

            Table table = e.Table();

            int count = 0;
            foreach (Id id in table.Type())
            {
                count++;
                Assert.True(id == world.Id<Position>() || id == world.Id<Velocity>());
            }

            Assert.Equal(2, count);
        }

        [Fact]
        private void GetTEnum()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(Number.One);
            world.Entity()
                .Set(Number.Two);
            world.Entity()
                .Set(Number.Three);

            Table table = e.Table();

            Number* n = table.GetPtr<Number>();
            Assert.True(n != null);
            Assert.Equal(Number.One, n![0]);
            Assert.Equal(Number.Two, n[1]);
            Assert.Equal(Number.Three, n[2]);
        }
    }
}
