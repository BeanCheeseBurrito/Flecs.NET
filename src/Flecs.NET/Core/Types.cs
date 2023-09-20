using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_type_t.
    /// </summary>
    public unsafe struct Types : IEquatable<Types>
    {
        private ecs_world_t* _world;
        private ecs_type_t* _handle;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_type_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a types wrapper around the provided world and handle.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="type"></param>
        public Types(ecs_world_t* world, ecs_type_t* type)
        {
            _world = world;
            _handle = type;
        }

        /// <summary>
        ///     Convert type to comma-separated string
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_type_str(World, Handle));
        }

        /// <summary>
        ///     Return number of ids in type
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Handle == null ? 0 : Handle->count;
        }

        /// <summary>
        ///     Return pointer to array.
        /// </summary>
        /// <returns></returns>
        public ulong* Array()
        {
            return _handle == null ? null : _handle->array;
        }

        /// <summary>
        ///     Get id at specified index in type.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Id Get(int index)
        {
            Ecs.Assert(Handle != null, nameof(ECS_INVALID_PARAMETER));
            Ecs.Assert(Handle->count > index, nameof(ECS_OUT_OF_RANGE));
            return Handle == null ? new Id(null) : new Id(World, Handle->array[index]);
        }

        /// <summary>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static implicit operator ecs_type_t*(Types types)
        {
            return To(types);
        }

        /// <summary>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static ecs_type_t* To(Types types)
        {
            return types.Handle;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Types other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Types types && Equals(types);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Types left, Types right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Types left, Types right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Returns comma separated string of type names.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Macros.IsStageOrWorld(World) ? Str() : string.Empty;
        }
    }
}
