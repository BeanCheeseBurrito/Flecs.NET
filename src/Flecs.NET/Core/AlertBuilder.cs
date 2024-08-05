using System;
using Flecs.NET.Core.BindingContext;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_alert_desc_t"/>.
/// </summary>
public unsafe partial struct AlertBuilder : IDisposable, IEquatable<AlertBuilder>
{
    private ecs_world_t* _world;
    private ecs_alert_desc_t _desc;
    private QueryBuilder _queryBuilder;
    private int _severityFilterCount;

    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World => ref _world;

    /// <summary>
    ///     A reference to the alert description.
    /// </summary>
    public ref ecs_alert_desc_t Desc => ref _desc;

    /// <summary>
    ///     A reference to the query builder.
    /// </summary>
    public ref QueryBuilder QueryBuilder => ref _queryBuilder;

    /// <summary>
    ///     Creates an alert builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    public AlertBuilder(ecs_world_t* world)
    {
        _world = world;
        _severityFilterCount = default;
        _desc = default;
        _queryBuilder = new QueryBuilder(world);
    }

    /// <summary>
    ///     Creates an alert builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="entity">The alert entity.</param>
    public AlertBuilder(ecs_world_t* world, ulong entity) : this(world)
    {
        Desc.entity = entity;
    }

    /// <summary>
    ///     Creates an alert builder for the provided world.
    /// </summary>
    /// <param name="world">The world.</param>
    /// <param name="name">The alert name.</param>
    public AlertBuilder(ecs_world_t* world, string name) : this(world)
    {
        if (string.IsNullOrEmpty(name))
            return;

        using NativeString nativeName = (NativeString)name;

        ecs_entity_desc_t desc = default;
        desc.name = nativeName;
        desc.sep = Pointers.DefaultSeparator;
        desc.root_sep = Pointers.DefaultSeparator;
        Desc.entity = ecs_entity_init(World, &desc);
    }

    /// <summary>
    ///     Cleans up native resources. This should be called if the alert builder
    ///     will be discarded and .Build() isn't called.
    /// </summary>
    public void Dispose()
    {
        QueryBuilder.Dispose();
    }

    /// <summary>
    ///     Builds a new alert.
    /// </summary>
    /// <returns></returns>
    public Alert Build()
    {
        fixed (ecs_alert_desc_t* alertDesc = &Desc)
        {
            QueryContext* queryContext = Memory.Alloc<QueryContext>(1);
            queryContext[0] = QueryBuilder.Context;

            alertDesc->query = QueryBuilder.Desc;
            alertDesc->query.binding_ctx = queryContext;
            alertDesc->query.binding_ctx_free = Pointers.QueryContextFree;

            Entity entity = new Entity(World, ecs_alert_init(World, alertDesc));

            if (entity == 0)
                Ecs.Error("Alert failed to init.");

            return new Alert(entity);
        }
    }

    /// <summary>
    ///     Sets the message of the alert.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public ref AlertBuilder Message(string message)
    {
        NativeString nativeMessage = (NativeString)message;
        QueryBuilder.Context.Strings.Add(nativeMessage);

        Desc.message = nativeMessage;
        return ref this;
    }

    /// <summary>
    ///     Sets the brief of the alert.
    /// </summary>
    /// <param name="brief"></param>
    /// <returns></returns>
    public ref AlertBuilder Brief(string brief)
    {
        NativeString nativeBrief = (NativeString)brief;
        QueryBuilder.Context.Strings.Add(nativeBrief);

        Desc.brief = nativeBrief;
        return ref this;
    }

    /// <summary>
    ///     Sets the doc name of the alert.
    /// </summary>
    /// <param name="docName"></param>
    /// <returns></returns>
    public ref AlertBuilder DocName(string docName)
    {
        NativeString nativeDocName = (NativeString)docName;
        QueryBuilder.Context.Strings.Add(nativeDocName);

        Desc.doc_name = nativeDocName;
        return ref this;
    }

    /// <summary>
    ///     Sets the retain period of the alert
    /// </summary>
    /// <param name="period"></param>
    /// <returns></returns>
    public ref AlertBuilder RetainPeriod(float period)
    {
        Desc.retain_period = period;
        return ref this;
    }

    /// <summary>
    ///     Sets the severity of the alert.
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    public ref AlertBuilder Severity(ulong kind)
    {
        Desc.severity = kind;
        return ref this;
    }

