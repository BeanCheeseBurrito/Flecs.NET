// When an application calls world.Progress(), the world is put in readonly mode.
// This ensures that systems (on multiple threads) can safely iterate
// components, without having to worry about components moving around while
// they're being read. This has as side effect that any operations (like adding
// or removing components) are not visible until the end of the frame (see the
// SyncPoint example for more details).
// Sometimes this is not what you want, and you need a change to be visible
// immediately. For these use cases, applications can use a NoReadonly system.
// This temporarily takes the world out of readonly mode, so a system can make
// changes that are directly visible.
// Because they mutate the world directly, NoReadonly systems are never ran on
// more than one thread, and no other systems are ran at the same time.

#if Cpp_Systems_NoReadonly

using Flecs.NET.Core;

using World world = World.Create();

// Create query to find all waiters without a plate
Query qWaiter = world.Query(
    filter: world.FilterBuilder()
        .With<Waiter>()
        .Without<Plate>(Ecs.Wildcard)
);

// System that assigns plates to waiter. By making this system NoReadonly
// plate assignments are assigned directly (not deferred) to waiters, which
// ensures that we won't assign plates to the same waiter more than once.
world.Routine(
    filter: world.FilterBuilder()
        .With<Plate>()
        .Without<Waiter>(Ecs.Wildcard),
    routine: world.RoutineBuilder().NoReadonly(),
    callback: (Iter it) =>
    {
        foreach (int i in it)
        {
            Entity plate = it.Entity(i);

            // Find an available waiter
            Entity waiter = qWaiter.First(); // TODO: Implement iter_iterable

            if (waiter != 0)
            {
                // An available waiter was found, assign a plate to it so
                // that the next plate will no longer find it.
                // The DeferSuspend function temporarily suspends deferring
                // operations, which ensures that our plate is assigned
                // immediately. Even though this is a NoReadonly system,
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

                Console.WriteLine($"Assigned {waiter.Name()} to {plate.Name()}");
            }
            else
            {
                // No available waiters, can't assign the plate
            }
        }
    }
);

// Create a few plates and waiters
Entity waiter1 = world.Entity("waiter_1").Add<Waiter>();
world.Entity("waiter_2").Add<Waiter>();
world.Entity("waiter_3").Add<Waiter>();

world.Entity("plate_1").Add<Plate>();
Entity plate2 = world.Entity("plate_2").Add<Plate>();
world.Entity("plate_3").Add<Plate>();

waiter1.Add<Plate>(plate2);
plate2.Add<Waiter>(waiter1);

// run systems
world.Progress();

public struct Waiter { }
public struct Plate { }

#endif

// Output:
