using Flecs.NET.Core;
using Namespace;
using NamespaceLvl1.NamespaceLvl2;
using Xunit;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Tests.Cpp;

public class ModuleTests
{
    [Fact]
    private void Import()
    {
        using World world = World.Create();
        Entity m = world.Import<Namespace.BasicModule>();
        Assert.True(m != 0);
        Assert.Equal(".Namespace.BasicModule", m.Path());
        Assert.True(m.Has(Ecs.Module));

        Entity e = world.Entity()
            .Add<Position>();
        Assert.True(e != 0);
        Assert.True(e.Has<Position>());
    }

    [Fact]
    private void LookupFromScope()
    {
        using World world = World.Create();
        world.Import<Namespace.BasicModule>();

        Entity nsEntity = world.Lookup("Namespace");
        Assert.True(nsEntity != 0);

        Entity moduleEntity = world.Lookup("Namespace.BasicModule");
        Assert.True(moduleEntity != 0);

        Entity positionEntity = world.Lookup("Namespace.BasicModule.Position");
        Assert.True(positionEntity != 0);

        Entity nestedModule = nsEntity.Lookup("BasicModule");
        Assert.True(moduleEntity == nestedModule);

        Entity modulePosition = moduleEntity.Lookup("Position");
        Assert.True(positionEntity == modulePosition);

        Entity nsPosition = nsEntity.Lookup("BasicModule.Position");
        Assert.True(positionEntity == nsPosition);
    }

    [Fact]
    private void NestedModule()
    {
        using World world = World.Create();
        world.Import<Namespace.BasicModule>();

        Entity velocity = world.Lookup("Namespace.NestedModule.Velocity");
        Assert.True(velocity != 0);

        Assert.Equal(".Namespace.NestedModule.Velocity", velocity.Path());
    }

    [Fact]
    private void NestedTypeModule()
    {
        using World world = World.Create();
        world.Import<NestedTypeModule>();

        Entity nsEntity = world.Lookup("Namespace");
        Assert.True(nsEntity != 0);

        Entity moduleEntity = world.Lookup("Namespace.NestedTypeModule");
        Assert.True(moduleEntity != 0);

        Entity typeEntity = world.Lookup("Namespace.NestedTypeModule.NestedType");
        Assert.True(typeEntity != 0);

        Entity nsTypeEntity = world.Lookup("Namespace.NestedTypeModule.NestedNameSpaceType");
        Assert.True(nsTypeEntity != 0);

        int childOfCount = 0;
        typeEntity.Each(Ecs.ChildOf, (Entity _) => { childOfCount++; });

        Assert.Equal(1, childOfCount);

        childOfCount = 0;
        nsTypeEntity.Each(Ecs.ChildOf, (Entity _) => { childOfCount++; });

        Assert.Equal(1, childOfCount);
    }

    [Fact]
    private void ComponentRedefinitionOutsideModule()
    {
        using World world = World.Create();

        world.Import<Namespace.BasicModule>();

        Entity posComp = world.Lookup("Namespace.BasicModule.Position");
        Assert.True(posComp != 0);

        Component<Position> pos = world.Component<Position>();
        Assert.True(pos != 0);
        Assert.True((Entity)pos == posComp);

        int childOfCount = 0;
        posComp.Each(Ecs.ChildOf, (Entity _) => { childOfCount++; });

        Assert.Equal(1, childOfCount);
    }

    [Fact]
    private void ModuleTagOnNamespace()
    {
        using World world = World.Create();

        Entity mid = world.Import<NestedModule>();
        Assert.True(mid.Has(Ecs.Module));

        Entity namespaceId = world.Lookup("Namespace");
        Assert.True(namespaceId.Has(Ecs.Module));
    }

    [Fact]
    private void RegisterWithRootName()
    {
        using World world = World.Create();

        Entity m = world.Import<NamedModule>();

        Entity mLookup = world.Lookup(".my_scope.NamedModule");
        Assert.True(m != 0);
        Assert.True(m == mLookup);

        Assert.True(world.Lookup(".Namespace.NamedModule") == 0);
    }

    [Fact]
    private void ImplicitModule()
    {
        using World world = World.Create();

        Entity m = world.Import<ImplicitModule>();
        Entity mLookup = world.Lookup(".Namespace.ImplicitModule");
        Assert.True(m != 0);
        Assert.True(m == mLookup);

        Component<Position> p = world.Component<Position>();
        Entity pLookup = world.Lookup(".Namespace.ImplicitModule.Position");
        Assert.True(p != 0);
        Assert.True((Entity)p == pLookup);
    }

