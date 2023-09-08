using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for storing ECS related types, delegates, and methods.
    /// </summary>
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     App init action.
        /// </summary>
        public delegate int AppInitAction(ecs_world_t* world);

        /// <summary>
        ///     Context free.
        /// </summary>
        public delegate void ContextFree(void* ctx);

        /// <summary>
        ///     Each entity callback.
        /// </summary>
        public delegate void EachEntityCallback(Entity entity);

        /// <summary>
        ///     Each id callback.
        /// </summary>
        public delegate void EachIdCallback(Id id);

        /// <summary>
        ///     Each index callback.
        /// </summary>
        public delegate void EachIndexCallback(Iter it, int i);

        /// <summary>
        ///     Finish action.
        /// </summary>
        public delegate void FiniAction(ecs_world_t* world, void* ctx);

        /// <summary>
        ///     Free.
        /// </summary>
        public delegate void Free(IntPtr data);

        /// <summary>
        ///     GroupBy action.
        /// </summary>
        public delegate ulong GroupByAction(ecs_world_t* world, ecs_table_t* table, ulong groupId, void* ctx);

        /// <summary>
        ///     Group create action.
        /// </summary>
        public delegate void* GroupCreateAction(ecs_world_t* world, ulong groupId, void* groupByCtx);

        /// <summary>
        ///     Group delete action.
        /// </summary>
        public delegate void GroupDeleteAction(ecs_world_t* world, ulong groupId, void* groupCtx, void* groupByCtx);

        /// <summary>
        ///     Iter action.
        /// </summary>
        public delegate void IterAction(ecs_iter_t* it);

        /// <summary>
        ///     Iter next action.
        /// </summary>
        public delegate byte IterNextAction(ecs_iter_t* it);

        /// <summary>
        ///     Iter callback.
        /// </summary>
        public delegate void IterCallback(Iter it);

        /// <summary>
        ///     OrderBy action.
        /// </summary>
        public delegate int OrderByAction(ulong e1, void* ptr1, ulong e2, void* ptr2);
    }

    // Builtin pipeline tags
    public static partial class Ecs
    {
        /// <summary>
        ///     Reference to EcsOnStart tag.
        /// </summary>
        public static ref ulong OnStart => ref EcsOnStart;

        /// <summary>
        ///     Reference to EcsPreFrame tag.
        /// </summary>
        public static ref ulong PreFrame => ref EcsPreFrame;

        /// <summary>
        ///     Reference to EcsOnLoad tag.
        /// </summary>
        public static ref ulong OnLoad => ref EcsOnLoad;

        /// <summary>
        ///     Reference to EcsPostLoad tag.
        /// </summary>
        public static ref ulong PostLoad => ref EcsPostLoad;

        /// <summary>
        ///     Reference to EcsPreUpdate tag.
        /// </summary>
        public static ref ulong PreUpdate => ref EcsPreUpdate;

        /// <summary>
        ///     Reference to EcsOnUpdate tag.
        /// </summary>
        public static ref ulong OnUpdate => ref EcsOnUpdate;

        /// <summary>
        ///     Reference to EcsOnValidate tag.
        /// </summary>
        public static ref ulong OnValidate => ref EcsOnValidate;

        /// <summary>
        ///     Reference to EcsPostUpdate tag.
        /// </summary>
        public static ref ulong PostUpdate => ref EcsPostUpdate;

        /// <summary>
        ///     Reference to EcsPreStore tag.
        /// </summary>
        public static ref ulong PreStore => ref EcsPreStore;

        /// <summary>
        ///     Reference to EcsOnStore tag.
        /// </summary>
        public static ref ulong OnStore => ref EcsOnStore;

        /// <summary>
        ///     Reference to EcsPostFrame tag.
        /// </summary>
        public static ref ulong PostFrame => ref EcsPostFrame;
    }
}
