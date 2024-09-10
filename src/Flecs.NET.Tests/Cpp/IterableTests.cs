using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    [SuppressMessage("ReSharper", "AccessToModifiedClosure")]
    public class IterableTests
    {
        [Fact]
        private void PageEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            e1.Set(new Self(e1));
            Entity e2 = world.Entity();
            e2.Set(new Self(e2));
            Entity e3 = world.Entity();
            e3.Set(new Self(e3));
            Entity e4 = world.Entity();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity();
            e5.Set(new Self(e5));

            using Query<Self> q = world.Query<Self>();

            int count = 0;
            q.Page(1, 3).Each((Entity e, ref Self self) =>
            {
                count++;
                Assert.True(e != e1);
                Assert.True(e != e5);
                Assert.True(e == self.Value);
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void PageIter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            e1.Set(new Self(e1));
            Entity e2 = world.Entity();
            e2.Set(new Self(e2));
            Entity e3 = world.Entity();
            e3.Set(new Self(e3));
            Entity e4 = world.Entity();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity();
            e5.Set(new Self(e5));

            using Query<Self> q = world.Query<Self>();

            int count = 0;
            q.Page(1, 3).Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> self = it.Field<Self>(0);
                    Assert.Equal(3, it.Count());
                    Assert.True(it.Entity(0) == e2);
                    Assert.True(it.Entity(1) == e3);
                    Assert.True(it.Entity(2) == e4);
                    Assert.True(it.Entity(0) == self[0].Value);
                    Assert.True(it.Entity(1) == self[1].Value);
                    Assert.True(it.Entity(2) == self[2].Value);
                    count += it.Count();
                }
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void WorkerEach()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            e1.Set(new Self(e1));
            Entity e2 = world.Entity();
            e2.Set(new Self(e2));
            Entity e3 = world.Entity();
            e3.Set(new Self(e3));
            Entity e4 = world.Entity();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity();
            e5.Set(new Self(e5));

            using Query<Self> q = world.Query<Self>();

            int count = 0;
            q.Worker(0, 2).Each((Entity e, ref Self self) =>
            {
                count++;
                Assert.True(e != e4);
                Assert.True(e != e5);
                Assert.True(e == self.Value);
            });

            Assert.Equal(3, count);

            count = 0;
            q.Worker(1, 2).Each((Entity e, ref Self self) =>
            {
                count++;
                Assert.True(e != e1);
                Assert.True(e != e2);
                Assert.True(e != e3);
                Assert.True(e == self.Value);
            });

            Assert.Equal(2, count);
        }

        [Fact]
        private void WorkerIter()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            e1.Set(new Self(e1));
            Entity e2 = world.Entity();
            e2.Set(new Self(e2));
            Entity e3 = world.Entity();
            e3.Set(new Self(e3));
            Entity e4 = world.Entity();
            e4.Set(new Self(e4));
            Entity e5 = world.Entity();
            e5.Set(new Self(e5));

            using Query<Self> q = world.Query<Self>();

            int count = 0;
            q.Worker(0, 2).Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> self = it.Field<Self>(0);
                    Assert.Equal(3, it.Count());
                    Assert.True(it.Entity(0) == e1);
                    Assert.True(it.Entity(1) == e2);
                    Assert.True(it.Entity(2) == e3);
                    Assert.True(it.Entity(0) == self[0].Value);
                    Assert.True(it.Entity(1) == self[1].Value);
                    Assert.True(it.Entity(2) == self[2].Value);
                    count += it.Count();
                }
            });

            Assert.Equal(3, count);

            count = 0;
            q.Worker(1, 2).Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Self> self = it.Field<Self>(0);
                    Assert.Equal(2, it.Count());
                    Assert.True(it.Entity(0) == e4);
                    Assert.True(it.Entity(1) == e5);
                    Assert.True(it.Entity(0) == self[0].Value);
                    Assert.True(it.Entity(1) == self[1].Value);
                    count += it.Count();
                }
            });

            Assert.Equal(2, count);
        }
    }
}
