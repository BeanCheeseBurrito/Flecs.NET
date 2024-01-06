# Flecs.NET
![](https://raw.githubusercontent.com/SanderMertens/flecs/master/docs/img/logo.png)

<div align="center">

[![MIT](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](https://github.com/SanderMertens/flecs/blob/master/LICENSE)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Flecs.NET.Release?style=for-the-badge&color=blue)](https://www.nuget.org/packages/Flecs.NET.Release)

[Docs](https://beancheeseburrito.github.io/Flecs.NET.Docs/) · [Examples](https://github.com/BeanCheeseBurrito/Flecs.NET/tree/main/src/Flecs.NET.Examples) · [Discord](https://discord.gg/BEzP5Rgrrp)

</div>

**Flecs.NET** is a high-level wrapper for [flecs](https://github.com/SanderMertens/flecs). Low-level bindings to the C api are included and generated with [Bindgen.NET](https://github.com/BeanCheeseBurrito/Bindgen.NET). Native libraries are cross-compiled with [Vezel-Dev's Zig Toolsets](https://github.com/vezel-dev/zig-toolsets).

## Show me the code!
```csharp
// Copy, paste, and run in a .NET project!

using Flecs.NET.Core;

using World ecs = World.Create();

Entity entity = ecs.Entity()
    .Set<Position>(new(10, 20))
    .Set<Velocity>(new(1, 2));

ecs.Each((ref Position p, ref Velocity v) =>
{
    p.X += v.X;
    p.Y += v.Y;
});

public record struct Position(float X, float Y);
public record struct Velocity(float X, float Y);
```

## Overview
**Flecs.NET - High-level C# port of the C++ wrapper**
- Compatible with .NET Standard 2.1 and NativeAOT
- Near feature parity with the C++ API
- Struct-based API with minimal GC interaction
- Supports both unmanaged and managed types as components
- Implicitly registers components on-the-fly

**Flecs.NET.Bindings - Low-level bindings of the C API**
- Build your own wrapper to suite your personal needs
- Auto-generated bindings for the entire flecs API
- Fully blittable interface with no runtime marshalling

**Flecs.NET.Native - Precompiled native libraries**
- Provides both shared and static libraries for Windows, MacOS, and Linux
- Packaged with Zig for dependency free cross-compilation everywhere

## NuGet
You can download the nuget package and use **Flecs.NET** right away!

**Flecs.NET (Wrapper + Bindings + Native Libraries): [Release](https://www.nuget.org/packages/Flecs.NET.Release/) | [Debug](https://www.nuget.org/packages/Flecs.NET.Debug/)**
```console
dotnet add PROJECT package Flecs.NET.Release --version *-*
```

**Flecs.NET.Bindings (Bindings + Native Libraries): [Release](https://www.nuget.org/packages/Flecs.NET.Bindings.Release/) | [Debug](https://www.nuget.org/packages/Flecs.NET.Bindings.Debug/)**
```console
dotnet add PROJECT package Flecs.NET.Bindings.Release --version *-*
```

**Flecs.NET.Native (Native Libraries): [Release](https://www.nuget.org/packages/Flecs.NET.Native.Release/) | [Debug](https://www.nuget.org/packages/Flecs.NET.Native.Debug/)**
```console
dotnet add PROJECT package Flecs.NET.Native.Release --version *-*
```

**Flecs.NET** provides both [release](https://www.nuget.org/packages/Flecs.NET.Release) and [debug](https://www.nuget.org/packages/Flecs.NET.Debug) packages for nuget.
To include both of them in your project based on your build configuration, use the package references below. The latest stable or prerelease versions will be added to your project.
```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net7.0</TargetFramework>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Flecs.NET.Debug" Version="*-*" Condition="'$(Configuration)' == 'Debug'" />
        <PackageReference Include="Flecs.NET.Release" Version="*-*" Condition="'$(Configuration)' == 'Release'" />
    </ItemGroup>

</Project>
```

## GitLab Package Registry
For more up-to-date packages, development builds are available on the [GitLab package registry](https://gitlab.com/BeanCheeseBurrito/Flecs.NET/-/packages). To add the development feed to your project, add the GitLab link below  as a restore source. You can now reference any package version listed [here](https://gitlab.com/BeanCheeseBurrito/Flecs.NET/-/packages)!
```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net7.0</TargetFramework>
        <RestoreAdditionalProjectSources>https://gitlab.com/api/v4/projects/51698729/packages/nuget/index.json</RestoreAdditionalProjectSources>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Flecs.NET.Debug" Version="3.2.8-dev-2023-10-30-11-06-14"/>
    </ItemGroup>

</Project>
```
> [!WARNING] 
> Development feed packages may be deleted without warning to free up space.

## Unity Package Manager
The **Flecs.NET** [Unity Package](https://github.com/BeanCheeseBurrito/Flecs.NET.Unity.git) is hosted on github and can be downloaded using the URL below.
- Open the package manager window ``Window > Package Manager``
- Click the ``+`` icon
- Click ``Add package from git URL...``
- Enter the **Flecs.NET.Unity** git URL
```console
https://github.com/BeanCheeseBurrito/Flecs.NET.Unity.git
```
> [!NOTE]
> Only Windows, MacOS, and Linux builds on x64 Mono are supported. IL2CPP, web, and mobile support are planned for the future.

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
> **Note**
> The binding generator needs access to system headers on MacOS. Ensure that XCode is installed.
```console
dotnet run --project src/Flecs.NET.Bindgen
```
### Build Flecs.NET
Compile the wrapper and native libraries. The [zig](https://ziglang.org/learn/overview/#cross-compiling-is-a-first-class-use-case) compiler will automatically be downloaded and cached in your local nuget package folder. Native libraries will be cross-compiled for linux, macos, and windows.
```console
dotnet build
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
## Contributing
Feel free to open an issue or pull request. All contributions are welcome!

**Ways to contribute:**
- Bug Fixes/Reports
- Feature Requests
- Documentation
- Examples/Snippets
- Demos
- Typo Corrections
