// Slots can be combined with prefab hierarchies to make it easier to access
// the child entities created for an instance.
//
// To create a slot, the SlotOf relationship is added to the child of a prefab,
// with as relationship target the prefab for which to register the slot. When
// the prefab is instantiated, each slot will be added as a relationship pair
// to the instance that looks like this:
//   (PrefabChild, InstanceChild)
//
// For a SpaceShip prefab and an Engine child, that pair would look like this:
//   (SpaceShip.Engine, Instance.Engine)
//
// To get the entity for a slot, an application can use the regular functions
// to inspect relationships and relationship targets (see code).
//
// Slots can be added to any level of a prefab hierarchy, as long as it is above
// (a parent of) the slot itself. When the prefab tree is instantiated, the
// slots are added to the entities that correspond with the prefab children.
//
// Without slots, an application would have to rely on manually looking up
// entities by name to get access to the instantiated children, like what the
// hierarchy example does.


#if Cpp_Prefabs_Slots

using Flecs.NET.Core;

using World world = World.Create();

// Create the same prefab hierarchy as from the hierarchy example, but now
// with the SlotOf relationship.
Entity spaceShip = world.Prefab("SpaceShip");
    Entity engine = world.Prefab("Engine")
        .ChildOf(spaceShip)
        .SlotOf(spaceShip);

    Entity cockpit = world.Prefab("Cockpit")
        .ChildOf(spaceShip)
        .SlotOf(spaceShip);

// Add an additional child to the Cockpit prefab to demonstrate how
// slots can be different from the parent. This slot could have been
// added to the Cockpit prefab, but instead we register it on the top
// level SpaceShip prefab.
Entity pilotSeat = world.Prefab("PilotSeat")
    .ChildOf(cockpit)
    .SlotOf(spaceShip);

// Create a prefab instance.
Entity inst = world.Entity("my_spaceship").IsA(spaceShip);

// Get the instantiated entities for the prefab slots
Entity instEngine = inst.Target(engine);
Entity instCockpit = inst.Target(cockpit);
Entity instSeat = inst.Target(pilotSeat);

Console.WriteLine("Instance Engine:  " + instEngine.Path());
Console.WriteLine("Instance Cockpit: " + instCockpit.Path());
Console.WriteLine("Instance Seat:    " + instSeat.Path());

#endif

// Output:
// Instance Engine:  ::my_spaceship::Engine
// Instance Cockpit: ::my_spaceship::Cockpit
// Instance Seat:    ::my_spaceship::Cockpit::PilotSeat
