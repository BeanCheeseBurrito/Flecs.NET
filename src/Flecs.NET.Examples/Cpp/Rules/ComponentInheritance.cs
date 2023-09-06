// This example shows how rules can be used to match simple inheritance trees.

#if Cpp_Rules_ComponentInheritance

using Flecs.NET.Core;

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
world.Entity("warrior_1").Add<Warrior>();
world.Entity("warrior_2").Add<Warrior>();

world.Entity("marksman_1").Add<Marksman>();
world.Entity("marksman_2").Add<Marksman>();

world.Entity("wizard_1").Add<Wizard>();
world.Entity("wizard_2").Add<Wizard>();

world.Entity("builder_1").Add<Builder>();
world.Entity("builder_2").Add<Builder>();

// Create a rule to find all ranged units
Rule r = world.Rule(
    filter: world.FilterBuilder().Term<RangedUnit>()
);

// Iterate the rule
r.Each((Entity entity) => Console.WriteLine($"Unit {entity.Name()} found"));

r.Destruct();

public struct Unit { }
public struct CombatUnit { }
public struct MeleeUnit { }
public struct RangedUnit { }

public struct Warrior  { }
public struct Wizard { }
public struct Marksman { }
public struct Builder { }

#endif

// Output:
// Unit wizard_1 found
// Unit wizard_2 found
// Unit marksman_1 found
// Unit marksman_2 found
