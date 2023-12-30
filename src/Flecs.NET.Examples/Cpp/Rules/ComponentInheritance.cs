// This example shows how rules can be used to match simple inheritance trees.

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

public static class Cpp_Rules_ComponentInheritance
{
    public static void Main()
    {
        using World world = World.Create();

        // Make the ECS aware of the inheritance relationships. Note that IsA
        // relationship used here is the same as in the prefab example.
        world.Component<CombatUnit>().Entity.IsA<Unit>();
        world.Component<MeleeUnit>().Entity.IsA<CombatUnit>();
        world.Component<RangedUnit>().Entity.IsA<CombatUnit>();

        world.Component<Warrior>().Entity.IsA<MeleeUnit>();
        world.Component<Wizard>().Entity.IsA<RangedUnit>();
        world.Component<Marksman>().Entity.IsA<RangedUnit>();
        world.Component<Builder>().Entity.IsA<Unit>();

        // Create a few units
        world.Entity("Warrior1").Add<Warrior>();
        world.Entity("Warrior2").Add<Warrior>();

        world.Entity("Marksman1").Add<Marksman>();
        world.Entity("Marksman2").Add<Marksman>();

        world.Entity("Wizard1").Add<Wizard>();
        world.Entity("Wizard2").Add<Wizard>();

        world.Entity("Builder1").Add<Builder>();
        world.Entity("Builder2").Add<Builder>();

        // Create a rule to find all ranged units
        Rule r = world.Rule<RangedUnit>();

        // Iterate the rule
        r.Each((Entity entity) => Console.WriteLine($"Unit {entity} found"));

        r.Destruct();
    }
}

// Output:
// Unit Wizard1 found
// Unit Wizard2 found
// Unit Marksman1 found
// Unit Marksman2 found
