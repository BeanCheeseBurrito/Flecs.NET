using Flecs.NET.Core;

// Components
file record struct Position(float X, float Y);

public static class Queries_FindEntity
{
    public static void Main()
    {
        using World world = World.Create();

        world.Entity("e1").Set(new Position(10, 20));
        world.Entity("e2").Set(new Position(20, 30));

        // Create a simple query for component Position
        using Query<Position> q = world.Query<Position>();

        // Find the entity for which Position.x is 20
        Entity e = q.Find((ref Position p) => p.X == 20);

        if (e != 0)
            Console.WriteLine($"Found entity {e.Path()}");
        else
            Console.WriteLine($"No entity found");
    }
}

// Output:
// Found entity e2
