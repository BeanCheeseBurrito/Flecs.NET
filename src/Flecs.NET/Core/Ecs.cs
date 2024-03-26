using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for storing ECS related globals.
    /// </summary>
    public static partial class Ecs
    {
        /// <summary>
        ///     Default path separator.
        /// </summary>
        public const string DefaultSeparator = ".";

        /// <summary>
        ///     Default path root.
        /// </summary>
        public const string DefaultRootSeparator = "::";
    }

    // TODO: Add proper logging
    // Debug
    public static partial class Ecs
    {
#if NETCOREAPP3_0_OR_GREATER
        /// <summary>
        ///     Debug assert.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="line"></param>
        /// <param name="member"></param>
        /// <param name="file"></param>
        /// <param name="conditionStr"></param>
        [Conditional("DEBUG")]
        public static void Assert(
            bool condition,
            string message = "",
            [CallerLineNumber] int line = default,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerArgumentExpression("condition")] string conditionStr = "")
        {
            if (condition)
                return;

            throw new AssertionException($"\n[Flecs.NET Assertion Failed]: {member}, Line {line}, {file}\n[Condition]: {conditionStr}\n[Assertion Message]: {message}");
        }

        /// <summary>
        ///     Debug assert.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="line"></param>
        /// <param name="member"></param>
        /// <param name="file"></param>
        [Conditional("DEBUG")]
        public static void Error(
            string message = "",
            [CallerLineNumber] int line = default,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "")
        {
            throw new ErrorException($"\n[Flecs.NET Error]: {member}, Line {line}, {file}\n[Error Message]: {message}");
        }
#else
        /// <summary>
        ///     Debug assert.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="line"></param>
        /// <param name="member"></param>
        /// <param name="file"></param>
        [Conditional("DEBUG")]
        public static void Assert(
            bool condition,
            string message = "",
            [CallerLineNumber] int line = default,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "")
        {
            if (condition)
                return;

            throw new AssertionException($"\n[Flecs.NET Assertion Failed]: {member}, Line {line}, {file}\n[Assertion Message]: {message}");
        }

        /// <summary>
        ///     Debug assert.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="line"></param>
        /// <param name="member"></param>
        /// <param name="file"></param>
        [Conditional("DEBUG")]
        public static void Error(
            string message = "",
            [CallerLineNumber] int line = default,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "")
        {
            throw new ErrorException($"\n[Flecs.NET Error]: {member}, Line {line}, {file}\n[Error Message]: {message}");
        }
