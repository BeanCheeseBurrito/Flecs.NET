#if !NET5_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class ObserverTests
    {
        [Fact]
        private void _2TermsOnAdd()
        {
            using World world = World.Create();

            int count = 0;

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnAdd)
                .Each((ref Position p, ref Velocity v) => { count++; });

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
                .Event(Ecs.UnSet)
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
                .With<TagA>()
                .With<TagB>()
                .With<TagC>()
                .With<TagD>()
                .With<TagE>()
                .With<TagF>()
                .With<TagG>()
                .With<TagH>()
                .With<TagI>()
                .With<TagJ>()
                .Iter((Iter it) =>
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.Equal(10, it.FieldCount());
                    count++;
                });

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
        private void _16Terms()
        {
            using World world = World.Create();

            int count = 0;

            Entity e = world.Entity();

            world.Observer()
                .Event(Ecs.OnAdd)
                .With<TagA>()
                .With<TagB>()
                .With<TagC>()
                .With<TagD>()
                .With<TagE>()
                .With<TagF>()
                .With<TagG>()
                .With<TagH>()
                .With<TagI>()
                .With<TagJ>()
                .With<TagK>()
                .With<TagL>()
                .With<TagM>()
                .With<TagN>()
                .With<TagO>()
                .With<TagP>()
                .Iter((Iter it) =>
                {
                    Assert.Equal(1, it.Count());
                    Assert.Equal(e, it.Entity(0));
                    Assert.Equal(16, it.FieldCount());
                    count++;
                });

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
                .Add<TagP>();

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
                .Iter((Iter it, Field<Position> p) =>
                {
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
                .Iter((Iter it) =>
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

            Entity e1 = world.Entity().Add<TagA>();
            Entity e2 = world.Entity().Add<TagA>();
            Entity e3 = world.Entity().Add<TagA>().Add<TagB>();

            int count = 0;

            world.Observer<TagA>()
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

            Entity e1 = world.Entity().Add<TagA>().Add<TagB>();
            Entity e2 = world.Entity().Add<TagA>().Add<TagB>();
            Entity e3 = world.Entity().Add<TagA>().Add<TagB>().Add<TagC>();
            world.Entity().Add<TagA>();
            world.Entity().Add<TagB>();

            int count = 0;

            world.Observer<TagA, TagB>()
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
        private void DefaultCtor()
        {
            using World world = World.Create();

            Observer observer = default;
            Assert.True(observer == 0);

            int count = 0;

            observer = world.Observer<TagA>()
                .Event(Ecs.OnAdd)
                .Each((Entity e) => { count++; });

            Assert.True(observer != 0);

            world.Entity().Add<TagA>();

            Assert.Equal(1, count);
        }

        [Fact]
        private void EntityCtor()
        {
            using World world = World.Create();

            Observer observer = world.Observer<TagA>()
                .Event(Ecs.OnAdd)
                .Each((Entity e) => { });

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
                .Each((Entity e, ref Position p) => { invoked++; });

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
                .Each((Entity e, ref Position p) => { invoked++; });

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

            world.Observer<MyTag>()
                .Event(Ecs.OnAdd)
                .Iter((Iter it) => { invoked++; });

            world.Entity()
                .Add<MyTag>();

            Assert.Equal(1, invoked);
        }

        [Fact]
        private void OnAddTagIter()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer<MyTag>()
                .Event(Ecs.OnAdd)
                .Iter((Iter it) => { invoked++; });

            world.Entity()
                .Add<MyTag>();

            Assert.Equal(1, invoked);
        }

        [Fact]
        private void OnAddTagEach()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Observer<MyTag>()
                .Event(Ecs.OnAdd)
                .Each((Entity e) => { invoked++; });

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
                .Each((Entity e) => { invoked++; });

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
                .Each((Entity e) => { invoked++; });

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

        // TODO: Fails in release mode.
//         [Fact]
//         private void RunCallback()
//         {
//             using World world = World.Create();
//
//             int count = 0;
//
//             world.Observer<Position>()
//                 .Event(EcsOnAdd)
//                 .Run(it =>
//                 {
//                     while (ecs_iter_next(it) == 1)
//                     {
// #if NET5_0_OR_GREATER
//                         ((delegate* unmanaged<ecs_iter_t*, void>)it->callback)(it);
// #else
//                         Marshal.GetDelegateForFunctionPointer<Ecs.IterAction>(it->callback)(it);
// #endif
//                     }
//                 })
//                 .Each((ref Position p) => { count++; }
//                 );
//
//             Entity e = world.Entity();
//             Assert.Equal(0, count);
//
//             e.Set(new Position { X = 10, Y = 20 });
//             Assert.Equal(1, count);
//         }

        [Fact]
        private void GetQuery()
        {
            using World world = World.Create();

            world.Entity().Set(new Position { X = 0, Y = 0 });
            world.Entity().Set(new Position { X = 1, Y = 0 });
            world.Entity().Set(new Position { X = 2, Y = 0 });

            int count = 0;

            Observer observer = world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) => { });

            Query query = observer.Query();

            query.Iter((Iter it, Field<Position> pos) =>
            {
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

            Observer observer = world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) => { count++; });

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

            Observer observer = world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) => { count++; });

            Entity e = world.Entity();
            Assert.Equal(0, count);

            world.DeferBegin();
            e.Set(new Position { X = 10, Y = 20 });

            Assert.Equal(0, count);
            world.DeferEnd();

            Assert.Equal(1, count);
        }

        [Fact]
        private void OnAddSingleton()
        {
            using World world = World.Create();

            int count = 0;

            Observer observer = world.Observer<Position>()
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

            Observer observer = world.Observer<Position>()
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

            Observer observer = world.Observer<Position>()
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

            Observer observer = world.Observer()
                .With<Position>(tgt).Singleton()
                .Event(Ecs.OnSet)
                .Each((Entity entity) => { count++; });

            world.Set(tgt, new Position { X = 10, Y = 20 });
            Assert.Equal(1, count);
        }

        [Fact]
        private void AddInYieldExisting()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set<Position>(default(Position));
            Entity e2 = world.Entity().Set<Position>(default(Position));
            Entity e3 = world.Entity().Set<Position>(default(Position));

            Observer observer = world.Observer<Position>()
                .Event(Ecs.OnAdd)
                .YieldExisting()
                .Each((Entity e) => { e.Add<Velocity>(); });

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

            Entity e1 = world.Entity().Set<Position>(default(Position)).Set<Mass>(default(Mass));
            Entity e2 = world.Entity().Set<Position>(default(Position)).Set<Mass>(default(Mass));
            Entity e3 = world.Entity().Set<Position>(default(Position)).Set<Mass>(default(Mass));

            Observer observer = world.Observer<Position, Mass>()
                .Event(Ecs.OnAdd)
                .YieldExisting()
                .Each((Entity e) => { e.Add<Velocity>(); });

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
                .Each((ref Position _) =>{ });

            Assert.Equal("MySystem", sys.Name());

            Entity ns = world.Entity(".ns");
            Assert.True(ns == sys.Parent());
        }
    }
}
