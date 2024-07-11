using Flecs.NET.Bindings;
using Flecs.NET.Core;

// Components
file record struct Mass(double Value);

public static class Cpp_Explorer
{
    public static void Main(string[] args)
    {
        // Passing in the command line arguments will allow the explorer to display
        // the application name.
        using World world = World.Create(args);

        world.Import<Ecs.Units>();
        world.Import<Ecs.Stats>(); // Collect statistics periodically

        // Mass component
        world.Component<Mass>()
            .Member<double, Ecs.Units.Masses.KiloGrams>("Value");

        // Simple hierarchy
        Entity sun = world.Entity("Sun")
            .Set(new Mass(1.988500e31));

        Entity earth = world.Entity("Earth")
            .ChildOf(sun)
            .Set(new Mass(5.9722e24));

        world.Entity("Moon")
            .ChildOf(earth)
            .Set(new Mass(7.34767309e22));

        // Run application with REST interface. When the application is running,
        // navigate to https://flecs.dev/explorer to inspect it!
        //
        // See docs/FlecsRemoteApi.md#explorer for more information.
        world.App().EnableRest().Run();

        // Alternatively you can run your own loop by setting the EcsRest singleton
        // and calling World.Progress().
        world.Set(default(flecs.EcsRest));
        while (world.Progress()) { }
    }
}
