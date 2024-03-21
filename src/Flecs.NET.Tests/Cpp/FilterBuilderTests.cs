#region

using Flecs.NET.Core;
using Xunit;

#endregion

namespace Flecs.NET.Tests.Cpp
{
    public class FilterBuilderTests
    {
        public FilterBuilderTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        public void BuilderAssignTypeArguments()
        {
            World ecs = World.Create();

            Filter q = ecs.Filter<Position, Velocity>();

            Entity e1 = ecs.Entity()
                .Add<Position>()
                .Add<Velocity>();

            ecs.Entity()
                .Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        public void BuilderAssignFromEmpty()
        {
            World ecs = World.Create();

            Filter q = ecs.FilterBuilder()
                .Term<Position>()
                .Term<Velocity>()
                .Build();

            Entity e1 = ecs.Entity()
                .Add<Position>()
                .Add<Velocity>();

            ecs.Entity()
                .Add<Position>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        public void AddPair()
        {
            World ecs = World.Create();

            Entity likes = ecs.Entity();
            Entity bob = ecs.Entity();
            Entity alice = ecs.Entity();

            Filter q = ecs.FilterBuilder()
                .Term(likes, bob)
                .Build();

            Entity e1 = ecs.Entity().Add(likes, bob);
            ecs.Entity().Add(likes, alice);

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        public void AddNot()
        {
            World ecs = World.Create();

            Filter q = ecs.FilterBuilder<Position>()
                .Term<Velocity>().Not()
                .Build();

            Entity e1 = ecs.Entity().Add<Position>();

            ecs.Entity()
                .Add<Position>()
                .Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        public void AddOr()
        {
            World ecs = World.Create();

            Filter q = ecs.FilterBuilder()
                .Term<Position>().Or()
                .Term<Velocity>()
                .Build();

            Entity e1 = ecs.Entity().Add<Position>();
            Entity e2 = ecs.Entity().Add<Velocity>();
            ecs.Entity().Add<Mass>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

            Assert.Equal(2, count);
        }

        [Fact]
        public void AddOptional()
        {
            World ecs = World.Create();

            Filter q = ecs.FilterBuilder()
                .Term<Position>()
                .Term<Velocity>().Optional()
                .Build();

            Entity e1 = ecs.Entity().Add<Position>();
            Entity e2 = ecs.Entity().Add<Position>().Add<Velocity>();
            ecs.Entity().Add<Velocity>().Add<Mass>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

            Assert.Equal(2, count);
        }

        //
        // [Fact]
        // public void ptr_type()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Position, Velocity*>().build();
        //
        //     var e1 = ecs.Entity().Add<Position>();
        //     var e2 = ecs.Entity().Add<Position>().Add<Velocity>();
        //     ecs.Entity().Add<Velocity>().Add<Mass>();
        //
        //     int count = 0;
        //     q.Each((Entity e, ref Position p, Velocity* v) {
        //         count ++;
        //         Assert.True(e == e1 || e == e2);
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // public void const_type()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<const Position>().build();
        //
        //     var e1 = ecs.Entity().Add<Position>();
        //     ecs.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.Each((Entity e, const ref Position p) {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        [Fact]
        public void StringTerm()
        {
            World ecs = World.Create();

            ecs.Component<Position>();

            Filter q = ecs.FilterBuilder()
                .Expr("Position")
                .Build();

            Entity e1 = ecs.Entity().Add<Position>();
            ecs.Entity().Add<Velocity>();

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        public void singleton_term()
        {
            World ecs = World.Create();

            ecs.Set(new Other { Value = 10 });

            Filter q = ecs.FilterBuilder<Self>()
                .With<Other>().Singleton()
                .Build();

            Entity
                e = ecs.Entity();
            e.Set(new Self { Value = e });
            e = ecs.Entity();
            e.Set(new Self { Value = e });
            e = ecs.Entity();
            e.Set(new Self { Value = e });

            int count = 0;

            q.Iter((Iter it, Field<Self> s) =>
            {
                Field<Other> o = it.Field<Other>(2);
                Assert.True(!it.IsSelf(2));
                Assert.Equal(10, o[0].Value);

                ref Other oRef = ref o[0];
                Assert.Equal(10, oRef.Value);

                foreach (int i in it)
                {
                    Assert.True(it.Entity(i) == s[i].Value);
                    count++;
                }
            });

            Assert.Equal(3, count);
        }
        //
        // [Fact]
        // public void isa_superSet_term()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .With<Other>().src().up()
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Iter((Iter it, Field<Self> s) => {
        //         var o = it.Field<const Other>(2);
        //         Assert.True(!it.IsSelf(2));
        //         Assert.Equal(o->value, 10);
        //
        //         foreach (int i in it) {
        //             Assert.True(it.Entity(i) == s[i].Value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void isa_self_superSet_term()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .With<Other>().src().self().up()
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 20}); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 20}); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //     int owned_count = 0;
        //
        //     q.Iter((Iter it, Field<Self> s) => {
        //         var o = it.Field<const Other>(2);
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
        // public void childof_superSet_term()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .With<Other>().src().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Iter((Iter it, Field<Self> s) => {
        //         var o = it.Field<const Other>(2);
        //         Assert.True(!it.IsSelf(2));
        //         Assert.Equal(o->value, 10);
        //
        //         foreach (int i in it) {
        //             Assert.True(it.Entity(i) == s[i].Value);
        //             count ++;
        //         }
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void childof_self_superSet_term()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .With<Other>().src().self().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 20}); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 20}); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //     int owned_count = 0;
        //
        //     q.Iter((Iter it, Field<Self> s) => {
        //         var o = it.Field<const Other>(2);
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
        // public void isa_superSet_term_w_each()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).src().up()
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void isa_self_superSet_term_w_each()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).src().self().up()
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(flecs::IsA, base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // public void childof_superSet_term_w_each()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).src().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void childof_self_superSet_term_w_each()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).src().self().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // public void isa_superSet_shortcut()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).up()
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().is_a(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().is_a(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().is_a(base); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void isa_superSet_shortcut_w_self()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).self().up(flecs::IsA)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().is_a(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().is_a(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().is_a(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // public void childof_superSet_shortcut()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).up(flecs::ChildOf)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void childof_superSet_shortcut_w_self()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Self, Other>()
        //         .arg(2).self().up(flecs::ChildOf)
        //         .build();
        //
        //     var base = ecs.Entity().Set(new Other { Value = 10});
        //
        //     var
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().child_of(base); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Set(new Other { Value = 10}); e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s, Other& o) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(o.Value, 10);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 5);
        // }
        //
        // [Fact]
        // public void relation()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity();
        //     var Bob = ecs.Entity();
        //     var Alice = ecs.Entity();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .term(Likes, Bob)
        //         .build();
        //
        //     var
        //     e = ecs.Entity().Add(Likes, Bob); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(Likes, Bob); e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity().Add(Likes, Alice); e.Set(new Self { Value = 0});
        //     e = ecs.Entity().Add(Likes, Alice); e.Set(new Self { Value = 0});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // public void relation_w_object_wildcard()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity();
        //     var Bob = ecs.Entity();
        //     var Alice = ecs.Entity();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .term(Likes, flecs::Wildcard)
        //         .build();
        //
        //     var
        //     e = ecs.Entity().Add(Likes, Bob); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(Likes, Bob); e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity().Add(Likes, Alice); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(Likes, Alice); e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity(); e.Set(new Self { Value = 0});
        //     e = ecs.Entity(); e.Set(new Self { Value = 0});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 4);
        // }
        //
        // [Fact]
        // public void relation_w_predicate_wildcard()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity();
        //     var Dislikes = ecs.Entity();
        //     var Bob = ecs.Entity();
        //     var Alice = ecs.Entity();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .term(flecs::Wildcard, Alice)
        //         .build();
        //
        //     var
        //     e = ecs.Entity().Add(Likes, Alice); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(Dislikes, Alice); e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity().Add(Likes, Bob); e.Set(new Self { Value = 0});
        //     e = ecs.Entity().Add(Dislikes, Bob); e.Set(new Self { Value = 0});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // public void add_pair_w_rel_type()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //
        //     var Dislikes = ecs.Entity();
        //     var Bob = ecs.Entity();
        //     var Alice = ecs.Entity();
        //
        //     var q = ecs.filter_builder<Self>()
        //         .With<Likes>(flecs::Wildcard)
        //         .build();
        //
        //     var
        //     e = ecs.Entity().Add<Likes>(Alice); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(Dislikes, Alice); e.Set(new Self { Value = 0});
        //
        //     e = ecs.Entity().Add<Likes>(Bob); e.Set(new Self { Value = e});
        //     e = ecs.Entity().Add(Dislikes, Bob); e.Set(new Self { Value = 0});
        //
        //     int count = 0;
        //
        //     q.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(2, count);
        // }
        //
        // [Fact]
        // public void template_term()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Position>()
        //         .With<Template<int>>()
        //         .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Template<int>>();
        //     ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e, ref Position p) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_subject_w_id()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<Position>()
        //         .With<Position>().id(flecs::This)
        //         .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Velocity>();
        //     ecs.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.Each((Entity e, ref Position p) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_subject_w_type()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Set<Position>({10, 20});
        //
        //     var q = ecs.filter_builder<Position>()
        //         .With<Position>().src<Position>()
        //         .build();
        //
        //     int count = 0;
        //     q.Each((Entity e, ref Position p) => {
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //         Assert.True(e == ecs.singleton<Position>());
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_object_w_id()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity("Likes");
        //     var Alice = ecs.Entity("Alice");
        //     var Bob = ecs.Entity("Bob");
        //
        //     var q = ecs.filter_builder<>()
        //         .term(Likes).second(Alice)
        //         .build();
        //
        //     var e1 = ecs.Entity().Add(Likes, Alice);
        //     ecs.Entity().Add(Likes, Bob);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_object_w_type()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity();
        //     struct Alice { };
        //     var Bob = ecs.Entity();
        //
        //     var q = ecs.filter_builder<>()
        //         .term(Likes).second<Alice>()
        //         .build();
        //
        //     var e1 = ecs.Entity().Add(Likes, ecs.id<Alice>());
        //     ecs.Entity().Add(Likes, Bob);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_term()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<>()
        //         .term(ecs.With<Position>())
        //         .build();
        //
        //     var e1 = ecs.Entity().Add<Position>();
        //     ecs.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_term_w_type()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder<>()
        //         .term(ecs.With<Position>())
        //         .build();
        //
        //     var e1 = ecs.Entity().Add<Position>();
        //     ecs.Entity().Add<Velocity>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_term_w_pair_type()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     struct Alice { };
        //     struct Bob { };
        //
        //     var q = ecs.filter_builder<>()
        //         .term(ecs.With<Likes, Alice>())
        //         .build();
        //
        //     var e1 = ecs.Entity().Add<Likes, Alice>();
        //     ecs.Entity().Add<Likes, Bob>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_term_w_id()
        // {
        //     World ecs = World.Create();
        //
        //     var Apples = ecs.Entity();
        //     var Pears = ecs.Entity();
        //
        //     var q = ecs.filter_builder<>()
        //         .term(ecs.term(Apples))
        //         .build();
        //
        //     var e1 = ecs.Entity().Add(Apples);
        //     ecs.Entity().Add(Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void explicit_term_w_pair_id()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity();
        //     var Apples = ecs.Entity();
        //     var Pears = ecs.Entity();
        //
        //     var q = ecs.filter_builder<>()
        //         .term(ecs.term(Likes, Apples))
        //         .build();
        //
        //     var e1 = ecs.Entity().Add(Likes, Apples);
        //     ecs.Entity().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void 1_term_to_empty()
        // {
        //     World ecs = World.Create();
        //
        //     var Likes = ecs.Entity();
        //     var Apples = ecs.Entity();
        //
        //     var qb = ecs.filter_builder<>()
        //         .With<Position>();
        //
        //     qb.term(Likes, Apples);
        //
        //     var q = qb.build();
        //
        //     Assert.Equal(q.field_count(), 2);
        //     Assert.Equal(q.term(0).id(), ecs.id<Position>());
        //     Assert.Equal(q.term(1).id(), ecs.pair(Likes, Apples));
        // }
        //
        // [Fact]
        // public void 2_subsequent_args()
        // {
        //     World ecs = World.Create();
        //
        //     struct Rel { int foo; };
        //
        //     int count = 0;
        //
        //     var s = ecs.system<Rel, const Velocity>()
        //         .arg(1).second(flecs::Wildcard)
        //         .arg(2).Singleton()
        //         .iter([&](flecs::iter it){
        //             count += it.count();
        //         });
        //
        //     ecs.Entity().Add<Rel, Tag>();
        //     ecs.Set<Velocity>({});
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
        //     f.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     return count;
        // }
        //
        // [Fact]
        // public void filter_as_arg()
        // {
        //     World ecs = World.Create();
        //
        //     var f = ecs.filter<Self>();
        //
        //     var e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     Assert.Equal(filter_arg(f), 3);
        // }
        //
        // static
        // int filter_move_arg(flecs::filter<Self>&& f) {
        //     int count = 0;
        //
        //     f.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     return count;
        // }
        //
        // [Fact]
        // public void filter_as_move_arg()
        // {
        //     World ecs = World.Create();
        //
        //     var f = ecs.filter<Self>();
        //
        //     var e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     Assert.Equal(filter_move_arg(ecs.filter<Self>()), 3);
        // }
        //
        // static
        // flecs::filter<Self> filter_return(flecs::world& ecs) {
        //     return ecs.filter<Self>();
        // }
        //
        // [Fact]
        // public void filter_as_return()
        // {
        //     World ecs = World.Create();
        //
        //     var e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     var f = filter_return(ecs);
        //
        //     int count = 0;
        //
        //     f.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void filter_copy()
        // {
        //     World ecs = World.Create();
        //
        //     var e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     var f = ecs.filter<Self>();
        //
        //     var f_2 = f;
        //
        //     int count = 0;
        //
        //     f_2.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void world_each_filter_1_component()
        // {
        //     World ecs = World.Create();
        //
        //     var e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e});
        //
        //     int count = 0;
        //
        //     ecs.Each((Entity e, Self& s) {
        //         Assert.True(e == s.Value);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void world_each_filter_2_components()
        // {
        //     World ecs = World.Create();
        //
        //     var e = ecs.Entity();
        //     e.Set(new Self { Value = e})
        //      .Set<Position>({10, 20});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e})
        //         .Set<Position>({10, 20});
        //
        //     e = ecs.Entity();
        //     e.Set(new Self { Value = e})
        //      .Set<Position>({10, 20});
        //
        //     int count = 0;
        //
        //     ecs.Each((Entity e, Self& s, ref Position p) {
        //         Assert.True(e == s.Value);
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void world_each_filter_1_component_no_entity()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Entity()
        //         .Set<Position>({10, 20});
        //
        //     ecs.Entity()
        //         .Set<Position>({10, 20});
        //
        //     ecs.Entity()
        //         .Set<Position>({10, 20})
        //         .Set<Velocity>({1, 2});
        //
        //     int count = 0;
        //
        //     ecs.each([&](ref Position p) {
        //         Assert.Equal(p.x, 10);
        //         Assert.Equal(p.y, 20);
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 3);
        // }
        //
        // [Fact]
        // public void world_each_filter_2_components_no_entity()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Entity()
        //         .Set<Position>({10, 20})
        //         .Set<Velocity>({1, 2});
        //
        //     ecs.Entity()
        //         .Set<Position>({10, 20})
        //         .Set<Velocity>({1, 2});
        //
        //     ecs.Entity()
        //         .Set<Position>({10, 20})
        //         .Set<Velocity>({1, 2});
        //
        //     ecs.Entity()
        //         .Set<Position>({3, 5});
        //
        //     ecs.Entity()
        //         .Set<Velocity>({20, 40});
        //
        //     int count = 0;
        //
        //     ecs.each([&](ref Position p, ref Velocity v) {
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
        // public void 10_terms()
        // {
        //     World ecs = World.Create();
        //
        //     var f = ecs.filter_builder<>()
        //         .With<TagA>()
        //         .With<TagB>()
        //         .With<TagC>()
        //         .With<TagD>()
        //         .With<TagE>()
        //         .With<TagF>()
        //         .With<TagG>()
        //         .With<TagH>()
        //         .With<TagI>()
        //         .With<TagJ>()
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 10);
        //
        //     var e = ecs.Entity()
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
        //     f.Iter((Iter it) {
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
        // public void 20_terms()
        // {
        //     World ecs = World.Create();
        //
        //     var f = ecs.filter_builder<>()
        //         .With<TagA>()
        //         .With<TagB>()
        //         .With<TagC>()
        //         .With<TagD>()
        //         .With<TagE>()
        //         .With<TagF>()
        //         .With<TagG>()
        //         .With<TagH>()
        //         .With<TagI>()
        //         .With<TagJ>()
        //         .With<TagK>()
        //         .With<TagL>()
        //         .With<TagM>()
        //         .With<TagN>()
        //         .With<TagO>()
        //         .With<TagP>()
        //         .With<TagQ>()
        //         .With<TagR>()
        //         .With<TagS>()
        //         .With<TagT>()
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 20);
        //
        //     var e = ecs.Entity()
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
        //     f.Iter((Iter it) {
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
        // public void term_after_arg()
        // {
        //     World ecs = World.Create();
        //
        //     var e_1 = ecs.Entity()
        //         .Add<TagA>()
        //         .Add<TagB>()
        //         .Add<TagC>();
        //
        //     ecs.Entity()
        //         .Add<TagA>()
        //         .Add<TagB>();
        //
        //     var f = ecs.filter_builder<TagA, TagB>()
        //         .arg(1).src(flecs::This) // dummy
        //         .With<TagC>()
        //         .build();
        //
        //     Assert.Equal(f.field_count(), 3);
        //
        //     int count = 0;
        //     f.Each((Entity e, TagA, TagB) {
        //         Assert.True(e == e_1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void name_arg()
        // {
        //     World ecs = World.Create();
        //
        //     var e = ecs.Entity("Foo").Set<Position>({10, 20});
        //
        //     var f = ecs.filter_builder<Position>()
        //         .arg(1).src().name("Foo")
        //         .build();
        //
        //     int count = 0;
        //     f.Iter((Iter it, Position* p) {
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
        // public void const_in_term()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Entity().Set<Position>({10, 20});
        //
        //     var f = ecs.filter_builder<>()
        //         .With<const Position>()
        //         .build();
        //
        //     int count = 0;
        //     f.Iter((Iter it) {
        //         var p = it.Field<const Position>(1);
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
        // public void const_optional()
        // {
        //     World ecs = World.Create();
        //
        //  ecs.Entity().Set<Position>({10, 20}).Add<TagA>();
        //     ecs.Entity().Add<TagA>();
        //
        //     var f = ecs.filter_builder<TagA, const Position*>().build();
        //
        //     int count = 0, Set_count = 0;
        //     f.Iter((Iter it) {
        //         Assert.Equal(it.count(), 1);
        //         if (it.is_Set(2)) {
        //             var p = it.Field<const Position>(2);
        //             Assert.True(it.is_readonly(2));
        //             Assert.Equal(p->x, 10);
        //             Assert.Equal(p->y, 20);
        //             Set_count ++;
        //         }
        //         count++;
        //  });
        //
        //     Assert.Equal(2, count);
        //     Assert.Equal(Set_count, 1);
        // }
        //
        // [Fact]
        // public void create_w_no_template_args()
        // {
        //     World ecs = World.Create();
        //
        //     var q = ecs.filter_builder().With<Position>().build();
        //
        //     var e1 = ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void 2_terms_w_expr()
        // {
        //     World ecs = World.Create();
        //
        //     var a = ecs.Entity("A");
        //     var b = ecs.Entity("B");
        //
        //     var e1 = ecs.Entity().Add(a).Add(b);
        //
        //     var f = ecs.filter_builder()
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
        // public void assert_on_uninitialized_term()
        // {
        //     install_test_abort();
        //
        //     World ecs = World.Create();
        //
        //     ecs.Entity("A");
        //     ecs.Entity("B");
        //
        //     test_expect_abort();
        //
        //     var f = ecs.filter_builder()
        //         .term()
        //         .term()
        //         .build();
        // }
        //
        // [Fact]
        // public void operator_shortcuts()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity a = ecs.Entity();
        //     flecs::entity b = ecs.Entity();
        //     flecs::entity c = ecs.Entity();
        //     flecs::entity d = ecs.Entity();
        //     flecs::entity e = ecs.Entity();
        //     flecs::entity f = ecs.Entity();
        //     flecs::entity g = ecs.Entity();
        //     flecs::entity h = ecs.Entity();
        //
        //     var filter = ecs.filter_builder()
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
        // public void inout_shortcuts()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity a = ecs.Entity();
        //     flecs::entity b = ecs.Entity();
        //     flecs::entity c = ecs.Entity();
        //     flecs::entity d = ecs.Entity();
        //
        //     var filter = ecs.filter_builder()
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
        // public void iter_column_w_const_as_array()
        // {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var e1 = world.Entity().Set<Position>({10, 20});
        //     var e2 = world.Entity().Set<Position>({20, 30});
        //
        //     int count = 0;
        //     f.Iter((Iter it) {
        //         const var p = it.Field<Position>(1);
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
        //     const Position *p = e1.get<Position>();
        //     Assert.Equal(p->x, 11);
        //     Assert.Equal(p->y, 22);
        //
        //     p = e2.get<Position>();
        //     Assert.Equal(p->x, 21);
        //     Assert.Equal(p->y, 32);
        // }
        //
        // [Fact]
        // public void iter_column_w_const_as_ptr()
        // {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var base = world.prefab().Set<Position>({10, 20});
        //     world.Entity().is_a(base);
        //     world.Entity().is_a(base);
        //
        //     int count = 0;
        //     f.Iter((Iter it) {
        //         const var p = it.Field<Position>(1);
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
        // public void iter_column_w_const_deref()
        // {
        //     flecs::world world;
        //
        //     var f = world.filter<Position>();
        //
        //     var base = world.prefab().Set<Position>({10, 20});
        //     world.Entity().is_a(base);
        //     world.Entity().is_a(base);
        //
        //     int count = 0;
        //     f.Iter((Iter it) {
        //         const var p = it.Field<Position>(1);
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
        // public void with_id()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with(ecs.id<Position>())
        //             .with(ecs.id<Velocity>())
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Velocity>();
        //     ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_name()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Component<Velocity>();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with("Velocity")
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Velocity>();
        //     ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_component()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with<Velocity>()
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Velocity>();
        //     ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_pair_id()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity();
        //     flecs::entity Apples = ecs.Entity();
        //     flecs::entity Pears = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with(Likes, Apples)
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add(Likes, Apples);
        //     ecs.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_pair_name()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity("Likes");
        //     flecs::entity Apples = ecs.Entity("Apples");
        //     flecs::entity Pears = ecs.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with("Likes", "Apples")
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add(Likes, Apples);
        //     ecs.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_pair_components()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //     struct Pears { };
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with<Likes, Apples>()
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Likes, Apples>();
        //     ecs.Entity().Add<Position>().Add<Likes, Pears>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_pair_component_id()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity();
        //     flecs::entity Pears = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with<Likes>(Apples)
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Likes>(Apples);
        //     ecs.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_pair_component_name()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity("Apples");
        //     flecs::entity Pears = ecs.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with<Likes>("Apples")
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add<Likes>(Apples);
        //     ecs.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void with_enum()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .with(Green)
        //             .build();
        //
        //     var e1 = ecs.Entity().Add<Position>().Add(Green);
        //     ecs.Entity().Add<Position>().Add(Red);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e1);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_id()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with(ecs.id<Position>())
        //             .without(ecs.id<Velocity>())
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add<Velocity>();
        //     var e2 = ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_name()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Component<Velocity>();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with(ecs.id<Position>())
        //             .without("Velocity")
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add<Velocity>();
        //     var e2 = ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_component()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without<Velocity>()
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add<Velocity>();
        //     var e2 = ecs.Entity().Add<Position>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_pair_id()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity();
        //     flecs::entity Apples = ecs.Entity();
        //     flecs::entity Pears = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without(Likes, Apples)
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add(Likes, Apples);
        //     var e2 = ecs.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_pair_name()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity("Likes");
        //     flecs::entity Apples = ecs.Entity("Apples");
        //     flecs::entity Pears = ecs.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without("Likes", "Apples")
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add(Likes, Apples);
        //     var e2 = ecs.Entity().Add<Position>().Add(Likes, Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_pair_components()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //     struct Pears { };
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without<Likes, Apples>()
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add<Likes, Apples>();
        //     var e2 = ecs.Entity().Add<Position>().Add<Likes, Pears>();
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_pair_component_id()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity();
        //     flecs::entity Pears = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without<Likes>(Apples)
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add<Likes>(Apples);
        //     var e2 = ecs.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_pair_component_name()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity("Apples");
        //     flecs::entity Pears = ecs.Entity("Pears");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without<Likes>("Apples")
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add<Likes>(Apples);
        //     var e2 = ecs.Entity().Add<Position>().Add<Likes>(Pears);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void without_enum()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .without(Green)
        //             .build();
        //
        //     ecs.Entity().Add<Position>().Add(Green);
        //     var e2 = ecs.Entity().Add<Position>().Add(Red);
        //
        //     int count = 0;
        //     q.Each((Entity e) => {
        //         count ++;
        //         Assert.True(e == e2);
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void write_id()
        // {
        //     World ecs = World.Create();
        //
        //     var q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write(ecs.id<Position>())
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void write_name()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Component<Position>();
        //
        //     var q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write("Position")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void write_component()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Component<Position>();
        //
        //     var q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write<Position>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void write_pair_id()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity();
        //     flecs::entity Apples = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
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
        // public void write_pair_name()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity("Likes");
        //     flecs::entity Apples = ecs.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
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
        // public void write_pair_components()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write<Likes, Apples>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Likes>());
        //     Assert.True(q.term(1).get_second() == ecs.id<Apples>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void write_pair_component_id()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write<Likes>(Apples)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void write_pair_component_name()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write<Likes>("Apples")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void write_enum()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .write(Green)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::Out);
        //     Assert.True(q.term(1).get_first() == ecs.id<Color>());
        //     Assert.True(q.term(1).get_second() == ecs.to_entity<Color>(Green));
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_id()
        // {
        //     World ecs = World.Create();
        //
        //     var q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read(ecs.id<Position>())
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_name()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Component<Position>();
        //
        //     var q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read("Position")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_component()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Component<Position>();
        //
        //     var q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read<Position>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Position>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_pair_id()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity();
        //     flecs::entity Apples = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
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
        // public void read_pair_name()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::entity Likes = ecs.Entity("Likes");
        //     flecs::entity Apples = ecs.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
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
        // public void read_pair_components()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     struct Apples { };
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read<Likes, Apples>()
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Likes>());
        //     Assert.True(q.term(1).get_second() == ecs.id<Apples>());
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_pair_component_id()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read<Likes>(Apples)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_pair_component_name()
        // {
        //     World ecs = World.Create();
        //
        //     struct Likes { };
        //     flecs::entity Apples = ecs.Entity("Apples");
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read<Likes>("Apples")
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Likes>());
        //     Assert.True(q.term(1).get_second() == Apples);
        //
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void read_enum()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> q =
        //         ecs.filter_builder()
        //             .with<Position>()
        //             .read(Green)
        //             .build();
        //
        //     Assert.True(q.term(1).inout() == flecs::In);
        //     Assert.True(q.term(1).get_first() == ecs.id<Color>());
        //     Assert.True(q.term(1).get_second() == ecs.to_entity<Color>(Green));
        //     Assert.True(q.term(1).get_src() == 0);
        // }
        //
        // [Fact]
        // public void assign_after_init()
        // {
        //     World ecs = World.Create();
        //
        //     flecs::filter<> f;
        //     flecs::filter_builder<> fb = ecs.filter_builder();
        //     fb.with<Position>();
        //     f = fb.build();
        //
        //     flecs::entity e1 = ecs.Entity().Set<Position>({10, 20});
        //
        //     int count = 0;
        //     f.Each((Entity e) => {
        //         Assert.True(e == e1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
        //
        // [Fact]
        // public void iter_w_stage()
        // {
        //     World ecs = World.Create();
        //
        //     ecs.Set_stage_count(2);
        //     flecs::world stage = ecs.get_stage(1);
        //
        //     var e1 = ecs.Entity().Add<Position>();
        //
        //     var q = ecs.filter<Position>();
        //
        //     int count = 0;
        //     q.each(stage, [&](flecs::iter& it, size_t i, ref Position) {
        //         Assert.True(it.world() == stage);
        //         Assert.True(it.Entity(i) == e1);
        //         count ++;
        //     });
        //
        //     Assert.Equal(1, count);
        // }
    }
}
