#if Cpp_HelloWorld

using Flecs.NET.Core;

using World world = World.Create();

// Register a system
// In C# we call them routines to prevent conflicts with the System namespace
world.Routine(
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

// Create an entity with name Bob, add Position and food preference
Entity bob = world.Entity("Bob")
    .Set(new Position { X = 0, Y = 0 })
    .Set(new Velocity { X = 1, Y = 2 })
    .Add<Eats, Apples>();

// Show us what you got
Console.WriteLine($"{bob.Name()}'s got [{bob.Type()}]");

// Run systems twice. Usually this function is called once per frame
world.Progress();
world.Progress();

// See if Bob has moved (he has)
ref readonly Position p = ref bob.Get<Position>();
Console.WriteLine($"{bob.Name()}'s position is ({p.X}, {p.Y})");

// Component types
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

// Tag types
public struct Eats { }
public struct Apples { }

#endif

// Output:
// Bob's got [Position, Velocity, (Identifier,Name), (Eats,Apples)]
// Bob's position is (2, 4)
