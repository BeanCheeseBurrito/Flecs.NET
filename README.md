# Flecs.NET
![](https://raw.githubusercontent.com/SanderMertens/flecs/master/docs/img/logo.png)

> **Warning**
> Work in progress.

## Running examples
> **Note**
> Flecs native libraries need to be built before running any of the examples. See step [Compile flecs](https://github.com/BeanCheeseBurrito/Flecs.NET#compile-flecs) on how to compile them.

To run any of the example programs, use ``dotnet run``and set the "Example" property to the example's path relative to the ``Flecs.NET.Examples`` project. Each level of the path must be separated by an underscore.

**Example**:
```bash
dotnet run --project src/Flecs.NET.Examples --property:Example=Cpp_Entities_Basics
```

## Building from source
### Clone the repo
Clone the repo and it's submodules.
```bash
git clone --recursive https://github.com/BeanCheeseBurrito/Flecs.NET.git
cd Flecs.NET
```
### Generate bindings
Generate the binding code.
```bash
dotnet run --project src/Flecs.NET.Bindgen
```
### Compile flecs
Compile the native libraries. The [zig](https://ziglang.org/learn/overview/#cross-compiling-is-a-first-class-use-case) compiler will automatically be downloaded and cached in your local nuget package folder. Native libraries will be cross-compiled for linux, macos, and windows.
```bash
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
