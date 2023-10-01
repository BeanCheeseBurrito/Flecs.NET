using Flecs.NET.Collections;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Collections
{
    public unsafe class UnsafeListTests
    {
        [Fact]
        public void CreateEmpty()
        {
            NativeList<Position> list = new NativeList<Position>();
            Assert.True(list.IsNull);
            Assert.Equal(0, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void CreateUnmanaged()
        {
            NativeList<Position> list = new NativeList<Position>(16);
            Assert.False(list.IsNull);
            Assert.Equal(16, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void AddUnmanaged()
        {
            NativeList<Position> list = new NativeList<Position>();
            Assert.True(list.IsNull);
            Assert.Equal(0, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Add(new Position());
            Assert.False(list.IsNull);
            Assert.Equal(1, list.Capacity);
            Assert.Equal(1, list.Count);

            list.Add(new Position());
            Assert.False(list.IsNull);
            Assert.Equal(2, list.Capacity);
            Assert.Equal(2, list.Count);

            list.Add(new Position());
            Assert.False(list.IsNull);
            Assert.Equal(4, list.Capacity);
            Assert.Equal(3, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void Get()
        {
            using NativeList<Position> list = new NativeList<Position>();

            list.Add(new Position() { X = 0 });
            list.Add(new Position() { X = 1 });
            list.Add(new Position() { X = 2 });

            Assert.Equal(new Position() { X = 0 }, list[0]);
            Assert.Equal(new Position() { X = 1 }, list[1]);
            Assert.Equal(new Position() { X = 2 }, list[2]);
        }

        [Fact]
        public void Set()
        {
            using NativeList<Position> list = new NativeList<Position>();

            list.Add(new Position() { X = 0 });
            list.Add(new Position() { X = 1 });
            list.Add(new Position() { X = 2 });

            Assert.Equal(new Position() { X = 0 }, list[0]);
            Assert.Equal(new Position() { X = 1 }, list[1]);
            Assert.Equal(new Position() { X = 2 }, list[2]);

            list[0] = new Position() { X = 0 + 10 };
            list[1] = new Position() { X = 1 + 10 };
            list[2] = new Position() { X = 2 + 10 };

            Assert.Equal(new Position() { X = 0 + 10 }, list[0]);
            Assert.Equal(new Position() { X = 1 + 10 }, list[1]);
            Assert.Equal(new Position() { X = 2 + 10 }, list[2]);
        }
    }
}
