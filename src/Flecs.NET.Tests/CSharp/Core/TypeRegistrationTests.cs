using System;
using Flecs.NET.Core;
using Xunit;

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Tests.CSharp.Core;

public unsafe class TypeRegistrationTests
{
    [Fact]
    private void StructSize()
    {
            Assert.Equal(sizeof(Position), Type<Position>.Size);
        }

    [Fact]
    private void ClassSize()
    {
            Assert.Equal(sizeof(string), Type<string>.Size);
        }

    [Fact]
    private void EnumSize()
    {
            Assert.Equal(sizeof(Color), Type<Color>.Size);
        }

    [Fact]
    private void TagSize()
    {
            Assert.Equal(0, Type<Tag>.Size);
        }

    [Fact]
    private void TypeStruct()
    {
            using World world = World.Create();

            Entity position = world.Entity(Type<Position>.Id(world));
            Assert.Equal(".Position", position.Path());
        }

    [Fact]
    private void TypeStructScoped()
    {
            using World world = World.Create();

            world.SetScope(world.Entity("Parent"));

            Entity position = world.Entity(Type<Position>.Id(world));
            Assert.Equal(".Position", position.Path());
        }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    private void TypeEnum(bool scoped)
    {
            using World world = World.Create();

            if (scoped)
                world.SetScope(world.Entity("Parent"));

            Entity color = world.Entity(Type<Color>.Id(world));
            Assert.Equal(".Color", color.Path());
            Assert.True(color.Has<EcsEnum>());
            Assert.True(color.Has(EcsExclusive));
            Assert.True(color.Has(EcsOneOf));
            Assert.True(color.Has(EcsPairIsTag));

            Entity red = world.Entity(Color.Red);
            Entity green = world.Entity(Color.Green);
            Entity blue = world.Entity(Color.Blue);

            Assert.Equal(".Color.Red", red.Path());
            Assert.Equal(".Color.Green", green.Path());
            Assert.Equal(".Color.Blue", blue.Path());

            Assert.True(red.IsChildOf(color));
            Assert.True(green.IsChildOf(color));
            Assert.True(blue.IsChildOf(color));

            Assert.True(red.Has(EcsConstant, Ecs.Wildcard));
            Assert.True(green.Has(EcsConstant, Ecs.Wildcard));
            Assert.True(blue.Has(EcsConstant, Ecs.Wildcard));
        }

    [Fact]
    private void ComponentStructReflection()
    {
            using World world = World.Create();

            world.Component<Position>();
            Entity entity = world.Entity("Test").Set(new Position());
            Id posComponent = entity.Table().Type().Get(0);
            Assert.True(world.Entity(posComponent).Has<Type>());
            Assert.Equal(typeof(Position), world.Entity(posComponent).Get<Type>());
        }

    [Fact]
    private void ComponentClassReflection()
    {
            using World world = World.Create();

            world.Component<ManagedComponent>();
            Entity entity = world.Entity("Test").Set(new ManagedComponent(1));
            Id comp = entity.Table().Type().Get(0);
            Assert.True(world.Entity(comp).Has<Type>());
            Assert.Equal(typeof(ManagedComponent), world.Entity(comp).Get<Type>());
        }

    [Fact]
    private void ComponentStruct()
    {
            using World world = World.Create();

            Entity position = world.Component<Position>();
            Assert.Equal(".Position", position.Path());
        }

    [Fact]
    private void ComponentStructScoped()
    {
            using World world = World.Create();

            world.SetScope(world.Entity("Parent"));

            Entity position = world.Component<Position>();
            Assert.Equal(".Parent.Position", position.Path());
        }

    [Fact]
    private void ComponentEnum()
    {
            using World world = World.Create();

            Entity color = world.Component<Color>();
            Assert.Equal(".Color", color.Path());
            Assert.True(color.Has<EcsEnum>());
            Assert.True(color.Has(EcsExclusive));
            Assert.True(color.Has(EcsOneOf));
            Assert.True(color.Has(EcsPairIsTag));

            Entity red = world.Entity(Color.Red);
            Entity green = world.Entity(Color.Green);
            Entity blue = world.Entity(Color.Blue);

            Assert.Equal(".Color.Red", red.Path());
            Assert.Equal(".Color.Green", green.Path());
            Assert.Equal(".Color.Blue", blue.Path());

            Assert.True(red.IsChildOf(color));
            Assert.True(green.IsChildOf(color));
            Assert.True(blue.IsChildOf(color));

            Assert.True(red.Has(EcsConstant, Ecs.Wildcard));
            Assert.True(green.Has(EcsConstant, Ecs.Wildcard));
            Assert.True(blue.Has(EcsConstant, Ecs.Wildcard));
        }

