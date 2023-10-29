using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public unsafe class TypeRegistrationTests
    {
        public TypeRegistrationTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void TypeClassComponentSize()
        {
            using World world = World.Create();

            Type<Position>.Id(world);
            Assert.Equal(sizeof(Position), Type<Position>.GetSize());
        }

        [Fact]
        private void TypeClassTagSize()
        {
            using World world = World.Create();

            Type<Tag>.Id(world);
            Assert.Equal(0, Type<Tag>.GetSize());
        }

        [Fact]
        private void TypeClassEnumSize()
        {
            using World world = World.Create();

            Type<StandardEnum>.Id(world);
            Assert.Equal(sizeof(StandardEnum), Type<StandardEnum>.GetSize());
        }

        [Fact]
        private void ComponentFactoryComponentSize()
        {
            using World world = World.Create();

            world.Component<Position>();
            Assert.Equal(sizeof(Position), Type<Position>.GetSize());
        }

        [Fact]
        private void ComponentFactoryTagSize()
        {
            using World world = World.Create();

            world.Component<Tag>();
            Assert.Equal(0, Type<Tag>.GetSize());
        }

        [Fact]
        private void ComponentFactoryEnumSize()
        {
            using World world = World.Create();

            world.Component<StandardEnum>();
            Assert.Equal(sizeof(StandardEnum), Type<StandardEnum>.GetSize());
        }
    }
}
