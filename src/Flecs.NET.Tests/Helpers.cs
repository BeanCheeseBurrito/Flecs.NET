using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

[assembly: SuppressMessage("Usage", "CA1050")]

namespace Test
{
    public record struct Foo(float X, float Y);
}

public record struct Position(float X, float Y);

public record struct Velocity(float X, float Y);

public record struct Mass(float Value);

public record struct Pair(float Value);

public record struct Template<T>(T X, T Y);

public record struct MyEvent(float Value);

public record struct EvtData(int Value);

public record struct RelData(int Foo);

public record struct Self(Entity Value);

public record struct EntityWrapper(Entity Value);

public record struct QueryWrapper(Query<Position, Velocity> Query);

public record struct Value(int Number);

public record struct Other(int Value);

public record struct Singleton(int Value);

public record struct PositionInitialized(float X, float Y);

public record struct ThisComp(int X);

public record struct OtherComp(int X);

public record ManagedComponent(int Value);

public record struct UnmanagedComponent(int Value);

public record struct SharedComponent(int Value);

public record struct SparseComponent(int Value);

public record struct UnmanagedStruct(int Value);

public record struct ManagedStruct(int Value)
{
    public object Dummy = null!;
}

public class ManagedClass(int value)
{
    public int Value = value;
    public object Dummy = null!;
}

public struct Base;

public struct Prefab;

public struct First;

public struct Second;

public struct PipelineType;

public struct Foo;

public struct Bar;

public struct Hello;

public struct Evt;

public struct EntityType;

public struct A;

public struct B;

public struct IdA;

public struct IdB;

public struct MyTag;

public struct Tgt;

public struct Rel;

public struct Obj;

public struct Likes;

public struct Bob;

public struct Alice;

public struct R;

public struct O1;

public struct O2;

public struct T1;

public struct T2;

public struct T3;

public struct Tag;

public struct Movement;

public struct Standing;

public struct Walking;

public struct Eats;

public struct Apples;

public struct Pears;

public struct TgtA;

public struct TgtB;

public struct TgtC;

public struct Tag0;

public struct Tag1;

public struct Tag2;

public struct Tag3;

public struct Tag4;

public struct Tag5;

public struct Tag6;

public struct Tag7;

public struct Tag8;

public struct Tag9;

public struct Tag10;

public struct Tag11;

public struct Tag12;

public struct Tag13;

public struct Tag14;

public struct Tag15;

public struct Tag16;

public struct Tag17;

public struct Tag18;

public struct Tag19;

public struct Tag20;

public struct Tag21;

public struct Tag22;

public struct Tag23;

public struct Tag24;

public struct Tag25;

public struct Tag26;

public struct Tag27;

public struct Tag28;

public struct Tag29;

public struct Tag30;

public struct Tag31;

public struct Child
{
    public struct GrandChild
    {
        public struct GreatGrandChild;
    }
}

public struct Turret
{
    public struct Base;
}

public struct Railgun
{
    public struct Base;

    public struct Head;

    public struct Beam;
}

public struct Parent
{
    public struct EntityType;
}

public enum StandardEnum
{
    Red,
    Green,
    Blue
}

public enum Color
{
    Red,
    Green,
    Blue
}

public enum Number
{
    One,
    Two,
    Three
}

public enum PipelineStepEnum
{
    CustomStep,
    CustomStep2
}

public enum Letters
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z
}

public struct Module : IFlecsModule
{
    public void InitModule(World world)
    {
        world.Module<Module>();
        world.Component<Position>();
    }
}

public struct MyModule : IFlecsModule
{
    public void InitModule(World world)
    {
        world.Module<MyModule>();
        world.Component<Position>();
    }
}

public class ReparentRootModule : IFlecsModule
{
    public void InitModule(World world)
    {
        world.Module<ReparentRootModule>("Namespace.ReparentRootModule");
    }
}

namespace Namespace
{
    public struct NestedNameSpaceType;

    public record struct Velocity(float X, float Y);

    public struct NestedModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<NestedModule>();
            world.Component<Velocity>("Velocity");
        }
    }

    public struct BasicModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<BasicModule>();
            world.Import<NestedModule>();
            world.Component<Position>("Position");
        }
    }

    public struct NestedTypeModule : IFlecsModule
    {
        public struct NestedType;

        public void InitModule(World world)
        {
            world.Module<NestedTypeModule>();
            world.Component<NestedType>();
            world.Component<NestedNameSpaceType>();
        }
    }

    public struct NamedModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<NamedModule>(".my_scope.NamedModule");
            world.Component<Position>("Position");
        }
    }

    public struct ImplicitModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Component<Position>();
        }
    }

    public struct NamedModuleInRoot : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<NamedModuleInRoot>(".NamedModuleInRoot");
            world.Component<Position>();
        }
    }

    public struct ReparentModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            Entity m = world.Module<ReparentModule>();
            m.ChildOf(world.Entity(".parent"));

            Entity other = world.Entity(".Namespace.ReparentModule");
            Assert.True(other != 0);
            Assert.True(other != m);
        }
    }
}

namespace NamespaceLvl1
{
    namespace NamespaceLvl2
    {
        public struct StructLvl1
        {
            public struct StructLvl21;

            public struct StructLvl22;
        }
    }
}

namespace NamespaceParent
{
    public record struct NamespaceType(float X);

    public struct ShorterParent : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<ShorterParent>("Namespace.ShorterParent");
            world.Component<NamespaceType>();
        }
    }

    public struct LongerParent : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<LongerParent>("NamespaceParentNamespace.LongerParent");
            world.Component<NamespaceType>();
        }
    }

    namespace NamespaceChild
    {
        public struct Nested : IFlecsModule
        {
            public void InitModule(World world)
            {
                world.Module<Nested>("Namespace.Child.Nested");
                world.Component<NamespaceType>();
            }
        }
    }
}

namespace RenamedRootModule
{
    public struct Module : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<Module>(".MyModule");
            for (int i = 0; i < 5; ++i)
            {
                Entity e = world.Entity();
                Assert.True(e.Id == (uint)e.Id);
            }
        }
    };
}
