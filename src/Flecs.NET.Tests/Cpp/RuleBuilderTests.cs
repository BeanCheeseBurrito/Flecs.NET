using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    public class RuleBuilderTests
    {
        public RuleBuilderTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void _1Type()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20));

            world.Entity().Set(new Velocity(10, 20));

            Rule r = world.Rule<Position>();

            int count = 0;
            r.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void _2Types()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity().Set(new Velocity(10, 20));

            Rule r = world.Rule<Position, Velocity>();

            int count = 0;
            r.Each((Entity e, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.True(e == e1);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void IdTerm()
        {
            using World world = World.Create();

            Entity tag = world.Entity();

            Entity e1 = world.Entity()
                .Add(tag);

            world.Entity().Set(new Velocity(10, 20));

            Rule r = world.RuleBuilder()
                .Term(tag)
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void TypeTerm()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20));

            world.Entity().Set(new Velocity(10, 20));

            Rule r = world.RuleBuilder()
                .Term<Position>()
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void IdPairTerm()
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Entity e1 = world.Entity()
                .Add(likes, apples);

            world.Entity()
                .Add(likes, pears);

            Rule r = world.RuleBuilder()
                .Term(likes, apples)
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void IdPairWildcardTerm()
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Entity e1 = world.Entity()
                .Add(likes, apples);

            Entity e2 = world.Entity()
                .Add(likes, pears);

            Rule r = world.RuleBuilder()
                .Term(likes, Ecs.Wildcard)
                .Build();

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(1) == world.Pair(likes, apples));
                    count++;
                }

                if (it.Entity(index) == e2)
                {
                    Assert.True(it.Id(1) == world.Pair(likes, pears));
                    count++;
                }
            });
            Assert.Equal(2, count);

            r.Destruct();
        }

        [Fact]
        private void TypePairTerm()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Add<Likes, Apples>();

            Entity e2 = world.Entity()
                .Add<Likes, Pears>();

            Rule r = world.RuleBuilder()
                .Term<Likes>(Ecs.Wildcard)
                .Build();

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(1) == world.Pair<Likes, Apples>());
                    count++;
                }

                if (it.Entity(index) == e2)
                {
                    Assert.True(it.Id(1) == world.Pair<Likes, Pears>());
                    count++;
                }
            });
            Assert.Equal(2, count);

            r.Destruct();
        }

        [Fact]
        private void PairTermWithVar()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Add<Likes, Apples>();

            Entity e2 = world.Entity()
                .Add<Likes, Pears>();

            Rule r = world.RuleBuilder()
                .Term<Likes>().Second().Var("Food")
                .Build();

            int foodVar = r.FindVar("Food");

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(1) == world.Pair<Likes, Apples>());
                    Assert.True(it.GetVar("Food") == world.Id<Apples>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Apples>());
                    count++;
                }

                if (it.Entity(index) == e2)
                {
                    Assert.True(it.Id(1) == world.Pair<Likes, Pears>());
                    Assert.True(it.GetVar("Food") == world.Id<Pears>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Pears>());
                    count++;
                }
            });
            Assert.Equal(2, count);

            r.Destruct();
        }

        [Fact]
        private void _2PairTermsWithVar()
        {
            using World world = World.Create();

            Entity bob = world.Entity()
                .Add<Eats, Apples>();

            Entity alice = world.Entity()
                .Add<Eats, Pears>()
                .Add<Likes>(bob);

            bob.Add<Likes>(alice);

            Rule r = world.RuleBuilder()
                .Term<Eats>().Second().Var("Food")
                .Term<Likes>().Second().Var("Person")
                .Build();

            int foodVar = r.FindVar("Food");
            int personVar = r.FindVar("Person");

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == bob)
                {
                    Assert.True(it.Id(1) == world.Pair<Eats, Apples>());
                    Assert.True(it.GetVar("Food") == world.Id<Apples>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Apples>());

                    Assert.True(it.Id(2) == world.Pair<Likes>(alice));
                    Assert.True(it.GetVar("Person") == alice);
                    Assert.True(it.GetVar(personVar) == alice);
                    count++;
                }

                if (it.Entity(index) == alice)
                {
                    Assert.True(it.Id(1) == world.Pair<Eats, Pears>());
                    Assert.True(it.GetVar("Food") == world.Id<Pears>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Pears>());

                    Assert.True(it.Id(2) == world.Pair<Likes>(bob));
                    Assert.True(it.GetVar("Person") == bob);
                    Assert.True(it.GetVar(personVar) == bob);
                    count++;
                }
            });
            Assert.Equal(2, count);

            r.Destruct();
        }

        [Fact]
        private void SetVar()
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            world.Entity()
                .Add<Likes>(apples);

            Entity e2 = world.Entity()
                .Add<Likes>(pears);

            Rule r = world.RuleBuilder()
                .Term<Likes>().Second().Var("Food")
                .Build();

            int foodVar = r.FindVar("Food");

            int count = 0;
            r.Iter()
                .SetVar(foodVar, pears)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    Assert.True(it.Id(1) == world.Pair<Likes>(pears));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    count++;
                });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void Set2Vars()
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Entity bob = world.Entity()
                .Add<Eats>(apples);

            Entity alice = world.Entity()
                .Add<Eats>(pears)
                .Add<Likes>(bob);

            bob.Add<Likes>(alice);

            Rule r = world.RuleBuilder()
                .Term<Eats>().Second().Var("Food")
                .Term<Likes>().Second().Var("Person")
                .Build();

            int foodVar = r.FindVar("Food");
            int personVar = r.FindVar("Person");

            int count = 0;
            r.Iter()
                .SetVar(foodVar, pears)
                .SetVar(personVar, bob)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == alice);
                    Assert.True(it.Id(1) == world.Pair<Eats>(pears));
                    Assert.True(it.Id(2) == world.Pair<Likes>(bob));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    Assert.True(it.GetVar("Person") == bob);
                    Assert.True(it.GetVar(personVar) == bob);
                    count++;
                });
            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void SetVarByName()
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            world.Entity()
                .Add<Likes>(apples);

            Entity e2 = world.Entity()
                .Add<Likes>(pears);

            Rule r = world.RuleBuilder()
                .Term<Likes>().Second().Var("Food")
                .Build();

            int count = 0;
            r.Iter()
                .SetVar("Food", pears)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    Assert.True(it.Id(1) == world.Pair<Likes>(pears));
                    count++;
                });
            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void Set2VarsByName()
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Entity bob = world.Entity()
                .Add<Eats>(apples);

            Entity alice = world.Entity()
                .Add<Eats>(pears)
                .Add<Likes>(bob);

            bob.Add<Likes>(alice);

            Rule r = world.RuleBuilder()
                .Term<Eats>().Second().Var("Food")
                .Term<Likes>().Second().Var("Person")
                .Build();

            int foodVar = r.FindVar("Food");
            int personVar = r.FindVar("Person");

            int count = 0;
            r.Iter()
                .SetVar("Food", pears)
                .SetVar("Person", bob)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == alice);
                    Assert.True(it.Id(1) == world.Pair<Eats>(pears));
                    Assert.True(it.Id(2) == world.Pair<Likes>(bob));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    Assert.True(it.GetVar("Person") == bob);
                    Assert.True(it.GetVar(personVar) == bob);
                    count++;
                });
            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void ExprWithVar()
        {
            using World world = World.Create();

            Entity rel = world.Entity("Rel");
            Entity obj = world.Entity();
            Entity e = world.Entity().Add(rel, obj);

            Rule r = world.RuleBuilder()
                .Expr("(Rel, $X)")
                .Build();

            int xVar = r.FindVar("X");
            Assert.True(xVar != -1);

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                Assert.True(it.Entity(index) == e);
                Assert.True(it.Pair(1).Second() == obj);
                count++;
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void GetFirst()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Rule q = world.Rule<A>();

            Entity first = q.Iter().First();
            Assert.True(first != 0);
            Assert.True(first == e1);

            q.Destruct();
        }

        [Fact]
        private void GetCountDirect()
        {
            using World world = World.Create();

            world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Rule q = world.Rule<A>();

            Assert.Equal(3, q.Count());

            q.Destruct();
        }

        [Fact]
        private void GetIsTrueDirect()
        {
            using World world = World.Create();

            world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Rule q1 = world.Rule<A>();
            Rule q2 = world.Rule<B>();

            Assert.True(q1.IsTrue());
            Assert.False(q2.IsTrue());

            q1.Destruct();
            q2.Destruct();
        }

        [Fact]
        private void GetFirstDirect()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<A>();
            world.Entity().Add<A>();
            world.Entity().Add<A>();

            Rule q = world.Rule<A>();

            Entity first = q.First();
            Assert.True(first != 0);
            Assert.True(first == e1);

            q.Destruct();
        }

        [Fact]
        private void VarSrcWithPrefixedName()
        {
            using World world = World.Create();

            Rule r = world.RuleBuilder()
                .Term<Foo>().Src("$Var")
                .Build();

            Entity e = world.Entity().Add<Foo>();

            int count = 0;
            r.Iter((Iter it) =>
            {
                Assert.True(it.GetVar("Var") == e);
                count++;
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void VarFirstWithPrefixedName()
        {
            using World world = World.Create();

            Rule r = world.RuleBuilder()
                .Term<Foo>()
                .Term().First("$Var")
                .Build();

            Entity e = world.Entity().Add<Foo>();

            int count = 0;
            r.Iter((Iter it) =>
            {
                Assert.Equal(1, it.Count());
                Assert.Equal(it.Entity(0), e);
                Assert.True(it.GetVar("Var") == world.Id<Foo>());
                count++;
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void VarSecondWithPrefixedName()
        {
            using World world = World.Create();

            Rule r = world.RuleBuilder()
                .Term<Foo>().Second("$Var")
                .Build();

            Entity t = world.Entity();
            Entity e = world.Entity().Add<Foo>(t);

            int count = 0;
            r.Iter((Iter it) =>
            {
                Assert.Equal(1, it.Count());
                Assert.Equal(it.Entity(0), e);
                Assert.True(it.GetVar("Var") == t);
                count++;
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void TermWithSecondVarString()
        {
            using World world = World.Create();

            Entity foo = world.Entity();

            Rule r = world.RuleBuilder()
                .Term(foo, "$Var")
                .Build();

            Entity t = world.Entity();
            Entity e = world.Entity().Add(foo, t);

            int count = 0;
            r.Iter((Iter it) =>
            {
                Assert.Equal(1, it.Count());
                Assert.Equal(it.Entity(0), e);
                Assert.True(it.GetVar("Var") == t);
                count++;
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void TermTypeWithSecondVarString()
        {
            using World world = World.Create();

            Rule r = world.RuleBuilder()
                .Term<Foo>("$Var")
                .Build();

            Entity t = world.Entity();
            Entity e = world.Entity().Add<Foo>(t);

            int count = 0;
            r.Iter((Iter it) =>
            {
                Assert.Equal(1, it.Count());
                Assert.Equal(it.Entity(0), e);
                Assert.True(it.GetVar("Var") == t);
                count++;
            });

            Assert.Equal(1, count);

            r.Destruct();
        }

        [Fact]
        private void NamedRule()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Rule q = world.Rule<Position>("my_query");

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

            q.Destruct();
        }

        [Fact]
        private void NamedScopedRule()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Rule q = world.Rule<Position>("my.query");

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

            q.Destruct();
        }

        [Fact]
        private void IsValid()
        {
            using World world = World.Create();

            Rule q1 = world.Rule<Position>();
            Assert.True(q1.IsValid());

            Ecs.Log.SetLevel(-4);
            Rule q2 = world.RuleBuilder().Expr("foo").Build();
            Assert.True(!q2.IsValid());

            q1.Destruct();
        }

        [Fact]
        private void UnresolvedByName()
        {
            using World world = World.Create();

            Rule q = world.RuleBuilder()
                .FilterFlags(EcsFilterUnresolvedByName)
                .Expr("$this == Foo")
                .Build();

            Assert.True(q.IsValid());

            Assert.False(q.Iter().IsTrue());

            world.Entity("Foo");

            Assert.True(q.Iter().IsTrue());

            q.Destruct();
        }

        [Fact]
        private void Scope()
        {
            using World world = World.Create();

            Entity root = world.Entity();
            Entity tagA = world.Entity();
            Entity tagB = world.Entity();

            world.Entity()
                .Add(root)
                .Add(tagA)
                .Add(tagB);

            Entity e2 = world.Entity()
                .Add(root)
                .Add(tagA);

            world.Entity()
                .Add(root)
                .Add(tagB);

            world.Entity()
                .Add(root);

            Rule r = world.RuleBuilder()
                .With(root)
                .ScopeOpen().Not()
                .With(tagA)
                .Without(tagB)
                .ScopeClose()
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                Assert.True(e != e2);
                count++;
            });

            Assert.Equal(3, count);

            r.Destruct();
        }

        // TODO: Fully implement iterable apis
        // [Fact]
        // private void IterWithStage()
        // {
        //     using World world = World.Create();
        //
        //     world.SetStageCount(2);
        //     World stage = world.GetStage(1);
        //
        //     Entity e1 = world.Entity().Add<Position>();
        //
        //     Rule q = world.Rule<Position>();
        //
        //     int count = 0;
        //     q.Each(stage, (Iter it, int i) =>
        //     {
        //         Assert.True(it.World() == stage);
        //         Assert.True(it.Entity(i) == e1);
        //         count++;
        //     });
        //
        //     Assert.Equal(1, count);
        //
        //     q.Destruct();
        // }

        [Fact]
        private void InspectTermsWithExpr()
        {
            using World world = World.Create();

            Rule f = world.RuleBuilder()
                .Expr("(ChildOf,0)")
                .Build();

            int count = 0;
            f.EachTerm((ref Term term) =>
            {
                Assert.True(term.Id().IsPair());
                count++;
            });

            Assert.Equal(1, count);

            f.Destruct();
        }

        [Fact]
        private void Find()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(10, 20));
            Entity e2 = world.Entity().Set(new Position(20, 30));

            Rule q = world.Rule<Position>();

            Entity r = q.Find((ref Position p) => { return p.X == 20; });

            Assert.True(r == e2);

            q.Destruct();
        }

        [Fact]
        private void FindNotFound()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(10, 20));
            world.Entity().Set(new Position(20, 30));

            Rule q = world.Rule<Position>();

            Entity r = q.Find((ref Position p) => { return p.X == 30; });

            Assert.True(r == 0);

            q.Destruct();
        }

        [Fact]
        private void FindWithEntity()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(10, 20)).Set(new Velocity(20, 30));
            Entity e2 = world.Entity().Set(new Position(20, 30)).Set(new Velocity(20, 30));

            Rule q = world.Rule<Position>();

            Entity r = q.Find((Entity e, ref Position p) =>
            {
                return p.X == e.Get<Velocity>().X &&
                       p.Y == e.Get<Velocity>().Y;
            });

            Assert.True(r == e2);

            q.Destruct();
        }
    }
}
