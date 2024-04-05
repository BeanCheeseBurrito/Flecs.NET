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

public static class Cpp_Relationships_Union
{
    public static void Main()
    {
        using World world = World.Create();

        // TODO: RIP unions
        // world.Component<Movement>().Entity.Add(Ecs.Union);
        // world.Component<Direction>().Entity.Add(Ecs.Union);

        // Create a query that subscribes for all entities that have a Direction
        // and that are walking
        Query q = world.QueryBuilder()
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
        q.Iter((Iter it) =>
        {
            // Get the column with direction states. This is stored as an array
            // with identifiers to the individual states
            Field<ulong> movement = it.Field<ulong>(1);
            Field<ulong> direction = it.Field<ulong>(2);

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
    }
}

// Output:
// e3: Movement: Walking, Direction: Back
// e1: Movement: Walking, Direction: Front