    [Fact]
    private void ModuleInNamespaceWithRootName()
    {
        using World world = World.Create();

        Entity m = world.Import<NamedModuleInRoot>();
        Entity mLookup = world.Lookup(".NamedModuleInRoot");
        Assert.True(m != 0);
        Assert.True(m == mLookup);
        Assert.Equal(".NamedModuleInRoot", m.Path());

        Component<Position> p = world.Component<Position>();
        Entity pLookup = world.Lookup(".NamedModuleInRoot.Position");
        Assert.True(p != 0);
        Assert.True((Entity)p == pLookup);
    }

    [Fact]
    private void ModuleAsEntity()
    {
        using World world = World.Create();

        Entity m = world.Import<Namespace.BasicModule>();
        Assert.True(m != 0);

        Entity e = world.Entity<Namespace.BasicModule>();
        Assert.True(m == e);
    }

    [Fact]
    private void ModuleAsComponent()
    {
        using World world = World.Create();

        Entity m = world.Import<Namespace.BasicModule>();
        Assert.True(m != 0);

        Component<Namespace.BasicModule> e = world.Component<Namespace.BasicModule>();
        Assert.True(m == (Entity)e);
    }

    [Fact]
    private void ModuleWithCoreName()
    {
        using World world = World.Create();

        Entity m = world.Import<Module>();
        Assert.True(m != 0);
        Assert.Equal(".Module", m.Path());

        Entity pos = m.Lookup("Position");
        Assert.True(pos != 0);
        Assert.Equal(".Module.Position", pos.Path());
        Assert.True(pos == world.Id<Position>());
    }

    [Fact]
    private void ImportAddonsTwoWorlds()
    {
        using World a = World.Create();

        Entity m1 = a.Import<Ecs.Stats>();
        Entity u1 = a.Import<Ecs.Units>();

        using World b = World.Create();
        Entity m2 = b.Import<Ecs.Stats>();
        Entity u2 = b.Import<Ecs.Units>();

        Assert.True(m1 == m2);
        Assert.True(u1 == u2);
    }

    [Fact]
    private void LookupModuleAfterReparent()
    {
        using World world = World.Create();

        Entity m = world.Import<NestedModule>();
        Assert.Equal(".Namespace.NestedModule", m.Path());
        Assert.True(world.Lookup(".Namespace.NestedModule") == m);

        Entity p = world.Entity("p");
        m.ChildOf(p);
        Assert.Equal(".p.NestedModule", m.Path());
        Assert.True(world.Lookup(".p.NestedModule") == m);

        Assert.True(world.Lookup(".Namespace.NestedModule") == 0);

        Entity e = world.Entity(".Namespace.NestedModule");
        Assert.True(e != m);

        Assert.Equal(1, world.QueryBuilder().Expr("(ChildOf, p.NestedModule)").Build().Count());
        Assert.Equal(0, world.QueryBuilder().Expr("(ChildOf, Namespace.NestedModule)").Build().Count());
    }

    [Fact]
    private void ReparentModuleInCtor()
    {
        using World world = World.Create();

        Entity m = world.Import<ReparentModule>();
        Assert.Equal(".parent.ReparentModule", m.Path());

        Entity other = world.Lookup(".Namespace.ReparentModule");
        Assert.True(other != 0);
        Assert.True(other != m);
    }

    [Fact]
    private void ImplicitlyAddModuleToScopesComponent()
    {
        using World world = World.Create();

        Entity current = world.Component<StructLvl1.StructLvl21>();
        Assert.True(current != 0);
        Assert.True(!current.Has(Ecs.Module));
        Assert.True(current.Has<EcsComponent>());
        Assert.True(current.Path() == ".NamespaceLvl1.NamespaceLvl2.StructLvl1.StructLvl21");

        current = current.Parent();
        Assert.True(current != 0);
        Assert.True(current.Has(Ecs.Module));
        Assert.True(current.Path() == ".NamespaceLvl1.NamespaceLvl2.StructLvl1");

        current = current.Parent();
        Assert.True(current != 0);
        Assert.True(current.Has(Ecs.Module));
        Assert.True(current.Path() == ".NamespaceLvl1.NamespaceLvl2");

        current = current.Parent();
        Assert.True(current != 0);
        Assert.True(current.Has(Ecs.Module));
        Assert.True(current.Path() == ".NamespaceLvl1");

        current = current.Parent();
        Assert.True(current == 0);
    }

