using System;
using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

[assembly: SuppressMessage("Usage", "CA1050")]

namespace Test
{
    public struct Foo
    {
        public float X;
        public float Y;
    }
}

public struct Position
{
    public float X { get; set; }
    public float Y { get; set; }

    public Position(float x, float y)
    {
        X = x;
        Y = y;
    }
}

public struct Velocity
{
    public float X { get; set; }
    public float Y { get; set; }

    public Velocity(float x, float y)
    {
        X = x;
        Y = y;
    }
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

public struct Mass
{
    public float Value { get; set; }

    public Mass(float value)
    {
        Value = value;
    }
}

public struct Pair
{
    public float Value { get; set; }
}

public struct Template<T>
{
    public T X { get; set; }
    public T Y { get; set; }

    public Template(T x, T y)
    {
        X = x;
        Y = y;
    }
}

public struct Self
{
    public Entity Value { get; set; }

    public Self(Entity value)
    {
        Value = value;
    }
}

public struct MyEvent
{
    public float Value { get; set; }

    public MyEvent(float value)
    {
        Value = value;
    }
}

public struct EntityWrapper
{
    public Entity Value { get; set; }

    public EntityWrapper(Entity value)
    {
        Value = value;
    }
}

public struct Parent
{
    public struct EntityType
    {
    }
}

public struct Value
{
    public int Number { get; set; }

    public Value(int number)
    {
        Number = number;
    }
}


public struct Other
{
    public int Value { get; set; }

    public Other(int value)
    {
        Value = value;
    }
}

public struct Singleton
{
    public int Value { get; set; }

    public Singleton(int value)
    {
        Value = value;
    }
}

public struct QueryWrapper
{
    public Query Query;

    public QueryWrapper(Query query)
    {
        Query = query;
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

namespace Namespace
{
    public struct NestedNameSpaceType { }

    public struct NestedModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<NestedModule>();
            world.Component<Velocity>("Velocity");
        }
    }

    public struct SimpleModule : IFlecsModule
    {
        public void InitModule(World world)
        {
            world.Module<SimpleModule>();
            world.Import<NestedModule>();
            world.Component<Position>("Position");
        }
    }

    public struct NestedTypeModule : IFlecsModule
    {
        public struct NestedType { }

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

public struct Module : IFlecsModule
{
    public void InitModule(World world)
    {
        world.Module<Module>();
        world.Component<Position>();
    }
}

namespace NamespaceLvl1
{
    namespace NamespaceLvl2
    {
        public struct StructLvl1
        {
            public struct StructLvl21 { }
            public struct StructLvl22 { }
        }
    }
}

public struct Child
{
    public struct GrandChild
    {
        public struct GreatGrandChild { }
    }
}

public struct PositionInitialized
{
    public float X;
    public float Y;

    public PositionInitialized(float x, float y)
    {
        X = x;
        Y = y;
    }
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

public struct Turret
{
    public struct Base { }
}

public struct Railgun
{
    public struct Base { }
    public struct Head { }
    public struct Beam { }
}

public struct EvtData
{
    public int Value;
}

public struct First
{
}

public struct Second
{
}

public struct PipelineType
{
}

public struct Base
{
}

public struct Foo
{
}

public struct Bar
{
}

public struct Evt
{
}

public struct Prefab
{
}

public struct EntityType
{
}

public struct A
{
}

public struct B
{
}

public struct IdA
{
}

public struct IdB
{
}

public struct MyTag
{
}

public struct Tgt
{
}

public struct Rel
{
}

public struct RelData
{
    public int Foo;

    public RelData(int foo)
    {
        Foo = foo;
    }
}

public struct Obj
{
}

public struct Likes
{
}

public struct Bob
{
}

public struct Alice
{
}

public struct R
{
}

public struct O1
{
}

public struct O2
{
}

public struct T1
{
}

public struct T2
{
}

public struct T3
{
}

public struct Movement
{
}

public struct Standing
{
}

public struct Walking
{
}

public struct Tag
{
}

public struct Eats
{
}

public struct Apples
{
}

public struct Pears
{
}

public struct TgtA
{
}

public struct TgtB
{
}

public struct TgtC
{
}

public struct Tag0
{
}

public struct Tag1
{
}

public struct Tag2
{
}

public struct Tag3
{
}

public struct Tag4
{
}

public struct Tag5
{
}

public struct Tag6
{
}

public struct Tag7
{
}

public struct Tag8
{
}

public struct Tag9
{
}

public struct Tag10
{
}

public struct Tag11
{
}

public struct Tag12
{
}

public struct Tag13
{
}

public struct Tag14
{
}

public struct Tag15
{
}

public struct Tag16
{
}

public struct Tag17
{
}

public struct Tag18
{
}

public struct Tag19
{
}

public struct Tag20
{
}


public struct Tag21
{
}

public struct Tag22
{
}


public struct Tag23
{
}


public struct Tag24
{
}


public struct Tag25
{
}

public struct Tag26
{
}

public struct Tag27
{
}

public struct Tag28
{
}

public struct Tag29
{
}

public struct Tag30
{
}

public struct Tag31
{
}
