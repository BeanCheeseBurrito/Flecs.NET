using Flecs.NET.Core;

// Components
file record struct TypeWithEntity(ulong Entity);

public static class Cpp_Reflection_EntityType
{
    public static void Main()
    {
        using World world = World.Create();

        world.Component<TypeWithEntity>()
            .Member(Ecs.Entity, "Entity");

        Entity foo = world.Entity("Foo");

        // Create entity with TypeWithEntity component.
        Entity e = world.Entity()
            .Set<TypeWithEntity>(new(foo));

        // Convert TypeWithEntity component to flecs expression string.
        ref TypeWithEntity reference = ref e.GetMut<TypeWithEntity>();
        Console.WriteLine(world.ToExpr(ref reference)); // {Entity: Foo}
    }
}

// Output:
// {Entity: Foo}
