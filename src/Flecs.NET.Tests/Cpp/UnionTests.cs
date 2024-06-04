using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public class UnionTests
    {
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
        private void AddCaseWithType()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add<Movement, Standing>();
            Assert.True((e.Has<Movement, Standing>()));

            e.Add<Movement, Walking>();

            Assert.True((e.Has<Movement, Walking>()));
            Assert.True((!e.Has<Movement, Standing>()));
        }

        [Fact]
        private void AddSwitchWithType()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add<Movement, Standing>();
            Assert.True((e.Has<Movement, Standing>()));

            e.Add<Movement, Walking>();

            Assert.True((e.Has<Movement, Walking>()));
            Assert.True((!e.Has<Movement, Standing>()));
        }

        [Fact]
        private void AddRemoveSwitchWithType()
        {
            using World world = World.Create();

            world.Component<Movement>().Entity.Add(Ecs.Union);

            Entity e = world.Entity().Add<Movement, Standing>();
            Assert.True(e.Has<Movement>(Ecs.Wildcard));
            Assert.True((e.Has<Movement, Standing>()));

            Table table = e.Table();

            e.Add<Movement, Walking>();

            Assert.True((e.Has<Movement, Walking>()));
            Assert.True((!e.Has<Movement, Standing>()));
            Assert.True(e.Table() == table);

            Entity c = e.Target<Movement>();
            Assert.True(c != 0);
            Assert.True(c == world.Id<Walking>());

            e.Remove<Movement>(Ecs.Wildcard);
            Assert.True(!e.Has<Movement>(Ecs.Wildcard));
            Assert.True((!e.Has<Movement, Walking>()));
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
