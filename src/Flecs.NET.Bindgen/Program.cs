using System.Runtime.CompilerServices;
using Bindgen.NET;

BindingOptions bindingOptions = new()
{
    Namespace = "Flecs.NET.Bindings",
    Class = "Native",

    DllImportPath = "libflecs",
    DllFilePaths =
    {
        "libflecs",
        "runtimes/linux-x64/native/libflecs",
        "runtimes/linux-arm64/native/libflecs",
        "runtimes/osx-x64/native/libflecs",
        "runtimes/osx-arm64/native/libflecs",
        "runtimes/win-x64/native/libflecs",
        "runtimes/win-arm64/native/libflecs"
    },

    IncludeBuiltInClangHeaders = true,

    InputFile = GetFlecsHeaderPath(),
    OutputFile = GetBindingsOutputPath(),

    GenerateMacros = true,
    GenerateExternVariables = true
};

BindingGenerator.Generate(bindingOptions);

string GetFlecsHeaderPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "..", "submodules", "flecs", "flecs.h"));
}

string GetBindingsOutputPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "Flecs.NET.Bindings", "Flecs.g.cs"));
}