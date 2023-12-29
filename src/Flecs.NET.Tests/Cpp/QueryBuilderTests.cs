using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp
{
    public class QueryBuilderTests
    {
        public QueryBuilderTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        public void CascadeDesc()
        {
            using World world = World.Create();

            Entity tag = world.Entity();
            Entity foo = world.Entity();
            Entity bar = world.Entity();

            Entity e0 = world.Entity().Add(tag);
            Entity e1 = world.Entity().IsA(e0);
            Entity e2 = world.Entity().IsA(e1);
            Entity e3 = world.Entity().IsA(e2);

            Query q = world.QueryBuilder()
                .Term(tag).Cascade().Descend()
                .Build();

            e1.Add(bar);
            e2.Add(foo);

            bool e1Found = false;
            bool e2Found = false;
            bool e3Found = false;

            int count = 0;
            q.Each((Entity e) =>
            {
                count++;

                if (e == e1)
                {
                    Assert.False(e1Found);
                    Assert.True(e2Found);
                    Assert.True(e3Found);
                    e1Found = true;
                }

                if (e == e2)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.True(e3Found);
                    e2Found = true;
                }

                if (e == e3)
                {
                    Assert.False(e1Found);
                    Assert.False(e2Found);
                    Assert.False(e3Found);
                    e3Found = true;
                }
            });

            Assert.True(e1Found);
            Assert.True(e2Found);
            Assert.True(e3Found);
            Assert.Equal(3, count);
        }
    }
}
