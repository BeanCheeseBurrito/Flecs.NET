using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A filter allows for uncached, adhoc iteration over ECS data.
    /// </summary>
    public unsafe partial struct Filter : IEquatable<Filter>, IDisposable
    {
        private ecs_world_t* _world;
        private ecs_filter_t _filter;
        private readonly ecs_filter_t* _filterPtr;
        private readonly bool _isOwned;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A pointer to the filter.
        /// </summary>
        // TODO: Fix GC hole.
        public ecs_filter_t* Handle => _isOwned ? (ecs_filter_t*)Unsafe.AsPointer(ref _filter) : _filterPtr;

        /// <summary>
        ///     Creates a filter.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filter"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public Filter(ecs_world_t* world, ecs_filter_t filter)
        {
            _world = world;
            _filter = filter;
            _isOwned = true;
            _filterPtr = default;
        }

        /// <summary>
        ///     Creates a filter with the specified world and filter pointer.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="filter"></param>
        public Filter(ecs_world_t* world, ecs_filter_t* filter)
        {
            _world = world;
            _isOwned = false;
            _filter = default;
            _filterPtr = filter;
        }

        /// <summary>
        ///     Cleans up resources.
        /// </summary>
        public void Dispose()
        {
            fixed (ecs_filter_t* mFilter = &_filter)
            {
                if (mFilter == Handle && Handle != null)
                    ecs_filter_fini(mFilter);
            }
        }

        /// <summary>
        ///     Returns the entity associated with the filter.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(Handle));
        }

        /// <summary>
        ///     Returns the field count of the filter.
        /// </summary>
        /// <returns></returns>
        public int FieldCount()
        {
            return _filter.field_count;
        }

        /// <summary>
        ///     Returns the string representation of the filter query.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_filter_str(World, Handle));
        }

        /// <summary>
        ///     Create an iterator object that can be modified before iterating.
        /// </summary>
        /// <returns></returns>
        public IterIterable Iter()
        {
            return new IterIterable(ecs_filter_iter(World, Handle), _next, _nextInstanced);
        }

        /// <summary>
        ///     Iterates the filter using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, Handle);
            while (ecs_filter_next(&iter) == 1)
                Invoker.Iter(&iter, func);
        }

        /// <summary>
        ///     Iterates the filter using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachEntityCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, Handle);
            while (ecs_filter_next_instanced(&iter) == 1)
                Invoker.Each(&iter, func);
        }

        /// <summary>
        ///     Iterates the filter using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachIndexCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, Handle);
            while (ecs_filter_next_instanced(&iter) == 1)
                Invoker.Each(&iter, func);
        }

        /// <summary>
        ///     Checks if two <see cref="Filter"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Filter other)
        {
            return _filterPtr == other._filterPtr;
        }

        /// <summary>
        ///     Checks if two <see cref="Filter"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Filter other && Equals(other);
        }

        /// <summary>
        ///     Gets the hash code of the <see cref="Filter"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _filterPtr->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Filter"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Filter left, Filter right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Filter"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Filter left, Filter right)
        {
            return !(left == right);
        }
    }

#if NET5_0_OR_GREATER
    public unsafe partial struct Filter
    {
        private static IntPtr _next = (IntPtr)(delegate* <ecs_iter_t*, byte>)&ecs_filter_next;
        private static IntPtr _nextInstanced = (IntPtr)(delegate* <ecs_iter_t*, byte>)&ecs_filter_next_instanced;
    }
#else
    public unsafe partial struct Filter
    {
        private static readonly IntPtr _next;
        private static readonly IntPtr _nextInstanced;

        private static readonly Ecs.IterNextAction _nextReference = ecs_filter_next;
        private static readonly Ecs.IterNextAction _nextInstancedReference = ecs_filter_next_instanced;

        static Filter()
        {
            _next = Marshal.GetFunctionPointerForDelegate(_nextReference);
            _nextInstanced = Marshal.GetFunctionPointerForDelegate(_nextInstancedReference);
        }
    }
#endif
}
