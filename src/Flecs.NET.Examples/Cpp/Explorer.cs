#if Cpp_Explorer

using Flecs.NET.Core;

// Passing in the command line arguments will allow the explorer to display
// the application name.
using World world = World.Create(args);

world.Import<Ecs.Units>();
world.Import<Ecs.Monitor>(); // Collect statistics periodically

// Mass component
world.Component<Mass>()
    .Member<double, Ecs.Units.Masses.KiloGrams>("Value");

// Simple hierarchy
Entity sun = world.Entity("Sun")
    .Set(new Mass { Value = 1.988500e31 });

Entity earth = world.Entity("Earth")
    .ChildOf(sun)
    .Set(new Mass { Value = 5.9722e24 });

world.Entity("Moon")
    .ChildOf(earth)
    .Set(new Mass { Value = 7.34767309e22 });

// Run application with REST interface. When the application is running,
// navigate to https://flecs.dev/explorer to inspect it!
//
// See docs/RestApi.md#explorer for more information.
return world.App().EnableRest().Run();

// Alternatively you can run your own loop with World.Progress()
// world.Set<EcsRest>(default);
// while (world.Progress()) { }

public struct Mass
{
    public double Value { get; set; }
}

#endif
