// This example is the same as the MutateEntity example, but instead stores the
// handle of the to be deleted entity in a component.

using Flecs.NET.Core;

// Components
file record struct Timeout(Entity ToDelete, float Value);

// Tags
file struct Tag;

public static class Cpp_Systems_MutateEntityHandle
{
    public static void Main()
    {
        using World world = World.Create();

        // System that deletes an entity after a timeout expires
        world.Routine<Timeout>()
            .Each((Iter it, int i, ref Timeout t) =>
            {
                t.Value -= it.DeltaTime();

                if (t.Value <= 0)
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
                    t.ToDelete.Mut(ref it).Destruct();
                    Console.WriteLine($"Expire: {t.ToDelete} deleted!");
                }
            });

        // System that prints remaining expiry time
        world.Routine<Timeout>()
            .Each((ref Timeout t) =>
            {
                Console.WriteLine($"PrintExpire: {t.ToDelete} has {t.Value:0.00} seconds left");
            });

        // Observer that triggers when entity is actually deleted
        world.Observer<Tag>()
            .Event(Ecs.OnRemove)
            .Each((Entity e) =>
            {
                Console.WriteLine($"Expired: {e.Name()} actually deleted");
            });

        // Add Tag so we can get notified when entity is actually deleted
        Entity toDelete = world.Entity("ToDelete")
            .Add<Tag>();

        world.Entity("MyEntity")
            .Set<Timeout>(new Timeout(toDelete, 3));

        world.SetTargetFps(1);

        while (world.Progress())
        {
            // If entity is no longer alive, exit
            if (!toDelete.IsAlive())
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
// Expire: ToDelete deleted!
// PrintExpire: MyEntity has -0.00 seconds left
// Expired: ToDelete actually deleted
