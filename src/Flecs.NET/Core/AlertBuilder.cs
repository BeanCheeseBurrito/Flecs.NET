using System;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_alert_desc_t.
    /// </summary>
    public unsafe struct AlertBuilder : IDisposable, IEquatable<AlertBuilder>
    {
        private ecs_world_t* _world;
        private NativeList<NativeString> _strings;
        private int _severityFilterCount;

        internal ecs_alert_desc_t AlertDesc;

        /// <summary>
        ///     Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Reference to alert description.
        /// </summary>
        public ref ecs_alert_desc_t Desc => ref AlertDesc;

        /// <summary>
        ///     Creates an alert builder for world.
        /// </summary>
        /// <param name="world"></param>
        public AlertBuilder(ecs_world_t* world)
        {
            AlertDesc = default;
            _world = world;
            _strings = default;
            _severityFilterCount = default;
        }

        /// <summary>
        ///     Cleans up the alert builder's resources.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < _strings.Count; i++)
                _strings[i].Dispose();

            _strings.Dispose();
        }

        /// <summary>
        ///     Sets the message of the alert.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ref AlertBuilder Message(string message)
        {
            NativeString nativeMessage = (NativeString)message;
            _strings.Add(nativeMessage);

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
            _strings.Add(nativeBrief);

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
            _strings.Add(nativeDocName);

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

            return ref Var(var);
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
            return ref SeverityFilter(Type<TSeverity>.Id(World), EnumType<TWithEnum>.Id(withEnum, World), var);
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
                BindingContext.DefaultSeparator, BindingContext.DefaultRootSeparator, Macros.False);

            Var(var);

            return ref Member(memberId);
        }

        /// <summary>
        ///     Set (component) id for member (optional). If Member() is set and id
        ///     is not set, the id will default to the member parent.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref AlertBuilder Id(ulong id)
        {
            Desc.id = id;
            return ref this;
        }

        /// <summary>
        ///     Set source variable for member.
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        public ref AlertBuilder Var(string var)
        {
            if (string.IsNullOrEmpty(var))
                return ref this;

            using NativeString nativeVar = (NativeString)var;
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
            return Equals(Desc, other.Desc);
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
            return AlertDesc.GetHashCode();
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
}
