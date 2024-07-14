// Events are propagated along relationship edges. This means that observers can
// listen for events from a parent or prefab, like triggering when a component
// inherited from a prefab was set.
//
// Event propagation happens automatically when an observer contains a filter
// with the EcsUp flag set (indicating upwards traversal). Observers use the
// same matching logic as queries: if a query with upwards traversal matches an
// entity, so will an observer.
//
// Events are only propagated along traversable relationship edges.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Observers_Propagate
{
    public static void Main()
    {
        using World world = World.Create();

        // Create observer that listens for events from both self and parent.
        world.Observer<Position, Position>()
            .TermAt(1).Parent()
            .Event(Ecs.OnSet)
            .Each((Iter it, int i, ref Position pSelf, ref Position pParent) =>
            {
                Console.Write($" - {it.Event()}: ");
                Console.Write($"{it.EventId()}: ");
                Console.Write($"{it.Entity(i)}: ");
                Console.Write($"Self: ({pSelf.X}, {pSelf.Y}) ");
                Console.Write($"Parent: ({pParent.X}, {pParent.Y})");
                Console.WriteLine();
            });

        // Create entity and parent
        Entity p = world.Entity("p");
        Entity e = world.Entity("e").ChildOf(p);

        // Set Position on entity. This doesn't trigger the observer yet, since the
        // parent doesn't have Position yet.
        e.Set(new Position { X = 10, Y = 20 });

        // Set Position on parent. This event will be propagated and trigger the
        // observer, as the observer query now matches.
        p.Set(new Position { X = 1, Y = 2 });
    }
}

// Output:
//  - OnSet: Position: e: Self: (10, 20) Parent: (1, 2)
