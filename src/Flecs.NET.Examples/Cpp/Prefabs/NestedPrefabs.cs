// Nested prefabs make it possible to reuse an existing prefab inside another
// prefab. An example of where this could be useful is a car with four wheels:
// instead of defining four times what a wheel is a Car prefab can reference an
// existing Wheel prefab.
//
// Nested prefabs can be created by adding a child that is a variant (inherits
// from) another prefab. For more information on variants, see the variants
// example.
//
// Instantiated children from a nested prefab still inherit from the original
// prefab. The reason for this is that an instantiated child is an exact copy
// of the prefab child, and the prefab child only has an IsA relationship to the
// nested prefab.
//
// This example shows how auto overriding (see the auto override example) can be
// used to give instantiated children from a nested prefab a private copy of an
// inherited component.

#if Cpp_Prefabs_NestedPrefabs

using Flecs.NET.Core;

using World world = World.Create();

// Create a Wheel prefab, make sure each instantiated wheel has a private
// copy of the TirePressure component.
Entity wheel = world.Prefab("Wheel")
    .SetOverride(new TirePressure { Value = 32 });

// Create a Car prefab with four wheels. Note how we're using the scope
// method, which has the same effect as adding the (ChildOf, Car) pair.
Entity car = world.Prefab("Car").Scope(() =>
{
    world.Prefab("FrontLeft").IsA(wheel);
    world.Prefab("FrontRight").IsA(wheel);
    world.Prefab("BackLeft").IsA(wheel);
    world.Prefab("BackRight").IsA(wheel);
});

// Create a prefab instance.
Entity inst = world.Entity("my_car").IsA(car);

// Lookup one of the wheels
Entity instFrontLeft = inst.Lookup("FrontLeft");

// The type shows that the child has a private copy of the TirePressure
// component, and an IsA relationship to the Wheel prefab.
Console.WriteLine($"Type: [{instFrontLeft.Type().Str()}]");

// Get the TirePressure component & print its value
ref readonly TirePressure p = ref instFrontLeft.Get<TirePressure>();
Console.WriteLine($"Pressure: " + p.Value);

public struct TirePressure
{
    public double Value { get; set; }
}

#endif

// Output:
// Type: [TirePressure, (Identifier,Name), (ChildOf,my_car), (IsA,Wheel)]
// Pressure: 32
