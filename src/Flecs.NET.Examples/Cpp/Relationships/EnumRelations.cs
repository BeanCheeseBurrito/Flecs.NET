// When an enumeration constant is added to an entity, it is added as a relationship
// pair where the relationship is the enum type, and the target is the constant. For
// example, this statement:
//   e.Add(Color.Red)
//
// adds this relationship:
//   (Color, Color.Red)
//
// Enums are registered as exclusive relationships, which means that adding an
// enum constant will replace the previous constant for that enumeration:
//   e.Add(Color.Green)
//
//  will replace Color.Red with Color.Green

using Flecs.NET.Core;

// Enums
file enum Tile
{
    Grass,
    Sand,
    Stone
}

file enum TileStatus
{
    Free,
    Occupied,
}

public static class Cpp_Relationships_EnumRelations
{
    public static void Main()
    {
        using World world = World.Create();

        // Create an entity with (Tile, Stone) and (TileStatus, Free) relationships
        Entity tile = world.Entity()
            .Add(Tile.Stone)
            .Add(TileStatus.Free);

        // (Tile, Tile.Stone), (TileStatus, TileStatus.Free)
        Console.WriteLine(tile.Type().Str());

        // Replace (TileStatus, Free) with (TileStatus, Occupied)
        tile.Add(TileStatus.Occupied);

        // (Tile, Tile.Stone), (TileStatus, TileStatus.Occupied)
        Console.WriteLine(tile.Type().Str());

        // Check if the entity has the Tile relationship and the Tile.Stone pair
        Console.WriteLine(tile.Has<Tile>()); // True
        Console.WriteLine(tile.Has(Tile.Stone)); // True

        // Get the current value of the enum
        ref readonly Tile v = ref tile.Get<Tile>();
        Console.WriteLine(v == Tile.Stone); // True

        // Create a few more entities that we can query
        world.Entity().Add(Tile.Grass).Add(TileStatus.Free);
        world.Entity().Add(Tile.Sand).Add(TileStatus.Occupied);

        // Iterate all entities with a Tile relationship
        using Query q1 = world.QueryBuilder()
            .With<Tile>(Ecs.Wildcard)
            .Build();

        q1.Each((Iter it, int i) =>
        {
            Entity tileConstant = it.Pair(1).Second();
            Console.WriteLine(tileConstant.Path());
        });

        // Outputs:
        //  Tile.Stone
        //  Tile.Grass
        //  Tile.Sand

        // Iterate only occupied tiles
        using Query q2 = world.QueryBuilder()
            .With<Tile>(Ecs.Wildcard)
            .With(TileStatus.Occupied)
            .Build();

        q2.Each((Iter it, int i) =>
        {
            Entity tileConstant = it.Pair(1).Second();
            Console.WriteLine(tileConstant.Path());
        });

        // Outputs:
        //  Tile.Stone
        //  Tile.Sand

        // Remove any instance of the TileStatus relationship
        tile.Remove<TileStatus>();
    }
}

// Output:
// (Tile,Tile.Stone), (TileStatus,TileStatus.Free)
// (Tile,Tile.Stone), (TileStatus,TileStatus.Occupied)
// True
// True
// True
// Tile.Stone
// Tile.Grass
// Tile.Sand
// Tile.Stone
// Tile.Sand
