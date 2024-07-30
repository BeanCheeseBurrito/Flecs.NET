using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Core;
using Flecs.NET.Utilities;
using Xunit;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Tests.Cpp
{
    [SuppressMessage("ReSharper", "AccessToModifiedClosure")]
    [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public unsafe class SystemTests
    {
        [Fact]
        private void Iter()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine<Position, Velocity>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);

                        foreach (int i in it)
                        {
                            p[i].X += v[i].X;
                            p[i].Y += v[i].Y;
                        }
                    }
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void IterConst()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine<Position, Velocity>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);

                        foreach (int i in it)
                        {
                            p[i].X += v[i].X;
                            p[i].Y += v[i].Y;
                        }
                    }
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void IterShared()
        {
            using World world = World.Create();

            world.Component<Position>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);
            world.Component<Velocity>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Entity @base = world.Entity()
                .Set(new Velocity(1, 2));

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Add(Ecs.IsA, @base);

            Entity e2 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(3, 4));

            world.Routine<Position>().Expr("Velocity(self|up IsA)")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);

                        if (!it.IsSelf(1))
                            foreach (int i in it)
                            {
                                p[i].X += v[0].X;
                                p[i].Y += v[0].Y;
                            }
                        else
                            foreach (int i in it)
                            {
                                p[i].X += v[i].X;
                                p[i].Y += v[i].Y;
                            }
                    }
                });

            world.Progress();

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(13, p->X);
            Assert.Equal(24, p->Y);
        }

        [Fact]
        private void IterOptional()
        {
            using World world = World.Create();
            world.Component<Mass>();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2))
                .Set(new Mass(1));

            Entity e2 = world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4))
                .Set(new Mass(1));

            Entity e3 = world.Entity()
                .Set(new Position(50, 60));

            Entity e4 = world.Entity()
                .Set(new Position(70, 80));

            world.Routine<Position, Velocity, Mass>()
                .TermAt(1).Optional()
                .TermAt(2).Optional()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);
                        Field<Mass> m = it.Field<Mass>(2);

                        if (it.IsSet(1) && it.IsSet(2))
                            foreach (int i in it)
                            {
                                p[i].X += v[i].X * m[i].Value;
                                p[i].Y += v[i].Y * m[i].Value;
                            }
                        else
                            foreach (int i in it)
                            {
                                p[i].X++;
                                p[i].Y++;
                            }
                    }
                });

            world.Progress();

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(33, p->X);
            Assert.Equal(44, p->Y);

            p = e3.GetPtr<Position>();
            Assert.Equal(51, p->X);
            Assert.Equal(61, p->Y);

            p = e4.GetPtr<Position>();
            Assert.Equal(71, p->X);
            Assert.Equal(81, p->Y);
        }

        [Fact]
        private void Each()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine<Position, Velocity>()
                .Each((Entity e, ref Position p, ref Velocity v) =>
                {
                    p.X += v.X;
                    p.Y += v.Y;
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void EachConst()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine<Position, Velocity>()
                .Each((Entity e, ref Position p, ref Velocity v) =>
                {
                    p.X += v.X;
                    p.Y += v.Y;
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void EachShared()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .Set(new Velocity(1, 2));

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Add(Ecs.IsA, @base);

            Entity e2 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(3, 4));

            world.Routine<Position, Velocity>()
                .Each((Entity e, ref Position p, ref Velocity v) =>
                {
                    p.X += v.X;
                    p.Y += v.Y;
                });

            world.Progress();

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(13, p->X);
            Assert.Equal(24, p->Y);
        }

        [Fact]
        private void EachOptional()
        {
            using World world = World.Create();
            world.Component<Mass>();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2))
                .Set(new Mass(1));

            Entity e2 = world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4))
                .Set(new Mass(1));

            Entity e3 = world.Entity()
                .Set(new Position(50, 60));

            Entity e4 = world.Entity()
                .Set(new Position(70, 80));

            world.Routine<Position, Velocity, Mass>()
                .TermAt(1).Optional()
                .TermAt(2).Optional()
                .Each((Entity e, ref Position p, ref Velocity v, ref Mass m) =>
                {
                    if (!Unsafe.IsNullRef(ref v) && !Unsafe.IsNullRef(ref m))
                    {
                        p.X += v.X * m.Value;
                        p.Y += v.Y * m.Value;
                    }
                    else
                    {
                        p.X++;
                        p.Y++;
                    }
                });

            world.Progress();

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(33, p->X);
            Assert.Equal(44, p->Y);

            p = e3.GetPtr<Position>();
            Assert.Equal(51, p->X);
            Assert.Equal(61, p->Y);

            p = e4.GetPtr<Position>();
            Assert.Equal(71, p->X);
            Assert.Equal(81, p->Y);
        }

        [Fact]
        private void Signature()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine().Expr("Position, Velocity")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);

                        foreach (int i in it)
                        {
                            p[i].X += v[i].X;
                            p[i].Y += v[i].Y;
                        }
                    }
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void SignatureConst()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine().Expr("Position, [in] Velocity")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);

                        foreach (int i in it)
                        {
                            p[i].X += v[i].X;
                            p[i].Y += v[i].Y;
                        }
                    }
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void SignatureShared()
        {
            using World world = World.Create();

            world.Component<Position>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);
            world.Component<Velocity>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            Entity @base = world.Entity()
                .Set(new Velocity(1, 2));

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Add(Ecs.IsA, @base);

            Entity e2 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(3, 4));

            world.Routine().Expr("Position, [in] Velocity(self|up IsA)")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);

                        if (!it.IsSelf(1))
                            foreach (int i in it)
                            {
                                p[i].X += v[0].X;
                                p[i].Y += v[0].Y;
                            }
                        else
                            foreach (int i in it)
                            {
                                p[i].X += v[i].X;
                                p[i].Y += v[i].Y;
                            }
                    }
                });

            world.Progress();

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(13, p->X);
            Assert.Equal(24, p->Y);
        }

        [Fact]
        private void SignatureOptional()
        {
            using World world = World.Create();
            world.Component<Mass>();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2))
                .Set(new Mass(1));

            Entity e2 = world.Entity()
                .Set(new Position(30, 40))
                .Set(new Velocity(3, 4))
                .Set(new Mass(1));

            Entity e3 = world.Entity()
                .Set(new Position(50, 60));

            Entity e4 = world.Entity()
                .Set(new Position(70, 80));

            world.Routine().Expr("Position, ?Velocity, ?Mass")
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);
                        Field<Velocity> v = it.Field<Velocity>(1);
                        Field<Mass> m = it.Field<Mass>(2);

                        if (it.IsSet(1) && it.IsSet(2))
                            foreach (int i in it)
                            {
                                p[i].X += v[i].X * m[i].Value;
                                p[i].Y += v[i].Y * m[i].Value;
                            }
                        else
                            foreach (int i in it)
                            {
                                p[i].X++;
                                p[i].Y++;
                            }
                    }
                });

            world.Progress();

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(33, p->X);
            Assert.Equal(44, p->Y);

            p = e3.GetPtr<Position>();
            Assert.Equal(51, p->X);
            Assert.Equal(61, p->Y);

            p = e4.GetPtr<Position>();
            Assert.Equal(71, p->X);
            Assert.Equal(81, p->Y);
        }

        [Fact]
        private void CopyNameOnCreate()
        {
            using World world = World.Create();

            string name = "Hello";

            Routine system1 = world.Routine<Position>(name)
                .Run((Iter it) => { while (it.Next()) { } });

            name = "World";
            Routine system2 = world.Routine<Position>(name)
                .Run((Iter it) => { while (it.Next()) { } });

            Assert.True(system1.Id != system2.Id);
        }

        [Fact]
        private void NestedSystem()
        {
            using World world = World.Create();

            Routine system1 = world.Routine<Position>("foo.bar")
                .Run((Iter it) => { while (it.Next()) { } });

            Assert.Equal("bar", system1.Entity.Name());

            Entity e = world.Lookup("foo");
            Assert.True(e.Id != 0);
            Assert.Equal("foo", e.Name());

            Entity se = e.Lookup("bar");
            Assert.True(se.Id != 0);
            Assert.Equal("bar", se.Name());
        }

        [Fact]
        private void EmptySignature()
        {
            using World world = World.Create();

            int count = 0;

            world.Routine()
                .Run((Iter it) =>
                {
                    while (it.Next())
                        count++;
                });

            world.Progress();

            Assert.Equal(1, count);
        }

        [Fact]
        private void IterTag()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Routine<MyTag>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                        invoked++;
                });

            world.Entity()
                .Add<MyTag>();

            world.Progress();

            Assert.Equal(1, invoked);
        }

        [Fact]
        private void EachTag()
        {
            using World world = World.Create();

            int invoked = 0;

            world.Routine<MyTag>()
                .Each((Entity e) => { invoked++; });

            world.Entity()
                .Add<MyTag>();

            world.Progress();

            Assert.Equal(1, invoked);
        }

        [Fact]
        private void SetInterval()
        {
            using World world = World.Create();

            Routine sys = world.Routine()
                .Kind(0)
                .Interval(1.0f)
                .Run((Iter it) => { });

            float i = sys.Interval();
            Assert.Equal(1.0f, i);

            sys.Interval(2.0f);

            i = sys.Interval();
            Assert.Equal(2.0f, i);
        }

        [Fact]
        private void OrderByType()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(3, 0));
            world.Entity().Set(new Position(1, 0));
            world.Entity().Set(new Position(5, 0));
            world.Entity().Set(new Position(2, 0));
            world.Entity().Set(new Position(4, 0));

            float lastVal = 0;
            int count = 0;

            Routine sys = world.Routine<Position>()
                .OrderBy<Position>((ulong e1, void* p1, ulong e2, void* p2) =>
                {
                    Position* pos1 = (Position*)p1;
                    Position* pos2 = (Position*)p2;
                    return Utils.Bool(pos1->X > pos2->X) - Utils.Bool(pos1->X < pos2->X);
                })
                .Each((Entity e, ref Position p) =>
                {
                    Assert.True(p.X > lastVal);
                    lastVal = p.X;
                    count++;
                });

            sys.Run();

            Assert.Equal(5, count);
        }

        [Fact]
        private void OrderById()
        {
            using World world = World.Create();

            Component<Position> pos = world.Component<Position>();

            world.Entity().Set(new Position(3, 0));
            world.Entity().Set(new Position(1, 0));
            world.Entity().Set(new Position(5, 0));
            world.Entity().Set(new Position(2, 0));
            world.Entity().Set(new Position(4, 0));

            float lastVal = 0;
            int count = 0;

            Routine sys = world.Routine<Position>()
                .OrderBy(pos, (ulong e1, void* p1, ulong e2, void* p2) =>
                {
                    Position* pos1 = (Position*)p1;
                    Position* pos2 = (Position*)p2;
                    return Utils.Bool(pos1->X > pos2->X) - Utils.Bool(pos1->X < pos2->X);
                })
                .Each((Entity e, ref Position p) =>
                {
                    Assert.True(p.X > lastVal);
                    lastVal = p.X;
                    count++;
                });

            sys.Run();

            Assert.Equal(5, count);
        }

        [Fact]
        private void OrderByTypeAfterCreate()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(3, 0));
            world.Entity().Set(new Position(1, 0));
            world.Entity().Set(new Position(5, 0));
            world.Entity().Set(new Position(2, 0));
            world.Entity().Set(new Position(4, 0));

            float lastVal = 0;
            int count = 0;

            Routine sys = world.Routine<Position>()
                .OrderBy<Position>((ulong e1, void* p1, ulong e2, void* p2) =>
                {
                    Position* pos1 = (Position*)p1;
                    Position* pos2 = (Position*)p2;
                    return Utils.Bool(pos1->X > pos2->X) - Utils.Bool(pos1->X < pos2->X);
                })
                .Each((Entity e, ref Position p) =>
                {
                    Assert.True(p.X > lastVal);
                    lastVal = p.X;
                    count++;
                });

            sys.Run();

            Assert.Equal(5, count);
        }

        [Fact]
        private void OrderByIdAfterCreate()
        {
            using World world = World.Create();

            Component<Position> pos = world.Component<Position>();

            world.Entity().Set(new Position(3, 0));
            world.Entity().Set(new Position(1, 0));
            world.Entity().Set(new Position(5, 0));
            world.Entity().Set(new Position(2, 0));
            world.Entity().Set(new Position(4, 0));

            float lastVal = 0;
            int count = 0;

            Routine sys = world.Routine<Position>()
                .OrderBy(pos, (ulong e1, void* p1, ulong e2, void* p2) =>
                {
                    Position* pos1 = (Position*)p1;
                    Position* pos2 = (Position*)p2;
                    return Utils.Bool(pos1->X > pos2->X) - Utils.Bool(pos1->X < pos2->X);
                })
                .Each((Entity e, ref Position p) =>
                {
                    Assert.True(p.X > lastVal);
                    lastVal = p.X;
                    count++;
                });

            sys.Run();

            Assert.Equal(5, count);
        }

        [Fact]
        private void GetQuery()
        {
            using World world = World.Create();

            world.Entity().Set(new Position(0, 0));
            world.Entity().Set(new Position(1, 0));
            world.Entity().Set(new Position(2, 0));

            int count = 0;

            Routine sys = world.Routine<Position>()
                .Each((Entity e, ref Position p) => { });

            Query q = sys.Query();

            q.Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> pos = it.Field<Position>(0);
                    foreach (int i in it)
                    {
                        Assert.Equal(i, pos[i].X);
                        count++;
                    }
                }
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void AddFromEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(0, 0));
            Entity e2 = world.Entity().Set(new Position(1, 0));
            Entity e3 = world.Entity().Set(new Position(2, 0));

            world.Routine<Position>()
                .Each((Entity e, ref Position p) =>
                {
                    e.Add<Velocity>();
                    Assert.True(!e.Has<Velocity>());
                });

            world.Progress();

            Assert.True(e1.Has<Velocity>());
            Assert.True(e2.Has<Velocity>());
            Assert.True(e3.Has<Velocity>());
        }

        [Fact]
        private void DeleteFromEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(0, 0));
            Entity e2 = world.Entity().Set(new Position(1, 0));
            Entity e3 = world.Entity().Set(new Position(2, 0));

            world.Routine<Position>()
                .Each((Entity e, ref Position p) =>
                {
                    e.Destruct();
                    Assert.True(e.IsAlive());
                });

            world.Progress();

            Assert.True(!e1.IsAlive());
            Assert.True(!e2.IsAlive());
            Assert.True(!e3.IsAlive());
        }

        [Fact]
        private void AddFromEachWorldHandle()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new EntityWrapper(world.Entity()));
            Entity e2 = world.Entity().Set(new EntityWrapper(world.Entity()));
            Entity e3 = world.Entity().Set(new EntityWrapper(world.Entity()));

            world.Routine<EntityWrapper>()
                .Each((Entity e, ref EntityWrapper c) => { c.Value.Mut(e).Add<Position>(); });

            world.Progress();

            Assert.True(e1.GetPtr<EntityWrapper>()->Value.Has<Position>());
            Assert.True(e2.GetPtr<EntityWrapper>()->Value.Has<Position>());
            Assert.True(e3.GetPtr<EntityWrapper>()->Value.Has<Position>());
        }

        [Fact]
        private void NewFromEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(0, 0));
            Entity e2 = world.Entity().Set(new Position(0, 0));
            Entity e3 = world.Entity().Set(new Position(0, 0));

            world.Routine<Position>()
                .Each((Entity e, ref Position p) =>
                {
                    e.Set(new EntityWrapper(
                        e.CsWorld().Entity().Add<Velocity>()
                    ));
                });

            world.Progress();

            Assert.True(e1.Has<EntityWrapper>());
            Assert.True(e2.Has<EntityWrapper>());
            Assert.True(e3.Has<EntityWrapper>());

            Assert.True(e1.GetPtr<EntityWrapper>()->Value.Has<Velocity>());
            Assert.True(e2.GetPtr<EntityWrapper>()->Value.Has<Velocity>());
            Assert.True(e3.GetPtr<EntityWrapper>()->Value.Has<Velocity>());
        }

        [Fact]
        private void AddFromIter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(0, 0));
            Entity e2 = world.Entity().Set(new Position(1, 0));
            Entity e3 = world.Entity().Set(new Position(2, 0));

            world.Routine<Position>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);

                        foreach (int i in it)
                        {
                            it.Entity(i).Add<Velocity>();
                            Assert.True(!it.Entity(i).Has<Velocity>());
                        }
                    }
                });

            world.Progress();

            Assert.True(e1.Has<Velocity>());
            Assert.True(e2.Has<Velocity>());
            Assert.True(e3.Has<Velocity>());
        }

        [Fact]
        private void DeleteFromIter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(0, 0));
            Entity e2 = world.Entity().Set(new Position(1, 0));
            Entity e3 = world.Entity().Set(new Position(2, 0));

            world.Routine<Position>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);

                        foreach (int i in it)
                        {
                            it.Entity(i).Destruct();
                            Assert.True(it.Entity(i).IsAlive());
                        }
                    }
                });

            world.Progress();

            Assert.True(!e1.IsAlive());
            Assert.True(!e2.IsAlive());
            Assert.True(!e3.IsAlive());
        }

        [Fact]
        private void AddFromIterWorldHandle()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new EntityWrapper(world.Entity()));
            Entity e2 = world.Entity().Set(new EntityWrapper(world.Entity()));
            Entity e3 = world.Entity().Set(new EntityWrapper(world.Entity()));

            world.Routine<EntityWrapper>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<EntityWrapper> c = it.Field<EntityWrapper>(0);

                        foreach (int i in it)
                            c[i].Value.Mut(it).Add<Position>();
                    }
                });

            world.Progress();

            Assert.True(e1.GetPtr<EntityWrapper>()->Value.Has<Position>());
            Assert.True(e2.GetPtr<EntityWrapper>()->Value.Has<Position>());
            Assert.True(e3.GetPtr<EntityWrapper>()->Value.Has<Position>());
        }

        [Fact]
        private void NewFromIter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(0, 0));
            Entity e2 = world.Entity().Set(new Position(0, 0));
            Entity e3 = world.Entity().Set(new Position(0, 0));

            world.Routine<Position>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);

                        foreach (int i in it)
                        {
                            it.Entity(i).Set(new EntityWrapper(
                                it.World().Entity().Add<Velocity>()
                            ));
                        }
                    }
                });

            world.Progress();

            Assert.True(e1.Has<EntityWrapper>());
            Assert.True(e2.Has<EntityWrapper>());
            Assert.True(e3.Has<EntityWrapper>());

            Assert.True(e1.GetPtr<EntityWrapper>()->Value.Has<Velocity>());
            Assert.True(e2.GetPtr<EntityWrapper>()->Value.Has<Velocity>());
            Assert.True(e3.GetPtr<EntityWrapper>()->Value.Has<Velocity>());
        }

        [Fact]
        private void EachWithMutChildrenIt()
        {
            using World world = World.Create();

            Entity parent = world.Entity().Set(new Position(0, 0));
            Entity e1 = world.Entity().Set(new Position(0, 0)).ChildOf(parent);
            Entity e2 = world.Entity().Set(new Position(0, 0)).ChildOf(parent);
            Entity e3 = world.Entity().Set(new Position(0, 0)).ChildOf(parent);

            int count = 0;

            world.Routine<Position>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Position> p = it.Field<Position>(0);

                        foreach (int i in it)
                        {
                            it.Entity(i).Children((Entity child) =>
                            {
                                child.Add<Velocity>();
                                count++;
                            });
                        }
                    }
                });

            world.Progress();

            Assert.Equal(3, count);

            Assert.True(e1.Has<Velocity>());
            Assert.True(e2.Has<Velocity>());
            Assert.True(e3.Has<Velocity>());
        }

        [Fact]
        private void ReadonlyChildrenIter()
        {
            using World world = World.Create();

            Entity parent = world.Entity();
            world.Entity().Set(new EntityWrapper(parent));
            world.Entity().Set(new Position(1, 0)).ChildOf(parent);
            world.Entity().Set(new Position(1, 0)).ChildOf(parent);
            world.Entity().Set(new Position(1, 0)).ChildOf(parent);

            int count = 0;

            world.Routine<EntityWrapper>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<EntityWrapper> c = it.Field<EntityWrapper>(0);

                        foreach (int i in it)
                        {
                            c[i].Value.Children((Entity child) =>
                            {
                                Position* p = child.GetPtr<Position>();
                                Assert.Equal(1, p->X);
                                Assert.Equal(0, p->Y);

                                count++;
                            });
                        }
                    }
                });

            world.Progress();

            Assert.Equal(3, count);
        }

        [Fact]
        private void RateFilter()
        {
            using World world = World.Create();

            int
                rootCount = 0,
                rootMult = 1,
                l1ACount = 0,
                l1AMult = 1,
                l1BCount = 0,
                l1BMult = 2,
                l1CCount = 0,
                l1CMult = 3,
                l2ACount = 0,
                l2AMult = 2,
                l2BCount = 0,
                l2BMult = 4,
                frameCount = 0;

            Routine root = world.Routine("root")
                .Run((Iter it) =>
                {
                    while (it.Next())
                        rootCount++;
                });

            Routine l1A = world.Routine("l1_a")
                .Rate(root, 1)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l1ACount++;
                });

            Routine l1B = world.Routine("l1_b")
                .Rate(root, 2)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l1BCount++;
                });

            world.Routine("l1_c")
                .Rate(root, 3)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l1CCount++;
                });

            world.Routine("l2_a")
                .Rate(l1A, 2)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l2ACount++;
                });

            world.Routine("l2_b")
                .Rate(l1B, 2)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l2BCount++;
                });

            for (int i = 0; i < 30; i++)
            {
                world.Progress();
                frameCount++;
                Assert.Equal(rootCount, frameCount / rootMult);
                Assert.Equal(l1ACount, frameCount / l1AMult);
                Assert.Equal(l1BCount, frameCount / l1BMult);
                Assert.Equal(l1CCount, frameCount / l1CMult);
                Assert.Equal(l2ACount, frameCount / l2AMult);
                Assert.Equal(l2BCount, frameCount / l2BMult);
            }
        }

        [Fact]
        private void UpdateRateFilter()
        {
            using World world = World.Create();

            int
                rootCount = 0,
                rootMult = 1,
                l1Count = 0,
                l1Mult = 2,
                l2Count = 0,
                l2Mult = 6,
                frameCount = 0;

            Routine root = world.Routine("root")
                .Run((Iter it) => {
                    while (it.Next())
                        rootCount++; });

            Routine l1 = world.Routine("l1")
                .Rate(root, 2)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l1Count++;
                });

            world.Routine("l2")
                .Rate(l1, 3)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        l2Count++;
                });

            for (int i = 0; i < 12; i++)
            {
                world.Progress();
                frameCount++;
                Assert.Equal(rootCount, frameCount / rootMult);
                Assert.Equal(l1Count, frameCount / l1Mult);
                Assert.Equal(l2Count, frameCount / l2Mult);
            }

            l1.Rate(4);
            l1Mult *= 2;
            l2Mult *= 2;

            frameCount = 0;
            l1Count = 0;
            l2Count = 0;
            rootCount = 0;

            for (int i = 0; i < 32; i++)
            {
                world.Progress();
                frameCount++;
                Assert.Equal(rootCount, frameCount / rootMult);
                Assert.Equal(l1Count, frameCount / l1Mult);
                Assert.Equal(l2Count, frameCount / l2Mult);
            }
        }

        [Fact]
        private void TestAutoDeferEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Tag>().Set(new Value(10));
            Entity e2 = world.Entity().Add<Tag>().Set(new Value(20));
            Entity e3 = world.Entity().Add<Tag>().Set(new Value(30));

            Routine s = world.Routine<Value>()
                .With<Tag>()
                .Each((Entity e, ref Value v) =>
                {
                    v.Number++;
                    e.Remove<Tag>();
                });

            s.Run();

            Assert.True(!e1.Has<Tag>());
            Assert.True(!e2.Has<Tag>());
            Assert.True(!e3.Has<Tag>());

            Assert.True(e1.Has<Value>());
            Assert.True(e2.Has<Value>());
            Assert.True(e3.Has<Value>());

            Assert.Equal(11, e1.GetPtr<Value>()->Number);
            Assert.Equal(21, e2.GetPtr<Value>()->Number);
            Assert.Equal(31, e3.GetPtr<Value>()->Number);
        }

        [Fact]
        private void TestAutoDeferIter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Add<Tag>().Set(new Value(10));
            Entity e2 = world.Entity().Add<Tag>().Set(new Value(20));
            Entity e3 = world.Entity().Add<Tag>().Set(new Value(30));

            Routine s = world.Routine<Value>()
                .With<Tag>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Value> v = it.Field<Value>(0);

                        foreach (int i in it)
                        {
                            v[i].Number++;
                            it.Entity(i).Remove<Tag>();
                        }
                    }
                });

            s.Run();

            Assert.True(!e1.Has<Tag>());
            Assert.True(!e2.Has<Tag>());
            Assert.True(!e3.Has<Tag>());

            Assert.True(e1.Has<Value>());
            Assert.True(e2.Has<Value>());
            Assert.True(e3.Has<Value>());

            Assert.Equal(11, e1.GetPtr<Value>()->Number);
            Assert.Equal(21, e2.GetPtr<Value>()->Number);
            Assert.Equal(31, e3.GetPtr<Value>()->Number);
        }

        [Fact]
        private void CustomPipeline()
        {
            using World world = World.Create();

            Entity preFrame = world.Entity().Add(Ecs.Phase);
            Entity onFrame = world.Entity().Add(Ecs.Phase).DependsOn(preFrame);
            Entity postFrame = world.Entity().Add(Ecs.Phase).DependsOn(onFrame);
            Entity tag = world.Entity();

            Entity pip = world.Pipeline()
                .With(Ecs.System)
                .With(Ecs.Phase).Cascade(Ecs.DependsOn)
                .With(tag)
                .Build();

            int count = 0;

            world.Routine()
                .Kind(postFrame)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.Equal(2, count);
                        count++;
                    }
                })
                .Entity.Add(tag);

            world.Routine()
                .Kind(onFrame)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.Equal(1, count);
                        count++;
                    }
                })
                .Entity.Add(tag);

            world.Routine()
                .Kind(preFrame)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.Equal(0, count);
                        count++;
                    }
                })
                .Entity.Add(tag);

            Assert.Equal(0, count);

            world.SetPipeline(pip);

            world.Progress();

            Assert.Equal(3, count);
        }

        [Fact]
        private void CustomPipelineWithKind()
        {
            using World world = World.Create();

            Entity tag = world.Entity();

            Entity pip = world.Pipeline()
                .With(Ecs.System)
                .With(tag)
                .Build();

            int count = 0;

            world.Routine()
                .Kind(tag)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.Equal(0, count);
                        count++;
                    }
                });

            world.Routine()
                .Kind(tag)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.Equal(1, count);
                        count++;
                    }
                });

            world.Routine()
                .Kind(tag)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.Equal(2, count);
                        count++;
                    }
                });

            Assert.Equal(0, count);

            world.SetPipeline(pip);

            world.Progress();

            Assert.Equal(3, count);
        }

        [Fact]
        private void InstancedQueryWithSingletonEach()
        {
            using World world = World.Create();

            world.Set(new Velocity(1, 2));

            Entity e1 = world.Entity().Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().Set(new Position(40, 50));
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().Set(new Position(50, 60));
            e5.Set(new Self(e5));

            e4.Add<Tag>();
            e5.Add<Tag>();

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .TermAt(2).Singleton()
                .Instanced()
                .Each((Entity e, ref Self s, ref Position p, ref Velocity v) =>
                {
                    Assert.True(e == s.Value);
                    p.X += v.X;
                    p.Y += v.Y;
                    count++;
                });

            s.Run();

            Assert.Equal(5, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));
        }

        [Fact]
        private void InstancedQueryWithBaseEach()
        {
            using World world = World.Create();

            Entity @base = world.Entity().Set(new Velocity(1, 2));

            Entity e1 = world.Entity().IsA(@base).Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().IsA(@base).Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().IsA(@base).Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().IsA(@base).Set(new Position(40, 50)).Add<Tag>();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().IsA(@base).Set(new Position(50, 60)).Add<Tag>();
            e5.Set(new Self(e5));
            Entity e6 = world.Entity().Set(new Position(60, 70)).Set(new Velocity(2, 3));
            e6.Set(new Self(e6));
            Entity e7 = world.Entity().Set(new Position(70, 80)).Set(new Velocity(4, 5));
            e7.Set(new Self(e7));

            int count = 0;
            Routine s = world.Routine<Self, Position, Velocity>()
                .Instanced()
                .Each((Entity e, ref Self s, ref Position p, ref Velocity v) =>
                {
                    Assert.True(e == s.Value);
                    p.X += v.X;
                    p.Y += v.Y;
                    count++;
                });

            s.Run();

            Assert.Equal(7, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));

            Assert.True(e6.Read((in Position p) =>
            {
                Assert.Equal(62, p.X);
                Assert.Equal(73, p.Y);
            }));

            Assert.True(e7.Read((in Position p) =>
            {
                Assert.Equal(74, p.X);
                Assert.Equal(85, p.Y);
            }));
        }

        [Fact]
        private void UninstancedQueryWithSingleton()
        {
            using World world = World.Create();

            world.Set(new Velocity(1, 2));

            Entity e1 = world.Entity().Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().Set(new Position(40, 50));
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().Set(new Position(50, 60));
            e5.Set(new Self(e5));

            e4.Add<Tag>();
            e5.Add<Tag>();

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .TermAt(2).Singleton()
                .Each((Entity e, ref Self s, ref Position p, ref Velocity v) =>
                {
                    Assert.True(e == s.Value);
                    p.X += v.X;
                    p.Y += v.Y;
                    count++;
                });

            s.Run();

            Assert.Equal(5, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));
        }

        [Fact]
        private void UninstancedQueryWithBaseEach()
        {
            using World world = World.Create();

            Entity @base = world.Entity().Set(new Velocity(1, 2));

            Entity e1 = world.Entity().IsA(@base).Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().IsA(@base).Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().IsA(@base).Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().IsA(@base).Set(new Position(40, 50)).Add<Tag>();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().IsA(@base).Set(new Position(50, 60)).Add<Tag>();
            e5.Set(new Self(e5));
            Entity e6 = world.Entity().Set(new Position(60, 70)).Set(new Velocity(2, 3));
            e6.Set(new Self(e6));
            Entity e7 = world.Entity().Set(new Position(70, 80)).Set(new Velocity(4, 5));
            e7.Set(new Self(e7));

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .Each((Entity e, ref Self s, ref Position p, ref Velocity v) =>
                {
                    Assert.True(e == s.Value);
                    p.X += v.X;
                    p.Y += v.Y;
                    count++;
                });

            s.Run();

            Assert.Equal(7, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));

            Assert.True(e6.Read((in Position p) =>
            {
                Assert.Equal(62, p.X);
                Assert.Equal(73, p.Y);
            }));

            Assert.True(e7.Read((in Position p) =>
            {
                Assert.Equal(74, p.X);
                Assert.Equal(85, p.Y);
            }));
        }

        [Fact]
        private void InstancedQueryWithSingletonIter()
        {
            using World world = World.Create();

            world.Set(new Velocity(1, 2));

            Entity e1 = world.Entity().Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().Set(new Position(40, 50));
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().Set(new Position(50, 60));
            e5.Set(new Self(e5));

            e4.Add<Tag>();
            e5.Add<Tag>();

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .TermAt(2).Singleton()
                .Instanced()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Self> s = it.Field<Self>(0);
                        Field<Position> p = it.Field<Position>(1);
                        Field<Velocity> v = it.Field<Velocity>(2);

                        Assert.True(it.Count() > 1);

                        foreach (int i in it)
                        {
                            p[i].X += v[0].X;
                            p[i].Y += v[0].Y;
                            Assert.True(it.Entity(i) == s[i].Value);
                            count++;
                        }
                    }
                });

            s.Run();

            Assert.Equal(5, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));
        }

        [Fact]
        private void InstancedQueryWithBaseIter()
        {
            using World world = World.Create();

            Entity @base = world.Entity().Set(new Velocity(1, 2));

            Entity e1 = world.Entity().IsA(@base).Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().IsA(@base).Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().IsA(@base).Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().IsA(@base).Set(new Position(40, 50)).Add<Tag>();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().IsA(@base).Set(new Position(50, 60)).Add<Tag>();
            e5.Set(new Self(e5));
            Entity e6 = world.Entity().Set(new Position(60, 70)).Set(new Velocity(2, 3));
            e6.Set(new Self(e6));
            Entity e7 = world.Entity().Set(new Position(70, 80)).Set(new Velocity(4, 5));
            e7.Set(new Self(e7));

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .Instanced()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Self> s = it.Field<Self>(0);
                        Field<Position> p = it.Field<Position>(1);
                        Field<Velocity> v = it.Field<Velocity>(2);

                        Assert.True(it.Count() > 1);

                        foreach (int i in it)
                        {
                            if (it.IsSelf(2))
                            {
                                p[i].X += v[i].X;
                                p[i].Y += v[i].Y;
                            }
                            else
                            {
                                p[i].X += v[0].X;
                                p[i].Y += v[0].Y;
                            }

                            Assert.True(it.Entity(i) == s[i].Value);
                            count++;
                        }
                    }
                });

            s.Run();

            Assert.Equal(7, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));

            Assert.True(e6.Read((in Position p) =>
            {
                Assert.Equal(62, p.X);
                Assert.Equal(73, p.Y);
            }));

            Assert.True(e7.Read((in Position p) =>
            {
                Assert.Equal(74, p.X);
                Assert.Equal(85, p.Y);
            }));
        }

        [Fact]
        private void UninstancedQueryWithSingletonIter()
        {
            using World world = World.Create();

            world.Set(new Velocity(1, 2));

            Entity e1 = world.Entity().Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().Set(new Position(40, 50));
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().Set(new Position(50, 60));
            e5.Set(new Self(e5));

            e4.Add<Tag>();
            e5.Add<Tag>();

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .TermAt(2).Singleton()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Self> s = it.Field<Self>(0);
                        Field<Position> p = it.Field<Position>(1);
                        Field<Velocity> v = it.Field<Velocity>(2);

                        Assert.True(it.Count() == 1);

                        foreach (int i in it)
                        {
                            p[i].X += v[i].X;
                            p[i].Y += v[i].Y;
                            Assert.True(it.Entity(i) == s[i].Value);
                            count++;
                        }
                    }
                });

            s.Run();

            Assert.Equal(5, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));
        }

        [Fact]
        private void UninstancedQueryWithBaseIter()
        {
            using World world = World.Create();

            Entity @base = world.Entity().Set(new Velocity(1, 2));

            Entity e1 = world.Entity().IsA(@base).Set(new Position(10, 20));
            e1.Set(new Self(e1));
            Entity e2 = world.Entity().IsA(@base).Set(new Position(20, 30));
            e2.Set(new Self(e2));
            Entity e3 = world.Entity().IsA(@base).Set(new Position(30, 40));
            e3.Set(new Self(e3));
            Entity e4 = world.Entity().IsA(@base).Set(new Position(40, 50)).Add<Tag>();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity().IsA(@base).Set(new Position(50, 60)).Add<Tag>();
            e5.Set(new Self(e5));
            Entity e6 = world.Entity().Set(new Position(60, 70)).Set(new Velocity(2, 3));
            e6.Set(new Self(e6));
            Entity e7 = world.Entity().Set(new Position(70, 80)).Set(new Velocity(4, 5));
            e7.Set(new Self(e7));

            int count = 0;

            Routine s = world.Routine<Self, Position, Velocity>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Field<Self> s = it.Field<Self>(0);
                        Field<Position> p = it.Field<Position>(1);
                        Field<Velocity> v = it.Field<Velocity>(2);

                        foreach (int i in it)
                        {
                            p[i].X += v[i].X;
                            p[i].Y += v[i].Y;
                            Assert.True(it.Entity(i) == s[i].Value);
                            count++;
                        }
                    }
                });

            s.Run();

            Assert.Equal(7, count);

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(21, p.X);
                Assert.Equal(32, p.Y);
            }));

            Assert.True(e3.Read((in Position p) =>
            {
                Assert.Equal(31, p.X);
                Assert.Equal(42, p.Y);
            }));

            Assert.True(e4.Read((in Position p) =>
            {
                Assert.Equal(41, p.X);
                Assert.Equal(52, p.Y);
            }));

            Assert.True(e5.Read((in Position p) =>
            {
                Assert.Equal(51, p.X);
                Assert.Equal(62, p.Y);
            }));

            Assert.True(e6.Read((in Position p) =>
            {
                Assert.Equal(62, p.X);
                Assert.Equal(73, p.Y);
            }));

            Assert.True(e7.Read((in Position p) =>
            {
                Assert.Equal(74, p.X);
                Assert.Equal(85, p.Y);
            }));
        }

        [Fact]
        private void CreateWithNoTemplateArgs()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            int count = 0;

            Routine s = world.Routine()
                .With<Position>()
                .Each((Entity e) =>
                {
                    Assert.True(e == entity);
                    count++;
                });

            s.Run();

            Assert.Equal(1, count);
        }

        [Fact]
        private void SystemWithTypeKindTypePipeline()
        {
            using World world = World.Create();

            world.Component<Second>().Entity
                .Add(Ecs.Phase)
                .DependsOn(
                    world.Component<First>().Entity
                        .Add(Ecs.Phase)
                );

            world.Pipeline<PipelineType>()
                .With(Ecs.System)
                .With(Ecs.Phase).Cascade(Ecs.DependsOn)
                .Build();

            world.SetPipeline<PipelineType>();

            Entity entity = world.Entity().Add<Tag>();

            int s1Count = 0;
            int s2Count = 0;

            world.Routine<Tag>()
                .Kind<Second>()
                .Each((Entity e) =>
                {
                    Assert.True(e == entity);
                    Assert.Equal(0, s1Count);
                    Assert.Equal(1, s2Count);
                    s1Count++;
                });

            world.Routine<Tag>()
                .Kind<First>()
                .Each((Entity e) =>
                {
                    Assert.True(e == entity);
                    Assert.Equal(0, s1Count);
                    s2Count++;
                });

            world.Progress();

            Assert.Equal(1, s1Count);
            Assert.Equal(1, s2Count);
        }

        [Fact]
        private void DefaultCtor()
        {
            using World world = World.Create();

            Routine sysVar;

            int count = 0;
            Routine sys = world.Routine<Position>()
                .Each((Entity e, ref Position p) =>
                {
                    Assert.Equal(10, p.X);
                    Assert.Equal(20, p.Y);
                    count++;
                });

            world.Entity().Set(new Position(10, 20));

            sysVar = sys;

            sysVar.Run();

            Assert.Equal(1, count);
        }

        [Fact]
        private void EntityCtor()
        {
            using World world = World.Create();

            int invoked = 0;

            Entity sys = world.Routine()
                .Run((Iter it) =>
                {
                    while (it.Next())
                        invoked++;
                });

            Routine sysFromId = world.Routine(sys);

            sysFromId.Run();
            Assert.Equal(1, invoked);
        }

        [Fact]
        private void EnsureInstancedWithEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(10, 20));

            int count = 0;
            Routine sys = world.Routine<Position>()
                .Each((Iter it, int i) =>
                {
                    Assert.True((it.Handle->flags & EcsIterIsInstanced) != 0);
                    Assert.True(it.Entity(i) == e1);
                    count++;
                });

            Query q = sys.Query();
            ecs_query_t* cF = q;
            Assert.True((cF->flags & EcsQueryIsInstanced) != 0);

            Assert.Equal(0, count);
            sys.Run();
            Assert.Equal(1, count);
        }

        [Fact]
        private void MultiThreadSystemWithQueryEach()
        {
            using World world = World.Create();

            world.SetThreads(2);

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            using Query q = world.Query<Velocity>();

            world.Routine<Position>()
                .MultiThreaded()
                .Each((Entity e, ref Position p) =>
                {
                    Position temp = p;

                    q.Iter(e).Each((ref Velocity v) =>
                    {
                        temp.X += v.X;
                        temp.Y += v.Y;
                    });

                    p = temp;
                });

            world.Progress();

            Position* p = e.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void MultiThreadSystemWithQueryEachWithIter()
        {
            using World world = World.Create();

            world.SetThreads(2);

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            using Query q = world.Query<Velocity>();

            world.Routine<Position>()
                .MultiThreaded()
                .Each((Iter it, int i, ref Position p) =>
                {
                    Position temp = p;

                    q.Iter(it).Each((ref Velocity v) =>
                    {
                        temp.X += v.X;
                        temp.Y += v.Y;
                    });

                    p = temp;
                });

            world.Progress();

            Position* p = e.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void MultiThreadSystemWithQueryEachWithWorld()
        {
            using World world = World.Create();

            world.SetThreads(2);

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            using Query q = world.Query<Velocity>();

            world.Routine<Position>()
                .MultiThreaded()
                .Each((Iter it, int i, ref Position p) =>
                {
                    Position temp = p;

                    q.Iter(it.World()).Each((ref Velocity v) =>
                    {
                        temp.X += v.X;
                        temp.Y += v.Y;
                    });

                    p = temp;
                });

            world.Progress();

            Position* p = e.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void MultiThreadSystemWithQueryIter()
        {
            using World world = World.Create();

            world.SetThreads(2);

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            using Query q = world.Query<Velocity>();

            world.Routine<Position>()
                .MultiThreaded()
                .Each((Entity e, ref Position p) =>
                {
                    Position temp = p;

                    q.Iter(e).Run((Iter it) =>
                    {
                        while (it.Next())
                        {
                            Field<Velocity> v = it.Field<Velocity>(0);

                            foreach (int i in it)
                            {
                                temp.X += v[i].X;
                                temp.Y += v[i].Y;
                            }
                        }
                    });

                    p = temp;
                });

            world.Progress();

            Position* p = e.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void MultiThreadSystemWithQueryIterWithIter()
        {
            using World world = World.Create();

            world.SetThreads(2);

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            using Query q = world.Query<Velocity>();

            world.Routine<Position>()
                .MultiThreaded()
                .Each((Iter it, int i, ref Position p) =>
                {
                    Position temp = p;

                    q.Iter(it).Run((Iter it) =>
                    {
                        while (it.Next())
                        {
                            Field<Velocity> v = it.Field<Velocity>(0);

                            foreach (int i in it)
                            {
                                temp.X += v[i].X;
                                temp.Y += v[i].Y;
                            }
                        }
                    });

                    p = temp;
                });

            world.Progress();

            Position* p = e.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void MultiThreadSystemWithQueryIterWithWorld()
        {
            using World world = World.Create();

            world.SetThreads(2);

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            using Query q = world.Query<Velocity>();

            world.Routine<Position>()
                .MultiThreaded()
                .Each((Iter it, int i, ref Position p) =>
                {
                    Position temp = p;

                    q.Iter(it.World()).Run((Iter it) =>
                    {
                        while (it.Next())
                        {
                            Field<Velocity> v = it.Field<Velocity>(0);

                            foreach (int i in it)
                            {
                                temp.X += v[i].X;
                                temp.Y += v[i].Y;
                            }
                        }
                    });

                    p = temp;
                });

            world.Progress();

            Position* p = e.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);
        }

        [Fact]
        private void RunCallback()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            world.Routine<Position, Velocity>()
                .Run((Iter it, Action<Iter> callback) =>
                {
                    while (it.Next())
                        callback(it);
                })
                .Each((ref Position p, ref Velocity v) =>
                {
                        p.X += v.X;
                        p.Y += v.Y;
                });

            world.Progress();

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void StartupSystem()
        {
            using World world = World.Create();

            int countA = 0, countB = 0;

            world.Routine()
                .Kind(Ecs.OnStart)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.True(it.DeltaTime() == 0);
                        countA++;
                    }
                });

            world.Routine()
                .Kind(Ecs.OnUpdate)
                .Run((Iter it) =>
                {
                    while (it.Next())
                    {
                        Assert.True(it.DeltaTime() != 0);
                        countB++;
                    }
                });

            world.Progress();
            Assert.Equal(1, countA);
            Assert.Equal(1, countB);

            world.Progress();
            Assert.Equal(1, countA);
            Assert.Equal(2, countB);
        }

        [Fact]
        private void IntervalTickSource()
        {
            using World world = World.Create();

            TimerEntity t = world.Timer().Interval(2.1f);

            ref EcsTimer timer = ref t.Entity.Ensure<EcsTimer>();
            timer.time = 0;

            int sysAInvoked = 0, sysBInvoked = 0;

            world.Routine()
                .TickSource(t)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysAInvoked++;
                });

            world.Routine()
                .TickSource(t)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysBInvoked++;
                });

            world.Progress(1.0f);
            Assert.Equal(0, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(0, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(1, sysAInvoked);
            Assert.Equal(1, sysBInvoked);
        }

        [Fact]
        private void RateTickSource()
        {
            using World world = World.Create();

            TimerEntity t = world.Timer().Rate(3);

            int sysAInvoked = 0, sysBInvoked = 0;

            world.Routine()
                .TickSource(t)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysAInvoked++;
                });

            world.Routine()
                .TickSource(t)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysBInvoked++;
                });

            world.Progress(1.0f);
            Assert.Equal(0, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(0, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(1, sysAInvoked);
            Assert.Equal(1, sysBInvoked);
        }

        [Fact]
        private void NestedRateTickSource()
        {
            using World world = World.Create();

            TimerEntity t3 = world.Timer().Rate(3);
            TimerEntity t6 = world.Timer().Rate(2, t3);

            int sysAInvoked = 0, sysBInvoked = 0;

            world.Routine()
                .TickSource(t3)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysAInvoked++;
                });

            world.Routine()
                .TickSource(t6)
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysBInvoked++;
                });

            world.Progress(1.0f);
            Assert.Equal(0, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(0, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(1, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(1, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(1, sysAInvoked);
            Assert.Equal(0, sysBInvoked);

            world.Progress(1.0f);
            Assert.Equal(2, sysAInvoked);
            Assert.Equal(1, sysBInvoked);
        }

        [Fact]
        private void TableGet()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(10, 20));
            Entity e2 = world.Entity().Set(new Position(20, 30));

            Routine s = world.Routine()
                .With<Position>()
                .Each((Iter it, int index) =>
                {
                    Entity e = it.Entity(index);
                    Position* p = &it.Table().GetPtr<Position>()[index];
                    Assert.True(p != null);
                    Assert.True(e == e1 || e == e2);
                    if (e == e1)
                    {
                        Assert.Equal(10, p->X);
                        Assert.Equal(20, p->Y);
                    }
                    else if (e == e2)
                    {
                        Assert.Equal(20, p->X);
                        Assert.Equal(30, p->Y);
                    }
                });

            s.Run();
        }

        [Fact]
        private void RangeGet()
        {
            using World world = World.Create();

            Entity e1 = world.Entity().Set(new Position(10, 20));
            Entity e2 = world.Entity().Set(new Position(20, 30));

            Routine s = world.Routine()
                .With<Position>()
                .Each((Iter it, int index) =>
                {
                    Entity e = it.Entity(index);
                    Position* p = &it.Range().GetPtr<Position>()[index];
                    Assert.True(p != null);
                    Assert.True(e == e1 || e == e2);
                    if (e == e1)
                    {
                        Assert.Equal(10, p->X);
                        Assert.Equal(20, p->Y);
                    }
                    else if (e == e2)
                    {
                        Assert.Equal(20, p->X);
                        Assert.Equal(30, p->Y);
                    }
                });

            s.Run();
        }

        [Fact]
        private void RandomizeTimers()
        {
            using World world = World.Create();

            Entity s1 = world.Routine()
                .Interval(1.0f)
                .Run((Iter it) => { while (it.Next()) { } });

            {
                EcsTimer* t = s1.GetPtr<EcsTimer>();
                Assert.True(t != null);
                Assert.True(t->time == 0);
            }

            world.RandomizeTimers();

            Entity s2 = world.Routine()
                .Interval(1.0f)
                .Run((Iter it) => { while (it.Next()) { } });

            {
                EcsTimer* t = s1.GetPtr<EcsTimer>();
                Assert.True(t != null);
                Assert.True(t->time != 0);
            }

            {
                EcsTimer* t = s2.GetPtr<EcsTimer>();
                Assert.True(t != null);
                Assert.True(t->time != 0);
            }
        }

        [Fact]
        private void SingletonTickSource()
        {
            using World world = World.Create();

            world.Timer<Tag0>().Timeout(1.5f);

            int sysInvoked = 0;

            world.Routine()
                .TickSource<Tag0>()
                .Run((Iter it) =>
                {
                    while (it.Next())
                        sysInvoked++;
                });

            world.Progress(1.0f);
            Assert.Equal(0, sysInvoked);

            world.Progress(1.0f);
            Assert.Equal(1, sysInvoked);

            world.Progress(2.0f);
            Assert.Equal(1, sysInvoked);
        }

        [Fact]
        private void PipelineStepWithKindEnum()
        {
            using World world = World.Create();

            world.Entity(PipelineStepEnum.CustomStep).Add(Ecs.Phase).DependsOn(Ecs.OnStart);

            bool ranTest = false;

            world.Routine().Kind(PipelineStepEnum.CustomStep).Run((Iter it) =>
            {
                while (it.Next())
                    ranTest = true;
            });

            world.Progress();
            Assert.True(ranTest);
        }

        [Fact]
        private void PipelineStepDependsOnPipelineStepWithEnum()
        {
            using World world = World.Create();

            world.Entity(PipelineStepEnum.CustomStep).Add(Ecs.Phase).DependsOn(Ecs.OnStart);
            world.Entity(PipelineStepEnum.CustomStep2).Add(Ecs.Phase).DependsOn(PipelineStepEnum.CustomStep);

            bool ranTest = false;

            world.Routine().Kind(PipelineStepEnum.CustomStep2).Run((Iter it) =>
            {
                while (it.Next())
                    ranTest = true;
            });

            world.Progress();
            Assert.True(ranTest);
        }

        [Fact]
        private void RegisterTwiceWithEach()
        {
            using World world = World.Create();

            int count1 = 0;
            int count2 = 0;

            Routine sys1 = world.Routine("Test")
                .Each((Iter it, int i) =>
                {
                    count1++;
                });

            sys1.Run();
            Assert.Equal(1, count1);

            Routine sys2 = world.Routine("Test")
                .Each((Iter it, int i) =>
                {
                    count2++;
                });

            sys2.Run();
            Assert.Equal(1, count2);
        }

        [Fact]
        private void RegisterTwiceWithRun()
        {
            using World world = World.Create();

            int count1 = 0;
            int count2 = 0;

            Routine sys1 = world.Routine("Test")
                .Run((Iter it) =>
                {
                    count1++;
                });

            sys1.Run();
            Assert.Equal(1, count1);

            Routine sys2 = world.Routine("Test")
                .Run((Iter it) =>
                {
                    count2++;
                });

            sys2.Run();
            Assert.Equal(1, count2);
        }

        [Fact]
        private void RegisterTwiceWithRunEach()
        {
            using World world = World.Create();

            int count1 = 0;
            int count2 = 0;

            Routine sys1 = world.Routine("Test")
                .Run((Iter it) =>
                {
                    count1++;
                });

            sys1.Run();
            Assert.Equal(1, count1);

            Routine sys2 = world.Routine("Test")
                .Each((Iter it, int i) =>
                {
                    count2++;
                });

            sys2.Run();
            Assert.Equal(1, count2);
        }

        [Fact]
        private void RegisterTwiceWithEachRun()
        {
            using World world = World.Create();

            int count1 = 0;
            int count2 = 0;

            Routine sys1 = world.Routine("Test")
                .Each((Iter it, int i) =>
                {
                    count1++;
                });

            sys1.Run();
            Assert.Equal(1, count1);

            Routine sys2 = world.Routine("Test")
                .Run((Iter it) =>
                {
                    count2++;
                });

            sys2.Run();
            Assert.Equal(1, count2);
        }
    }
}
