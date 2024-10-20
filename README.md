# Flecs.NET
![](https://raw.githubusercontent.com/SanderMertens/flecs/master/docs/img/logo.png)

<div align="center">

[![MIT](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](https://github.com/SanderMertens/flecs/blob/master/LICENSE)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Flecs.NET.Release?style=for-the-badge&color=blue)](https://www.nuget.org/packages/Flecs.NET.Release)

[Docs](https://www.flecs.dev/flecs/) · [Examples](https://github.com/BeanCheeseBurrito/Flecs.NET/tree/main/src/Flecs.NET.Examples) · [Discord](https://discord.gg/BEzP5Rgrrp)

</div>

**Flecs.NET** is a high-level wrapper for [flecs](https://github.com/SanderMertens/flecs). Low-level bindings to the C api are included and generated with [Bindgen.NET](https://github.com/BeanCheeseBurrito/Bindgen.NET). Native libraries are cross-compiled with [Vezel-Dev's Zig Toolsets](https://github.com/vezel-dev/zig-toolsets).

## Show me the code!
```csharp
// Copy, paste, and run in a .NET project!

using Flecs.NET.Core;

using World ecs = World.Create();

Entity entity = ecs.Entity()
    .Set(new Position(10, 20))
    .Set(new Velocity(1, 2));

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
- Modern .NET 8
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
        <TargetFramework>net8.0</TargetFramework>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Flecs.NET.Debug" Version="*-*" Condition="'$(Configuration)' == 'Debug'" />
        <PackageReference Include="Flecs.NET.Release" Version="*-*" Condition="'$(Configuration)' == 'Release'" />
    </ItemGroup>

</Project>
```

## GitHub Package Registry
For more up-to-date packages, development builds are available on the [GitHub package registry](https://github.com/BeanCheeseBurrito?tab=packages&repo_name=Flecs.NET). Packages are automatically uploaded on every commit to the main branch.

To access development builds from your project, you first need to create a GitHub personal access token with the ``read:packages`` permission. (See [Creating a personal access token (classic)](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens#creating-a-personal-access-token-classic))

Once you have created a personal access token, run the following command to add the GitHub feed as a new package source. Replace ``YOUR_GITHUB_USERNAME`` with your GitHub username and ``YOUR_GITHUB_TOKEN`` with your personal access token.
```bash
dotnet nuget add source --name "flecs.net" --username "YOUR_GITHUB_USERNAME" --password "YOUR_GITHUB_TOKEN" --store-password-in-clear-text "https://nuget.pkg.github.com/BeanCheeseBurrito/index.json"
```

You can now reference any package from the [GitHub feed](https://github.com/BeanCheeseBurrito?tab=packages&repo_name=Flecs.NET)!

```console
dotnet add PROJECT package Flecs.NET.Release --version 4.0.2-dev-2024-10-20-03-23-34
```
```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Flecs.NET.Debug" Version="4.0.2-dev-2024-10-20-03-23-34"/>
    </ItemGroup>

</Project>
```
___
By default, the GitHub feed will be added to your global ``nuget.config`` file and can be referenced by any project on your machine. If wish to add the feed to a single project/solution, create a ``nuget.config`` file at the root of your project/solution directory and run the following command with the ``--configfile`` option.
```bash
dotnet nuget add source --configfile "./nuget.config" --name "flecs.net" --username "YOUR_GITHUB_USERNAME" --password "YOUR_GITHUB_TOKEN" --store-password-in-clear-text "https://nuget.pkg.github.com/BeanCheeseBurrito/index.json"
```
To remove the GitHub feed from your NuGet package sources, run the following command.
```bash
dotnet nuget remove source "flecs.net"
```
GitHub Actions workflows can be authenticated using the ``GITHUB_TOKEN`` secret.
```yaml
- name: Add GitHub source
  run: dotnet nuget add source --name "flecs.net" --username "USERNAME" --password "${{ secrets.GITHUB_TOKEN }}" --store-password-in-clear-text "https://nuget.pkg.github.com/BeanCheeseBurrito/index.json"
```
> [!WARNING]
> Development feed packages may be deleted without warning to free up space.

## Running examples

To run any of the example programs, use ``dotnet run``and set the "Example" property to the example's path relative to the ``Flecs.NET.Examples`` project. Each level of the path must be separated by an underscore.

**Example**:
```console
dotnet run --project src/Flecs.NET.Examples --property:Example=Entities_Basics
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
### Build Flecs.NET
Compile the wrapper and native libraries. The [zig](https://ziglang.org/learn/overview/#cross-compiling-is-a-first-class-use-case) compiler will automatically be downloaded and cached in your local nuget package folder. Native libraries will be cross-compiled for linux, macos, and windows.
```console
dotnet build
```

### Reference the project
Reference the project and import the native libraries. You should now be able to use **Flecs.NET** from your project.

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
    </PropertyGroup>

    <Import Project="PATH/Flecs.NET/src/Flecs.NET.Native/Flecs.NET.Native.targets" />

    <ItemGroup>
        <ProjectReference Include="PATH/Flecs.NET/src/Flecs.NET/Flecs.NET.csproj" />
    </ItemGroup>

</Project>
```

### Running the bindings generator
Low-level bindings to the flecs C API are pre-generated and included in the [Flecs.NET.Bindings](https://github.com/BeanCheeseBurrito/Flecs.NET/tree/main/src/Flecs.NET.Bindings) project by default. If needed, you can run the following command to regenerate the bindings file.
> [!NOTE]
> The binding generator needs access to system headers on MacOS. Ensure that XCode is installed.
```console
dotnet run --project src/Flecs.NET.Bindgen
```
### Running the code generator
**Flecs.NET** relies on code generation to avoid manual code duplication. If any changes are made to the [Flecs.NET.Codegen](https://github.com/BeanCheeseBurrito/Flecs.NET/tree/main/src/Flecs.NET.Codegen) project, you can run the following command to rerun the code generators. The generated files will be output to this [folder](https://github.com/BeanCheeseBurrito/Flecs.NET/tree/main/src/Flecs.NET/Generated).
```console
dotnet run --project src/Flecs.NET.Codegen
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
