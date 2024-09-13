using Flecs.NET.Core;

// Components
file record struct Timeout(float Value);

public static class Systems_MutateEntity
{
    public static void Main()
    {
        using World world = World.Create();

        // System that deletes an entity after a timeout expires
        world.System<Timeout>()
            .Each((Iter it, int i, ref Timeout t) =>
            {
                t.Value -= it.DeltaTime();

                if (t.Value <= 0)
                {
                    // Delete the entity

                    // To make sure that the storage doesn't change while a system
                    // is iterating entities, and multiple threads can safely access
                    // the data, mutations (like a delete) are added to a command
                    // queue and executed when it's safe to do so.

                    // When the entity to be mutated is not the same as the entity
                    // provided by the system, an additional Mut() call is required.
                    // See the MutateEntityHandle example.
                    Entity e = it.Entity(i);
                    e.Destruct();
                    Console.WriteLine($"Expire: {e} deleted!");
                }
            });

        // System that prints remaining expiry time
        world.System<Timeout>()
            .Each((Entity e, ref Timeout t) =>
            {
                Console.WriteLine($"PrintExpire: {e} has {t.Value:0.00} seconds left");
            });

        // Observer that triggers when entity is actually deleted
        world.Observer<Timeout>()
            .Event(Ecs.OnRemove)
            .Each((Entity e, ref Timeout _) =>
            {
                Console.WriteLine($"Expired: {e} actually deleted");
            });

        Entity e = world.Entity("MyEntity")
            .Set(new Timeout(3));

        world.SetTargetFps(1);

        while (world.Progress())
        {
            // If entity is no longer alive, exit
            if (!e.IsAlive())
                break;

            Console.WriteLine("Tick...");
        }
    }
}

// Output:
// PrintExpire: MyEntity has 2.00 seconds left
// Tick...
// PrintExpire: MyEntity has 1.00 seconds left
// Tick...
// Expire: MyEntity deleted!
// PrintExpire: MyEntity has -0.00 seconds left
// Expired: MyEntity actually deleted
