using System;
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Tests.Cpp;

public class ObserverTests
{
    [Fact]
    private void _2TermsOnAdd()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position, Velocity>()
            .Event(Ecs.OnAdd)
            .Each((ref Position _, ref Velocity _) => { count++; });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(0, count);

        e.Set(new Velocity { X = 1, Y = 2 });
        Assert.Equal(1, count);
    }

    [Fact]
    private void _2TermsOnRemove()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position, Velocity>()
            .Event(Ecs.OnRemove)
            .Each((ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(0, count);

        e.Set(new Velocity { X = 1, Y = 2 });
        Assert.Equal(0, count);

        e.Remove<Velocity>();
        Assert.Equal(1, count);

        e.Remove<Position>();
        Assert.Equal(1, count);
    }

    [Fact]
    private void _2TermsOnSet()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position, Velocity>()
            .Event(Ecs.OnSet)
            .Each((ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(0, count);

        e.Set(new Velocity { X = 1, Y = 2 });
        Assert.Equal(1, count);
    }

    [Fact]
    private void _2TermsUnSet()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position, Velocity>()
            .Event(Ecs.OnRemove)
            .Each((ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(0, count);

        e.Set(new Velocity { X = 1, Y = 2 });
        Assert.Equal(0, count);

        e.Remove<Velocity>();
        Assert.Equal(1, count);

        e.Remove<Position>();
        Assert.Equal(1, count);
    }

    [Fact]
    private void _10Terms()
    {
        using World world = World.Create();

        int count = 0;

        Entity e = world.Entity();

        world.Observer()
            .Event(Ecs.OnAdd)
            .With<Tag0>()
            .With<Tag1>()
            .With<Tag2>()
            .With<Tag3>()
            .With<Tag4>()
            .With<Tag5>()
            .With<Tag6>()
            .With<Tag7>()
            .With<Tag8>()
            .With<Tag9>()
            .Each((Iter it, int _) =>
            {
                Assert.Equal(1, it.Count());
                Assert.Equal(e, it.Entity(0));
                Assert.Equal(10, it.FieldCount());
                count++;
            });

        e.Add<Tag0>()
            .Add<Tag1>()
            .Add<Tag2>()
            .Add<Tag3>()
            .Add<Tag4>()
            .Add<Tag5>()
            .Add<Tag6>()
            .Add<Tag7>()
            .Add<Tag8>()
            .Add<Tag9>();

        Assert.Equal(1, count);
    }

    [Fact]
    private void _16Terms()
    {
        using World world = World.Create();

        int count = 0;

        Entity e = world.Entity();

        world.Observer()
            .Event(Ecs.OnAdd)
            .With<Tag0>()
            .With<Tag1>()
            .With<Tag2>()
            .With<Tag3>()
            .With<Tag4>()
            .With<Tag5>()
            .With<Tag6>()
            .With<Tag7>()
            .With<Tag8>()
            .With<Tag9>()
            .With<Tag10>()
            .With<Tag11>()
            .With<Tag12>()
            .With<Tag13>()
            .With<Tag14>()
            .With<Tag15>()
            .Each((Iter it, int _) =>
            {
                Assert.Equal(1, it.Count());
                Assert.Equal(e, it.Entity(0));
                Assert.Equal(16, it.FieldCount());
                count++;
            });

        e.Add<Tag0>()
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
            .Add<Tag15>();

        Assert.Equal(1, count);
    }

    [Fact]
    private void _2EntitiesIter()
    {
        using World world = World.Create();

        Entity e1 = world.Entity();
        Entity e2 = world.Entity();

        int count = 0;
        Entity last = default;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);

                    foreach (int i in it)
                    {
                        count++;
                        if (it.Entity(i) == e1)
                        {
                            Assert.Equal(10, p[i].X);
                            Assert.Equal(20, p[i].Y);
                        }
                        else if (it.Entity(i) == e2)
                        {
                            Assert.Equal(30, p[i].X);
                            Assert.Equal(40, p[i].Y);
                        }
                        else
                        {
                            Assert.True(false);
                        }

                        last = it.Entity(i);
                    }
                }
            });

        e1.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
        Assert.True(last == e1);

        e2.Set(new Position { X = 30, Y = 40 });
        Assert.Equal(2, count);
        Assert.True(last == e2);
    }

    [Fact]
    private void _2EntitiesTableColumn()
    {
        using World world = World.Create();

        Entity e1 = world.Entity();
        Entity e2 = world.Entity();

        int count = 0;
        Entity last = default;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Range().Get<Position>();

                    foreach (int i in it)
                    {
                        count++;

                        if (it.Entity(i) == e1)
                        {
                            Assert.Equal(10, p[i].X);
                            Assert.Equal(20, p[i].Y);
                        }
                        else if (it.Entity(i) == e2)
                        {
                            Assert.Equal(30, p[i].X);
                            Assert.Equal(40, p[i].Y);
                        }
                        else
                        {
                            Assert.True(false);
                        }

                        last = it.Entity(i);
                    }
                }
            });

        e1.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
        Assert.True(last == e1);

        e2.Set(new Position { X = 30, Y = 40 });
        Assert.Equal(2, count);
        Assert.True(last == e2);
    }

    [Fact]
    private void _2EntitiesEach()
    {
        using World world = World.Create();

        Entity e1 = world.Entity();
        Entity e2 = world.Entity();

        int count = 0;
        Entity last = default;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Entity e, ref Position p) =>
            {
                count++;

                if (e == e1)
                {
                    Assert.Equal(10, p.X);
                    Assert.Equal(20, p.Y);
                }
                else if (e == e2)
                {
                    Assert.Equal(30, p.X);
                    Assert.Equal(40, p.Y);
                }
                else
                {
                    Assert.True(false);
                }

                last = e;
            });

        e1.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
        Assert.True(last == e1);

        e2.Set(new Position { X = 30, Y = 40 });
        Assert.Equal(2, count);
        Assert.True(last == e2);
    }

    [Fact]
    private void CreateWithNoTemplateArgs()
    {
        using World world = World.Create();

        Entity e1 = world.Entity();

        int count = 0;

        world.Observer()
            .With<Position>()
            .Event(Ecs.OnAdd)
            .Each((Entity e) =>
            {
                Assert.True(e == e1);
                count++;
            });

        e1.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
    }

    [Fact]
    private void YieldExisting()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Tag0>();
        Entity e2 = world.Entity().Add<Tag0>();
        Entity e3 = world.Entity().Add<Tag0>().Add<Tag1>();

        int count = 0;

        world.Observer()
            .With<Tag0>()
            .Event(Ecs.OnAdd)
            .YieldExisting()
            .Each((Entity e) =>
            {
                if (e == e1)
                    count++;
                if (e == e2)
                    count += 2;
                if (e == e3)
                    count += 3;
            });

        Assert.Equal(6, count);
    }

    [Fact]
    private void YieldExisting2Terms()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Tag0>().Add<Tag1>();
        Entity e2 = world.Entity().Add<Tag0>().Add<Tag1>();
        Entity e3 = world.Entity().Add<Tag0>().Add<Tag1>().Add<Tag2>();
        world.Entity().Add<Tag0>();
        world.Entity().Add<Tag1>();

        int count = 0;

        world.Observer()
            .With<Tag0>()
            .With<Tag1>()
            .Event(Ecs.OnAdd)
            .YieldExisting()
            .Each((Entity e) =>
            {
                if (e == e1)
                    count++;
                if (e == e2)
                    count += 2;
                if (e == e3)
                    count += 3;
            });

        Assert.Equal(6, count);
    }

    [Fact]
    private void YieldExistingOnCreateFlag()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Tag0>();
        Entity e2 = world.Entity().Add<Tag0>();
        Entity e3 = world.Entity().Add<Tag0>().Add<Tag1>();

        int count = 0;

        Observer o = world.Observer()
            .With<Tag0>()
            .Event(Ecs.OnAdd)
            .Event(Ecs.OnRemove)
            .ObserverFlags(EcsObserverYieldOnCreate)
            .Each((Entity e) =>
            {
                if (e == e1)
                    count++;
                if (e == e2)
                    count += 2;
                if (e == e3)
                    count += 3;
            });

        Assert.Equal(6, count);

        o.Destruct();

        Assert.Equal(6, count);
    }

    [Fact]
    private void YieldExistingOnDeleteFlag()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Tag0>();
        Entity e2 = world.Entity().Add<Tag0>();
        Entity e3 = world.Entity().Add<Tag0>().Add<Tag1>();

        int count = 0;

        Observer o = world.Observer()
            .With<Tag0>()
            .Event(Ecs.OnAdd)
            .Event(Ecs.OnRemove)
            .ObserverFlags(EcsObserverYieldOnDelete)
            .Each((Entity e) =>
            {
                if (e == e1)
                    count++;
                if (e == e2)
                    count += 2;
                if (e == e3)
                    count += 3;
            });

        Assert.Equal(0, count);

        o.Destruct();

        Assert.Equal(6, count);
    }

    [Fact]
    private void YieldExistingOnCreateDeleteFlag()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Tag0>();
        Entity e2 = world.Entity().Add<Tag0>();
        Entity e3 = world.Entity().Add<Tag0>().Add<Tag1>();

        int count = 0;

        Observer o = world.Observer()
            .With<Tag0>()
            .Event(Ecs.OnAdd)
            .Event(Ecs.OnRemove)
            .ObserverFlags(EcsObserverYieldOnCreate | EcsObserverYieldOnDelete)
            .Each((Entity e) =>
            {
                if (e == e1)
                    count++;
                if (e == e2)
                    count += 2;
                if (e == e3)
                    count += 3;
            });

        Assert.Equal(6, count);

        o.Destruct();

        Assert.Equal(12, count);
    }

    [Fact]
    private void DefaultCtor()
    {
        using World world = World.Create();

        Observer observer = default;
        Assert.True(observer == 0);

        int count = 0;

        observer = world.Observer()
            .With<Tag0>()
            .Event(Ecs.OnAdd)
            .Each((Entity _) => { count++; });

        Assert.True(observer != 0);

        world.Entity().Add<Tag0>();

        Assert.Equal(1, count);
    }

    [Fact]
    private void EntityCtor()
    {
        using World world = World.Create();

        Observer observer = world.Observer()
            .With<Tag0>()
            .Event(Ecs.OnAdd)
            .Each((Entity _) => { });

        ulong entity = observer;

        Observer entityObserver = world.Observer(entity);
        Assert.True(entityObserver == observer);
    }

    [Fact]
    private void OnAdd()
    {
        using World world = World.Create();

        int invoked = 0;

        world.Observer<Position>()
            .Event(Ecs.OnAdd)
            .Each((Entity _, ref Position _) => { invoked++; });

        world.Entity()
            .Add<Position>();

        Assert.Equal(1, invoked);
    }

    [Fact]
    private void OnRemove()
    {
        using World world = World.Create();

        int invoked = 0;

        world.Observer<Position>()
            .Event(Ecs.OnRemove)
            .Each((Entity _, ref Position _) => { invoked++; });

        Entity e = world.Entity()
            .Add<Position>();

        Assert.Equal(0, invoked);

        e.Remove<Position>();

        Assert.Equal(1, invoked);
    }

    [Fact]
    private void OnAddTagAction()
    {
        using World world = World.Create();

        int invoked = 0;

        world.Observer()
            .With<MyTag>()
            .Event(Ecs.OnAdd)
            .Run((Iter it) =>
            {
                while (it.Next())
                    invoked++;
            });

        world.Entity()
            .Add<MyTag>();

        Assert.Equal(1, invoked);
    }

    [Fact]
    private void OnAddTagIter()
    {
        using World world = World.Create();

        int invoked = 0;

        world.Observer()
            .With<MyTag>()
            .Event(Ecs.OnAdd)
            .Run((Iter it) =>
            {
                while (it.Next())
                    invoked++;
            });

        world.Entity()
            .Add<MyTag>();

        Assert.Equal(1, invoked);
    }

    [Fact]
    private void OnAddTagEach()
    {
        using World world = World.Create();

        int invoked = 0;

        world.Observer()
            .With<MyTag>()
            .Event(Ecs.OnAdd)
            .Each((Entity _) => { invoked++; });

        world.Entity()
            .Add<MyTag>();

        Assert.Equal(1, invoked);
    }

    [Fact]
    private void OnAddExpr()
    {
        using World world = World.Create();

        int invoked = 0;

        world.Component<Tag>();

        world.Observer()
            .Expr("Tag")
            .Event(Ecs.OnAdd)
            .Each((Entity _) => { invoked++; });

        Entity e = world.Entity().Add<Tag>();

        Assert.Equal(1, invoked);

        e.Remove<Tag>();

        Assert.Equal(1, invoked);
    }

    [Fact]
    private void WithFilterTerm()
    {
        using World world = World.Create();

        Entity tagA = world.Entity();
        Entity tagB = world.Entity();

        int invoked = 0;

        world.Observer()
            .With(tagA)
            .With(tagB).Filter()
            .Event(Ecs.OnAdd)
            .Each((Entity _) => { invoked++; });

        Entity e = world.Entity();
        Assert.Equal(0, invoked);

        e.Add(tagB);
        Assert.Equal(0, invoked);

        e.Add(tagA);
        Assert.Equal(1, invoked);

        e.Remove(tagB);
        Assert.Equal(1, invoked);

        e.Add(tagB);
        Assert.Equal(1, invoked);

        e.Clear();
        Assert.Equal(1, invoked);

        e.Add(tagA);
        Assert.Equal(1, invoked);
    }

    [Fact]
    private void RunCallback()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position>()
            .Event(EcsOnAdd)
            .Run((Iter it, Action<Iter> each) =>
            {
                while (it.Next())
                    each(it);
            })
            .Each((ref Position _) => { count++; });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
    }

    [Fact]
    private void RunCallbackWith1Field()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Assert.Equal(10, p[0].X);
                    Assert.Equal(20, p[0].Y);

                    count++;
                }
            });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position(10, 20));
        Assert.Equal(1, count);
    }

    [Fact]
    private void RunCallbackWith2Fields()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position, Velocity>()
            .Event(Ecs.OnSet)
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Field<Velocity> v = it.Field<Velocity>(1);

                    Assert.Equal(10, p[0].X);
                    Assert.Equal(20, p[0].Y);
                    Assert.Equal(1, v[0].X);
                    Assert.Equal(2, v[0].Y);

                    count++;
                }
            });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position(10, 20));
        Assert.Equal(0, count);

        e.Set(new Velocity(1, 2));
        Assert.Equal(1, count);
    }

    [Fact]
    private void RunCallbackWithYieldExisting1Field()
    {
        using World world = World.Create();

        int count = 0;
        world.Entity().Set(new Position(10, 20));

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .YieldExisting()
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);

                    Assert.Equal(10, p[0].X);
                    Assert.Equal(20, p[0].Y);

                    count++;
                }
            });

        Assert.Equal(1, count);
    }

    [Fact]
    private void RunCallbackWithYieldExisting2Fields()
    {
        using World world = World.Create();

        int count = 0;
        world.Entity().Set(new Position(10, 20)).Set(new Velocity(1, 2));

        world.Observer<Position, Velocity>()
            .Event(Ecs.OnSet)
            .YieldExisting()
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Field<Velocity> v = it.Field<Velocity>(1);

                    Assert.Equal(10, p[0].X);
                    Assert.Equal(20, p[0].Y);
                    Assert.Equal(1, v[0].X);
                    Assert.Equal(2, v[0].Y);

                    count++;
                }
            });

        Assert.Equal(1, count);
    }

    [Fact]
    private void GetQuery()
    {
        using World world = World.Create();

        world.Entity().Set(new Position { X = 0, Y = 0 });
        world.Entity().Set(new Position { X = 1, Y = 0 });
        world.Entity().Set(new Position { X = 2, Y = 0 });

        int count = 0;

        Observer<Position> observer = world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Entity _, ref Position _) => { });

        Query<Position> query = observer.Query();

        query.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> pos = it.Field<Position>(0);

                foreach (int i in it)
                {
                    Assert.Equal(i, pos[i].X);
                    count++;
                }
            }
        });

        Assert.Equal(3, count);
    }

    [Fact]
    private void OnSetWithSet()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Entity _, ref Position _) => { count++; });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
    }

    [Fact]
    private void OnSetWithDeferSet()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Entity _, ref Position _) => { count++; });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        world.DeferBegin();
        e.Set(new Position { X = 10, Y = 20 });

        Assert.Equal(0, count);
        world.DeferEnd();

        Assert.Equal(1, count);
    }

    [Fact]
    private void OnSetWithSetSetSparse()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.Sparse);

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Entity _, ref Position _) => { count++; });

        Entity e = world.Entity();
        Assert.Equal(0, count);

        e.Set(new Position(10, 20));
        Assert.Equal(1, count);
    }

    [Fact]
    private void OnAddSingleton()
    {
        using World world = World.Create();

        int count = 0;

        world.Observer<Position>()
            .TermAt(0).Singleton()
            .Event(Ecs.OnSet)
            .Each((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

        world.Set(new Position { X = 10, Y = 20 });

        Assert.Equal(1, count);
    }

    [Fact]
    private void OnAddPairSingleton()
    {
        using World world = World.Create();

        int count = 0;

        Entity tgt = world.Entity();

        world.Observer<Position>()
            .TermAt(0).Second(tgt).Singleton()
            .Event(Ecs.OnSet)
            .Each((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

        world.Set(tgt, new Position { X = 10, Y = 20 });

        Assert.Equal(1, count);
    }

    [Fact]
    private void OnAddPairWildcardSingleton()
    {
        using World world = World.Create();

        int count = 0;

        Entity tgt1 = world.Entity();
        Entity tgt2 = world.Entity();

        world.Observer<Position>()
            .TermAt(0).Second(Ecs.Wildcard).Singleton()
            .Event(Ecs.OnSet)
            .Each((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

        world.Set(tgt1, new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);

        world.Set(tgt2, new Position { X = 10, Y = 20 });
        Assert.Equal(2, count);
    }

    [Fact]
    private void OnAddWithPairSingleton()
    {
        using World world = World.Create();

        int count = 0;

        Entity tgt = world.Entity();

        world.Observer()
            .With<Position>(tgt).Singleton()
            .Event(Ecs.OnSet)
            .Each((Entity _) => { count++; });

        world.Set(tgt, new Position { X = 10, Y = 20 });
        Assert.Equal(1, count);
    }

    [Fact]
    private void AddInYieldExisting()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Set(default(Position));
        Entity e2 = world.Entity().Set(default(Position));
        Entity e3 = world.Entity().Set(default(Position));

        world.Observer<Position>()
            .Event(Ecs.OnAdd)
            .YieldExisting()
            .Each((Entity e, ref Position _) => { e.Add<Velocity>(); });

        Assert.True(e1.Has<Position>());
        Assert.True(e1.Has<Velocity>());

        Assert.True(e2.Has<Position>());
        Assert.True(e2.Has<Velocity>());

        Assert.True(e3.Has<Position>());
        Assert.True(e3.Has<Velocity>());
    }

    [Fact]
    private void AddInYieldExistingMulti()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Set(default(Position)).Set(default(Mass));
        Entity e2 = world.Entity().Set(default(Position)).Set(default(Mass));
        Entity e3 = world.Entity().Set(default(Position)).Set(default(Mass));

        world.Observer<Position, Mass>()
            .Event(Ecs.OnAdd)
            .YieldExisting()
            .Each((Entity e, ref Position _, ref Mass _) => { e.Add<Velocity>(); });

        Assert.True(e1.Has<Position>());
        Assert.True(e1.Has<Mass>());
        Assert.True(e1.Has<Velocity>());

        Assert.True(e2.Has<Position>());
        Assert.True(e2.Has<Mass>());
        Assert.True(e2.Has<Velocity>());

        Assert.True(e3.Has<Position>());
        Assert.True(e3.Has<Mass>());
        Assert.True(e3.Has<Velocity>());
    }

    [Fact]
    private void NameFromRoot()
    {
        using World world = World.Create();

        Entity sys = world.Observer<Position>(".ns.MySystem")
            .Event(Ecs.OnSet)
            .Each((ref Position _) => { });

        Assert.Equal("MySystem", sys.Name());

        Entity ns = world.Entity(".ns");
        Assert.True(ns == sys.Parent());
    }

    [Fact]
    private void ImplicitRegisterInEmitForNamedEntity()
    {
        using World world = World.Create();

        Entity e1 = world.Entity("e1");
        Entity e2 = world.Entity();

        e1.Observe((ref MyEvent evt) =>
        {
            Assert.Equal(10, evt.Value);
            e2.Set(new Position(10, 20));
        });

        e1.Emit(new MyEvent(10));
    }

    [Fact]
    private void AddToNamedInEmitForNamedEntity()
    {
        using World world = World.Create();

        world.Component<Position>();

        Entity e1 = world.Entity("e1");
        Entity e2 = world.Entity("e2");

        e1.Observe((ref MyEvent evt) =>
        {
            Assert.Equal(10, evt.Value);
            e2.Set(new Position(10, 20));
        });

        e1.Emit(new MyEvent(10));
    }

    [Fact]
    private void ImplicitRegisterInEmitForNamedEntityWithDefer()
    {
        using World world = World.Create();

        Entity e1 = world.Entity("e1");
        Entity e2 = world.Entity();

        e1.Observe((ref MyEvent evt) =>
        {
            Assert.Equal(10, evt.Value);
            e2.Set(new Position(10, 20));
        });

        world.DeferBegin();
        e1.Emit(new MyEvent(10));
        world.DeferEnd();
    }

    [Fact]
    private void AddToNamedInEmitForNamedEntityWithDefer()
    {
        using World world = World.Create();

        world.Component<Position>();

        Entity e1 = world.Entity("e1");
        Entity e2 = world.Entity("e2");

        e1.Observe((ref MyEvent evt) =>
        {
            Assert.Equal(10, evt.Value);
            e2.Set(new Position(10, 20));
        });

        world.DeferBegin();
        e1.Emit(new MyEvent(10));
        world.DeferEnd();
    }

    [Fact]
    private void RegisterTwiceWithEach()
    {
        using World world = World.Create();

        int count1 = 0;
        int count2 = 0;

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Each((ref Position _) => { count1++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count1);

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Each((ref Position _) => { count2++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count2);
    }

    [Fact]
    private void RegisterTwiceWithRun()
    {
        using World world = World.Create();

        int count1 = 0;
        int count2 = 0;

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Run((Iter _) => { count1++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count1);

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Run((Iter _) => { count2++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count2);
    }

    [Fact]
    private void RegisterTwiceWithRunEach()
    {
        using World world = World.Create();

        int count1 = 0;
        int count2 = 0;

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Run((Iter _) => { count1++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count1);

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Each((ref Position _) => { count2++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count2);
    }

    [Fact]
    private void RegisterTwiceWithEachRun()
    {
        using World world = World.Create();

        int count1 = 0;
        int count2 = 0;

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Each((ref Position _) => { count1++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count1);

        world.Observer<Position>("Test")
            .Event(Ecs.OnSet)
            .Run((Iter _) => { count2++; });

        world.Entity().Set(new Position(10, 20));
        Assert.Equal(1, count2);
    }

    [Fact]
    private void OtherTable()
    {
        using World world = World.Create();

        int count = 0;
        Entity matched = default;

        world.Observer<Velocity>()
            .Event(Ecs.OnAdd)
            .Each((Iter it, int row, ref Velocity _) =>
            {
                Assert.True(it.Table().Has<Velocity>());
                Assert.True(!it.OtherTable().Has<Velocity>());
                matched = it.Entity(row);
                count++;
            });

        Entity e = world.Entity().Add<Position>().Add<Velocity>();
        Assert.True(e == matched);
        Assert.Equal(1, count);
    }

    [Fact]
    private void OtherTableWithPair()
    {
        using World world = World.Create();

        int count = 0;
        Entity matched = default;

        world.Observer()
            .With<Likes, Apples>()
            .Event(Ecs.OnAdd)
            .Each((Iter it, int row) =>
            {
                Assert.True((it.Table().Has<Likes, Apples>()));
                Assert.True((!it.OtherTable().Has<Likes, Apples>()));
                matched = it.Entity(row);
                count++;
            });

        Entity e = world.Entity().Add<Position>().Add<Likes, Apples>();
        Assert.True(e == matched);
        Assert.Equal(1, count);
    }

    [Fact]
    private void OtherTableWithPairWildcard()
    {
        using World world = World.Create();

        int count = 0;
        Entity matched = default;

        world.Observer()
            .With<Likes, Apples>()
            .Event(Ecs.OnAdd)
            .Each((Iter it, int row) =>
            {
                Assert.True((it.Table().Has<Likes>(Ecs.Wildcard)));
                Assert.True((!it.OtherTable().Has<Likes>(Ecs.Wildcard)));
                matched = it.Entity(row);
                count++;
            });

        Entity e = world.Entity().Add<Position>().Add<Likes, Apples>();
        Assert.True(e == matched);
        Assert.Equal(1, count);
    }

    [Fact]
    private void OnAddInherited()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        int count = 0;
        Entity matched = default;

        world.Observer<Position>()
            .Event(Ecs.OnAdd)
            .Each((Entity e, ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
                matched = e;
            });

        Entity p = world.Prefab().Set(new Position(10, 20));
        Assert.Equal(0, count);

        Entity i = world.Entity().IsA(p);
        Assert.Equal(1, count);
        Assert.True(i == matched);
    }

    [Fact]
    private void OnSetInherited()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        int count = 0;
        Entity matched = default;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Entity e, ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
                matched = e;
            });

        Entity p = world.Prefab().Set(new Position(10, 20));
        Assert.Equal(0, count);

        Entity i = world.Entity().IsA(p);
        Assert.Equal(1, count);
        Assert.True(i == matched);
    }

    [Fact]
    private void OnRemoveInherited()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        int count = 0;
        Entity matched = default;

        world.Observer<Position>()
            .Event(Ecs.OnRemove)
            .Each((Entity e, ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
                matched = e;
            });

        Entity p = world.Prefab().Set(new Position(10, 20));
        Assert.Equal(0, count);

        Entity i = world.Entity().IsA(p);
        Assert.Equal(0, count);

        p.Remove<Position>();
        Assert.Equal(1, count);
        Assert.True(i == matched);
    }

    [Fact]
    private void OnSetAfterRemoveOverride()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        Entity @base = world.Prefab()
            .Set(new Position(1, 2));

        Entity e1 = world.Entity().IsA(@base)
            .Set(new Position(10, 20));

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Iter it, int row, ref Position p) =>
            {
                Assert.True(it.Entity(row) == e1);
                Assert.True(it.Src(0) == @base);
                Assert.Equal(1, p.X);
                Assert.Equal(2, p.Y);
                count++;
            });

        e1.Remove<Position>();

        Assert.Equal(1, count);
    }

    [Fact]
    private void OnSetAfterRemoveOverrideCreateObserverBefore()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        int count = 0;

        Entity @base = world.Prefab();
        Entity e1 = world.Entity();

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Iter it, int row, ref Position _) =>
            {
                Assert.True(it.Entity(row) == e1);
                Assert.True(it.Src(0) == @base);
                count++;
            });

        @base.Set(new Position(1, 2));
        e1.Add<Position>().IsA(@base);

        Assert.Equal(0, count);

        e1.Remove<Position>();

        Assert.Equal(1, count);
    }

    [Fact]
    private void OnSetWithOverrideAfterDelete()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        Entity @base = world.Prefab()
            .Set(new Position(1, 2));

        Entity e1 = world.Entity().IsA(@base)
            .Set(new Position(10, 20));

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Iter _, int _, ref Position _) =>
            {
                count++;
            });

        e1.Destruct();

        Assert.Equal(0, count);
    }

    [Fact]
    private void OnSetWithOverrideAfterClear()
    {
        using World world = World.Create();

        world.Component<Position>().Add(Ecs.OnInstantiate, Ecs.Inherit);

        Entity @base = world.Prefab()
            .Set(new Position(1, 2));

        Entity e1 = world.Entity().IsA(@base)
            .Set(new Position(10, 20));

        int count = 0;

        world.Observer<Position>()
            .Event(Ecs.OnSet)
            .Each((Iter _, int _, ref Position _) =>
            {
                count++;
            });

        e1.Clear();

        Assert.Equal(0, count);
    }
}
