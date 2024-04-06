using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    [SuppressMessage("ReSharper", "AccessToModifiedClosure")]
    public class QueryBuilderTests
    {
        public QueryBuilderTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void GroupBy()
        {
            using World world = World.Create();

            world.Component<TagA>();
            world.Component<TagB>();
            world.Component<TagC>();
            world.Component<TagX>();

            Query q = world.QueryBuilder()
                .With<TagX>()
                .GroupBy<TagX>(GroupByFirstId)
                .Build();

            Query qReverse = world.QueryBuilder()
                .With<TagX>()
                .GroupBy<TagX>(GroupByFirstIdNegated)
                .Build();

            Entity e3 = world.Entity().Add<TagX>().Add<TagC>();
            Entity e2 = world.Entity().Add<TagX>().Add<TagB>();
            Entity e1 = world.Entity().Add<TagX>().Add<TagA>();

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
}
