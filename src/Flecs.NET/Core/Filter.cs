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
    public unsafe partial struct Filter : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_filter_t _filter;
        private ecs_filter_t* _filterPtr;
        private bool _isOwned;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A pointer to the filter.
        /// </summary>
        public ecs_filter_t* FilterPtr => _isOwned ? (ecs_filter_t*)Unsafe.AsPointer(ref _filter) : _filterPtr;

        /// <summary>
        ///     Creates a filter.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public Filter(ecs_world_t* world, FilterBuilder filterBuilder = default, string name = "")
        {
            Assert.True(world == filterBuilder.World, "Worlds are different");

            _filter = ECS_FILTER_INIT;
            _filterPtr = default;
            _world = world;
            _isOwned = true;

            ecs_filter_desc_t* filterDesc = &filterBuilder.FilterDesc;
            filterDesc->terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            filterDesc->terms_buffer_count = filterBuilder.Terms.Count;

            if (!string.IsNullOrEmpty(name))
            {
                using NativeString nativeName = (NativeString)name;

                ecs_entity_desc_t entityDesc = default;
                entityDesc.name = nativeName;
                entityDesc.sep = BindingContext.DefaultSeparator;
                entityDesc.root_sep = BindingContext.DefaultRootSeparator;
                filterDesc->entity = ecs_entity_init(world, &entityDesc);
            }

            fixed (ecs_filter_t* filter = &_filter)
            {
                filterDesc->storage = filter;
                if (ecs_filter_init(world, filterDesc) == null)
                    throw new InvalidOperationException("Failed to init filter");
            }

            filterBuilder.Dispose();
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
                if (mFilter == FilterPtr && FilterPtr != null)
                    ecs_filter_fini(mFilter);
            }
        }

        /// <summary>
        ///     Returns the entity associated with the filter.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(FilterPtr));
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
            return NativeString.GetStringAndFree(ecs_filter_str(World, FilterPtr));
        }

        /// <summary>
        ///     Create an iterator object that can be modified before iterating.
        /// </summary>
        /// <returns></returns>
        public IterIterable Iter()
        {
            return new IterIterable(ecs_filter_iter(World, FilterPtr), _next, _nextInstanced);
        }

        /// <summary>
        ///     Iterates the filter using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, FilterPtr);
            while (ecs_filter_next(&iter) == 1)
                Invoker.Iter(&iter, func);
        }

        /// <summary>
        ///     Iterates the filter using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachEntityCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, FilterPtr);
            while (ecs_filter_next_instanced(&iter) == 1)
                Invoker.Each(&iter, func);
        }

        /// <summary>
        ///     Iterates the filter using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachIndexCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, FilterPtr);
            while (ecs_filter_next_instanced(&iter) == 1)
                Invoker.Each(&iter, func);
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
        private static IntPtr _next;
        private static IntPtr _nextInstanced;

        private static Ecs.IterNextAction _nextReference = ecs_filter_next;
        private static Ecs.IterNextAction _nextInstancedReference = ecs_filter_next_instanced;

        static Filter()
        {
            _next = Marshal.GetFunctionPointerForDelegate(_nextReference);
            _nextInstanced = Marshal.GetFunctionPointerForDelegate(_nextInstancedReference);
        }
    }
#endif
}
