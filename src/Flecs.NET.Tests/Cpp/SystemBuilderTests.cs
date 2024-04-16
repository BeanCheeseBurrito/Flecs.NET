using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public class SystemBuilderTests
    {
        [Fact]
        private void BuilderAssignSameType()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;

            Routine s = world.Routine<Position, Velocity>()
                .Each((Entity e, ref Position p, ref Velocity v) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void BuilderBuildToAuto()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;

            Routine s = world.Routine<Position, Velocity>()
                .Each((Entity e, ref Position p, ref Velocity v) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void BuilderBuildNStatements()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;

            RoutineBuilder qb = world.Routine();
            qb.With<Position>();
            qb.With<Velocity>();
            Routine s = qb.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            s.Run();

            Assert.Equal(1, count);
        }

        [Fact]
        private void _1Type()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine<Position>()
                .Each((Entity e, ref Position p) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void Add1Type()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine()
                .With<Position>()
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void Add2Types()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine()
                .With<Position>()
                .With<Velocity>()
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void Add1TypeWith1Type()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine<Position>()
                .With<Velocity>()
                .Each((Entity e, ref Position p) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void Add2TypesWith1Type()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>().Add<Mass>();
            world.Entity().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine<Position>()
                .With<Velocity>()
                .With<Mass>()
                .Each((Entity e, ref Position p) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void AddPair()
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity bob = world.Entity();
            Entity alice = world.Entity();

            Entity e1 = world.Entity().Add(likes, bob);
            world.Entity().Add(likes, alice);

            int count = 0;

            Routine s = world.Routine()
                .With(likes, bob)
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void AddNot()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Position>().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine<Position>()
                .With<Velocity>().Not()
                .Each((Entity e, ref Position p) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void AddOr()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();
            world.Entity().Add<Mass>();

            int count = 0;

            Routine s = world.Routine()
                .With<Position>().Or()
                .With<Velocity>()
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1 || e == e2);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(2, count);
        }

        [Fact]
        private void AddOptional()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>().Add<Mass>();

            int count = 0;

            Routine s = world.Routine()
                .With<Position>()
                .With<Velocity>().Optional()
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1 || e == e2);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(2, count);
        }

        [Fact]
        private void StringTerm()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;

            Routine s = world.Routine()
                .Expr("Position")
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            s.Run();

            Assert.Equal(1, count);
        }

        [Fact]
        private void SingletonTerm()
        {
            using World world = World.Create();

            world.Set(new Singleton(10));

            int count = 0;

            Routine s = world.Routine<Entity>()
                .With<Singleton>().Singleton()
                .Iter((Iter it, Field<Entity> e) =>
                {
                    Field<Singleton> s = it.Field<Singleton>(1);
                    Assert.True(!it.IsSelf(1));
                    Assert.Equal(10, s[0].Value);

                    ref Singleton sRef = ref s[0];
                    Assert.Equal(10, sRef.Value);

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == e[i]);
                        count++;
                    }
                });

            Entity e = world.Entity();
            e.Set(new Entity(e));
            e = world.Entity();
            e.Set(new Entity(e));
            e = world.Entity();
            e.Set(new Entity(e));

            s.Run();

            Assert.Equal(3, count);
        }

        [Fact]
        private void _10Terms()
        {
            using World world = World.Create();

            int count = 0;

            Entity e = world.Entity()
                .Add<TagA>()
                .Add<TagB>()
                .Add<TagC>()
                .Add<TagD>()
                .Add<TagE>()
                .Add<TagF>()
                .Add<TagG>()
                .Add<TagH>()
                .Add<TagI>()
                .Add<TagJ>();

            Routine s = world.Routine()
                .With<TagA>()
                .With<TagB>()
                .With<TagC>()
                .With<TagD>()
                .With<TagE>()
                .With<TagF>()
                .With<TagG>()
                .With<TagH>()
                .With<TagI>()
                .With<TagJ>()
                .Iter((Iter it) =>
                {
                    Assert.Equal(1, it.Count());
                    Assert.True(it.Entity(0) == e);
                    Assert.Equal(10, it.FieldCount());
                    count++;
                });

            s.Run();

            Assert.Equal(1, count);
        }

        [Fact]
        private void _16Terms()
        {
            using World world = World.Create();

            int count = 0;

            Entity e = world.Entity()
                .Add<TagA>()
                .Add<TagB>()
                .Add<TagC>()
                .Add<TagD>()
                .Add<TagE>()
                .Add<TagF>()
                .Add<TagG>()
                .Add<TagH>()
                .Add<TagI>()
                .Add<TagJ>()
                .Add<TagK>()
                .Add<TagL>()
                .Add<TagM>()
                .Add<TagN>()
                .Add<TagO>()
                .Add<TagP>();

            Routine s = world.Routine()
                .With<TagA>()
                .With<TagB>()
                .With<TagC>()
                .With<TagD>()
                .With<TagE>()
                .With<TagF>()
                .With<TagG>()
                .With<TagH>()
                .With<TagI>()
                .With<TagJ>()
                .With<TagK>()
                .With<TagL>()
                .With<TagM>()
                .With<TagN>()
                .With<TagO>()
                .With<TagP>()
                .Iter((Iter it) =>
                {
                    Assert.Equal(1, it.Count());
                    Assert.True(it.Entity(0) == e);
                    Assert.Equal(16, it.FieldCount());
                    count++;
                });

            s.Run();

            Assert.Equal(1, count);
        }

        [Fact]
        private void NameArg()
        {
            using World world = World.Create();

            Routine s = world.Routine<Position>("MySystem")
                .TermAt(0).Src().Name("MySystem")
                .Iter((Iter it, Field<Position> p) => { });

            Assert.True(s.Entity.Has<Position>());
        }

        [Fact]
        private void CreateWithNoTemplateArgs()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();

            int count = 0;

            Routine s = world.Routine()
                .With<Position>()
                .Each((Entity e) =>
                {
                    count++;
                    Assert.True(e == e1);
                });

            Assert.Equal(0, count);
            s.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void WriteAnnotation()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<TagA>();

            int aCount = 0, bCount = 0;

            world.Routine<TagA>()
                .With<TagB>().Write()
                .Each((Entity e) =>
                {
                    aCount++;
                    Assert.True(e == e1);
                    e.Add<TagB>();
                });

            world.Routine<TagB>()
                .Each((Entity e) =>
                {
                    bCount++;
                    Assert.True(e == e1);
                    Assert.True(e.Has<TagB>());
                });

            Assert.Equal(0, aCount);
            Assert.Equal(0, bCount);

            world.Progress();

            Assert.Equal(1, aCount);
            Assert.Equal(1, bCount);

            Assert.True(e1.Has<TagB>());
        }

        [Fact]
        private void NameFromRoot()
        {
            using World world = World.Create();

            Entity sys = world.Routine("::ns.MySystem")
                .Each((Entity e) => { });

            Assert.Equal("MySystem", sys.Name());

            Entity ns = world.Entity("::ns");
            Assert.True(ns == sys.Parent());
        }
    }
}
