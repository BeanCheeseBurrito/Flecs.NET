using System;
using System.Collections;
using System.Collections.Generic;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_type_t.
    /// </summary>
    public unsafe struct Types : IEnumerable<Id>, IEquatable<Types>
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
        ///     Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Id> IEnumerable<Id>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Gets an enumerator for the id array.
        /// </summary>
        /// <returns></returns>
        public TypesEnumerator GetEnumerator()
        {
            return new TypesEnumerator(Handle);
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
        ///     Checks if two <see cref="Types"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Types other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="Types"/> instances are equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Types types && Equals(types);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Types"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Types"/> are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Types left, Types right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Types"/> are not equal.
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

    /// <summary>
    ///     Enumerator for <see cref="Types"/>.
    /// </summary>
    public unsafe struct TypesEnumerator : IEnumerator<Id>
    {
        /// <summary>
        ///     Pointer to <see cref="ecs_type_t"/>.
        /// </summary>
        public ecs_type_t* Handle { get; }

        /// <summary>
        ///     Length of the enumerator.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Current index of the enumerator.
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        ///     Current Id of the enumerator.
        /// </summary>
        public readonly Id Current => CurrentIndex < 0 ? default : Handle->array[CurrentIndex];

        /// <summary>
        ///     Current Id of the enumerator.
        /// </summary>
        readonly object IEnumerator.Current => Current;

        /// <summary>
        ///     Create a new enumerator with the provided <see cref="ecs_type_t"/> handle.
        /// </summary>
        /// <param name="handle"></param>
        public TypesEnumerator(ecs_type_t* handle)
        {
            Handle = handle;
            Length = handle->count;
            CurrentIndex = -1;
        }

        /// <summary>
        ///     Moves to the next index of the id array.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            CurrentIndex++;
            return CurrentIndex < Length;
        }

        /// <summary>
        ///     Resets the current index of the enumerator.
        /// </summary>
        public void Reset()
        {
            CurrentIndex = -1;
        }

        /// <summary>
        ///     Disposes of resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
