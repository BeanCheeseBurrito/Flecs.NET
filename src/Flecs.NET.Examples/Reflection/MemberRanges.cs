using Flecs.NET.Core;

// Components
file record struct CpuUtilization(double Value);

public static class Reflection_MemberRanges
{
    public static void Main()
    {
        using World world = World.Create();

        world.Component<CpuUtilization>()
            .Member<double>("Value")
            .Range(0.0, 100.0) // Specifics values that the member can assume.
            .WarningRange(0.0, 60.0) // Values outside this range are considered a warning.
            .ErrorRange(0.0, 80.0); // Values outside this range are considered an error.

        world.Entity("MachineA").Set(new CpuUtilization(50.0));
        world.Entity("MachineB").Set(new CpuUtilization(75.0));
        world.Entity("MachineC").Set(new CpuUtilization(90.0));

        // Uncomment this line and open
        //   https://www.flecs.dev/explorer?show=query&query=CpuUtilization
        // to see how ranges affect visualization:
        // world.App().EnableRest().Run();
    }
}
