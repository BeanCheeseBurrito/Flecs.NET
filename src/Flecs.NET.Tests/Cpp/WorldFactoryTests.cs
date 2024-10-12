using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp;

public unsafe class WorldFactoryTests
{
    [Fact]
    private void Entity()
    {
        using World world = World.Create();

        Entity e = world.Entity();
        Assert.True(e.Id != 0);
    }

    [Fact]
    private void EntityWithName()
    {
        using World world = World.Create();

        Entity e = world.Entity("MyName");
        Assert.True(e.Id != 0);
        Assert.Equal("MyName", e.Name());
    }

    [Fact]
    private void EntityWithId()
    {
        using World world = World.Create();

        Entity e = world.Entity(100);
        Assert.True(e.Id == 100);
    }

    [Fact]
    private void Prefab()
    {
        using World world = World.Create();

        Entity e = world.Prefab();
        Assert.True(e.Id != 0);
        Assert.True(e.Has(Ecs.Prefab));
    }

    [Fact]
    private void PrefabWithName()
    {
        using World world = World.Create();

        Entity e = world.Prefab("MyName");
        Assert.True(e.Id != 0);
        Assert.True(e.Has(Ecs.Prefab));
        Assert.Equal("MyName", e.Name());
    }

    [Fact]
    private void System()
    {
        using World world = World.Create();

        System<Position, Velocity> system = world.System<Position, Velocity>()
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
            });

        Assert.True(system.Id != 0);

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 })
            .Set(new Velocity { X = 1, Y = 2 });

        world.Progress();

        Position* p = e.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void SystemWithName()
    {
        using World world = World.Create();

        System<Position, Velocity> system = world.System<Position, Velocity>("MySystem")
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                p.X += v.X;
                p.Y += v.Y;
            });

        Assert.True(system.Id != 0);
        Assert.Equal("MySystem", system.Entity.Name());

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 })
            .Set(new Velocity { X = 1, Y = 2 });

        world.Progress();

        Position* p = e.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void SystemWithExpr()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        System_ system = world.System("MySystem")
            .Expr("Position, [in] Velocity")
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Position> p = it.Field<Position>(0);
                    Field<Velocity> v = it.Field<Velocity>(1);

                    foreach (int i in it)
                    {
                        p[i].X += v[i].X;
                        p[i].Y += v[i].Y;
                    }
                }
            });

        Assert.True(system.Id != 0);
        Assert.Equal("MySystem", system.Entity.Name());

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 })
            .Set(new Velocity { X = 1, Y = 2 });

        world.Progress();

        Position* p = e.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void Query()
    {
        using World world = World.Create();

        using Query<Position, Velocity> q = world.Query<Position, Velocity>();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 })
            .Set(new Velocity { X = 1, Y = 2 });

        q.Each((ref Position p, ref Velocity v) =>
        {
            p.X += v.X;
            p.Y += v.Y;
        });

        Position* p = e.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void QueryWithExpr()
    {
        using World world = World.Create();

        world.Component<Position>();
        world.Component<Velocity>();

        using Query q = world.QueryBuilder().Expr("Position, [in] Velocity").Build();

        Entity e = world.Entity()
            .Set(new Position { X = 10, Y = 20 })
            .Set(new Velocity { X = 1, Y = 2 });

        q.Run((Iter it) =>
        {
            while (it.Next())
            {
                Field<Position> p = it.Field<Position>(0);
                Field<Velocity> v = it.Field<Velocity>(1);

                foreach (int i in it)
                {
                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            }
        });

        Position* p = e.GetPtr<Position>();
        Assert.Equal(11, p->X);
        Assert.Equal(22, p->Y);
    }

    [Fact]
    private void Module()
    {
        using World world = World.Create();

        world.Import<MyModule>();

        Entity p = world.Lookup("MyModule.Position");
        Assert.True(p.Id != 0);
    }
}
