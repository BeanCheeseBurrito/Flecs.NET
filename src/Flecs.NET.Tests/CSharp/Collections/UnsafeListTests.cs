using Flecs.NET.Collections;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Collections
{
    public unsafe class UnsafeListTests
    {
        [Fact]
        public void CreateEmpty()
        {
            UnsafeList<Position> list = new UnsafeList<Position>();
            Assert.True(list.IsNull);
            Assert.Equal(0, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void CreateUnmanaged()
        {
            UnsafeList<Position> list = new UnsafeList<Position>(16);
            Assert.False(list.IsManaged);
            Assert.False(list.IsNull);
            Assert.Equal(16, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void CreateManaged()
        {
            UnsafeList<string> list = new UnsafeList<string>(16);
            Assert.True(list.IsManaged);
            Assert.False(list.IsNull);
            Assert.Equal(16, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void AddUnmanaged()
        {
            UnsafeList<Position> list = new UnsafeList<Position>();
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
        public void AddManaged()
        {
            UnsafeList<string> list = new UnsafeList<string>();
            Assert.True(list.IsNull);
            Assert.Equal(0, list.Capacity);
            Assert.Equal(0, list.Count);

            list.Add(string.Empty);
            Assert.False(list.IsNull);
            Assert.Equal(1, list.Capacity);
            Assert.Equal(1, list.Count);

            list.Add(string.Empty);
            Assert.False(list.IsNull);
            Assert.Equal(2, list.Capacity);
            Assert.Equal(2, list.Count);

            list.Add(string.Empty);
            Assert.False(list.IsNull);
            Assert.Equal(4, list.Capacity);
            Assert.Equal(3, list.Count);

            list.Dispose();
            Assert.True(list.IsNull);
        }

        [Fact]
        public void GetUnmanaged()
        {
            using UnsafeList<Position> list = new UnsafeList<Position>();

            list.Add(new Position() { X = 0 });
            list.Add(new Position() { X = 1 });
            list.Add(new Position() { X = 2 });

            Assert.Equal(new Position() { X = 0 }, list[0]);
            Assert.Equal(new Position() { X = 1 }, list[1]);
            Assert.Equal(new Position() { X = 2 }, list[2]);
        }

        [Fact]
        public void GetManaged()
        {
            UnsafeList<string> list = new UnsafeList<string>();

            list.Add("0");
            list.Add("1");
            list.Add("2");

            Assert.Equal("0", list[0]);
            Assert.Equal("1", list[1]);
            Assert.Equal("2", list[2]);
        }

        [Fact]
        public void SetUnmanaged()
        {
            using UnsafeList<Position> list = new UnsafeList<Position>();

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

        [Fact]
        public void SetManaged()
        {
            using UnsafeList<string> list = new UnsafeList<string>();

            list.Add("0");
            list.Add("1");
            list.Add("2");

            Assert.Equal("0", list[0]);
            Assert.Equal("1", list[1]);
            Assert.Equal("2", list[2]);

            list[0] = "0" + "10";
            list[1] = "1" + "10";
            list[2] = "2" + "10";

            Assert.Equal("0" + "10", list[0]);
            Assert.Equal("1" + "10", list[1]);
            Assert.Equal("2" + "10", list[2]);
        }
    }
}