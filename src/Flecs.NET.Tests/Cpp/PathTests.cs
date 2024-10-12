using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.Cpp;

public unsafe class PathTests
{
    [Fact]
    private void Name()
    {
        using World world = World.Create();

        Entity e = new Entity(world, "foo");
        Assert.Equal("foo", e.Name());

        Entity eWorld = world.Lookup("foo");
        Assert.True(e == eWorld);

        eWorld = world.Lookup(".foo");
        Assert.True(e == eWorld);
    }

    [Fact]
    private void PathDepth1()
    {
        using World world = World.Create();

        Entity e = new Entity(world, "foo.bar");
        Assert.Equal("bar", e.Name());
        Assert.Equal(".foo.bar", e.Path());

        Entity eWorld = world.Lookup("bar");
        Assert.True(0 == eWorld);

        eWorld = world.Lookup("foo.bar");
        Assert.True(e == eWorld);

        eWorld = world.Lookup(".foo.bar");
        Assert.True(e == eWorld);
    }

    [Fact]
    private void PathDepth2()
    {
        using World world = World.Create();

        Entity e = new Entity(world, "foo.bar.hello");
        Assert.Equal("hello", e.Name());
        Assert.Equal(".foo.bar.hello", e.Path());

        Entity eWorld = world.Lookup("hello");
        Assert.True(0 == eWorld);

        eWorld = world.Lookup("foo.bar.hello");
        Assert.True(e == eWorld);

        eWorld = world.Lookup(".foo.bar.hello");
        Assert.True(e == eWorld);
    }

    [Fact]
    private void EntityLookupName()
    {
        using World world = World.Create();

        Entity parent = new Entity(world, "foo");
        Assert.Equal("foo", parent.Name());
        Assert.Equal(".foo", parent.Path());

        Entity e = new Entity(world, "foo.bar");
        Assert.Equal("bar", e.Name());
        Assert.Equal(".foo.bar", e.Path());

        Entity parentE = parent.Lookup("bar");
        Assert.True(e == parentE);

        parentE = parent.Lookup(".foo.bar");
        Assert.True(e == parentE);
    }

    [Fact]
    private void EntityLookupDepth1()
    {
        using World world = World.Create();

        Entity parent = new Entity(world, "foo");
        Assert.Equal("foo", parent.Name());
        Assert.Equal(".foo", parent.Path());

        Entity e = new Entity(world, "foo.bar.hello");
        Assert.Equal("hello", e.Name());
        Assert.Equal(".foo.bar.hello", e.Path());

        Entity parentE = parent.Lookup("bar.hello");
        Assert.True(e == parentE);

        parentE = parent.Lookup(".foo.bar.hello");
        Assert.True(e == parentE);
    }

    [Fact]
    private void EntityLookupDepth2()
    {
        using World world = World.Create();

        Entity parent = new Entity(world, "foo");
        Assert.Equal("foo", parent.Name());
        Assert.Equal(".foo", parent.Path());

        Entity e = new Entity(world, "foo.bar.hello.world");
        Assert.Equal("world", e.Name());
        Assert.Equal(".foo.bar.hello.world", e.Path());

        Entity parentE = parent.Lookup("bar.hello.world");
        Assert.True(e == parentE);

        parentE = parent.Lookup(".foo.bar.hello.world");
        Assert.True(e == parentE);
    }

    [Fact]
    private void AliasComponent()
    {
        using World world = World.Create();

        Entity e = world.Use<Position>("MyPosition");
        Entity a = world.Lookup("MyPosition");
        Entity c = world.Lookup("Position");

        Assert.True(e == a);
        Assert.True(e == c);
    }

    [Fact]
    private void AliasScopedComponent()
    {
        using World world = World.Create();

        Entity e = world.Use<Test.Foo>();
        Entity a = world.Lookup("Foo");
        Entity c = world.Lookup("Test.Foo");

        Assert.True(e == a);
        Assert.True(e == c);
    }

    [Fact]
    private void AliasScopedComponentWithName()
    {
        using World world = World.Create();

        Entity e = world.Use<Test.Foo>("FooAlias");
        Entity a = world.Lookup("FooAlias");
        Entity f = world.Lookup("Foo");
        Entity c = world.Lookup("Test.Foo");

        Assert.True(e == a);
        Assert.True(e == c);
        Assert.True(f == 0);
    }

