// When an entity is instantiated from a prefab, components are by default
// copied from the prefab to the instance. This behavior can be customized with
// the OnInstantiate trait, which has three options:
//
// - Override (copy to instance)
// - Inherit (inherit from prefab)
// - DontInherit (don't copy or inherit)
//
// When a component is inheritable, it can be overridden manually by adding the
// component to the instance, which also copies the value from the prefab
// component. Additionally, when creating a prefab it is possible to flag a
// component as "auto override", which can change the behavior for a specific
// prefab from "inherit" to "override".
//
// This example shows how these different features can be used.

using Flecs.NET.Core;

// Components
file record struct Attack(float Value);
file record struct Defense(float Value);
file record struct Damage(float Value);

public static class Cpp_Prefabs_Override
{
    public static void Main()
    {
        using World world = World.Create();

        // Change the instantiation behavior for Attack and Defense to inherit.
        world.Component<Attack>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);
        world.Component<Defense>().Entity.Add(Ecs.OnInstantiate, Ecs.Inherit);

        // Attack and Damage are properties that can be shared across many
        // spaceships. This saves memory, and speeds up prefab creation as we don't
        // have to copy the values of Attack and Defense to private components.
        Entity spaceShip = world.Prefab("SpaceShip")
            .Set<Attack>(new(75))
            .Set<Defense>(new(100))
            .Set<Damage>(new(50));

        // Create a prefab instance.
        Entity inst = world.Entity("MySpaceship").IsA(spaceShip);

        // The entity will now have a private copy of the Damage component, but not
        // of the Attack and Defense components. We can see this when we look at the
        // type of the instance:
        Console.WriteLine(inst.Type());

        // Even though Attack was not automatically overridden, we can always
        // override it manually afterwards by adding it:
        inst.Add<Attack>();

        // The Attack component now shows up in the entity type:
        Console.WriteLine(inst.Type());

        // We can get all components on the instance, regardless of whether they
        // are overridden or not. Note that the overridden components (Attack and
        // Damage) are initialized with the values from the prefab component:
        ref readonly Attack a = ref inst.Get<Attack>();
        ref readonly Defense d = ref inst.Get<Defense>();
        ref readonly Damage dmg = ref inst.Get<Damage>();

        Console.WriteLine("Attack: " + a.Value);
        Console.WriteLine("Defense: " + d.Value);
        Console.WriteLine("Damage: " + dmg.Value);
    }
}

// Output:
// Damage, (Identifier,Name), (IsA,SpaceShip)
// Attack, Damage, (Identifier,Name), (IsA,SpaceShip)
// Attack: 75
// Defense: 100
// Damage: 50
