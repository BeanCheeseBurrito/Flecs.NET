using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Flecs.NET.Utilities;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    [SuppressMessage("ReSharper", "AccessToModifiedClosure")]
    [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
    [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
    public unsafe class QueryBuilderTests
    {
        public static IEnumerable<object[]> CacheKinds => new List<object[]>
        {
            new object[] { EcsQueryCacheDefault },
            new object[] { EcsQueryCacheAuto }
        };

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderAssignSameType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder<Position, Velocity>()
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderAssignToEmpty(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position, Velocity>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderAssignFromEmpty(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .CacheKind(cacheKind)
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderBuild(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder<Position, Velocity>()
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderBuildToAuto(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position, Velocity>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderBuildNStatements(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            QueryBuilder qb = world.QueryBuilder();
            qb.With<Position>();
            qb.With<Velocity>();
            qb.CacheKind(cacheKind);
            Query q = qb.Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _1Type(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _2Types(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity().Set(new Velocity(10, 20));

            Query r = world.QueryBuilder<Position, Velocity>()
                .CacheKind(cacheKind)
                .Build();

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
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IdTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity tag = world.Entity();

            Entity e1 = world.Entity()
                .Add(tag);

            world.Entity().Set(new Velocity(10, 20));

            Query r = world.QueryBuilder()
                .With(tag)
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TypeTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20));

            world.Entity().Set(new Velocity(10, 20));

            Query r = world.QueryBuilder()
                .With<Position>()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IdPairTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Entity e1 = world.Entity()
                .Add(likes, apples);

            world.Entity()
                .Add(likes, pears);

            Query r = world.QueryBuilder()
                .With(likes, apples)
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IdPairWildcardTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Entity e1 = world.Entity()
                .Add(likes, apples);

            Entity e2 = world.Entity()
                .Add(likes, pears);

            Query r = world.QueryBuilder()
                .With(likes, Ecs.Wildcard)
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(0) == world.Pair(likes, apples));
                    count++;
                }

                if (it.Entity(index) == e2)
                {
                    Assert.True(it.Id(0) == world.Pair(likes, pears));
                    count++;
                }
            });
            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TypePairTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Add<Likes, Apples>();

            Entity e2 = world.Entity()
                .Add<Likes, Pears>();

            Query r = world.QueryBuilder()
                .With<Likes>(Ecs.Wildcard)
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(0) == world.Pair<Likes, Apples>());
                    count++;
                }

                if (it.Entity(index) == e2)
                {
                    Assert.True(it.Id(0) == world.Pair<Likes, Pears>());
                    count++;
                }
            });
            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void PairTermWithVar(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Add<Likes, Apples>();

            Entity e2 = world.Entity()
                .Add<Likes, Pears>();

            Query r = world.QueryBuilder()
                .With<Likes>().Second().Var("Food")
                .CacheKind(cacheKind)
                .Build();

            int foodVar = r.FindVar("Food");

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(0) == world.Pair<Likes, Apples>());
                    Assert.True(it.GetVar("Food") == world.Id<Apples>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Apples>());
                    count++;
                }

                if (it.Entity(index) == e2)
                {
                    Assert.True(it.Id(0) == world.Pair<Likes, Pears>());
                    Assert.True(it.GetVar("Food") == world.Id<Pears>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Pears>());
                    count++;
                }
            });
            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _2PairTermsWithVar(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity bob = world.Entity()
                .Add<Eats, Apples>();

            Entity alice = world.Entity()
                .Add<Eats, Pears>()
                .Add<Likes>(bob);

            bob.Add<Likes>(alice);

            Query r = world.QueryBuilder()
                .With<Eats>().Second().Var("Food")
                .With<Likes>().Second().Var("Person")
                .CacheKind(cacheKind)
                .Build();

            int foodVar = r.FindVar("Food");
            int personVar = r.FindVar("Person");

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == bob)
                {
                    Assert.True(it.Id(0) == world.Pair<Eats, Apples>());
                    Assert.True(it.GetVar("Food") == world.Id<Apples>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Apples>());

                    Assert.True(it.Id(1) == world.Pair<Likes>(alice));
                    Assert.True(it.GetVar("Person") == alice);
                    Assert.True(it.GetVar(personVar) == alice);
                    count++;
                }

                if (it.Entity(index) == alice)
                {
                    Assert.True(it.Id(0) == world.Pair<Eats, Pears>());
                    Assert.True(it.GetVar("Food") == world.Id<Pears>());
                    Assert.True(it.GetVar(foodVar) == world.Id<Pears>());

                    Assert.True(it.Id(1) == world.Pair<Likes>(bob));
                    Assert.True(it.GetVar("Person") == bob);
                    Assert.True(it.GetVar(personVar) == bob);
                    count++;
                }
            });
            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetVar(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            world.Entity()
                .Add<Likes>(apples);

            Entity e2 = world.Entity()
                .Add<Likes>(pears);

            Query r = world.QueryBuilder()
                .With<Likes>().Second().Var("Food")
                .CacheKind(cacheKind)
                .Build();

            int foodVar = r.FindVar("Food");

            int count = 0;
            r.Iter()
                .SetVar(foodVar, pears)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    Assert.True(it.Id(0) == world.Pair<Likes>(pears));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    count++;
                });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Set2Vars(ecs_query_cache_kind_t cacheKind)
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

            Query r = world.QueryBuilder()
                .With<Eats>().Second().Var("Food")
                .With<Likes>().Second().Var("Person")
                .CacheKind(cacheKind)
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
                    Assert.True(it.Id(0) == world.Pair<Eats>(pears));
                    Assert.True(it.Id(1) == world.Pair<Likes>(bob));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    Assert.True(it.GetVar("Person") == bob);
                    Assert.True(it.GetVar(personVar) == bob);
                    count++;
                });
            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetVarByName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            world.Entity()
                .Add<Likes>(apples);

            Entity e2 = world.Entity()
                .Add<Likes>(pears);

            Query r = world.QueryBuilder()
                .With<Likes>().Second().Var("Food")
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Iter()
                .SetVar("Food", pears)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    Assert.True(it.Id(0) == world.Pair<Likes>(pears));
                    count++;
                });
            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Set2VarsByName(ecs_query_cache_kind_t cacheKind)
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

            Query r = world.QueryBuilder()
                .With<Eats>().Second().Var("Food")
                .With<Likes>().Second().Var("Person")
                .CacheKind(cacheKind)
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
                    Assert.True(it.Id(0) == world.Pair<Eats>(pears));
                    Assert.True(it.Id(1) == world.Pair<Likes>(bob));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    Assert.True(it.GetVar("Person") == bob);
                    Assert.True(it.GetVar(personVar) == bob);
                    count++;
                });
            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetVarOnQuery(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            world.Entity()
                .Add<Likes>(apples);

            Entity e2 = world.Entity()
                .Add<Likes>(pears);

            Query r = world.QueryBuilder()
                .With<Likes>().Second().Var("Food")
                .CacheKind(cacheKind)
                .Build();

            int foodVar = r.FindVar("Food");

            int count = 0;
            r.SetVar(foodVar, pears)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    Assert.True(it.Id(0) == world.Pair<Likes>(pears));
                    Assert.True(it.GetVar("Food") == pears);
                    Assert.True(it.GetVar(foodVar) == pears);
                    count++;
                });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetVarByNameOnQuery(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            world.Entity()
                .Add<Likes>(apples);

            Entity e2 = world.Entity()
                .Add<Likes>(pears);

            Query r = world.QueryBuilder()
                .With<Likes>().Second().Var("Food")
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.SetVar("Food", pears)
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    Assert.True(it.Id(0) == world.Pair<Likes>(pears));
                    Assert.True(it.GetVar("Food") == pears);
                    count++;
                });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetTableVar(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();
            Entity e3 = world.Entity().Add<Position>().Add<Velocity>();

            Query r = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.SetVar("this", e1.Table())
                .Each((Iter it, int index) =>
                {
                    if (index == 0)
                        Assert.True(it.Entity(index) == e1);
                    else if (index == 1) Assert.True(it.Entity(index) == e2);
                    count++;
                });

            Assert.Equal(2, count);

            r.SetVar("this", e3.Table())
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e3);
                    count++;
                });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetRangeVar(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();
            Entity e3 = world.Entity().Add<Position>().Add<Velocity>();

            Query r = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;

            r.SetVar("this", e1.Range())
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e1);
                    count++;
                });

            Assert.Equal(1, count);

            r.SetVar("this", e2.Range())
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e2);
                    count++;
                });

            Assert.Equal(2, count);

            r.SetVar("this", e3.Range())
                .Each((Iter it, int index) =>
                {
                    Assert.True(it.Entity(index) == e3);
                    count++;
                });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetTableVarChained(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            /* var e1 = */
            world.Entity().Add<Position>();
            /* var e2 = */
            world.Entity().Add<Position>();
            Entity e3 = world.Entity().Add<Position>().Add<Velocity>();
            /* var e4 = */
            world.Entity().Add<Velocity>();

            Query q1 = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            Query q2 = world.QueryBuilder<Velocity>()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;

            q1.Run((Iter it) =>
            {
                while (it.Next())
                {
                    q2.SetVar("this", it.Table()).Each((Entity e) =>
                    {
                        Assert.True(e == e3);
                        count++;
                    });
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetRangeVarChained(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            /* var e1 = */
            world.Entity().Add<Position>();
            /* var e2 = */
            world.Entity().Add<Position>();
            Entity e3 = world.Entity().Add<Position>().Add<Velocity>();
            /* var e4 = */
            world.Entity().Add<Velocity>();

            Query q1 = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            Query q2 = world.QueryBuilder<Velocity>()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;

            q1.Run((Iter it) =>
            {
                while (it.Next())
                {
                    q2.SetVar("this", it.Range()).Each((Entity e) =>
                    {
                        Assert.True(e == e3);
                        count++;
                    });
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExprWithVar(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity rel = world.Entity("Rel");
            Entity obj = world.Entity();
            Entity e = world.Entity().Add(rel, obj);

            Query r = world.QueryBuilder()
                .Expr("(Rel, $X)")
                .CacheKind(cacheKind)
                .Build();

            int xVar = r.FindVar("X");
            Assert.True(xVar != -1);

            int count = 0;
            r.Each((Iter it, int index) =>
            {
                Assert.True(it.Entity(index) == e);
                Assert.True(it.Pair(0).Second() == obj);
                count++;
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Add1Type(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Add2Types(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Add1TypeWith1Type(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position>()
                .With<Velocity>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Add2TypesWith1Type(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position>()
                .With<Velocity>()
                .With<Mass>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>().Add<Mass>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AddPair(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity bob = world.Entity();
            Entity alice = world.Entity();

            Query q = world.QueryBuilder()
                .With(likes, bob)
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add(likes, bob);
            world.Entity().Add(likes, alice);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AddNot(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position>()
                .With<Velocity>().Oper(Ecs.Not)
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Position>().Add<Velocity>();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AddOr(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>().Oper(Ecs.Or)
                .With<Velocity>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();
            world.Entity().Add<Mass>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AddOptional(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>().Oper(Ecs.Optional)
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>().Add<Mass>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void StringTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Position>();

            Query q = world.QueryBuilder()
                .Expr("Position")
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SingletonTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Set(new Other(10));

            Query q = world.QueryBuilder<Self>()
                .With<Other>().Singleton().InOut()
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity();
            e.Set(new Self(e));
            e = world.Entity();
            e.Set(new Self(e));
            e = world.Entity();
            e.Set(new Self(e));

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> s = it.Field<Self>(0);
                    Field<Other> o = it.Field<Other>(1);
                    Assert.True(!it.IsSelf(1));
                    Assert.Equal(10, o[0].Value);

                    ref Other oRef = ref o[0];
                    Assert.Equal(10, oRef.Value);

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == s[i].Value);
                        count++;
                    }
                }
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsASupersetTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Other>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Query q = world.QueryBuilder<Self>()
                .With<Other>().Src().Up(Ecs.IsA).In()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> s = it.Field<Self>(0);
                    Field<Other> o = it.Field<Other>(1);
                    Assert.True(!it.IsSelf(1));
                    Assert.Equal(10, o[0].Value);

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == s[i].Value);
                        count++;
                    }
                }
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsASelfSupersetTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Other>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Query q = world.QueryBuilder<Self>()
                .With<Other>().Src().Self().Up(Ecs.IsA).In()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(20));
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(20));
            e.Set(new Self(e));

            int count = 0;
            int ownedCount = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> s = it.Field<Self>(0);
                    Field<Other> o = it.Field<Other>(1);

                    if (!it.IsSelf(1))
                        Assert.Equal(10, o[0].Value);
                    else
                        foreach (int i in it)
                        {
                            Assert.Equal(20, o[i].Value);
                            ownedCount++;
                        }

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == s[i].Value);
                        count++;
                    }
                }
            });

            Assert.Equal(5, count);
            Assert.Equal(2, ownedCount);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ChildOfSupersetTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Self>()
                .With<Other>().Src().Up().In()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> s = it.Field<Self>(0);
                    Field<Other> o = it.Field<Other>(1);
                    Assert.True(!it.IsSelf(1));
                    Assert.Equal(10, o[0].Value);

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == s[i].Value);
                        count++;
                    }
                }
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ChildOfSelfSupersetTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Self>()
                .With<Other>().Src().Self().Up().In()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(20));
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(20));
            e.Set(new Self(e));

            int count = 0;
            int ownedCount = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> s = it.Field<Self>(0);
                    Field<Other> o = it.Field<Other>(1);

                    if (!it.IsSelf(1))
                        Assert.Equal(10, o[0].Value);
                    else
                        foreach (int i in it)
                        {
                            Assert.Equal(20, o[i].Value);
                            ownedCount++;
                        }

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == s[i].Value);
                        count++;
                    }
                }
            });

            Assert.Equal(5, count);
            Assert.Equal(2, ownedCount);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsASupersetTermWithEach(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Other>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Src().Up(Ecs.IsA)
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsASelfSupersetTermWithEach(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Other>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Src().Self().Up(Ecs.IsA)
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Add(Ecs.IsA, @base);
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(5, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ChildOfSupersetTermWithEach(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Src().Up()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ChildOfSelfSupersetTermWithEach(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Src().Self().Up()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(5, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsASupersetShortcut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Other>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Up(Ecs.IsA)
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().IsA(@base);
            e.Set(new Self(e));
            e = world.Entity().IsA(@base);
            e.Set(new Self(e));
            e = world.Entity().IsA(@base);
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsASupersetShortcutWithSelf(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Other>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Self().Up(Ecs.IsA)
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().IsA(@base);
            e.Set(new Self(e));
            e = world.Entity().IsA(@base);
            e.Set(new Self(e));
            e = world.Entity().IsA(@base);
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(5, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ChildOfSupersetShortcut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Up()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ChildOfSupersetShortcutWithSelf(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Self, Other>()
                .TermAt(1).Self().Up()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().ChildOf(@base);
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));
            e = world.Entity().Set(new Other(10));
            e.Set(new Self(e));

            int count = 0;

            q.Each((Entity e, ref Self s, ref Other o) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, o.Value);
                count++;
            });

            Assert.Equal(5, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Relation(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity bob = world.Entity();
            Entity alice = world.Entity();

            Query q = world.QueryBuilder<Self>()
                .With(likes, bob)
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity().Add(likes, bob);
            e.Set(new Self(e));
            e = world.Entity().Add(likes, bob);
            e.Set(new Self(e));

            e = world.Entity().Add(likes, alice);
            e.Set(new Self(default));
            e = world.Entity().Add(likes, alice);
            e.Set(new Self(default));

            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void RelationWithObjectWildcard(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity bob = world.Entity();
            Entity alice = world.Entity();

            Query q = world.QueryBuilder<Self>()
                .With(likes, Ecs.Wildcard)
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity().Add(likes, bob);
            e.Set(new Self(e));
            e = world.Entity().Add(likes, bob);
            e.Set(new Self(e));

            e = world.Entity().Add(likes, alice);
            e.Set(new Self(e));
            e = world.Entity().Add(likes, alice);
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(default));
            e = world.Entity();
            e.Set(new Self(default));

            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(4, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void RelationWithPredicateWildcard(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity bob = world.Entity();
            Entity alice = world.Entity();
            Entity likes = world.Entity();
            Entity dislikes = world.Entity();

            Query q = world.QueryBuilder<Self>()
                .With(Ecs.Wildcard, alice)
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity().Add(likes, alice);
            e.Set(new Self(e));
            e = world.Entity().Add(dislikes, alice);
            e.Set(new Self(e));

            e = world.Entity().Add(likes, bob);
            e.Set(new Self(default));
            e = world.Entity().Add(dislikes, bob);
            e.Set(new Self(default));

            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AddPairWithRelType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity dislikes = world.Entity();
            Entity bob = world.Entity();
            Entity alice = world.Entity();

            Query q = world.QueryBuilder<Self>()
                .With<Likes>(Ecs.Wildcard)
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity().Add<Likes>(alice);
            e.Set(new Self(e));
            e = world.Entity().Add(dislikes, alice);
            e.Set(new Self(default));

            e = world.Entity().Add<Likes>(bob);
            e.Set(new Self(e));
            e = world.Entity().Add(dislikes, bob);
            e.Set(new Self(default));

            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TemplateTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position>()
                .With<Template<int>>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Template<int>>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitSubjectWithId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder<Position>()
                .With<Position>().Id(Ecs.This)
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitSubjectWithType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Set(new Position(10, 20));

            Query q = world.QueryBuilder<Position>()
                .With<Position>().Src<Position>()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
                Assert.True(e == world.Singleton<Position>());
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitObjectWithId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity alice = world.Entity();
            Entity bob = world.Entity();

            Query q = world.QueryBuilder()
                .With(likes).Second(alice)
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add(likes, alice);
            world.Entity().Add(likes, bob);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitObjectWithType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity bob = world.Entity();

            Query q = world.QueryBuilder()
                .With(likes).Second<Alice>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add(likes, world.Id<Alice>());
            world.Entity().Add(likes, bob);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitTerm(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With(world.Term(world.Id<Position>()))
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitTermWithType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With(world.Term<Position>())
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitTermWithPairType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With(world.Term<Likes, Alice>())
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Likes, Alice>();
            world.Entity().Add<Likes, Bob>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitTermWithId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Query q = world.QueryBuilder()
                .With(world.Term(apples))
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add(apples);
            world.Entity().Add(pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ExplicitTermWithPairId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Query q = world.QueryBuilder()
                .With(world.Term(likes, apples))
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add(likes, apples);
            world.Entity().Add(likes, pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _1TermToEmpty(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();

            QueryBuilder qb = world.QueryBuilder()
                .With<Position>()
                .CacheKind(cacheKind);

            qb.With(likes, apples);

            Query q = qb.Build();

            Assert.Equal(2, q.FieldCount());
            Assert.Equal(world.Id<Position>(), q.Term(0).Id());
            Assert.Equal(world.Pair(likes, apples), q.Term(1).Id());
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _2SubsequentArgs(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            int count = 0;

            Routine s = world.Routine<RelData, Velocity>()
                .TermAt(0).Second(Ecs.Wildcard)
                .TermAt(1).Singleton()
                .Run((Iter it) =>
                {
                    while (it.Next())
                        count += it.Count();
                });

            world.Entity().Add<RelData, Tag>();
            world.Set(new Velocity());

            s.Run();

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void OptionalTagIsSet(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<TagA>()
                .With<TagB>().Oper(Ecs.Optional)
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<TagA>().Add<TagB>();
            Entity e2 = world.Entity().Add<TagA>();

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());

                    count += it.Count();

                    if (it.Entity(0) == e1)
                    {
                        Assert.True(it.IsSet(0));
                        Assert.True(it.IsSet(1));
                    }
                    else
                    {
                        Assert.True(it.Entity(0) == e2);
                        Assert.True(it.IsSet(0));
                        Assert.False(it.IsSet(1));
                    }
                }
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _10Terms(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
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
                .CacheKind(cacheKind)
                .Build();

            Assert.Equal(10, f.FieldCount());

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

            int count = 0;
            f.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.True(it.Entity(0) == e);
                    Assert.Equal(10, it.FieldCount());
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _16Terms(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
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
                .CacheKind(cacheKind)
                .Build();

            Assert.Equal(16, f.FieldCount());

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
                .Add<TagP>()
                .Add<TagQ>()
                .Add<TagR>()
                .Add<TagS>()
                .Add<TagT>();

            int count = 0;
            f.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.True(it.Entity(0) == e);
                    Assert.Equal(16, it.FieldCount());
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        private static ulong GroupByFirstId(ecs_world_t* world, ecs_table_t* table, ulong id, void* ctx)
        {
            ecs_type_t* type = ecs_table_get_type(table);
            return type->array[0];
        }

        private static ulong GroupByFirstIdNegated(ecs_world_t* world, ecs_table_t* table, ulong id, void* ctx)
        {
            return ~GroupByFirstId(world, table, id, ctx);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByRaw(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<TagA>();
            world.Component<TagB>();
            world.Component<TagC>();
            world.Component<TagX>();

            Query q = world.QueryBuilder()
                .With<TagX>()
                .GroupBy(world.Id<TagX>(), GroupByFirstId)
                .Build();

            Query qReverse = world.QueryBuilder()
                .With<TagX>()
                .GroupBy(world.Id<TagX>(), GroupByFirstIdNegated)
                .Build();

            Entity e3 = world.Entity().Add<TagX>().Add<TagC>();
            Entity e2 = world.Entity().Add<TagX>().Add<TagB>();
            Entity e1 = world.Entity().Add<TagX>().Add<TagA>();

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    if (count == 0)
                        Assert.True(it.Entity(0) == e1);
                    else if (count == 1)
                        Assert.True(it.Entity(0) == e2);
                    else if (count == 2)
                        Assert.True(it.Entity(0) == e3);
                    else
                        Assert.True(false);
                    count++;
                }
            });
            Assert.Equal(3, count);

            count = 0;
            qReverse.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    if (count == 0)
                        Assert.True(it.Entity(0) == e3);
                    else if (count == 1)
                        Assert.True(it.Entity(0) == e2);
                    else if (count == 2)
                        Assert.True(it.Entity(0) == e1);
                    else
                        Assert.True(false);
                    count++;
                }
            });
            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByTemplate(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<TagA>();
            world.Component<TagB>();
            world.Component<TagC>();
            world.Component<TagX>();

            Query q = world.QueryBuilder()
                .With<TagX>()
                .GroupBy<TagX>(GroupByFirstId)
                .Build();

            Query qReverse = world.QueryBuilder()
                .With<TagX>()
                .GroupBy<TagX>(GroupByFirstIdNegated)
                .Build();

            Entity e3 = world.Entity().Add<TagX>().Add<TagC>();
            Entity e2 = world.Entity().Add<TagX>().Add<TagB>();
            Entity e1 = world.Entity().Add<TagX>().Add<TagA>();

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    if (count == 0)
                        Assert.True(it.Entity(0) == e1);
                    else if (count == 1)
                        Assert.True(it.Entity(0) == e2);
                    else if (count == 2)
                        Assert.True(it.Entity(0) == e3);
                    else
                        Assert.True(false);
                    count++;
                }
            });
            Assert.Equal(3, count);

            count = 0;
            qReverse.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    if (count == 0)
                        Assert.True(it.Entity(0) == e3);
                    else if (count == 1)
                        Assert.True(it.Entity(0) == e2);
                    else if (count == 2)
                        Assert.True(it.Entity(0) == e1);
                    else
                        Assert.True(false);
                    count++;
                }
            });
            Assert.Equal(3, count);
        }

        private static ulong group_by_rel(ecs_world_t* world, ecs_table_t* table, ulong id, void* ctx)
        {
            ulong match;
            return ecs_search(world, table, Macros.Pair(id, EcsWildcard), &match) != -1 ? Macros.PairSecond(match) : 0;
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByIterOne(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();
            Entity tgtC = world.Entity();
            Entity tag = world.Entity();

            world.Entity().Add(rel, tgtA);
            Entity e2 = world.Entity().Add(rel, tgtB);
            world.Entity().Add(rel, tgtC);

            world.Entity().Add(rel, tgtA).Add(tag);
            Entity e5 = world.Entity().Add(rel, tgtB).Add(tag);
            world.Entity().Add(rel, tgtC).Add(tag);

            Query q = world.QueryBuilder()
                .With(rel, Ecs.Wildcard)
                .GroupBy(rel, group_by_rel)
                .Build();

            bool e2Found = false;
            bool e5Found = false;
            int count = 0;

            q.Iter().SetGroup(tgtB).Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                Assert.True(it.GroupId() == tgtB);

                if (e == e2) e2Found = true;
                if (e == e5) e5Found = true;
                count++;
            });

            Assert.Equal(2, count);
            Assert.True(e2Found);
            Assert.True(e5Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByIterOneTemplate(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();


            world.Entity().Add<Rel, TgtA>();
            Entity e2 = world.Entity().Add<Rel, TgtB>();
            world.Entity().Add<Rel, TgtC>();

            world.Entity().Add<Rel, TgtA>().Add<Tag>();
            Entity e5 = world.Entity().Add<Rel, TgtB>().Add<Tag>();
            world.Entity().Add<Rel, TgtC>().Add<Tag>();

            Query q = world.QueryBuilder()
                .With<Rel>(Ecs.Wildcard)
                .GroupBy<Rel>(group_by_rel)
                .Build();

            bool e2Found = false;
            bool e5Found = false;
            int count = 0;

            q.Iter().SetGroup<TgtB>().Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                Assert.True(it.GroupId() == world.Id<TgtB>());

                if (e == e2) e2Found = true;
                if (e == e5) e5Found = true;
                count++;
            });

            Assert.Equal(2, count);
            Assert.True(e2Found);
            Assert.True(e5Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByIterOneAllGroups(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();
            Entity tgtC = world.Entity();
            Entity tag = world.Entity();

            Entity e1 = world.Entity().Add(rel, tgtA);
            Entity e2 = world.Entity().Add(rel, tgtB);
            Entity e3 = world.Entity().Add(rel, tgtC);

            Entity e4 = world.Entity().Add(rel, tgtA).Add(tag);
            Entity e5 = world.Entity().Add(rel, tgtB).Add(tag);
            Entity e6 = world.Entity().Add(rel, tgtC).Add(tag);

            Query q = world.QueryBuilder()
                .With(rel, Ecs.Wildcard)
                .GroupBy(rel, group_by_rel)
                .Build();

            int e1Found = 0;
            int e2Found = 0;
            int e3Found = 0;
            int e4Found = 0;
            int e5Found = 0;
            int e6Found = 0;
            int count = 0;
            ulong groupId = 0;

            Ecs.EachIndexCallback func = (Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                Assert.True(it.GroupId() == groupId);
                if (e == e1) e1Found++;
                if (e == e2) e2Found++;
                if (e == e3) e3Found++;
                if (e == e4) e4Found++;
                if (e == e5) e5Found++;
                if (e == e6) e6Found++;
                count++;
            };

            groupId = tgtB;
            q.Iter().SetGroup(tgtB).Each(func);
            Assert.Equal(2, count);
            Assert.Equal(1, e2Found);
            Assert.Equal(1, e5Found);

            groupId = tgtA;
            q.Iter().SetGroup(tgtA).Each(func);
            Assert.Equal(4, count);
            Assert.Equal(1, e1Found);
            Assert.Equal(1, e4Found);

            groupId = tgtC;
            q.Iter().SetGroup(tgtC).Each(func);
            Assert.Equal(6, count);
            Assert.Equal(1, e3Found);
            Assert.Equal(1, e6Found);

            Assert.Equal(1, e1Found);
            Assert.Equal(1, e2Found);
            Assert.Equal(1, e3Found);
            Assert.Equal(1, e4Found);
            Assert.Equal(1, e5Found);
            Assert.Equal(1, e6Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByDefaultFuncWithId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();
            Entity tgtC = world.Entity();

            Entity e1 = world.Entity().Add(rel, tgtC);
            Entity e2 = world.Entity().Add(rel, tgtB);
            Entity e3 = world.Entity().Add(rel, tgtA);

            Query q = world.QueryBuilder()
                .With(rel, Ecs.Wildcard)
                .GroupBy(rel)
                .Build();

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;
            int count = 0;

            q.Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                if (e == e1)
                {
                    Assert.True(it.GroupId() == tgtC);
                    Assert.True(!e1Found);
                    Assert.True(e2Found);
                    Assert.True(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.True(it.GroupId() == tgtB);
                    Assert.True(!e1Found);
                    Assert.True(!e2Found);
                    Assert.True(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.True(it.GroupId() == tgtA);
                    Assert.True(!e1Found);
                    Assert.True(!e2Found);
                    Assert.True(!e3Found);
                    e3Found = true;
                }

                count++;
            });

            Assert.Equal(3, count);
            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByDefaultFuncWithType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();


            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();
            Entity tgtC = world.Entity();

            Entity e1 = world.Entity().Add<Rel>(tgtC);
            Entity e2 = world.Entity().Add<Rel>(tgtB);
            Entity e3 = world.Entity().Add<Rel>(tgtA);

            Query q = world.QueryBuilder()
                .With<Rel>(Ecs.Wildcard)
                .GroupBy<Rel>()
                .Build();

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;
            int count = 0;

            q.Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                if (e == e1)
                {
                    Assert.True(it.GroupId() == tgtC);
                    Assert.True(!e1Found);
                    Assert.True(e2Found);
                    Assert.True(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.True(it.GroupId() == tgtB);
                    Assert.True(!e1Found);
                    Assert.True(!e2Found);
                    Assert.True(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.True(it.GroupId() == tgtA);
                    Assert.True(!e1Found);
                    Assert.True(!e2Found);
                    Assert.True(!e3Found);
                    e3Found = true;
                }

                count++;
            });

            Assert.Equal(3, count);
            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void GroupByCallbacks(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();
            Entity tgtC = world.Entity();

            Entity e1 = world.Entity().Add<Rel>(tgtC);
            Entity e2 = world.Entity().Add<Rel>(tgtB);
            Entity e3 = world.Entity().Add<Rel>(tgtA);

            int* groupByCtx = stackalloc int[1];

            Query q = world.QueryBuilder()
                .With<Rel>(Ecs.Wildcard)
                .GroupBy<Rel>()
                .GroupByCtx(&groupByCtx[0])
                .OnGroupCreate((ecs_world_t* world, ulong id, void* groupbyArg) =>
                {
                    Assert.True(world != null);
                    Assert.True(id != 0);
                    Assert.True(groupbyArg != null);
                    Assert.True(groupbyArg == &groupByCtx[0]);
                    ulong* ctx = Memory.Alloc<ulong>(1);
                    *ctx = id;
                    return ctx;
                })
                .OnGroupDelete(
                    (ecs_world_t* world, ulong id, void* ctx, void* groupbyArg) =>
                    {
                        Assert.True(world != null);
                        Assert.True(id != 0);
                        Assert.True(groupbyArg != null);
                        Assert.True(groupbyArg == &groupByCtx[0]);
                        Assert.True(ctx != null);
                        Assert.Equal(id, *(ulong*)ctx);
                        Memory.Free(ctx);
                    })
                .Build();

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;
            int count = 0;

            q.Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                if (e == e1)
                {
                    Assert.True(it.GroupId() == tgtC);
                    Assert.True(!e1Found);
                    Assert.True(e2Found);
                    Assert.True(e3Found);
                    e1Found = true;
                    ulong* ctx = (ulong*)q.GroupCtx(it.GroupId());
                    Assert.Equal(it.GroupId(), *ctx);
                }

                if (e == e2)
                {
                    Assert.True(it.GroupId() == tgtB);
                    Assert.True(!e1Found);
                    Assert.True(!e2Found);
                    Assert.True(e3Found);
                    e2Found = true;
                    ulong* ctx = (ulong*)q.GroupCtx(it.GroupId());
                    Assert.Equal(it.GroupId(), *ctx);
                }

                if (e == e3)
                {
                    Assert.True(it.GroupId() == tgtA);
                    Assert.True(!e1Found);
                    Assert.True(!e2Found);
                    Assert.True(!e3Found);
                    e3Found = true;
                    ulong* ctx = (ulong*)q.GroupCtx(it.GroupId());
                    Assert.Equal(it.GroupId(), *ctx);
                }

                count++;
            });

            Assert.Equal(3, count);
            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetGroupOnQuery(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();
            Entity tgtC = world.Entity();
            Entity tag = world.Entity();

            world.Entity().Add(rel, tgtA);
            Entity e2 = world.Entity().Add(rel, tgtB);
            world.Entity().Add(rel, tgtC);

            world.Entity().Add(rel, tgtA).Add(tag);
            Entity e5 = world.Entity().Add(rel, tgtB).Add(tag);
            world.Entity().Add(rel, tgtC).Add(tag);

            Query q = world.QueryBuilder()
                .With(rel, Ecs.Wildcard)
                .GroupBy(rel, group_by_rel)
                .Build();

            bool e2Found = false;
            bool e5Found = false;
            int count = 0;

            q.SetGroup(tgtB).Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                Assert.True(it.GroupId() == tgtB);

                if (e == e2) e2Found = true;
                if (e == e5) e5Found = true;
                count++;
            });

            Assert.Equal(2, count);
            Assert.True(e2Found);
            Assert.True(e5Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void SetGroupTypeOnQuery(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity tag = world.Entity();

            world.Entity().Add<Rel, TgtA>();
            Entity e2 = world.Entity().Add<Rel, TgtB>();
            world.Entity().Add<Rel, TgtC>();

            world.Entity().Add<Rel, TgtA>().Add(tag);
            Entity e5 = world.Entity().Add<Rel, TgtB>().Add(tag);
            world.Entity().Add<Rel, TgtC>().Add(tag);

            Query q = world.QueryBuilder()
                .With<Rel>(Ecs.Wildcard)
                .GroupBy<Rel>(group_by_rel)
                .Build();

            bool e2Found = false;
            bool e5Found = false;
            int count = 0;

            q.SetGroup<TgtB>().Each((Iter it, int i) =>
            {
                Entity e = it.Entity(i);
                Assert.True(it.GroupId() == world.Id<TgtB>());

                if (e == e2) e2Found = true;
                if (e == e5) e5Found = true;
                count++;
            });

            Assert.Equal(2, count);
            Assert.True(e2Found);
            Assert.True(e5Found);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void CreateWithNoTemplateArgs(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AnyWildcard(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apple = world.Entity();
            Entity mango = world.Entity();

            Entity e1 = world.Entity()
                .Add(likes, apple)
                .Add(likes, mango);

            Query q = world.QueryBuilder()
                .With(likes, Ecs.Any)
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Cascade(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity tag = world.Entity().Add(Ecs.OnInstantiate, Ecs.Inherit);
            Entity foo = world.Entity();
            Entity bar = world.Entity();

            Entity e0 = world.Entity().Add(tag);
            Entity e1 = world.Entity().IsA(e0);
            Entity e2 = world.Entity().IsA(e1);
            Entity e3 = world.Entity().IsA(e2);

            Query q = world.QueryBuilder()
                .With(tag).Cascade(Ecs.IsA)
                .Cached()
                .Build();

            e1.Add(bar);
            e2.Add(foo);

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;

                if (e == e1)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.True(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.True(e1Found);
                    Assert.True(e2Found);
                    Assert.False(e3Found);
                    e3Found = true;
                }
            });

            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void CascadeDesc(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity tag = world.Entity().Add(Ecs.OnInstantiate, Ecs.Inherit);
            Entity foo = world.Entity();
            Entity bar = world.Entity();

            Entity e0 = world.Entity().Add(tag);
            Entity e1 = world.Entity().IsA(e0);
            Entity e2 = world.Entity().IsA(e1);
            Entity e3 = world.Entity().IsA(e2);

            Query q = world.QueryBuilder()
                .With(tag).Cascade(Ecs.IsA).Descend()
                .Cached()
                .Build();

            e1.Add(bar);
            e2.Add(foo);

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;

                if (e == e1)
                {
                    Assert.False(e1Found);
                    Assert.True(e2Found);
                    Assert.True(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.True(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e3Found = true;
                }
            });

            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void CascadeWithRelationship(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity tag = world.Entity();
            Entity foo = world.Entity();
            Entity bar = world.Entity();

            Entity e0 = world.Entity().Add(tag);
            Entity e1 = world.Entity().ChildOf(e0);
            Entity e2 = world.Entity().ChildOf(e1);
            Entity e3 = world.Entity().ChildOf(e2);

            Query q = world.QueryBuilder()
                .With(tag).Cascade(Ecs.ChildOf)
                .Cached()
                .Build();

            e1.Add(bar);
            e2.Add(foo);

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;

                if (e == e1)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.True(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.True(e1Found);
                    Assert.True(e2Found);
                    Assert.False(e3Found);
                    e3Found = true;
                }
            });

            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void UpWithType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();


            world.Component<Rel>().Entity.Add(Ecs.Traversable);

            Query q = world.QueryBuilder<Self>()
                .With<Other>().Src().Up<Rel>().In()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Entity().Set(new Other(10));

            Entity e = world.Entity().Add<Rel>(@base);
            e.Set(new Self(e));
            e = world.Entity().Add<Rel>(@base);
            e.Set(new Self(e));
            e = world.Entity().Add<Rel>(@base);
            e.Set(new Self(e));

            int count = 0;

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> s = it.Field<Self>(0);
                    Field<Other> o = it.Field<Other>(1);
                    Assert.True(!it.IsSelf(1));
                    Assert.Equal(10, o[0].Value);

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == s[i].Value);
                        count++;
                    }
                }
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void CascadeWithType(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();


            world.Component<Rel>().Entity.Add(Ecs.Traversable);

            Entity tag = world.Entity();
            Entity foo = world.Entity();
            Entity bar = world.Entity();

            Entity e0 = world.Entity().Add(tag);
            Entity e1 = world.Entity().Add<Rel>(e0);
            Entity e2 = world.Entity().Add<Rel>(e1);
            Entity e3 = world.Entity().Add<Rel>(e2);

            Query q = world.QueryBuilder()
                .With(tag).Cascade<Rel>()
                .Cached()
                .Build();

            e1.Add(bar);
            e2.Add(foo);

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;

                if (e == e1)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.True(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.True(e1Found);
                    Assert.True(e2Found);
                    Assert.False(e3Found);
                    e3Found = true;
                }
            });

            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void NamedQuery(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Query q = world.QueryBuilder("my_query")
                .With<Position>()
                .CacheKind(cacheKind)
                .Build();

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

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TermWithWrite(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>()
                .With<Position>().Write()
                .CacheKind(cacheKind)
                .Build();

            Assert.True(q.Term(0).InOut() == Ecs.InOutDefault);
            Assert.True(q.Term(0).GetSrc() == Ecs.This);
            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TermWithRead(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .With<Position>()
                .With<Position>().Read()
                .CacheKind(cacheKind)
                .Build();

            Assert.True(q.Term(0).InOut() == Ecs.InOutDefault);
            Assert.True(q.Term(0).GetSrc() == Ecs.This);
            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IterWithStage(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.SetStageCount(2);
            World stage = world.GetStage(1);

            Entity e1 = world.Entity().Add<Position>();

            Query q = world.Query<Position>();

            int count = 0;
            q.Iter(stage).Each((Iter it, int i) =>
            {
                Assert.True(it.World() == stage);
                Assert.True(it.Entity(i) == e1);
                count++;
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void BuilderForceAssignOperator(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(10, 20));

            Entity f = world.Entity().Set(
                new QueryWrapper(
                    world.QueryBuilder()
                        .With<Position>()
                        .CacheKind(cacheKind)
                        .Build()
                )
            );

            int count = 0;
            f.GetPtr<QueryWrapper>()->Query.Each((Entity e) =>
            {
                Assert.True(e == e1);
                count++;
            });
        }

        private static int QueryArg(Query q)
        {
            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            return count;
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void QueryAsArg(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder<Self>()
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Assert.Equal(3, QueryArg(f));
        }

        private static int QueryMoveArg(Query q)
        {
            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            return count;
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void QueryAsMoveArg(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Assert.Equal(3, QueryMoveArg(world.Query<Self>()));
        }

        private static Query QueryReturn(World world)
        {
            return world.Query<Self>();
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void QueryAsReturn(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Query f = QueryReturn(world);

            int count = 0;

            f.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void QueryCopy(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Query f = world.QueryBuilder<Self>()
                .CacheKind(cacheKind)
                .Build();

            Query f2 = f;

            int count = 0;

            f2.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachQuery1Component(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            int count = 0;

            world.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachQuery2Components(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e))
                .Set(new Position(10, 20));

            e = world.Entity();
            e.Set(new Self(e))
                .Set(new Position(10, 20));

            e = world.Entity();
            e.Set(new Self(e))
                .Set(new Position(10, 20));

            int count = 0;

            world.Each((Entity e, ref Self s, ref Position p) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachQuery1ComponentNoEntity(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Entity()
                .Set(new Position(10, 20));

            world.Entity()
                .Set(new Position(10, 20));

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            int count = 0;

            world.Each((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachQuery2ComponentsNoEntity(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity()
                .Set(new Position(3, 5));

            world.Entity()
                .Set(new Velocity(20, 40));

            int count = 0;

            world.Each((ref Position p, ref Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TermAfterArg(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Add<TagA>()
                .Add<TagB>()
                .Add<TagC>();

            world.Entity()
                .Add<TagA>()
                .Add<TagB>();

            Query f = world.QueryBuilder<TagA, TagB>()
                .TermAt(0).Src(Ecs.This)
                .With<TagC>()
                .CacheKind(cacheKind)
                .Build();

            Assert.Equal(3, f.FieldCount());

            int count = 0;
            f.Each((Entity e) =>
            {
                Assert.True(e == e1);
                count++;
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void NameArg(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity("Foo").Set(new Position(10, 20));

            Query f = world.QueryBuilder<Position>()
                .TermAt(0).Src().Name("Foo")
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            f.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    count++;
                    Assert.Equal(10, p[0].X);
                    Assert.Equal(20, p[0].Y);
                    Assert.True(it.Src(0) == e);
                }
            });

            Assert.Equal(1, count);
        }

        // [Theory]
        // [MemberData(nameof(CacheKinds))]
        // private void ConstInTerm(ecs_query_cache_kind_t cacheKind)
        // {
        //     using World world = World.Create();
        //
        //     world.Entity().Set(new Position(10, 20));
        //
        //     Query f = world.QueryBuilder()
        //         .With<Position>()
        //         .CacheKind(cacheKind)
        //         .Build();
        //
        //     int count = 0;
        //     f.Run((Iter it) =>
        //     {
        //         Field<Position> p = it.Field<Position>(0);
        //         Assert.True(it.IsReadonly(0));
        //         foreach (int i in it)
        //         {
        //             count++;
        //             Assert.Equal(10, p[i].X);
        //             Assert.Equal(20, p[i].Y);
        //         }
        //     });
        //
        //     Assert.Equal(1, count);
        // }

        // [Theory]
        // [MemberData(nameof(CacheKinds))]
        // private void ConstOptional(ecs_query_cache_kind_t cacheKind)
        // {
        //     using World world = World.Create();
        //
        //     world.Entity().Set(new Position(10, 20)).Add<TagA>();
        //     world.Entity().Add<TagA>();
        //
        //     Query f = world.QueryBuilder<TagA, Position>()
        //         .CacheKind(cacheKind)
        //         .Build();
        //
        //     int count = 0, setCount = 0;
        //     f.Run((Iter it) =>
        //     {
        //         Assert.Equal(1, it.Count());
        //         if (it.IsSet(1))
        //         {
        //             Field<Position> p = it.Field<Position>(1);
        //             Assert.True(it.IsReadonly(1));
        //             Assert.Equal(10, p[0].X);
        //             Assert.Equal(20, p[0].Y);
        //             setCount++;
        //         }
        //
        //         count++;
        //     });
        //
        //     Assert.Equal(2, count);
        //     Assert.Equal(1, setCount);
        // }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void _2TermsWithExpr(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity a = world.Entity("A");
            Entity b = world.Entity("B");

            Entity e1 = world.Entity().Add(a).Add(b);

            Query f = world.QueryBuilder()
                .Expr("A, B")
                .CacheKind(cacheKind)
                .Build();

            Assert.Equal(2, f.FieldCount());

            int count = 0;
            f.Each((Iter it, int index) =>
            {
                if (it.Entity(index) == e1)
                {
                    Assert.True(it.Id(0) == a);
                    Assert.True(it.Id(1) == b);
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        // [Theory]
        // [MemberData(nameof(CacheKinds))]
        // private void AssertOnUninitializedTerm(ecs_query_cache_kind_t cacheKind)
        // {
        //     // install_test_abort();
        //
        //     using World world = World.Create();
        //
        //     world.Entity("A");
        //     world.Entity("B");
        //
        //     // test_expect_abort();
        //
        //     using Query f = world.QueryBuilder()
        //         .Term()
        //         .Term()
        //         .CacheKind(cacheKind)
        //         .Build();
        // }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void OperatorShortcuts(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity a = world.Entity();
            Entity b = world.Entity();
            Entity c = world.Entity();
            Entity d = world.Entity();
            Entity e = world.Entity();
            Entity f = world.Entity();
            Entity g = world.Entity();
            Entity h = world.Entity();

            Query query = world.QueryBuilder()
                .With(a).And()
                .With(b).Or()
                .With(c)
                .With(d).Not()
                .With(e).Optional()
                .With(f).AndFrom()
                .With(g).OrFrom()
                .With(h).NotFrom()
                .CacheKind(cacheKind)
                .Build();

            Term t = query.Term(0);
            Assert.Equal(a, t.Id());
            Assert.Equal(Ecs.And, t.Oper());

            t = query.Term(1);
            Assert.Equal(b, t.Id());
            Assert.Equal(Ecs.Or, t.Oper());

            t = query.Term(2);
            Assert.Equal(c, t.Id());
            Assert.Equal(Ecs.And, t.Oper());

            t = query.Term(3);
            Assert.Equal(d, t.Id());
            Assert.Equal(Ecs.Not, t.Oper());

            t = query.Term(4);
            Assert.Equal(e, t.Id());
            Assert.Equal(Ecs.Optional, t.Oper());

            t = query.Term(5);
            Assert.Equal(f, t.Id());
            Assert.Equal(Ecs.AndFrom, t.Oper());

            t = query.Term(6);
            Assert.Equal(g, t.Id());
            Assert.Equal(Ecs.OrFrom, t.Oper());

            t = query.Term(7);
            Assert.Equal(h, t.Id());
            Assert.Equal(Ecs.NotFrom, t.Oper());
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void InOutShortcuts(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity a = world.Entity();
            Entity b = world.Entity();
            Entity c = world.Entity();
            Entity d = world.Entity();

            Query query = world.QueryBuilder()
                .With(a).In()
                .With(b).Out()
                .With(c).InOut()
                .With(d).InOutNone()
                .CacheKind(cacheKind)
                .Build();

            Term t = query.Term(0);
            Assert.Equal(a, t.Id());
            Assert.Equal(Ecs.In, t.InOut());

            t = query.Term(1);
            Assert.Equal(b, t.Id());
            Assert.Equal(Ecs.Out, t.InOut());

            t = query.Term(2);
            Assert.Equal(c, t.Id());
            Assert.Equal(Ecs.InOut, t.InOut());

            t = query.Term(3);
            Assert.Equal(d, t.Id());
            Assert.Equal(Ecs.InOutNone, t.InOut());
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IterColumnWithConstAsArray(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            Entity e1 = world.Entity().Set(new Position(10, 20));
            Entity e2 = world.Entity().Set(new Position(20, 30));

            int count = 0;
            f.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    foreach (int i in it)
                    {
                        p[i].X += 1;
                        p[i].Y += 2;

                        count++;
                    }
                }
            });

            Assert.Equal(2, count);

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(21, p->X);
            Assert.Equal(32, p->Y);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IterColumnWithConstAsPtr(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Prefab().Set(new Position(10, 20));
            world.Entity().IsA(@base);
            world.Entity().IsA(@base);

            int count = 0;
            f.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    for (int i = 0; i < it.Count(); i++)
                    {
                        Assert.Equal(10, p[0].X);
                        Assert.Equal(20, p[0].Y);
                        count++;
                    }
                }
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IterColumnWithConstDeref(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder<Position>()
                .CacheKind(cacheKind)
                .Build();

            Entity @base = world.Prefab().Set(new Position(10, 20));
            world.Entity().IsA(@base);
            world.Entity().IsA(@base);

            int count = 0;
            f.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Position pv = p[0];
                    for (int i = 0; i < it.Count(); i++)
                    {
                        Assert.Equal(10, pv.X);
                        Assert.Equal(20, pv.Y);
                        count++;
                    }
                }
            });

            Assert.Equal(2, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With(world.Id<Position>())
                    .With(world.Id<Velocity>())
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Velocity>();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With("Velocity")
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithComponent(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With<Velocity>()
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithPairId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With(likes, apples)
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add(likes, apples);
            world.Entity().Add<Position>().Add(likes, pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithPairName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity("Likes");
            Entity apples = world.Entity("Apples");
            Entity pears = world.Entity("Pears");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With("Likes", "Apples")
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add(likes, apples);
            world.Entity().Add<Position>().Add(likes, pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithPairComponents(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With<Likes, Apples>()
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Likes, Apples>();
            world.Entity().Add<Position>().Add<Likes, Pears>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithPairComponentId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With<Likes>(apples)
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Likes>(apples);
            world.Entity().Add<Position>().Add<Likes>(pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithPairComponentName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity("Apples");
            Entity pears = world.Entity("Pears");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With<Likes>("Apples")
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add<Likes>(apples);
            world.Entity().Add<Position>().Add<Likes>(pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithEnum(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .With(Color.Green)
                    .CacheKind(cacheKind)
                    .Build();

            Entity e1 = world.Entity().Add<Position>().Add(Color.Green);
            world.Entity().Add<Position>().Add(Color.Red);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With(world.Id<Position>())
                    .Without(world.Id<Velocity>())
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add<Velocity>();
            Entity e2 = world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Velocity>();

            Query q =
                world.QueryBuilder()
                    .With(world.Id<Position>())
                    .Without("Velocity")
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add<Velocity>();
            Entity e2 = world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutComponent(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without<Velocity>()
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add<Velocity>();
            Entity e2 = world.Entity().Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutPairId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without(likes, apples)
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add(likes, apples);
            Entity e2 = world.Entity().Add<Position>().Add(likes, pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutPairName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity("Likes");
            Entity apples = world.Entity("Apples");
            Entity pears = world.Entity("Pears");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without("Likes", "Apples")
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add(likes, apples);
            Entity e2 = world.Entity().Add<Position>().Add(likes, pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutPairComponents(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without<Likes, Apples>()
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add<Likes, Apples>();
            Entity e2 = world.Entity().Add<Position>().Add<Likes, Pears>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutPairComponentId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();
            Entity pears = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without<Likes>(apples)
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add<Likes>(apples);
            Entity e2 = world.Entity().Add<Position>().Add<Likes>(pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutPairComponentName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity("Apples");
            Entity pears = world.Entity("Pears");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without<Likes>("Apples")
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add<Likes>(apples);
            Entity e2 = world.Entity().Add<Position>().Add<Likes>(pears);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithoutEnum(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Without(Color.Green)
                    .CacheKind(cacheKind)
                    .Build();

            world.Entity().Add<Position>().Add(Color.Green);
            Entity e2 = world.Entity().Add<Position>().Add(Color.Red);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e2);
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WriteId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write(world.Id<Position>())
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Position>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WriteName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Position>();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write("Position")
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Position>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WriteComponent(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Position>();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write<Position>()
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Position>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WritePairId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write(likes, apples)
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == likes);
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WritePairName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity("Likes");
            Entity apples = world.Entity("Apples");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write("Likes", "Apples")
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == likes);
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WritePairComponents(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write<Likes, Apples>()
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Likes>());
            Assert.True(q.Term(1).GetSecond() == world.Id<Apples>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WritePairComponentId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write<Likes>(apples)
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Likes>());
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WritePairComponentName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity("Apples");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write<Likes>("Apples")
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Likes>());
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WriteEnum(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Write(Color.Green)
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.Out);
            Assert.True(q.Term(1).GetFirst() == world.Id<Color>());
            Assert.True(q.Term(1).GetSecond() == world.ToEntity(Color.Green));
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read(world.Id<Position>())
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Position>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Position>();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read("Position")
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Position>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadComponent(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Component<Position>();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read<Position>()
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Position>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadPairId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity apples = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read(likes, apples)
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == likes);
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadPairName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity likes = world.Entity("Likes");
            Entity apples = world.Entity("Apples");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read("Likes", "Apples")
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == likes);
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadPairComponents(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read<Likes, Apples>()
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Likes>());
            Assert.True(q.Term(1).GetSecond() == world.Id<Apples>());
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadPairComponentId(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read<Likes>(apples)
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Likes>());
            Assert.True(q.Term(1).GetSecond() == apples);
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadPairComponentName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity apples = world.Entity("Apples");

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read<Likes>("Apples")
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Likes>());
            Assert.True(q.Term(1).GetSecond() == apples);

            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void ReadEnum(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q =
                world.QueryBuilder()
                    .With<Position>()
                    .Read(Color.Green)
                    .CacheKind(cacheKind)
                    .Build();

            Assert.True(q.Term(1).InOut() == Ecs.In);
            Assert.True(q.Term(1).GetFirst() == world.Id<Color>());
            Assert.True(q.Term(1).GetSecond() == world.ToEntity(Color.Green));
            Assert.True(q.Term(1).GetSrc() == 0);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void AssignAfterInit(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f;
            QueryBuilder fb = world.QueryBuilder();
            fb.With<Position>();
            fb.CacheKind(cacheKind);
            f = fb.Build();

            Entity e1 = world.Entity().Set(new Position(10, 20));

            int count = 0;
            f.Each((Entity e) =>
            {
                Assert.True(e == e1);
                count++;
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithtInOut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
                .With(world.Id<Position>())
                .CacheKind(cacheKind)
                .Build();

            Assert.True(f.Term(0).InOut() == Ecs.InOutDefault);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithTInOut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
                .With<Position>()
                .CacheKind(cacheKind)
                .Build();

            Assert.True(f.Term(0).InOut() == Ecs.InOutDefault);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithRTInOut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
                .With<Position, Velocity>()
                .CacheKind(cacheKind)
                .Build();

            Assert.True(f.Term(0).InOut() == Ecs.InOutDefault);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithRtInOut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
                .With<Position>(world.Id<Velocity>())
                .CacheKind(cacheKind)
                .Build();

            Assert.True(f.Term(0).InOut() == Ecs.InOutDefault);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WithrtInOut(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder()
                .With(world.Id<Position>(), world.Id<Velocity>())
                .CacheKind(cacheKind)
                .Build();

            Assert.True(f.Term(0).InOut() == Ecs.InOutDefault);
        }

        private static int FilterMoveArg(Query q)
        {
            int count = 0;

            q.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            return count;
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void FilterAsMoveArg(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query f = world.QueryBuilder<Self>()
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Assert.Equal(3, FilterMoveArg(world.Query<Self>()));
        }

        private static Query FilterReturn(World world, ecs_query_cache_kind_t cacheKind)
        {
            return world.QueryBuilder<Self>()
                .CacheKind(cacheKind)
                .Build();
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void FilterAsReturn(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Query f = FilterReturn(world, cacheKind);

            int count = 0;

            f.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void FilterCopy(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            Query f = world.QueryBuilder<Self>()
                .CacheKind(cacheKind)
                .Build();

            Query f2 = f;

            int count = 0;

            f2.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachFilter1Component(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            e = world.Entity();
            e.Set(new Self(e));

            int count = 0;

            world.Each((Entity e, ref Self s) =>
            {
                Assert.True(e == s.Value);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachFilter2Components(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Set(new Self(e))
                .Set(new Position(10, 20));

            e = world.Entity();
            e.Set(new Self(e))
                .Set(new Position(10, 20));

            e = world.Entity();
            e.Set(new Self(e))
                .Set(new Position(10, 20));

            int count = 0;

            world.Each((Entity e, ref Self s, ref Position p) =>
            {
                Assert.True(e == s.Value);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachFilter1ComponentNoEntity(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Entity()
                .Set(new Position(10, 20));

            world.Entity()
                .Set(new Position(10, 20));

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            int count = 0;

            world.Each((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void WorldEachFilter2ComponentsNoEntity(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Entity()
                .Set(new Position(3, 5));

            world.Entity()
                .Set(new Velocity(20, 40));

            int count = 0;

            world.Each((ref Position p, ref Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
                count++;
            });

            Assert.Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void VarSrcWithPrefixedName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query r = world.QueryBuilder()
                .With<Foo>().Src("$Var")
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity().Add<Foo>();

            int count = 0;
            r.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.True(it.GetVar("Var") == e);
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void VarFirstWithPrefixedName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query r = world.QueryBuilder()
                .With<Foo>()
                .Term().First("$Var")
                .CacheKind(cacheKind)
                .Build();

            Entity e = world.Entity().Add<Foo>();

            int count = 0;
            r.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.True(it.GetVar("Var") == world.Id<Foo>());
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void VarSecondWithPrefixedName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query r = world.QueryBuilder()
                .With<Foo>().Second("$Var")
                .CacheKind(cacheKind)
                .Build();

            Entity t = world.Entity();
            Entity e = world.Entity().Add<Foo>(t);

            int count = 0;
            r.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.True(it.GetVar("Var") == t);
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TermWithSecondVarString(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity foo = world.Entity();

            Query r = world.QueryBuilder()
                .With(foo, "$Var")
                .CacheKind(cacheKind)
                .Build();

            Entity t = world.Entity();
            Entity e = world.Entity().Add(foo, t);

            int count = 0;
            r.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.True(it.GetVar("Var") == t);
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void TermTypeWithSecondVarString(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query r = world.QueryBuilder()
                .With<Foo>("$Var")
                .CacheKind(cacheKind)
                .Build();

            Entity t = world.Entity();
            Entity e = world.Entity().Add<Foo>(t);

            int count = 0;
            r.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.True(it.GetVar("Var") == t);
                    count++;
                }
            });

            Assert.Equal(1, count);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void NamedRule(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Query q = world.QueryBuilder<Position>("my_query")
                .CacheKind(cacheKind)
                .Build();

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

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void NamedScopedRule(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Position>();

            Query q = world.QueryBuilder<Position>("my.query")
                .CacheKind(cacheKind)
                .Build();

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
            Assert.Equal(".my.query", qe.Path());
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void IsValid(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q1 = world.Query<Position>();
            Assert.True(q1);

            Ecs.Log.SetLevel(-4);
            Query q2 = world.QueryBuilder()
                .Expr("foo")
                .CacheKind(cacheKind)
                .Build();
            Assert.True(!q2);
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void UnresolvedByName(ecs_query_cache_kind_t cacheKind)
        {
            using World world = World.Create();

            Query q = world.QueryBuilder()
                .QueryFlags(EcsQueryAllowUnresolvedByName)
                .Expr("$this == Foo")
                .CacheKind(cacheKind)
                .Build();

            Assert.True(q);

            Assert.False(q.Iter().IsTrue());

            world.Entity("Foo");

            Assert.True(q.Iter().IsTrue());
        }

        [Theory]
        [MemberData(nameof(CacheKinds))]
        private void Scope(ecs_query_cache_kind_t cacheKind)
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

            Query r = world.QueryBuilder()
                .With(root)
                .ScopeOpen().Not()
                .With(tagA)
                .Without(tagB)
                .ScopeClose()
                .CacheKind(cacheKind)
                .Build();

            int count = 0;
            r.Each((Entity e) =>
            {
                Assert.True(e != e2);
                count++;
            });

            Assert.Equal(3, count);
        }
    }
}
