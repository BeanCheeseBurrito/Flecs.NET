using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Wrapper around ecs_event_desc_t.
    /// </summary>
    public unsafe struct EventBuilder
    {
        private ecs_world_t* _world;
        private ecs_event_desc_t _desc;
        private ecs_type_t _ids;
        private fixed ulong _idsArray[FLECS_EVENT_DESC_MAX];

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the event description.
        /// </summary>
        public ref ecs_event_desc_t Desc => ref _desc;

        /// <summary>
        ///     Creates an event builder.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="eventId"></param>
        public EventBuilder(ecs_world_t* world, ulong eventId)
        {
            _world = world;
            _desc = default;
            _ids = default;

            _desc.@event = eventId;
        }

        /// <summary>
        ///     Add (component) id to emit for
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref EventBuilder Id(ulong id)
        {
            fixed (EventBuilder* self = &this)
            {
                _ids.array = self->_idsArray;
                _ids.array[_ids.count] = id;
                _ids.count++;
                return ref this;
            }
        }

        /// <summary>
        ///     Add pair to emit for.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref EventBuilder Id(ulong first, ulong second)
        {
            return ref Id(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Add component to emit for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref EventBuilder Id<T>()
        {
            return ref Id(Type<T>.Id(World));
        }

        /// <summary>
        ///     Add pair to emit for.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref EventBuilder Id<TFirst>(ulong second)
        {
            return ref Id(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Add pair to emit for.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref EventBuilder Id<TFirst, TSecond>()
        {
            return ref Id(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Set entity for which to emit event
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ref EventBuilder Entity(ulong entity)
        {
            ecs_record_t *r = ecs_record_find(World, entity);

            Ecs.Assert(r != null, nameof(ECS_INVALID_PARAMETER));
            Ecs.Assert(r->table != null, nameof(ECS_INVALID_PARAMETER));

            _desc.table = r->table;
            _desc.offset = Macros.RecordToRow(r->row);
            _desc.count = 1;
            return ref this;
        }

        /// <summary>
        ///     Set table for which to emit event.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public ref EventBuilder Table(ecs_table_t *t, int offset = 0, int count = 0)
        {
            _desc.table = t;
            _desc.offset = offset;
            _desc.count = count;
            return ref this;
        }

        /// <summary>
        ///     Set event data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ref EventBuilder Ctx(void* data)
        {
            _desc.param = data;
            return ref this;
        }

        /// <summary>
        ///     Set event data.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref EventBuilder Ctx<T>(T* data) where T : unmanaged
        {
            _desc.param = data;
            return ref this;
        }

        /// <summary>
        ///     Emits the event.
        /// </summary>
        public void Emit()
        {
            Ecs.Assert(_ids.count != 0, nameof(ECS_INVALID_PARAMETER));
            Ecs.Assert(_desc.table != null, nameof(ECS_INVALID_PARAMETER));

            fixed (EventBuilder* self = &this)
            {
                _ids.array = self->_idsArray;
                _desc.ids = &self->_ids;
                _desc.observable = ecs_get_world(World);
                ecs_emit(World, &self->_desc);
            }
        }
    }
}
