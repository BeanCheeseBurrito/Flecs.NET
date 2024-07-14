// Transitive relationships make it possible to tell the ECS that if an entity
// has a relationship (R, X) and X has relationship (R, Y), the entity should be
// treated as if it also has (R, Y). In practice this is useful for expressing
// things like:
//
// Bob lives in SanFrancisco
// San Francisco is located in the United States
// Therefore Bob also lives in the United States.
//
// An example of transitivity can be seen in the ComponentInheritance example.
// This example uses the builtin IsA relationship, which is transitive. This
// example shows how to achieve similar behavior with a user-defined relationship.

using Flecs.NET.Core;

// Tags
file struct LocatedIn;
file struct Planet;
file struct Continent;
file struct Country;
file struct State;
file struct City;
file struct Person;

public static class Queries_TransitiveQueries
{
    public static void Main()
    {
        using World world = World.Create();

        // Register the LocatedIn relationship as transitive
        world.Component<LocatedIn>().Entity.Add(Ecs.Transitive);

        // Populate the store with locations
        Entity earth = world.Entity("Earth")
            .Add<Planet>();

        // Continents
        Entity northAmerica = world.Entity("NorthAmerica")
            .Add<Continent>()
            .Add<LocatedIn>(earth);

        Entity europe = world.Entity("Europe")
            .Add<Continent>()
            .Add<LocatedIn>(earth);

        // Countries
        Entity unitedStates = world.Entity("UnitedStates")
            .Add<Country>()
            .Add<LocatedIn>(northAmerica);

        Entity netherlands = world.Entity("Netherlands")
            .Add<Country>()
            .Add<LocatedIn>(europe);

        // States
        Entity california = world.Entity("California")
            .Add<State>()
            .Add<LocatedIn>(unitedStates);

        Entity washington = world.Entity("Washington")
            .Add<State>()
            .Add<LocatedIn>(unitedStates);

        Entity noordHolland = world.Entity("NoordHolland")
            .Add<State>()
            .Add<LocatedIn>(netherlands);

        // Cities
        Entity sanFrancisco = world.Entity("SanFrancisco")
            .Add<City>()
            .Add<LocatedIn>(california);

        Entity seattle = world.Entity("Seattle")
            .Add<City>()
            .Add<LocatedIn>(washington);

        Entity amsterdam = world.Entity("Amsterdam")
            .Add<City>()
            .Add<LocatedIn>(noordHolland);

        // Inhabitants
        world.Entity("Bob")
            .Add<Person>()
            .Add<LocatedIn>(sanFrancisco);

        world.Entity("Alice")
            .Add<Person>()
            .Add<LocatedIn>(seattle);

        world.Entity("Job")
            .Add<Person>()
            .Add<LocatedIn>(amsterdam);

        // Create a query that finds the countries persons live in. Note that these
        // have not been explicitly added to the Person entities, but because the
        // LocatedIn is transitive, the query engine will traverse the relationship
        // until it found something that is a country.
        //
        // The equivalent of this query in the DSL is:
        //   Person, (LocatedIn, $location), Country($location)
        using Query q = world.QueryBuilder()
            .With<Person>()
            .With<LocatedIn>("$location")
            .With<Country>().Src("$location")
            .Build();

        // Lookup the index of the variable. This will let us quickly lookup its
        // value while we're iterating.
        int locationVar = q.FindVar("location");

        // Iterate the query
        q.Each((Iter it, int i) =>
        {
            Console.WriteLine($"{it.Entity(i)} lives in {it.GetVar(locationVar)}");
        });
    }
}

// Output:
// Bob lives in UnitedStates
// Alice lives in UnitedStates
// Job lives in Netherlands
