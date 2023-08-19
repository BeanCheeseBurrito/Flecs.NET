using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public class ObserverTests
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
                filterBuilder: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnRemove),
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
                filterBuilder: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnSet),
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
                filterBuilder: world.FilterBuilder().Term<Position>().Term<Velocity>(),
                observerBuilder: world.ObserverBuilder().Event(EcsUnSet),
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
                filterBuilder: world.FilterBuilder()
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
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder()
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
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder().Term<Position>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnSet),
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
        //
        // void Observer_2_entities_table_column() {
        //     using World world = World.Create();
        //
        //     Entity e1 = world.Entity();
        //     Entity e2 = world.Entity();
        //
        //     int count = 0;
        //     Entity last;
        //
        //     world.observer<const Position>()
        //         .Event(flecs::OnSet)
        //         .iter([&](flecs::iter& it) {
        //             auto p = it.range().get<Position>();
        //
        //             for (auto i : it) {
        //                 count ++;
        //                 if (it.entity(i) == e1) {
        //                     test_int(p[i].x, 10);
        //                     test_int(p[i].y, 20);
        //                 } else if (it.entity(i) == e2) {
        //                     test_int(p[i].x, 30);
        //                     test_int(p[i].y, 40);
        //                 } else {
        //                     Assert.True(false);
        //                 }
        //
        //                 last = it.entity(i);
        //             }
        //         });
        //
        //     e1.Set(new Position() { X = 10, Y = 20 });
        //     Assert.Equal(1, count);
        //     Assert.True(last == e1);
        //
        //     e2.Set(new Position() { X = 30, Y = 40 });
        //     Assert.Equal(2, count);
        //     Assert.True(last == e2);
        // }

        [Fact]
        private void YieldExisting()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<TagA>();
            Entity e2 = world.Entity().Add<TagA>();
            Entity e3 = world.Entity().Add<TagA>().Add<TagB>();

            int count = 0;

            world.Observer(
                filterBuilder: world.FilterBuilder().Term<TagA>(),
                observerBuilder: world.ObserverBuilder()
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
                filterBuilder: world.FilterBuilder()
                    .Term<TagA>()
                    .Term<TagB>(),
                observerBuilder: world.ObserverBuilder()
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
                filterBuilder: world.FilterBuilder().Term<TagA>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder().Term<TagA>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
                callback: it => { }
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
                filterBuilder: world.FilterBuilder().Term<Position>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder().Term<Position>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnRemove),
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
                filterBuilder: world.FilterBuilder().Term<MyTag>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder().Term<MyTag>(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder().Expr("Tag"),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
                filterBuilder: world.FilterBuilder()
                    .Term(tagA)
                    .Term(tagB).Filter(),
                observerBuilder: world.ObserverBuilder().Event(EcsOnAdd),
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
        //
        // void Observer_run_callback() {
        //     using World world = World.Create();
        //
        //     int count = 0;
        //
        //     world.observer<Position>()
        //         .Event(EcsOnAdd)
        //         .run([](flecs::iter_t *it) {
        //             while (ecs_iter_next(it)) {
        //                 it->callback(it);
        //             }
        //         })
        //         .each([&](Position& p) {
        //             count ++;
        //         });
        //
        //     Entity e = world.Entity();
        //     Assert.Equal(0, count);
        //
        //     e.Set(new Position() { X = 10, Y = 20 });
        //     Assert.Equal(1, count);
        // }
        // TODO: This is crashing when ecs_fini is called on world. Investigate this later.
        // [Fact]
        // private void GetFilter() {
        //     using World world = World.Create();
        //
        //     world.Entity().Set(new Position() { X = 0, Y = 0 });
        //     world.Entity().Set(new Position() { X = 1, Y = 0 });
        //     world.Entity().Set(new Position() { X = 2, Y = 0 });
        //
        //     int count = 0;
        //
        //     Observer observer = world.Observer(
        //         filterBuilder: world.FilterBuilder().Term<Position>(),
        //         observerBuilder: world.ObserverBuilder().Event(EcsOnSet),
        //         callback: it => { }
        //     );
        //
        //     Filter filter = observer.Filter();
        //
        //     filter.Iter(it =>
        //     {
        //         Column<Position> pos = it.Field<Position>(1);
        //
        //         foreach (int i in it) {
        //             Assert.Equal(i, pos[i].X);
        //             count++;
        //         }
        //     });
        //
        //     Assert.Equal(3, count);
        // }
    }
}