using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class FilterBuilderTests
    {
        public FilterBuilderTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void BuilderAssign()
        {
            using World world = World.Create();

            using Filter filter = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Position>()
                    .Term<Velocity>()
            );

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            Entity _ = world.Entity().Add<Position>();

            int count = 0;

            filter.Each(entity =>
            {
                count++;
                Assert.True(entity == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void BuilderFilterWrapper()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position { X = 10, Y = 20 });

            Entity f = world.Entity().Set(new FilterWrapper
            {
                Filter = world.Filter(
                    filter: world.FilterBuilder().Term<Position>()
                )
            });

            int count = 0;

            f.Get<FilterWrapper>().Filter.Each(entity =>
            {
                Assert.True(entity == e1);
                count++;
            });

            Assert.Equal(1, count);
            f.Get<FilterWrapper>().Filter.Dispose();
        }

        //
        // [Fact]
        // private void builder_build_n_statements() {
        //     using World world = World.Create();
        //
        //     var qb = world.filter_builder<>();
        //     qb.Term<Position>();
        //     qb.Term<Velocity>();
        //     var q = qb.build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void 1_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>().build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void add_1_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<>()
        //         .Term<Position>()
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void add_2_types() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<>()
        //         .Term<Position>()
        //         .Term<Velocity>()
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void add_1_type_w_1_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>()
        //         .Term<Velocity>()
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void add_2_types_w_1_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>()
        //         .Term<Velocity>()
        //         .Term<Mass>()
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>().Add<Mass>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        [Fact]
        private void AddPair()
        {
            using World world = World.Create();

            Entity likes = world.Entity();
            Entity bob = world.Entity();
            Entity alice = world.Entity();

            using Filter q = world.Filter(
                filter: world.FilterBuilder().Term(likes, bob)
            );

            Entity e1 = world.Entity().Add(likes, bob);
            world.Entity().Add(likes, alice);

            int count = 0;
            q.Each(e =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void AddNot()
        {
            using World world = World.Create();

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Position>()
                    .Term<Velocity>().Oper(EcsNot)
            );

            Entity e1 = world.Entity().Add<Position>();
            world.Entity().Add<Position>().Add<Velocity>();

            int count = 0;
            q.Each(e =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void AddOr()
        {
            using World world = World.Create();

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Position>().Oper(EcsOr)
                    .Term<Velocity>()
            );

            Entity e1 = world.Entity().Add<Position>();
            Entity e2 = world.Entity().Add<Velocity>();
            world.Entity().Add<Mass>();

            int count = 0;
            q.Each(e =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

            Assert.Equal(2, count);
        }

        [Fact]
        private void AddOptional()
        {
            using World world = World.Create();

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Position>()
                    .Term<Velocity>().Oper(EcsOptional)
            );

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


        [Fact]
        private void StringTerm()
        {
            using World world = World.Create();

            world.Component<Position>();

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Expr("Position")
            );

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

        [Fact]
        private void SingletonTerm()
        {
            using World world = World.Create();

            world.Set(new Other { Value = 10 });

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Self>()
                    .Term<Other>().Singleton()
            );

            Entity e = world.Entity();
            e.Set(new Self { Value = e });
            e = world.Entity();
            e.Set(new Self { Value = e });
            e = world.Entity();
            e.Set(new Self { Value = e });

            int count = 0;

            q.Iter(it =>
            {
                Column<Self> s = it.Field<Self>(1);
                Column<Other> o = it.Field<Other>(2);

                Assert.True(!it.IsSelf(2));
                Assert.Equal(10, o[0].Value);

                ref readonly Other oRef = ref o[0];
                Assert.Equal(10, oRef.Value);

                foreach (int i in it)
                {
                    Assert.True(it.Entity(i) == s[i].Value);
                    count++;
                }
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void IsASupersetTerm()
        {
            using World world = World.Create();

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Self>()
                    .Term<Other>().Src().Up()
            );

            Entity @base = world.Entity().Set(new Other { Value = 10 });

            Entity e = world.Entity().Add(EcsIsA, @base);
            e.Set(new Self { Value = e });
            e = world.Entity().Add(EcsIsA, @base);
            e.Set(new Self { Value = e });
            e = world.Entity().Add(EcsIsA, @base);
            e.Set(new Self { Value = e });

            int count = 0;

            q.Iter(it =>
            {
                Column<Self> s = it.Field<Self>(1);
                Column<Other> o = it.Field<Other>(2);

                Assert.True(!it.IsSelf(2));
                Assert.Equal(10, o[0].Value);

                foreach (int i in it)
                {
                    Assert.True(it.Entity(i) == s[i].Value);
                    count++;
                }
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void IsASelfSupersetTerm()
        {
            using World world = World.Create();

            using Filter q = world.Filter(
                filter: world.FilterBuilder()
                    .Term<Self>()
                    .Term<Other>().Src().Self().Up()
            );

            Entity @base = world.Entity().Set(new Other { Value = 10 });

            Entity e = world.Entity().Add(EcsIsA, @base);
            e.Set(new Self { Value = e });
            e = world.Entity().Add(EcsIsA, @base);
            e.Set(new Self { Value = e });
            e = world.Entity().Add(EcsIsA, @base);
            e.Set(new Self { Value = e });
            e = world.Entity().Set(new Other { Value = 20 });
            e.Set(new Self { Value = e });
            e = world.Entity().Set(new Other { Value = 20 });
            e.Set(new Self { Value = e });

            int count = 0;
            int ownedCount = 0;

            q.Iter(it =>
            {
                Column<Self> s = it.Field<Self>(1);
                Column<Other> o = it.Field<Other>(2);

                if (!it.IsSelf(2))
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
            });

            Assert.Equal(5, count);
            Assert.Equal(2, ownedCount);
        }
        //
        // [Fact]
        // private void childof_superset_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self>()
        //         .Term<Other>().src().up(flecs::ChildOf)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //         Assert.True(!it.IsSelf(2));
        //         Assert.Equal(o->value, 10);
        //
        //         foreach (int i in it) {
        //             Assert.True(it.Entity(i) == s[i].Value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void childof_self_superset_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self>()
        //         .Term<Other>().src().self().up(flecs::ChildOf)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 20 }); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 20 }); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //     int owned_count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //
        //         if (!it.IsSelf(2)) {
        //             Assert.Equal(o->value, 10);
        //         } else {
        //             foreach (int i in it) {
        //                 Assert.Equal(o[i].Value, 20);
        //                 owned_count ++;
        //             }
        //         }
        //
        //         foreach (int i in it) {
        //             Assert.True(it.Entity(i) == s[i].Value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 5);
        //     Assert.Equal(owned_count, 2);
        // }
        //
        // [Fact]
        // private void isa_superset_term_w_each() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).src().up()
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().Add(EcsIsA, @base); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(EcsIsA, @base); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(EcsIsA, @base); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void isa_self_superset_term_w_each() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).src().self().up()
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().Add(EcsIsA, @base); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(EcsIsA, @base); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(EcsIsA, @base); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // private void childof_superset_term_w_each() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).src().up(flecs::ChildOf)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void childof_self_superset_term_w_each() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).src().self().up(flecs::ChildOf)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // private void isa_superset_shortcut() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).up()
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().is_a(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().is_a(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().is_a(@base); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void isa_superset_shortcut_w_self() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).self().up(EcsIsA)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().is_a(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().is_a(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().is_a(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // private void childof_superset_shortcut() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).up(flecs::ChildOf)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void childof_superset_shortcut_w_self() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).self().up(flecs::ChildOf)
        //         .build();
        //
        //     var @base = world.Entity().Set(new Other { Value = 10 });
        //
        //     var
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().child_of(@base); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //     e = world.Entity().Set(new Other { Value = 10 }); e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // private void relation() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     var Bob = world.Entity();
        //     var Alice = world.Entity();
        //
        //     var q = world.filter_builder<Self>()
        //         .term(Likes, Bob)
        //         .build();
        //
        //     var
        //     e = world.Entity().Add(Likes, Bob); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(Likes, Bob); e.Set(new Self { Value = e });
        //
        //     e = world.Entity().Add(Likes, Alice); e.set<Self>({0});
        //     e = world.Entity().Add(Likes, Alice); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // private void relation_w_object_wildcard() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     var Bob = world.Entity();
        //     var Alice = world.Entity();
        //
        //     var q = world.filter_builder<Self>()
        //         .term(Likes, flecs::Wildcard)
        //         .build();
        //
        //     var
        //     e = world.Entity().Add(Likes, Bob); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(Likes, Bob); e.Set(new Self { Value = e });
        //
        //     e = world.Entity().Add(Likes, Alice); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(Likes, Alice); e.Set(new Self { Value = e });
        //
        //     e = world.Entity(); e.set<Self>({0});
        //     e = world.Entity(); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 4);
        // }
        //
        // [Fact]
        // private void relation_w_predicate_wildcard() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     var Dislikes = world.Entity();
        //     var Bob = world.Entity();
        //     var Alice = world.Entity();
        //
        //     var q = world.filter_builder<Self>()
        //         .term(flecs::Wildcard, Alice)
        //         .build();
        //
        //     var
        //     e = world.Entity().Add(Likes, Alice); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(Dislikes, Alice); e.Set(new Self { Value = e });
        //
        //     e = world.Entity().Add(Likes, Bob); e.set<Self>({0});
        //     e = world.Entity().Add(Dislikes, Bob); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // private void add_pair_w_rel_type() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //
        //     var Dislikes = world.Entity();
        //     var Bob = world.Entity();
        //     var Alice = world.Entity();
        //
        //     var q = world.filter_builder<Self>()
        //         .Term<Likes>(flecs::Wildcard)
        //         .build();
        //
        //     var
        //     e = world.Entity().Add<Likes>(Alice); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(Dislikes, Alice); e.set<Self>({0});
        //
        //     e = world.Entity().Add<Likes>(Bob); e.Set(new Self { Value = e });
        //     e = world.Entity().Add(Dislikes, Bob); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // private void template_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>()
        //         .Term<Template<int>>()
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Template<int>>();
        //     world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_subject_w_id() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>()
        //         .Term<Position>().id(flecs::This)
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_subject_w_type() {
        //     using World world = World.Create();
        //
        //     world.Set(new Position { X = 10, Y = 20 });
        //
        //     var q = world.filter_builder<Position>()
        //         .Term<Position>().src<Position>()
        //         .build();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p) {
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //         Assert.True(e == world.singleton<Position>());
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_object_w_id() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity("Likes");
        //     var Alice = world.Entity("Alice");
        //     var Bob = world.Entity("Bob");
        //
        //     var q = world.filter_builder<>()
        //         .term(Likes).second(Alice)
        //         .build();
        //
        //     var e1 = world.Entity().Add(Likes, Alice);
        //     world.Entity().Add(Likes, Bob);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_object_w_type() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     struct Alice { };
        //     var Bob = world.Entity();
        //
        //     var q = world.filter_builder<>()
        //         .term(Likes).second<Alice>()
        //         .build();
        //
        //     var e1 = world.Entity().Add(Likes, world.id<Alice>());
        //     world.Entity().Add(Likes, Bob);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<>()
        //         .term(world.Term<Position>())
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_term_w_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<>()
        //         .term(world.Term<Position>())
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_term_w_pair_type() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     struct Alice { };
        //     struct Bob { };
        //
        //     var q = world.filter_builder<>()
        //         .term(world.Term<Likes, Alice>())
        //         .build();
        //
        //     var e1 = world.Entity().Add<Likes, Alice>();
        //     world.Entity().Add<Likes, Bob>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_term_w_id() {
        //     using World world = World.Create();
        //
        //     var Apples = world.Entity();
        //     var Pears = world.Entity();
        //
        //     var q = world.filter_builder<>()
        //         .term(world.term(Apples))
        //         .build();
        //
        //     var e1 = world.Entity().Add(Apples);
        //     world.Entity().Add(Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void explicit_term_w_pair_id() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     var Apples = world.Entity();
        //     var Pears = world.Entity();
        //
        //     var q = world.filter_builder<>()
        //         .term(world.term(Likes, Apples))
        //         .build();
        //
        //     var e1 = world.Entity().Add(Likes, Apples);
        //     world.Entity().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void 1_term_to_empty() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     var Apples = world.Entity();
        //
        //     var qb = world.filter_builder<>()
        //         .Term<Position>();
        //
        //     qb.term(Likes, Apples);
        //
        //     var q = qb.build();
        //
        //     Assert.Equal(q.field_count(), 2);
        //     Assert.Equal(q.term(0).id(), world.id<Position>());
        //     Assert.Equal(q.term(1).id(), world.pair(Likes, Apples));
        // }
        //
        // [Fact]
        // private void 2_subsequent_args() {
        //     using World world = World.Create();
        //
        //     struct Rel { int foo; };
        //
        //     int count = 0;
        //
        //     var s = world.system<Rel, const Velocity>()
        //         .arg(1).second(flecs::Wildcard)
        //         .arg(2).singleton()
        //         .iter([&](flecs::iter it){
        //             count += it.count();
        //         });
        //
        //     world.Entity().Add<Rel, Tag>();
        //     world.set<Velocity>({});
        //
        //     s.run();
        //
        //     Assert.Equal(1, count);
        // }
        //
        // static
        // int filter_arg(flecs::filter<Self> f) {
        //     int count = 0;
        //
        //     f.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     return count;
        // }
        //
        // [Fact]
        // private void filter_as_arg() {
        //     using World world = World.Create();
        //
        //     var f = world.filter<Self>();
        //
        //     var e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     Assert.Equal(filter_arg(f), 3);
        // }
        //
        // static
        // int filter_move_arg(flecs::filter<Self>&& f) {
        //     int count = 0;
        //
        //     f.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     return count;
        // }
        //
        // [Fact]
        // private void filter_as_move_arg() {
        //     using World world = World.Create();
        //
        //     var f = world.filter<Self>();
        //
        //     var e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     Assert.Equal(filter_move_arg(world.filter<Self>()), 3);
        // }
        //
        // static
        // flecs::filter<Self> filter_return(flecs::world& ecs) {
        //     return world.filter<Self>();
        // }
        //
        // [Fact]
        // private void filter_as_return() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     var f = filter_return(ecs);
        //
        //     int count = 0;
        //
        //     f.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void filter_copy() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     var f = world.filter<Self>();
        //
        //     var f_2 = f;
        //
        //     int count = 0;
        //
        //     f_2.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void world_each_filter_1_component() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e });
        //
        //     int count = 0;
        //
        //     world.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void world_each_filter_2_components() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.Set(new Self { Value = e })
        //      .Set(new Position { X = 10, Y = 20 });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e })
        //         .Set(new Position { X = 10, Y = 20 });
        //
        //     e = world.Entity();
        //     e.Set(new Self { Value = e })
        //      .Set(new Position { X = 10, Y = 20 });
        //
        //     int count = 0;
        //
        //     world.each([&](flecs::entity e, Self& s, Position& p) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void world_each_filter_1_component_no_entity() {
        //     using World world = World.Create();
        //
        //     world.Entity()
        //         .Set(new Position { X = 10, Y = 20 });
        //
        //     world.Entity()
        //         .Set(new Position { X = 10, Y = 20 });
        //
        //     world.Entity()
        //         .Set(new Position { X = 10, Y = 20 })
        //         .set<Velocity>({1, 2});
        //
        //     int count = 0;
        //
        //     world.each([&](Position& p) {
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void world_each_filter_2_components_no_entity() {
        //     using World world = World.Create();
        //
        //     world.Entity()
        //         .Set(new Position { X = 10, Y = 20 })
        //         .set<Velocity>({1, 2});
        //
        //     world.Entity()
        //         .Set(new Position { X = 10, Y = 20 })
        //         .set<Velocity>({1, 2});
        //
        //     world.Entity()
        //         .Set(new Position { X = 10, Y = 20 })
        //         .set<Velocity>({1, 2});
        //
        //     world.Entity()
        //         .set<Position>({3, 5});
        //
        //     world.Entity()
        //         .set<Velocity>({20, 40});
        //
        //     int count = 0;
        //
        //     world.each([&](Position& p, Velocity& v) {
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         Assert.Equal(v.x, 1);
        //         Assert.Equal(v.y, 2);
        //         count ++;
        //     });
        //
        //     Assert.Equal(3, count);
        // }
        //
        // [Fact]
        // private void 10_terms() {
        //     using World world = World.Create();
        //
        //     var f = world.filter_builder<>()
        //         .Term<TagA>()
        //         .Term<TagB>()
        //         .Term<TagC>()
        //         .Term<TagD>()
        //         .Term<TagE>()
        //         .Term<TagF>()
        //         .Term<TagG>()
        //         .Term<TagH>()
        //         .Term<TagI>()
        //         .Term<TagJ>()
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 10);
        //
        //     var e = world.Entity()
        //         .Add<TagA>()
        //         .Add<TagB>()
        //         .Add<TagC>()
        //         .Add<TagD>()
        //         .Add<TagE>()
        //         .Add<TagF>()
        //         .Add<TagG>()
        //         .Add<TagH>()
        //         .Add<TagI>()
        //         .Add<TagJ>();
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         Assert.Equal(it.count(), 1);
        //         Assert.True(it.Entity(0) == e);
        //         Assert.Equal(it.field_count(), 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void 20_terms() {
        //     using World world = World.Create();
        //
        //     var f = world.filter_builder<>()
        //         .Term<TagA>()
        //         .Term<TagB>()
        //         .Term<TagC>()
        //         .Term<TagD>()
        //         .Term<TagE>()
        //         .Term<TagF>()
        //         .Term<TagG>()
        //         .Term<TagH>()
        //         .Term<TagI>()
        //         .Term<TagJ>()
        //         .Term<TagK>()
        //         .Term<TagL>()
        //         .Term<TagM>()
        //         .Term<TagN>()
        //         .Term<TagO>()
        //         .Term<TagP>()
        //         .Term<TagQ>()
        //         .Term<TagR>()
        //         .Term<TagS>()
        //         .Term<TagT>()
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 20);
        //
        //     var e = world.Entity()
        //         .Add<TagA>()
        //         .Add<TagB>()
        //         .Add<TagC>()
        //         .Add<TagD>()
        //         .Add<TagE>()
        //         .Add<TagF>()
        //         .Add<TagG>()
        //         .Add<TagH>()
        //         .Add<TagI>()
        //         .Add<TagJ>()
        //         .Add<TagK>()
        //         .Add<TagL>()
        //         .Add<TagM>()
        //         .Add<TagN>()
        //         .Add<TagO>()
        //         .Add<TagP>()
        //         .Add<TagQ>()
        //         .Add<TagR>()
        //         .Add<TagS>()
        //         .Add<TagT>();
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         Assert.Equal(it.count(), 1);
        //         Assert.True(it.Entity(0) == e);
        //         Assert.Equal(it.field_count(), 20);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void term_after_arg() {
        //     using World world = World.Create();
        //
        //     var e_1 = world.Entity()
        //         .Add<TagA>()
        //         .Add<TagB>()
        //         .Add<TagC>();
        //
        //     world.Entity()
        //         .Add<TagA>()
        //         .Add<TagB>();
        //
        //     var f = world.filter_builder<TagA, TagB>()
        //         .arg(1).src(flecs::This) // dummy
        //         .Term<TagC>()
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 3);
        //
        //     int count = 0;
        //     f.each([&](flecs::entity e, TagA, TagB) {
        //         Assert.True(e == e_1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void name_arg() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity("Foo").Set(new Position { X = 10, Y = 20 });
        //
        //     var f = world.filter_builder<Position>()
        //         .arg(1).src().name("Foo")
        //         .build();
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it, Position* p) {
        //         count ++;
        //         Assert.Equal(p->x, 10);
        //         Assert.Equal(p->y, 20);
        //         Assert.True(it.src(1) == e);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void const_in_term() {
        //     using World world = World.Create();
        //
        //     world.Entity().Set(new Position { X = 10, Y = 20 });
        //
        //     var f = world.filter_builder<>()
        //         .Term<const Position>()
        //         .build();
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         var p = it.field<const Position>(1);
        //         Assert.True(it.is_readonly(1));
        //         foreach (int i in it) {
        //             count ++;
        //             Assert.Equal(p[i].x, 10);
        //             Assert.Equal(p[i].y, 20);
        //         }
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void const_optional() {
        //     using World world = World.Create();
        //
        //  world.Entity().Set(new Position { X = 10, Y = 20 }).Add<TagA>();
        //     world.Entity().Add<TagA>();
        //
        //     var f = world.filter_builder<TagA, const Position*>().build();
        //
        //     int count = 0, set_count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         Assert.Equal(it.count(), 1);
        //         if (it.is_set(2)) {
        //             var p = it.field<const Position>(2);
        //             Assert.True(it.is_readonly(2));
        //             Assert.Equal(p->x, 10);
        //             Assert.Equal(p->y, 20);
        //             set_count ++;
        //         }
        //         count++;
        //  });
        //
        //     Assert.Equal(2, count);
        //     Assert.Equal(set_count, 1);
        // }
        //
        // [Fact]
        // private void create_w_no_template_args() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder().Term<Position>().build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void 2_terms_w_expr() {
        //     using World world = World.Create();
        //
        //     var a = world.Entity("A");
        //     var b = world.Entity("B");
        //
        //     var e1 = world.Entity().Add(a).Add(b);
        //
        //     var f = world.filter_builder()
        //         .expr("A, B")
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 2);
        //
        //     int count = 0;
        //     f.each([&](flecs::iter& it, size_t index) {
        //         if (it.Entity(index) == e1) {
        //             Assert.True(it.id(1) == a);
        //             Assert.True(it.id(2) == b);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void assert_on_uninitialized_term() {
        //     install_test_abort();
        //
        //     using World world = World.Create();
        //
        //     world.Entity("A");
        //     world.Entity("B");
        //
        //     test_expect_abort();
        //
        //     var f = world.filter_builder()
        //         .term()
        //         .term()
        //         .build();
        // }
        //
        // [Fact]
        // private void operator_shortcuts() {
        //     using World world = World.Create();
        //
        //     flecs::entity a = world.Entity();
        //     flecs::entity b = world.Entity();
        //     flecs::entity c = world.Entity();
        //     flecs::entity d = world.Entity();
        //     flecs::entity e = world.Entity();
        //     flecs::entity f = world.Entity();
        //     flecs::entity g = world.Entity();
        //     flecs::entity h = world.Entity();
        //
        //     var filter = world.filter_builder()
        //         .term(a).and_()
        //         .term(b).or_()
        //         .term(c)
        //         .term(d).not_()
        //         .term(e).optional()
        //         .term(f).and_from()
        //         .term(g).or_from()
        //         .term(h).not_from()
        //         .build();
        //
        //     var t = filter.term(0);
        //     Assert.Equal(t.id(), a);
        //     Assert.Equal(t.Oper(), flecs::And);
        //
        //     t = filter.term(1);
        //     Assert.Equal(t.id(), b);
        //     Assert.Equal(t.Oper(), flecs::Or);
        //
        //     t = filter.term(2);
        //     Assert.Equal(t.id(), c);
        //     Assert.Equal(t.Oper(), flecs::And);
        //
        //     t = filter.term(3);
        //     Assert.Equal(t.id(), d);
        //     Assert.Equal(t.Oper(), flecs::Not);
        //
        //     t = filter.term(4);
        //     Assert.Equal(t.id(), e);
        //     Assert.Equal(t.Oper(), flecs::Optional);
        //
        //     t = filter.term(5);
        //     Assert.Equal(t.id(), f);
        //     Assert.Equal(t.Oper(), flecs::AndFrom);
        //
        //     t = filter.term(6);
        //     Assert.Equal(t.id(), g);
        //     Assert.Equal(t.Oper(), flecs::OrFrom);
        //
        //     t = filter.term(7);
        //     Assert.Equal(t.id(), h);
        //     Assert.Equal(t.Oper(), flecs::NotFrom);
        // }
        //
        // [Fact]
        // private void inout_shortcuts() {
        //     using World world = World.Create();
        //
        //     flecs::entity a = world.Entity();
        //     flecs::entity b = world.Entity();
        //     flecs::entity c = world.Entity();
        //     flecs::entity d = world.Entity();
        //
        //     var filter = world.filter_builder()
        //         .term(a).in()
        //         .term(b).out()
        //         .term(c).inout()
        //         .term(d).inout_none()
        //         .build();
        //
        //     var t = filter.term(0);
        //     Assert.Equal(t.id(), a);
        //     Assert.Equal(t.inout(), flecs::In);
        //
        //     t = filter.term(1);
        //     Assert.Equal(t.id(), b);
        //     Assert.Equal(t.inout(), flecs::Out);
        //
        //     t = filter.term(2);
        //     Assert.Equal(t.id(), c);
        //     Assert.Equal(t.inout(), flecs::InOut);
        //
        //     t = filter.term(3);
        //     Assert.Equal(t.id(), d);
        //     Assert.Equal(t.inout(), flecs::InOutNone);
        // }
        //
        // [Fact]
        // private void iter_column_w_const_as_array() {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var e1 = world.Entity().Set(new Position { X = 10, Y = 20 });
        //     var e2 = world.Entity().set<Position>({20, 30});
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         const var p = it.field<Position>(1);
        //         foreach (int i in it) {
        //             p[i].x += 1;
        //             p[i].y += 2;
        //
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(2, count);
        //
        //     const Position *p = e1.Get<Position>();
        //     Assert.Equal(p->x, 11);
        //     Assert.Equal(p->y, 22);
        //
        //     p = e2.Get<Position>();
        //     Assert.Equal(p->x, 21);
        //     Assert.Equal(p->y, 32);
        // }
        //
        // [Fact]
        // private void iter_column_w_const_as_ptr() {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var @base = world.prefab().Set(new Position { X = 10, Y = 20 });
        //     world.Entity().is_a(@base);
        //     world.Entity().is_a(@base);
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         const var p = it.field<Position>(1);
        //         for (size_t i = 0; i < it.count(); i ++) {
        //             Assert.Equal(p->x, 10);
        //             Assert.Equal(p->y, 20);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // private void iter_column_w_const_deref() {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var @base = world.prefab().Set(new Position { X = 10, Y = 20 });
        //     world.Entity().is_a(@base);
        //     world.Entity().is_a(@base);
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         const var p = it.field<Position>(1);
        //         Position pv = *p;
        //         for (size_t i = 0; i < it.count(); i ++) {
        //             Assert.Equal(pv.x, 10);
        //             Assert.Equal(pv.y, 20);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // private void with_id() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with(world.id<Position>())
        //             .with(world.id<Velocity>())
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_name() {
        //     using World world = World.Create();
        //
        //     world.Component<Velocity>();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with("Velocity")
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_component() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with<Velocity>()
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity();
        //     flecs::entity Apples = world.Entity();
        //     flecs::entity Pears = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with(Likes, Apples)
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add(Likes, Apples);
        //     world.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_pair_name() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity("Likes");
        //     flecs::entity Apples = world.Entity("Apples");
        //     flecs::entity Pears = world.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with("Likes", "Apples")
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add(Likes, Apples);
        //     world.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_pair_components() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //     struct Pears { };
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with<Likes, Apples>()
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Likes, Apples>();
        //     world.Entity().Add<Position>().Add<Likes, Pears>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_pair_component_id() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity();
        //     flecs::entity Pears = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with<Likes>(Apples)
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Likes>(Apples);
        //     world.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_pair_component_name() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity("Apples");
        //     flecs::entity Pears = world.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with<Likes>("Apples")
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add<Likes>(Apples);
        //     world.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void with_enum() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with(Green)
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().Add(Green);
        //     world.Entity().Add<Position>().Add(Red);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_id() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with(world.id<Position>())
        //             .without(world.id<Velocity>())
        //             .build();
        //
        //     world.Entity().Add<Position>().Add<Velocity>();
        //     var e2 = world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_name() {
        //     using World world = World.Create();
        //
        //     world.Component<Velocity>();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with(world.id<Position>())
        //             .without("Velocity")
        //             .build();
        //
        //     world.Entity().Add<Position>().Add<Velocity>();
        //     var e2 = world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_component() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without<Velocity>()
        //             .build();
        //
        //     world.Entity().Add<Position>().Add<Velocity>();
        //     var e2 = world.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity();
        //     flecs::entity Apples = world.Entity();
        //     flecs::entity Pears = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without(Likes, Apples)
        //             .build();
        //
        //     world.Entity().Add<Position>().Add(Likes, Apples);
        //     var e2 = world.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_pair_name() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity("Likes");
        //     flecs::entity Apples = world.Entity("Apples");
        //     flecs::entity Pears = world.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without("Likes", "Apples")
        //             .build();
        //
        //     world.Entity().Add<Position>().Add(Likes, Apples);
        //     var e2 = world.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_pair_components() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //     struct Pears { };
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without<Likes, Apples>()
        //             .build();
        //
        //     world.Entity().Add<Position>().Add<Likes, Apples>();
        //     var e2 = world.Entity().Add<Position>().Add<Likes, Pears>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_pair_component_id() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity();
        //     flecs::entity Pears = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without<Likes>(Apples)
        //             .build();
        //
        //     world.Entity().Add<Position>().Add<Likes>(Apples);
        //     var e2 = world.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_pair_component_name() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity("Apples");
        //     flecs::entity Pears = world.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without<Likes>("Apples")
        //             .build();
        //
        //     world.Entity().Add<Position>().Add<Likes>(Apples);
        //     var e2 = world.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void without_enum() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without(Green)
        //             .build();
        //
        //     world.Entity().Add<Position>().Add(Green);
        //     var e2 = world.Entity().Add<Position>().Add(Red);
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void write_id() {
        //     using World world = World.Create();
        //
        //     var q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write(world.id<Position>())
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_name() {
        //     using World world = World.Create();
        //
        //     world.Component<Position>();
        //
        //     var q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write("Position")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_component() {
        //     using World world = World.Create();
        //
        //     world.Component<Position>();
        //
        //     var q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write<Position>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity();
        //     flecs::entity Apples = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write(Likes, Apples)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == Likes);
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_pair_name() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity("Likes");
        //     flecs::entity Apples = world.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write("Likes", "Apples")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == Likes);
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_pair_components() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write<Likes, Apples>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Likes>());
        //     Assert.True(q.term(1).get_second() == world.id<Apples>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_pair_component_id() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write<Likes>(Apples)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_pair_component_name() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write<Likes>("Apples")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void write_enum() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .write(Green)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == world.id<Color>());
        //     Assert.True(q.term(1).get_second() == world.to_entity<Color>(Green));
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_id() {
        //     using World world = World.Create();
        //
        //     var q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read(world.id<Position>())
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_name() {
        //     using World world = World.Create();
        //
        //     world.Component<Position>();
        //
        //     var q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read("Position")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_component() {
        //     using World world = World.Create();
        //
        //     world.Component<Position>();
        //
        //     var q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read<Position>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity();
        //     flecs::entity Apples = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read(Likes, Apples)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == Likes);
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_pair_name() {
        //     using World world = World.Create();
        //
        //     flecs::entity Likes = world.Entity("Likes");
        //     flecs::entity Apples = world.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read("Likes", "Apples")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == Likes);
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_pair_components() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read<Likes, Apples>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Likes>());
        //     Assert.True(q.term(1).get_second() == world.id<Apples>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_pair_component_id() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read<Likes>(Apples)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_pair_component_name() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = world.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read<Likes>("Apples")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void read_enum() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .read(Green)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == world.id<Color>());
        //     Assert.True(q.term(1).get_second() == world.to_entity<Color>(Green));
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // private void assign_after_init() {
        //     using World world = World.Create();
        //
        //     flecs::filter<> f;
        //     flecs::filter_builder<> fb = world.filter_builder();
        //     fb.with<Position>();
        //     f = fb.build();
        //
        //     flecs::entity e1 = world.Entity().Set(new Position { X = 10, Y = 20 });
        //
        //     int count = 0;
        //     f.each([&](flecs::entity e) {
        //         Assert.True(e == e1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void iter_w_stage() {
        //     using World world = World.Create();
        //
        //     world.set_stage_count(2);
        //     flecs::world stage = world.get_stage(1);
        //
        //     var e1 = world.Entity().Add<Position>();
        //
        //     var q = world.filter<Position>();
        //
        //     int count = 0;
        //     q.each(stage, [&](flecs::iter& it, size_t i, Position&) {
        //         Assert.True(it.world() == stage);
        //         Assert.True(it.Entity(i) == e1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
    }
}
