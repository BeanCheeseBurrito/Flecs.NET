using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public unsafe class TypeRegistrationTests // TODO: Add type registration tests
    {
        [Fact]
        private void StructSize()
        {
            using World world = World.Create();
            Assert.Equal(sizeof(Position), Type<Position>.Size);
        }

        [Fact]
        private void EmptyStructSize()
        {
            using World world = World.Create();
            Assert.Equal(0, Type<Tag>.Size);
        }

        [Fact]
        private void EnumSize()
        {
            using World world = World.Create();
            Assert.Equal(sizeof(Color), Type<Color>.Size);
        }

        [Fact]
        private void ClassSize()
        {
            using World world = World.Create();
            Assert.Equal(sizeof(string), Type<string>.Size);
        }
    }
}
