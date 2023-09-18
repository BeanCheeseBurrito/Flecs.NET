// This example extends the ComponentInheritance example, and shows how
// we can use a single rule to match units from different players and platoons
// by setting query variables before we iterate.
//
// The units in this example belong to a platoon, with the platoons belonging
// to a player.

#if Cpp_Rules_SettingVariables

using Flecs.NET.Core;

const int playerCount = 100;
const int platoonsPerPlayer = 3;

using World world = World.Create();

// See ComponentInheritance example
world.Component<CombatUnit>().Entity.IsA<Unit>();
world.Component<MeleeUnit>().Entity.IsA<CombatUnit>();
world.Component<RangedUnit>().Entity.IsA<CombatUnit>();

world.Component<Warrior>().Entity.IsA<MeleeUnit>();
world.Component<Wizard>().Entity.IsA<RangedUnit>();
world.Component<Marksman>().Entity.IsA<RangedUnit>();

// Populate store with players and platoons
for (int p = 0; p < playerCount; p++) {
    Entity player;

    // Give first player a name so we can look it up later
    if (p == 0)
        player = world.Entity("MyPlayer");
    else
        player = world.Entity();

    // Add player tag so we can query for all players if we want to
    player.Add<Player>();

    for (int pl = 0; pl < platoonsPerPlayer; pl++) {
        Entity platoon = world.Entity().Add<Player>(player);

        // Add platoon tag so we can query for all platoons if we want to
        platoon.Add<Platoon>();

        // Add warriors, wizards and marksmen to the platoon
        world.Entity().Add<Warrior>().Add<Platoon>(platoon);
        world.Entity().Add<Marksman>().Add<Platoon>(platoon);
        world.Entity().Add<Wizard>().Add<Platoon>(platoon);
    }
}

// Create a rule to find all RangedUnits for a platoon/player. The
// equivalent query in the query DSL would look like this:
//   (Platoon, $Platoon), Player($Platoon, $Player)
//
// The way to read how this query is evaluated is:
// - find all entities with (Platoon, *), store * in _Platoon
// - check if _Platoon has (Player, *), store * in _Player
Rule r = world.Rule(
    filter: world.FilterBuilder()
        .With<RangedUnit>()
        .With<Platoon>().Second("$Platoon")
        .With<Player>("$Player").Src("$Platoon")
);

// If we would iterate this rule it would return all ranged units for all
// platoons & for all players. We can limit the results to just a single
// platoon or a single player setting a variable beforehand. In this example
// we'll just find all platoons & ranged units for a single player.

int playerVar = r.FindVar("Player");
int platoonVar = r.FindVar("Platoon");

// Iterate rule, limit the results to units of MyPlayer
r.Iter().SetVar(playerVar, world.Lookup("MyPlayer")).Each((Iter it, int i) =>
{
    Entity unit = it.Entity(i);
    Console.Write($"Unit {unit.Path()} of class {it.Id(1).Str()} ");
    Console.Write($"in platoon {it.GetVar(platoonVar).Path()} ");
    Console.WriteLine($"for player {it.GetVar(playerVar).Path()}");
});

r.Destruct();

// Unit datamodel
public struct Unit { }
public struct CombatUnit { }
public struct MeleeUnit { }
public struct RangedUnit { }
public struct Warrior { }
public struct Wizard { }
public struct Marksman { }

// Player/Platoon tags
public struct Player { }
public struct Platoon { }

#endif

// Output:
// Unit 532 of class Wizard in platoon 529 for player MyPlayer
// Unit 536 of class Wizard in platoon 533 for player MyPlayer
// Unit 540 of class Wizard in platoon 537 for player MyPlayer
// Unit 531 of class Marksman in platoon 529 for player MyPlayer
// Unit 535 of class Marksman in platoon 533 for player MyPlayer
// Unit 539 of class Marksman in platoon 537 for player MyPlayer

// Try removing the SetVar call, this will cause the iterator to return
// all units in all platoons for all players.
