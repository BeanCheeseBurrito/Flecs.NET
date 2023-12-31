using System.Runtime.CompilerServices;
using Flecs.NET.Core;

public static class Example
{
    public static void Main()
    {
        Console.WriteLine("To run an example, use \"dotnet run --project Flecs.NET.Examples -p\"");
        Console.WriteLine("Example: \"dotnet run --project src/Flecs.NET.Examples --property:Example=Cpp_Entities_Basics\"");
    }

    [ModuleInitializer]
    internal static void Setup()
    {
        FlecsInternal.StripFileLocalTypeNameGuid = true;
    }
}
