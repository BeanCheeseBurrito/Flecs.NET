// Overriding makes it possible for a prefab instance to obtain a private copy
// of an inherited component. To override a component the regular add operation
// is used. The overridden component will be initialized with the value of the
// inherited component.
//
// In some cases a prefab instance should always have a private copy of an
// inherited component. This can be achieved with an auto override which can be
// added to a prefab. Components with an auto override are automatically
// overridden when the prefab is instantiated.

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

        // Attack and Damage are properties that can be shared across many
        // spaceships. This saves memory, and speeds up prefab creation as we don't
        // have to copy the values of Attack and Defense to private components.
        Entity spaceShip = world.Prefab("SpaceShip")
            .Set<Attack>(new(75))
            .Set<Defense>(new(100));

        // Damage is a property that is private to a spaceship, so add an auto
        // override for it. This ensures that each prefab instance will have a
        // private copy of the component.
        spaceShip.SetOverride<Damage>(new(0));

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
// Damage: 0
