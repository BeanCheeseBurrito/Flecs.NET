using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Tests.Cpp;

[SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
[SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
[SuppressMessage("ReSharper", "AccessToDisposedClosure")]
[SuppressMessage("ReSharper", "AccessToModifiedClosure")]
[SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
public unsafe class QueryTests
{
    public static int InvokedCount;

    public QueryTests()
    {
        InvokedCount = default;
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

    // TODO: Remove this?
    // [Fact]
    // private void TermEachTag()
    // {
    //     using World world = World.Create();
    //
    //     Entity e1 = world.Entity().Add<Foo>();
    //     Entity e2 = world.Entity().Add<Foo>();
    //     Entity e3 = world.Entity().Add<Foo>();
    //
    //     e3.Add<Tag>();
    //
    //     int count = 0;
    //     world.Each((Entity e, ref Foo _) =>
    //     {
    //         if (e == e1 || e == e2 || e == e3) count++;
    //     });
    //
    //     Assert.Equal(3, count);
    // }

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
            if (e == e1 || e == e2 || e == e3) count++;
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
            if (e == e1 || e == e2 || e == e3) count++;
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
            if (e == e1 || e == e2 || e == e3) count++;
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
            if (e == e1 || e == e2 || e == e3) count++;
        });

        Assert.Equal(3, count);
    }

    [Fact]
    private void TermGetId()
    {
        using World world = World.Create();

        Entity foo = world.Entity();
        Entity bar = world.Entity();

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With<Velocity>()
            .With(foo, bar)
            .Build();

        Assert.Equal(3, q.FieldCount());

        Term
            t = q.Term(0);
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

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With<Velocity>().Src(src)
            .With(foo, bar)
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

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With<Velocity>().Src(src)
            .With(foo, bar)
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

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With<Velocity>().Src(src)
            .With(foo, bar)
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

        using Query q = world.QueryBuilder()
            .With<A>()
            .Build();

        Entity first = q.Iter().First();
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

        using Query q = world.QueryBuilder()
            .With<A>()
            .Build();

        Assert.Equal(3, q.Count());
    }

    [Fact]
    private void GetIsTrueDirect()
    {
        using World world = World.Create();

        world.Entity().Add<A>();
        world.Entity().Add<A>();
        world.Entity().Add<A>();

        using Query q1 = world.QueryBuilder()
            .With<A>()
            .Build();

        using Query q2 = world.QueryBuilder()
            .With<B>()
            .Build();

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

        using Query q = world.QueryBuilder()
            .With<A>()
            .Build();

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

        using Query<Position, Velocity> f = world.QueryBuilder<Position, Velocity>()
            .TermAt(0).Src(e)
            .TermAt(1).Src(e)
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

        using Query<Position, Velocity> f = world.QueryBuilder<Position, Velocity>()
            .TermAt(0).Src(e)
            .TermAt(1).Src(e)
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

    // [Fact]
    // private void InvalidEachWithNoThis()
    // {
    //     install_test_abort();
    //
    //     using World world = World.Create();
    //
    //     Entity e = world.Entity()
    //         .Set(new Position(10, 20))
    //         .Set(new Velocity(1, 2));
    //
    //     using var f = world.QueryBuilder<Position, Velocity>()
    //         .TermAt(0).Src(e)
    //         .TermAt(1).Src(e)
    //         .Build();
    //
    //     test_expect_abort();
    //
    //     f.Each((Entity e, ref Position p, ref Velocity v) => { });
    // }

    [Fact]
    private void NamedQuery()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        Entity e2 = world.Entity().Add<Position>();

        using Query<Position> q = world.Query<Position>("my_query");

        int count = 0;
        q.Each((Entity e, ref Position _) =>
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
    private void NamedScopedQuery()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        Entity e2 = world.Entity().Add<Position>();

        using Query<Position> q = world.Query<Position>("my.query");

        int count = 0;
        q.Each((Entity e, ref Position _) =>
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

    [Fact]
    private void SetThisVar()
    {
        using World world = World.Create();

        world.Entity().Set(new Position(1, 2));
        Entity e2 = world.Entity().Set(new Position(3, 4));
        world.Entity().Set(new Position(5, 6));

        using Query<Position> q = world.Query<Position>("my.query");

        int count = 0;
        q.Iter().SetVar(0, e2).Each((Entity e, ref Position _) =>
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

        using Query f = world.QueryBuilder()
            .Expr("(ChildOf,#0)")
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

        using Query<Position> q = world.Query<Position>();

        Entity r = q.Find((ref Position p) => { return p.X == 20; });

        Assert.True(r == e2);
    }

    [Fact]
    private void FindNotFound()
    {
        using World world = World.Create();

        world.Entity().Set(new Position(10, 20));
        world.Entity().Set(new Position(20, 30));

        using Query<Position> q = world.Query<Position>();

        Entity r = q.Find((ref Position p) => { return p.X == 30; });

        Assert.True(r == 0);
    }

    [Fact]
    private void FindWithEntity()
    {
        using World world = World.Create();

        world.Entity().Set(new Position(10, 20)).Set(new Velocity(20, 30));
        Entity e2 = world.Entity().Set(new Position(20, 30)).Set(new Velocity(20, 30));

        using Query<Position> q = world.Query<Position>();

        Entity r = q.Find((Entity e, ref Position p) =>
        {
            return p.X == e.GetPtr<Velocity>()->X &&
                   p.Y == e.GetPtr<Velocity>()->Y;
        });

        Assert.True(r == e2);
    }

    [Fact]
    private void FindWithMatchEmptyTables()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Set(new Position(10, 20)).Add<Velocity>();
        e1.Destruct();
        Entity e2 = world.Entity().Set(new Position(20, 30));

        using Query<Position> q = world.QueryBuilder<Position>()
            .QueryFlags(Ecs.QueryMatchEmptyTables)
            .Build();

        Entity r = q.Find((ref Position p) =>
        {
            return p.X == 20;
        });

        Assert.True(r == e2);
    }

    [Fact]
    private void FindWithEntityWithMatchEmptyTables()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Set(new Position(10, 20)).Add<Velocity>();
        e1.Destruct();
        Entity e2 = world.Entity().Set(new Position(20, 30));

        using Query<Position> q = world.QueryBuilder<Position>()
            .QueryFlags(EcsQueryMatchEmptyTables)
            .Build();

        Entity r = q.Find((Entity e, ref Position p) =>
        {
            return p.X == 20;
        });

        Assert.True(r == e2);
    }

    [Fact]
    private void Run()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void RunConst()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void RunShared()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);
        world.Component<Velocity>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        Entity @base = world.Entity()
            .Set(new Velocity(1, 2));

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Add(Ecs.IsA, @base);

        Entity e2 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        using Query<Position> q = world.QueryBuilder<Position>()
            .Expr("Velocity(self|up IsA)")
            .Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                if (!it.IsSelf(1))
                    foreach (int i in it)
                    {
                        p[i].X += v[0].X;
                        p[i].Y += v[0].Y;
                    }
                else
                    foreach (int i in it)
                    {
                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                    }
            }
        });

        Position* p = e1.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);

        p = e2.GetPtr<Position>();
        Assert.Equal(13, p->X);
        Assert.Equal(24, p->Y);
    }

    [Fact]
    private void RunOptional()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();
        world.Component<Mass>();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2))
            .Set(new Mass(1));

        Entity e2 = world.Entity()
            .Set(new Position(30, 40))
            .Set(new Velocity(3, 4))
            .Set(new Mass(1));

        Entity e3 = world.Entity()
            .Set(new Position(50, 60));

        Entity e4 = world.Entity()
            .Set(new Position(70, 80));

        using Query<Position, Velocity, Mass> q = world.QueryBuilder<Position, Velocity, Mass>()
            .TermAt(1).Optional()
            .TermAt(2).Optional()
            .Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);
                Field<Mass> m = it.Field<Mass>(2);

                if (it.IsSet(1) && it.IsSet(2))
                    foreach (int i in it)
                    {
                        p[i].X += v[i].X * m[i].Value;
                        p[i].Y += v[i].Y * m[i].Value;
                    }
                else
                    foreach (int i in it)
                    {
                        p[i].X++;
                        p[i].Y++;
                    }
            }
        });

        Position* p = e1.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);

        p = e2.GetPtr<Position>();
        Assert.Equal(33, p->X);
        Assert.Equal(44, p->Y);

        p = e3.GetPtr<Position>();
        Assert.Equal(51, p->X);
        Assert.Equal(61, p->Y);

        p = e4.GetPtr<Position>();
        Assert.Equal(71, p->X);
        Assert.Equal(81, p->Y);
    }

    [Fact]
    private void RunSparse()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.Sparse);
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    ref Position p = ref it.FieldAt<Position>(0, i);
                    p.X += v[i].X;
                    p.Y += v[i].Y;
                }
            }
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void RunSparseWithWith()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.Sparse);
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With<Velocity>()
            .Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    ref Position p = ref it.FieldAt<Position>(0, i);
                    p.X += v[i].X;
                    p.Y += v[i].Y;
                }
            }
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void Each()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void EachConst()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void EachShared()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity @base = world.Entity()
            .Set(new Velocity(1, 2));

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Add(Ecs.IsA, @base);

        Entity e2 = world.Entity()
            .Set(new Position(20, 30))
            .Add(Ecs.IsA, @base);

        Entity e3 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        Position* p = e1.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);

        p = e2.GetPtr<Position>();
        Assert.Equal(21, p->X);
        Assert.Equal(32, p->Y);

        p = e3.GetPtr<Position>();
        Assert.Equal(13, p->X);
        Assert.Equal(24, p->Y);
    }

    [Fact]
    private void EachOptional()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();
        world.Component<Mass>();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2))
            .Set(new Mass(1));

        Entity e2 = world.Entity()
            .Set(new Position(30, 40))
            .Set(new Velocity(3, 4))
            .Set(new Mass(1));

        Entity e3 = world.Entity()
            .Set(new Position(50, 60));

        Entity e4 = world.Entity()
            .Set(new Position(70, 80));

        using Query<Position, Velocity, Mass> q = world.QueryBuilder<Position, Velocity, Mass>()
            .TermAt(1).Optional()
            .TermAt(2).Optional()
            .Build();

        q.Each((ref Position p, ref Velocity v, ref Mass m) =>
        {
            if (!Unsafe.IsNullRef(ref v) && !Unsafe.IsNullRef(ref m))
            {
                p.X += v.X * m.Value;
                p.Y += v.Y * m.Value;
            }
            else
            {
                p.X++;
                p.Y++;
            }
        });

        Position* p = e1.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);

        p = e2.GetPtr<Position>();
        Assert.Equal(33, p->X);
        Assert.Equal(44, p->Y);

        p = e3.GetPtr<Position>();
        Assert.Equal(51, p->X);
        Assert.Equal(61, p->Y);

        p = e4.GetPtr<Position>();
        Assert.Equal(71, p->X);
        Assert.Equal(81, p->Y);
    }

    [Fact]
    private void EachSparse()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.Sparse);
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void EachSparseWithWith()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.Sparse);
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With<Velocity>()
            .Build();

        q.Each((Iter it, int row) =>
        {
            ref Position p = ref it.FieldAt<Position>(0, row);
            ref Velocity v = ref it.FieldAt<Velocity>(1, row);
            p.X += v.X;
            p.Y += v.Y;
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void EachSparseMany()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.Sparse);
        world.Component<Velocity>();

        List<Entity> entities = new List<Entity>();

        for (int i = 0; i < 2000; i++)
        {
            entities.Add(
                world.Entity()
                    .Set(new Position(10 + i, 20 + i))
                    .Set(new Velocity(i, i))
            );
        }

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        q.Each(static (ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        for (int i = 0; i < 2000; i++)
        {
            Entity e = entities[i];

            Position* p = e.GetPtr<Position>();
            Assert.Equal(10 + i * 2, p->X);
            Assert.Equal(20 + i * 2, p->Y);

            Velocity* v = e.GetPtr<Velocity>();
            Assert.Equal(i, v->X);
            Assert.Equal(i, v->Y);
        }
    }

    [Fact]
    private void Signature()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query q = world.QueryBuilder().Expr("Position, Velocity").Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void SignatureConst()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity entity = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        using Query q = world.QueryBuilder().Expr("Position, [in] Velocity").Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        });

        Position* p = entity.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void SignatureShared()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);
        world.Component<Velocity>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        Entity @base = world.Entity()
            .Set(new Velocity(1, 2));

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Add(Ecs.IsA, @base);

        Entity e2 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(3, 4));

        using Query q = world.QueryBuilder()
            .Expr("Position, [in] Velocity(self|up IsA)")
            .Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                if (!it.IsSelf(1))
                    foreach (int i in it)
                    {
                        p[i].X += v[0].X;
                        p[i].Y += v[0].Y;
                    }
                else
                    foreach (int i in it)
                    {
                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                    }
            }
        });

        Position* p = e1.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);

        p = e2.GetPtr<Position>();
        Assert.Equal(13, p->X);
        Assert.Equal(24, p->Y);
    }

    [Fact]
    private void SignatureOptional()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();
        world.Component<Mass>();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2))
            .Set(new Mass(1));

        Entity e2 = world.Entity()
            .Set(new Position(30, 40))
            .Set(new Velocity(3, 4))
            .Set(new Mass(1));

        Entity e3 = world.Entity()
            .Set(new Position(50, 60));

        Entity e4 = world.Entity()
            .Set(new Position(70, 80));

        using Query q = world.QueryBuilder().Expr("Position, ?Velocity, ?Mass").Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);
                Field<Mass> m = it.Field<Mass>(2);

                if (it.IsSet(1) && it.IsSet(2))
                    foreach (int i in it)
                    {
                        p[i].X += v[i].X * m[i].Value;
                        p[i].Y += v[i].Y * m[i].Value;
                    }
                else
                    foreach (int i in it)
                    {
                        p[i].X++;
                        p[i].Y++;
                    }
            }
        });

        Position* p = e1.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);

        p = e2.GetPtr<Position>();
        Assert.Equal(33, p->X);
        Assert.Equal(44, p->Y);

        p = e3.GetPtr<Position>();
        Assert.Equal(51, p->X);
        Assert.Equal(61, p->Y);

        p = e4.GetPtr<Position>();
        Assert.Equal(71, p->X);
        Assert.Equal(81, p->Y);
    }

    [Fact]
    private void QuerySinglePair()
    {
        using World world = World.Create();

        world.Entity().Add<Pair, Position>();
        Entity e2 = world.Entity().Add<Pair, Velocity>();

        using Query q = world.QueryBuilder()
            .Expr("(Pair, Velocity)")
            .Build();

        int tableCount = 0;
        int entityCount = 0;

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                tableCount++;
                foreach (int i in it)
                {
                    Assert.True(it.Entity(i) == e2);
                    entityCount++;
                }
            }
        });

        Assert.Equal(1, tableCount);
        Assert.Equal(1, entityCount);
    }

    [Fact]
    private void TagWithEach()
    {
        using World world = World.Create();

        using Query q = world.QueryBuilder()
            .With<Tag>()
            .Build();

        Entity e = world.Entity()
            .Add<Tag>();

        q.Each((Entity qe) => { Assert.True(qe == e); });
    }

    [Fact]
    private void SharedTagWithEach()
    {
        using World world = World.Create();

        using Query q = world.QueryBuilder()
            .With<Tag>()
            .Build();

        Entity @base = world.Prefab()
            .Add<Tag>();

        Entity e = world.Entity()
            .Add(Ecs.IsA, @base);

        q.Each((Entity qe) => { Assert.True(qe == e); });
    }

    private static int ComparePosition(ulong e1, void* p1, ulong e2, void* p2)
    {
        Position* pos1 = (Position*)p1;
        Position* pos2 = (Position*)p2;
        return pos1->X.CompareTo(pos2->X);
    }

    [Fact]
    private void SortBy()
    {
        using World world = World.Create();

        world.Entity().Set(new Position(1, 0));
        world.Entity().Set(new Position(6, 0));
        world.Entity().Set(new Position(2, 0));
        world.Entity().Set(new Position(5, 0));
        world.Entity().Set(new Position(4, 0));

        using Query<Position> q = world.QueryBuilder<Position>()
            .OrderBy<Position>(ComparePosition)
            .Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Assert.Equal(5, it.Count());
                Assert.Equal(1, p[0].X);
                Assert.Equal(2, p[1].X);
                Assert.Equal(4, p[2].X);
                Assert.Equal(5, p[3].X);
                Assert.Equal(6, p[4].X);
            }
        });
    }

    [Fact]
    private void Changed()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position(1, 0));

        using Query<Position> q = world.QueryBuilder<Position>()
            .Cached()
            .Build();

        using Query<Position> qW = world.Query<Position>();

        Assert.True(q.Changed());

        q.Each((ref Position _) => { });
        Assert.False(q.Changed());

        e.Set(new Position(2, 0));
        Assert.True(q.Changed());

        q.Each((ref Position _) => { });
        Assert.False(q.Changed());

        qW.Each((ref Position _) => { });
        Assert.True(q.Changed());
    }

    [Fact]
    private void DefaultCtor()
    {
        using World world = World.Create();

        Query<Position> qVar;

        int count = 0;
        using Query<Position> q = world.Query<Position>();

        world.Entity().Set(new Position(10, 20));

        qVar = q;

        qVar.Each((ref Position p) =>
        {
            Assert.Equal(10, p.X);
            Assert.Equal(20, p.Y);
            count++;
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void ExprWithTemplate()
    {
        using World world = World.Create();

        Component<Template<int>> comp = world.Component<Template<int>>();
        Assert.Equal("Template<System.Int32>", comp.Name());

        int count = 0;
        using Query<Position> q = world.QueryBuilder<Position>().Expr("Template<System.Int32>").Build();

        world.Entity()
            .Set(new Position(10, 20))
            .Set(new Template<int>(30, 40));

        q.Each((Entity e, ref Position p) =>
        {
            Assert.Equal(10, p.X);
            Assert.Equal(20, p.Y);

            Template<int>* t = e.GetPtr<Template<int>>();
            Assert.Equal(30, t->X);
            Assert.Equal(40, t->Y);

            count++;
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void QueryTypeWithTemplate()
    {
        using World world = World.Create();

        Component<Template<int>> comp = world.Component<Template<int>>();
        Assert.Equal("Template<System.Int32>", comp.Name());

        int count = 0;
        using Query<Position, Template<int>> q = world.Query<Position, Template<int>>();

        world.Entity()
            .Set(new Position(10, 20))
            .Set(new Template<int>(30, 40));

        q.Each((ref Position p, ref Template<int> t) =>
        {
            Assert.Equal(10, p.X);
            Assert.Equal(20, p.Y);

            Assert.Equal(30, t.X);
            Assert.Equal(40, t.Y);

            count++;
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void CompareTermId()
    {
        using World world = World.Create();

        int count = 0;
        Entity e = world.Entity().Add<Tag>();

        using Query q = world.QueryBuilder()
            .With<Tag>()
            .Build();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Assert.True(it.Id(0) == it.World().Id<Tag>());
                Assert.True(it.Entity(0) == e);
            }

            count++;
        });

        Assert.Equal(1, count);
    }

    // [Fact]
    // private void TestNoDeferEach()
    // {
    //     install_test_abort();
    //
    //     using World world = World.Create();
    //
    //     world.Entity().Add<Tag>().Set(new Value(10));
    //
    //     using var q = world.QueryBuilder<Value>()
    //         .With<Tag>()
    //         .Build();
    //
    //     q.Each((Entity e, ref Value v) =>
    //     {
    //         test_expect_abort();
    //         e.Remove<Tag>();
    //     });
    //
    //     Assert.True(false);
    // }

    // [Fact]
    // private void TestNoDeferIter()
    // {
    //     install_test_abort();
    //
    //     using World world = World.Create();
    //
    //     world.Entity().Add<Tag>().Set(new Value(10));
    //
    //     using var q = world.QueryBuilder<Value>()
    //         .With<Tag>()
    //         .Build();
    //
    //     q.Run((Iter it, Field<Value> v) =>
    //     {
    //         foreach (int i in it)
    //         {
    //             test_expect_abort();
    //             it.Entity(i).Remove<Tag>();
    //         }
    //     });
    //
    //     Assert.True(false);
    // }

    [Fact]
    private void InspectTerms()
    {
        using World world = World.Create();

        Entity p = world.Entity();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>()
            .With(Ecs.ChildOf, p)
            .Build();

        Assert.Equal(3, q.FieldCount());

        Term t = q.Term(0);
        Assert.Equal(world.Id<Position>(), t.Id());
        Assert.Equal(Ecs.And, t.Oper());
        Assert.Equal(Ecs.InOutDefault, t.InOut());

        t = q.Term(1);
        Assert.Equal(world.Id<Velocity>(), t.Id());
        Assert.Equal(Ecs.And, t.Oper());
        Assert.Equal(Ecs.InOutDefault, t.InOut());

        t = q.Term(2);
        Assert.Equal(world.Pair(Ecs.ChildOf, p), t.Id());
        Assert.Equal(Ecs.And, t.Oper());
        Assert.Equal(Ecs.InOutDefault, t.InOut());
        Assert.True(t.Id().Second() == p);
    }

    [Fact]
    private void InspectTermsWithEach()
    {
        using World world = World.Create();

        Entity p = world.Entity();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>()
            .With(Ecs.ChildOf, p)
            .Build();

        int count = 0;
        q.EachTerm((ref Term t) =>
        {
            if (count == 0)
            {
                Assert.Equal(world.Id<Position>(), t.Id());
                Assert.Equal(Ecs.InOutDefault, t.InOut());
            }
            else if (count == 1)
            {
                Assert.Equal(world.Id<Velocity>(), t.Id());
                Assert.Equal(Ecs.InOutDefault, t.InOut());
            }
            else if (count == 2)
            {
                Assert.Equal(world.Pair(Ecs.ChildOf, p), t.Id());
                Assert.True(t.Id().Second() == p);
                Assert.Equal(Ecs.InOutDefault, t.InOut());
            }
            else
            {
                Assert.True(false);
            }

            Assert.Equal(Ecs.And, t.Oper());

            count++;
        });

        Assert.Equal(3, count);
    }

    [Fact]
    private void CompToStr()
    {
        using World world = World.Create();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>()
            .Build();
        Assert.Equal("Position($this), Velocity($this)", q.Str());
    }

    [Fact]
    private void PairToStr()
    {
        using World world = World.Create();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>()
            .With<Eats, Apples>()
            .Build();
        Assert.Equal("Position($this), Velocity($this), Eats($this,Apples)", q.Str());
    }

    [Fact]
    private void OperNotToStr()
    {
        using World world = World.Create();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>().Oper(Ecs.Not)
            .Build();
        Assert.Equal("Position($this), !Velocity($this)", q.Str());
    }

    [Fact]
    private void OperOptionalToStr()
    {
        using World world = World.Create();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>().Oper(Ecs.Optional)
            .Build();
        Assert.Equal("Position($this), ?Velocity($this)", q.Str());
    }

    [Fact]
    private void OperOrToStr()
    {
        using World world = World.Create();

        using Query q = world.QueryBuilder()
            .With<Position>().Oper(Ecs.Or)
            .With<Velocity>()
            .Build();
        Assert.Equal("Position($this) || Velocity($this)", q.Str());
    }

    [Fact]
    private void EachNoEntity1Comp()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position(1, 2));

        using Query<Position> q = world.Query<Position>();

        int count = 0;
        q.Each((ref Position p) =>
        {
            Assert.Equal(1, p.X);
            Assert.Equal(2, p.Y);
            p.X += 1;
            p.Y += 2;
            count++;
        });

        Assert.Equal(1, count);

        Position* pos = e.GetPtr<Position>();
        Assert.Equal(2, pos->X);
        Assert.Equal(4, pos->Y);
    }

    [Fact]
    private void EachNoEntity2Comps()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position(1, 2))
            .Set(new Velocity(10, 20));

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        int count = 0;
        q.Each((ref Position p, ref Velocity v) =>
        {
            Assert.Equal(1, p.X);
            Assert.Equal(2, p.Y);
            Assert.Equal(10, v.X);
            Assert.Equal(20, v.Y);

            p.X += 1;
            p.Y += 2;
            v.X += 1;
            v.Y += 2;
            count++;
        });

        Assert.Equal(1, count);

        Assert.True(e.Read((ref readonly Position p, ref readonly Velocity v) =>
        {
            Assert.Equal(2, p.X);
            Assert.Equal(4, p.Y);

            Assert.Equal(11, v.X);
            Assert.Equal(22, v.Y);
        }));

        Assert.Equal(1, count);
    }

    [Fact]
    private void IterNoComps1Comp()
    {
        using World world = World.Create();

        world.Entity().Add<Position>();
        world.Entity().Add<Position>();
        world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Velocity>();

        using Query<Position> q = world.Query<Position>();

        int count = 0;
        q.Run((Iter it) =>
        {
            while (it.Next())
                count += it.Count();
        });

        Assert.Equal(3, count);
    }

    [Fact]
    private void IterNoComps2Comps()
    {
        using World world = World.Create();

        world.Entity().Add<Velocity>();
        world.Entity().Add<Position>();
        world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Position>().Add<Velocity>();

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        int count = 0;
        q.Run((Iter it) =>
        {
            while (it.Next())
                count += it.Count();
        });

        Assert.Equal(2, count);
    }

    [Fact]
    private void IterNoCompsNoComps()
    {
        using World world = World.Create();

        world.Entity().Add<Velocity>();
        world.Entity().Add<Position>();
        world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Position>().Add<Velocity>();

        using Query q = world.QueryBuilder()
            .With<Position>()
            .Build();

        int count = 0;
        q.Run((Iter it) =>
        {
            while (it.Next())
                count += it.Count();
        });

        Assert.Equal(3, count);
    }

    [Fact]
    private void IterQueryInSystem()
    {
        using World world = World.Create();

        world.Entity().Add<Position>().Add<Velocity>();

        using Query<Velocity> q = world.Query<Velocity>();

        int count = 0;
        world.System<Position>()
            .Each((Entity _, ref Position _) => { q.Each((Entity _, ref Velocity _) => { count++; }); });

        world.Progress();

        Assert.Equal(1, count);
    }

    [Fact]
    private void IterType()
    {
        using World world = World.Create();

        world.Entity().Add<Position>();
        world.Entity().Add<Position>().Add<Velocity>();

        using Query<Position> q = world.Query<Position>();

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Assert.True(it.Type().Count() >= 1);
                Assert.True(it.Table().Has<Position>());
            }
        });
    }

    [Fact]
    private void InstancedQueryWithSingletonEach()
    {
        using World world = World.Create();

        world.Set(new Velocity(1, 2));

        Entity e1 = world.Entity().Set(new Position(10, 20));
        e1.Set(new Self(e1));
        Entity e2 = world.Entity().Set(new Position(20, 30));
        e2.Set(new Self(e2));
        Entity e3 = world.Entity().Set(new Position(30, 40));
        e3.Set(new Self(e3));
        Entity e4 = world.Entity().Set(new Position(40, 50));
        e4.Set(new Self(e4));
        Entity e5 = world.Entity().Set(new Position(50, 60));
        e5.Set(new Self(e5));

        e4.Add<Tag>();
        e5.Add<Tag>();

        using Query<Self, Position, Velocity> q = world.QueryBuilder<Self, Position, Velocity>()
            .TermAt(2).Singleton()
            .Build();

        int count = 0;
        q.Each((Entity e, ref Self s, ref Position p, ref Velocity v) =>
        {
            Assert.True(e == s.Value);
            p.X += v.X;
            p.Y += v.Y;
            count++;
        });

        Assert.Equal(5, count);

        Assert.True(e1.Read((ref readonly Position p) =>
        {
            Assert.Equal(11, p.X);
            Assert.Equal(22, p.Y);
        }));

        Assert.True(e2.Read((ref readonly Position p) =>
        {
            Assert.Equal(21, p.X);
            Assert.Equal(32, p.Y);
        }));

        Assert.True(e3.Read((ref readonly Position p) =>
        {
            Assert.Equal(31, p.X);
            Assert.Equal(42, p.Y);
        }));

        Assert.True(e4.Read((ref readonly Position p) =>
        {
            Assert.Equal(41, p.X);
            Assert.Equal(52, p.Y);
        }));

        Assert.True(e5.Read((ref readonly Position p) =>
        {
            Assert.Equal(51, p.X);
            Assert.Equal(62, p.Y);
        }));
    }

    [Fact]
    private void InstancedQueryWithBaseEach()
    {
        using World world = World.Create();

        Entity @base = world.Entity().Set(new Velocity(1, 2));

        Entity e1 = world.Entity().IsA(@base).Set(new Position(10, 20));
        e1.Set(new Self(e1));
        Entity e2 = world.Entity().IsA(@base).Set(new Position(20, 30));
        e2.Set(new Self(e2));
        Entity e3 = world.Entity().IsA(@base).Set(new Position(30, 40));
        e3.Set(new Self(e3));
        Entity e4 = world.Entity().IsA(@base).Set(new Position(40, 50)).Add<Tag>();
        e4.Set(new Self(e4));
        Entity e5 = world.Entity().IsA(@base).Set(new Position(50, 60)).Add<Tag>();
        e5.Set(new Self(e5));
        Entity e6 = world.Entity().Set(new Position(60, 70)).Set(new Velocity(2, 3));
        e6.Set(new Self(e6));
        Entity e7 = world.Entity().Set(new Position(70, 80)).Set(new Velocity(4, 5));
        e7.Set(new Self(e7));

        using Query<Self, Position, Velocity> q = world.QueryBuilder<Self, Position, Velocity>()
            .Build();

        int count = 0;
        q.Each((Entity e, ref Self s, ref Position p, ref Velocity v) =>
        {
            Assert.True(e == s.Value);
            p.X += v.X;
            p.Y += v.Y;
            count++;
        });

        Assert.Equal(7, count);

        Assert.True(e1.Read((ref readonly Position p) =>
        {
            Assert.Equal(11, p.X);
            Assert.Equal(22, p.Y);
        }));

        Assert.True(e2.Read((ref readonly Position p) =>
        {
            Assert.Equal(21, p.X);
            Assert.Equal(32, p.Y);
        }));

        Assert.True(e3.Read((ref readonly Position p) =>
        {
            Assert.Equal(31, p.X);
            Assert.Equal(42, p.Y);
        }));

        Assert.True(e4.Read((ref readonly Position p) =>
        {
            Assert.Equal(41, p.X);
            Assert.Equal(52, p.Y);
        }));

        Assert.True(e5.Read((ref readonly Position p) =>
        {
            Assert.Equal(51, p.X);
            Assert.Equal(62, p.Y);
        }));

        Assert.True(e6.Read((ref readonly Position p) =>
        {
            Assert.Equal(62, p.X);
            Assert.Equal(73, p.Y);
        }));

        Assert.True(e7.Read((ref readonly Position p) =>
        {
            Assert.Equal(74, p.X);
            Assert.Equal(85, p.Y);
        }));
    }

    [Fact]
    private void InstancedQueryWithSingletonIter()
    {
        using World world = World.Create();

        world.Set(new Velocity(1, 2));

        Entity e1 = world.Entity().Set(new Position(10, 20));
        e1.Set(new Self(e1));
        Entity e2 = world.Entity().Set(new Position(20, 30));
        e2.Set(new Self(e2));
        Entity e3 = world.Entity().Set(new Position(30, 40));
        e3.Set(new Self(e3));
        Entity e4 = world.Entity().Set(new Position(40, 50));
        e4.Set(new Self(e4));
        Entity e5 = world.Entity().Set(new Position(50, 60));
        e5.Set(new Self(e5));

        e4.Add<Tag>();
        e5.Add<Tag>();

        using Query<Self, Position, Velocity> q = world.QueryBuilder<Self, Position, Velocity>()
            .TermAt(2).Singleton()
            .Build();

        int count = 0;
        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Self> s = it.Field<Self>(0);
                Field<Position> p = it.Field<Position>(1);
                Field<Velocity> v = it.Field<Velocity>(2);

                Assert.True(it.Count() > 1);

                foreach (int i in it)
                {
                    p[i].X += v[0].X;
                    p[i].Y += v[0].Y;
                    Assert.True(it.Entity(i) == s[i].Value);
                    count++;
                }
            }
        });

        Assert.Equal(5, count);

        Assert.True(e1.Read((ref readonly Position p) =>
        {
            Assert.Equal(11, p.X);
            Assert.Equal(22, p.Y);
        }));

        Assert.True(e2.Read((ref readonly Position p) =>
        {
            Assert.Equal(21, p.X);
            Assert.Equal(32, p.Y);
        }));

        Assert.True(e3.Read((ref readonly Position p) =>
        {
            Assert.Equal(31, p.X);
            Assert.Equal(42, p.Y);
        }));

        Assert.True(e4.Read((ref readonly Position p) =>
        {
            Assert.Equal(41, p.X);
            Assert.Equal(52, p.Y);
        }));

        Assert.True(e5.Read((ref readonly Position p) =>
        {
            Assert.Equal(51, p.X);
            Assert.Equal(62, p.Y);
        }));
    }

    [Fact]
    private void InstancedQueryWithBaseIter()
    {
        using World world = World.Create();

        Entity @base = world.Entity().Set(new Velocity(1, 2));

        Entity e1 = world.Entity().IsA(@base).Set(new Position(10, 20));
        e1.Set(new Self(e1));
        Entity e2 = world.Entity().IsA(@base).Set(new Position(20, 30));
        e2.Set(new Self(e2));
        Entity e3 = world.Entity().IsA(@base).Set(new Position(30, 40));
        e3.Set(new Self(e3));
        Entity e4 = world.Entity().IsA(@base).Set(new Position(40, 50)).Add<Tag>();
        e4.Set(new Self(e4));
        Entity e5 = world.Entity().IsA(@base).Set(new Position(50, 60)).Add<Tag>();
        e5.Set(new Self(e5));
        Entity e6 = world.Entity().Set(new Position(60, 70)).Set(new Velocity(2, 3));
        e6.Set(new Self(e6));
        Entity e7 = world.Entity().Set(new Position(70, 80)).Set(new Velocity(4, 5));
        e7.Set(new Self(e7));

        using Query<Self, Position, Velocity> q = world.QueryBuilder<Self, Position, Velocity>()
            .Build();

        int count = 0;
        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Self> s = it.Field<Self>(0);
                Field<Position> p = it.Field<Position>(1);
                Field<Velocity> v = it.Field<Velocity>(2);

                Assert.True(it.Count() > 1);

                foreach (int i in it)
                {
                    if (it.IsSelf(2))
                    {
                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                    }
                    else
                    {
                        p[i].X += v[0].X;
                        p[i].Y += v[0].Y;
                    }

                    Assert.True(it.Entity(i) == s[i].Value);
                    count++;
                }
            }
        });

        Assert.Equal(7, count);

        Assert.True(e1.Read((ref readonly Position p) =>
        {
            Assert.Equal(11, p.X);
            Assert.Equal(22, p.Y);
        }));

        Assert.True(e2.Read((ref readonly Position p) =>
        {
            Assert.Equal(21, p.X);
            Assert.Equal(32, p.Y);
        }));

        Assert.True(e3.Read((ref readonly Position p) =>
        {
            Assert.Equal(31, p.X);
            Assert.Equal(42, p.Y);
        }));

        Assert.True(e4.Read((ref readonly Position p) =>
        {
            Assert.Equal(41, p.X);
            Assert.Equal(52, p.Y);
        }));

        Assert.True(e5.Read((ref readonly Position p) =>
        {
            Assert.Equal(51, p.X);
            Assert.Equal(62, p.Y);
        }));

        Assert.True(e6.Read((ref readonly Position p) =>
        {
            Assert.Equal(62, p.X);
            Assert.Equal(73, p.Y);
        }));

        Assert.True(e7.Read((ref readonly Position p) =>
        {
            Assert.Equal(74, p.X);
            Assert.Equal(85, p.Y);
        }));
    }

    [Fact]
    private void QueryEachFromComponent()
    {
        using World world = World.Create();

        world.Entity().Set(new Position()).Set(new Velocity());
        world.Entity().Set(new Position()).Set(new Velocity());

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();
        Entity e = world.Entity().Set(new QueryWrapper(q));

        QueryWrapper* qc = e.GetPtr<QueryWrapper>();
        Assert.True(qc != null);

        int count = 0;
        qc->Query.Each((Entity _, ref Position _, ref Velocity _) => { count++; });
        Assert.Equal(2, count);
    }

    [Fact]
    private void QueryIterFromComponent()
    {
        using World world = World.Create();

        world.Entity().Set(new Position()).Set(new Velocity());
        world.Entity().Set(new Position()).Set(new Velocity());

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();
        Entity e = world.Entity().Set(new QueryWrapper(q));

        QueryWrapper* qc = e.GetPtr<QueryWrapper>();
        Assert.True(qc != null);

        int count = 0;
        qc->Query.Run((Iter it) =>
        {
            while (it.Next())
                count += it.Count();
        });
        Assert.Equal(2, count);
    }

    private static void EachFunc(Entity e, ref Position p)
    {
        InvokedCount++;
        p.X++;
        p.Y++;
    }

    private static void IterFunc(Iter it, Field<Position> p)
    {
        Assert.Equal(1, it.Count());
        InvokedCount++;
        p[0].X++;
        p[0].Y++;
    }

    [Fact]
    private void QueryEachWithFuncPtr()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position(10, 20));

        using Query<Position> q = world.Query<Position>();

        q.Each(&EachFunc);

        Assert.Equal(1, InvokedCount);

        Position* ptr = e.GetPtr<Position>();
        Assert.Equal(11, ptr->X);
        Assert.Equal(21, ptr->Y);
    }

    [Fact]
    private void QueryIterWithFuncPtr()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position(10, 20));

        using Query<Position> q = world.Query<Position>();

        q.Iter(&IterFunc);

        Assert.Equal(1, InvokedCount);

        Position* ptr = e.GetPtr<Position>();
        Assert.Equal(11, ptr->X);
        Assert.Equal(21, ptr->Y);
    }

    [Fact]
    private void QueryEachWithFuncNoPtr()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position(10, 20));

        using Query<Position> q = world.Query<Position>();

        q.Each(EachFunc);

        Assert.Equal(1, InvokedCount);

        Position* ptr = e.GetPtr<Position>();
        Assert.Equal(11, ptr->X);
        Assert.Equal(21, ptr->Y);
    }

    [Fact]
    private void QueryIterWithFuncNoPtr()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position(10, 20));

        using Query<Position> q = world.Query<Position>();

        q.Iter(IterFunc);

        Assert.Equal(1, InvokedCount);

        Position* ptr = e.GetPtr<Position>();
        Assert.Equal(11, ptr->X);
        Assert.Equal(21, ptr->Y);
    }

    [Fact]
    private void QueryEachWithIter()
    {
        using World world = World.Create();

        Entity e1 = world.Entity();
        e1.Set(new Self(e1));
        e1.Set(new Position(10, 20));
        Entity e2 = world.Entity();
        e2.Set(new Self(e2));
        e2.Set(new Position(20, 30));

        using Query<Self, Position> q = world.Query<Self, Position>();

        int invoked = 0;
        q.Each((Iter it, int i, ref Self s, ref Position p) =>
        {
            Assert.Equal(2, it.Count());
            Assert.Equal(s.Value, it.Entity(i));
            p.X++;
            p.Y++;
            invoked++;
        });

        Assert.Equal(2, invoked);

        Position* ptr = e1.GetPtr<Position>();
        Assert.Equal(11, ptr->X);
        Assert.Equal(21, ptr->Y);

        ptr = e2.GetPtr<Position>();
        Assert.Equal(21, ptr->X);
        Assert.Equal(31, ptr->Y);
    }

    // [Fact]
    // private void InvalidFieldFromEachWithIter()
    // {
    //     install_test_abort();
    //
    //     using World world = World.Create();
    //
    //     world.Entity()
    //         .Set(new Position(10, 20))
    //         .Set(new Velocity(1, 2));
    //
    //     using var q = world.QueryBuilder<Position>()
    //         .With<Velocity>().InOut()
    //         .Build();
    //
    //     test_expect_abort();
    //
    //     q.Each((Iter it, int index, ref Position p) => { it.Field(1); });
    // }

    // [Fact]
    // private void InvalidFieldTFromEachWithIter()
    // {
    //     install_test_abort();
    //
    //     using World world = World.Create();
    //
    //     world.Entity()
    //         .Set(new Position(10, 20))
    //         .Set(new Velocity(1, 2));
    //
    //     using var q = world.QueryBuilder<Position>()
    //         .With<Velocity>().InOut()
    //         .Build();
    //
    //     test_expect_abort();
    //
    //     q.Each((Iter it, int index, ref Position p) => { it.Field<Velocity>(1); });
    // }

    // [Fact]
    // private void InvalidFieldConstTFromEachWithIter()
    // {
    //     install_test_abort();
    //
    //     using World world = World.Create();
    //
    //     world.Entity()
    //         .Set(new Position(10, 20))
    //         .Set(new Velocity(1, 2));
    //
    //     using var q = world.QueryBuilder<Position>()
    //         .With<Velocity>().InOut()
    //         .Build();
    //
    //     test_expect_abort();
    //
    //     q.Each((Iter it, int index, ref Position p) => { it.Field<Velocity>(1); });
    // }

    // [Fact]
    // private void FieldAtFromEachWithIter()
    // {
    //     using World world = World.Create();
    //
    //     Entity e1 = world.Entity()
    //         .Set(new Position(10, 20))
    //         .Set(new Velocity(1, 2));
    //
    //     Entity e2 = world.Entity()
    //         .Set(new Position(20, 30))
    //         .Set(new Velocity(3, 4));
    //
    //     using var q = world.QueryBuilder<Position>()
    //         .With<Velocity>().InOut()
    //         .Build();
    //
    //     int count = 0;
    //
    //     q.Each((Iter it, int row, ref Position p) =>
    //     {
    //         Velocity* v = (Velocity*)it.FieldAt(1, row);
    //         if (it.Entity(row) == e1)
    //         {
    //             Assert.Equal(1, v->X);
    //             Assert.Equal(2, v->Y);
    //             count++;
    //         }
    //         else if (it.Entity(row) == e2)
    //         {
    //             Assert.Equal(3, v->X);
    //             Assert.Equal(4, v->Y);
    //             count++;
    //         }
    //     });
    //
    //     Assert.Equal(2, count);
    // }

    [Fact]
    private void FieldAtTFromEachWithIter()
    {
        using World world = World.Create();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        Entity e2 = world.Entity()
            .Set(new Position(20, 30))
            .Set(new Velocity(3, 4));

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>().InOut()
            .Build();

        int count = 0;

        q.Each((Iter it, int row, ref Position _) =>
        {
            ref Velocity v = ref it.FieldAt<Velocity>(1, row);
            if (it.Entity(row) == e1)
            {
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
                count++;
            }
            else if (it.Entity(row) == e2)
            {
                Assert.Equal(3, v.X);
                Assert.Equal(4, v.Y);
                count++;
            }
        });

        Assert.Equal(2, count);
    }

    [Fact]
    private void FieldAtConstTFromEachWithIter()
    {
        using World world = World.Create();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        Entity e2 = world.Entity()
            .Set(new Position(20, 30))
            .Set(new Velocity(3, 4));

        using Query<Position> q = world.QueryBuilder<Position>()
            .With<Velocity>().InOut()
            .Build();

        int count = 0;

        q.Each((Iter it, int row, ref Position _) =>
        {
            ref Velocity v = ref it.FieldAt<Velocity>(1, row);
            if (it.Entity(row) == e1)
            {
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
                count++;
            }
            else if (it.Entity(row) == e2)
            {
                Assert.Equal(3, v.X);
                Assert.Equal(4, v.Y);
                count++;
            }
        });

        Assert.Equal(2, count);
    }

    [Fact]
    private void ChangeTracking()
    {
        using World world = World.Create();

        using Query<Position> qw = world.Query<Position>();
        using Query<Position> qr = world.QueryBuilder<Position>()
            .Cached()
            .Build();

        Entity e1 = world.Entity().Add<Tag>().Set(new Position(10, 20));
        world.Entity().Set(new Position(20, 30));

        Assert.True(qr.Changed());
        qr.Run((Iter it) =>
        {
            while (it.Next())
            {
            }
        });
        Assert.False(qr.Changed());

        int count = 0, changeCount = 0;

        qw.Run((Iter it) =>
        {
            while (it.Next())
            {
                Assert.Equal(1, it.Count());

                count++;

                if (it.Entity(0) == e1)
                {
                    it.Skip();
                    continue;
                }

                changeCount++;
            }
        });

        Assert.Equal(2, count);
        Assert.Equal(1, changeCount);

        count = 0;
        changeCount = 0;

        Assert.True(qr.Changed());

        qr.Run((Iter it) =>
        {
            while (it.Next())
            {
                Assert.Equal(1, it.Count());

                count++;

                if (it.Entity(0) == e1)
                {
                    Assert.False(it.Changed());
                }
                else
                {
                    Assert.True(it.Changed());
                    changeCount++;
                }
            }
        });

        Assert.Equal(2, count);
        Assert.Equal(1, changeCount);
    }

    [Fact]
    private void NotWithWrite()
    {
        using World world = World.Create();

        using Query q = world.QueryBuilder()
            .With<A>()
            .With<B>().Oper(Ecs.Not).Write()
            .Build();

        Entity e = world.Entity().Add<A>();

        int count = 0;
        world.Defer(() =>
        {
            q.Each((Entity ent) =>
            {
                ent.Add<B>();
                count++;
            });
        });

        Assert.Equal(1, count);
        Assert.True(e.Has<B>());

        q.Each((Entity _) => { count++; });

        Assert.Equal(1, count);
    }

    [Fact]
    private void InstancedNestedQueryWithIter()
    {
        using World world = World.Create();

        using Query q1 = world.QueryBuilder()
            .With<Position>()
            .With<Mass>().Singleton().InOut()
            .Build();

        using Query q2 = world.QueryBuilder()
            .With<Velocity>()
            .Build();

        world.Add<Mass>();
        world.Entity().Add<Velocity>();
        world.Entity().Add<Position>();
        world.Entity().Add<Position>();

        int count = 0;

        q2.Run((Iter it2) =>
        {
            while (it2.Next())
            {
                q1.Iter(it2).Run((Iter it1) =>
                {
                    while (it1.Next())
                    {
                        Assert.Equal(2, it1.Count());
                        count += it1.Count();
                    }
                });
            }
        });

        Assert.Equal(2, count);
    }

    [Fact]
    private void InstancedNestedQueryWithEntity()
    {
        using World world = World.Create();

        using Query q1 = world.QueryBuilder()
            .With<Position>()
            .With<Mass>().Singleton().InOut()
            .Build();

        using Query q2 = world.QueryBuilder()
            .With<Velocity>()
            .Build();

        world.Add<Mass>();
        world.Entity().Add<Velocity>();
        world.Entity().Add<Position>();
        world.Entity().Add<Position>();

        int count = 0;

        q2.Each((Entity e2) =>
        {
            q1.Iter(e2).Run((Iter it1) =>
            {
                while (it1.Next())
                {
                    Assert.Equal(2, it1.Count());
                    count += it1.Count();
                }
            });
        });

        Assert.Equal(2, count);
    }

    [Fact]
    private void InstancedNestedQueryWithWorld()
    {
        using World world = World.Create();

        using Query q1 = world.QueryBuilder()
            .With<Position>()
            .With<Mass>().Singleton().InOut()
            .Build();

        using Query q2 = world.QueryBuilder()
            .With<Velocity>()
            .Build();

        world.Add<Mass>();
        world.Entity().Add<Velocity>();
        world.Entity().Add<Position>();
        world.Entity().Add<Position>();

        int count = 0;

        q2.Run((Iter it2) =>
        {
            while (it2.Next())
            {
                q1.Iter(it2.World()).Run((Iter it1) =>
                {
                    while (it1.Next())
                    {
                        Assert.Equal(2, it1.Count());
                        count += it1.Count();
                    }
                });
            }
        });

        Assert.Equal(2, count);
    }

    [Fact]
    private void CapturedQuery()
    {
        using World world = World.Create();

        using Query<Position> q = world.Query<Position>();
        Entity e1 = world.Entity().Set(new Position(10, 20));

        Action action = () =>
        {
            int count = 0;
            q.Each((Entity e, ref Position p) =>
            {
                Assert.True(e == e1);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });
            Assert.Equal(1, count);
        };

        action();
    }

    [Fact]
    private void PageIterCapturedQuery()
    {
        using World world = World.Create();

        using Query<Position> q = world.Query<Position>();
        world.Entity().Set(new Position(10, 20));
        Entity e2 = world.Entity().Set(new Position(20, 30));
        world.Entity().Set(new Position(10, 20));

        Action action = () =>
        {
            int count = 0;
            q.Iter().Page(1, 1).Each((Entity e, ref Position p) =>
            {
                Assert.True(e == e2);
                Assert.Equal(20, p.X);
                Assert.Equal(30, p.Y);
                count++;
            });
            Assert.Equal(1, count);
        };

        action();
    }

    [Fact]
    private void WorkerIterCapturedQuery()
    {
        using World world = World.Create();

        using Query<Position> q = world.Query<Position>();
        world.Entity().Set(new Position(10, 20));
        Entity e2 = world.Entity().Set(new Position(20, 30));
        world.Entity().Set(new Position(10, 20));

        Action action = () =>
        {
            int count = 0;
            q.Iter().Worker(1, 3).Each((Entity e, ref Position p) =>
            {
                Assert.True(e == e2);
                Assert.Equal(20, p.X);
                Assert.Equal(30, p.Y);
                count++;
            });
            Assert.Equal(1, count);
        };

        action();
    }

    [Fact]
    private void SetGroupCapturedQuery()
    {
        using World world = World.Create();

        Entity rel = world.Entity();
        Entity tgtA = world.Entity();
        Entity tgtB = world.Entity();

        using Query<Position> q = world.QueryBuilder<Position>()
            .GroupBy(rel)
            .Build();

        world.Entity().Set(new Position(10, 20)).Add(rel, tgtA);
        Entity e2 = world.Entity().Set(new Position(20, 30)).Add(rel, tgtB);

        new Action(() =>
        {
            int count = 0;
            q.SetGroup(tgtB).Each((Entity e, ref Position p) =>
            {
                Assert.True(e == e2);
                Assert.Equal(20, p.X);
                Assert.Equal(30, p.Y);
                count++;
            });
            Assert.Equal(1, count);
        })();
    }

    [Fact]
    private void SetVarCapturedQuery()
    {
        using World world = World.Create();

        Entity rel = world.Entity();
        Entity tgtA = world.Entity();
        Entity tgtB = world.Entity();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With(rel, "$var")
            .Build();

        world.Entity().Set(new Position(10, 20)).Add(rel, tgtA);
        Entity e2 = world.Entity().Set(new Position(20, 30)).Add(rel, tgtB);

        new Action(() =>
        {
            int count = 0;
            q.SetVar("var", tgtB).Each((Entity e, ref Position p) =>
            {
                Assert.True(e == e2);
                Assert.Equal(20, p.X);
                Assert.Equal(30, p.Y);
                count++;
            });
            Assert.Equal(1, count);
        })();
    }

    [Fact]
    private void SetVarIdCapturedQuery()
    {
        using World world = World.Create();

        Entity rel = world.Entity();
        Entity tgtA = world.Entity();
        Entity tgtB = world.Entity();

        using Query<Position> q = world.QueryBuilder<Position>()
            .With(rel, "$var")
            .Build();

        int var = q.FindVar("var");
        Assert.True(var != -1);

        world.Entity().Set(new Position(10, 20)).Add(rel, tgtA);
        Entity e2 = world.Entity().Set(new Position(20, 30)).Add(rel, tgtB);

        new Action(() =>
        {
            int count = 0;
            q.SetVar(var, tgtB).Each((Entity e, ref Position p) =>
            {
                Assert.True(e == e2);
                Assert.Equal(20, p.X);
                Assert.Equal(30, p.Y);
                count++;
            });
            Assert.Equal(1, count);
        })();
    }

    [Fact]
    private void IterEntities()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Set(new Position(10, 20));
        Entity e2 = world.Entity().Set(new Position(10, 20));
        Entity e3 = world.Entity().Set(new Position(10, 20));

        world.Query<Position>()
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(3, it.Count());

                    Field<ulong> entities = it.Entities();
                    Assert.True(entities[0] == e1);
                    Assert.True(entities[1] == e2);
                    Assert.True(entities[2] == e3);
                }
            });
    }

    [Fact]
    private void IterGetPairWithId()
    {
        using World world = World.Create();

        Entity rel = world.Entity();
        Entity tgt = world.Entity();
        Entity e = world.Entity().Add(rel, tgt);

        using Query q = world.QueryBuilder()
            .With(rel, Ecs.Wildcard)
            .Build();

        int count = 0;

        q.Each((Iter it, int i) =>
        {
            Assert.True(it.Id(0).IsPair());
            Assert.True(it.Id(0).First() == rel);
            Assert.True(it.Id(0).Second() == tgt);
            Assert.True(e == it.Entity(i));
            count++;
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void QueryFromEntity()
    {
        using World world = World.Create();

        Entity qe = world.Entity();
        using Query<Position, Velocity> q1 = world.QueryBuilder<Position, Velocity>(qe)
            .Build();

        world.Entity().Add<Position>();
        Entity e2 = world.Entity().Add<Position>().Add<Velocity>();

        int count = 0;
        q1.Each((Entity e, ref Position _, ref Velocity _) =>
        {
            count++;
            Assert.True(e == e2);
        });
        Assert.Equal(1, count);

        Query q2 = world.Query(qe);
        q2.Each((Entity e) =>
        {
            count++;
            Assert.True(e == e2);
        });
        Assert.Equal(2, count);
    }

    [Fact]
    private void QueryFromEntityName()
    {
        using World world = World.Create();

        using Query<Position, Velocity> q1 = world.QueryBuilder<Position, Velocity>("qe")
            .Build();

        world.Entity().Add<Position>();
        Entity e2 = world.Entity().Add<Position>().Add<Velocity>();

        int count = 0;
        q1.Each((Entity e, ref Position _, ref Velocity _) =>
        {
            count++;
            Assert.True(e == e2);
        });
        Assert.Equal(1, count);

        Query q2 = world.Query("qe");
        q2.Each((Entity e) =>
        {
            count++;
            Assert.True(e == e2);
        });
        Assert.Equal(2, count);
    }

    [Fact]
    private void RunWithIterFini()
    {
        using World world = World.Create();

        using Query<Position> q1 = world.Query<Position>();

        int count = 0;
        q1.Run((Iter it) =>
        {
            it.Fini();
            count++;
        });

        Assert.Equal(1, count);
    }


    [Fact]
    private void RunWithIterFiniInterrupt()
    {
        using World world = World.Create();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Add<Foo>();
        world.Entity()
            .Set(new Position(10, 20))
            .Add<Bar>();
        world.Entity()
            .Set(new Position(10, 20))
            .Add<Hello>();

        using Query<Position> q = world.Query<Position>();

        int count = 0;
        q.Run((Iter it) =>
        {
            Assert.True(it.Next());
            Assert.Equal(1, it.Count());
            Assert.Equal(e1, it.Entity(0));

            Assert.True(it.Next());
            count++;
            it.Fini();
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void RunWithIterFiniEmpty()
    {
        using World world = World.Create();

        using Query<Position> q = world.Query<Position>();

        int count = 0;
        q.Run((Iter it) =>
        {
            count++;
            it.Fini();
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void RunWithIterFiniNoQuery()
    {
        using World world = World.Create();

        using Query q = world.Query();

        int count = 0;
        q.Run((Iter it) =>
        {
            count++;
            it.Fini();
        });

        Assert.Equal(1, count);
    }

    [Fact]
    private void AddToMatchFromStagedQuery()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity e = world.Entity().Add<Position>();

        World stage = world.GetStage(0);

        world.ReadonlyBegin(false);

        stage.Query<Position>()
            .Each((Entity e, ref Position _) =>
            {
                e.Add<Velocity>();
                Assert.True(!e.Has<Velocity>());
            });

        world.ReadonlyEnd();

        Assert.True(e.Has<Position>());
        Assert.True(e.Has<Velocity>());
    }

    [Fact]
    private void AddToMatchFromStagedQueryReadonlyThreaded()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        Entity e = world.Entity().Add<Position>();

        World stage = world.GetStage(0);

        world.ReadonlyBegin(true);

        stage.Query<Position>()
            .Each((Entity e, ref Position _) =>
            {
                e.Add<Velocity>();
                Assert.True(!e.Has<Velocity>());
            });

        world.ReadonlyEnd();

        Assert.True(e.Has<Position>());
        Assert.True(e.Has<Velocity>());
    }

    [Fact]
    private void EmptyTablesEach()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();
        world.Component<Tag>();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        Entity e2 = world.Entity()
            .Set(new Position(20, 30))
            .Set(new Velocity(2, 3));

        e2.Add<Tag>();
        e2.Remove<Tag>();

        using Query<Position, Velocity> q = world.QueryBuilder<Position, Velocity>()
            .QueryFlags(EcsQueryMatchEmptyTables)
            .Build();

        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        {
            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }
        {
            Position* p = e2.GetPtr<Position>();
            Assert.Equal(22, p->X);
            Assert.Equal(33, p->Y);
        }
    }

    // TODO: Fix later
    // [Fact]
    // private void EmptyTablesEachWithEntity()
    // {
    //     using World world = World.Create();
    //
    //     world.Component<Position>();
    //     world.Component<Velocity>();
    //     world.Component<Tag>();
    //
    //     Entity e1 = world.Entity()
    //         .Set(new Position(10, 20))
    //         .Set(new Velocity(1, 2));
    //
    //     Entity e2 = world.Entity()
    //         .Set(new Position(20, 30))
    //         .Set(new Velocity(2, 3));
    //
    //     e2.Add<Tag>();
    //     e2.Remove<Tag>();
    //
    //     using var q = world.QueryBuilder<Position, Velocity>()
    //         .QueryFlags(EcsQueryMatchEmptyTables)
    //         .Build();
    //
    //     q.Each((Entity e, ref Position p, ref Velocity v) =>
    //     {
    //         p.X += v.X;
    //         p.Y += v.Y;
    //     });
    //
    //     {
    //         Position* p = e1.GetPtr<Position>();
    //         Assert.Equal(11, p->X);
    //         Assert.Equal(22, p->Y);
    //     }
    //     {
    //         Position* p = e2.GetPtr<Position>();
    //         Assert.Equal(22, p->X);
    //         Assert.Equal(33, p->Y);
    //     }
    // }

    [Fact]
    private void EmptyTablesEachWithIter()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();
        world.Component<Tag>();

        Entity e1 = world.Entity()
            .Set(new Position(10, 20))
            .Set(new Velocity(1, 2));

        Entity e2 = world.Entity()
            .Set(new Position(20, 30))
            .Set(new Velocity(2, 3));

        e2.Add<Tag>();
        e2.Remove<Tag>();

        using Query<Position, Velocity> q = world.QueryBuilder<Position, Velocity>()
            .QueryFlags(EcsQueryMatchEmptyTables)
            .Build();

        q.Each((Iter _, int _, ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        {
            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }
        {
            Position* p = e2.GetPtr<Position>();
            Assert.Equal(22, p->X);
            Assert.Equal(33, p->Y);
        }
    }

    [Fact]
    private void PairWithVariableSrc()
    {
        using World world = World.Create();

        world.Component<RelData>();
        world.Component<ThisComp>();
        world.Component<OtherComp>();

        Entity other = world.Entity()
            .Set(new OtherComp(10));

        for (int i = 0; i < 3; i++)
            world.Entity()
                .Set(new ThisComp(i))
                .Add<RelData>(other);

        using Query<RelData, ThisComp, OtherComp> q = world.QueryBuilder<RelData, ThisComp, OtherComp>()
            .TermAt(0).Second("$other")
            .TermAt(2).Src("$other")
            .Build();

        int isPresent = 0;
        q.Each((ref RelData _, ref ThisComp thisComp, ref OtherComp otherComp) =>
        {
            isPresent |= 1 << thisComp.X;
            Assert.Equal(10, otherComp.X);
        });

        Assert.Equal(7, isPresent);
    }

    [Fact]
    private void PairWithVariableSrcNoRowFields()
    {
        using World world = World.Create();

        world.Component<RelData>();
        world.Component<ThisComp>();
        world.Component<OtherComp>();

        Entity other = world.Entity()
            .Set(new OtherComp(0));

        world.Entity()
            .Set(new OtherComp(1));

        for (int i = 0; i < 3; i++)
            world.Entity()
                .Set(new ThisComp(i))
                .Add<RelData>(other);

        using Query<RelData, ThisComp, OtherComp> q = world.QueryBuilder<RelData, ThisComp, OtherComp>()
            .TermAt(0).Second("$other")
            .TermAt(2).Src("$other")
            .Build();

        int isPresent = 0;
        q.Each((ref RelData _, ref ThisComp thisComp, ref OtherComp otherComp) =>
        {
            isPresent |= 1 << thisComp.X;
            Assert.Equal(0, otherComp.X);
        });

        Assert.Equal(7, isPresent);
    }

    [Fact]
    private void IterTargets()
    {
        using World world = World.Create();

        Entity likes = world.Entity();
        Entity pizza = world.Entity();
        Entity salad = world.Entity();
        Entity alice = world.Entity()
            .Add(likes, pizza)
            .Add(likes, salad);

        using Query q = world.QueryBuilder()
            .With(likes, Ecs.Any)
            .Build();

        int count = 0;
        int tgtCount = 0;

        q.Each((Iter it, int row) =>
        {
            Entity e = it.Entity(row);
            Assert.True(e == alice);

            it.Targets(0, (Entity tgt) =>
            {
                if (tgtCount == 0)
                    Assert.True(tgt == pizza);
                if (tgtCount == 1)
                    Assert.True(tgt == salad);
                tgtCount++;
            });

            count++;
        });

        Assert.Equal(1, count);
        Assert.Equal(2, tgtCount);
    }

    [Fact]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void IterTargets2ndField()
    {
        using World world = World.Create();

        Entity likes = world.Entity();
        Entity pizza = world.Entity();
        Entity salad = world.Entity();
        Entity alice = world.Entity()
            .Add<Position>()
            .Add(likes, pizza)
            .Add(likes, salad);

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With(likes, Ecs.Any)
            .Build();

        int count = 0;
        int tgtCount = 0;

        q.Each((Iter it, int row) =>
        {
            Entity e = it.Entity(row);
            Assert.True(e == alice);

            it.Targets(1, (Entity tgt) =>
            {
                if (tgtCount == 0)
                    Assert.True(tgt == pizza);
                if (tgtCount == 1)
                    Assert.True(tgt == salad);
                tgtCount++;
            });

            count++;
        });

        Assert.Equal(1, count);
        Assert.Equal(2, tgtCount);
    }

    [Fact]
    private void IterTargetsFieldOutOfRange()
    {
        using World world = World.Create();

        Entity likes = world.Entity();
        Entity pizza = world.Entity();
        Entity salad = world.Entity();
        Entity alice = world.Entity()
            .Add(likes, pizza)
            .Add(likes, salad);

        using Query q = world.QueryBuilder()
            .With(likes, Ecs.Any)
            .Build();

        q.Each((Iter it, int row) =>
        {
            Entity e = it.Entity(row);
            Assert.True(e == alice);

            Assert.Throws<Ecs.AssertionException>(() =>
            {
                it.Targets(1, (Entity _) => { });
            });
        });
    }

    [Fact]
    private void IterTargetsFieldNotAPair()
    {
        using World world = World.Create();

        Entity likes = world.Entity();
        Entity pizza = world.Entity();
        Entity salad = world.Entity();
        Entity alice = world.Entity()
            .Add<Position>()
            .Add(likes, pizza)
            .Add(likes, salad);

        using Query q = world.QueryBuilder()
            .With<Position>()
            .Build();

        q.Each((Iter it, int row) =>
        {
            Entity e = it.Entity(row);
            Assert.True(e == alice);

            Assert.Throws<Ecs.AssertionException>(() =>
            {
                it.Targets(1, (Entity _) => { });
            });
        });
    }

    [Fact]
    private void IterTargetsFieldNotSet()
    {
        using World world = World.Create();

        Entity likes = world.Entity();
        Entity alice = world.Entity()
            .Add<Position>();

        using Query q = world.QueryBuilder()
            .With<Position>()
            .With(likes, Ecs.Any).Optional()
            .Build();

        q.Each((Iter it, int row) =>
        {
            Entity e = it.Entity(row);
            Assert.True(e == alice);
            Assert.True(!it.IsSet(1));

            Assert.Throws<Ecs.AssertionException>(() =>
            {
                it.Targets(1, (Entity _) => { });
            });
        });
    }
}
