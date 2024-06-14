using System;
using Flecs.NET.Bindings;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_world_info_t"/>*.
    /// </summary>
    public readonly unsafe struct WorldInfo : IEquatable<WorldInfo>
    {
        private readonly ecs_world_info_t* _handle;

        /// <summary>
        ///     The handle to the world info.
        /// </summary>
        public ref readonly ecs_world_info_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a new <see cref="WorldInfo"/> instance with the provided <see cref="ecs_world_info_t"/> pointer.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public WorldInfo(ecs_world_info_t* handle)
        {
            _handle = handle;
        }

        /// <summary>
        ///     Last issued component entity id.
        /// </summary>
        public ulong LastComponentId => Handle->last_component_id;

        /// <summary>
        ///     First allowed entity id.
        /// </summary>
        public ulong MinId => Handle->min_id;

        /// <summary>
        ///     Last allowed entity id.
        /// </summary>
        public ulong MaxId => Handle->max_id;

        /// <summary>
        ///     Raw delta time. (No time scaling)
        /// </summary>
        public float DeltaTimeRaw => Handle->delta_time_raw;

        /// <summary>
        ///     Time passed to or computed by <see cref="World.Progress"/>.
        /// </summary>
        public float DeltaTime => Handle->delta_time;

        /// <summary>
        ///     Time scale applied to <see cref="DeltaTime"/>.
        /// </summary>
        public float TimeScale => Handle->time_scale;

        /// <summary>
        ///     Target fps.
        /// </summary>
        public float TargetFps => Handle->target_fps;

        /// <summary>
        ///     Total time spent processing a frame.
        /// </summary>
        public float FrameTimeTotal => Handle->frame_time_total;

        /// <summary>
        ///     Total time spent in systems.
        /// </summary>
        public float SystemTimeTotal => Handle->system_time_total;

        /// <summary>
        ///     Total time spent in merges.
        /// </summary>
        public float EmitTimeTotal => Handle->emit_time_total;

        /// <summary>
        ///     Time elapsed in simulation.
        /// </summary>
        public float MergeTimeTotal => Handle->merge_time_total;

        /// <summary>
        ///     Time elapsed in simulation.
        /// </summary>
        public float WorldTimeTotal => Handle->world_time_total;

        /// <summary>
        ///     Time elapsed in simulation. (No time scaling)
        /// </summary>
        public float WorldTimeTotalRaw => Handle->world_time_total_raw;

        /// <summary>
        ///     Time spent on query rematching.
        /// </summary>
        public float RematchTimeTotal => Handle->rematch_time_total;

        /// <summary>
        ///     Total number of frames.
        /// </summary>
        public long FrameCountTotal => Handle->frame_count_total;

        /// <summary>
        ///     Total number of merges.
        /// </summary>
        public long MergeCountTotal => Handle->merge_count_total;

        /// <summary>
        ///     Total number of rematches.
        /// </summary>
        public long RematchCountTotal => Handle->rematch_count_total;

        /// <summary>
        ///     Total number of times a new id was created.
        /// </summary>
        public long IdCreateTotal => Handle->id_create_total;

        /// <summary>
        ///     Total number of times an id was deleted.
        /// </summary>
        public long IdDeleteTotal => Handle->id_delete_total;

        /// <summary>
        ///     Total number of times a table was created.
        /// </summary>
        public long TableCreateTotal => Handle->table_create_total;

        /// <summary>
        ///     Total number of times a table was deleted.
        /// </summary>
        public long TableDeleteTotal => Handle->table_delete_total;

        /// <summary>
        ///     Total number of pipeline builds.
        /// </summary>
        public long PipelineBuildCountTotal => Handle->pipeline_build_count_total;

        /// <summary>
        ///     Total number of systems ran in last frame.
        /// </summary>
        public long SystemsRanFrame => Handle->systems_ran_frame;

        /// <summary>
        ///     Total number of times observer was invoked.
        /// </summary>
        public long ObserversRanFrame => Handle->observers_ran_frame;

        /// <summary>
        ///     Number of tag (No data) ids in the world.
        /// </summary>
        public int TagIdCount => Handle->tag_id_count;

        /// <summary>
        ///     Number of component (data) ids in the world.
        /// </summary>
        public int ComponentIdCount => Handle->component_id_count;

        /// <summary>
        ///     Number of pair ids in the world.
        /// </summary>
        public int PairIdCount => Handle->pair_id_count;

        /// <summary>
        ///     Number of tables.
        /// </summary>
        public int TableCount => Handle->table_count;

        /// <summary>
        ///     Number of tables without entities.
        /// </summary>
        public int EmptyTableCount => Handle->empty_table_count;

        /// <summary>
        ///     Command counts.
        /// </summary>
        public Commands Cmd => new Commands(Handle);

        /// <summary>
        ///     Value set by <see cref="Native.ecs_set_name_prefix"/>
        /// </summary>
        public string NamePrefix => NativeString.GetString(Handle->name_prefix);

        /// <summary>
        ///     Checks if two <see cref="WorldInfo"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(WorldInfo other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="WorldInfo"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is WorldInfo other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="WorldInfo"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="WorldInfo"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(WorldInfo left, WorldInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="WorldInfo"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(WorldInfo left, WorldInfo right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     A wrapper around <see cref="ecs_world_info_t.cmd"/>.
        /// </summary>
        public readonly struct Commands : IEquatable<Commands>
        {
            private readonly ecs_world_info_t* _handle;

            /// <summary>
            ///     The handle to the world info.
            /// </summary>
            public ref readonly ecs_world_info_t* Handle => ref _handle;

            /// <summary>
            ///     Creates a new <see cref="Commands"/> instance with the provided <see cref="ecs_world_info_t"/> pointer.
            /// </summary>
            /// <param name="handle">The handle.</param>
            public Commands(ecs_world_info_t* handle)
            {
                _handle = handle;
            }

            /// <summary>
            ///     Add commands processed.
            /// </summary>
            public long AddCount => Handle->cmd.add_count;

            /// <summary>
            ///     Remove commands processed.
            /// </summary>
            public long RemoveCount => Handle->cmd.remove_count;

            /// <summary>
            ///     Delete commands processed.
            /// </summary>
            public long DeleteCount => Handle->cmd.delete_count;

            /// <summary>
            ///     Clear commands processed.
            /// </summary>
            public long ClearCount => Handle->cmd.clear_count;

            /// <summary>
            ///     Set commands processed.
            /// </summary>
            public long SetCount => Handle->cmd.set_count;

            /// <summary>
            ///     Ensure commands processed.
            /// </summary>
            public long EnsureCount => Handle->cmd.ensure_count;

            /// <summary>
            ///     Modified commands processed.
            /// </summary>
            public long ModifiedCount => Handle->cmd.modified_count;

            /// <summary>
            ///     Commands discarded, happens when entity is no longer alive when running the command.
            /// </summary>
            public long DiscardCount => Handle->cmd.discard_count;

            /// <summary>
            ///     Enqueued custom events.
            /// </summary>
            public long EventCount => Handle->cmd.event_count;

            /// <summary>
            ///     Other commands processed.
            /// </summary>
            public long OtherCount => Handle->cmd.other_count;

            /// <summary>
            ///     Entities for which commands were batched.
            /// </summary>
            public long BatchedEntityCount => Handle->cmd.batched_entity_count;

            /// <summary>
            ///     Commands batched.
            /// </summary>
            public long BatchedCommandCount => Handle->cmd.batched_command_count;

            /// <summary>
            ///     Checks if two <see cref="Commands"/> instances are equal.
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(Commands other)
            {
                return Handle == other.Handle;
            }

            /// <summary>
            ///     Checks if two <see cref="Commands"/> instances are equal.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object? obj)
            {
                return obj is Commands other && Equals(other);
            }

            /// <summary>
            ///     Returns the hash code for the <see cref="Commands"/>.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return Handle->GetHashCode();
            }

            /// <summary>
            ///     Checks if two <see cref="Commands"/> instances are equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator ==(Commands left, Commands right)
            {
                return left.Equals(right);
            }

            /// <summary>
            ///     Checks if two <see cref="Commands"/> instances are not equal.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static bool operator !=(Commands left, Commands right)
            {
                return !(left == right);
            }
        }
    }
}
