// This example extends the ComponentInheritance example, and shows how
// we can use a single query to match units from different players and platoons
// by setting query variables before we iterate.
//
// The units in this example belong to a platoon, with the platoons belonging
// to a player.

using Flecs.NET.Core;

// Unit datamodel
file struct Unit;
file struct CombatUnit;
file struct MeleeUnit;
file struct RangedUnit;
file struct Warrior;
file struct Wizard;
file struct Marksman;

// Player/Platoon tags
file struct Player;
file struct Platoon;

public static class Cpp_Queries_SettingVariables
{
    const int PlayerCount = 100;
    const int PlatoonsPerPlayer = 3;

    public static void Main()
    {
        using World world = World.Create();

        // See ComponentInheritance example
        world.Component<CombatUnit>().Entity.IsA<Unit>();
        world.Component<MeleeUnit>().Entity.IsA<CombatUnit>();
        world.Component<RangedUnit>().Entity.IsA<CombatUnit>();

        world.Component<Warrior>().Entity.IsA<MeleeUnit>();
        world.Component<Wizard>().Entity.IsA<RangedUnit>();
        world.Component<Marksman>().Entity.IsA<RangedUnit>();

        // Populate store with players and platoons
        for (int p = 0; p < PlayerCount; p++)
        {
            Entity player;

            // Give first player a name so we can look it up later
            if (p == 0)
                player = world.Entity("MyPlayer");
            else
                player = world.Entity();

            // Add player tag so we can query for all players if we want to
            player.Add<Player>();

            for (int pl = 0; pl < PlatoonsPerPlayer; pl++)
            {
                Entity platoon = world.Entity().Add<Player>(player);

                // Add platoon tag so we can query for all platoons if we want to
                platoon.Add<Platoon>();

                // Add warriors, wizards and marksmen to the platoon
                world.Entity().Add<Warrior>().Add<Platoon>(platoon);
                world.Entity().Add<Marksman>().Add<Platoon>(platoon);
                world.Entity().Add<Wizard>().Add<Platoon>(platoon);
            }
        }

        // Create a query to find all RangedUnits for a platoon/player. The
        // equivalent query in the query DSL would look like this:
        //   (Platoon, $platoon), Player($platoon, $player)
        //
        // The way to read how this query is evaluated is:
        // - find all entities with (Platoon, *), store * in _Platoon
        // - check if _Platoon has (Player, *), store * in _Player
        using Query q = world.QueryBuilder<RangedUnit>()
            .With<Platoon>().Second("$platoon")
            .With<Player>("$player").Src("$platoon")
            .Build();

        // If we would iterate this query it would return all ranged units for all
        // platoons & for all players. We can limit the results to just a single
        // platoon or a single player setting a variable beforehand. In this example
        // we'll just find all platoons & ranged units for a single player.

        int playerVar = q.FindVar("Player");
        int platoonVar = q.FindVar("Platoon");

        // Iterate query, limit the results to units of MyPlayer
        q.Iter().SetVar(playerVar, world.Lookup("MyPlayer")).Each((Iter it, int i) =>
        {
            Entity unit = it.Entity(i);
            Console.Write($"Unit {unit.Path()} of class {it.Id(1).Str()} ");
            Console.Write($"in platoon {it.GetVar(platoonVar).Path()} ");
            Console.WriteLine($"for player {it.GetVar(playerVar).Path()}");
        });
    }
}

// Output:
// Unit 532 of class Wizard in platoon 529 for player MyPlayer
// Unit 536 of class Wizard in platoon 533 for player MyPlayer
// Unit 540 of class Wizard in platoon 537 for player MyPlayer
// Unit 531 of class Marksman in platoon 529 for player MyPlayer
// Unit 535 of class Marksman in platoon 533 for player MyPlayer
// Unit 539 of class Marksman in platoon 537 for player MyPlayer

// Try removing the SetVar call, this will cause the iterator to return
// all units in all platoons for all players.
