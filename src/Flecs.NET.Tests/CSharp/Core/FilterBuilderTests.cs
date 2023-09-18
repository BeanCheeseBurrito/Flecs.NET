using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{
    public class FilterBuilderTests
    {
        public FilterBuilderTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        private void TermAt()
        {
            using World world = World.Create();

            using FilterBuilder filterBuilder = world.FilterBuilder();

            filterBuilder.With<Position>();
            filterBuilder.With<Position>();
            filterBuilder.With<Position>();
            Assert.Equal(3, filterBuilder.Terms.Count);

            filterBuilder.TermAt(2).Id(500);
            Assert.Equal(500UL, filterBuilder.Terms[1].src.id);
        }
    }
}
