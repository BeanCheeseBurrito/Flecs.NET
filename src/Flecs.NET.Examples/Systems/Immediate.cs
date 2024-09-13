// When an application calls world.Progress(), the world is put in readonly mode.
// This ensures that systems (on multiple threads) can safely iterate
// components, without having to worry about components moving around while
// they're being read. This has as side effect that any operations (like adding
// or removing components) are not visible until the end of the frame (see the
// SyncPoint example for more details).
// Sometimes this is not what you want, and you need a change to be visible
// immediately. For these use cases, applications can use an immediate system.
// This temporarily takes the world out of readonly mode, so a system can make
// changes that are directly visible.
// Because they mutate the world directly, immediate systems are never ran on
// more than one thread, and no other systems are ran at the same time.

using Flecs.NET.Core;

// Tags
file struct Waiter;
file struct Plate;

public static class Systems_Immediate
{
    public static void Main()
    {
        using World world = World.Create();

        // Create query to find all waiters without a plate
        using Query qWaiter = world.QueryBuilder()
            .With<Waiter>()
            .Without<Plate>(Ecs.Wildcard)
            .Build();

        // System that assigns plates to waiter. By making this system immediate,
        // plate assignments are assigned directly (not deferred) to waiters, which
        // ensures that we won't assign plates to the same waiter more than once.
        world.System()
            .With<Plate>()
            .Without<Waiter>(Ecs.Wildcard)
            .Immediate()
            .Each((Iter it, int i) =>
            {
                Entity plate = it.Entity(i);

                // Find an available waiter
                Entity waiter = qWaiter.First();

                if (waiter != 0)
                {
                    // An available waiter was found, assign a plate to it so
                    // that the next plate will no longer find it.
                    // The DeferSuspend function temporarily suspends deferring
                    // operations, which ensures that our plate is assigned
                    // immediately. Even though this is an immediate system,
                    // deferring is still enabled by default, as adding/removing
                    // components to the entities being iterated would interfere
                    // with the system iterator.
                    it.World().DeferSuspend();
                    waiter.Add<Plate>(plate);
                    it.World().DeferResume();

                    // Now that deferring is resumed, we can safely also add the
                    // waiter to the plate. We can't do this while deferring is
                    // suspended, because the plate is the entity we're
                    // currently iterating, and we don't want to move it to a
                    // different table while we're iterating it.
                    plate.Add<Waiter>(waiter);

                    Console.WriteLine($"Assigned {waiter.Name()} to {plate}!");
                }
                else
                {
                    // No available waiters, can't assign the plate
                }
            });

        // Create a few plates and waiters
        Entity waiter1 = world.Entity("Waiter1").Add<Waiter>();
        world.Entity("Waiter2").Add<Waiter>();
        world.Entity("Waiter3").Add<Waiter>();

        world.Entity("Plate1").Add<Plate>();
        Entity plate2 = world.Entity("Plate2").Add<Plate>();
        world.Entity("Plate3").Add<Plate>();

        waiter1.Add<Plate>(plate2);
        plate2.Add<Waiter>(waiter1);

        // run systems
        world.Progress();
    }
}

// Output:
// Assigned Waiter3 to Plate1!
// Assigned Waiter2 to Plate3!
