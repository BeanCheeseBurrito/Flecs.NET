using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for an alert.
    /// </summary>
    public unsafe struct Alert
    {
        private Entity _entity;

        /// <summary>
        ///     Reference to entity.
        /// </summary>
        public ref Entity Entity => ref _entity;

        /// <summary>
        ///     Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _entity.World;

        /// <summary>
        ///     Creates an alert.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="filterBuilder"></param>
        /// <param name="alertBuilder"></param>
        public Alert(ecs_world_t* world, string name = "", FilterBuilder filterBuilder = default,
            AlertBuilder alertBuilder = default)
        {
            ecs_alert_desc_t* alertDesc = &alertBuilder.AlertDesc;
            alertDesc->filter = filterBuilder.Desc;
            alertDesc->filter.terms_buffer = (ecs_term_t*)filterBuilder.Terms.Data;
            alertDesc->filter.terms_buffer_count = filterBuilder.Terms.Count;

            if (!string.IsNullOrEmpty(name))
            {
                using NativeString nativeName = (NativeString)name;

                ecs_entity_desc_t entityDesc = default;
                entityDesc.name = nativeName;
                entityDesc.sep = BindingContext.DefaultSeparator;
                entityDesc.root_sep = BindingContext.DefaultRootSeparator;
                alertDesc->entity = ecs_entity_init(world, &entityDesc);
            }

            _entity = new Entity(world, ecs_alert_init(world, alertDesc));

            filterBuilder.Dispose();
            alertBuilder.Dispose();
        }

        /// <summary>
        ///      Returns the entity's name if it has one, otherwise return its id.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Entity.ToString();
        }
    }
}
