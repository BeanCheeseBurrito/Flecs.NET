using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct Alert
    {
        private Entity _entity;

        public ref Entity Entity => ref _entity;
        public ref ecs_world_t* World => ref _entity.World;

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
                using NativeString nativeSep = (NativeString)"::";

                ecs_entity_desc_t entityDesc = default;
                entityDesc.name = nativeName;
                entityDesc.sep = nativeSep;
                entityDesc.root_sep = nativeSep;
                alertDesc->entity = ecs_entity_init(world, &entityDesc);
            }

            _entity = new Entity(world, ecs_alert_init(world, alertDesc));

            filterBuilder.Dispose();
            alertBuilder.Dispose();
        }
    }
}
