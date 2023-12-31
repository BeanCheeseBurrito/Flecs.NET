// When a prefab has children, they are instantiated for an instance when the
// IsA relationship to the prefab is added.

using Flecs.NET.Core;

public static class Cpp_Prefabs_Hierarchy
{
    public static void Main()
    {
        using World world = World.Create();

        // Create a prefab hierarchy.
        Entity spaceShip = world.Prefab("SpaceShip");
        world.Prefab("Engine").ChildOf(spaceShip);
        world.Prefab("Cockpit").ChildOf(spaceShip);

        // Instantiate the prefab. This also creates an Engine and Cockpit child
        // for the instance.
        Entity inst = world.Entity("MySpaceship").IsA(spaceShip);
        Entity instEngine = inst.Lookup("Engine");
        Entity instCockpit = inst.Lookup("Cockpit");

        Console.WriteLine($"Instance Engine: {instEngine.Path()}");
        Console.WriteLine($"Instance Cockpit: {instCockpit.Path()}");
    }
}

// Output:
// Instance Engine: MySpaceship.Engine
// Instance Cockpit: MySpaceship.Cockpit
