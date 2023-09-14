// Prefabs are entities that can be used as templates for other entities. They
// are created with a builtin Prefab tag, which by default excludes them from
// queries and systems.
//
// Prefab instances are entities that have an IsA relationship to the prefab.
// The IsA relationship causes instances to inherit the components from the
// prefab. By default all instances for a prefab share its components.
//
// Inherited components save memory as they only need to be stored once for all
// prefab instances. They also speed up the creation of prefabs, as inherited
// components don't need to be copied to the instances.
//
// To get a private copy of a component, an instance can add it which is called
// an override. Overrides can be manual (by using add) or automatic (see the
// AutoOverride example).
//
// If a prefab has children, adding the IsA relationship instantiates the prefab
// children for the instance (see hierarchy example).

#if Cpp_Prefabs_Basics

using Flecs.NET.Core;

using World world = World.Create();

// Create a SpaceShip prefab with a Defense component.
Entity spaceShip = world.Prefab("SpaceShip")
    .Set(new Defense { Value = 50 });

// Create a prefab instance
Entity inst = world.Entity("my_spaceship").IsA(spaceShip);

// Because of the IsA relationship, the instance now shares the Defense
// component with the prefab, and can be retrieved as a regular component:
ref readonly Defense dInstance = ref inst.Get<Defense>();
Console.WriteLine($"Defense: {dInstance.Value}");

// Because the component is shared, changing the value on the prefab will
// also change the value for the instance:
spaceShip.Set(new Defense { Value = 100 });
Console.WriteLine($"Defense after set: {dInstance.Value}");

// Prefab components can be iterated like regular components:
world.Each((Entity e, ref Defense d) =>
{
    Console.WriteLine($"{e.Path()}: {d.Value}");
});

public struct Defense
{
    public double Value { get; set; }
}

#endif

// Output:
// Defense: 50
// Defense after set: 100
// my_spaceship: 100
