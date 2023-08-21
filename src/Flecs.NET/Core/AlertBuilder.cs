using System;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct AlertBuilder : IDisposable
    {
        public ecs_world_t* World { get; }

        internal ecs_alert_desc_t AlertDesc;
        internal UnsafeList<NativeString> Strings;

        private int _severityFilterCount;

        public AlertBuilder(ecs_world_t* world)
        {
            World = world;
            AlertDesc = default;
            Strings = default;
            _severityFilterCount = default;
        }

        public void Dispose()
        {
            for (int i = 0; i < Strings.Count; i++)
                Strings[i].Dispose();

            Strings.Dispose();
        }

        public ref AlertBuilder Message(string message)
        {
            NativeString nativeMessage = (NativeString)message;
            Strings.Add(nativeMessage);

            AlertDesc.message = nativeMessage;
            return ref this;
        }

        public ref AlertBuilder Brief(string brief)
        {
            NativeString nativeBrief = (NativeString)brief;
            Strings.Add(nativeBrief);

            AlertDesc.brief = nativeBrief;
            return ref this;
        }

        public ref AlertBuilder DocName(string docName)
        {
            NativeString nativeDocName = (NativeString)docName;
            Strings.Add(nativeDocName);

            AlertDesc.doc_name = nativeDocName;
            return ref this;
        }

        public ref AlertBuilder RetainPeriod(float period)
        {
            AlertDesc.retain_period = period;
            return ref this;
        }

        public ref AlertBuilder Severity(ulong kind)
        {
            AlertDesc.severity = kind;
            return ref this;
        }

        public ref AlertBuilder Severity<T>()
        {
            return ref Severity(Type<T>.Id(World));
        }

        public ref AlertBuilder SeverityFilter(ulong kind, ulong with, string var = "")
        {
            Assert.True(_severityFilterCount < ECS_ALERT_MAX_SEVERITY_FILTERS, "Maxium number of severity filters reached");

            ref ecs_alert_severity_filter_t filter = ref AlertDesc.severity_filters[_severityFilterCount++];
            filter.severity = kind;
            filter.with = with;

            return ref Var(var);
        }

        public ref AlertBuilder SeverityFilter<TSeverity>(ulong with, string var = "")
        {
            return ref SeverityFilter(Type<TSeverity>.Id(World), with, var);
        }

        public ref AlertBuilder SeverityFilter<TSeverity, TWith>(string var = "")
        {
            return ref SeverityFilter(Type<TSeverity>.Id(World), Type<TWith>.Id(World), var);
        }

        public ref AlertBuilder SeverityFilter<TSeverity, TWithEnum>(TWithEnum withEnum, string var = "") where TWithEnum : Enum
        {
            return ref SeverityFilter(Type<TSeverity>.Id(World), EnumType<TWithEnum>.Id(withEnum, World), var);
        }

        public ref AlertBuilder Member(ulong member)
        {
            AlertDesc.member = member;
            return ref this;
        }

        public ref AlertBuilder Id(ulong id)
        {
            AlertDesc.id = id;
            return ref this;
        }

        public ref AlertBuilder Id<T>(string member, string var = "")
        {
            using NativeString nativeMember = (NativeString)member;
            using NativeString nativeSep = (NativeString)"::";

            ulong id = Type<T>.Id(World);
            ulong memberId = ecs_lookup_path_w_sep(World, id, nativeMember, nativeSep, nativeSep, Macros.False);

            Var(var);

            return ref Member(memberId);
        }

        public ref AlertBuilder Var(string var)
        {
            if (string.IsNullOrEmpty(var))
                return ref this;

            using NativeString nativeVar = (NativeString)var;
            AlertDesc.var = nativeVar;

            return ref this;
        }

    }
}
