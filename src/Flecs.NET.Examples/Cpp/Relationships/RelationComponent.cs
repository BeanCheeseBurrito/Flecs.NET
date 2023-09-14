// This example shows how relationships can be combined with components to attach
// data to a relationship.

#if Cpp_Relationships_RelationComponent

using Flecs.NET.Core;

using World world = World.Create(args);

// When one element of a pair is a component and the other element is a tag,
// the pair assumes the type of the component.
Entity e1 = world.Entity().SetFirst<Requires, Gigawatts>(new Requires { Amount = 1.21 });
ref readonly Requires r = ref e1.GetFirst<Requires, Gigawatts>();
Console.WriteLine($"requires: {r.Amount}");

// The component can be either the first or second part of a pair:
Entity e2 = world.Entity().SetSecond<Gigawatts, Requires>(new Requires { Amount = 1.21 });
r = ref e2.GetSecond<Gigawatts, Requires>();
Console.WriteLine($"requires: {r.Amount}");

// Note that <Requires, Gigawatts> and <Gigawatts, Requires> are two
// different pairs, and can be added to an entity at the same time.

// If both parts of a pair are components, the pair assumes the type of
// the first element:
Entity e3 = world.Entity().SetFirst<Expires, Position>(new Expires { Timeout = 0.5 });
ref readonly Expires e = ref e3.GetFirst<Expires, Position>();
Console.WriteLine($"expires: {e.Timeout}");

// You can prevent a pair from assuming the type of a component by adding
// the Tag property to a relationship:
world.Component<MustHave>().Entity.Add(Ecs.Tag);

// Even though Position is a component, <MustHave, Position> contains no
// data because MustHave has the Tag property.
world.Entity().Add<MustHave, Position>();

// The Id.TypeId method can be used to find the component type for a pair:
Console.WriteLine(world.Pair<Requires, Gigawatts>().TypeId().Path());
Console.WriteLine(world.Pair<Gigawatts, Requires>().TypeId().Path());
Console.WriteLine(world.Pair<Expires, Position>().TypeId().Path());
Console.WriteLine(world.Pair<MustHave, Position>().TypeId().Path());

// When querying for a relationship component, add the pair type as template
// argument to the builder:
Query q = world.Query(
    filter: world.FilterBuilder()
        .Term<Requires>().Second<Gigawatts>() // set second part of pair for first term
);

// When iterating, always use the pair type:
q.Each((Iter it, int i) =>
{
    Column<Requires> rq = it.Field<Requires>(1);
    Console.WriteLine($"requires {rq[i].Amount} gigawatts");
});

public struct Requires
{
    public double Amount { get; set; }
}

public struct Expires
{
    public double Timeout { get; set; }
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Gigawatts { }
public struct MustHave { }

#endif

// Output:
// requires: 1.21
// requires: 1.21
// expires: 0.5
// Requires
// Requires
// Expires
// 0
// requires 1.21 gigawatts
