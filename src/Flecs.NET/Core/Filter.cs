using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Filter : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_filter_t _filter;

        public ref ecs_world_t* World => ref _world;
        public ecs_filter_t* FilterPtr => (ecs_filter_t*)Unsafe.AsPointer(ref _filter);

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

        public Filter(ecs_world_t* world, ecs_filter_t* filter)
        {
            _world = world;
            fixed (ecs_filter_t* mFilter = &_filter)
            {
                ecs_filter_move(mFilter, filter);
            }
        }

        public void Dispose()
        {
            fixed (ecs_filter_t* mFilter = &_filter)
            {
                if (mFilter == FilterPtr && FilterPtr != null)
                    ecs_filter_fini(mFilter);
            }
        }

        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(FilterPtr));
        }

        public int FieldCount()
        {
            return _filter.field_count;
        }

        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_filter_str(World, FilterPtr));
        }

        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_filter_iter(World, FilterPtr);
            Invoker.Iter(func, ecs_filter_next, &iter);
        }
    }
}
