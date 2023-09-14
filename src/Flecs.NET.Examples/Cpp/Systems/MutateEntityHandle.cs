#if Cpp_Systems_MutateEntityHandle

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

            // To make sure the delete operation is enqueued (see
            // MutateEntity example for more details) we need to provide it
            // with a mutable context (stage) using the Mut() function. If
            // we don't provide a mutable context, the operation will be
            // attempted on the context stored in the Entity object,
            // which would throw a readonly error.

            // The it.World() function can be used to provide the context:
            //   t.ToDelete.Mut(it.World()).Destruct();
            //
            // The current entity can also be used to provide context. This
            // is useful for functions that accept a Entity:
            //   t.ToDelete.Mut(it.Entity(i)).Destruct();
            //
            // A shortcut is to use the iterator directly:
            t[i].ToDelete.Mut(ref it).Destruct();
            Console.WriteLine($"Expire: {t[i].ToDelete.Name()} deleted!");
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
    filter: world.FilterBuilder().With<Tag>(),
    observer: world.ObserverBuilder().Event(Ecs.OnRemove),
    callback: (Entity e) =>
    {
        Console.WriteLine($"Expired: {e.Name()} actually deleted");
    }
);

// Add Tag so we can get notified when entity is actually deleted
Entity toDelete = world.Entity("ToDelete")
    .Add<Tag>();

world.Entity("MyEntity")
    .Set(new Timeout { ToDelete = toDelete, Value = 3.0});

world.SetTargetFps(1);

while (world.Progress())
{
    // If entity is no longer alive, exit
    if (!toDelete.IsAlive())
        break;

    Console.WriteLine("Tick...");
}
public struct Timeout
{
    public Entity ToDelete { get; set; }
    public double Value { get; set; }
}

public struct Tag { }

#endif

// Output:
// PrintExpire: MyEntity has 2.00 seconds left
// Tick...
// PrintExpire: MyEntity has 1.00 seconds left
// Tick...
// Expire: ToDelete deleted!
// PrintExpire: MyEntity has -0.00 seconds left
// Expired: ToDelete actually deleted
