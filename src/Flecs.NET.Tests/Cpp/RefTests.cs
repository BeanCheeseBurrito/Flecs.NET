using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp;

public unsafe class RefTests
{
    [Fact]
    public void GetRefByPtr()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 });

        Ref<Position> reference = e.GetRef<Position>();
        Assert.True(reference.GetPtr()->X == 10);
        Assert.True(reference.GetPtr()->Y == 20);
    }

    [Fact]
    public void GetRefByMethod()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 });

        Ref<Position> reference = e.GetRef<Position>();
        Assert.True(reference.GetPtr()->X == 10);
        Assert.True(reference.GetPtr()->Y == 20);
    }

    [Fact]
    public void RefAfterAdd()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 });

        Ref<Position> reference = e.GetRef<Position>();

        e.Add<Velocity>();
        Assert.True(reference.GetPtr()->X == 10);
        Assert.True(reference.GetPtr()->Y == 20);
    }

    [Fact]
    public void RefAfterRemove()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 })
            .Set(new Velocity { X = 1, Y = 1 });

        Ref<Position> reference = e.GetRef<Position>();

        e.Remove<Velocity>();
        Assert.True(reference.GetPtr()->X == 10);
        Assert.True(reference.GetPtr()->Y == 20);
    }

    [Fact]
    public void RefAfterSet()
    {
        using World world = World.Create();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 });

        Ref<Position> reference = e.GetRef<Position>();

        e.Set(new Velocity { X = 1, Y = 1 });
        Assert.True(reference.GetPtr()->X == 10);
        Assert.True(reference.GetPtr()->Y == 20);
    }

    [Fact]
    public void RefBeforeSet()
    {
        using World world = World.Create();

        Entity e = world.Entity();
        Ref<Position> reference = e.GetRef<Position>();

        e.Set(new Position { X = 10, Y = 20 });

        Assert.True(reference.GetPtr()->X == 10);
        Assert.True(reference.GetPtr()->Y == 20);
    }

    [Fact]
    public void NonConstRef()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position { X = 10, Y = 20 });
        Ref<Position> reference = e.GetRef<Position>();
        reference.GetPtr()->X++;

        Assert.Equal(11, e.GetPtr<Position>()->X);
    }

    [Fact]
    public void PairRef()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set<Position, Tag>(new Position { X = 10, Y = 20 });
        Ref<Position> reference = e.GetRefFirst<Position, Tag>();
        reference.GetPtr()->X++;

        Assert.Equal(11, e.GetFirstPtr<Position, Tag>()->X);
    }

    [Fact]
    public void PairRefWithEntity()
    {
        using World world = World.Create();

        Entity tag = world.Entity();
        Entity e = world.Entity().Set(tag, new Position { X = 10, Y = 20 });
        Ref<Position> reference = e.GetRef<Position>(tag);
        reference.GetPtr()->X++;

        Assert.Equal(11, e.GetPtr<Position>(tag)->X);
    }

    [Fact]
    public void PairRefSecond()
    {
        using World world = World.Create();

        Entity tag = world.Entity();
        Entity e = world.Entity().SetSecond(tag, new Position { X = 10, Y = 20 });
        Ref<Position> reference = e.GetRefSecond<Position>(tag);
        reference.GetPtr()->X++;

        Assert.Equal(11, e.GetSecondPtr<Position>(tag)->X);
    }

    [Fact]
    public void FromStage()
    {
        using World world = World.Create();
        World stage = world.GetStage(0);
        Entity e = stage.Entity().Set(new Position { X = 10, Y = 20 });
        Ref<Position> reference = e.GetRef<Position>();
        Assert.Equal(10, reference.GetPtr()->X);
        Assert.Equal(20, reference.GetPtr()->Y);
    }

    [Fact]
    public void DefaultCtor()
    {
        using World world = World.Create();

        Ref<Position> p;

        Entity e = world.Entity().Set(new Position { X = 10, Y = 20 });

        p = e.GetRef<Position>();
        Assert.Equal(10, p.GetPtr()->X);
        Assert.Equal(20, p.GetPtr()->Y);
    }

    [Fact]
    public void TryGet()
    {
        using World world = World.Create();

        Ref<Position> p = default;

        Assert.True(p.TryGetPtr() == null);
    }

    [Fact]
    public void TryGetAfterDelete()
    {
        using World world = World.Create();

        Entity e = world.Entity().Set(new Position(10, 20));

        Ref<Position> p = e.GetRef<Position>();
        Position* ptr = p.TryGetPtr();
        Assert.True(ptr != null);
        Assert.Equal(10, ptr->X);
        Assert.Equal(20, ptr->Y);

        e.Destruct();

        ptr = p.TryGetPtr();
        Assert.True(ptr == null);
    }

    [Fact]
    public void Has()
    {
        using World world = World.Create();

        Entity e = world.Entity();

        {
            Ref<Position> p = e.GetRef<Position>();
            Assert.True(!p.Has());
        }

        e.Set(new Position(10, 20));

        {
            Ref<Position> p = e.GetRef<Position>();
            Assert.True(p.Has());
        }
    }

    [Fact]
    public void BoolOperator()
    {
        using World world = World.Create();

        Entity e = world.Entity();

        {
            Ref<Position> p = e.GetRef<Position>();
            Assert.True(!p);
        }

        e.Set(new Position(10, 20));

        {
            Ref<Position> p = e.GetRef<Position>();
            Assert.True(p);
        }
    }
}
