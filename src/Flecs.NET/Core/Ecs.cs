using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe class Ecs
    {
        public delegate void FiniAction(ecs_world_t* world, void* ctx);

        public delegate int AppInitAction(ecs_world_t* world);

        public delegate void ContextFree(void* ctx);

        public delegate void Free(IntPtr data);

        public delegate ulong GroupByAction(ecs_world_t* world, ecs_table_t* table, ulong groupId, void* ctx);

        public delegate void* GroupCreateAction(ecs_world_t* world, ulong groupId, void* groupByCtx);

        public delegate void GroupDeleteAction(ecs_world_t* world, ulong groupId, void* groupCtx, void* groupByCtx);

        public delegate void IterAction(ecs_iter_t* iter);

        public delegate void IterCallback(Iter iter);

        public delegate byte IterNext(ecs_iter_t* iter);

        public delegate void EachEntityCallback(Entity entity);

        public delegate void EachIdCallback(Id id);

        public delegate int OrderByAction(ulong e1, void* ptr1, ulong e2, void* ptr2);
    }
}
