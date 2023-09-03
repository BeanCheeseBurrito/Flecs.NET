using System.Runtime.InteropServices;
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class ObserverTests
    {
        public ObserverTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void OnAdd2Terms()
        {
            using World world = World.Create();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { count += it.Count(); }
            );

            Entity e = world.Entity();
            Assert.Equal(0, count);

            e.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(0, count);

            e.Set(new Velocity() { X = 1, Y = 2 });
            Assert.Equal(1, count);
        }

        [Fact]
        private void OnRemove2Terms()
        {
            using World world = World.Create();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observer: world.ObserverBuilder().Event(EcsOnRemove),
                callback: it =>
                {
                    Column<Position> p = it.Field<Position>(1);
                    Column<Velocity> v = it.Field<Velocity>(2);

                    foreach (int i in it)
                    {
                        count++;
                        Assert.Equal(10, p[i].X);
                        Assert.Equal(20, p[i].Y);
                        Assert.Equal(1, v[i].X);
                        Assert.Equal(2, v[i].Y);
                    }
                }
            );

            Entity e = world.Entity();
            Assert.Equal(0, count);

            e.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(0, count);

            e.Set(new Velocity() { X = 1, Y = 2 });
            Assert.Equal(0, count);

            e.Remove<Velocity>();
            Assert.Equal(1, count);

            e.Remove<Position>();
            Assert.Equal(1, count);
        }

        [Fact]
        private void OnSet2Terms()
        {
            using World world = World.Create();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observer: world.ObserverBuilder().Event(EcsOnSet),
                callback: it =>
                {
                    Column<Position> p = it.Field<Position>(1);
                    Column<Velocity> v = it.Field<Velocity>(2);

                    foreach (int i in it)
                    {
                        count++;
                        Assert.Equal(10, p[i].X);
                        Assert.Equal(20, p[i].Y);
                        Assert.Equal(1, v[i].X);
                        Assert.Equal(2, v[i].Y);
                    }
                }
            );

            Entity e = world.Entity();
            Assert.Equal(0, count);

            e.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(0, count);

            e.Set(new Velocity() { X = 1, Y = 2 });
            Assert.Equal(1, count);
        }

        [Fact]
        private void OnUnset2Terms()
        {
            using World world = World.Create();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observer: world.ObserverBuilder().Event(EcsUnSet),
                callback: it =>
                {
                    Column<Position> p = it.Field<Position>(1);
                    Column<Velocity> v = it.Field<Velocity>(2);

                    foreach (int i in it)
                    {
                        count++;
                        Assert.Equal(10, p[i].X);
                        Assert.Equal(20, p[i].Y);
                        Assert.Equal(1, v[i].X);
                        Assert.Equal(2, v[i].Y);
                    }
                }
            );

            Entity e = world.Entity();
            Assert.Equal(0, count);

            e.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(0, count);

            e.Set(new Velocity() { X = 1, Y = 2 });
            Assert.Equal(0, count);

            e.Remove<Velocity>();
            Assert.Equal(1, count);

            e.Remove<Position>();
            Assert.Equal(1, count);
        }

        [Fact]
        private void OnAdd10Terms()
        {
            using World world = World.Create();

            int count = 0;

            Entity e = world.Entity();

            world.Observer(
                filter: world.FilterBuilder()
                    .Term<TagA>()
                    .Term<TagB>()
                    .Term<TagC>()
                    .Term<TagD>()
                    .Term<TagE>()
                    .Term<TagF>()
                    .Term<TagG>()
                    .Term<TagH>()
                    .Term<TagI>()
                    .Term<TagJ>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it =>
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.Equal(10, it.FieldCount());
                    count++;
                }
            );

            e.Add<TagA>()
                .Add<TagB>()
                .Add<TagC>()
                .Add<TagD>()
                .Add<TagE>()
                .Add<TagF>()
                .Add<TagG>()
                .Add<TagH>()
                .Add<TagI>()
                .Add<TagJ>();

            Assert.Equal(1, count);
        }

        [Fact]
        private void OnAdd20Terms()
        {
            using World world = World.Create();

            int count = 0;

            Entity e = world.Entity();

            world.Observer(
                filter: world.FilterBuilder()
                    .Term<TagA>()
                    .Term<TagB>()
                    .Term<TagC>()
                    .Term<TagD>()
                    .Term<TagE>()
                    .Term<TagF>()
                    .Term<TagG>()
                    .Term<TagH>()
                    .Term<TagI>()
                    .Term<TagJ>()
                    .Term<TagK>()
                    .Term<TagL>()
                    .Term<TagM>()
                    .Term<TagN>()
                    .Term<TagO>()
                    .Term<TagP>()
                    .Term<TagQ>()
                    .Term<TagR>()
                    .Term<TagS>()
                    .Term<TagT>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it =>
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.Equal(20, it.FieldCount());
                    count++;
                }
            );

            e.Add<TagA>()
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

            Assert.Equal(1, count);
        }

        [Fact]
        private void Iter2Entities()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            int count = 0;
            Entity last = default;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnSet),
                callback: it =>
                {
                    Column<Position> p = it.Field<Position>(1);

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
            );

            e1.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(1, count);
            Assert.True(last == e1);

            e2.Set(new Position() { X = 30, Y = 40 });
            Assert.Equal(2, count);
            Assert.True(last == e2);
        }

        [Fact]
        private void TableColumn2Entities()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            int count = 0;
            Entity last = default;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnSet),
                callback: it =>
                {
                    Column<Position> p = it.Range().Get<Position>();

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
            );

            e1.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(1, count);
            Assert.True(last == e1);

            e2.Set(new Position() { X = 30, Y = 40 });
            Assert.Equal(2, count);
            Assert.True(last == e2);
        }

        [Fact]
        private void YieldExisting()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<TagA>();
            Entity e2 = world.Entity().Add<TagA>();
            Entity e3 = world.Entity().Add<TagA>().Add<TagB>();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<TagA>(),
                observer: world.ObserverBuilder()
                    .Event(EcsOnAdd)
                    .YieldExisting(),
                callback: it =>
                {
                    foreach (int i in it)
                    {
                        Entity e = it.Entity(i);

                        if (e == e1)
                            count++;
                        if (e == e2)
                            count += 2;
                        if (e == e3)
                            count += 3;
                    }
                }
            );

            Assert.Equal(6, count);
        }

        [Fact]
        private void YieldExisting2Terms()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<TagA>().Add<TagB>();
            Entity e2 = world.Entity().Add<TagA>().Add<TagB>();
            Entity e3 = world.Entity().Add<TagA>().Add<TagB>().Add<TagC>();
            world.Entity().Add<TagA>();
            world.Entity().Add<TagB>();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder()
                    .Term<TagA>()
                    .Term<TagB>(),
                observer: world.ObserverBuilder()
                    .Event(EcsOnAdd)
                    .YieldExisting(),
                callback: it =>
                {
                    foreach (int i in it)
                    {
                        Entity e = it.Entity(i);

                        if (e == e1)
                            count++;
                        if (e == e2)
                            count += 2;
                        if (e == e3)
                            count += 3;
                    }
                }
            );

            Assert.Equal(6, count);
        }

        [Fact]
        private void DefaultCtor()
        {
            using World world = World.Create();

            Observer observer = default;
            Assert.True(observer == 0);

            int count = 0;

            observer = world.Observer(
                filter: world.FilterBuilder().Term<TagA>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { count += it.Count(); }
            );

            Assert.True(observer != 0);

            world.Entity().Add<TagA>();

            Assert.Equal(1, count);
        }

        [Fact]
        private void EntityCtor()
        {
            using World world = World.Create();

            Observer observer = world.Observer(
                filter: world.FilterBuilder().Term<TagA>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: _ => { }
            );

            ulong entity = observer;

            Observer entityObserver = world.Observer(entity);
            Assert.True(entityObserver == observer);
        }

        [Fact]
        private void OnAdd()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { invoked += it.Count(); }
            );

            world.Entity()
                .Add<Position>();

            Assert.Equal(1, invoked);
        }

        [Fact]
        private void OnRemove()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnRemove),
                callback: it => { invoked += it.Count(); }
            );

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

            world.Observer(
                filter: world.FilterBuilder().Term<MyTag>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { invoked += it.Count(); }
            );

            world.Entity()
                .Add<MyTag>();

            Assert.Equal(1, invoked);
        }

        [Fact]
        private void OnAddTagIter()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<MyTag>(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { invoked += it.Count(); }
            );

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

            world.Observer(
                filter: world.FilterBuilder().Expr("Tag"),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { invoked += it.Count(); }
            );

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

            world.Observer(
                filter: world.FilterBuilder()
                    .Term(tagA)
                    .Term(tagB).Filter(),
                observer: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { invoked += it.Count(); }
            );

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

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder()
                    .Event(EcsOnAdd)
                    .Run(it =>
                    {
                        while (ecs_iter_next(it) == 1)
                        {
#if NET5_0_OR_GREATER
                            ((delegate* unmanaged<ecs_iter_t*, void>)it->callback)(it);
#else
                            Marshal.GetDelegateForFunctionPointer<Ecs.IterAction>(it->callback)(it);
#endif
                        }
                    }),
                callback: it => { count += it.Count(); }
            );

            Entity e = world.Entity();
            Assert.Equal(0, count);

            e.Set(new Position() { X = 10, Y = 20 });
            Assert.Equal(1, count);
        }

        [Fact]
        private void GetFilter()
        {
            using World world = World.Create();

            world.Entity().Set(new Position() { X = 0, Y = 0 });
            world.Entity().Set(new Position() { X = 1, Y = 0 });
            world.Entity().Set(new Position() { X = 2, Y = 0 });

            int count = 0;

            Observer observer = world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnSet),
                callback: _ => { }
            );

            using Filter filter = observer.Filter();

            filter.Iter(it =>
            {
                Column<Position> pos = it.Field<Position>(1);
                foreach (int i in it)
                {
                    Assert.Equal(i, pos[i].X);
                    count++;
                }
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void OnSetWithSet()
        {
            using World world = World.Create();

            int count = 0;

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnSet),
                callback: it => { count += it.Count(); }
            );

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

            world.Observer(
                filter: world.FilterBuilder().Term<Position>(),
                observer: world.ObserverBuilder().Event(EcsOnSet),
                callback: it => { count += it.Count(); }
            );

            Entity e = world.Entity();
            Assert.Equal(0, count);

            world.DeferBegin();
            e.Set(new Position { X = 10, Y = 20 });

            Assert.Equal(0, count);
            world.DeferEnd();

            Assert.Equal(1, count);
        }
    }
}