    [Fact]
    private void ImplicitlyAddModuleToScopesEntity()
    {
        using World world = World.Create();

        Entity current = world.Entity<StructLvl1.StructLvl22>().Set(default(EcsComponent));
        Assert.True(current != 0);
        Assert.True(!current.Has(Ecs.Module));
        Assert.True(current.Has<EcsComponent>());
        Assert.True(current.Path() == ".NamespaceLvl1.NamespaceLvl2.StructLvl1.StructLvl22");

        current = current.Parent();
        Assert.True(current != 0);
        Assert.True(current.Has(Ecs.Module));
        Assert.True(current.Path() == ".NamespaceLvl1.NamespaceLvl2.StructLvl1");

        current = current.Parent();
        Assert.True(current != 0);
        Assert.True(current.Has(Ecs.Module));
        Assert.True(current.Path() == ".NamespaceLvl1.NamespaceLvl2");

        current = current.Parent();
        Assert.True(current != 0);
        Assert.True(current.Has(Ecs.Module));
        Assert.True(current.Path() == ".NamespaceLvl1");

        current = current.Parent();
        Assert.True(current == 0);
    }

    [Fact]
    private void RenameNamespaceShorter()
    {
        using World world = World.Create();

        Entity m = world.Import<NamespaceParent.ShorterParent>();
        Assert.True(m.Has(Ecs.Module));
        Assert.Equal(".Namespace.ShorterParent", m.Path());
        Assert.True(world.Lookup(".NamespaceParent") == 0);
        Assert.True(world.Lookup(".NamespaceParent.ShorterParent") == 0);
        Assert.True(world.Lookup(".NamespaceParent.ShorterParent.NamespaceType") == 0);
        Assert.True(world.Lookup(".Namespace.ShorterParent.NamespaceType") != 0);

        Entity ns = world.Lookup(".Namespace");
        Assert.True(ns != 0);
        Assert.True(ns.Has(Ecs.Module));
    }

    [Fact]
    private void RenameNamespaceLonger()
    {
        using World world = World.Create();

        Entity m = world.Import<NamespaceParent.LongerParent>();
        Assert.True(m.Has(Ecs.Module));
        Assert.Equal(".NamespaceParentNamespace.LongerParent", m.Path());
        Assert.True(world.Lookup(".NamespaceParent") == 0);
        Assert.True(world.Lookup(".NamespaceParent.LongerParent") == 0);
        Assert.True(world.Lookup(".NamespaceParent.LongerParent.NamespaceType") == 0);
        Assert.True(world.Lookup(".NamespaceParentNamespace.LongerParent.NamespaceType") != 0);

        Entity ns = world.Lookup(".NamespaceParentNamespace");
        Assert.True(ns != 0);
        Assert.True(ns.Has(Ecs.Module));
    }

    [Fact]
    private void RenameNamespaceNested()
    {
        using World world = World.Create();

        Entity m = world.Import<NamespaceParent.NamespaceChild.Nested>();
        Assert.True(m.Has(Ecs.Module));
        Assert.Equal(".Namespace.Child.Nested", m.Path());
        Assert.True(world.Lookup(".Namespace.Child.Nested.NamespaceType") != 0);
        Assert.True(world.Lookup(".NamespaceParent.NamespaceChild.Nested.NamespaceType") == 0);
        Assert.True(world.Lookup(".NamespaceParent.NamespaceChild.Nested") == 0);
        Assert.True(world.Lookup(".NamespaceParent.NamespaceChild") == 0);
        Assert.True(world.Lookup(".NamespaceParent") == 0);

        Entity ns = world.Lookup(".Namespace");
        Assert.True(ns != 0);
        Assert.True(ns.Has(Ecs.Module));

        Entity nsChild = world.Lookup(".Namespace.Child");
        Assert.True(nsChild != 0);
        Assert.True(nsChild.Has(Ecs.Module));
    }

    [Fact]
    private void RenameReparentRootModule()
    {
        using World world = World.Create();

        Entity m = world.Import<ReparentRootModule>();
        Entity p = m.Parent();

        Assert.True(p != 0);
        Assert.Equal("Namespace", p.Name());
        Assert.Equal("ReparentRootModule", m.Name());
    }

    [Fact]
    private void ModuleNoRecycleAfterRenameReparent()
    {
        using World world = World.Create();

        Entity m = world.Import<RenamedRootModule.Module>();
        Entity p = m.Parent();
        Assert.True(p == 0);
        Assert.Equal("MyModule", m.Name());
    }

    [Fact]
    private void ReimportAfterDelete()
    {
        using World world = World.Create();

        {
            Entity m = world.Import<Module>();
            Assert.True(m.Lookup("Position") == world.Component<Position>().Entity);
            Assert.True(m == world.Entity<Module>());
        }

        world.Entity<Module>().Destruct();

        {
            Entity m = world.Import<Module>();
            Assert.True(m.Lookup("Position") == world.Component<Position>().Entity);
            Assert.True(m == world.Entity<Module>());
        }
    }
}