    [Fact]
    private void AliasEntity()
    {
        using World world = World.Create();

        Entity e = world.Entity("Foo");

        world.Use(e, "FooAlias");

        Entity a = world.Lookup("FooAlias");

        Assert.True(e == a);
    }

    [Fact]
    private void AliasEntityByName()
    {
        using World world = World.Create();

        Entity e = world.Entity("Foo");

        world.Use(e, "FooAlias");

        Entity l = world.Lookup("FooAlias");

        Assert.True(e == l);
    }

    [Fact]
    private void AliasEntityByScopedName()
    {
        using World world = World.Create();

        Entity e = world.Entity("Foo.Bar");

        Entity a = world.Use("Foo.Bar", "FooAlias");

        Entity l = world.Lookup("FooAlias");

        Assert.True(e == a);
        Assert.True(e == l);
    }

    [Fact]
    private void AliasEntityEmpty()
    {
        using World world = World.Create();

        Entity parent = world.Entity("parent");
        Entity child = world.Entity("child").ChildOf(parent);

        Entity e = world.Lookup("child");

        Assert.True(e == 0);

        world.Use(child);

        e = world.Lookup("child");

        Assert.True(e != 0);

        world.Use(child, "FooAlias");

        e = world.Lookup("child");

        Assert.True(e == 0);

        e = world.Lookup("FooAlias");

        Assert.True(e != 0);
    }

    [Fact]
    private void IdFromStr0Entity()
    {
        using World world = World.Create();

        Id id = world.Id("#0");
        Assert.True(id == 0);
    }

    [Fact]
    private void IdFromStrEntityFromStr()
    {
        using World world = World.Create();

        Entity foo = world.Entity("foo");

        Id id = world.Id("foo");
        Assert.True(id != 0);
        Assert.True(id == foo);
    }

    [Fact]
    private void IdFromStrUnresolvedEntityFromStr()
    {
        using World world = World.Create();

        Id id = world.Id("foo");
        Assert.True(id == 0);
    }

    [Fact]
    private void IdFromStrScopedEntityFromStr()
    {
        using World world = World.Create();

        Entity foo = world.Entity("foo.bar");

        Id id = world.Id("foo.bar");
        Assert.True(id != 0);
        Assert.True(id == foo);
    }

    [Fact]
    private void IdFromStrTemplateEntityFromStr()
    {
        using World world = World.Create();

        Entity foo = world.Entity("foo<bar>");

        Id id = world.Id("foo<bar>");
        Assert.True(id != 0);
        Assert.True(id == foo);
    }

    [Fact]
    private void IdFromStrPairFromStr()
    {
        using World world = World.Create();

        Entity rel = world.Entity("Rel");
        Entity tgt = world.Entity("Tgt");

        Id id = world.Id("(Rel, Tgt)");
        Assert.True(id != 0);
        Assert.True(id == world.Pair(rel, tgt));
    }

    [Fact]
    private void IdFromStrUnresolvedPairFromStr()
    {
        using World world = World.Create();

        world.Entity("Rel");

        Id id = world.Id("(Rel, Tgt)");
        Assert.True(id == 0);
    }

    [Fact]
    private void IdFromStrWildcardPairFromStr()
    {
        using World world = World.Create();

        Entity rel = world.Entity("Rel");

        Id id = world.Id("(Rel, *)");
        Assert.True(id != 0);
        Assert.True(id == world.Pair(rel, Ecs.Wildcard));
    }

    [Fact]
    private void IdFromStrAnyPairFromStr()
    {
        using World world = World.Create();

        Entity rel = world.Entity("Rel");

        Id id = world.Id("(Rel, _)");
        Assert.True(id != 0);
        Assert.True(id == world.Pair(rel, Ecs.Any));
    }

    [Fact]
    private void IdFromStrInvalidPair()
    {
        using World world = World.Create();

        world.Entity("Rel");
        world.Entity("Tgt");

        Id id = world.Id("(Rel, Tgt");
        Assert.True(id == 0);
    }
}
