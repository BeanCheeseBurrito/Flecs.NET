using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class SnapshotTests
    {
        public SnapshotTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void SimpleSnapshot()
        {
            using World world = World.Create();

            Entity e = new Entity(world)
                .Set(new Position { X = 10, Y = 20 })
                .Set(new Velocity { X = 1, Y = 1 });

            Snapshot s = new Snapshot(world);
            s.Take();

            e.Set(new Position { X = 30, Y = 40 });
            e.Set(new Velocity { X = 2, Y = 2 });

            s.Restore();

            Position* p = e.GetPtr<Position>();
            Velocity* v = e.GetPtr<Velocity>();

            Assert.True(p != null);
            Assert.True(v != null);

            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Assert.Equal(1, v->X);
            Assert.Equal(1, v->Y);
        }
    }
}
