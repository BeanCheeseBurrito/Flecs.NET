// This example shows different ways of setting component data for an entity.

using Flecs.NET.Core;

// Components
file record struct Position(int X, int Y);
file record struct Velocity(int X, int Y);
file record struct Size(int X, int Y);

public static unsafe class Entities_SettingComponents
{
    public static void Main()
    {
        using World world = World.Create();

        // Create an entity.
        Entity entity = world.Entity();

        // Set data for a typed component using a copy or reference.
        Position position = new(1, 1);
        entity.Set(position);
        entity.Set(ref position);

        // Set data for a typed component using a pointer.
        Velocity velocity = new(2, 2);
        entity.SetPtr(&velocity);

        // Create an untyped component at runtime.
        UntypedComponent sizeComp = world.Component("Size")
            .Member<int>("X")
            .Member<int>("Y");

        // Set data for an untyped component using a pointer.
        Size size = new(3, 3);
        entity.SetUntyped(sizeComp, &size);

        Console.WriteLine(entity.Get<Position>().ToString());            // Position { X = 1, Y = 1 }
        Console.WriteLine(entity.Get<Velocity>().ToString());            // Velocity { X = 2, Y = 2 }
        Console.WriteLine(((Size*)entity.GetPtr(sizeComp))->ToString()); // Size { X = 3, Y = 3 }
    }
}

// Output:
// Position { X = 1, Y = 1 }
// Velocity { X = 2, Y = 2 }
// Size { X = 3, Y = 3 }
