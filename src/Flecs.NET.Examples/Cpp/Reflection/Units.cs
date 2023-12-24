#if Cpp_Reflection_Units

using System.Runtime.InteropServices;
using Flecs.NET.Bindings;
using Flecs.NET.Core;

{
    using World world = World.Create();

    // Import units module.
    world.Import<Ecs.Units>();

    // Register reflection data with units. This can improve the way information
    // is visualized in tools, such as the explorer.
    world.Component<WeatherStation>()
        .Member<double, Ecs.Units.Temperatures.Celsius>("Temperature")
        .Member<double, Ecs.Units.Pressures.Bar>("Pressure")
        .Member<double, Ecs.Units.Lengths.MilliMeters>("Precipitation");

    Entity e = world.Entity().Set<WeatherStation>(new(24, 1.2f, 0.5f));

    // Use cursor API to print values with units
    ref WeatherStation reference = ref e.GetMut<WeatherStation>();
    Cursor cur = world.Cursor(ref reference);
    cur.Push();
    PrintValue(ref cur);
    cur.Next();
    PrintValue(ref cur);
    cur.Next();
    PrintValue(ref cur);
    cur.Pop();

    return 0;
}

unsafe void PrintValue(ref Cursor cursor) {
    // Get unit entity and component.
    Entity unit = cursor.GetUnit();
    ref readonly Native.EcsUnit unitData = ref unit.Get<Native.EcsUnit>();

    // Print value with unit symbol.
    string symbol = Marshal.PtrToStringAnsi((IntPtr)unitData.symbol)!;
    Console.WriteLine($"{cursor.GetMember()}: {cursor.GetFloat():0.00} {symbol}");
}

public struct WeatherStation(double temperature, double pressure, double precipitation)
{
    public double Temperature { get; set; } = temperature;
    public double Pressure { get; set; } = pressure;
    public double Precipitation { get; set; } = precipitation;
}

#endif

// Output:
// Temperature: 24.00 °C
// Pressure: 1.20 bar
// Precipitation: 0.50 mm
