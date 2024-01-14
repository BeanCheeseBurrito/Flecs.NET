using System;
using System.IO;
using Flecs.NET.Bindings;
using UnityEngine;
using UnityEngine.Scripting;

// Unity quietly strips unused assemblies when compiling for IL2CPP. AlwaysLinkAssembly ensures
// the Flecs.NET.Unity assembly will always be included so it can insert the correct flecs
// runtimes paths.

[assembly: AlwaysLinkAssembly]

namespace Flecs.NET.Unity
{
    public static class FlecsInitialization
    {
        private const string PackageName = "dev.flecs.net";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize()
        {
#if UNITY_EDITOR
            Native.BindgenInternal.DllFilePaths.InsertRange(0, EditorPackagePaths());
#else
            Native.BindgenInternal.DllFilePaths.InsertRange(0, RuntimePackagePaths());
#endif
            Native.BindgenInternal.ResolveLibrary();

            if (Native.BindgenInternal._libraryHandle == IntPtr.Zero) // TODO add BindgenInternal#IsLibraryResolved?
                Debug.LogError("Failed to initialize Flecs.NET: unable to find valid flecs library for platform.");
        }

        private static string[] EditorPackagePaths()
        {
            const string import = Native.BindgenInternal.DllImportPath;
#if FLECS_UNITY_NDEBUG
            const string config = "Release";
#else
            const string config = "Debug";
#endif
            return new[]
            {
                Path.GetFullPath($"Packages/{PackageName}/Flecs.NET.Native/{config}/arm64/{import}"),
                Path.GetFullPath($"Packages/{PackageName}/Flecs.NET.Native/{config}/x64/{import}")
            };
        }

        private static string[] RuntimePackagePaths()
        {
            const string import = Native.BindgenInternal.DllImportPath;

            return new[]
            {
                $"{Application.dataPath}/Plugins/x86_64/{import}",
                $"{Application.dataPath}/Plugins/{import}"
            };
        }
    }
}
