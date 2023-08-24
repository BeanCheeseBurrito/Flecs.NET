using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Rule : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_rule_t* _handle;

        public ref ecs_world_t* World => ref _world;
        public ref ecs_rule_t* Handle => ref _handle;

        public Rule(ecs_world_t* world, string name = "", FilterBuilder filterBuilder = default)
        {
            _world = world;

            ecs_filter_desc_t* filterDesc = &filterBuilder.FilterDesc;

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

            _handle = ecs_rule_init(world, filterDesc);

            if (Handle == null)
                throw new InvalidOperationException("Rule failed to init");

            filterBuilder.Dispose();
        }

        public void Dispose()
        {
            Destruct();
        }

        public void Destruct()
        {
            if (Handle == null)
                return;

            ecs_rule_fini(Handle);
            World = null;
            Handle = null;
        }

        public bool IsValid()
        {
            return Handle != null;
        }

        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(Handle));
        }

        public Filter Filter()
        {
            return new Filter(World, ecs_rule_get_filter(Handle));
        }

        public string Str()
        {
            ecs_filter_t* filter = ecs_rule_get_filter(Handle);
            return NativeString.GetStringAndFree(ecs_filter_str(World, filter));
        }

        public string RuleStr()
        {
            return NativeString.GetStringAndFree(ecs_rule_str(Handle));
        }

        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_rule_iter(World, Handle);
            Invoker.Iter(func, ecs_rule_next, &iter);
        }
    }
}
