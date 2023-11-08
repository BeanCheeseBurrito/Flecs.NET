using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class EventTests
    {
        public EventTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        public void Event1IdEntity()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity id = world.Entity();
            Entity e1 = world.Entity().Add(id);

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(id),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id(id)
                .Entity(e1)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event2IdsEntity()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity idA = world.Entity();
            Entity idB = world.Entity();
            Entity e1 = world.Entity().Add(idA).Add(idB);

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(idA),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Observer(
                world.FilterBuilder().Term(idB),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id(idA)
                .Id(idB)
                .Entity(e1)
                .Emit();

            Assert.Equal(2, count);
        }

        [Fact]
        public void Event1IdTable()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity id = world.Entity();
            Entity e1 = world.Entity().Add(id);

            Table table = e1.Table();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(id),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id(id)
                .Table(table.Handle)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event2IdsTable()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity idA = world.Entity();
            Entity idB = world.Entity();
            Entity e1 = world.Entity().Add(idA).Add(idB);
            Table table = e1.Table();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(idA),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Observer(
                world.FilterBuilder().Term(idB),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id(idA)
                .Id(idB)
                .Table(table.Handle)
                .Emit();

            Assert.Equal(2, count);
        }

        [Fact]
        public void EventType()
        {
            using World world = World.Create();

            Entity id = world.Entity();
            Entity e1 = world.Entity().Add(id);

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(id),
                world.ObserverBuilder().Event<Evt>(),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event<Evt>()
                .Id(id)
                .Entity(e1)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event1Component()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<IdA>();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term<IdA>(),
                world.ObserverBuilder().Event<Evt>(),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event<Evt>()
                .Id<IdA>()
                .Entity(e1)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event2Components()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<IdA>().Add<IdB>();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term<IdA>(),
                world.ObserverBuilder().Event<Evt>(),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Observer(
                world.FilterBuilder().Term<IdB>(),
                world.ObserverBuilder().Event<Evt>(),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event<Evt>()
                .Id<IdA>()
                .Id<IdB>()
                .Entity(e1)
                .Emit();

            Assert.Equal(2, count);
        }

        [Fact]
        public void EventVoidContext()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity id = world.Entity();
            Entity e1 = world.Entity().Add(id);

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(id),
                world.ObserverBuilder().Event(evt),
                (Iter it) =>
                {
                    Assert.True(it.Entity(0) == e1);
                    Assert.Equal(10, it.Param<EvtData>().Value);
                    count++;
                }
            );

            EvtData data = new EvtData { Value = 10 };

            world.Event(evt)
                .Id(id)
                .Entity(e1)
                .Ctx(&data)
                .Emit();

            Assert.Equal(1, count);
        }

        // TODO: Come back to this test once ctx function is done.
        // [Fact]
        // public void EventTypedContext()
        // {
        //     using World world = World.Create();
        //
        //     var id = world.Entity();
        //     var e1 = world.Entity().Add(id);
        //
        //     int count = 0;
        //
        //     world.Observer(
        //         filter: world.FilterBuilder().Term(id),
        //         observer: world.ObserverBuilder().Event<EvtData>(),
        //         callback: (Iter it) =>
        //         {
        //             Assert.True(it.Entity(0) == e1);
        //             Assert.Equal(10, it.Param<EvtData>().Value);
        //             count++;
        //         }
        //     );
        //
        //     world.Event<EvtData>()
        //         .Id(id)
        //         .Entity(e1)
        //         .Ctx(new EvtData { Value = 10 })
        //         .Emit();
        //
        //     Assert.Equal(1, count);
        // }

        // [Fact]
        // public void evt_implicit_typed_ctx() {
        //     using World world = World.Create();
        //
        //     var id = world.Entity();
        //     var e1 = world.Entity().Add(id);
        //
        //     int count = 0;
        //
        //     world.Observer()
        //         .Event<EvtData>()
        //         .term(id)
        //         .iter((flecs::iter& it) {
        //             Assert.True(it.Entity(0) == e1);
        //             Assert.Equal(it.param<EvtData>()->value, 10);
        //             count++;
        //         });
        //
        //     world.Event<EvtData>()
        //         .Id(id)
        //         .Entity(e1)
        //         .ctx({10})
        //         .Emit();
        //
        //     Assert.Equal(1, count);
        // }

        [Fact]
        public void Event1IdPairRelIdObjIdEntity()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity rel = world.Entity();
            Entity obj = world.Entity();
            Entity e1 = world.Entity().Add(rel, obj);

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term(rel, obj),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id(rel, obj)
                .Entity(e1)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event1IdPairRelObjIdEntity()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity obj = world.Entity();
            Entity e1 = world.Entity().Add<IdA>(obj);

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term<IdA>(obj),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id<IdA>(obj)
                .Entity(e1)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event1IdPairRelObjEntity()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity e1 = world.Entity().Add<IdA, IdB>();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term<IdA, IdB>(),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.Event(evt)
                .Id<IdA, IdB>()
                .Entity(e1)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EmitStagedFromWorld()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity e1 = world.Entity().Add<Tag>();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term<Tag>(),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.ReadonlyBegin();

            world.Event(evt)
                .Id<Tag>()
                .Entity(e1)
                .Emit();

            world.ReadonlyEnd();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EmitStagedFromStage()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity e1 = world.Entity().Add<Tag>();

            int count = 0;

            world.Observer(
                world.FilterBuilder().Term<Tag>(),
                world.ObserverBuilder().Event(evt),
                (Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                }
            );

            world.ReadonlyBegin();

            world.GetStage(0).Event(evt)
                .Id<Tag>()
                .Entity(e1)
                .Emit();

            world.ReadonlyEnd();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EmitCustomForAny()
        {
            using World world = World.Create();

            int countA = 0;
            int countB = 0;

            Entity e1 = world.Entity().Add<Tag>();
            Entity e2 = world.Entity().Add<Tag>();

            world.Observer(
                world.FilterBuilder().With(EcsAny).Src(e1),
                world.ObserverBuilder().Event<Evt>(),
                (Entity e) =>
                {
                    Assert.True(e == 0);
                    countA++;
                }
            );

            world.Observer(
                world.FilterBuilder().With(EcsAny).Src(e2),
                world.ObserverBuilder().Event<Evt>(),
                (Entity e) =>
                {
                    Assert.True(e == 0);
                    countB++;
                }
            );

            world.Event<Evt>()
                .Id(EcsAny)
                .Entity(e1)
                .Emit();

            Assert.Equal(1, countA);
            Assert.Equal(0, countB);

            world.Event<Evt>()
                .Id(EcsAny)
                .Entity(e2)
                .Emit();

            Assert.Equal(1, countA);
            Assert.Equal(1, countB);
        }

        [Fact]
        public void EntityEmitEventId()
        {
            using World world = World.Create();

            Entity evt = world.Entity();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe(evt, (Entity src) =>
            {
                count++;
                Assert.True(src == e);
            });

            Assert.Equal(0, count);

            e.Emit(evt);

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventType()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe<Evt>((Entity src) =>
            {
                count++;
                Assert.True(src == e);
            });

            Assert.Equal(0, count);

            e.Emit<Evt>();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventWithPayload()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe((Entity src, ref Position p) =>
            {
                count++;
                Assert.True(src == e);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });

            Assert.Equal(0, count);

            e.Emit(new Position { X = 10, Y = 20 });

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventIdNoSrc()
        {
            using World world = World.Create();

            Entity evt = world.Entity();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe(evt, () => { count++; });

            Assert.Equal(0, count);

            e.Emit(evt);

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventTypeNoSrc()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe<Evt>(() => { count++; });

            Assert.Equal(0, count);

            e.Emit<Evt>();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventWithPayloadNoSrc()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe((ref Position p) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });

            Assert.Equal(0, count);

            e.Emit(new Position { X = 10, Y = 20 });

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventWithPayloadDerivedEventType()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe((Entity src, ref Position p) =>
            {
                count++;
                Assert.True(src == e);
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });

            Assert.Equal(0, count);

            e.Emit(new Position { X = 10, Y = 20 });

            Assert.Equal(1, count);
        }

        [Fact]
        public void EntityEmitEventWithPayloadDerivedEventTypeNoSrc()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe((ref Position p) =>
            {
                count++;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });

            Assert.Equal(0, count);

            e.Emit(new Position { X = 10, Y = 20 });

            Assert.Equal(1, count);
        }
    }
}
