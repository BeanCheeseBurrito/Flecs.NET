using System.Runtime.CompilerServices;
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class PairTests
    {
        public PairTests()
        {
            FlecsInternal.Reset();
        }

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
                .SetFirst<Pair, Position>(new Pair { Value = 10 });

            Assert.True(Type<Pair>.RawId != Type<Position>.RawId);

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
        private void PairInstances1()
        {
            using World world = World.Create();

            world.Entity()
                .SetFirst<Pair, Position>(new Pair { Value = 10 });

            int invokeCount = 0;
            int entityCount = 0;
            int traitValue = 0;

            world.Routine(
                filter: world.FilterBuilder().Expr("(Pair, *)"),
                callback: it =>
                {
                    Column<Pair> tr = it.Field<Pair>(1);
                    invokeCount++;

                    foreach (int i in it)
                    {
                        entityCount++;
                        traitValue += (int)tr[i].Value;
                    }
                }
            );

            world.Progress();

            Assert.Equal(1, invokeCount);
            Assert.Equal(1, entityCount);
            Assert.Equal(10, traitValue);
        }

        [Fact]
        private void PairInstances2()
        {
            using World world = World.Create();

            world.Entity()
                .SetFirst<Pair, Position>(new Pair { Value = 10 })
                .SetFirst<Pair, Velocity>(new Pair { Value = 20 });

            int invokeCount = 0;
            int entityCount = 0;
            int traitValue = 0;

            world.Routine(
                filter: world.FilterBuilder().Expr("(Pair, *)"),
                callback: it =>
                {
                    Column<Pair> tr = it.Field<Pair>(1);
                    invokeCount++;

                    foreach (int i in it)
                    {
                        entityCount++;
                        traitValue += (int)tr[i].Value;
                    }
                }
            );

            world.Progress();

            Assert.Equal(2, invokeCount);
            Assert.Equal(2, entityCount);
            Assert.Equal(30, traitValue);
        }

        [Fact]
        private void OverridePair()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .SetFirst<Pair, Position>(new Pair { Value = 10 });

            Entity instance = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True((instance.Has<Pair, Position>()));
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

            Entity pair = world.Entity();

            Entity @base = world.Entity()
                .SetSecond(pair, new Position { X = 10, Y = 20 });

            Entity instance = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True((instance.HasSecond<Position>(pair)));
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
        private void GetMutPair()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            Pair* t = e.GetMutFirstPtr<Pair, Position>();
            Assert.True(t != null);
            t->Value = 10;

            Pair* t2 = e.GetFirstPtr<Pair, Position>();
            Assert.True(t == t2);
            Assert.Equal(10, t->Value);
        }

        [Fact]
        private void GetMutPairExisting()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .SetFirst<Pair, Position>(new Pair { Value = 20 });

            Pair* t = e.GetMutFirstPtr<Pair, Position>();
            Assert.True(t != null);
            Assert.Equal(20, t->Value);
            t->Value = 10;

            Pair* t2 = e.GetFirstPtr<Pair, Position>();
            Assert.True(t == t2);
            Assert.Equal(10, t->Value);
        }

        [Fact]
        private void GetMutPairTag()
        {
            using World world = World.Create();

            Entity pair = world.Entity();

            Entity e = world.Entity();

            Position* p = e.GetMutSecondPtr<Position>(pair);
            Assert.True(p != null);
            p->X = 10;
            p->Y = 20;

            Position* p2 = e.GetSecondPtr<Position>(pair);
            Assert.True(p == p2);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void GetMutPairTagExisting()
        {
            using World world = World.Create();

            Entity pair = world.Entity();

            Entity e = world.Entity()
                .SetSecond(pair, new Position { X = 10, Y = 20 });

            Position* p = e.GetMutSecondPtr<Position>(pair);
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* p2 = e.GetSecondPtr<Position>(pair);
            Assert.True(p == p2);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void GetMutRTagO()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .SetSecond<Tag, Position>(new Position { X = 10, Y = 20 });

            Position* t = e.GetMutSecondPtr<Tag, Position>();
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

            Id rel = world.Component<Position>().Id;
            Id obj = world.Entity().Id;

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

            Id obj = world.Entity().Id;

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
        private void GetRO()
        {
            using World world = World.Create();

            Entity e = world.Entity().SetFirst<Position, Tag>(new Position { X = 10, Y = 20 });

            Assert.True((e.Has<Position, Tag>()));

            Position* ptr = e.GetFirstPtr<Position, Tag>();
            Assert.True(ptr != null);

            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void GetRTagO()
        {
            using World world = World.Create();

            Entity e = world.Entity().SetSecond<Tag, Position>(new Position { X = 10, Y = 20 });

            Assert.True((e.Has<Tag, Position>()));

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

            Id rel = world.Entity().Id;

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
        //
        // [Fact]
        // private void each() {
        //     using World world = World.Create();
        //
        //     auto p_1 = world.Entity();
        //     auto p_2 = world.Entity();
        //
        //     Entity e = world.Entity()
        //         .Add(p_1)
        //         .Add(p_2);
        //
        //     int32_t count = 0;
        //
        //     e.each([&](Id e) {
        //         if (count == 0) {
        //             Assert.True(e == p_1);
        //         } else if (count == 1) {
        //             Assert.True(e == p_2);
        //         } else {
        //             Assert.True(false);
        //         }
        //
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void each_pair() {
        //     using World world = World.Create();
        //
        //     auto pair = world.Component<Pair>();
        //     auto pos = world.Component<Position>();
        //     auto vel = world.Component<Velocity>();
        //
        //     Entity e = world.Entity()
        //         .Add<Pair, Position>()
        //         .Add<Pair, Velocity>();
        //
        //     int32_t count = 0;
        //
        //     e.each(pair, [&](flecs::entity object) {
        //         if (count == 0) {
        //             Assert.True(object == pos);
        //         } else if (count == 1) {
        //             Assert.True(object == vel);
        //         } else {
        //             Assert.True(false);
        //         }
        //
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void each_pair_by_type() {
        //     using World world = World.Create();
        //
        //     auto pos = world.Component<Position>();
        //     auto vel = world.Component<Velocity>();
        //
        //     Entity e = world.Entity()
        //         .Add<Pair, Position>()
        //         .Add<Pair, Velocity>();
        //
        //     int32_t count = 0;
        //
        //     e.each<Pair>([&](flecs::entity object) {
        //         if (count == 0) {
        //             Assert.True(object == pos);
        //         } else if (count == 1) {
        //             Assert.True(object == vel);
        //         } else {
        //             Assert.True(false);
        //         }
        //
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void each_pair_w_isa() {
        //     using World world = World.Create();
        //
        //     auto p_1 = world.Entity();
        //     auto p_2 = world.Entity();
        //
        //     Entity e = world.Entity()
        //         .is_a(p_1)
        //         .is_a(p_2);
        //
        //     int32_t count = 0;
        //
        //     e.each(flecs::IsA, [&](flecs::entity object) {
        //         if (count == 0) {
        //             Assert.True(object == p_1);
        //         } else if (count == 1) {
        //             Assert.True(object == p_2);
        //         } else {
        //             Assert.True(false);
        //         }
        //
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void each_pair_w_recycled_rel() {
        //     using World world = World.Create();
        //
        //     auto e_1 = world.Entity();
        //     auto e_2 = world.Entity();
        //
        //     world.Entity().Destruct(); // force recycling
        //
        //     Entity pair = world.Entity();
        //
        //     Assert.True((uint)pair.Id != pair.Id); // ensure recycled
        //
        //     Entity e = world.Entity()
        //         .Add(pair, e_1)
        //         .Add(pair, e_2);
        //
        //     int32_t count = 0;
        //
        //     // should work correctly
        //     e.each(pair, [&](flecs::entity object) {
        //         if (count == 0) {
        //             Assert.True(object == e_1);
        //         } else if (count == 1) {
        //             Assert.True(object == e_2);
        //         } else {
        //             Assert.True(false);
        //         }
        //
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void each_pair_w_recycled_obj() {
        //     using World world = World.Create();
        //
        //     auto pair = world.Component<Pair>();
        //
        //     world.Entity().Destruct(); // force recycling
        //     auto e_1 = world.Entity();
        //     Assert.True((uint)e_1.Id != e_1.Id); // ensure recycled
        //
        //     world.Entity().Destruct();
        //     auto e_2 = world.Entity();
        //     Assert.True((uint)e_2.Id != e_2.Id);
        //
        //     Entity e = world.Entity()
        //         .Add<Pair>(e_1)
        //         .Add<Pair>(e_2);
        //
        //     int32_t count = 0;
        //
        //     // should work correctly
        //     e.each(pair, [&](flecs::entity object) {
        //         if (count == 0) {
        //             Assert.True(object == e_1);
        //         } else if (count == 1) {
        //             Assert.True(object == e_2);
        //         } else {
        //             Assert.True(false);
        //         }
        //
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void match_pair() {
        //     using World world = World.Create();
        //
        //     auto Eats = world.Entity();
        //     auto Dislikes = world.Entity();
        //
        //     auto Apples = world.Entity();
        //     auto Pears = world.Entity();
        //     auto Bananas = world.Entity();
        //
        //     Entity e = world.Entity()
        //         .Set<Position>({10, 20}) // should not be matched
        //         .Add(Eats, Apples)
        //         .Add(Eats, Pears)
        //         .Add(Dislikes, Bananas);
        //
        //     int32_t count = 0;
        //
        //     e.each(Eats, Apples,
        //         [&](Id id) {
        //             Assert.True(id.First() == Eats);
        //             Assert.True(id.Second() == Apples);
        //             count ++;
        //         });
        //
        //     Assert.Equal(count, 1);
        // }
        //
        // [Fact]
        // private void match_pair_obj_wildcard() {
        //     using World world = World.Create();
        //
        //     auto Eats = world.Entity();
        //     auto Dislikes = world.Entity();
        //
        //     auto Apples = world.Entity();
        //     auto Pears = world.Entity();
        //     auto Bananas = world.Entity();
        //
        //     Entity e = world.Entity()
        //         .Set<Position>({10, 20}) // should not be matched
        //         .Add(Eats, Apples)
        //         .Add(Eats, Pears)
        //         .Add(Dislikes, Bananas);
        //
        //     int32_t count = 0;
        //
        //     e.each(Eats, flecs::Wildcard,
        //         [&](Id id) {
        //             Assert.True(id.First() == Eats);
        //             Assert.True(id.Second() == Apples || id.Second() == Pears);
        //             count ++;
        //         });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void match_pair_rel_wildcard() {
        //     using World world = World.Create();
        //
        //     auto Eats = world.Entity();
        //     auto Dislikes = world.Entity();
        //
        //     auto Apples = world.Entity();
        //     auto Pears = world.Entity();
        //     auto Bananas = world.Entity();
        //
        //     Entity e = world.Entity()
        //         .Set<Position>({10, 20}) // should not be matched
        //         .Add(Eats, Apples)
        //         .Add(Eats, Pears)
        //         .Add(Dislikes, Bananas);
        //
        //     int32_t count = 0;
        //
        //     e.each(flecs::Wildcard, Pears,
        //         [&](Id id) {
        //             Assert.True(id.First() == Eats);
        //             Assert.True(id.Second() == Pears);
        //             count ++;
        //         });
        //
        //     Assert.Equal(count, 1);
        // }
        //
        // [Fact]
        // private void match_pair_both_wildcard() {
        //     using World world = World.Create();
        //
        //     auto Eats = world.Entity();
        //     auto Dislikes = world.Entity();
        //
        //     auto Apples = world.Entity();
        //     auto Pears = world.Entity();
        //     auto Bananas = world.Entity();
        //
        //     Entity e = world.Entity()
        //         .Set<Position>({10, 20}) // should not be matched
        //         .Add(Eats, Apples)
        //         .Add(Eats, Pears)
        //         .Add(Dislikes, Bananas);
        //
        //     int32_t count = 0;
        //
        //     e.each(flecs::Wildcard, flecs::Wildcard,
        //         [&](Id id) {
        //             count ++;
        //         });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void has_tag_w_object() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //
        //     auto Bob = world.Entity();
        //     Entity e = world.Entity().Add<Likes>(Bob);
        //     Assert.True(e.Has<Likes>(Bob));
        // }
        //
        // [Fact]
        // private void has_second_tag() {
        //     using World world = World.Create();
        //
        //     struct Bob { };
        //
        //     auto Likes = world.Entity();
        //     Entity e = world.Entity().AddSecond<Bob>(Likes);
        //     Assert.True(e.HasSecond<Bob>(Likes));
        // }
        //
        // struct Eats { int amount; };
        // struct Apples { };
        // struct Pears { };
        //
        // using EatsApples = flecs::pair<Eats, Apples>;
        // using EatsPears = flecs::pair<Eats, Pears>;
        //
        // [Fact]
        // private void add_pair_type() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Add<EatsApples>();
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<EatsApples>()));
        // }
        //
        // [Fact]
        // private void remove_pair_type() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Add<EatsApples>();
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<EatsApples>()));
        //
        //     e.Remove<EatsApples>();
        //     Assert.True(!(e.Has<Eats, Apples>()));
        //     Assert.True(!(e.Has<EatsApples>()));
        // }
        //
        // [Fact]
        // private void set_pair_type() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Set<EatsApples>({10});
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<EatsApples>()));
        //
        //     const Eats *ptr = e.GetPtr<EatsApples>();
        //     Assert.Equal(ptr->amount, 10);
        //
        //     Assert.True((ptr == e.GetPtr<Eats, Apples>()));
        // }
        //
        // [Fact]
        // private void has_pair_type() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Add<Eats, Apples>();
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<EatsApples>()));
        // }
        //
        // [Fact]
        // private void get_1_pair_arg() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Set<EatsApples>({10});
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<EatsApples>()));
        //
        //     test_bool(e.get([](const EatsApples& a) {
        //         Assert.Equal(a->amount, 10);
        //     }), true);
        // }
        //
        // [Fact]
        // private void get2_pair_arg() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .Set<EatsApples>({10})
        //         .Set<EatsPears>({20});
        //
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<Eats, Pears>()));
        //     Assert.True((e.Has<EatsApples>()));
        //     Assert.True((e.Has<EatsPears>()));
        //
        //     test_bool(e.get([](const EatsApples& a, const EatsPears& p) {
        //         Assert.Equal(a->amount, 10);
        //         Assert.Equal(p->amount, 20);
        //     }), true);
        // }
        //
        // [Fact]
        // private void set_1_pair_arg() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .set([](EatsApples&& a) {
        //             a->amount = 10;
        //         });
        //
        //     auto eats = e.GetPtr<EatsApples>();
        //     Assert.Equal(eats->amount, 10);
        // }
        //
        // [Fact]
        // private void set2_pair_arg() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .set([](EatsApples&& a, EatsPears&& p) {
        //             a->amount = 10;
        //             p->amount = 20;
        //         });
        //
        //     auto eats = e.GetPtr<EatsApples>();
        //     Assert.Equal(eats->amount, 10);
        //
        //     eats = e.GetPtr<EatsPears>();
        //     Assert.Equal(eats->amount, 20);
        // }
        //
        // [Fact]
        // private void get_inline_pair_type() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Set<EatsApples>({10});
        //     Assert.True((e.Has<Eats, Apples>()));
        //     Assert.True((e.Has<EatsApples>()));
        //
        //     test_bool(e.get([](const flecs::pair<Eats, Apples>& a) {
        //         Assert.Equal(a->amount, 10);
        //     }), true);
        // }
        //
        // [Fact]
        // private void set_inline_pair_type() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .set([](flecs::pair<Eats, Apples>&& a) {
        //             a->amount = 10;
        //         });
        //
        //     auto eats = e.GetPtr<EatsApples>();
        //     Assert.Equal(eats->amount, 10);
        // }
        //
        // [Fact]
        // private void get_pair_type_object() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().SetSecond<Apples, Eats>({10});
        //     Assert.True((e.Has<Apples, Eats>()));
        //
        //     test_bool(e.get([](const flecs::pair_object<Apples, Eats>& a) {
        //         Assert.Equal(a->amount, 10);
        //     }), true);
        // }
        //
        // [Fact]
        // private void set_pair_type_object() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .set([](flecs::pair_object<Apples, Eats>&& a) {
        //             a->amount = 10;
        //         });
        //
        //     auto eats = e.GetSecondPtr<Apples, Eats>();
        //     Assert.Equal(eats->amount, 10);
        // }
        //
        // struct Event {
        //     const char *value;
        // };
        //
        // struct Begin { };
        // struct End { };
        //
        // using BeginEvent = flecs::pair<Begin, Event>;
        // using EndEvent = flecs::pair<End, Event>;
        //
        // [Fact]
        // private void set_get_second_variants() {
        //     using World world = World.Create();
        //
        //     auto e1 = world.Entity().SetSecond<Begin, Event>({"Big Bang"});
        //     Assert.True((e1.Has<Begin, Event>()));
        //     const Event* v = e1.GetSecondPtr<Begin, Event>();
        //     Assert.True(v != null);
        //     Assert.Equal(v->value, "Big Bang");
        //
        //     auto e2 = world.Entity().Set<Begin, Event>({"Big Bang"});
        //     Assert.True((e2.Has<Begin, Event>()));
        //     v = e2.GetPtr<Begin, Event>();
        //     Assert.True(v != null);
        //     Assert.Equal(v->value, "Big Bang");
        //
        //     auto e3 = world.Entity().Set<flecs::pair<Begin, Event>>({"Big Bang"});
        //     Assert.True((e3.Has<flecs::pair<Begin, Event>>()));
        //     v = e3.GetPtr<flecs::pair<Begin, Event>>();
        //     Assert.True(v != null);
        //     Assert.Equal(v->value, "Big Bang");
        //
        //     auto e4 = world.Entity().Set<BeginEvent>({"Big Bang"});
        //     Assert.True((e4.Has<BeginEvent>()));
        //     v = e4.GetPtr<BeginEvent>();
        //     Assert.True(v != null);
        //     Assert.Equal(v->value, "Big Bang");
        // }
        //
        // [Fact]
        // private void get_object_for_type_self() {
        //     using World world = World.Create();
        //
        //     auto @base = world.Entity().Add<Tag>();
        //     auto self = world.Entity().is_a(@base).Add<Tag>();
        //
        //     auto obj = self.target_for<Tag>(flecs::IsA);
        //     Assert.True(obj != 0);
        //     Assert.True(obj == self);
        // }
        //
        // [Fact]
        // private void get_object_for_type_base() {
        //     using World world = World.Create();
        //
        //     auto @base = world.Entity().Add<Tag>();
        //     auto self = world.Entity().is_a(@base);
        //
        //     auto obj = self.target_for<Tag>(flecs::IsA);
        //     Assert.True(obj != 0);
        //     Assert.True(obj == @base);
        // }
        //
        // [Fact]
        // private void get_object_for_id_self() {
        //     using World world = World.Create();
        //
        //     auto tag = world.Entity();
        //     auto @base = world.Entity().Add(tag);
        //     auto self = world.Entity().is_a(@base).Add(tag);
        //
        //     auto obj = self.target_for(flecs::IsA, tag);
        //     Assert.True(obj != 0);
        //     Assert.True(obj == self);
        // }
        //
        // [Fact]
        // private void get_object_for_id_base() {
        //     using World world = World.Create();
        //
        //     auto tag = world.Entity();
        //     auto @base = world.Entity().Add(tag);
        //     auto self = world.Entity().is_a(@base);
        //
        //     auto obj = self.target_for(flecs::IsA, tag);
        //     Assert.True(obj != 0);
        //     Assert.True(obj == @base);
        // }
        //
        // [Fact]
        // private void get_object_for_id_not_found() {
        //     using World world = World.Create();
        //
        //     auto tag = world.Entity();
        //     auto @base = world.Entity();
        //     auto self = world.Entity().is_a(@base);
        //
        //     auto obj = self.target_for(flecs::IsA, tag);
        //     Assert.True(obj == 0);
        // }
        //
        // [Fact]
        // private void deref_pair() {
        //     using World world = World.Create();
        //
        //     Position v = {10, 20};
        //
        //     flecs::pair<Position, Tag> p(v);
        //     Assert.Equal(p->X, 10);
        //     Assert.Equal(p->Y, 20);
        //
        //     Position pos = *p;
        //     Assert.Equal(pos->X, 10);
        //     Assert.Equal(pos->Y, 20);
        // }
        //
        // [Fact]
        // private void deref_const_pair() {
        //     using World world = World.Create();
        //
        //     Position v = {10, 20};
        //
        //     const flecs::pair<Position, Tag> p(v);
        //     Assert.Equal(p->X, 10);
        //     Assert.Equal(p->Y, 20);
        //
        //     Position pos = *p;
        //     Assert.Equal(pos->X, 10);
        //     Assert.Equal(pos->Y, 20);
        // }
        //
        // [Fact]
        // private void deref_pair_obj() {
        //     using World world = World.Create();
        //
        //     Position v = {10, 20};
        //
        //     flecs::pair<Tag, Position> p(v);
        //     Assert.Equal(p->X, 10);
        //     Assert.Equal(p->Y, 20);
        //
        //     Position pos = *p;
        //     Assert.Equal(pos->X, 10);
        //     Assert.Equal(pos->Y, 20);
        // }
        //
        // [Fact]
        // private void deref_const_pair_obj() {
        //     using World world = World.Create();
        //
        //     Position v = {10, 20};
        //
        //     const flecs::pair<Tag, Position> p(v);
        //     Assert.Equal(p->X, 10);
        //     Assert.Equal(p->Y, 20);
        //
        //     Position pos = *p;
        //     Assert.Equal(pos->X, 10);
        //     Assert.Equal(pos->Y, 20);
        // }
        //
        // [Fact]
        // private void set_R_existing_value() {
        //     using World world = World.Create();
        //
        //     Position p{10, 20};
        //     flecs::entity e = world.Entity().Set<Position, Tag>(p);
        //
        //     Position* ptr = e.GetPtr<Position, Tag>();
        //     Assert.True(ptr != null);
        //     Assert.Equal(10, ptr->X);
        //     Assert.Equal(20, ptr->Y);
        // }
    }
}
