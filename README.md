# Flecs.NET
![](https://raw.githubusercontent.com/SanderMertens/flecs/master/docs/img/logo.png)

> **Warning**
> Work in progress.

## Building from source
Clone the repo and it's submodules.
```bash
git clone --recursive https://github.com/BeanCheeseBurrito/Flecs.NET.git
cd Flecs.NET
```

Generate the binding code.
```bash
dotnet run --project src/Flecs.NET.Bindgen
```

Compile the native libraries. The zig compiler will automatically be downloaded and cached in your local nuget package folder. Native libraries will be cross-compiled for linux, macos, and windows.
```bash
dotnet build src/Flecs.NET.Native
```

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
