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

#if Cpp_Relationships_EnumRelations

using Flecs.NET.Core;

using World world = World.Create(args);

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
ref readonly Tile v = ref tile.GetEnum<Tile>();
Console.WriteLine(v == Tile.Stone); // True

// Create a few more entities that we can query
world.Entity().Add(Tile.Grass).Add(TileStatus.Free);
world.Entity().Add(Tile.Sand).Add(TileStatus.Occupied);

// Iterate all entities with a Tile relationship
using Filter filter1 = world.Filter(
    filter: world.FilterBuilder().With<Tile>(Ecs.Wildcard)
);

filter1.Each((Iter it, int i) =>
{
    Entity tileConstant = it.Pair(1).Second();
    Console.WriteLine(tileConstant.Path());
});

// Outputs:
//  Tile.Stone
//  Tile.Grass
//  Tile.Sand

// Iterate only occupied tiles
using Filter filter2 = world.Filter(
    filter: world.FilterBuilder()
        .With<Tile>(Ecs.Wildcard)
        .With(TileStatus.Occupied)
);

filter2.Each((Iter it, int i) =>
{
    Entity tileConstant = it.Pair(1).Second();
    Console.WriteLine(tileConstant.Path());
});

// Outputs:
//  Tile.Stone
//  Tile.Sand

// Remove any instance of the TileStatus relationship
tile.Remove<TileStatus>();

public enum Tile
{
    Grass,
    Sand,
    Stone
}

public enum TileStatus
{
    Free,
    Occupied,
}

#endif

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
