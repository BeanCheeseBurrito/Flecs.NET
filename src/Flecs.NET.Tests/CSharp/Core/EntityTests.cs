using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

public class EntityTests
{
    [Fact]
    public void AddManagedClass()
    {
            using World world = World.Create();
            Entity entity = world.Entity();

            entity.Add<ManagedClass>();
            Assert.True(entity.Has<ManagedClass>());
        }

    [Fact]
    public void SetManagedClass()
    {
            using World world = World.Create();
            Entity entity = world.Entity();

            entity.Set(new ManagedClass(10));
            Assert.True(entity.Has<ManagedClass>());

            ref readonly ManagedClass component = ref entity.Get<ManagedClass>();
            Assert.True(component.Value == 10);
        }

    [Fact]
    public void AddManagedStruct()
    {
            using World world = World.Create();
            Entity entity = world.Entity();

            entity.Add<ManagedStruct>();
            Assert.True(entity.Has<ManagedStruct>());
        }

    [Fact]
    public void SetManagedStruct()
    {
            using World world = World.Create();
            Entity entity = world.Entity();

            entity.Set(new ManagedStruct(10));
            Assert.True(entity.Has<ManagedStruct>());

            ref readonly ManagedStruct component = ref entity.Get<ManagedStruct>();
            Assert.True(component.Value == 10);
        }
}