using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Tests.Cpp;

public unsafe class PairTests
{
    [Fact]
    private void AddComponentPair()
    {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Add<Pair, Position>();

            Assert.True(entity.Id != 0);
            Assert.True(entity.Has<Pair, Position>());
            Assert.True(!entity.Has<Position, Pair>());

            Assert.Equal("(Pair,Position)", entity.Type().Str());
        }

    [Fact]
    private void AddTagPair()
    {
            using World world = World.Create();

            world.Component<Position>();
            Entity pair = world.Entity("Pair");

            Entity entity = world.Entity()
                .AddSecond<Position>(pair);

            Assert.True(entity.Id != 0);
            Assert.True(entity.HasSecond<Position>(pair));
            Assert.True(!entity.Has<Position>(pair));
            Assert.Equal("(Pair,Position)", entity.Type().Str());
        }

    [Fact]
    private void AddTagPairToTag()
    {
            using World world = World.Create();

            Entity tag = world.Entity("Tag");
            Entity pair = world.Entity("Pair");

            Entity entity = world.Entity()
                .Add(pair, tag);

            Assert.True(entity.Id != 0);
            Assert.True(entity.Has(pair, tag));
            Assert.Equal("(Pair,Tag)", entity.Type().Str());
        }

    [Fact]
    private void RemoveComponentPair()
    {
            using World world = World.Create();

            world.Component<Position>();
            world.Component<Pair>();

            Entity entity = world.Entity()
                .Add<Pair, Position>();

            Assert.True(entity.Id != 0);
            Assert.True(entity.Has<Pair, Position>());
            Assert.True(!entity.Has<Position, Pair>());

            Assert.Equal("(Pair,Position)", entity.Type().Str());

            entity.Remove<Position, Pair>();
            Assert.True(!entity.Has<Position, Pair>());
        }

    [Fact]
    private void RemoveTagPair()
    {
            using World world = World.Create();

            world.Component<Position>();
            Entity pair = world.Entity("Pair");

            Entity entity = world.Entity()
                .AddSecond<Position>(pair);

            Assert.True(entity.Id != 0);
            Assert.True(entity.HasSecond<Position>(pair));
            Assert.True(!entity.Has<Position>(pair));
            Assert.Equal("(Pair,Position)", entity.Type().Str());

            entity.Remove<Position>(pair);
            Assert.True(!entity.Has<Position>(pair));
        }

    [Fact]
    private void RemoveTagPairToTag()
    {
            using World world = World.Create();

            Entity tag = world.Entity("Tag");
            Entity pair = world.Entity("Pair");

            Entity entity = world.Entity()
                .Add(pair, tag);

            Assert.True(entity.Id != 0);
            Assert.True(entity.Has(pair, tag));
            Assert.Equal("(Pair,Tag)", entity.Type().Str());

            entity.Remove(tag, pair);
            Assert.True(!entity.Has(tag, pair));
        }

    [Fact]
    private void SetComponentPair()
    {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set<Pair, Position>(new Pair { Value = 10 });

            Assert.True(world.Component<Pair>().Id != world.Component<Position>().Id);

            Assert.True(entity.Id != 0);
            Assert.True(entity.Has<Pair, Position>());
            Assert.True(!entity.Has<Position, Pair>());

            Assert.Equal("(Pair,Position)", entity.Type().Str());

            Pair* t = entity.GetFirstPtr<Pair, Position>();
            Assert.Equal(10, t->Value);
        }

