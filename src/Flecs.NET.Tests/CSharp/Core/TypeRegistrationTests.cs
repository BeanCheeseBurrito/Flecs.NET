using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public unsafe class TypeRegistrationTests
    {
        [Fact]
        private void TypeClassComponentSize()
        {
            using World world = World.Create();

            Type<Position>.Id(world);
            Assert.Equal(sizeof(Position), Type<Position>.Size);
        }

        [Fact]
        private void TypeClassTagSize()
        {
            using World world = World.Create();

            Type<Tag>.Id(world);
            Assert.Equal(0, Type<Tag>.Size);
        }

        [Fact]
        private void TypeClassEnumSize()
        {
            using World world = World.Create();

            Type<StandardEnum>.Id(world);
            Assert.Equal(sizeof(StandardEnum), Type<StandardEnum>.Size);
        }

        [Fact]
        private void ComponentFactoryComponentSize()
        {
            using World world = World.Create();

            world.Component<Position>();
            Assert.Equal(sizeof(Position), Type<Position>.Size);
        }

        [Fact]
        private void ComponentFactoryTagSize()
        {
            using World world = World.Create();

            world.Component<Tag>();
            Assert.Equal(0, Type<Tag>.Size);
        }

        [Fact]
        private void ComponentFactoryEnumSize()
        {
            using World world = World.Create();

            world.Component<StandardEnum>();
            Assert.Equal(sizeof(StandardEnum), Type<StandardEnum>.Size);
        }
    }
}
