using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

[assembly: SuppressMessage("Usage", "CA1050")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

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
    public void InitModule(ref World world)
    {
        world.Module<MyModule>();
        world.Component<Position>();
    }
}

public struct Pod
{
    public int Value { get; set; }

    public static int CtorInvoked { get; set; }
    public static int DtorInvoked { get; set; }
    public static int MoveInvoked { get; set; }
    public static int CopyInvoked { get; set; }

    public Pod(int value)
    {
        Value = value;
    }

    static Pod()
    {
        Type<Pod>.TypeHooks = new TypeHooks<Pod>
        {
            Ctor = (ref Pod data, TypeInfo typeInfo) =>
            {
                CtorInvoked++;
                data = default;
            },
            Dtor = (ref Pod data, TypeInfo typeInfo) =>
            {
                DtorInvoked++;
                data = default;
            },
            Move = (ref Pod dst, ref Pod src, TypeInfo typeInfo) =>
            {
                MoveInvoked++;
                dst = src;
                src = default;
            },
            Copy = (ref Pod dst, ref Pod src, TypeInfo typeInfo) =>
            {
                CopyInvoked++;
                dst = src;
            },

            OnAdd = (Iter it, Field<Pod> pod) => { },
            OnSet = (Iter it, Field<Pod> pod) => { },
            OnRemove = (Iter it, Field<Pod> pod) => { }
        };
    }
}

namespace Namespace
{
    public struct NestedNameSpaceType { }

    public struct NestedModule : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            world.Module<NestedModule>();
            world.Component<Velocity>("Velocity");
        }
    }

    public struct SimpleModule : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            world.Module<SimpleModule>();
            world.Import<NestedModule>();
            world.Component<Position>("Position");
        }
    }

    public struct NestedTypeModule : IFlecsModule
    {
        public struct NestedType { }

        public void InitModule(ref World world)
        {
            world.Module<NestedTypeModule>();
            world.Component<NestedType>();
            world.Component<NestedNameSpaceType>();
        }
    }

    public struct NamedModule : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            world.Module<NamedModule>("::my_scope.NamedModule");
            world.Component<Position>("Position");
        }
    }

    public struct ImplicitModule : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            world.Component<Position>();
        }
    }

    public struct NamedModuleInRoot : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            world.Module<NamedModuleInRoot>("::NamedModuleInRoot");
            world.Component<Position>();
        }
    }

    public struct ReparentModule : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            Entity m = world.Module<ReparentModule>();
            m.ChildOf(world.Entity("::parent"));

            Entity other = world.Entity("::Namespace.ReparentModule");
            Assert.True(other != 0);
            Assert.True(other != m);
        }
    }
}

public struct Module : IFlecsModule
{
    public void InitModule(ref World world)
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

public struct TagA
{
}

public struct TagB
{
}

public struct TagC
{
}

public struct TagD
{
}

public struct TagE
{
}

public struct TagF
{
}

public struct TagG
{
}

public struct TagH
{
}

public struct TagI
{
}

public struct TagJ
{
}

public struct TagK
{
}

public struct TagL
{
}

public struct TagM
{
}

public struct TagN
{
}

public struct TagO
{
}

public struct TagP
{
}

public struct TagQ
{
}

public struct TagR
{
}

public struct TagS
{
}

public struct TagT
{
}

public struct TagU
{
}


public struct TagV
{
}

public struct TagW
{
}


public struct TagX
{
}


public struct TagY
{
}


public struct TagZ
{
}
