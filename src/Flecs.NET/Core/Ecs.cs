using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    /// A static class for storing ECS related types, delegates, and methods.
    /// </summary>
    public static unsafe class Ecs
    {
        /// <summary>
        /// App init action.
        /// </summary>
        public delegate int AppInitAction(ecs_world_t* world);

        /// <summary>
        /// Context free.
        /// </summary>
        public delegate void ContextFree(void* ctx);

        /// <summary>
        /// Each entity callback.
        /// </summary>
        public delegate void EachEntityCallback(Entity entity);

        /// <summary>
        /// Each id callback.
        /// </summary>
        public delegate void EachIdCallback(Id id);

        /// <summary>
        /// Finish action.
        /// </summary>
        public delegate void FiniAction(ecs_world_t* world, void* ctx);

        /// <summary>
        /// Free.
        /// </summary>
        public delegate void Free(IntPtr data);

        /// <summary>
        /// GroupBy action.
        /// </summary>
        public delegate ulong GroupByAction(ecs_world_t* world, ecs_table_t* table, ulong groupId, void* ctx);

        /// <summary>
        /// Group create action.
        /// </summary>
        public delegate void* GroupCreateAction(ecs_world_t* world, ulong groupId, void* groupByCtx);

        /// <summary>
        /// Group delete action.
        /// </summary>
        public delegate void GroupDeleteAction(ecs_world_t* world, ulong groupId, void* groupCtx, void* groupByCtx);

        /// <summary>
        /// Iter action.
        /// </summary>
        public delegate void IterAction(ecs_iter_t* it);

        /// <summary>
        /// Iter callback.
        /// </summary>
        public delegate void IterCallback(Iter it);

        /// <summary>
        /// Iter next.
        /// </summary>
        public delegate byte IterNext(ecs_iter_t* it);

        /// <summary>
        /// OrderBy action.
        /// </summary>
        public delegate int OrderByAction(ulong e1, void* ptr1, ulong e2, void* ptr2);
    }
}
