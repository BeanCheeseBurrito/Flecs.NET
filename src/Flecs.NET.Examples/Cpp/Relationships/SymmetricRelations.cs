#if Cpp_Relationships_SymmetricRelations

using Flecs.NET.Core;

using World world = World.Create(args);

// Register TradesWith as symmetric relationship. Symmetric relationships
// go both ways, adding (R, B) to A will also add (R, A) to B.
world.Component<TradesWith>().Entity.Add(Ecs.Symmetric);

// Create two players
Entity player1 = world.Entity();
Entity player2 = world.Entity();

// Add (TradesWith, player2) to player1. This also adds
// (TradesWith, player1) to player2.
player1.Add<TradesWith>(player2);

// Log platoon of unit
Console.WriteLine($"Player 1 trades with Player 2: {player1.Has<TradesWith>(player2)}");
Console.WriteLine($"Player 2 trades with Player 1: {player2.Has<TradesWith>(player1)}");

// Type for TradesWith relationship
public struct TradesWith { }

#endif

// Output:
// Player 1 trades with Player 2: True
// Player 2 trades with Player 1: True
