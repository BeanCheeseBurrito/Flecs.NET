using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_build_info_t"/>*.
    /// </summary>
    /// TODO: Add wrapper over ecs_build_info_t->addons.
    public readonly unsafe struct BuildInfo : IEquatable<BuildInfo>
    {
        private readonly ecs_build_info_t* _handle;

        /// <summary>
        ///     The <see cref="ecs_build_info_t"/>* handle.
        /// </summary>
        public ref readonly ecs_build_info_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a new <see cref="BuildInfo"/> instance with the provided <see cref="ecs_build_info_t"/> pointer.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public BuildInfo(ecs_build_info_t* handle)
        {
            _handle = handle;
        }

        /// <summary>
        ///     Compiler used to compile flecs.
        /// </summary>
        public string Compiler => NativeString.GetString(Handle->compiler);

        /// <summary>
        ///     String version.
        /// </summary>
        public string Version => NativeString.GetString(Handle->version);

        /// <summary>
        ///      Major flecs version .
        /// </summary>
        public int VersionMajor => Handle->version_major;

        /// <summary>
        ///     Minor flecs version.
        /// </summary>
        public int VersionMinor => Handle->version_minor;

        /// <summary>
        ///     Patch flecs version.
        /// </summary>
        public int VersionPatch => Handle->version_patch;

        /// <summary>
        ///     Is this a debug build.
        /// </summary>
        public bool Debug => Utils.Bool(Handle->debug);

        /// <summary>
        ///     Is this a sanitize build.
        /// </summary>
        public bool Sanitize => Utils.Bool(Handle->sanitize);

        /// <summary>
        ///  Is this a perf tracing build.
        /// </summary>
        public bool PerfTrace => Utils.Bool(Handle->perf_trace);

        /// <summary>
        ///     Checks if two <see cref="BuildInfo"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BuildInfo other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="BuildInfo"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is BuildInfo other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="BuildInfo"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="BuildInfo"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(BuildInfo left, BuildInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="BuildInfo"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(BuildInfo left, BuildInfo right)
        {
            return !(left == right);
        }
    }
}
