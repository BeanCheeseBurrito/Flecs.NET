using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

public class MiscTests
{
    [Fact]
    private void AppInit()
    {
        using World world = World.Create();

        world.System()
            .Each(static (Iter it, int _) =>
            {
                Assert.Equal("TestString", it.World().Get<string>());
            });

        world.App()
            .Init(static (World world) =>
            {
                world.Set("TestString");
            })
            .Frames(1)
            .Run();
    }
}