    [Fact]
    private void ComponentEnumScoped()
    {
            using World world = World.Create();

            world.SetScope(world.Entity("Parent"));

            Entity color = world.Component<Color>();
            Assert.Equal(".Parent.Color", color.Path());
            Assert.True(color.Has<EcsEnum>());
            Assert.True(color.Has(EcsExclusive));
            Assert.True(color.Has(EcsOneOf));
            Assert.True(color.Has(EcsPairIsTag));

            Entity red = world.Entity(Color.Red);
            Entity green = world.Entity(Color.Green);
            Entity blue = world.Entity(Color.Blue);

            Assert.Equal(".Parent.Color.Red", red.Path());
            Assert.Equal(".Parent.Color.Green", green.Path());
            Assert.Equal(".Parent.Color.Blue", blue.Path());

            Assert.True(red.IsChildOf(color));
            Assert.True(green.IsChildOf(color));
            Assert.True(blue.IsChildOf(color));

            Assert.True(red.Has(EcsConstant, Ecs.Wildcard));
            Assert.True(green.Has(EcsConstant, Ecs.Wildcard));
            Assert.True(blue.Has(EcsConstant, Ecs.Wildcard));
        }

    [Fact]
    private void MultiWorld()
    {
            using World a = World.Create();
            using World b = World.Create();
            using World c = World.Create();

            int positionIndex = Type<Position>.CacheIndex;
            int velocityIndex = Type<Velocity>.CacheIndex;
            int massIndex     = Type<Mass>.CacheIndex;
            int tagIndex      = Type<Tag>.CacheIndex;

            Entity aPosition = a.Component<Position>();
            Entity aVelocity = a.Component<Velocity>();
            Entity aMass     = a.Component<Mass>();
            Entity aTag      = a.Component<Tag>();

            Entity bVelocity = b.Component<Velocity>();
            Entity bMass     = b.Component<Mass>();
            Entity bPosition = b.Component<Position>();
            Entity bTag      = a.Component<Tag>();

            Entity cMass     = c.Component<Mass>();
            Entity cPosition = c.Component<Position>();
            Entity cVelocity = c.Component<Velocity>();
            Entity cTag      = a.Component<Tag>();

            // Different registration orders should produce different ids.
            Assert.NotEqual(aPosition, bPosition);
            Assert.NotEqual(aPosition, cPosition);

            Assert.NotEqual(aVelocity, bVelocity);
            Assert.NotEqual(aVelocity, cVelocity);

            Assert.NotEqual(aMass, bMass);
            Assert.NotEqual(aMass, cMass);

            Assert.Equal(aTag, bTag);
            Assert.Equal(aTag, cTag);

            // Cache indexes will remain static for the duration of the program.
            Assert.Equal(positionIndex, Type<Position>.CacheIndex);
            Assert.Equal(velocityIndex, Type<Velocity>.CacheIndex);
            Assert.Equal(massIndex,     Type<Mass>.CacheIndex);
            Assert.Equal(tagIndex,      Type<Tag>.CacheIndex);
        }

    [Fact]
    private void TrimmedNames()
    {
            using World world = World.Create();

            world.SetScope<Child>();

            Entity child = world.Component<Child>();
            Entity grandChild = world.Component<Child.GrandChild>();
            Entity greatGrandChild = world.Component<Child.GrandChild.GreatGrandChild>();
            Entity position = world.Component<Position>();

            Assert.True(!child.Has(Ecs.ChildOf, Ecs.Wildcard));
            Assert.True(grandChild.IsChildOf(child));
            Assert.True(greatGrandChild.IsChildOf(grandChild));
            Assert.True(position.IsChildOf(child));

            Assert.Equal(".Child", child.Path());
            Assert.Equal(".Child.GrandChild", grandChild.Path());
            Assert.Equal(".Child.GrandChild.GreatGrandChild", greatGrandChild.Path());
            Assert.Equal(".Child.Position", position.Path());
        }

    [Fact]
    private void EnumInSystem()
    {
            using World world = World.Create();

            world.System()
                .Iter((Iter it) =>
                {
                    Entity color = it.World().Component<Color>();
                    Entity red = it.World().Entity(Color.Red);
                    Assert.True(red != 0);
                    Assert.True(red.IsChildOf(color));
                    Assert.True(red.Has(EcsConstant, Ecs.Wildcard));
                });

            world.Progress();
        }
}