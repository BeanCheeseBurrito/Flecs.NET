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

        [Fact]
        private void EachManagedClass()
        {
            using World world = World.Create();
            using Query<ManagedClass> query = world.Query<ManagedClass>();

            for (int i = 0; i < 5; i ++)
                world.Entity().Set(new ManagedClass(10));

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter _, int _, ref ManagedClass component) =>
            {
                Assert.Equal(10, component.Value);
            });
        }

        [Fact]
        private void EachManagedStruct()
        {
            using World world = World.Create();
            using Query<ManagedStruct> query = world.Query<ManagedStruct>();

            for (int i = 0; i < 5; i ++)
                world.Entity().Set(new ManagedStruct(10));

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter _, int _, ref ManagedStruct component) =>
            {
                Assert.Equal(10, component.Value);
            });
        }

        [Fact]
        private void EachSharedUnmanaged()
        {
            using World world = World.Create();

            world.Component<SharedComponent>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            using Query<UnmanagedComponent, SharedComponent> query = world.Query<UnmanagedComponent, SharedComponent>();

            Entity prefab = world.Prefab()
                .Set(new UnmanagedComponent(10))
                .Set(new SharedComponent(20));

            for (int i = 0; i < 5; i++)
                world.Entity().IsA(prefab);

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter it, int _, ref UnmanagedComponent c1, ref SharedComponent c2) =>
            {
                Assert.Equal(IterationTechnique.Shared, it.GetIterationTechnique(2));
                Assert.Equal(10, c1.Value);
                Assert.Equal(20, c2.Value);
            });
        }

        [Fact]
        private void EachSharedManaged()
        {
            using World world = World.Create();

            world.Component<SharedComponent>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            using Query<ManagedComponent, SharedComponent> query = world.Query<ManagedComponent, SharedComponent>();

            Entity prefab = world.Prefab()
                .Set(new ManagedComponent(10))
                .Set(new SharedComponent(20));

            for (int i = 0; i < 5; i++)
                world.Entity().IsA(prefab);

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter it, int _, ref ManagedComponent c1, ref SharedComponent c2) =>
            {
                Assert.Equal(IterationTechnique.Shared, it.GetIterationTechnique(2));
                Assert.Equal(10, c1.Value);
                Assert.Equal(20, c2.Value);
            });
        }

        [Fact]
        private void EachSparseUnmanaged()
        {
            using World world = World.Create();

            world.Component<SparseComponent>().Entity.Add(Ecs.Sparse);

            using Query<UnmanagedComponent, SparseComponent> query = world.Query<UnmanagedComponent, SparseComponent>();

            Entity prefab = world.Prefab()
                .Set(new UnmanagedComponent(10))
                .Set(new SparseComponent(20));

            for (int i = 0; i < 5; i++)
                world.Entity().IsA(prefab);

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter it, int _, ref UnmanagedComponent c1, ref SparseComponent c2) =>
            {
                Assert.Equal(IterationTechnique.Sparse, it.GetIterationTechnique(2));
                Assert.Equal(10, c1.Value);
                Assert.Equal(20, c2.Value);
            });
        }

        [Fact]
        private void EachSparseManaged()
        {
            using World world = World.Create();

            world.Component<SparseComponent>().Entity.Add(Ecs.Sparse);

            using Query<ManagedComponent, SparseComponent> query = world.Query<ManagedComponent, SparseComponent>();

            Entity prefab = world.Prefab()
                .Set(new ManagedComponent(10))
                .Set(new SparseComponent(20));

            for (int i = 0; i < 5; i++)
                world.Entity().IsA(prefab);

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter it, int _, ref ManagedComponent c1, ref SparseComponent c2) =>
            {
                Assert.Equal(IterationTechnique.Sparse, it.GetIterationTechnique(2));
                Assert.Equal(10, c1.Value);
                Assert.Equal(20, c2.Value);
            });
        }

        [Fact]
        private void EachSparseSharedUnmanaged()
        {
            using World world = World.Create();

            world.Component<SparseComponent>().Entity.Add(Ecs.Sparse);
            world.Component<SharedComponent>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            using Query<UnmanagedComponent, SparseComponent, SharedComponent> query = world.Query<UnmanagedComponent, SparseComponent, SharedComponent>();

            Entity prefab = world.Prefab()
                .Set(new UnmanagedComponent(10))
                .Set(new SparseComponent(20))
                .Set(new SharedComponent(30));

            for (int i = 0; i < 5; i++)
                world.Entity().IsA(prefab);

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter it, int _, ref UnmanagedComponent c1, ref SparseComponent c2, ref SharedComponent c3) =>
            {
                Assert.Equal(IterationTechnique.Sparse | IterationTechnique.Shared, it.GetIterationTechnique(3));
                Assert.Equal(10, c1.Value);
                Assert.Equal(20, c2.Value);
                Assert.Equal(30, c3.Value);
            });
        }

        [Fact]
        private void EachSparseSharedManaged()
        {
            using World world = World.Create();

            world.Component<SparseComponent>().Entity.Add(Ecs.Sparse);
            world.Component<SharedComponent>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

            using Query<ManagedComponent, SparseComponent, SharedComponent> query = world.Query<ManagedComponent, SparseComponent, SharedComponent>();

            Entity prefab = world.Prefab()
                .Set(new ManagedComponent(10))
                .Set(new SparseComponent(20))
                .Set(new SharedComponent(30));

            for (int i = 0; i < 5; i++)
                world.Entity().IsA(prefab);

            Assert.True(query.IsTrue());
            Assert.Equal(5, query.Count());

            query.Each(static (Iter it, int _, ref ManagedComponent c1, ref SparseComponent c2, ref SharedComponent c3) =>
            {
                Assert.Equal(IterationTechnique.Sparse | IterationTechnique.Shared, it.GetIterationTechnique(3));
                Assert.Equal(10, c1.Value);
                Assert.Equal(20, c2.Value);
                Assert.Equal(30, c3.Value);
            });
        }
    }
}
