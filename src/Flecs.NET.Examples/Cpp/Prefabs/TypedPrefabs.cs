// Just like how entities can be associated with a type (like components)
// prefabs can be associated with types as well. Types can be more convenient to
// work with than entity handles, for a few reasons:
//
// - There is no need to pass around or store entity handles
// - Prefabs automatically assume the name of the type
// - Nested types can be used to create prefab hierarchies
//
// While this functionality is not unique to prefabs (the same mechanism is
// used to distribute component handles), prefabs are a good fit, especially
// when combined with prefab slots (see slots example and code below).

#if Cpp_Prefabs_TypedPrefabs

using Flecs.NET.Core;

using World world = World.Create();

// Associate types with prefabs
world.Prefab<Turret>();
world.Prefab<Turret.Base>().SlotOf<Turret>();
world.Prefab<Turret.Head>().SlotOf<Turret>();

world.Prefab<Railgun>().IsA<Turret>();
world.Prefab<Railgun.Beam>().SlotOf<Railgun>();

// Create prefab instance.
Entity inst = world.Entity("my_railgun").IsA<Railgun>();

// Get entities for slots
Entity instBase = inst.Target<Turret.Base>();
Entity instHead = inst.Target<Turret.Head>();
Entity instBeam = inst.Target<Railgun.Beam>();

Console.WriteLine("Instance Base: " + instBase.Path());
Console.WriteLine("Instance Head: " + instHead.Path());
Console.WriteLine("Instance Beam: " + instBeam.Path());

// Create types that mirror the prefab hierarchy.
public struct Turret
{
    public struct Base { }
    public struct Head { }
}

public struct Railgun
{
    public struct Beam { }
}

#endif

// Output:
// Instance Base: ::my_railgun::Base
// Instance Head: ::my_railgun::Head
// Instance Beam: ::my_railgun::Beam
