using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A filter allows for uncached, adhoc iteration over ECS data.
    /// </summary>
    public unsafe struct Filter : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_filter_t _filter;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A pointer to the filter.
        /// </summary>
        public ecs_filter_t* FilterPtr => (ecs_filter_t*)Unsafe.AsPointer(ref _filter);


        /// <summary>
        ///     Creates a filter.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public Filter(ecs_world_t* world, string name = "", FilterBuilder filterBuilder = default)
        {
            Assert.True(world == filterBuilder.World, "Worlds are different");

            _filter = ECS_FILTER_INIT;
            _world = world;

            ecs_filter_desc_t* filterDesc = &filterBuilder.FilterDesc;
            filterDesc->terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            filterDesc->terms_buffer_count = filterBuilder.Terms.Count;

            if (!string.IsNullOrEmpty(name))
            {
                using NativeString nativeName = (NativeString)name;
                using NativeString nativeSep = (NativeString)"::";

                ecs_entity_desc_t entityDesc = default;
                entityDesc.name = nativeName;
                entityDesc.sep = nativeSep;
                entityDesc.root_sep = nativeSep;
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
            fixed (ecs_filter_t* mFilter = &_filter)
            {
                ecs_filter_move(mFilter, filter);
            }
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
                Invoker.EachEntity(&iter, func);
        }
    }
}
