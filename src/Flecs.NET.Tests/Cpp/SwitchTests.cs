using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public class SwitchTests
    {
        public SwitchTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void AddCase()
        {
            using World world = World.Create();

            Entity standing = world.Entity("Standing");
            Entity walking = world.Entity("Walking");
            Entity movement = world.Entity().Add(Ecs.Union);

            Entity e = world.Entity()
                .Add(movement, standing);
            Assert.True(e.Has(movement, standing));

            Table table = e.Table();

            e.Add(movement, walking);
            Assert.True(e.Table() == table);

            Assert.True(e.Has(movement, walking));
            Assert.True(!e.Has(movement, standing));
        }

        [Fact]
        private void GetCase()
        {
            using World world = World.Create();

            Entity standing = world.Entity("Standing");
            world.Entity("Walking");
            Entity movement = world.Entity().Add(Ecs.Union);

            Entity e = world.Entity()
                .Add(movement, standing);
            Assert.True(e.Has(movement, standing));

            Assert.True(e.Target(movement) == standing);
        }

        [Fact]
        private void SystemWithCase()
        {
            using World world = World.Create();

            Entity standing = world.Entity("Standing");
            Entity walking = world.Entity("Walking");
            Entity movement = world.Entity("Movement").Add(Ecs.Union);

            world.Entity().Add(movement, walking);
            world.Entity().Add(movement, walking);
            world.Entity().Add(movement, standing);

            int count = 0, invokeCount = 0;
            world.Routine()
                .Expr("(Movement, Walking)")
                .Iter((Iter it) =>
                {
                    Field<ulong> m = it.Field<ulong>(1);

                    invokeCount++;
                    foreach (int i in it)
                    {
                        Assert.True(m[i] == walking);
                        count++;
                    }
                });

            world.Progress();

            Assert.Equal(2, invokeCount);
            Assert.Equal(2, count);
        }

        [Fact]
        private void SystemWithCaseBuilder()
        {
            using World world = World.Create();

            Entity standing = world.Entity("Standing");
            Entity walking = world.Entity("Walking");
            Entity movement = world.Entity().Add(Ecs.Union);

            world.Entity().Add(movement, walking);
            world.Entity().Add(movement, walking);
            world.Entity().Add(movement, standing);

            int count = 0, invokeCount = 0;
            world.Routine()
                .Term(movement, walking)
                .Iter((Iter it) =>
                {
                    Field<ulong> m = it.Field<ulong>(1);

                    invokeCount++;
                    foreach (int i in it)
                    {
                        Assert.True(m[i] == walking);
                        count++;
                    }
                });

            world.Progress();

            Assert.Equal(2, invokeCount);
            Assert.Equal(2, count);
        }

        [Fact]
        private void SystemWithSwitch()
        {
            using World world = World.Create();

            Entity standing = world.Entity("Standing");
            Entity walking = world.Entity("Walking");
            Entity movement = world.Entity("Movement").Add(Ecs.Union);

            Entity e1 = world.Entity().Add(movement, walking);
            Entity e2 = world.Entity().Add(movement, walking);
            Entity e3 = world.Entity().Add(movement, standing);

            int count = 0, invokeCount = 0;
            world.Routine()
                .Expr("(Movement, *)")
                .Iter((Iter it) =>
                {
                    Field<ulong> m = it.Field<ulong>(1);

                    invokeCount++;
                    foreach (int i in it)
                    {
                        if (it.Entity(i) == e1 || it.Entity(i) == e2)
                            Assert.Equal(walking, m[i]);
                        else if (it.Entity(i) == e3) Assert.Equal(standing, m[i]);
                        count++;
                    }
                });

            world.Progress();

            Assert.Equal(1, invokeCount);
            Assert.Equal(3, count);
        }

        [Fact]
        private void SystemWithSwitchTypeBuilder()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            world.Entity().Add<Movement, Walking>();
            world.Entity().Add<Movement, Walking>();
            world.Entity().Add<Movement, Standing>();

            int count = 0, invokeCount = 0;
            world.Routine()
                .Term<Movement, Walking>()
                .Iter((Iter it) =>
                {
                    Field<ulong> movement = it.Field<ulong>(1);

                    invokeCount++;
                    foreach (int i in it)
                    {
                        Assert.True(movement[i] == world.Id<Walking>());
                        count++;
                    }
                });

            world.Progress();

            Assert.Equal(2, invokeCount);
            Assert.Equal(2, count);
        }

        [Fact]
        private void AddCaseWithType()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add<Movement, Standing>();
            Assert.True(e.Has<Movement, Standing>());

            e.Add<Movement, Walking>();

            Assert.True(e.Has<Movement, Walking>());
            Assert.True(!e.Has<Movement, Standing>());
        }

        [Fact]
        private void AddSwitchWithType()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add<Movement, Standing>();
            Assert.True(e.Has<Movement, Standing>());

            e.Add<Movement, Walking>();

            Assert.True(e.Has<Movement, Walking>());
            Assert.True(!e.Has<Movement, Standing>());
        }

        [Fact]
        private void AddRemoveSwitchWithType()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add<Movement, Standing>();
            Assert.True(e.Has<Movement>(Ecs.Wildcard));
            Assert.True(e.Has<Movement, Standing>());

            Table table = e.Table();

            e.Add<Movement, Walking>();

            Assert.True(e.Has<Movement, Walking>());
            Assert.True(!e.Has<Movement, Standing>());
            Assert.True(e.Table() == table);

            Entity c = e.Target<Movement>();
            Assert.True(c != 0);
            Assert.True(c == world.Id<Walking>());

            e.Remove<Movement>(Ecs.Wildcard);
            Assert.True(!e.Has<Movement>(Ecs.Wildcard));
            Assert.True(!e.Has<Movement, Walking>());
            Assert.True(e.Table() != table);
        }

        [Fact]
        private void SwitchEnumType()
        {
            using World world = World.Create();

            world.Component<Color>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add(Color.Red);
            Assert.True(e.Has(Color.Red));
            Assert.True(!e.Has(Color.Green));
            Assert.True(!e.Has(Color.Blue));
            Assert.True(e.Has<Color>(Ecs.Wildcard));

            Table table = e.Table();

            e.Add(Color.Green);
            Assert.True(!e.Has(Color.Red));
            Assert.True(e.Has(Color.Green));
            Assert.True(!e.Has(Color.Blue));
            Assert.True(e.Has<Color>(Ecs.Wildcard));
            Assert.True(e.Table() == table);

            e.Add(Color.Blue);
            Assert.True(!e.Has(Color.Red));
            Assert.True(!e.Has(Color.Green));
            Assert.True(e.Has(Color.Blue));
            Assert.True(e.Has<Color>(Ecs.Wildcard));
            Assert.True(e.Table() == table);

            e.Remove<Color>();
            Assert.True(!e.Has(Color.Red));
            Assert.True(!e.Has(Color.Green));
            Assert.True(!e.Has(Color.Blue));
            Assert.True(!e.Has<Color>(Ecs.Wildcard));
            Assert.True(e.Table() != table);
        }
    }
}