#endif

        /// <summary>
        ///     Flecs.NET assertion exception.
        /// </summary>
        [Serializable]
        public class AssertionException : Exception
        {
            /// <summary>
            ///
            /// </summary>
            public AssertionException()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            public AssertionException(string message) : base(message)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            /// <param name="innerException"></param>
            public AssertionException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

        /// <summary>
        ///     Flecs.NET error exception.
        /// </summary>
        [Serializable]
        public class ErrorException : Exception
        {
            /// <summary>
            ///
            /// </summary>
            public ErrorException()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            public ErrorException(string message) : base(message)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            /// <param name="innerException"></param>
            public ErrorException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

        /// <summary>
        ///     Flecs native exception.
        /// </summary>
        [Serializable]
        public class NativeException : Exception
        {
            /// <summary>
            ///
            /// </summary>
            public NativeException()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            public NativeException(string message) : base(message)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            /// <param name="innerException"></param>
            public NativeException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }

    // Log namespace
    public static unsafe partial class Ecs
    {
        /// <summary>
        ///     Static class for flecs logging functions.
        /// </summary>
        public static class Log
        {
            /// <summary>
            ///     Set log level.
            /// </summary>
            /// <param name="level"></param>
            public static void SetLevel(int level)
            {
                _ = ecs_log_set_level(level);
            }

            /// <summary>
            ///     Get log level.
            /// </summary>
            /// <returns></returns>
            public static int GetLevel()
            {
                return ecs_log_get_level();
            }

            /// <summary>
            ///     Enable colors in logging.
            /// </summary>
            /// <param name="enabled"></param>
            public static void EnableColors(bool enabled = true)
            {
                ecs_log_enable_colors(Macros.Bool(enabled));
            }

            /// <summary>
            ///     Enable timestamps in logging.
            /// </summary>
            /// <param name="enabled"></param>
            public static void EnableTimestamp(bool enabled = true)
            {
                ecs_log_enable_timestamp(Macros.Bool(enabled));
            }

            /// <summary>
            ///     Enable time delta in logging.
            /// </summary>
            /// <param name="enabled"></param>
            public static void EnableTimeDelta(bool enabled = true)
            {
                ecs_log_enable_timedelta(Macros.Bool(enabled));
            }

            /// <summary>
            ///     Debug trace (Level 1)
            /// </summary>
            /// <param name="message"></param>
            /// <param name="file"></param>
            /// <param name="line"></param>
            public static void Dbg(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
            {
                using NativeString nativeMessage = (NativeString)message;
                using NativeString nativeFile = (NativeString)file;
                ecs_log_(1, nativeFile, line, nativeMessage);
            }

            /// <summary>
            ///     Trace (Level 0)
            /// </summary>
            /// <param name="message"></param>
            /// <param name="file"></param>
            /// <param name="line"></param>
            public static void Trace(string message = "", [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0)
            {
                using NativeString nativeMessage = (NativeString)message;
                using NativeString nativeFile = (NativeString)file;
                ecs_log_(0, nativeFile, line, nativeMessage);
            }

            /// <summary>
            ///     Trace (Level -2)
            /// </summary>
            /// <param name="message"></param>
            /// <param name="file"></param>
            /// <param name="line"></param>
            public static void Warn(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
            {
                using NativeString nativeMessage = (NativeString)message;
                using NativeString nativeFile = (NativeString)file;
                ecs_log_(-2, nativeFile, line, nativeMessage);
            }

            /// <summary>
            ///     Trace (Level -3)
            /// </summary>
            /// <param name="message"></param>
            /// <param name="file"></param>
            /// <param name="line"></param>
            public static void Err(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
            {
                using NativeString nativeMessage = (NativeString)message;
                using NativeString nativeFile = (NativeString)file;
                ecs_log_(-3, nativeFile, line, nativeMessage);
            }

            /// <summary>
            ///     Trace (Level 0)
            /// </summary>
            /// <param name="message"></param>
            /// <param name="file"></param>
            /// <param name="line"></param>
            public static void Push(string message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
            {
                using NativeString nativeMessage = (NativeString)message;
                using NativeString nativeFile = (NativeString)file;
                ecs_log_(1, nativeFile, line, nativeMessage);
                ecs_log_push_(0);
            }

            /// <summary>
            ///     Increase log indentation.
            /// </summary>
            public static void Push()
            {
                ecs_log_push_(0);
            }

            /// <summary>
            ///     Decrease log indentation.
            /// </summary>
            public static void Pop()
            {
                ecs_log_pop_(0);
            }
        }
    }

    // Delegates
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
        ///     Copy type hook callback.
        /// </summary>
        public delegate void CopyCallback(void* src, void* dst, int count, ecs_type_info_t* typeInfo);

        /// <summary>
        ///     Copy type hook callback.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public delegate void CopyCallback<T>(ref T src, ref T dst, TypeInfo typeInfo);

        /// <summary>
        ///     Ctor type hook callback.
        /// </summary>
        public delegate void CtorCallback(void* data, int count, ecs_type_info_t* typeInfo);

        /// <summary>
        ///     Ctor type hook callback.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public delegate void CtorCallback<T>(ref T data, TypeInfo typeInfo);

        /// <summary>
        ///     Dtor type hook callback.
        /// </summary>
        public delegate void DtorCallback(void* data, int count, ecs_type_info_t* typeInfo);

        /// <summary>
        ///     Dtor type hook callback.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public delegate void DtorCallback<T>(ref T data, TypeInfo typeInfo);

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
        ///     Iter callback.
        /// </summary>
        public delegate void IterCallback(Iter it);

        /// <summary>
        ///     Iter next action.
        /// </summary>
        public delegate byte IterNextAction(ecs_iter_t* it);

        /// <summary>
        ///     Move type hook callback.
        /// </summary>
        public delegate void MoveCallback(void* src, void* dst, int count, ecs_type_info_t* typeInfo);

        /// <summary>
        ///     Move type hook callback.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public delegate void MoveCallback<T>(ref T src, ref T dst, TypeInfo typeInfo);

        /// <summary>
        ///     OrderBy action.
        /// </summary>
        public delegate int OrderByAction(ulong e1, void* ptr1, ulong e2, void* ptr2);

        /// <summary>
        ///     A callback that takes a reference to a world.
        /// </summary>
        public delegate void WorldCallback(ref World world);

        /// <summary>
        ///     A callback that takes a reference to a term.
        /// </summary>
        public delegate void TermCallback(ref Term term);
    }

    // Built-in global entities, tags, and flags.
    public static partial class Ecs
    {
        // Enums

        /// <summary>
        ///     Equivalent to <see cref="EcsInOutDefault"/>
        /// </summary>
        public const ecs_inout_kind_t InOutDefault = EcsInOutDefault;

        /// <summary>
        ///     Equivalent to <see cref="EcsInOutNone"/>
        /// </summary>
        public const ecs_inout_kind_t InOutNone = EcsInOutNone;

        /// <summary>
        ///     Equivalent to <see cref="EcsOut"/>
        /// </summary>
        public const ecs_inout_kind_t InOut = EcsOut;

        /// <summary>
        ///     Equivalent to <see cref="EcsIn"/>
        /// </summary>
        public const ecs_inout_kind_t In = EcsIn;

        /// <summary>
        ///     Equivalent to <see cref="EcsOut"/>
        /// </summary>
        public const ecs_inout_kind_t Out = EcsOut;

        /// <summary>
        ///     Equivalent to <see cref="EcsAnd"/>
        /// </summary>
        public const ecs_oper_kind_t And = EcsAnd;

        /// <summary>
        ///     Equivalent to <see cref="EcsOr"/>
        /// </summary>
        public const ecs_oper_kind_t Or = EcsOr;

        /// <summary>
        ///     Equivalent to <see cref="EcsNot"/>
        /// </summary>
        public const ecs_oper_kind_t Not = EcsNot;

        /// <summary>
        ///     Equivalent to <see cref="EcsOptional"/>
        /// </summary>
        public const ecs_oper_kind_t Optional = EcsOptional;

        /// <summary>
        ///     Equivalent to <see cref="EcsAndFrom"/>
        /// </summary>
        public const ecs_oper_kind_t AndFrom = EcsAndFrom;

        /// <summary>
        ///     Equivalent to <see cref="EcsOrFrom"/>
        /// </summary>
        public const ecs_oper_kind_t OrFrom = EcsOrFrom;

        /// <summary>
        ///     Equivalent to <see cref="EcsNotFrom"/>
        /// </summary>
        public const ecs_oper_kind_t NotFrom = EcsNotFrom;

        // Built-in term flags

        /// <summary>
        ///     Equivalent to <see cref="EcsSelf"/>
        /// </summary>
        public const uint Self = EcsSelf;

        /// <summary>
        ///     Equivalent to <see cref="EcsUp"/>
        /// </summary>
        public const uint Up = EcsUp;

        /// <summary>
        ///     Equivalent to <see cref="EcsDown"/>
        /// </summary>
        public const uint Down = EcsDown;

        /// <summary>
        ///     Equivalent to <see cref="EcsCascade"/>
        /// </summary>
        public const uint Cascade = EcsCascade;

        /// <summary>
        ///     Equivalent to <see cref="EcsDesc"/>
        /// </summary>
        public const uint Desc = EcsDesc;

        /// <summary>
        ///     Equivalent to <see cref="EcsParent"/>
        /// </summary>
        public const uint Parent = EcsParent;

        /// <summary>
        ///     Equivalent to <see cref="EcsIsVariable"/>
        /// </summary>
        public const uint IsVariable = EcsIsVariable;

        /// <summary>
        ///     Equivalent to <see cref="EcsIsEntity"/>
        /// </summary>
        public const uint IsEntity = EcsIsEntity;

        /// <summary>
        ///     Equivalent to <see cref="EcsFilter"/>
        /// </summary>
        public const uint Filter = EcsFilter;

        /// <summary>
        ///     Equivalent to <see cref="EcsTraverseFlags"/>
        /// </summary>
        public const uint TraverseFlags = EcsTraverseFlags;

        // Build-in id flags

        /// <summary>
        ///     Reference to <see cref="ECS_PAIR"/>.
        /// </summary>
        public static ref ulong Pair => ref ECS_PAIR;

        /// <summary>
        ///     Reference to <see cref="ECS_OVERRIDE"/>.
        /// </summary>
        public static ref ulong Override => ref ECS_OVERRIDE;

        /// <summary>
        ///     Reference to <see cref="ECS_TOGGLE"/>.
        /// </summary>
        public static ref ulong Toggle => ref ECS_TOGGLE;

        // Built-in tags

        /// <summary>
        ///     Reference to <see cref="EcsQuery"/>.
        /// </summary>
        public static ref ulong Query => ref EcsQuery;

        /// <summary>
        ///     Reference to <see cref="EcsObserver"/>.
        /// </summary>
        public static ref ulong Observer => ref EcsObserver;

        /// <summary>
        ///     Reference to <see cref="EcsPrivate"/>.
        /// </summary>
        public static ref ulong Private => ref EcsPrivate;

        /// <summary>
        ///     Reference to <see cref="EcsModule"/>.
        /// </summary>
        public static ref ulong Module => ref EcsModule;

        /// <summary>
        ///     Reference to <see cref="EcsPrefab"/>.
        /// </summary>
        public static ref ulong Prefab => ref EcsPrefab;

        /// <summary>
        ///     Reference to <see cref="EcsDisabled"/>.
        /// </summary>
        public static ref ulong Disabled => ref EcsDisabled;

        /// <summary>
        ///     Reference to <see cref="EcsEmpty"/>.
        /// </summary>
        public static ref ulong Empty => ref EcsEmpty;

        /// <summary>
        ///     Reference to <see cref="EcsMonitor"/>.
        /// </summary>
        public static ref ulong MonitorId => ref EcsMonitor;

        /// <summary>
        ///     Reference to <see cref="EcsSystem"/>.
        /// </summary>
        public static ref ulong System => ref EcsSystem;

        /// <summary>
        ///     Reference to <see cref="EcsSystem"/>.
        /// </summary>
        public static ref ulong Routine => ref EcsSystem;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDEcsPipelineID_"/>.
        /// </summary>
        public static ref ulong Pipeline => ref FLECS_IDEcsPipelineID_;

        /// <summary>
        ///     Reference to <see cref="EcsPhase"/>.
        /// </summary>
        public static ref ulong Phase => ref EcsPhase;

        // Built-in event tags

        /// <summary>
        ///     Reference to <see cref="EcsOnAdd"/>.
        /// </summary>
        public static ref ulong OnAdd => ref EcsOnAdd;

        /// <summary>
        ///     Reference to <see cref="EcsOnRemove"/>.
        /// </summary>
        public static ref ulong OnRemove => ref EcsOnRemove;

        /// <summary>
        ///     Reference to <see cref="EcsOnSet"/>.
        /// </summary>
        public static ref ulong OnSet => ref EcsOnSet;

        /// <summary>
        ///     Reference to <see cref="EcsUnSet"/>.
        /// </summary>
        public static ref ulong UnSet => ref EcsUnSet;

        /// <summary>
        ///     Reference to <see cref="EcsOnTableCreate"/>.
        /// </summary>
        public static ref ulong OnTableCreate => ref EcsOnTableCreate;

        /// <summary>
        ///     Reference to <see cref="EcsOnTableDelete"/>.
        /// </summary>
        public static ref ulong OnTableDelete => ref EcsOnTableDelete;

        // Built-in entity ids

        /// <summary>
        ///     Reference to <see cref="EcsFlecs"/>.
        /// </summary>
        public static ref ulong Flecs => ref EcsFlecs;

        /// <summary>
        ///     Reference to <see cref="EcsFlecsCore"/>.
        /// </summary>
        public static ref ulong FlecsCore => ref EcsFlecsCore;

        /// <summary>
        ///     Reference to <see cref="EcsWorld"/>.
        /// </summary>
        public static ref ulong World => ref EcsWorld;

        // Relationship properties

        /// <summary>
        ///     Reference to <see cref="EcsWildcard"/>.
        /// </summary>
        public static ref ulong Wildcard => ref EcsWildcard;

        /// <summary>
        ///     Reference to <see cref="EcsAny"/>.
        /// </summary>
        public static ref ulong Any => ref EcsAny;

        /// <summary>
        ///     Reference to <see cref="EcsThis"/>.
        /// </summary>
        public static ref ulong This => ref EcsThis;

        /// <summary>
        ///     Reference to <see cref="EcsTransitive"/>.
        /// </summary>
        public static ref ulong Transitive => ref EcsTransitive;

        /// <summary>
        ///     Reference to <see cref="EcsReflexive"/>.
        /// </summary>
        public static ref ulong Reflexive => ref EcsReflexive;

        /// <summary>
        ///     Reference to <see cref="EcsFinal"/>.
        /// </summary>
        public static ref ulong Final => ref EcsFinal;

        /// <summary>
        ///     Reference to <see cref="EcsDontInherit"/>.
        /// </summary>
        public static ref ulong DontInherit => ref EcsDontInherit;

        /// <summary>
        ///     Reference to <see cref="EcsAlwaysOverride"/>.
        /// </summary>
        public static ref ulong AlwaysOverride => ref EcsAlwaysOverride;

        /// <summary>
        ///     Reference to <see cref="EcsTag"/>.
        /// </summary>
        public static ref ulong Tag => ref EcsTag;

        /// <summary>
        ///     Reference to <see cref="EcsUnion"/>.
        /// </summary>
        public static ref ulong Union => ref EcsUnion;

        /// <summary>
        ///     Reference to <see cref="EcsExclusive"/>.
        /// </summary>
        public static ref ulong Exclusive => ref EcsExclusive;

        /// <summary>
        ///     Reference to <see cref="EcsAcyclic"/>.
        /// </summary>
        public static ref ulong Acyclic => ref EcsAcyclic;

        /// <summary>
        ///     Reference to <see cref="EcsTraversable"/>.
        /// </summary>
        public static ref ulong Traversable => ref EcsTraversable;

        /// <summary>
        ///     Reference to <see cref="EcsSymmetric"/>.
        /// </summary>
        public static ref ulong Symmetric => ref EcsSymmetric;

        /// <summary>
        ///     Reference to <see cref="EcsWith"/>.
        /// </summary>
        public static ref ulong With => ref EcsWith;

        /// <summary>
        ///     Reference to <see cref="EcsOneOf"/>.
        /// </summary>
        public static ref ulong OneOf => ref EcsOneOf;

        // Built-in relationships

        /// <summary>
        ///     Reference to <see cref="EcsIsA"/>.
        /// </summary>
        public static ref ulong IsA => ref EcsIsA;

        /// <summary>
        ///     Reference to <see cref="EcsChildOf"/>.
        /// </summary>
        public static ref ulong ChildOf => ref EcsChildOf;

        /// <summary>
        ///     Reference to <see cref="EcsDependsOn"/>.
        /// </summary>
        public static ref ulong DependsOn => ref EcsDependsOn;

        /// <summary>
        ///     Reference to <see cref="EcsSlotOf"/>.
        /// </summary>
        public static ref ulong SlotOf => ref EcsSlotOf;

        // Built-in identifiers

        /// <summary>
        ///     Reference to <see cref="EcsName"/>.
        /// </summary>
        public static ref ulong Name => ref EcsName;

        /// <summary>
        ///     Reference to <see cref="EcsSymbol"/>.
        /// </summary>
        public static ref ulong Symbol => ref EcsSymbol;

        // Cleanup policies

        /// <summary>
        ///     Reference to <see cref="EcsOnDelete"/>.
        /// </summary>
        public static ref ulong OnDelete => ref EcsOnDelete;

        /// <summary>
        ///     Reference to <see cref="EcsOnDeleteTarget"/>.
        /// </summary>
        public static ref ulong OnDeleteTarget => ref EcsOnDeleteTarget;

        /// <summary>
        ///     Reference to <see cref="EcsRemove"/>.
        /// </summary>
        public static ref ulong Remove => ref EcsRemove;

        /// <summary>
        ///     Reference to <see cref="EcsDelete"/>.
        /// </summary>
        public static ref ulong Delete => ref EcsDelete;

        /// <summary>
        ///     Reference to <see cref="EcsPanic"/>.
        /// </summary>
        public static ref ulong Panic => ref EcsPanic;

        // Misc

        /// <summary>
        ///     Reference to <see cref="EcsFlatten"/>.
        /// </summary>
        public static ref ulong Flatten => ref EcsFlatten;

        /// <summary>
        ///     Reference to <see cref="EcsDefaultChildComponent"/>.
        /// </summary>
        public static ref ulong DefaultChildComponent => ref EcsDefaultChildComponent;

        // Built-in predicates for comparing entity ids in queries. Only supported by rules.

        /// <summary>
        ///     Reference to <see cref="EcsPredEq"/>.
        /// </summary>
        public static ref ulong PredEq => ref EcsPredEq;

        /// <summary>
        ///     Reference to <see cref="EcsPredMatch"/>.
        /// </summary>
        public static ref ulong PredMatch => ref EcsPredMatch;

        /// <summary>
        ///     Reference to <see cref="EcsPredLookup"/>.
        /// </summary>
        public static ref ulong PredLookup => ref EcsPredLookup;

        // Built-in marker entities for query scopes

        /// <summary>
        ///     Reference to <see cref="EcsScopeOpen"/>.
        /// </summary>
        public static ref ulong ScopeOpen => ref EcsScopeOpen;

        /// <summary>
        ///     Reference to <see cref="EcsScopeClose"/>.
        /// </summary>
        public static ref ulong ScopeClose => ref EcsScopeClose;

        // Built-in pipeline tags

        /// <summary>
        ///     Reference to <see cref="EcsOnStart"/>.
        /// </summary>
        public static ref ulong OnStart => ref EcsOnStart;

        /// <summary>
        ///     Reference to <see cref="EcsPreFrame"/>.
        /// </summary>
        public static ref ulong PreFrame => ref EcsPreFrame;

        /// <summary>
        ///     Reference to <see cref="EcsOnLoad"/>.
        /// </summary>
        public static ref ulong OnLoad => ref EcsOnLoad;

        /// <summary>
        ///     Reference to <see cref="EcsPostLoad"/>.
        /// </summary>
        public static ref ulong PostLoad => ref EcsPostLoad;

        /// <summary>
        ///     Reference to <see cref="EcsPreUpdate"/>.
        /// </summary>
        public static ref ulong PreUpdate => ref EcsPreUpdate;

        /// <summary>
        ///     Reference to <see cref="EcsOnUpdate"/>.
        /// </summary>
        public static ref ulong OnUpdate => ref EcsOnUpdate;

        /// <summary>
        ///     Reference to <see cref="EcsOnValidate"/>.
        /// </summary>
        public static ref ulong OnValidate => ref EcsOnValidate;

        /// <summary>
        ///     Reference to <see cref="EcsPostUpdate"/>.
        /// </summary>
        public static ref ulong PostUpdate => ref EcsPostUpdate;

        /// <summary>
        ///     Reference to <see cref="EcsPreStore"/>.
        /// </summary>
        public static ref ulong PreStore => ref EcsPreStore;

        /// <summary>
        ///     Reference to <see cref="EcsOnStore"/>.
        /// </summary>
        public static ref ulong OnStore => ref EcsOnStore;

        /// <summary>
        ///     Reference to <see cref="EcsPostFrame"/>.
        /// </summary>
        public static ref ulong PostFrame => ref EcsPostFrame;

        // Built-in reflection types

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_bool_tID_"/>.
        /// </summary>
        public static ref ulong Bool => ref FLECS_IDecs_bool_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_char_tID_"/>.
        /// </summary>
        public static ref ulong Char => ref FLECS_IDecs_char_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_byte_tID_"/>.
        /// </summary>
        public static ref ulong Byte => ref FLECS_IDecs_byte_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_u8_tID_"/>.
        /// </summary>
        public static ref ulong U8 => ref FLECS_IDecs_u8_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_u16_tID_"/>.
        /// </summary>
        public static ref ulong U16 => ref FLECS_IDecs_u16_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_u32_tID_"/>.
        /// </summary>
        public static ref ulong U32 => ref FLECS_IDecs_u32_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_u64_tID_"/>.
        /// </summary>
        public static ref ulong U64 => ref FLECS_IDecs_u64_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_uptr_tID_"/>.
        /// </summary>
        public static ref ulong Uptr => ref FLECS_IDecs_uptr_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_i8_tID_"/>.
        /// </summary>
        public static ref ulong I8 => ref FLECS_IDecs_i8_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_i16_tID_"/>.
        /// </summary>
        public static ref ulong I16 => ref FLECS_IDecs_i16_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_i32_tID_"/>.
        /// </summary>
        public static ref ulong I32 => ref FLECS_IDecs_i32_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_i64_tID_"/>.
        /// </summary>
        public static ref ulong I64 => ref FLECS_IDecs_i64_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_iptr_tID_"/>.
        /// </summary>
        public static ref ulong Iptr => ref FLECS_IDecs_iptr_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_f32_tID_"/>.
        /// </summary>
        public static ref ulong F32 => ref FLECS_IDecs_f32_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_f64_tID_"/>.
        /// </summary>
        public static ref ulong F64 => ref FLECS_IDecs_f64_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_string_tID_"/>.
        /// </summary>
        public static ref ulong String => ref FLECS_IDecs_string_tID_;

        /// <summary>
        ///     Reference to <see cref="FLECS_IDecs_entity_tID_"/>.
        /// </summary>
        public static ref ulong Entity => ref FLECS_IDecs_entity_tID_;

        /// <summary>
        ///     Reference to <see cref="EcsConstant"/>.
        /// </summary>
        public static ref ulong Constant => ref EcsConstant;

        /// <summary>
        ///     Reference to <see cref="EcsQuantity"/>.
        /// </summary>
        public static ref ulong Quantity => ref EcsQuantity;
    }

    // Static classes
    public static partial class Ecs
    {
        /// <summary>
        ///     Utilities for documenting entities, components and systems.
        /// </summary>
        public static class Doc
        {
            /// <summary>
            ///     Reference to <see cref="EcsDocBrief"/>.
            /// </summary>
            public static ref ulong Brief => ref EcsDocBrief;

            /// <summary>
            ///     Reference to <see cref="EcsDocDetail"/>.
            /// </summary>
            public static ref ulong Detail => ref EcsDocDetail;

            /// <summary>
            ///     Reference to <see cref="EcsDocLink"/>.
            /// </summary>
            public static ref ulong Link => ref EcsDocLink;

            /// <summary>
            ///     Reference to <see cref="EcsDocColor"/>.
            /// </summary>
            public static ref ulong Color => ref EcsDocColor;
        }
    }
}
