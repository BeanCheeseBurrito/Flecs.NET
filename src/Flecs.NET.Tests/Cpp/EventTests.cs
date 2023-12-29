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

            world.Observer()
                .Event(evt)
                .Term(id)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term(idA)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

            world.Observer()
                .Event(evt)
                .Term(idB)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term(id)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term(idA)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

            world.Observer()
                .Event(evt)
                .Term(idB)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event<Evt>()
                .Term(id)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event<Evt>()
                .Term<IdA>()
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event<Evt>()
                .Term<IdA>()
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

            world.Observer()
                .Event<Evt>()
                .Term<IdB>()
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term(id)
                .Iter((Iter it) =>
                {
                    Assert.True(it.Entity(0) == e1);
                    Assert.Equal(10, it.Param<EvtData>().Value);
                    count++;
                });

            EvtData data = new EvtData { Value = 10 };

            world.Event(evt)
                .Id(id)
                .Entity(e1)
                .Ctx(&data)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EventTypedContext()
        {
            using World world = World.Create();

            Entity id = world.Entity();
            Entity e1 = world.Entity().Add(id);

            int count = 0;

            world.Observer()
                .Event<EvtData>()
                .Term(id)
                .Iter((Iter it) =>
                {
                    Assert.True(it.Entity(0) == e1);
                    Assert.Equal(10, it.Param<EvtData>().Value);
                    count++;
                });

            EvtData evtData = new EvtData { Value = 10 };

            world.Event<EvtData>()
                .Id(id)
                .Entity(e1)
                .Ctx(&evtData)
                .Emit();

            Assert.Equal(1, count);
        }

        [Fact]
        public void Event1IdPairRelIdObjIdEntity()
        {
            using World world = World.Create();

            Entity evt = world.Entity();
            Entity rel = world.Entity();
            Entity obj = world.Entity();
            Entity e1 = world.Entity().Add(rel, obj);

            int count = 0;

            world.Observer()
                .Event(evt)
                .Term(rel, obj)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term<IdA>(obj)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term<IdA, IdB>()
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term<Tag>()
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event(evt)
                .Term<Tag>()
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

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

            world.Observer()
                .Event<Evt>()
                .Term(Ecs.Any).Src(e1)
                .Each((Entity e) =>
                {
                    Assert.True(e == 0);
                    countA++;
                });

            world.Observer()
                .Event<Evt>()
                .Term(Ecs.Any).Src(e2)
                .Each((Entity e) =>
                {
                    Assert.True(e == 0);
                    countB++;
                });

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

        [Fact]
        public void EnqueueEvent()
        {
            using World world = World.Create();

            int count = 0;

            Entity evt = world.Entity();
            Entity idA = world.Entity();
            Entity e1 = world.Entity().Add(idA);

            world.Observer()
                .Event(evt)
                .Term(idA)
                .Each((Entity e) =>
                {
                    Assert.True(e == e1);
                    count++;
                });

            world.DeferBegin();

            world.Event(evt)
                .Id(idA)
                .Entity(e1)
                .Enqueue();

            Assert.Equal(0, count);

            world.DeferEnd();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EnqueueEntityEvent()
        {
            using World world = World.Create();

            int count = 0;

            Entity evt = world.Entity();
            Entity idA = world.Entity();
            Entity e1 = world.Entity().Add(idA);

            e1.Observe(evt, () => { count++; });

            world.DeferBegin();

            e1.Enqueue(evt);

            Assert.Equal(0, count);

            world.DeferEnd();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EnqueueEventWithPayload()
        {
            using World world = World.Create();

            int count = 0;

            Entity idA = world.Entity();
            Entity e1 = world.Entity().Add(idA);

            world.Observer()
                .Event<Position>()
                .Term(idA)
                .Each((Iter it, int i) =>
                {
                    Assert.True(it.Entity(i) == e1);
                    Assert.Equal(10, it.Param<Position>().X);
                    Assert.Equal(20, it.Param<Position>().Y);
                    count++;
                });

            world.DeferBegin();

            Position position = new Position { X = 10, Y = 20 };

            world.Event<Position>()
                .Id(idA)
                .Entity(e1)
                .Ctx(&position)
                .Enqueue();

            Assert.Equal(0, count);

            world.DeferEnd();

            Assert.Equal(1, count);
        }

        [Fact]
        public void EnqueueEntityEventWithPayload()
        {
            using World world = World.Create();

            int count = 0;

            Entity idA = world.Entity();
            Entity e1 = world.Entity().Add(idA);

            e1.Observe((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                count++;
            });

            world.DeferBegin();

            e1.Enqueue(new Position { X = 10, Y = 20 });

            Assert.Equal(0, count);

            world.DeferEnd();

            Assert.Equal(1, count);
        }
    }
}
