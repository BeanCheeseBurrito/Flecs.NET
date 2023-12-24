#if Cpp_Reflection_MemberRanges

using Flecs.NET.Core;

{
    using World world = World.Create();

    world.Component<CpuUtilization>()
        .Member<double>("value")
        .Range(0.0, 100.0)       // Specifics values that the member can assume.
        .WarningRange(0.0, 60.0) // Values outside this range are considered a warning.
        .ErrorRange(0.0, 80.0);  // Values outside this range are considered an error.

    world.Entity("MachineA").Set<CpuUtilization>(new(50.0));
    world.Entity("MachineB").Set<CpuUtilization>(new(75.0));
    world.Entity("MachineC").Set<CpuUtilization>(new(90.0));

    // Open https://www.flecs.dev/explorer?show=query&query=CpuUtilization to
    // see how ranges affect visualization.
    world.App().EnableRest().Run();

    return 0;
}

public struct CpuUtilization(double value)
{
    public double Value { get; set; } = value;
}

#endif

// Output:
// {X: 10, Y: 20}
