# Flecs.NET
![](https://raw.githubusercontent.com/SanderMertens/flecs/master/docs/img/logo.png)

<div align="center">

[![MIT](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](https://github.com/SanderMertens/flecs/blob/master/LICENSE)

</div>

**Flecs.NET** is a high-level wrapper for flecs. Low-level bindings to the C api are included and generated with [Bindgen.NET](https://github.com/BeanCheeseBurrito/Bindgen.NET). Native libraries are cross-compiled with [Vezel-Dev's Zig Toolsets](https://github.com/vezel-dev/zig-toolsets).
> **Warning**
> This repo is a work in progress. Bugs are expected and the API is subject to change.

## Nuget
You can download the nuget package and use Flecs.NET right away!

**Flecs.NET (Wrapper + bindings + native libraries): [Release](https://www.nuget.org/packages/Flecs.NET.Release/) | [Debug](https://www.nuget.org/packages/Flecs.NET.Debug/)**
```console
dotnet add PROJECT package Flecs.NET.Release --version *-*
```

**Flecs.NET.Bindings (Bindings): [Release](https://www.nuget.org/packages/Flecs.NET.Bindings.Release/) | [Debug](https://www.nuget.org/packages/Flecs.NET.Bindings.Debug/)**:
```console
dotnet add PROJECT package Flecs.NET.Bindings.Release --version *-*
```

**Flecs.NET.Native (Native libraries) [Release](https://www.nuget.org/packages/Flecs.NET.Native.Release/) | [Debug](https://www.nuget.org/packages/Flecs.NET.Native.Debug/)**:
```console
dotnet add PROJECT package Flecs.NET.Native.Release --version *-*
```

Flecs.NET provides both [release](https://www.nuget.org/packages/Flecs.NET.Release) and [debug](https://www.nuget.org/packages/Flecs.NET.Debug) packages for nuget.
To include both of them in your project based on your build configuration, use the packages references below. The latest stable or prerelease versions will be added to your project.
```xml
<ItemGroup>
    <PackageReference Include="Flecs.NET.Debug" Version="*-*" Condition="'$(Configuration)' == 'Debug'" />
    <PackageReference Include="Flecs.NET.Release" Version="*-*" Condition="'$(Configuration)' == 'Release'" />
</ItemGroup>
```

## Show me the code!
```csharp
using Flecs.NET.Core;

using World world = World.Create();

Routine routine = world.Routine(
    filter: world.FilterBuilder().Term<Position>().Term<Velocity>(),
    query: world.QueryBuilder(),
    routine: world.RoutineBuilder(),
    callback: it =>
    {
        Column<Position> p = it.Field<Position>(1);
        Column<Velocity> v = it.Field<Velocity>(2);

        foreach (int i in it)
        {
            p[i].X += v[i].X;
            p[i].Y += v[i].Y;
        }
    }
);

Entity entity = world.Entity("Bob")
    .Set(new Position { X = 10, Y = 20 })
    .Set(new Velocity { X = 1, Y = 2 });

while (world.Progress()) { }

public struct Position
{
    public float X { get; set; }
    public float Y { get; set; }
}

public struct Velocity
{
    public float X { get; set; }
    public float Y { get; set; }
}
```

## Running examples
> **Note**
> Flecs native libraries need to be built before running any of the examples. See step [Compile flecs](https://github.com/BeanCheeseBurrito/Flecs.NET#compile-flecs) on how to compile them.

To run any of the example programs, use ``dotnet run``and set the "Example" property to the example's path relative to the ``Flecs.NET.Examples`` project. Each level of the path must be separated by an underscore.

**Example**:
```console
dotnet run --project src/Flecs.NET.Examples --property:Example=Cpp_Entities_Basics
```

## Building from source
### Clone the repo
Clone the repo and it's submodules.
```console
git clone --recursive https://github.com/BeanCheeseBurrito/Flecs.NET.git
cd Flecs.NET
```
### Restore dependencies
Run the following command on the solution to restore all project dependencies.
```console
dotnet restore
```
### Generate bindings
Generate the binding code. Bindings are generated with [Bindgen.NET](https://github.com/BeanCheeseBurrito/Bindgen.NET).
```console
dotnet run --project src/Flecs.NET.Bindgen
```
### Compile flecs
Compile the native libraries. The [zig](https://ziglang.org/learn/overview/#cross-compiling-is-a-first-class-use-case) compiler will automatically be downloaded and cached in your local nuget package folder. Native libraries will be cross-compiled for linux, macos, and windows.
```console
dotnet build src/Flecs.NET.Native
```

### Reference the project
Reference the project and import the native libraries.

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net7.0</TargetFramework>
    </PropertyGroup>

    <Import Project="PATH/Flecs.NET/src/Flecs.NET.Native/Flecs.NET.Native.targets" />

    <ItemGroup>
        <ProjectReference Include="PATH/Flecs.NET/src/Flecs.NET/Flecs.NET.csproj" />
    </ItemGroup>

</Project>
```
