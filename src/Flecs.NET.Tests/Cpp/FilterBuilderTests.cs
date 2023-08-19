using Flecs.NET.Core;
using Xunit;

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

            Filter filter = world.Filter(
                filterBuilder: world.FilterBuilder()
                    .Term<Position>()
                    .Term<Velocity>()
            );

            Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
            Entity _ = world.Entity().Add<Position>();

            int count = 0;

            filter.Iter(iter =>
            {
                count += iter.Count();
                Assert.True(iter.Entity(0) == e1);
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
                    filterBuilder: world.FilterBuilder().Term<Position>()
                )
            });

            int count = 0;

            f.Get<FilterWrapper>().Filter.Iter(iter =>
            {
                Assert.True(iter.Entity(0) == e1);
                count++;
            });

            Assert.Equal(1, count);
        }

        //
        // [Fact]
        // private void builder_build_n_statements() {
        //     using World world = World.Create();
        //
        //     var qb = world.filter_builder<>();
        //     qb.term<Position>();
        //     qb.term<Velocity>();
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
        //         .term<Position>()
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
        //         .term<Position>()
        //         .term<Velocity>()
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
        //         .term<Velocity>()
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
        //         .term<Velocity>()
        //         .term<Mass>()
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
        // [Fact]
        // private void add_pair() {
        //     using World world = World.Create();
        //
        //     var Likes = world.Entity();
        //     var Bob = world.Entity();
        //     var Alice = world.Entity();
        //
        //     var q = world.filter_builder<>()
        //         .term(Likes, Bob)
        //         .build();
        //
        //     var e1 = world.Entity().add(Likes, Bob);
        //     world.Entity().add(Likes, Alice);
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
        // private void add_not() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>()
        //         .term<Velocity>().oper(flecs::Not)
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     world.Entity().Add<Position>().Add<Velocity>();
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
        // private void add_or() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<>()
        //         .term<Position>().oper(flecs::Or)
        //         .term<Velocity>()
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     var e2 = world.Entity().Add<Velocity>();
        //     world.Entity().Add<Mass>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1 || e == e2);
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void add_optional() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<>()
        //         .term<Position>()
        //         .term<Velocity>().oper(flecs::Optional)
        //         .build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     var e2 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Velocity>().Add<Mass>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e) {
        //         count ++;
        //         Assert.True(e == e1 || e == e2);
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void ptr_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position, Velocity*>().build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     var e2 = world.Entity().Add<Position>().Add<Velocity>();
        //     world.Entity().Add<Velocity>().Add<Mass>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, Position& p, Velocity* v) {
        //         count ++;
        //         Assert.True(e == e1 || e == e2);
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void const_type() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<const Position>().build();
        //
        //     var e1 = world.Entity().Add<Position>();
        //     world.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.each([&](flecs::entity e, const Position& p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // private void string_term() {
        //     using World world = World.Create();
        //
        //     world.component<Position>();
        //
        //     var q = world.filter_builder<>()
        //         .expr("Position")
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
        // private void singleton_term() {
        //     using World world = World.Create();
        //
        //     world.set<Other>({10});
        //
        //     var q = world.filter_builder<Self>()
        //         .term<Other>().singleton()
        //         .build();
        //
        //     auto
        //     e = world.Entity(); e.set<Self>({e});
        //     e = world.Entity(); e.set<Self>({e});
        //     e = world.Entity(); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //         Assert.True(!it.is_self(2));
        //         Assert.Equal(o->value, 10);
        //
        //         const Other& o_ref = *o;
        //         Assert.Equal(o_ref.value, 10);
        //
        //         for (var i : it) {
        //             Assert.True(it.entity(i) == s[i].value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void isa_superset_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self>()
        //         .term<Other>().src().up()
        //         .build();
        //
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //         Assert.True(!it.is_self(2));
        //         Assert.Equal(o->value, 10);
        //
        //         for (var i : it) {
        //             Assert.True(it.entity(i) == s[i].value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void isa_self_superset_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self>()
        //         .term<Other>().src().self().up()
        //         .build();
        //
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().set<Other>({20}); e.set<Self>({e});
        //     e = world.Entity().set<Other>({20}); e.set<Self>({e});
        //
        //     int count = 0;
        //     int owned_count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //
        //         if (!it.is_self(2)) {
        //             Assert.Equal(o->value, 10);
        //         } else {
        //             for (var i : it) {
        //                 Assert.Equal(o[i].value, 20);
        //                 owned_count ++;
        //             }
        //         }
        //
        //         for (var i : it) {
        //             Assert.True(it.entity(i) == s[i].value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 5);
        //     Assert.Equal(owned_count, 2);
        // }
        //
        // [Fact]
        // private void childof_superset_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self>()
        //         .term<Other>().src().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //         Assert.True(!it.is_self(2));
        //         Assert.Equal(o->value, 10);
        //
        //         for (var i : it) {
        //             Assert.True(it.entity(i) == s[i].value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void childof_self_superset_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self>()
        //         .term<Other>().src().self().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().set<Other>({20}); e.set<Self>({e});
        //     e = world.Entity().set<Other>({20}); e.set<Self>({e});
        //
        //     int count = 0;
        //     int owned_count = 0;
        //
        //     q.iter([&](flecs::iter& it, Self *s) {
        //         var o = it.field<const Other>(2);
        //
        //         if (!it.is_self(2)) {
        //             Assert.Equal(o->value, 10);
        //         } else {
        //             for (var i : it) {
        //                 Assert.Equal(o[i].value, 20);
        //                 owned_count ++;
        //             }
        //         }
        //
        //         for (var i : it) {
        //             Assert.True(it.entity(i) == s[i].value);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().add(flecs::IsA, base); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().is_a(base); e.set<Self>({e});
        //     e = world.Entity().is_a(base); e.set<Self>({e});
        //     e = world.Entity().is_a(base); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void isa_superset_shortcut_w_self() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Self, Other>()
        //         .arg(2).self().up(flecs::IsA)
        //         .build();
        //
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().is_a(base); e.set<Self>({e});
        //     e = world.Entity().is_a(base); e.set<Self>({e});
        //     e = world.Entity().is_a(base); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
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
        //     var base = world.Entity().set<Other>({10});
        //
        //     auto
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().child_of(base); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //     e = world.Entity().set<Other>({10}); e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s, Other& o) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(o.value, 10);
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
        //     auto
        //     e = world.Entity().add(Likes, Bob); e.set<Self>({e});
        //     e = world.Entity().add(Likes, Bob); e.set<Self>({e});
        //
        //     e = world.Entity().add(Likes, Alice); e.set<Self>({0});
        //     e = world.Entity().add(Likes, Alice); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
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
        //     auto
        //     e = world.Entity().add(Likes, Bob); e.set<Self>({e});
        //     e = world.Entity().add(Likes, Bob); e.set<Self>({e});
        //
        //     e = world.Entity().add(Likes, Alice); e.set<Self>({e});
        //     e = world.Entity().add(Likes, Alice); e.set<Self>({e});
        //
        //     e = world.Entity(); e.set<Self>({0});
        //     e = world.Entity(); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
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
        //     auto
        //     e = world.Entity().add(Likes, Alice); e.set<Self>({e});
        //     e = world.Entity().add(Dislikes, Alice); e.set<Self>({e});
        //
        //     e = world.Entity().add(Likes, Bob); e.set<Self>({0});
        //     e = world.Entity().add(Dislikes, Bob); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
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
        //         .term<Likes>(flecs::Wildcard)
        //         .build();
        //
        //     auto
        //     e = world.Entity().Add<Likes>(Alice); e.set<Self>({e});
        //     e = world.Entity().add(Dislikes, Alice); e.set<Self>({0});
        //
        //     e = world.Entity().Add<Likes>(Bob); e.set<Self>({e});
        //     e = world.Entity().add(Dislikes, Bob); e.set<Self>({0});
        //
        //     int count = 0;
        //
        //     q.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void template_term() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder<Position>()
        //         .term<Template<int>>()
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
        //         .term<Position>().id(flecs::This)
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
        //         .term<Position>().src<Position>()
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
        //     var Likes = world.entity("Likes");
        //     var Alice = world.entity("Alice");
        //     var Bob = world.entity("Bob");
        //
        //     var q = world.filter_builder<>()
        //         .term(Likes).second(Alice)
        //         .build();
        //
        //     var e1 = world.Entity().add(Likes, Alice);
        //     world.Entity().add(Likes, Bob);
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
        //     var e1 = world.Entity().add(Likes, world.id<Alice>());
        //     world.Entity().add(Likes, Bob);
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
        //         .term(world.term<Position>())
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
        //         .term(world.term<Position>())
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
        //         .term(world.term<Likes, Alice>())
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
        //     var e1 = world.Entity().add(Apples);
        //     world.Entity().add(Pears);
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
        //     var e1 = world.Entity().add(Likes, Apples);
        //     world.Entity().add(Likes, Pears);
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
        //         .term<Position>();
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
        //         Assert.True(e == s.value);
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
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     Assert.Equal(filter_arg(f), 3);
        // }
        //
        // static
        // int filter_move_arg(flecs::filter<Self>&& f) {
        //     int count = 0;
        //
        //     f.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
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
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
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
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     var f = filter_return(ecs);
        //
        //     int count = 0;
        //
        //     f.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void filter_copy() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     var f = world.filter<Self>();
        //
        //     var f_2 = f;
        //
        //     int count = 0;
        //
        //     f_2.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void world_each_filter_1_component() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     e = world.Entity();
        //     e.set<Self>({e});
        //
        //     int count = 0;
        //
        //     world.each([&](flecs::entity e, Self& s) {
        //         Assert.True(e == s.value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void world_each_filter_2_components() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity();
        //     e.set<Self>({e})
        //      .Set(new Position { X = 10, Y = 20 });
        //
        //     e = world.Entity();
        //     e.set<Self>({e})
        //         .Set(new Position { X = 10, Y = 20 });
        //
        //     e = world.Entity();
        //     e.set<Self>({e})
        //      .Set(new Position { X = 10, Y = 20 });
        //
        //     int count = 0;
        //
        //     world.each([&](flecs::entity e, Self& s, Position& p) {
        //         Assert.True(e == s.value);
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
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
        //     Assert.Equal(count, 3);
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
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // private void 10_terms() {
        //     using World world = World.Create();
        //
        //     var f = world.filter_builder<>()
        //         .term<TagA>()
        //         .term<TagB>()
        //         .term<TagC>()
        //         .term<TagD>()
        //         .term<TagE>()
        //         .term<TagF>()
        //         .term<TagG>()
        //         .term<TagH>()
        //         .term<TagI>()
        //         .term<TagJ>()
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
        //         Assert.True(it.entity(0) == e);
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
        //         .term<TagA>()
        //         .term<TagB>()
        //         .term<TagC>()
        //         .term<TagD>()
        //         .term<TagE>()
        //         .term<TagF>()
        //         .term<TagG>()
        //         .term<TagH>()
        //         .term<TagI>()
        //         .term<TagJ>()
        //         .term<TagK>()
        //         .term<TagL>()
        //         .term<TagM>()
        //         .term<TagN>()
        //         .term<TagO>()
        //         .term<TagP>()
        //         .term<TagQ>()
        //         .term<TagR>()
        //         .term<TagS>()
        //         .term<TagT>()
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
        //         Assert.True(it.entity(0) == e);
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
        //         .term<TagC>()
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
        //     var e = world.entity("Foo").Set(new Position { X = 10, Y = 20 });
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
        //         .term<const Position>()
        //         .build();
        //
        //     int count = 0;
        //     f.iter([&](flecs::iter& it) {
        //         var p = it.field<const Position>(1);
        //         Assert.True(it.is_readonly(1));
        //         for (var i : it) {
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
        //     Assert.Equal(count, 2);
        //     Assert.Equal(set_count, 1);
        // }
        //
        // [Fact]
        // private void create_w_no_template_args() {
        //     using World world = World.Create();
        //
        //     var q = world.filter_builder().term<Position>().build();
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
        //     var a = world.entity("A");
        //     var b = world.entity("B");
        //
        //     var e1 = world.Entity().add(a).add(b);
        //
        //     var f = world.filter_builder()
        //         .expr("A, B")
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 2);
        //
        //     int count = 0;
        //     f.each([&](flecs::iter& it, size_t index) {
        //         if (it.entity(index) == e1) {
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
        //     world.entity("A");
        //     world.entity("B");
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
        //     Assert.Equal(t.oper(), flecs::And);
        //
        //     t = filter.term(1);
        //     Assert.Equal(t.id(), b);
        //     Assert.Equal(t.oper(), flecs::Or);
        //
        //     t = filter.term(2);
        //     Assert.Equal(t.id(), c);
        //     Assert.Equal(t.oper(), flecs::And);
        //
        //     t = filter.term(3);
        //     Assert.Equal(t.id(), d);
        //     Assert.Equal(t.oper(), flecs::Not);
        //
        //     t = filter.term(4);
        //     Assert.Equal(t.id(), e);
        //     Assert.Equal(t.oper(), flecs::Optional);
        //
        //     t = filter.term(5);
        //     Assert.Equal(t.id(), f);
        //     Assert.Equal(t.oper(), flecs::AndFrom);
        //
        //     t = filter.term(6);
        //     Assert.Equal(t.id(), g);
        //     Assert.Equal(t.oper(), flecs::OrFrom);
        //
        //     t = filter.term(7);
        //     Assert.Equal(t.id(), h);
        //     Assert.Equal(t.oper(), flecs::NotFrom);
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
        //         for (var i : it) {
        //             p[i].x += 1;
        //             p[i].y += 2;
        //
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 2);
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
        //     var base = world.prefab().Set(new Position { X = 10, Y = 20 });
        //     world.Entity().is_a(base);
        //     world.Entity().is_a(base);
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
        //     Assert.Equal(count, 2);
        // }
        //
        // [Fact]
        // private void iter_column_w_const_deref() {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var base = world.prefab().Set(new Position { X = 10, Y = 20 });
        //     world.Entity().is_a(base);
        //     world.Entity().is_a(base);
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
        //     Assert.Equal(count, 2);
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
        //     world.component<Velocity>();
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
        //     var e1 = world.Entity().Add<Position>().add(Likes, Apples);
        //     world.Entity().Add<Position>().add(Likes, Pears);
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
        //     flecs::entity Likes = world.entity("Likes");
        //     flecs::entity Apples = world.entity("Apples");
        //     flecs::entity Pears = world.entity("Pears");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .with("Likes", "Apples")
        //             .build();
        //
        //     var e1 = world.Entity().Add<Position>().add(Likes, Apples);
        //     world.Entity().Add<Position>().add(Likes, Pears);
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
        //     flecs::entity Apples = world.entity("Apples");
        //     flecs::entity Pears = world.entity("Pears");
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
        //     var e1 = world.Entity().Add<Position>().add(Green);
        //     world.Entity().Add<Position>().add(Red);
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
        //     world.component<Velocity>();
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
        //     world.Entity().Add<Position>().add(Likes, Apples);
        //     var e2 = world.Entity().Add<Position>().add(Likes, Pears);
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
        //     flecs::entity Likes = world.entity("Likes");
        //     flecs::entity Apples = world.entity("Apples");
        //     flecs::entity Pears = world.entity("Pears");
        //
        //     flecs::filter<> q =
        //         world.filter_builder()
        //             .with<Position>()
        //             .without("Likes", "Apples")
        //             .build();
        //
        //     world.Entity().Add<Position>().add(Likes, Apples);
        //     var e2 = world.Entity().Add<Position>().add(Likes, Pears);
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
        //     flecs::entity Apples = world.entity("Apples");
        //     flecs::entity Pears = world.entity("Pears");
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
        //     world.Entity().Add<Position>().add(Green);
        //     var e2 = world.Entity().Add<Position>().add(Red);
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
        //     world.component<Position>();
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
        //     world.component<Position>();
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
        //     flecs::entity Likes = world.entity("Likes");
        //     flecs::entity Apples = world.entity("Apples");
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
        //     flecs::entity Apples = world.entity("Apples");
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
        //     world.component<Position>();
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
        //     world.component<Position>();
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
        //     flecs::entity Likes = world.entity("Likes");
        //     flecs::entity Apples = world.entity("Apples");
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
        //     flecs::entity Apples = world.entity("Apples");
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
        //         Assert.True(it.entity(i) == e1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
    }
}