using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for ecs_rule_t.
    /// </summary>
    public unsafe struct Rule : IDisposable
    {
        private ecs_world_t* _world;
        private ecs_rule_t* _handle;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_rule_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a rule for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public Rule(ecs_world_t* world, FilterBuilder filterBuilder = default, string name = "")
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

        /// <summary>
        ///     Disposes the rule.
        /// </summary>
        public void Dispose()
        {
            Destruct();
        }

        /// <summary>
        ///     Destructs the rule.
        /// </summary>
        public void Destruct()
        {
            if (Handle == null)
                return;

            ecs_rule_fini(Handle);
            World = null;
            Handle = null;
        }

        /// <summary>
        ///     Tests if the rule is not null.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return Handle != null;
        }

        /// <summary>
        ///     Returns the entity associated with the rule.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(Handle));
        }

        /// <summary>
        ///     Returns the filter for the rule.
        /// </summary>
        /// <returns></returns>
        public Filter Filter()
        {
            return new Filter(World, ecs_rule_get_filter(Handle));
        }

        /// <summary>
        ///     Converts rule to a string expression.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            ecs_filter_t* filter = ecs_rule_get_filter(Handle);
            return NativeString.GetStringAndFree(ecs_filter_str(World, filter));
        }

        /// <summary>
        ///     Converts rule to a string that can be used to aid in debugging.
        /// </summary>
        /// <returns></returns>
        public string RuleStr()
        {
            return NativeString.GetStringAndFree(ecs_rule_str(Handle));
        }

        /// <summary>
        ///     Iterates the rule using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_rule_iter(World, Handle);
            while (ecs_rule_next(&iter) == 1)
                Invoker.Iter(&iter, func);
        }

        /// <summary>
        ///     Iterates the rule using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachEntityCallback func)
        {
            ecs_iter_t iter = ecs_rule_iter(World, Handle);
            while (ecs_rule_next_instanced(&iter) == 1)
                Invoker.EachEntity(&iter, func);
        }
    }
}
