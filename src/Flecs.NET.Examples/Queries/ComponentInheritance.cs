// This example shows how queries can be used to match simple inheritance trees.

using Flecs.NET.Core;

// Tags
file struct Unit;
file struct CombatUnit;
file struct MeleeUnit;
file struct RangedUnit;
file struct Warrior;
file struct Wizard;
file struct Marksman;
file struct Builder;

public static class Queries_ComponentInheritance
{
    public static void Main()
    {
        using World world = World.Create();

        // Make the ECS aware of the inheritance relationships. Note that IsA
        // relationship used here is the same as in the prefab example.
        world.Component<CombatUnit>().IsA<Unit>();
        world.Component<MeleeUnit>().IsA<CombatUnit>();
        world.Component<RangedUnit>().IsA<CombatUnit>();

        world.Component<Warrior>().IsA<MeleeUnit>();
        world.Component<Wizard>().IsA<RangedUnit>();
        world.Component<Marksman>().IsA<RangedUnit>();
        world.Component<Builder>().IsA<Unit>();

        // Create a few units
        world.Entity("Warrior1").Add<Warrior>();
        world.Entity("Warrior2").Add<Warrior>();

        world.Entity("Marksman1").Add<Marksman>();
        world.Entity("Marksman2").Add<Marksman>();

        world.Entity("Wizard1").Add<Wizard>();
        world.Entity("Wizard2").Add<Wizard>();

        world.Entity("Builder1").Add<Builder>();
        world.Entity("Builder2").Add<Builder>();

        // Create a query to find all ranged units
        using Query q = world.QueryBuilder()
            .With<RangedUnit>()
            .Build();

        // Iterate the query
        q.Each((Entity entity) => Console.WriteLine($"Unit {entity} found"));
    }
}

// Output:
// Unit Wizard1 found
// Unit Wizard2 found
// Unit Marksman1 found
// Unit Marksman2 found
