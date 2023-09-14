// Applications can pass context data to a system. A common use case where this
// comes in handy is when a system needs to iterate more than one query. The
// following example shows how to pass a custom query into a system for a simple
// collision detection example.

#if Cpp_Systems_SystemCtx

using Flecs.NET.Core;

unsafe
{
    double Sqr(double value)
    {
        return value * value;
    }

    double DistanceSqr(Position p1, Position p2)
    {
        return Sqr(p2.X - p1.X) + Sqr(p2.Y - p1.Y);
    }

    double Rand(int max)
    {
        return Random.Shared.NextDouble() % max;
    }

    using World world = World.Create();

    Query qCollide = world.Query(
        filter: world.FilterBuilder()
            .With<Position>()
            .With<Radius>()
    );

    Routine routine = world.Routine(
        name: "Collide",
        filter: world.FilterBuilder()
            .With<Position>()
            .With<Radius>(),
        routine: world.RoutineBuilder().Ctx(&qCollide),
        callback: (Iter it, int i1) =>
        {
            Column<Position> p1 = it.Field<Position>(1);
            Column<Radius> r1 = it.Field<Radius>(2);

            ref Query q = ref it.Ctx<Query>();
            Entity e1 = it.Entity(i1);

            q.Each((Iter it, int i2) =>
            {
                Column<Position> p2 = it.Field<Position>(1);
                Column<Radius> r2 = it.Field<Radius>(2);

                Entity e2 = it.Entity(i2);

                // don't collide with self
                if (e1 == e2)
                    return;

                // Simple trick to prevent collisions from being detected
                // twice with the entities reversed.
                if (e1 > e2)
                    return;

                // Check for collision
                double dSqr = DistanceSqr(p1[i1], p2[i2]);
                double rSqr = Sqr(r1[i1].Value + r2[i2].Value);

                if (rSqr > dSqr)
                    Console.WriteLine($"{e1} and {e2} collided!");
            });
        }
    );

    // Create a few test entities
    for (int i = 0; i < 10; i ++) {
        world.Entity()
            .Set(new Position { X = Rand(100), Y = Rand(100) })
            .Set(new Radius { Value = Rand(10) + 1 });
    }

    // Run the system
    routine.Run();
}

public struct Position
{
    public double X { get; set; }
    public double Y { get; set; }
}

public struct Radius
{
    public double Value { get; set; }
}

#endif

// Output:
// 534 and 535 collided!
// 534 and 536 collided!
// 534 and 537 collided!
// 534 and 538 collided!
// 534 and 539 collided!
// ...
