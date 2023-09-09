using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Core;
using Xunit;

[assembly: SuppressMessage("Usage", "CA1050")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

public struct Position
{
    public float X { get; set; }
    public float Y { get; set; }
}

public struct Velocity
{
    public float X { get; set; }
    public float Y { get; set; }
}

public struct Mass
{
    public float Value { get; set; }
}

public struct Pair
{
    public float Value { get; set; }
}

public struct Template<T>
{
    public T X { get; set; }
    public T Y { get; set; }
}

public struct Self
{
    public Entity Value { get; set; }
}

public struct FilterWrapper
{
    public Filter Filter { get; set; }

    public FilterWrapper(Filter filter)
    {
        Filter = filter;
    }
};

public struct Parent
{
    public struct EntityType
    {
    }
}

public struct Other
{
    public int Value { get; set; }
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
    public int CtorInvoked { get; set; }
    public int DtorInvoked { get; set; }
    public int MoveInvoked { get; set; }
    public int CopyInvoked { get; set; }

    static Pod()
    {
        Type<Pod>.SetTypeHooks(new TypeHooks
        {
            Ctor = (ref CtorData ctorData) =>
            {
                ctorData.Get<Pod>().CtorInvoked++;
                ctorData.Get<Pod>().Value = 10;
            },
            Dtor = (ref DtorData dtorData) => { dtorData.Get<Pod>().DtorInvoked++; },
            Move = (ref MoveData moveData) => { },
            Copy = (ref CopyData copyData) => { }
        });
    }
};

public struct MyTag
{
}

public struct Tgt
{
}

public struct Rel
{
}

public struct Likes
{
}

public struct Bob
{
}

public struct Tag
{
}

public struct TgtA
{
}

public struct TgtB
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
