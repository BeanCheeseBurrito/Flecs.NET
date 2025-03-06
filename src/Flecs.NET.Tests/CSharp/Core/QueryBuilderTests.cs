using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

[SuppressMessage("ReSharper", "AccessToModifiedClosure")]
public class QueryBuilderTests
{
    [Fact]
    private void GroupBy()
    {
        using World world = World.Create();

        world.Component<Tag0>();
        world.Component<Tag1>();
        world.Component<Tag2>();
        world.Component<Tag3>();

        using Query q = world.QueryBuilder()
            .With<Tag3>()
            .GroupBy<Tag3>(GroupByFirstId)
            .Build();

        using Query qReverse = world.QueryBuilder()
            .With<Tag3>()
            .GroupBy<Tag3>(GroupByFirstIdNegated)
            .Build();

        Entity e3 = world.Entity().Add<Tag3>().Add<Tag2>();
        Entity e2 = world.Entity().Add<Tag3>().Add<Tag1>();
        Entity e1 = world.Entity().Add<Tag3>().Add<Tag0>();

        int count = 0;
        q.Iter((Iter it) =>
        {
            Assert.Equal(1, it.Count());

            if (count == 0)
                Assert.True(it.Entity(0) == e1);
            else if (count == 1)
                Assert.True(it.Entity(0) == e2);
            else if (count == 2)
                Assert.True(it.Entity(0) == e3);
            else
                Assert.True(false);

            count++;
        });

        Assert.Equal(3, count);

        count = 0;
        qReverse.Iter((Iter it) =>
        {
            Assert.Equal(1, it.Count());

            if (count == 0)
                Assert.True(it.Entity(0) == e3);
            else if (count == 1)
                Assert.True(it.Entity(0) == e2);
            else if (count == 2)
                Assert.True(it.Entity(0) == e1);
            else
                Assert.True(false);

            count++;
        });
        Assert.Equal(3, count);

        return;

        static ulong GroupByFirstId(World world, Table table, ulong id)
        {
            FlecsType type = table.Type();
            return type.Get(0);
        }

        static ulong GroupByFirstIdNegated(World world, Table table, ulong id)
        {
            return ~GroupByFirstId(world, table, id);
        }
    }


    [Fact]
    private void TermAt()
    {
        using World world = World.Create();
        world.Component<Tag>();
        world.Component<Position>();
        world.Component<Velocity>();

        world.QueryBuilder()
            .Cached()
            .With<Position>()
            .TermAt<Position>(0)
            .Build();

        world.QueryBuilder()
            .Cached()
            .With<Tag, Position>()
            .TermAt<Position>(0)
            .Build();

        world.QueryBuilder()
            .Cached()
            .With<Tag>().Second<Position>()
            .TermAt<Position>(0)
            .Build();

        Assert.Throws<Ecs.AssertionException>(() =>
        {
            world.QueryBuilder()
                .Cached()
                .With<Tag>().Second<Position>()
                .TermAt<Tag>(0)
                .Build();
        });

        Assert.Throws<Ecs.AssertionException>(() =>
        {
            world.QueryBuilder()
                .Cached()
                .With<Velocity>().Second<Position>()
                .TermAt<Position>(0)
                .Build();
        });

        Assert.Throws<Ecs.AssertionException>(() =>
        {
            world.Component<Tag>().Add(Ecs.PairIsTag);

            world.QueryBuilder()
                .Cached()
                .With<Tag, Position>()
                .TermAt<Position>(0)
                .Build();

            world.Component<Tag>().Remove(Ecs.PairIsTag);
        });
    }
}
