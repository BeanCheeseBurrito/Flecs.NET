using System;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public unsafe class QueryTests
    {
        [Fact]
        private void IterCallbackDelegate()
        {
            using World world = World.Create();
            using Query query = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(Callback);

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
            using Query query = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query query = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Run(Callback);

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
        private void IterCallbackPointer()
        {
            using World world = World.Create();
            using Query query = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter(&Callback);

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
            using Query query = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query query = world.QueryBuilder()
                .With<Position>()
                .With<Velocity>()
                .Build();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each(&Callback);

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
            using Query<Position, Velocity> query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Run(&Callback);

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
    }
}
