using System.Diagnostics;
using System.Runtime.CompilerServices;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class FilterTests
    {
        public FilterTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void TermEachComponent()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(1, 2));
            Entity e2 = world.Entity().Set(new Position(3, 4));
            Entity e3 = world.Entity().Set(new Position(5, 6));

            e3.Add<Tag>();

            int count = 0;
            world.Each((Entity e, ref Position p) =>
            {
                if (e == e1)
                {
                    Assert.Equal(1, p.X);
                    Assert.Equal(2, p.Y);
                    count++;
                }

                if (e == e2)
                {
                    Assert.Equal(3, p.X);
                    Assert.Equal(4, p.Y);
                    count++;
                }

                if (e == e3)
                {
                    Assert.Equal(5, p.X);
                    Assert.Equal(6, p.Y);
                    count++;
                }
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermEachTag()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Foo>();
            Entity e2 = world.Entity().Add<Foo>();
            Entity e3 = world.Entity().Add<Foo>();

            e3.Add<Tag>();

            int count = 0;
            world.Each((Entity e, ref Foo _) =>
            {
                if (e == e1 || e == e2 || e == e3)
                    count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermEachId()
        {
            using World world = World.Create();

            Entity foo = world.Entity();

            Entity e1 = world.Entity().Add(foo);
            Entity e2 = world.Entity().Add(foo);
            Entity e3 = world.Entity().Add(foo);

            e3.Add<Tag>();

            int count = 0;
            world.Each(foo, (Entity e) =>
            {
                if (e == e1 || e == e2 || e == e3)
                    count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermEachPairType()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Rel, Obj>();
            Entity e2 = world.Entity().Add<Rel, Obj>();
            Entity e3 = world.Entity().Add<Rel, Obj>();

            e3.Add<Tag>();

            int count = 0;
            world.Each<Rel, Obj>((Entity e) =>
            {
                if (e == e1 || e == e2 || e == e3)
                    count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermEachPairId()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity obj = world.Entity();

            Entity e1 = world.Entity().Add(rel, obj);
            Entity e2 = world.Entity().Add(rel, obj);
            Entity e3 = world.Entity().Add(rel, obj);

            e3.Add<Tag>();

            int count = 0;
            world.Each(world.Pair(rel, obj), (Entity e) =>
            {
                if (e == e1 || e == e2 || e == e3)
                    count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermEachPairRelationWildcard()
        {
            using World world = World.Create();

            Entity rel1 = world.Entity();
            Entity rel2 = world.Entity();
            Entity obj = world.Entity();

            Entity e1 = world.Entity().Add(rel1, obj);
            Entity e2 = world.Entity().Add(rel1, obj);
            Entity e3 = world.Entity().Add(rel2, obj);

            e3.Add<Tag>();

            int count = 0;
            world.Each(world.Pair(Ecs.Wildcard, obj), (Entity e) =>
            {
                if (e == e1 || e == e2 || e == e3)
                    count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermEachPairObjectWildcard()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity obj1 = world.Entity();
            Entity obj2 = world.Entity();

            Entity e1 = world.Entity().Add(rel, obj1);
            Entity e2 = world.Entity().Add(rel, obj1);
            Entity e3 = world.Entity().Add(rel, obj2);

            e3.Add<Tag>();

            int count = 0;
            world.Each(world.Pair(rel, Ecs.Wildcard), (Entity e) =>
            {
                if (e == e1 || e == e2 || e == e3)
                    count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void TermGetId()
        {
            using World world = World.Create();

            Entity foo = world.Entity();
            Entity bar = world.Entity();

            Query q = world.QueryBuilder()
                .Term<Position>()
                .Term<Velocity>()
                .Term(foo, bar)
                .Build();

            Assert.Equal(3, q.FieldCount());

            Term t = q.Term(0);
            Assert.True(t.Id() == world.Id<Position>());
            t = q.Term(1);
            Assert.True(t.Id() == world.Id<Velocity>());
            t = q.Term(2);
            Assert.True(t.Id() == world.Pair(foo, bar));
        }

        [Fact]
        private void TermGetSubj()
        {
            using World world = World.Create();

            Entity foo = world.Entity();
            Entity bar = world.Entity();
            Entity src = world.Entity();

            Query q = world.QueryBuilder()
                .Term<Position>()
                .Term<Velocity>().Src(src)
                .Term(foo, bar)
                .Build();

            Assert.Equal(3, q.FieldCount());

            Term
                t = q.Term(0);
            Assert.True(t.GetSrc() == Ecs.This);
            t = q.Term(1);
            Assert.True(t.GetSrc() == src);
            t = q.Term(2);
            Assert.True(t.GetSrc() == Ecs.This);
        }

        [Fact]
        private void TermGetPred()
        {
            using World world = World.Create();

            Entity foo = world.Entity();
            Entity bar = world.Entity();
            Entity src = world.Entity();

            Query q = world.QueryBuilder()
                .Term<Position>()
                .Term<Velocity>().Src(src)
                .Term(foo, bar)
                .Build();

            Assert.Equal(3, q.FieldCount());

            Term
                t = q.Term(0);
            Assert.True(t.GetFirst() == world.Id<Position>());
            t = q.Term(1);
            Assert.True(t.GetFirst() == world.Id<Velocity>());
            t = q.Term(2);
            Assert.True(t.GetFirst() == foo);
        }

        [Fact]
        private void TermGetObj()
        {
            using World world = World.Create();

            Entity foo = world.Entity();
            Entity bar = world.Entity();
            Entity src = world.Entity();

            Query q = world.QueryBuilder()
                .Term<Position>()
                .Term<Velocity>().Src(src)
                .Term(foo, bar)
                .Build();

            Assert.Equal(3, q.FieldCount());

            Term
                t = q.Term(0);
            Assert.True(t.GetSecond() == 0);
            t = q.Term(1);
            Assert.True(t.GetSecond() == 0);
            t = q.Term(2);
            Assert.True(t.GetSecond() == bar);
        }

        [Fact]
        private void GetFirst()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Filter q = world.Filter<A>();

            Entity first = q.First();
            Assert.True(first != 0);
            Assert.True(first == e1);
        }

        [Fact]
        private void GetCountDirect()
        {
            using World world = World.Create();

            world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Filter q = world.Filter<A>();

            Assert.Equal(3, q.Count());
        }

        [Fact]
        private void GetIsTrueDirect()
        {
            using World world = World.Create();

            world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Filter q1 = world.Filter<A>();
            Filter q2 = world.Filter<B>();

            Assert.True(q1.IsTrue());
            Assert.False(q2.IsTrue());
        }

        [Fact]
        private void GetFirstDirect()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Filter q = world.Filter<A>();

            Entity first = q.First();
            Assert.True(first != 0);
            Assert.True(first == e1);
        }

        [Fact]
        private void EachWithNoThis()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Filter f = world.FilterBuilder<Position, Velocity>()
                .Arg(1).Src(e)
                .Arg(2).Src(e)
                .Build();

            int count = 0;

            f.Each((ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void EachWithIterNoThis()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Filter f = world.FilterBuilder<Position, Velocity>()
                .Arg(1).Src(e)
                .Arg(2).Src(e)
                .Build();

            int count = 0;

            f.Each((Iter it, int index, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
                Assert.Equal(0, index);
                Assert.Equal(0, it.Count());
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void NamedFilter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Filter q = world.Filter<Position>("my_query");

            int count = 0;
            q.Each((Entity e) =>
            {
                Assert.True(e == e1 || e == e2);
                count++;
            });
            Assert.Equal(2, count);

            Entity qe = q.Entity();
            Assert.True(qe != 0);
            Assert.Equal("my_query", qe.Name());
        }

        [Fact]
        private void NamedScopedFilter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Filter q = world.Filter<Position>("my.query");

            int count = 0;
            q.Each((Entity e) =>
            {
                Assert.True(e == e1 || e == e2);
                count++;
            });
            Assert.Equal(2, count);

            Entity qe = q.Entity();
            Assert.True(qe != 0);
            Assert.Equal("query", qe.Name());
            Assert.Equal("::my.query", qe.Path());
        }

        [Fact]
        private void SetThisVar()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(1, 2));
            Entity e2 = world.Entity().Set(new Position(3, 4));
            world.Entity().Set(new Position(5, 6));

            Filter q = world.Filter<Position>("my.Query");

            int count = 0;
            q.Iter().SetVar(0, e2).Each((Entity e) =>
            {
                Assert.True(e == e2);
                count++;
            });
            Assert.Equal(1, count);
        }

        [Fact]
        private void InspectTermsWithExpr()
        {
            using World world = World.Create();

            Filter f = world.FilterBuilder()
                .Expr("(ChildOf,0)")
                .Build();

            int count = 0;
            f.EachTerm((ref Term term) =>
            {
                Assert.True(term.Id().IsPair());
                count++;
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void Find()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(10, 20));
            Entity e2 = world.Entity().Set(new Position(20, 30));

            Filter q = world.Filter<Position>();

            Entity r = q.Find((ref Position p) => { return p.X == 20; });

            Assert.True(r == e2);
        }

        [Fact]
        private void FindNotFound()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(10, 20));
            world.Entity().Set(new Position(20, 30));

            Filter q = world.Filter<Position>();

            Entity r = q.Find((ref Position p) => { return p.X == 30; });

            Assert.True(r == 0);
        }

        [Fact]
        private void FindWithEntity()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(10, 20)).Set(new Velocity(20, 30));
            Entity e2 = world.Entity().Set(new Position(20, 30)).Set(new Velocity(20, 30));

            Filter q = world.Filter<Position>();

            Entity r = q.Find((Entity e, ref Position p) =>
            {
                return p.X == e.GetPtr<Velocity>()->X &&
                       p.Y == e.GetPtr<Velocity>()->Y;
            });

            Assert.True(r == e2);
        }

        [Fact]
        private void OptionalPairTerm()
        {
            using World world = World.Create();

            world.Entity()
                .Add<TagA>()
                .Set<Position, Tag>(new Position(1.0f, 2.0f));
            world.Entity()
                .Add<TagA>();

            int withPair = 0, withoutPair = 0;

            Filter f = world.FilterBuilder()
                .Term<Position, Tag>().Optional()
                .With<TagA>()
                .Build();

            f.Each((Entity e, ref Position p) =>
            {
                if (!Unsafe.IsNullRef(ref p))
                {
                    withPair++;
                    Assert.Equal(1.0f, p.X);
                    Assert.Equal(2.0f, p.Y);
                }
                else
                {
                    withoutPair++;
                }
            });

            world.Progress(1.0f);

            Assert.Equal(1, withPair);
            Assert.Equal(1, withoutPair);
        }
    }
}
