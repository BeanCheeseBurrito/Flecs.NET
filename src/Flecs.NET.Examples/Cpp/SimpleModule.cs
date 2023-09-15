#if Cpp_SimpleModule

using Flecs.NET.Core;

using World world = World.Create();

world.Import<Simple.Module>();

// Create system that uses component from module
world.Routine(
    name: "PrintPosition",
    filter: world.FilterBuilder().With<Simple.Position>(),
    callback: (Iter it, int i) =>
    {
        Column<Simple.Position> p = it.Field<Simple.Position>(1);
        Console.WriteLine($"p = ({p[i].X}, {p[i].Y}) (System)");
    }
);

// Create entity with imported components
Entity e = world.Entity()
    .Set(new Simple.Position { X = 10, Y = 20 })
    .Set(new Simple.Velocity { X = 1, Y = 1 });

// Call progress which runs imported Move system
world.Progress();

// Use component from module in operation
ref readonly Simple.Position p = ref e.Get<Simple.Position>();
Console.WriteLine($"p = ({p.X}, {p.Y}) (Get)");

namespace Simple
{
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

            world.Routine(
                name: "Move",
                filter: world.FilterBuilder()
                    .With<Position>()
                    .With<Velocity>(),
                callback: (Iter it, int i) =>
                {
                    Column<Position> p = it.Field<Position>(1);
                    Column<Velocity> v = it.Field<Velocity>(2);

                    p[i].X += v[i].X;
                    p[i].Y += v[i].Y;
                }
            );
        }
    }

    public struct Position
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public struct Velocity
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}

#endif

// Output:
// p = (11, 21) (System)
// p = (11, 21) (Get)
