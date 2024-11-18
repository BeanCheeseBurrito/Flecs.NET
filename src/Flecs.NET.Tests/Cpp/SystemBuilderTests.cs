using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp;

public class SystemBuilderTests
{
    [Fact]
    private void BuilderAssignSameType()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Position>();

        int count = 0;

        System<Position, Velocity> s = world.System<Position, Velocity>()
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void BuilderBuildToAuto()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Position>();

        int count = 0;

        System<Position, Velocity> s = world.System<Position, Velocity>()
            .Each((Entity e, ref Position p, ref Velocity v) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void BuilderBuildNStatements()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Position>();

        int count = 0;

        SystemBuilder qb = world.System();
        qb.With<Position>();
        qb.With<Velocity>();
        System_ s = qb.Each((Entity e) =>
        {
            count++;
            Assert.True(e == e1);
        });

        s.Run();

        Assert.Equal(1, count);
    }

    [Fact]
    private void _1Type()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        world.Entity().Add<Velocity>();

        int count = 0;

        System<Position> s = world.System<Position>()
            .Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void Add1Type()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        world.Entity().Add<Velocity>();

        int count = 0;

        System_ s = world.System()
            .With<Position>()
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void Add2Types()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Velocity>();

        int count = 0;

        System_ s = world.System()
            .With<Position>()
            .With<Velocity>()
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void Add1TypeWith1Type()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Velocity>();

        int count = 0;

        System<Position> s = world.System<Position>()
            .With<Velocity>()
            .Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void Add2TypesWith1Type()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>().Add<Velocity>().Add<Mass>();
        world.Entity().Add<Velocity>();

        int count = 0;

