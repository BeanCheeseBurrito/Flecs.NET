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
            world.Component<Tag23>();

            using Query q = world.QueryBuilder()
                .With<Tag23>()
                .GroupBy<Tag23>(GroupByFirstId)
                .Build();

            using Query qReverse = world.QueryBuilder()
                .With<Tag23>()
                .GroupBy<Tag23>(GroupByFirstIdNegated)
                .Build();

            Entity e3 = world.Entity().Add<Tag23>().Add<Tag2>();
            Entity e2 = world.Entity().Add<Tag23>().Add<Tag1>();
            Entity e1 = world.Entity().Add<Tag23>().Add<Tag0>();

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

            static ulong GroupByFirstId(World world, Table table, Entity id)
            {
                Types type = table.Type();
                return type.Get(0);
            }

            static ulong GroupByFirstIdNegated(World world, Table table, Entity id)
            {
                return ~GroupByFirstId(world, table, id);
            }
        }
}