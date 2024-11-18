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
// This code uses enumeration relationships. See the EnumRelations example for
// more details.

using Flecs.NET.Core;

// Enums
file enum Movement
{
    Walking,
    Running
}

file enum Direction
{
    Front,
    Back,
    Left,
    Right
}

public static class Relationships_Union
{
    public static void Main()
    {
        using World world = World.Create();

        world.Component<Movement>().Add(Ecs.Union);
        world.Component<Direction>().Add(Ecs.Union);

        // Create a query that subscribes for all entities that have a Direction
        // and that are walking
        using Query q = world.QueryBuilder()
            .With(Movement.Walking)
            .With<Direction>(Ecs.Wildcard)
            .Build();

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
        q.Each((Iter it, int i) =>
        {
            // Movement will always be Walking, Direction can be any state
            Console.Write(it.Entity(i).Name());
            Console.Write(": Movement: ");
            Console.Write(it.Pair(0).Second().Name());
            Console.Write(", Direction: ");
            Console.Write(it.Pair(1).Second().Name());
            Console.WriteLine();
        });
    }
}

// Output:
// e3: Movement: Walking, Direction: Back
// e1: Movement: Walking, Direction: Front
