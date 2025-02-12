using System.Runtime.CompilerServices;
using Bindgen.NET;

BindingOptions bindingOptions = new()
{
    Namespace = "Flecs.NET.Bindings",
    Class = "flecs",

    DllImportPath = "flecs",

    SuppressedWarnings = { "CS8981" },

    SystemIncludeDirectories = { Path.Combine(BuildConstants.ZigLibPath, "include") },
    IncludeDirectories = { GetFlecsIncludePath() },

    InputFile = GetFlecsHeaderPath(),
    OutputFile = GetBindingsOutputPath(),
    NativeOutputFile = GetBindingsHelperOutputPath(),

    GenerateExternVariables = true,
    GenerateDisableRuntimeMarshallingAttribute = true,

    Ignored = {"FLECS_IDEcsPipelineQueryID_"}
};

if (OperatingSystem.IsMacOS())
    bindingOptions.SystemIncludeDirectories.Add(Path.Combine(BuildConstants.ZigLibPath, "libc", "include", "any-macos-any"));

BindingGenerator.Generate(bindingOptions);

return;

string GetFlecsIncludePath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "..", "native", "flecs", "include"));
}

string GetFlecsHeaderPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "..", "native", "flecs", "include", "flecs.h"));
}

string GetBindingsOutputPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "Flecs.NET.Bindings", "Flecs.g.cs"));
}

string GetBindingsHelperOutputPath([CallerFilePath] string filePath = "")
{
    return Path.GetFullPath(Path.Combine(filePath, "..", "..", "..", "native", "flecs_helpers.c"));
}
