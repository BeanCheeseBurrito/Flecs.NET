using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public class EventTests
    {
        public EventTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        public void EntityEmitEventWithManagedPayload()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Tag>();

            int count = 0;
            e.Observe((Entity src, ref string str) =>
            {
                count++;
                Assert.True(src == e);
                Assert.Equal("Test", str);
            });

            Assert.Equal(0, count);

            string str = "Test";
            e.Emit(ref str);

            Assert.Equal(1, count);
        }

        [Fact]
        public void EnqueueEntityEventWithManagedPayload()
        {
            using World world = World.Create();

            int count = 0;

            Entity id = world.Entity();
            Entity entity = world.Entity().Add(id);

            entity.Observe((ref string str) =>
            {
                Assert.Equal("Test", str);
                count++;
            });

            world.DeferBegin();

            string str = "Test";

            entity.Enqueue(ref str);

            Assert.Equal(0, count);

            world.DeferEnd();

            Assert.Equal(1, count);
        }
    }
}
