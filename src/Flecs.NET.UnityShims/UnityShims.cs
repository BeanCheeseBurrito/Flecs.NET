#pragma warning disable CS8618 // Non-nullable property should contain value when exiting constructor

// Mocked UnityEngine symbols so that Flecs.NET.Unity can compile without errors. Implementation
// is unnecessary because Flecs.NET.Unity is distributed as source and so will get linked with
// the real UnityEngine symbols.

using System;

namespace UnityEditor
{
    public class EditorApplication
    {
        public static void ExitPlaymode() { }
    }
}

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RuntimeInitializeOnLoadMethodAttribute : Attribute
    {
        public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType _) {}
    }

    public enum RuntimeInitializeLoadType
    {
        SubsystemRegistration
    }

    public static class Application
    {
        public static string dataPath { get; }
    }

    public static class Debug
    {
        public static void Log(string _) {}
        public static void LogError(string _) {}
    }
}

namespace UnityEngine.Scripting
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AlwaysLinkAssemblyAttribute : Attribute {}
}
