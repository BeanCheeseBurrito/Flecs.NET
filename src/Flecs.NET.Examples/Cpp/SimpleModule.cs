using Flecs.NET.Core;
using Simple;

public static class Cpp_SimpleModule
{
    public static void Main()
    {
        using World world = World.Create();

        world.Import<Simple.Module>();

        // Create system that uses component from module
        world.Routine<Simple.Position>("PrintPosition")
            .Each((ref Simple.Position p) =>
            {
                Console.WriteLine($"p = ({p.X}, {p.Y}) (System)");
            });

        // Create entity with imported components
        Entity e = world.Entity()
            .Set<Simple.Position>(new(10, 20))
            .Set<Simple.Velocity>(new(1, 1));

        // Call progress which runs imported Move system
        world.Progress();

        // Use component from module in operation
        ref readonly Simple.Position p = ref e.Get<Simple.Position>();
        Console.WriteLine($"p = ({p.X}, {p.Y}) (Get)");
    }
}

namespace Simple
{
    public record struct Position(float X, float Y);
    public record struct Velocity(float X, float Y);

    // Modules need to implement the IFlecsModule interface
    public struct Module : IFlecsModule
    {
        public void InitModule(ref World world)
        {
            // Register module with world. The module entity will be created with the
            // same hierarchy as the .NET namespaces (e.g. Simple.Module)
            world.Module<Module>();

            // All contents of the module are created inside the module's namespace, so
            // the Position component will be created as Simple.Module.Position

            // Component registration is optional, however by registering components
            // inside the module constructor, they will be created inside the scope
            // of the module.
            world.Component<Position>();
            world.Component<Velocity>();

            world.Routine<Position, Velocity>("Move")
                .Each((ref Position p, ref Velocity v) =>
                {
                    p.X += v.X;
                    p.Y += v.Y;
                });
        }
    }
}

// Output:
// p = (11, 21) (System)
// p = (11, 21) (Get)
