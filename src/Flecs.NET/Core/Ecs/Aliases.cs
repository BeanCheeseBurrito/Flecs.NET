#pragma warning disable CA1707, CA1708

using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

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
    ///     Equivalent to <see cref="EcsInOutFilter"/>
    /// </summary>
    public const ecs_inout_kind_t InOutFilter = EcsInOutFilter;

    /// <summary>
    ///     Equivalent to <see cref="EcsInOut"/>
    /// </summary>
    public const ecs_inout_kind_t InOut = EcsInOut;

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

    /// <summary>
    ///     Equivalent to <see cref="EcsQueryCacheDefault"/>
    /// </summary>
    public const ecs_query_cache_kind_t QueryCacheDefault = EcsQueryCacheDefault;

    /// <summary>
    ///     Equivalent to <see cref="EcsQueryCacheAuto"/>
    /// </summary>
    public const ecs_query_cache_kind_t QueryCacheAuto = EcsQueryCacheAuto;

    /// <summary>
    ///     Equivalent to <see cref="EcsQueryCacheAll"/>
    /// </summary>
    public const ecs_query_cache_kind_t QueryCacheAll = EcsQueryCacheAll;

    /// <summary>
    ///     Equivalent to <see cref="EcsQueryCacheNone"/>
    /// </summary>
    public const ecs_query_cache_kind_t QueryCacheNone = EcsQueryCacheNone;


    // Built-in term flags

    /// <summary>
    ///     Equivalent to <see cref="EcsSelf"/>
    /// </summary>
    public const ulong Self = EcsSelf;

    /// <summary>
    ///     Equivalent to <see cref="EcsUp"/>
    /// </summary>
    public const ulong Up = EcsUp;

    /// <summary>
    ///     Equivalent to <see cref="EcsTrav"/>
    /// </summary>
    public const ulong Trav = EcsTrav;

    /// <summary>
    ///     Equivalent to <see cref="EcsCascade"/>
    /// </summary>
    public const ulong Cascade = EcsCascade;

    /// <summary>
    ///     Equivalent to <see cref="EcsDesc"/>
    /// </summary>
    public const ulong Desc = EcsDesc;

    /// <summary>
    ///     Equivalent to <see cref="EcsIsVariable"/>
    /// </summary>
    public const ulong IsVariable = EcsIsVariable;

    /// <summary>
    ///     Equivalent to <see cref="EcsIsEntity"/>
    /// </summary>
    public const ulong IsEntity = EcsIsEntity;

    /// <summary>
    ///     Equivalent to <see cref="EcsIsName"/>
    /// </summary>
    public const ulong IsName = EcsIsName;

    /// <summary>
    ///     Equivalent to <see cref="EcsTraverseFlags"/>
    /// </summary>
    public const ulong TraverseFlags = EcsTraverseFlags;

    /// <summary>
    ///     Equivalent to <see cref="EcsTermRefFlags"/>
    /// </summary>
    public const ulong TermRefFlags = EcsTermRefFlags;

    // Id bit flags

    /// <summary>
    ///     Reference to <see cref="ECS_PAIR"/>.
    /// </summary>
    public static ref ulong PAIR => ref ECS_PAIR;

    /// <summary>
    ///     Reference to <see cref="ECS_AUTO_OVERRIDE"/>.
    /// </summary>
    public static ref ulong AUTO_OVERRIDE => ref ECS_AUTO_OVERRIDE;

    /// <summary>
    ///     Reference to <see cref="ECS_TOGGLE"/>.
    /// </summary>
    public static ref ulong TOGGLE => ref ECS_TOGGLE;

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
    public static ref ulong Monitor => ref EcsMonitor;

    /// <summary>
    ///     Reference to <see cref="EcsSystem"/>.
    /// </summary>
    public static ref ulong System => ref EcsSystem;

    /// <summary>
    ///     Reference to <see cref="FLECS_IDEcsPipelineID_"/>.
    /// </summary>
    public static ref ulong Pipeline => ref FLECS_IDEcsPipelineID_;

    /// <summary>
    ///     Reference to <see cref="EcsPhase"/>.
    /// </summary>
    public static ref ulong Phase => ref EcsPhase;

    /// <summary>
    ///     Reference to <see cref="EcsConstant"/>.
    /// </summary>
    public static ref ulong Constant => ref EcsConstant;

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

    // Component Traits

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
    ///     Reference to <see cref="EcsInheritable"/>.
    /// </summary>
    public static ref ulong Inheritable => ref EcsInheritable;

    /// <summary>
    ///     Reference to <see cref="EcsPairIsTag"/>.
    /// </summary>
    public static ref ulong PairIsTag => ref EcsPairIsTag;

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

    /// <summary>
    ///     Reference to <see cref="EcsTrait"/>.
    /// </summary>
    public static ref ulong Trait => ref EcsTrait;

    /// <summary>
    ///     Reference to <see cref="EcsRelationship"/>.
    /// </summary>
    public static ref ulong Relationship => ref EcsRelationship;

    /// <summary>
    ///     Reference to <see cref="EcsTarget"/>.
    /// </summary>
    public static ref ulong Target => ref EcsTarget;

    /// <summary>
    ///     Reference to <see cref="EcsCanToggle"/>.
    /// </summary>
    public static ref ulong CanToggle => ref EcsCanToggle;

    // OnInstantiate Trait

    /// <summary>
    ///     Reference to <see cref="EcsOnInstantiate"/>.
    /// </summary>
    public static ref ulong OnInstantiate => ref EcsOnInstantiate;

    /// <summary>
    ///     Reference to <see cref="EcsOverride"/>.
    /// </summary>
    public static ref ulong Override => ref EcsOverride;

    /// <summary>
    ///     Reference to <see cref="EcsInherit"/>.
    /// </summary>
    public static ref ulong Inherit => ref EcsInherit;

    /// <summary>
    ///     Reference to <see cref="EcsDontInherit"/>.
    /// </summary>
    public static ref ulong DontInherit => ref EcsDontInherit;

    // OnDelete/OnDeleteTarget Traits

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

    // Storage

    /// <summary>
    ///     Reference to <see cref="EcsSparse"/>.
    /// </summary>
    public static ref ulong Sparse => ref EcsSparse;

    /// <summary>
    ///     Reference to <see cref="EcsUnion"/>.
    /// </summary>
    public static ref ulong Union => ref EcsUnion;

    // Built-in predicates for comparing entity ids in queries.

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
    ///     Reference to <see cref="EcsQuantity"/>.
    /// </summary>
    public static ref ulong Quantity => ref EcsQuantity;

    /// <summary>
    ///     Query must match prefabs.
    /// </summary>
    public const uint QueryMatchPrefab = EcsQueryMatchPrefab;

    /// <summary>
    ///     Query must match disabled entities.
    /// </summary>
    public const uint QueryMatchDisabled = EcsQueryMatchDisabled;

    /// <summary>
    ///     Query must match empty tables.
    /// </summary>
    public const uint QueryMatchEmptyTables = EcsQueryMatchEmptyTables;

    /// <summary>
    ///     Query may have unresolved entity identifiers.
    /// </summary>
    public const uint QueryAllowUnresolvedByName = EcsQueryAllowUnresolvedByName;

    /// <summary>
    ///     Query only returns whole tables (ignores toggle/member fields).
    /// </summary>
    public const uint QueryTableOnly = EcsQueryTableOnly;
}

#pragma warning restore CA1707, CA1708
