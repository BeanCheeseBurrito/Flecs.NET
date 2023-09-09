using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public class DocTests
    {
        public DocTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void SetBrief()
        {
            using World world = World.Create();

            Entity e = world.Entity("Foo");

            e.SetDocBrief("A brief description");

            Assert.True(e.Has<EcsDocDescription>(Ecs.Doc.Brief));

            Assert.Equal("A brief description", e.DocBrief());
        }

        [Fact]
        private void SetName()
        {
            using World world = World.Create();

            Entity e = world.Entity("Foo");

            e.SetDocName("A name");

            Assert.True(e.Has<EcsDocDescription>(Ecs.Name));

            Assert.Equal("A name", e.DocName());
        }

        [Fact]
        private void SetLink()
        {
            using World world = World.Create();

            Entity e = world.Entity("Foo");

            e.SetDocLink("A link");

            Assert.True(e.Has<EcsDocDescription>(Ecs.Doc.Link));

            Assert.Equal("A link", e.DocLink());
        }

        [Fact]
        private void SetColor()
        {
            using World world = World.Create();

            Entity e = world.Entity("Foo");

            e.SetDocColor("A color");

            Assert.True(e.Has<EcsDocDescription>(Ecs.Doc.Color));

            Assert.Equal("A color", e.DocColor());
        }

        [Fact]
        private void GetNameNoDocName()
        {
            using World world = World.Create();

            Entity e = world.Entity("Foo");

            Assert.True(!e.Has<EcsDocDescription>(Ecs.Name));

            Assert.Equal("Foo", e.DocName());
        }
    }
}
