// This example shows one possible way to implement scene management
// using pipelines.

#if Cpp_GameMechanics_SceneManagement

using Flecs.NET.Core;

// Removes all entities who are children of
// the current scene root.
// (NOTE: should use DeferBegin() / DeferEnd())
void ResetScene(World world)
{
    world.DeleteWith(Ecs.ChildOf, world.Entity<SceneRoot>());
}

void MenuScene(Iter it, int i)
{
    Console.WriteLine("\n>> ActiveScene has changed to `MenuScene`\n");

    World world = it.World();
    Component<SceneRoot> scene = world.Component<SceneRoot>();

    ResetScene(world);

    // Creates a start menu button
    // when we enter the menu scene.
    world.Entity("Start Button")
        .Set(new Button { Text = "Play the Game!" })
        .Set(new Position { X = 50, Y = 50 })
        .ChildOf(scene);

    world.SetPipeline(world.Get<MenuScene>().Pipeline);
}

void GameScene(Iter it, int i)
{
    Console.WriteLine("\n>> ActiveScene has changed to `GameScene`\n");

    World world = it.World();
    Component<SceneRoot> scene = world.Component<SceneRoot>();

    ResetScene(world);

    // Creates a player character
    // when we enter the game scene.
    world.Entity("Player")
        .Set(new Character { })
        .Set(new Health { Amount = 2 })
        .Set(new Position { X = 0, Y = 0 })
        .ChildOf(scene);

    world.SetPipeline(world.Get<GameScene>().Pipeline);
}

void InitScenes(World world)
{
    // Can only have one active scene
    // in a game at a time.
    world.Component<ActiveScene>().Entity
        .Add(Ecs.Exclusive);

    // Each scene gets a pipeline that
    // runs the associated systems plus
    // all other scene-agnostic systems.
    Pipeline menu = world.Pipeline(
        filter: world.FilterBuilder()
            .With(Ecs.System)
            .Without<GameScene>() // Use "Without()" of the other scenes
                                  // so that we can run every system that
                                  // doesn't have a scene attached to it.
    );

    Pipeline game = world.Pipeline(
        filter: world.FilterBuilder()
            .With(Ecs.System)
            .Without<MenuScene>()
    );

    // Set pipeline entities on the scenes
    // to easily find them later with Get().
    world.Set(new MenuScene { Pipeline = menu.Entity });
    world.Set(new GameScene { Pipeline = game.Entity });

    // Observer to call scene change logic for
    // MenuScene when added to the ActiveScene.
    world.Observer(
        name: "Scene Change to Menu",
        filter: world.FilterBuilder().With<ActiveScene, MenuScene>(),
        observer: world.ObserverBuilder().Event(Ecs.OnAdd),
        callback: MenuScene
    );

    // Observer to call scene change logic for
    // GameScene when added to the ActiveScene.
    world.Observer(
        name: "Scene Change to Game",
        filter: world.FilterBuilder().With<ActiveScene, GameScene>(),
        observer: world.ObserverBuilder().Event(Ecs.OnAdd),
        callback: GameScene
    );
}

void InitSystems(World world)
{
    // Will run every time regardless of the
    // current scene we're in.
    world.Routine(
        name: "Print Position",
        filter: world.FilterBuilder().With<Position>(),
        callback: (Iter it, int i) =>
        {
            Column<Position> p = it.Field<Position>(1);

            // Prints out the position of the
            // entity.
            Console.WriteLine($"{it.Entity(i)}: ({p[i].X}, {p[i].Y})");
        }
    );

    // Will only run when the game scene is
    // currently active.
    world.Routine(
        name: "Characters Lose Health",
        filter: world.FilterBuilder().With<Health>(),
        routine: world.RoutineBuilder().Kind<GameScene>(),
        callback: (Iter it, int i) =>
        {
            Column<Health> h = it.Field<Health>(1);

            // Prints out the character's health
            // and then decrements it by one.
            Console.WriteLine($"{h[i].Amount} health remaining");
            h[i].Amount--;
        }
    );

    // Will only run when the menu scene is
    // currently active.
    world.Routine(
        name: "Print Menu Button Text",
        filter: world.FilterBuilder().With<Button>(),
        routine: world.RoutineBuilder().Kind<MenuScene>(),
        callback: (Iter it, int i) =>
        {
            Column<Button> b = it.Field<Button>(1);

            // Prints out the text of the menu
            // button.
            Console.WriteLine($"Button says \"{b[i].Text}\"");
        }
    );
}

using World ecs = World.Create();

InitScenes(ecs);
InitSystems(ecs);

ecs.Add<ActiveScene, MenuScene>();
ecs.Progress();

ecs.Add<ActiveScene, GameScene>();
ecs.Progress();
ecs.Progress();
ecs.Progress();

ecs.Add<ActiveScene, MenuScene>();
ecs.Progress();

ecs.Add<ActiveScene, GameScene>();
ecs.Progress();
ecs.Progress();
ecs.Progress();

// Scene relationships/tags
public struct ActiveScene { } // Represents the current scene
public struct SceneRoot { }   // Parent for all entities unique to the scene

// Scenes
public struct MenuScene
{
    public Entity Pipeline { get; set; }
}

public struct GameScene
{
    public Entity Pipeline { get; set; }
}

// Components for Example
public struct Position
{
    public float X { get; set; }
    public float Y { get; set; }
}

public struct Button
{
    public string Text { get; set; }
}

public struct Character
{
    public bool Alive { get; set; }
}

public struct Health
{
    public int Amount { get; set; }
}

#endif

// Output:
// >> ActiveScene has changed to `MenuScene`
//
// Start Button: (50, 50)
// Button says "Play the Game!"
//
// >> ActiveScene has changed to `GameScene`
//
// Player: (0, 0)
// 2 health remaining
// Player: (0, 0)
// 1 health remaining
// Player: (0, 0)
// 0 health remaining
//
// >> ActiveScene has changed to `MenuScene`
//
// Start Button: (50, 50)
// Button says "Play the Game!"
//
// >> ActiveScene has changed to `GameScene`
//
// Player: (0, 0)
// 2 health remaining
// Player: (0, 0)
// 1 health remaining
// Player: (0, 0)
// 0 health remaining
