// This example shows one possible way to implement scene management
// using pipelines.

using Flecs.NET.Core;

// Scene relationships/tags
file struct ActiveScene; // Represents the current scene
file struct SceneRoot;   // Parent for all entities unique to the scene

// Scenes
file record struct MenuScene(Pipeline Pipeline);
file record struct GameScene(Pipeline Pipeline);

// Components for Example
file record struct Position(float X, float Y);
file record struct Button(string Text);
file record struct Character(bool Alive);
file record struct Health(int Amount);

file static class Cpp_GameMechanics_SceneManagement
{
    public static void Main()
    {
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
    }

    // Removes all entities who are children of
    // the current scene root.
    // (NOTE: should use DeferBegin() / DeferEnd())
    static void ResetScene(World world)
    {
        world.DeleteWith(Ecs.ChildOf, world.Entity<SceneRoot>());
    }

    static void MenuScene(Iter it, int i)
    {
        Console.WriteLine("\n>> ActiveScene has changed to `MenuScene`\n");

        World world = it.World();
        Component<SceneRoot> scene = world.Component<SceneRoot>();

        ResetScene(world);

        // Creates a start menu button
        // when we enter the menu scene.
        world.Entity("Start Button")
            .Set(new Button("Play the Game!"))
            .Set(new Position(50, 50))
            .ChildOf(scene);

        world.SetPipeline(world.Get<MenuScene>().Pipeline);
    }

    static void GameScene(Iter it, int i)
    {
        Console.WriteLine("\n>> ActiveScene has changed to `GameScene`\n");

        World world = it.World();
        Component<SceneRoot> scene = world.Component<SceneRoot>();

        ResetScene(world);

        // Creates a player character
        // when we enter the game scene.
        world.Entity("Player")
            .Set(default(Character))
            .Set(default(Position))
            .Set(new Health(2))
            .ChildOf(scene);

        world.SetPipeline(world.Get<GameScene>().Pipeline);
    }

    static void InitScenes(World world)
    {
        // Can only have one active scene
        // in a game at a time.
        world.Component<ActiveScene>().Entity
            .Add(Ecs.Exclusive);

        // Each scene gets a pipeline that
        // runs the associated systems plus
        // all other scene-agnostic systems.
        Pipeline menu = world.Pipeline()
            .With(Ecs.System)
            .Without<GameScene>() // Use "Without()" of the other scenes
                                    // so that we can run every system that
                                    // doesn't have a scene attached to it.
            .Build();

        Pipeline game = world.Pipeline()
            .With(Ecs.System)
            .Without<MenuScene>()
            .Build();

        // Set pipeline entities on the scenes
        // to easily find them later with Get().
        world.Set(new MenuScene(menu));
        world.Set(new GameScene(game));

        // Observer to call scene change logic for
        // MenuScene when added to the ActiveScene.
        world.Observer<ActiveScene>("Scene Change to Menu")
            .TermAt(0).Second<MenuScene>()
            .Event(Ecs.OnAdd)
            .Each(MenuScene);

        // Observer to call scene change logic for
        // GameScene when added to the ActiveScene.
        world.Observer<ActiveScene>("Scene Change to Game")
            .TermAt(0).Second<GameScene>()
            .Event(Ecs.OnAdd)
            .Each(GameScene);
    }

    static void InitSystems(World world)
    {
        // Will run every time regardless of the
        // current scene we're in.
        world.Routine<Position>("Print Position")
            .Each((Entity e, ref Position p) =>
            {
                Console.WriteLine($"{e}: ({p.X}, {p.Y})");
            });

        // Will only run when the game scene is
        // currently active.
        world.Routine<Health>("Characters Lose Health")
            .Kind<GameScene>()
            .Each((ref Health h) =>
            {
                // Prints out the character's health
                // and then decrements it by one.
                Console.WriteLine($"{h.Amount} health remaining");
                h.Amount--;
            });


        // Will only run when the menu scene is
        // currently active.
        world.Routine<Button>("Print Menu Button Text")
            .Kind<MenuScene>()
            .Each((ref Button b) =>
            {
                // Prints out the text of the menu
                // button.
                Console.WriteLine($"Button says \"{b.Text}\"");
            });
    }
}

// Output:
// >> ActiveScene has changed to `MenuScene`
//
// SceneRoot.Start Button: (50, 50)
// Button says "Play the Game!"
//
// >> ActiveScene has changed to `GameScene`
//
// SceneRoot.Player: (0, 0)
// 2 health remaining
// SceneRoot.Player: (0, 0)
// 1 health remaining
// SceneRoot.Player: (0, 0)
// 0 health remaining
//
// >> ActiveScene has changed to `MenuScene`
//
// SceneRoot.Start Button: (50, 50)
// Button says "Play the Game!"
//
// >> ActiveScene has changed to `GameScene`
//
// SceneRoot.Player: (0, 0)
// 2 health remaining
// SceneRoot.Player: (0, 0)
// 1 health remaining
// SceneRoot.Player: (0, 0)
// 0 health remaining
