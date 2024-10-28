using System.Diagnostics;
using System.Runtime.CompilerServices;
using Bindgen.NET;

BindingOptions bindingOptions = new()
{
    Namespace = "Flecs.NET.Bindings",
    Class = "flecs",

    DllImportPath = "flecs",

    DllFilePaths =
    {
        "flecs",
        "libflecs",
        "runtimes/linux-x64/native/libflecs",
        "runtimes/linux-arm64/native/libflecs",
        "runtimes/osx-x64/native/libflecs",
        "runtimes/osx-arm64/native/libflecs",
        "runtimes/win-x64/native/flecs",
        "runtimes/win-arm64/native/flecs"
    },

    SuppressedWarnings = { "CS8981" },

    SystemIncludeDirectories = { Path.Combine(BuildConstants.ZigLibPath, "include") },

    InputFile = GetFlecsHeaderPath(),
    OutputFile = GetBindingsOutputPath(),

    GenerateMacros = true,
    GenerateExternVariables = true,
    GenerateStructEqualityFunctions = true
};

if (OperatingSystem.IsMacOS())
    bindingOptions.SystemIncludeDirectories.Add(Path.Combine(BuildConstants.ZigLibPath, "libc", "include", "any-macos-any"));

BindingGenerator.Generate(bindingOptions);

return;

string GetFlecsHeaderPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "..", "submodules", "flecs", "distr", "flecs.h"));
}

string GetBindingsOutputPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "Flecs.NET.Bindings", "Flecs.g.cs"));
}
