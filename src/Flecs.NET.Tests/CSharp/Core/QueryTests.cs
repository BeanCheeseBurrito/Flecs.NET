using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public unsafe class QueryTests
    {
        [Fact]
        private void IterDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

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
        private void Iter2TypesDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter<Position, Velocity>(Callback);

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
        private void Each2TypesDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each<Position, Velocity>(Callback);

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
        private void EachEntityDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

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
        private void EachEntity2TypesDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each<Position, Velocity>(Callback);

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
        private void EachIndexDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

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
        private void EachIndex2TypesDelegateCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each<Position, Velocity>(Callback);

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

#if NET5_0_OR_GREATER
        [Fact]
        private void IterPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

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
        private void Iter2TypesPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Iter<Position, Velocity>(&Callback);

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
        private void Each2TypesPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each<Position, Velocity>(&Callback);

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
        private void EachEntityPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

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
        private void EachEntity2TypesPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each<Position, Velocity>(&Callback);

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
        private void EachIndexPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

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
        private void EachIndex2TypesPointerCallback()
        {
            using World world = World.Create();
            using Query query = world.Query<Position, Velocity>();

            Entity e = world.Entity("e")
                .Set(new Position(10, 20))
                .Set(new Velocity(30, 40));

            query.Each<Position, Velocity>(&Callback);

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
#endif
    }
}
