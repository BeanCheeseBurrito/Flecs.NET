using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Flecs.NET.Bindings;
using Flecs.NET.Core;
using UnityEngine;
using UnityEngine.Scripting;
using Debug = UnityEngine.Debug;

// Unity quietly strips unused assemblies when compiling for IL2CPP. AlwaysLinkAssembly ensures
// the Flecs.NET.Unity assembly will always be included so it can insert the correct flecs
// runtimes paths.

[assembly: AlwaysLinkAssembly]

namespace Flecs.NET.Unity
{
    public static unsafe class FlecsInitialization
    {
        private const string PackageName = "dev.flecs.net";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize()
        {
#if UNITY_EDITOR
            flecs.BindgenInternal.DllFilePaths.InsertRange(0, EditorPackagePaths());
#else
            flecs.BindgenInternal.DllFilePaths.InsertRange(0, RuntimePackagePaths());
#endif
            flecs.BindgenInternal.ResolveLibrary();

            if (flecs.BindgenInternal.LibraryHandle == IntPtr.Zero) // TODO add BindgenInternal#IsLibraryResolved?
                Debug.LogError("Failed to initialize Flecs.NET: unable to find valid flecs library for platform.");

            Ecs.Os.SetLog(FlecsLogCallback);
        }

        private static string[] EditorPackagePaths()
        {
            const string import = flecs.BindgenInternal.DllImportPath;
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
            const string import = flecs.BindgenInternal.DllImportPath;

            return new[]
            {
                $"{Application.dataPath}/Plugins/x86_64/{import}",
                $"{Application.dataPath}/Plugins/{import}"
            };
        }

        private static void FlecsLogCallback(int level, byte* file, int line, byte* message)
        {
            string levelStr = level switch
            {
                -2 => "Warning",
                -3 => "Error",
                -4 => "Fatal",
                >= 4 => "Journal",
                _ => "Info"
            };

            string fileStr = Marshal.PtrToStringAnsi((IntPtr)file);
            string messageStr = Marshal.PtrToStringAnsi((IntPtr)message);

            if (level <= -3)
                Debug.LogError($"{levelStr}: {fileStr}: {line}: {messageStr}\n{new StackTrace(true)}");
            else
                Debug.Log($"{levelStr}: {fileStr}: {line}: {messageStr}\n{new StackTrace(true)}");
        }
    }
}
