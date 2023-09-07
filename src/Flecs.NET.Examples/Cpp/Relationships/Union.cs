// This example shows how to use union relationships. Union relationships behave
// much like exclusive relationships in that entities can have only one instance
// and that adding an instance removes the previous instance.
//
// What makes union relationships stand out is that changing the relationship
// target doesn't change the archetype of an entity. This allows for quick
// switching of tags, which can be useful when encoding state machines in ECS.
//
// There is a tradeoff, and that is that because a single archetype can contain
// entities with multiple targets, queries need to do a bit of extra work to
// only return the requested target.
//
// This code uses enumeration relationships. See the enum_relations example for
// more details.

#if Cpp_Relationships_Union

using Flecs.NET.Core;
using static Flecs.NET.Bindings.Native;

using World world = World.Create(args);

world.Component<Movement>().Entity.Add(EcsUnion);
world.Component<Direction>().Entity.Add(EcsUnion);

// Create a query that subscribes for all entities that have a Direction
// and that are walking
Query q = world.Query(
    filter: world.FilterBuilder()
        .With(Movement.Walking)
        .With<Direction>(EcsWildcard)
);

// Create a few entities with various state combinations
world.Entity("e1")
    .Add(Movement.Walking)
    .Add(Direction.Front);

world.Entity("e2")
    .Add(Movement.Running)
    .Add(Direction.Left);

Entity e3 = world.Entity("e3")
    .Add(Movement.Running)
    .Add(Direction.Back);

// Add Walking to e3. This will remove the Running case
e3.Add(Movement.Walking);

// Iterate the query
q.Iter((Iter it) =>
{
    // Get the column with direction states. This is stored as an array
    // with identifiers to the individual states
    Column<ulong> movement = it.Field<ulong>(1);
    Column<ulong> direction = it.Field<ulong>(2);

    foreach (int i in it)
    {
        // Movement will always be Walking, Direction can be any state
        Console.Write(it.Entity(i).Name());
        Console.Write(": Movement: ");
        Console.Write(it.World().GetAlive(movement[i]).Name());
        Console.Write(", Direction: ");
        Console.Write(it.World().GetAlive(direction[i]).Name());
        Console.WriteLine();
    }
});

public enum Movement
{
    Walking,
    Running
}

public enum Direction
{
    Front,
    Back,
    Left,
    Right
}

#endif

// Output:
// e3: Movement: Walking, Direction: Back
// e1: Movement: Walking, Direction: Front