    [Fact]
    private void SetTagPair()
    {
            using World world = World.Create();

            Entity pair = world.Entity("Pair");

            Entity entity = world.Entity()
                .SetSecond(pair, new Position { X = 10, Y = 20 });

            Assert.True(entity.Id != 0);
            Assert.True(entity.HasSecond<Position>(pair));
            Assert.Equal("(Pair,Position)", entity.Type().Str());

            Position* p = entity.GetSecondPtr<Position>(pair);
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

    [Fact]
    private void System1PairInstances()
    {
            using World world = World.Create();

            world.Entity()
                .Set<Pair, Position>(new Pair { Value = 10 });

            int invokeCount = 0;
            int entityCount = 0;
            int traitValue = 0;

            world.System()
                .Expr("(Pair, *)")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Pair> tr = it.Field<Pair>(0);
                        invokeCount++;

                        foreach (int i in it)
                        {
                            entityCount++;
                            traitValue += (int)tr[i].Value;
                        }
                    }
                });

            world.Progress();

            Assert.Equal(1, invokeCount);
            Assert.Equal(1, entityCount);
            Assert.Equal(10, traitValue);
        }

    [Fact]
    private void System2PairInstances()
    {
            using World world = World.Create();

            world.Entity()
                .Set<Pair, Position>(new Pair { Value = 10 })
                .Set<Pair, Velocity>(new Pair { Value = 20 });

            int invokeCount = 0;
            int entityCount = 0;
            int traitValue = 0;

            world.System()
                .Expr("(Pair, *)")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Pair> tr = it.Field<Pair>(0);
                        invokeCount++;

                        foreach (int i in it)
                        {
                            entityCount++;
                            traitValue += (int)tr[i].Value;
                        }
                    }
                });

            world.Progress();

            Assert.Equal(2, invokeCount);
            Assert.Equal(2, entityCount);
            Assert.Equal(30, traitValue);
        }

    [Fact]
    private void OverridePair()
    {
            using World world = World.Create();

            world.Component<Pair>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Entity @base = world.Entity()
                .Set<Pair, Position>(new Pair { Value = 10 });

            Entity instance = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(instance.Has<Pair, Position>());
            Pair* t = instance.GetFirstPtr<Pair, Position>();
            Assert.Equal(10, t->Value);

            Pair* t2 = @base.GetFirstPtr<Pair, Position>();
            Assert.True(t == t2);

            instance.Add<Pair, Position>();
            t = instance.GetFirstPtr<Pair, Position>();
            Assert.Equal(10, t->Value);
            Assert.True(t != t2);

            instance.Remove<Pair, Position>();
            t = instance.GetFirstPtr<Pair, Position>();
            Assert.Equal(10, t->Value);
            Assert.True(t == t2);
        }

    [Fact]
    private void OverrideTagPair()
    {
            using World world = World.Create();

            Entity pair = world.Entity().Add(Ecs.OnInstantiate, Ecs.Inherit);

            Entity @base = world.Entity()
                .SetSecond(pair, new Position { X = 10, Y = 20 });

            Entity instance = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(instance.HasSecond<Position>(pair));
            Position* t = instance.GetSecondPtr<Position>(pair);
            Assert.Equal(10, t->X);
            Assert.Equal(20, t->Y);

            Position* t2 = @base.GetSecondPtr<Position>(pair);
            Assert.True(t == t2);

            instance.AddSecond<Position>(pair);
            t = instance.GetSecondPtr<Position>(pair);
            Assert.Equal(10, t->X);
            Assert.Equal(20, t->Y);
            Assert.True(t != t2);

            instance.RemoveSecond<Position>(pair);
            t = instance.GetSecondPtr<Position>(pair);
            Assert.Equal(10, t->X);
            Assert.Equal(20, t->Y);
            Assert.True(t == t2);
        }

    [Fact]
    private void EnsurePair()
    {
            using World world = World.Create();

            Entity e = world.Entity();

            Pair* t = e.EnsureFirstPtr<Pair, Position>();
            Assert.True(t != null);
            t->Value = 10;

            Pair* t2 = e.GetFirstPtr<Pair, Position>();
            Assert.True(t == t2);
            Assert.Equal(10, t->Value);
        }

    [Fact]
    private void EnsurePairExisting()
    {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set<Pair, Position>(new Pair { Value = 20 });

            Pair* t = e.EnsureFirstPtr<Pair, Position>();
            Assert.True(t != null);
            Assert.Equal(20, t->Value);
            t->Value = 10;

            Pair* t2 = e.GetFirstPtr<Pair, Position>();
            Assert.True(t == t2);
            Assert.Equal(10, t->Value);
        }

    [Fact]
    private void EnsurePairTag()
    {
            using World world = World.Create();

            Entity pair = world.Entity();

            Entity e = world.Entity();

            Position* p = e.EnsureSecondPtr<Position>(pair);
            Assert.True(p != null);
            p->X = 10;
            p->Y = 20;

            Position* p2 = e.GetSecondPtr<Position>(pair);
            Assert.True(p == p2);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

    [Fact]
    private void EnsurePairTagExisting()
    {
            using World world = World.Create();

            Entity pair = world.Entity();

            Entity e = world.Entity()
                .SetSecond(pair, new Position { X = 10, Y = 20 });

            Position* p = e.EnsureSecondPtr<Position>(pair);
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* p2 = e.GetSecondPtr<Position>(pair);
            Assert.True(p == p2);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

    [Fact]
    private void EnsureRTagO()
    {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set<Tag, Position>(new Position { X = 10, Y = 20 });

            Position* t = e.EnsureSecondPtr<Tag, Position>();
            Assert.True(t != null);
            Assert.Equal(10, t->X);
            Assert.Equal(20, t->Y);
            t->X = 30;
            t->Y = 40;

            Position* t2 = e.GetSecondPtr<Tag, Position>();
            Assert.True(t == t2);
            Assert.Equal(30, t->X);
            Assert.Equal(40, t->Y);
        }

    [Fact]
    private void GetRelationFromId()
    {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity obj = world.Entity();

            Id pair = new Id(rel, obj);

            Assert.True(pair.First() == rel);
            Assert.True(pair.Second() != rel);

            Assert.True(pair.First().IsAlive());
            Assert.True(pair.First().IsValid());
        }

    [Fact]
    private void GetSecondFromId()
    {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity obj = world.Entity();

            Id pair = new Id(rel, obj);

            Assert.True(pair.First() != obj);
            Assert.True(pair.Second() == obj);

            Assert.True(pair.Second().IsAlive());
            Assert.True(pair.Second().IsValid());
        }

    [Fact]
    private void GetRecycledRelationFromId()
    {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity obj = world.Entity();

            rel.Destruct();
            obj.Destruct();

            rel = world.Entity();
            obj = world.Entity();

            // Make sure ids are recycled
            Assert.True((uint)rel.Id != rel.Id);
            Assert.True((uint)obj.Id != obj.Id);

            Id pair = new Id(rel, obj);

            Assert.True(pair.First() == rel);
            Assert.True(pair.Second() != rel);

            Assert.True(pair.First().IsAlive());
            Assert.True(pair.First().IsValid());
        }

    [Fact]
    private void GetRecycledObjectFromId()
    {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity obj = world.Entity();

            rel.Destruct();
            obj.Destruct();

            rel = world.Entity();
            obj = world.Entity();

            // Make sure ids are recycled
            Assert.True((uint)rel.Id != rel.Id);
            Assert.True((uint)obj.Id != obj.Id);

            Id pair = new Id(rel, obj);

            Assert.True(pair.First() == rel);
            Assert.True(pair.Second() != rel);

            Assert.True(pair.Second().IsAlive());
            Assert.True(pair.Second().IsValid());
        }

    [Fact]
    private void GetRelObj()
    {
            using World world = World.Create();

            Component<Position> rel = world.Component<Position>();
            Entity obj = world.Entity();

            Entity e = world.Entity()
                .Set(obj, new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position>(obj));
            Assert.True(e.Has(rel, obj));

            void* ptr = e.GetPtr(rel, obj);
            Assert.True(ptr != null);

            Assert.Equal(10, ((Position*)ptr)->X);
            Assert.Equal(20, ((Position*)ptr)->Y);
        }

    [Fact]
    private void GetRelObjId()
    {
            using World world = World.Create();

            Id rel = world.Component<Position>();
            Id obj = world.Entity();

            Entity e = world.Entity()
                .Set(obj, new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position>(obj));
            Assert.True(e.Has(rel, obj));

            void* ptr = e.GetPtr(rel, obj);
            Assert.True(ptr != null);

            Assert.Equal(10, ((Position*)ptr)->X);
            Assert.Equal(20, ((Position*)ptr)->Y);
        }

    [Fact]
    private void GetRelObjUlong()
    {
            using World world = World.Create();

            ulong rel = world.Component<Position>();
            ulong obj = world.Entity();

            Entity e = world.Entity()
                .Set(obj, new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position>(obj));
            Assert.True(e.Has(rel, obj));

            void* ptr = e.GetPtr(rel, obj);
            Assert.True(ptr != null);

            Assert.Equal(10, ((Position*)ptr)->X);
            Assert.Equal(20, ((Position*)ptr)->Y);
        }

    [Fact]
    private void GetRObj()
    {
            using World world = World.Create();

            Entity obj = world.Entity();

            Entity e = world.Entity()
                .Set(obj, new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position>(obj));

            Position* ptr = e.GetPtr<Position>(obj);
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetRObjId()
    {
            using World world = World.Create();

            Id obj = world.Entity();

            Entity e = world.Entity()
                .Set(obj, new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position>(obj));

            Position* ptr = e.GetPtr<Position>(obj);
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetRObjUlong()
    {
            using World world = World.Create();

            ulong obj = world.Entity();

            Entity e = world.Entity()
                .Set(obj, new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position>(obj));

            Position* ptr = e.GetPtr<Position>(obj);
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetRelO()
    {
            using World world = World.Create();

            Entity e = world.Entity().Set<Position, Tag>(new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Position, Tag>());

            Position* ptr = e.GetFirstPtr<Position, Tag>();
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetRTagO()
    {
            using World world = World.Create();

            Entity e = world.Entity().Set<Tag, Position>(new Position { X = 10, Y = 20 });

            Assert.True(e.Has<Tag, Position>());

            Position* ptr = e.GetSecondPtr<Tag, Position>();
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetSecond()
    {
            using World world = World.Create();

            Entity rel = world.Entity();

            Entity e = world.Entity()
                .SetSecond(rel, new Position { X = 10, Y = 20 });

            Assert.True(e.HasSecond<Position>(rel));

            Position* ptr = e.GetSecondPtr<Position>(rel);
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetSecondId()
    {
            using World world = World.Create();

            Id rel = world.Entity();

            Entity e = world.Entity()
                .SetSecond(rel, new Position { X = 10, Y = 20 });

            Assert.True(e.HasSecond<Position>(rel));

            Position* ptr = e.GetSecondPtr<Position>(rel);
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void GetSecondUlong()
    {
            using World world = World.Create();

            ulong rel = world.Entity();

            Entity e = world.Entity()
                .SetSecond(rel, new Position { X = 10, Y = 20 });

            Assert.True(e.HasSecond<Position>(rel));

            Position* ptr = e.GetSecondPtr<Position>(rel);
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

    [Fact]
    private void Each()
    {
            using World world = World.Create();

            Entity p1 = world.Entity();
            Entity p2 = world.Entity();

            Entity e = world.Entity()
                .Add(p1)
                .Add(p2);

            int count = 0;

            e.Each(entity =>
            {
                if (count == 0)
                    Assert.True(entity == p1);
                else if (count == 1)
                    Assert.True(entity == p2);
                else
                    Assert.True(false);

                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void EachPair()
    {
            using World world = World.Create();

            Component<Pair> pair = world.Component<Pair>();
            Component<Position> pos = world.Component<Position>();
            Component<Velocity> vel = world.Component<Velocity>();

            Entity e = world.Entity()
                .Add<Pair, Position>()
                .Add<Pair, Velocity>();

            int count = 0;

            e.Each(pair, obj =>
            {
                if (count == 0)
                    Assert.True(obj == (Entity)pos);
                else if (count == 1)
                    Assert.True(obj == (Entity)vel);
                else
                    Assert.True(false);

                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void EachPairByType()
    {
            using World world = World.Create();

            Component<Position> pos = world.Component<Position>();
            Component<Velocity> vel = world.Component<Velocity>();

            Entity e = world.Entity()
                .Add<Pair, Position>()
                .Add<Pair, Velocity>();

            int count = 0;

            e.Each<Pair>(obj =>
            {
                if (count == 0)
                    Assert.True(obj == (Entity)pos);
                else if (count == 1)
                    Assert.True(obj == (Entity)vel);
                else
                    Assert.True(false);

                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void EachPairWithIsA()
    {
            using World world = World.Create();

            Entity p1 = world.Entity();
            Entity p2 = world.Entity();

            Entity e = world.Entity()
                .IsA(p1)
                .IsA(p2);

            int count = 0;

            e.Each(EcsIsA, obj =>
            {
                if (count == 0)
                    Assert.True(obj == p1);
                else if (count == 1)
                    Assert.True(obj == p2);
                else
                    Assert.True(false);

                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void EachPairWithRecycledRel()
    {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            world.Entity().Destruct(); // force recycling

            Entity pair = world.Entity();

            Assert.True((uint)pair.Id != pair.Id); // ensure recycled

            Entity e = world.Entity()
                .Add(pair, e1)
                .Add(pair, e2);

            int count = 0;

            // should work correctly
            e.Each(pair, obj =>
            {
                if (count == 0)
                    Assert.True(obj == e1);
                else if (count == 1)
                    Assert.True(obj == e2);
                else
                    Assert.True(false);

                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void EachPairWithRecycledObj()
    {
            using World world = World.Create();

            Component<Pair> pair = world.Component<Pair>();

            world.Entity().Destruct(); // force recycling
            Entity e1 = world.Entity();
            Assert.True((uint)e1.Id != e1.Id); // ensure recycled

            world.Entity().Destruct();
            Entity e2 = world.Entity();
            Assert.True((uint)e2.Id != e2.Id);

            Entity e = world.Entity()
                .Add<Pair>(e1)
                .Add<Pair>(e2);

            int count = 0;

            // should work correctly
            e.Each(pair, obj =>
            {
                if (count == 0)
                    Assert.True(obj == e1);
                else if (count == 1)
                    Assert.True(obj == e2);
                else
                    Assert.True(false);

                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void MatchPair()
    {
            using World world = World.Create();

            Entity eats = world.Entity();
            Entity dislikes = world.Entity();

            Entity apples = world.Entity();
            Entity pears = world.Entity();
            Entity bananas = world.Entity();

            Entity e = world.Entity()
                .Set(new Position { X = 10, Y = 20 }) // should not be matched
                .Add(eats, apples)
                .Add(eats, pears)
                .Add(dislikes, bananas);

            int count = 0;

            e.Each(eats, apples, id =>
            {
                Assert.True(id.First() == eats);
                Assert.True(id.Second() == apples);
                count++;
            });

            Assert.Equal(1, count);
        }

    [Fact]
    private void MatchPairObjWildcard()
    {
            using World world = World.Create();

            Entity eats = world.Entity();
            Entity dislikes = world.Entity();

            Entity apples = world.Entity();
            Entity pears = world.Entity();
            Entity bananas = world.Entity();

            Entity e = world.Entity()
                .Set(new Position { X = 10, Y = 20 }) // should not be matched
                .Add(eats, apples)
                .Add(eats, pears)
                .Add(dislikes, bananas);

            int count = 0;

            e.Each(eats, EcsWildcard, id =>
            {
                Assert.True(id.First() == eats);
                Assert.True(id.Second() == apples || id.Second() == pears);
                count++;
            });

            Assert.Equal(2, count);
        }

    [Fact]
    private void MatchPairRelWildcard()
    {
            using World world = World.Create();

            Entity eats = world.Entity();
            Entity dislikes = world.Entity();

            Entity apples = world.Entity();
            Entity pears = world.Entity();
            Entity bananas = world.Entity();

            Entity e = world.Entity()
                .Set(new Position { X = 10, Y = 20 }) // should not be matched
                .Add(eats, apples)
                .Add(eats, pears)
                .Add(dislikes, bananas);

            int count = 0;

            e.Each(EcsWildcard, pears, id =>
            {
                Assert.True(id.First() == eats);
                Assert.True(id.Second() == pears);
                count++;
            });

            Assert.Equal(1, count);
        }

    [Fact]
    private void MatchPairBothWildcard()
    {
            using World world = World.Create();

            Entity eats = world.Entity();
            Entity dislikes = world.Entity();

            Entity apples = world.Entity();
            Entity pears = world.Entity();
            Entity bananas = world.Entity();

            Entity e = world.Entity()
                .Set(new Position { X = 10, Y = 20 }) // should not be matched
                .Add(eats, apples)
                .Add(eats, pears)
                .Add(dislikes, bananas);

            int count = 0;

            e.Each(EcsWildcard, EcsWildcard, _ => { count++; });

            Assert.Equal(3, count);
        }

    [Fact]
    private void HasTagWithObject()
    {
            using World world = World.Create();

            Entity bob = world.Entity();
            Entity e = world.Entity().Add<Likes>(bob);
            Assert.True(e.Has<Likes>(bob));
        }

    [Fact]
    private void HasSecondTag()
    {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity e = world.Entity().AddSecond<Bob>(likes);
            Assert.True(e.HasSecond<Bob>(likes));
        }

    [Fact]
    private void GetObjectForTypeSelf()
    {
            using World world = World.Create();

            world.Component<Tag>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Entity @base = world.Entity().Add<Tag>();
            Entity self = world.Entity().IsA(@base).Add<Tag>();

            Entity obj = self.TargetFor<Tag>(EcsIsA);
            Assert.True(obj != 0);
            Assert.True(obj == self);
        }

    [Fact]
    private void GetObjectForTypeBase()
    {
            using World world = World.Create();

            world.Component<Tag>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Entity @base = world.Entity().Add<Tag>();
            Entity self = world.Entity().IsA(@base);

            Entity obj = self.TargetFor<Tag>(EcsIsA);
            Assert.True(obj != 0);
            Assert.True(obj == @base);
        }

    [Fact]
    private void GetObjectForIdSelf()
    {
            using World world = World.Create();

            Entity tag = world.Entity().Add(Ecs.OnInstantiate, Ecs.Inherit);
            Entity @base = world.Entity().Add(tag);
            Entity self = world.Entity().IsA(@base).Add(tag);

            Entity obj = self.TargetFor(EcsIsA, tag);
            Assert.True(obj != 0);
            Assert.True(obj == self);
        }

    [Fact]
    private void GetObjectForIdBase()
    {
            using World world = World.Create();

            Entity tag = world.Entity().Add(Ecs.OnInstantiate, Ecs.Inherit);
            Entity @base = world.Entity().Add(tag);
            Entity self = world.Entity().IsA(@base);

            Entity obj = self.TargetFor(EcsIsA, tag);
            Assert.True(obj != 0);
            Assert.True(obj == @base);
        }

    [Fact]
    private void GetObjectForIdNotFound()
    {
            using World world = World.Create();

            Entity tag = world.Entity().Add(Ecs.OnInstantiate, Ecs.Inherit);
            Entity @base = world.Entity();
            Entity self = world.Entity().IsA(@base);

            Entity obj = self.TargetFor(EcsIsA, tag);
            Assert.True(obj == 0);
        }

    [Fact]
    private void SetRelExistingValue()
    {
            using World world = World.Create();

            Position p = new Position { X = 10, Y = 20 };
            Entity e = world.Entity().Set<Position, Tag>(p);

            Position* ptr = e.GetFirstPtr<Position, Tag>();
            Assert.True(ptr != null);
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }


    [Fact]
    private void SymmetricWithChildOf()
    {
            using World world = World.Create();

            world.Component<Likes>().Entity.Add(Ecs.Symmetric);

            Entity idk = world.Entity("Idk");

            Entity bob = world.Entity("Bob")
                .ChildOf(idk);

            Entity alice = world.Entity("Alice")
                .ChildOf(idk)
                .Add<Likes>(bob);

            Assert.True(bob.Has<Likes>(alice));
        }
}