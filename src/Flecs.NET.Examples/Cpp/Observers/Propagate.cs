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

#if Cpp_Observers_Propagate

using Flecs.NET.Core;

using World world = World.Create(args);

// Create observer that listens for events from both self and parent
world.Observer(
    filter: world.FilterBuilder()
        .Term<Position>()
        .Term<Position>().Parent(), // select 2nd Position from parent
    observer: world.ObserverBuilder().Event(Ecs.OnSet),
    callback: (Iter it, int i) =>
    {
        Column<Position> pSelf = it.Field<Position>(1);
        Column<Position> pParent = it.Field<Position>(2);

        Console.Write($" - {it.Event().Name()}: ");
        Console.Write($"{it.EventId().Str()}: ");
        Console.Write($"{it.Entity(i).Name()}: ");
        Console.Write($"self: ({pSelf[i].X}, {pSelf[i].Y}) ");
        Console.Write($"parent: ({pParent[i].X}, {pParent[i].Y})");
        Console.WriteLine();
    }
);

// Create entity and parent
Entity p = world.Entity("p");
Entity e = world.Entity("e").ChildOf(p);

// Set Position on entity. This doesn't trigger the observer yet, since the
// parent doesn't have Position yet.
e.Set(new Position { X = 10, Y = 20 });

// Set Position on parent. This event will be propagated and trigger the
// observer, as the observer query now matches.
p.Set(new Position { X = 1, Y = 2 });

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

#endif

// Output:
//  - OnSet: Position: e: self: (10, 20) parent: (1, 2)
