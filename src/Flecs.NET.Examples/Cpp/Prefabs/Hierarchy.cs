// When a prefab has children, they are instantiated for an instance when the
// IsA relationship to the prefab is added.

#if Cpp_Prefabs_Hierarchy

using Flecs.NET.Core;

using World world = World.Create();

// Create a prefab hierarchy.
Entity spaceShip = world.Prefab("SpaceShip");
world.Prefab("Engine"). ChildOf(spaceShip);
world.Prefab("Cockpit").ChildOf(spaceShip);

// Instantiate the prefab. This also creates an Engine and Cockpit child
// for the instance.
Entity inst = world.Entity("my_spaceship").IsA(spaceShip);
Entity instEngine = inst.Lookup("Engine");
Entity instCockpit = inst.Lookup("Cockpit");

Console.WriteLine($"Instance Engine: {instEngine.Path()}");
Console.WriteLine($"Instance Cockpit: {instCockpit.Path()}");

#endif

// Output:
// Instance Engine: my_spaceship.Engine
// Instance Cockpit: my_spaceship.Cockpit
