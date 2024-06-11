// This example shows how relationships can be combined with components to attach
// data to a relationship.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Requires(float Amount);
file record struct Expires(float Timeout);

// Tags
file struct Gigawatts;
file struct MustHave;

public static class Cpp_Relationships_RelationComponent
{
    public static void Main()
    {
        using World world = World.Create();

        // When one element of a pair is a component and the other element is a tag,
        // the pair assumes the type of the component.
        Entity e1 = world.Entity().Set<Requires, Gigawatts>(new Requires(1.21f));
        ref readonly Requires r = ref e1.GetFirst<Requires, Gigawatts>();
        Console.WriteLine($"Requires: {r.Amount}");

        // The component can be either the first or second part of a pair:
        Entity e2 = world.Entity().Set<Gigawatts, Requires>(new Requires(1.21f));
        r = ref e2.GetSecond<Gigawatts, Requires>();
        Console.WriteLine($"Requires: {r.Amount}");

        // Note that <Requires, Gigawatts> and <Gigawatts, Requires> are two
        // different pairs, and can be added to an entity at the same time.

        // If both parts of a pair are components, the pair assumes the type of
        // the first element:
        Entity e3 = world.Entity().Set<Expires, Position>(new Expires(0.5f));
        ref readonly Expires e = ref e3.GetFirst<Expires, Position>();
        Console.WriteLine($"Expires: {e.Timeout}");

        // You can prevent a pair from assuming the type of a component by adding
        // the Tag property to a relationship:
        world.Component<MustHave>().Entity.Add(Ecs.PairIsTag);

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
        using Query q = world.QueryBuilder<Requires>()
            .TermAt(1).Second<Gigawatts>() // Set second part of pair for first term
            .Build();

        // When iterating, always use the pair type:
        q.Each((ref Requires rq) =>
        {
            Console.WriteLine($"Requires {rq.Amount} gigawatts");
        });
    }
}

// Output:
// Requires: 1.21
// Requires: 1.21
// Expires: 0.5
// Requires
// Requires
// Expires
// 0
// Requires 1.21 gigawatts
