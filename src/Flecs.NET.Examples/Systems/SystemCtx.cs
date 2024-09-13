// Applications can pass context data to a system. A common use case where this
// comes in handy is when a system needs to iterate more than one query. The
// following example shows how to pass a custom query into a system for a simple
// collision detection example.

using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);
file record struct Radius(float Value);

file static unsafe class Systems_SystemCtx
{
    public static void Main()
    {
        using World world = World.Create();

        using Query<Position, Radius> qCollide = world.Query<Position, Radius>();

        System<Position, Radius> system = world.System<Position, Radius>("Collide")
            .Ctx(&qCollide)
            .Each((Iter it, int i, ref Position p, ref Radius r) =>
            {
                ref Query<Position, Radius> q = ref it.Ctx<Query<Position, Radius>>();
                Entity e1 = it.Entity(i);

                Position p1 = p;
                Radius r1 = r;

                q.Each((Entity e2, ref Position p2, ref Radius r2) =>
                {
                    // don't collide with self
                    if (e1 == e2)
                        return;

                    // Simple trick to prevent collisions from being detected
                    // twice with the entities reversed.
                    if (e1 > e2)
                        return;

                    // Check for collision
                    double dSqr = DistanceSqr(p1, p2);
                    double rSqr = Sqr(r1.Value + r2.Value);

                    if (rSqr > dSqr)
                        Console.WriteLine($"{e1} and {e2} collided!");
                });
            });

        // Create a few test entities
        for (int i = 0; i < 10; i++)
        {
            world.Entity()
                .Set(new Position(Rand(100), Rand(100)))
                .Set(new Radius(Rand(10) + 1));
        }

        // Run the system
        system.Run();
    }

    private static float Sqr(float value)
    {
        return value * value;
    }

    private static float DistanceSqr(Position p1, Position p2)
    {
        return Sqr(p2.X - p1.X) + Sqr(p2.Y - p1.Y);
    }

    private static float Rand(int max)
    {
        return Random.Shared.NextSingle() % max;
    }
}

// Output:
// 534 and 535 collided!
// 534 and 536 collided!
// 534 and 537 collided!
// 534 and 538 collided!
// 534 and 539 collided!
// ...
