#if Cpp_Systems_MutateEntity

using Flecs.NET.Core;

using World world = World.Create();

// System that deletes an entity after a timeout expires
world.Routine(
    filter: world.FilterBuilder().With<Timeout>(),
    callback: (Iter it, int i) =>
    {
        Column<Timeout> t = it.Field<Timeout>(1);
        t[i].Value -= it.DeltaTime();

        if (t[i].Value <= 0)
        {
            // Delete the entity

            // To make sure that the storage doesn't change while a system
            // is iterating entities, and multiple threads can safely access
            // the data, mutations (like a delete) are added to a command
            // queue and executed when it's safe to do so.

            // When the entity to be mutated is not the same as the entity
            // provided by the system, an additional mut() call is required.
            // See the MutateEntityHandle example.
            Entity e = it.Entity(i);
            e.Destruct();
            Console.WriteLine($"Expire: {e.Name()} deleted!");
        }
    }
);

// System that prints remaining expiry time
world.Routine(
    filter: world.FilterBuilder().With<Timeout>(),
    callback: (Iter it, int i) =>
    {
        Column<Timeout> t = it.Field<Timeout>(1);
        Console.WriteLine($"PrintExpire: {it.Entity(i).Name()} has {t[i].Value:0.00} seconds left");
    }
);

// Observer that triggers when entity is actually deleted
world.Observer(
    filter: world.FilterBuilder().With<Timeout>(),
    observer: world.ObserverBuilder().Event(Ecs.OnRemove),
    callback: (Iter it, int i) =>
    {
        Column<Timeout> t = it.Field<Timeout>(1);
        Console.WriteLine($"Expired: {it.Entity(i).Name()} actually deleted");
    }
);

Entity e = world.Entity("MyEntity")
    .Set(new Timeout { Value = 3.0 });

world.SetTargetFps(1);

while (world.Progress())
{
    // If entity is no longer alive, exit
    if (!e.IsAlive())
        break;

    Console.WriteLine("Tick...");
}

public struct Timeout
{
    public double Value { get; set; }
}

#endif

// Output:
// PrintExpire: MyEntity has 2.00 seconds left
// Tick...
// PrintExpire: MyEntity has 1.00 seconds left
// Tick...
// Expire: MyEntity deleted!
// PrintExpire: MyEntity has -0.00 seconds left
// Expired: MyEntity actually deleted