    /// <summary>
    ///     Sets the severity of the alert.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref AlertBuilder Severity<T>()
    {
        return ref Severity(Type<T>.Id(World));
    }

    /// <summary>
    ///     Adds a severity filter to the alert.
    /// </summary>
    /// <param name="kind"></param>
    /// <param name="with"></param>
    /// <param name="var"></param>
    /// <returns></returns>
    public ref AlertBuilder SeverityFilter(ulong kind, ulong with, string var = "")
    {
        Ecs.Assert(_severityFilterCount < ECS_ALERT_MAX_SEVERITY_FILTERS,
            "Maximum number of severity filters reached");

        ref ecs_alert_severity_filter_t filter = ref Desc.severity_filters[_severityFilterCount++];
        filter.severity = kind;
        filter.with = with;

        return ref AlertVar(var);
    }

    /// <summary>
    ///     Adds a severity filter to the alert.
    /// </summary>
    /// <param name="with"></param>
    /// <param name="var"></param>
    /// <typeparam name="TSeverity"></typeparam>
    /// <returns></returns>
    public ref AlertBuilder SeverityFilter<TSeverity>(ulong with, string var = "")
    {
        return ref SeverityFilter(Type<TSeverity>.Id(World), with, var);
    }

    /// <summary>
    ///     Adds a severity filter to the alert.
    /// </summary>
    /// <param name="var"></param>
    /// <typeparam name="TSeverity"></typeparam>
    /// <typeparam name="TWith"></typeparam>
    /// <returns></returns>
    public ref AlertBuilder SeverityFilter<TSeverity, TWith>(string var = "")
    {
        return ref SeverityFilter(Type<TSeverity>.Id(World), Type<TWith>.Id(World), var);
    }

    /// <summary>
    ///     Adds a severity filter to the alert.
    /// </summary>
    /// <param name="withEnum"></param>
    /// <param name="var"></param>
    /// <typeparam name="TSeverity"></typeparam>
    /// <typeparam name="TWithEnum"></typeparam>
    /// <returns></returns>
    public ref AlertBuilder SeverityFilter<TSeverity, TWithEnum>(TWithEnum withEnum, string var = "")
        where TWithEnum : Enum
    {
        return ref SeverityFilter(Type<TSeverity>.Id(World), Type<TWithEnum>.Id(World, withEnum), var);
    }

    /// <summary>
    ///     Set the member to create an alert for out of range values.
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public ref AlertBuilder Member(ulong member)
    {
        Desc.member = member;
        return ref this;
    }

    /// <summary>
    ///     Set member to create an alert for out of range of values.
    /// </summary>
    /// <param name="member"></param>
    /// <param name="var"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref AlertBuilder Member<T>(string member, string var = "")
    {
        using NativeString nativeMember = (NativeString)member;

        ulong id = Type<T>.Id(World);
        ulong memberId = ecs_lookup_path_w_sep(World, id, nativeMember,
            Pointers.DefaultSeparator, Pointers.DefaultSeparator, Utils.False);

        AlertVar(var);

        return ref Member(memberId);
    }

    /// <summary>
    ///     Set (component) id for member (optional). If Member() is set and id
    ///     is not set, the id will default to the member parent.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ref AlertBuilder AlertId(ulong id)
    {
        Desc.id = id;
        return ref this;
    }

    /// <summary>
    ///     Set source variable for member.
    /// </summary>
    /// <param name="var"></param>
    /// <returns></returns>
    public ref AlertBuilder AlertVar(string var)
    {
        if (string.IsNullOrEmpty(var))
            return ref this;

        NativeString nativeVar = (NativeString)var;
        QueryBuilder.Context.Strings.Add(nativeVar);
        Desc.var = nativeVar;

        return ref this;
    }

    /// <summary>
    ///     Checks if two <see cref="AlertBuilder"/> instances are equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(AlertBuilder other)
    {
        return Desc == other.Desc && QueryBuilder == other.QueryBuilder;
    }

    /// <summary>
    ///     Checks if two <see cref="AlertBuilder"/> instances are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is AlertBuilder other && Equals(other);
    }

    /// <summary>
    ///     Gets the hash code of the <see cref="AlertBuilder"/>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Desc.GetHashCode(), QueryBuilder.GetHashCode());
    }

    /// <summary>
    ///     Checks if two <see cref="AlertBuilder"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(AlertBuilder left, AlertBuilder right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks if two <see cref="AlertBuilder"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(AlertBuilder left, AlertBuilder right)
    {
        return !(left == right);
    }
}