using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public class TypeHookTests
    {
        public struct Pod
        {
            public int Value { get; set; }

            public static int CtorInvoked { get; set; }
            public static int DtorInvoked { get; set; }
            public static int MoveInvoked { get; set; }
            public static int CopyInvoked { get; set; }

            public Pod(int value)
            {
                Value = value;
            }

            public static void RegisterHooks(World world)
            {
                CtorInvoked = 0;
                DtorInvoked = 0;
                MoveInvoked = 0;
                CopyInvoked = 0;

                world.Component<Pod>()
                    .Ctor((ref Pod data, TypeInfo typeInfo) =>
                    {
                        CtorInvoked++;
                        data = default;
                        data.Value = 10;
                    })
                    .Dtor((ref Pod data, TypeInfo typeInfo) =>
                    {
                        DtorInvoked++;
                        data = default;
                    })
                    .Move((ref Pod dst, ref Pod src, TypeInfo typeInfo) =>
                    {
                        MoveInvoked++;
                        dst = src;
                        src = default;
                    })
                    .Copy((ref Pod dst, ref Pod src, TypeInfo typeInfo) =>
                    {
                        CopyInvoked++;
                        dst = src;
                    });
            }
        }

        [Fact]
        private void Add()
        {
            using World world = World.Create();

            Pod.RegisterHooks(world);

            Entity e = world.Entity().Add<Pod>();

            Assert.Equal(10, e.Get<Pod>().Value);
            Assert.Equal(1, Pod.CtorInvoked);
            Assert.Equal(0, Pod.DtorInvoked);
            Assert.Equal(0, Pod.CopyInvoked);
            Assert.Equal(0, Pod.MoveInvoked);
        }

        [Fact]
        private void AddRemove()
        {
            using World world = World.Create();

            Pod.RegisterHooks(world);

            Entity e = world.Entity().Add<Pod>();

            Assert.Equal(10, e.Get<Pod>().Value);
            Assert.Equal(1, Pod.CtorInvoked);
            Assert.Equal(0, Pod.DtorInvoked);
            Assert.Equal(0, Pod.CopyInvoked);
            Assert.Equal(0, Pod.MoveInvoked);

            e.Remove<Pod>();

            Assert.Equal(1, Pod.CtorInvoked);
            Assert.Equal(1, Pod.DtorInvoked);
            Assert.Equal(0, Pod.CopyInvoked);
            Assert.Equal(0, Pod.MoveInvoked);
        }

        [Fact]
        private void AddAdd()
        {
            using World world = World.Create();

            Pod.RegisterHooks(world);

            Entity e = world.Entity().Add<Pod>();

            Assert.Equal(10, e.Get<Pod>().Value);
            Assert.Equal(1, Pod.CtorInvoked);
            Assert.Equal(0, Pod.DtorInvoked);
            Assert.Equal(0, Pod.CopyInvoked);
            Assert.Equal(0, Pod.MoveInvoked);

            e.Add<Position>();

            Assert.Equal(2, Pod.CtorInvoked);
            Assert.Equal(1, Pod.DtorInvoked);
            Assert.Equal(0, Pod.CopyInvoked);
            Assert.Equal(1, Pod.MoveInvoked);
        }

        [Fact]
        private void Set()
        {
            using World world = World.Create();

            Pod.RegisterHooks(world);

            Entity e = world.Entity().Set(new Pod(20));

            Assert.Equal(20, e.Get<Pod>().Value);
            Assert.Equal(1, Pod.CtorInvoked);
            Assert.Equal(0, Pod.DtorInvoked);
            Assert.Equal(1, Pod.CopyInvoked);
            Assert.Equal(0, Pod.MoveInvoked);
        }
    }
}
