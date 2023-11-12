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
    internal static class FlecsInitialization
    {
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
            {
                Debug.LogError("Failed to initialize Flecs.NET: unable to find valid flecs library for platform.");
            }
        }

        private static string[] EditorPackagePaths()
        {
            const string import = Native.BindgenInternal.DllImportPath;
#if FLECS_DEBUG
            const string config = "Debug";
#else
            const string config = "Release";
#endif
            return new []
            {
                Path.GetFullPath($"Packages/flecs/Flecs.NET.Native/{config}/runtimes/linux-x64/native/{import}"),
                Path.GetFullPath($"Packages/flecs/Flecs.NET.Native/{config}/runtimes/win-x64/native/{import}"),
            };
        }

        private static string[] RuntimePackagePaths()
        {
            const string import = Native.BindgenInternal.DllImportPath;

            return new []
            {
                $"{Application.dataPath}/Plugins/x86_64/{import}",
                $"{Application.dataPath}/Plugins/{import}",
            };
        }
    }

}