        System<Position> s = world.System<Position>()
            .With<Velocity>()
            .With<Mass>()
            .Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void AddPair()
    {
        using World world = World.Create();

        Entity likes = world.Entity();
        Entity bob = world.Entity();
        Entity alice = world.Entity();

        Entity e1 = world.Entity().Add(likes, bob);
        world.Entity().Add(likes, alice);

        int count = 0;

        System_ s = world.System()
            .With(likes, bob)
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void AddNot()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        world.Entity().Add<Position>().Add<Velocity>();

        int count = 0;

        System<Position> s = world.System<Position>()
            .With<Velocity>().Not()
            .Each((Entity e, ref Position p) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void AddOr()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        Entity e2 = world.Entity().Add<Velocity>();
        world.Entity().Add<Mass>();

        int count = 0;

        System_ s = world.System()
            .With<Position>().Or()
            .With<Velocity>()
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(2, count);
    }

    [Fact]
    private void AddOptional()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        Entity e2 = world.Entity().Add<Position>().Add<Velocity>();
        world.Entity().Add<Velocity>().Add<Mass>();

        int count = 0;

        System_ s = world.System()
            .With<Position>()
            .With<Velocity>().Optional()
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1 || e == e2);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(2, count);
    }

    [Fact]
    private void StringTerm()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();
        world.Entity().Add<Velocity>();

        int count = 0;

        System_ s = world.System()
            .Expr("Position")
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

        s.Run();

        Assert.Equal(1, count);
    }

    [Fact]
    private void SingletonTerm()
    {
        using World world = World.Create();

        world.Set(new Singleton(10));

        int count = 0;

        System<Entity> s = world.System<Entity>()
            .With<Singleton>().Singleton()
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Field<Entity> e = it.Field<Entity>(0);
                    Field<Singleton> s = it.Field<Singleton>(1);
                    Assert.True(!it.IsSelf(1));
                    Assert.Equal(10, s[0].Value);

                    ref Singleton sRef = ref s[0];
                    Assert.Equal(10, sRef.Value);

                    foreach (int i in it)
                    {
                        Assert.True(it.Entity(i) == e[i]);
                        count++;
                    }
                }
            });

        Entity e = world.Entity();
        e.Set(new Entity(e));
        e = world.Entity();
        e.Set(new Entity(e));
        e = world.Entity();
        e.Set(new Entity(e));

        s.Run();

        Assert.Equal(3, count);
    }

    [Fact]
    private void _10Terms()
    {
        using World world = World.Create();

        int count = 0;

        Entity e = world.Entity()
            .Add<Tag0>()
            .Add<Tag1>()
            .Add<Tag2>()
            .Add<Tag3>()
            .Add<Tag4>()
            .Add<Tag5>()
            .Add<Tag6>()
            .Add<Tag7>()
            .Add<Tag8>()
            .Add<Tag9>();

        System_ s = world.System()
            .With<Tag0>()
            .With<Tag1>()
            .With<Tag2>()
            .With<Tag3>()
            .With<Tag4>()
            .With<Tag5>()
            .With<Tag6>()
            .With<Tag7>()
            .With<Tag8>()
            .With<Tag9>()
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.True(it.Entity(0) == e);
                    Assert.Equal(10, it.FieldCount());
                    count++;
                }
            });

        s.Run();

        Assert.Equal(1, count);
    }

    [Fact]
    private void _16Terms()
    {
        using World world = World.Create();

        int count = 0;

        Entity e = world.Entity()
            .Add<Tag0>()
            .Add<Tag1>()
            .Add<Tag2>()
            .Add<Tag3>()
            .Add<Tag4>()
            .Add<Tag5>()
            .Add<Tag6>()
            .Add<Tag7>()
            .Add<Tag8>()
            .Add<Tag9>()
            .Add<Tag10>()
            .Add<Tag11>()
            .Add<Tag12>()
            .Add<Tag13>()
            .Add<Tag14>()
            .Add<Tag15>();

        System_ s = world.System()
            .With<Tag0>()
            .With<Tag1>()
            .With<Tag2>()
            .With<Tag3>()
            .With<Tag4>()
            .With<Tag5>()
            .With<Tag6>()
            .With<Tag7>()
            .With<Tag8>()
            .With<Tag9>()
            .With<Tag10>()
            .With<Tag11>()
            .With<Tag12>()
            .With<Tag13>()
            .With<Tag14>()
            .With<Tag15>()
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                    Assert.Equal(1, it.Count());
                    Assert.True(it.Entity(0) == e);
                    Assert.Equal(16, it.FieldCount());
                    count++;
                }
            });

        s.Run();

        Assert.Equal(1, count);
    }

    [Fact]
    private void NameArg()
    {
        using World world = World.Create();

        System<Position> s = world.System<Position>("MySystem")
            .TermAt(0).Src().Name("MySystem")
            .Run((Iter it) =>
            {
                while (it.Next())
                {
                }
            });

        Assert.True(s.Has<Position>());
    }

    [Fact]
    private void CreateWithNoTemplateArgs()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Position>();

        int count = 0;

        System_ s = world.System()
            .With<Position>()
            .Each((Entity e) =>
            {
                count++;
                Assert.True(e == e1);
            });

        Assert.Equal(0, count);
        s.Run();
        Assert.Equal(1, count);
    }

    [Fact]
    private void WriteAnnotation()
    {
        using World world = World.Create();

        Entity e1 = world.Entity().Add<Tag0>();

        int aCount = 0, bCount = 0;

        world.System()
            .With<Tag0>()
            .With<Tag1>().Write()
            .Each((Entity e) =>
            {
                aCount++;
                Assert.True(e == e1);
                e.Add<Tag1>();
            });

        world.System()
            .With<Tag1>()
            .Each((Entity e) =>
            {
                bCount++;
                Assert.True(e == e1);
                Assert.True(e.Has<Tag1>());
            });

        Assert.Equal(0, aCount);
        Assert.Equal(0, bCount);

        world.Progress();

        Assert.Equal(1, aCount);
        Assert.Equal(1, bCount);

        Assert.True(e1.Has<Tag1>());
    }

    [Fact]
    private void NameFromRoot()
    {
        using World world = World.Create();

        Entity sys = world.System(".ns.MySystem")
            .Each((Entity e) => { });

        Assert.Equal("MySystem", sys.Name());

        Entity ns = world.Entity(".ns");
        Assert.True(ns == sys.Parent());
    }
}
