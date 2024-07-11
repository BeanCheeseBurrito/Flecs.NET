using System;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public unsafe class ObserverTests
    {
        [Fact]
        private void IterCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it)
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void IterFieldCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, Field<Position> p, Field<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void IterSpanCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, Span<Position> p, Span<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void IterPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, Position* p, Velocity* v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void EachEntityCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Entity e)
            {
                ref Position p = ref e.GetMut<Position>();
                ref Velocity v = ref e.GetMut<Velocity>();

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachIterCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, int i)
            {
                Entity e = it.Entity(i);

                ref Position p = ref it.FieldAt<Position>(0, i);
                ref Velocity v = ref it.FieldAt<Velocity>(1, i);

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(ref Position p, ref Velocity v)
            {
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Position* p, Velocity* v)
            {
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void EachEntityRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Entity e, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachEntityPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Entity e, Position* p, Velocity* v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void EachIterRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, int i, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachIterPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, int i, Position* p, Velocity* v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it)
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Field<Velocity> v = it.Field<Velocity>(1);

                    foreach (int i in it)
                    {
                        Assert.Equal("e", it.Entity(i).Name());
                        Assert.Equal(new Position(10, 20), p[i]);
                        Assert.Equal(new Velocity(30, 40), v[i]);

                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                    }
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it)
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterFieldCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Field<Position> p, Field<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterSpanCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Span<Position> p, Span<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Position* p, Velocity* v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachEntityCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e)
            {
                ref Position p = ref e.GetMut<Position>();
                ref Velocity v = ref e.GetMut<Velocity>();

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachIterCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i)
            {
                Entity e = it.Entity(i);

                ref Position p = ref it.FieldAt<Position>(0, i);
                ref Velocity v = ref it.FieldAt<Velocity>(1, i);

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(ref Position p, ref Velocity v)
            {
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Position* p, Velocity* v)
            {
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachEntityRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachEntityPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, Position* p, Velocity* v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachIterRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachIterPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, Position* p, Velocity* v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

#if NET5_0_OR_GREATER
        [Fact]
        private void IterCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it)
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void IterFieldCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, Field<Position> p, Field<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void IterSpanCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, Span<Position> p, Span<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void IterPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, Position* p, Velocity* v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void EachEntityCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Entity e)
            {
                ref Position p = ref e.GetMut<Position>();
                ref Velocity v = ref e.GetMut<Velocity>();

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachIterCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, int i)
            {
                Entity e = it.Entity(i);

                ref Position p = ref it.FieldAt<Position>(0, i);
                ref Velocity v = ref it.FieldAt<Velocity>(1, i);

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(ref Position p, ref Velocity v)
            {
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Position* p, Velocity* v)
            {
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void EachEntityRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Entity e, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachEntityPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Entity e, Position* p, Velocity* v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void EachIterRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, int i, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void EachIterPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it, int i, Position* p, Velocity* v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Callback(Iter it)
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Field<Velocity> v = it.Field<Velocity>(1);

                    foreach (int i in it)
                    {
                        Assert.Equal("e", it.Entity(i).Name());
                        Assert.Equal(new Position(10, 20), p[i]);
                        Assert.Equal(new Velocity(30, 40), v[i]);

                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                    }
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it)
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterFieldCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Field<Position> p, Field<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterSpanCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Span<Position> p, Span<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Position* p, Velocity* v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachEntityCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e)
            {
                ref Position p = ref e.GetMut<Position>();
                ref Velocity v = ref e.GetMut<Velocity>();

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachIterCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i)
            {
                Entity e = it.Entity(i);

                ref Position p = ref it.FieldAt<Position>(0, i);
                ref Velocity v = ref it.FieldAt<Velocity>(1, i);

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(ref Position p, ref Velocity v)
            {
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Position* p, Velocity* v)
            {
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachEntityRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachEntityPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, Position* p, Velocity* v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachIterRefCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachIterPointerCallbackDelegate()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, Position* p, Velocity* v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it)
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterFieldCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Field<Position> p, Field<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterSpanCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Span<Position> p, Span<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithIterPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Position* p, Velocity* v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachEntityCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e)
            {
                ref Position p = ref e.GetMut<Position>();
                ref Velocity v = ref e.GetMut<Velocity>();

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachIterCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i)
            {
                Entity e = it.Entity(i);

                ref Position p = ref it.FieldAt<Position>(0, i);
                ref Velocity v = ref it.FieldAt<Velocity>(1, i);

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(ref Position p, ref Velocity v)
            {
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Position* p, Velocity* v)
            {
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachEntityRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachEntityPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, Position* p, Velocity* v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachIterRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackDelegateWithEachIterPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, Position* p, Velocity* v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it)
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterFieldCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Field<Position> p, Field<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterSpanCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Span<Position> p, Span<Velocity> v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithIterPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Iter<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, Position* p, Velocity* v)
            {
                foreach (int i in it)
                {
                    Assert.Equal("e", it.Entity(i).Name());
                    Assert.Equal(new Position(10, 20), p[i]);
                    Assert.Equal(new Velocity(30, 40), v[i]);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachEntityCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e)
            {
                ref Position p = ref e.GetMut<Position>();
                ref Velocity v = ref e.GetMut<Velocity>();

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachIterCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i)
            {
                Entity e = it.Entity(i);

                ref Position p = ref it.FieldAt<Position>(0, i);
                ref Velocity v = ref it.FieldAt<Velocity>(1, i);

                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(ref Position p, ref Velocity v)
            {
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Position* p, Velocity* v)
            {
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachEntityRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachEntityPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Entity e, Position* p, Velocity* v)
            {
                Assert.Equal("e", e.Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachIterRefCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, ref Position p, ref Velocity v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), p);
                Assert.Equal(new Velocity(30, 40), v);

                p.X += v.X;
                p.Y += v.Y;
            }
        }

        [Fact]
        private void RunCallbackPointerWithEachIterPointerCallbackPointer()
        {
            using World world = World.Create();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            world.Observer<Position, Velocity>()
                .Event(Ecs.OnSet)
                .YieldExisting()
                .Run(&Run)
                .Each<Position, Velocity>(&Callback);

            Assert.Equal(new Position(40, 60), e.Get<Position>());

            return;

            static void Run(Iter it, Action<Iter> callback)
            {
                while (it.Next())
                    callback(it);
            }

            static void Callback(Iter it, int i, Position* p, Velocity* v)
            {
                Assert.Equal("e", it.Entity(i).Name());
                Assert.Equal(new Position(10, 20), *p);
                Assert.Equal(new Velocity(30, 40), *v);

                p->X += v->X;
                p->Y += v->Y;
            }
        }
#endif
    }
}
