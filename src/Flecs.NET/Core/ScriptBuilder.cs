using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_script_desc_t"/>.
    /// </summary>
    public unsafe struct ScriptBuilder : IEquatable<ScriptBuilder>, IDisposable
    {
        private ecs_world_t* _world;
        private ecs_script_desc_t _desc;

        private NativeString _code;
        private NativeString _fileName;

        /// <summary>
        ///     Reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Reference to the desc.
        /// </summary>
        public ref ecs_script_desc_t Desc => ref _desc;

        /// <summary>
        ///     Creates a <see cref="ScriptBuilder"/> with the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        public ScriptBuilder(ecs_world_t* world)
        {
            _world = world;
            _desc = default;
            _code = default;
            _fileName = default;
        }

        /// <summary>
        ///     Creates a <see cref="ScriptBuilder"/> with the provided world and entity name.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="name">The entity name.</param>
        public ScriptBuilder(ecs_world_t* world, string name)
        {
            _world = world;
            _desc = default;
            _code = default;
            _fileName = default;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = Pointers.DefaultSeparator;
            entityDesc.root_sep = Pointers.DefaultSeparator;

            Desc.entity = ecs_entity_init(world, &entityDesc);
        }

        /// <summary>
        ///     Disposes the script builder. This should be called if the script builder
        ///     will be discarded and .Run() isn't called.
        /// </summary>
        public void Dispose()
        {
            _code.Dispose();
            _code = default;
            _fileName.Dispose();
            _fileName = default;
        }

        /// <summary>
        ///     Set to parse script from string.
        /// </summary>
        /// <param name="str">The script string.</param>
        /// <returns>Reference to self.</returns>
        public ref ScriptBuilder Code(string str)
        {
            _code.Dispose();
            _code = (NativeString)str;
            Desc.code = _code;
            return ref this;
        }

        /// <summary>
        ///     Set to load script from file.
        /// </summary>
        /// <param name="str">The script file name.</param>
        /// <returns>Reference to self.</returns>
        public ref ScriptBuilder FileName(string str)
        {
            _fileName.Dispose();
            _fileName = (NativeString)str;
            Desc.filename = _fileName;
            return ref this;
        }

        /// <summary>
        ///     Load managed script.
        /// </summary>
        /// <returns>The entity associated with the script.</returns>
        public Entity Run()
        {
            fixed (ecs_script_desc_t* desc = &Desc)
            {
                Entity entity = new Entity(World, ecs_script_init(World, desc));
                Dispose();
                return entity;
            }
        }

        /// <summary>
        ///     Checks if two <see cref="ScriptBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ScriptBuilder other)
        {
            return Desc == other.Desc;
        }

        /// <summary>
        ///     Checks if two <see cref="ScriptBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is ScriptBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="ScriptBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="ScriptBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ScriptBuilder left, ScriptBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="ScriptBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ScriptBuilder left, ScriptBuilder right)
        {
            return !(left == right);
        }
    }
}
