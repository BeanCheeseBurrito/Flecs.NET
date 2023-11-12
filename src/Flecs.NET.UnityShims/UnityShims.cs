#pragma warning disable CS8618 // Non-nullable property should contain value when exiting constructor

using System;

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
        public static void LogError(string _) {}
    }
}
