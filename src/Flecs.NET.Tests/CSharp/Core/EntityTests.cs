using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp
{
    public class EntityTests
    {
        [Fact]
        public void AddManaged()
        {
            using World world = World.Create();
            Entity entity = world.Entity();

            entity.Add<string>();
            Assert.True(entity.Has<string>());
        }

        [Fact]
        public void SetManaged()
        {
            using World world = World.Create();
            Entity entity = world.Entity();

            entity.Set<string>("Text");
            Assert.True(entity.Has<string>());

            ref readonly string str = ref entity.Get<string>();
            Assert.True(str == "Text");
        }
    }
}
