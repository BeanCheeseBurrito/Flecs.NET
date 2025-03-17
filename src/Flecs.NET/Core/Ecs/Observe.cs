namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    internal static void Observe(Entity entity, ulong eventId, InvokerCallback callback, delegate*<ecs_iter_t*, void> invoker)
    {
        IteratorContext* iteratorContext = Memory.AllocZeroed<IteratorContext>(1);
        iteratorContext->Callback.Set(callback, invoker);

        ecs_observer_desc_t desc = default;
        desc.events[0] = eventId;
        desc.query.terms[0].id = EcsAny;
        desc.query.terms[0].src.id = entity;
        desc.callback = &Functions.IteratorCallback;
        desc.callback_ctx = iteratorContext;
        desc.callback_ctx_free = &Functions.IteratorContextFree;

        ulong observer = ecs_observer_init(entity.World, &desc);
        ecs_add_id(entity.World, observer, Pair(EcsChildOf, entity));
    }
}
