using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_alert_desc_t.
    /// </summary>
    public unsafe partial struct AlertBuilder : IDisposable, IEquatable<AlertBuilder>
    {
        private ecs_world_t* _world;
        private int _severityFilterCount;

        internal ecs_alert_desc_t AlertDesc;
        internal QueryBuilder QueryBuilder;

        /// <summary>
        ///     Reference to world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Reference to alert description.
        /// </summary>
        public ref ecs_alert_desc_t Desc => ref AlertDesc;

        /// <summary>
        ///     Creates an alert builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        public AlertBuilder(ecs_world_t* world)
        {
            _world = world;
            _severityFilterCount = default;
            AlertDesc = default;
            QueryBuilder = new QueryBuilder(world);
        }

        /// <summary>
        ///     Creates an alert builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="entity">The alert entity.</param>
        public AlertBuilder(ecs_world_t* world, ulong entity) : this(world)
        {
            AlertDesc.entity = entity;
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
            desc.sep = BindingContext.DefaultSeparator;
            desc.root_sep = BindingContext.DefaultSeparator;
            AlertDesc.entity = ecs_entity_init(World, &desc);
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
            fixed (ecs_alert_desc_t* alertDesc = &AlertDesc)
            {
                BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
                queryContext[0] = QueryBuilder.Context;

                alertDesc->query = QueryBuilder.Desc;
                alertDesc->query.binding_ctx = queryContext;
                alertDesc->query.binding_ctx_free = BindingContext.QueryContextFreePointer;

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
                BindingContext.DefaultSeparator, BindingContext.DefaultSeparator, Macros.False);

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
            return HashCode.Combine(AlertDesc.GetHashCode(), QueryBuilder.GetHashCode());
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

    // QueryBuilder Extensions
    public unsafe partial struct AlertBuilder
    {
        /// <inheritdoc cref="QueryBuilder.Self()"/>
        public ref AlertBuilder Self()
        {
            QueryBuilder.Self();
            return ref this;
        }

        // /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
        // public ref AlertBuilder Id(ulong id)
        // {
        //     QueryBuilder.Id(id);
        //     return ref this;
        // }

        /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
        public ref AlertBuilder Entity(ulong entity)
        {
            QueryBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Name(string)"/>
        public ref AlertBuilder Name(string name)
        {
            QueryBuilder.Name(name);
            return ref this;
        }

        // /// <inheritdoc cref="QueryBuilder.Var(string)"/>
        // public ref AlertBuilder Var(string name)
        // {
        //     QueryBuilder.Var(name);
        //     return ref this;
        // }

        /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
        public ref AlertBuilder Term(ulong id)
        {
            QueryBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src()"/>
        public ref AlertBuilder Src()
        {
            QueryBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First()"/>
        public ref AlertBuilder First()
        {
            QueryBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second()"/>
        public ref AlertBuilder Second()
        {
            QueryBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
        public ref AlertBuilder Src(ulong srcId)
        {
            QueryBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
        public ref AlertBuilder Src<T>()
        {
            QueryBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
        public ref AlertBuilder Src<T>(T value) where T : Enum
        {
            QueryBuilder.Src(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(string)"/>
        public ref AlertBuilder Src(string name)
        {
            QueryBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
        public ref AlertBuilder First(ulong firstId)
        {
            QueryBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}()"/>
        public ref AlertBuilder First<T>()
        {
            QueryBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
        public ref AlertBuilder First<T>(T value) where T : Enum
        {
            QueryBuilder.First(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(string)"/>
        public ref AlertBuilder First(string name)
        {
            QueryBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
        public ref AlertBuilder Second(ulong secondId)
        {
            QueryBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
        public ref AlertBuilder Second<T>()
        {
            QueryBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
        public ref AlertBuilder Second<T>(T value) where T : Enum
        {
            QueryBuilder.Second(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(string)"/>
        public ref AlertBuilder Second(string secondName)
        {
            QueryBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
        public ref AlertBuilder Up(ulong traverse = 0)
        {
            QueryBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
        public ref AlertBuilder Up<T>()
        {
            QueryBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
        public ref AlertBuilder Up<T>(T value) where T : Enum
        {
            QueryBuilder.Up(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
        public ref AlertBuilder Cascade(ulong traverse = 0)
        {
            QueryBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
        public ref AlertBuilder Cascade<T>()
        {
            QueryBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
        public ref AlertBuilder Cascade<T>(T value) where T : Enum
        {
            QueryBuilder.Cascade(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Descend()"/>
        public ref AlertBuilder Descend()
        {
            QueryBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Parent()"/>
        public ref AlertBuilder Parent()
        {
            QueryBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
        public ref AlertBuilder Trav(ulong traverse, uint flags = 0)
        {
            QueryBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
        public ref AlertBuilder Trav<T>(uint flags = 0)
        {
            QueryBuilder.Trav<T>(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
        public ref AlertBuilder Trav<T>(T value, uint flags = 0) where T : Enum
        {
            QueryBuilder.Trav(value, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.IdFlags(ulong)"/>
        public ref AlertBuilder IdFlags(ulong flags)
        {
            QueryBuilder.IdFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
        public ref AlertBuilder InOut(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
        public ref AlertBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write()"/>
        public ref AlertBuilder Write()
        {
            QueryBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read()"/>
        public ref AlertBuilder Read()
        {
            QueryBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
        public ref AlertBuilder ReadWrite()
        {
            QueryBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.In()"/>
        public ref AlertBuilder In()
        {
            QueryBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Out()"/>
        public ref AlertBuilder Out()
        {
            QueryBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut()"/>
        public ref AlertBuilder InOut()
        {
            QueryBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
        public ref AlertBuilder InOutNone()
        {
            QueryBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
        public ref AlertBuilder Oper(ecs_oper_kind_t oper)
        {
            QueryBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.And()"/>
        public ref AlertBuilder And()
        {
            QueryBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Or()"/>
        public ref AlertBuilder Or()
        {
            QueryBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Not()"/>
        public ref AlertBuilder Not()
        {
            QueryBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Optional()"/>
        public ref AlertBuilder Optional()
        {
            QueryBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
        public ref AlertBuilder AndFrom()
        {
            QueryBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
        public ref AlertBuilder OrFrom()
        {
            QueryBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
        public ref AlertBuilder NotFrom()
        {
            QueryBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Singleton()"/>
        public ref AlertBuilder Singleton()
        {
            QueryBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Filter()"/>
        public ref AlertBuilder Filter()
        {
            QueryBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Instanced()"/>
        public ref AlertBuilder Instanced()
        {
            QueryBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Flags(uint)"/>
        public ref AlertBuilder Flags(uint flags)
        {
            QueryBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
        public ref AlertBuilder CacheKind(ecs_query_cache_kind_t kind)
        {
            QueryBuilder.CacheKind(kind);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cached()"/>
        public ref AlertBuilder Cached()
        {
            QueryBuilder.Cached();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
        public ref AlertBuilder Expr(string expr)
        {
            QueryBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
        public ref AlertBuilder With(Term term)
        {
            QueryBuilder.With(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
        public ref AlertBuilder With(ulong id)
        {
            QueryBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string)"/>
        public ref AlertBuilder With(string name)
        {
            QueryBuilder.With(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
        public ref AlertBuilder With(ulong first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
        public ref AlertBuilder With(ulong first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
        public ref AlertBuilder With(string first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
        public ref AlertBuilder With(string first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}()"/>
        public ref AlertBuilder With<T>()
        {
            QueryBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
        public ref AlertBuilder With<T>(T value) where T : Enum
        {
            QueryBuilder.With(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
        public ref AlertBuilder With<TFirst>(ulong second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
        public ref AlertBuilder With<TFirst>(string second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
        public ref AlertBuilder With<TFirst, TSecond>()
        {
            QueryBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
        public ref AlertBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
        public ref AlertBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
        public ref AlertBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
        public ref AlertBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
        public ref AlertBuilder WithSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
        public ref AlertBuilder WithSecond<TSecond>(string first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
        public ref AlertBuilder Without(Term term)
        {
            QueryBuilder.Without(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
        public ref AlertBuilder Without(ulong id)
        {
            QueryBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string)"/>
        public ref AlertBuilder Without(string name)
        {
            QueryBuilder.Without(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
        public ref AlertBuilder Without(ulong first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
        public ref AlertBuilder Without(ulong first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
        public ref AlertBuilder Without(string first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
        public ref AlertBuilder Without(string first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
        public ref AlertBuilder Without<T>()
        {
            QueryBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
        public ref AlertBuilder Without<T>(T value) where T : Enum
        {
            QueryBuilder.Without(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
        public ref AlertBuilder Without<TFirst>(ulong second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
        public ref AlertBuilder Without<TFirst>(string second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
        public ref AlertBuilder Without<TFirst, TSecond>()
        {
            QueryBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
        public ref AlertBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
        public ref AlertBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
        public ref AlertBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
        public ref AlertBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
        public ref AlertBuilder WithoutSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
        public ref AlertBuilder WithoutSecond<TSecond>(string first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
        public ref AlertBuilder Write(Term term)
        {
            QueryBuilder.Write(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
        public ref AlertBuilder Write(ulong id)
        {
            QueryBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string)"/>
        public ref AlertBuilder Write(string name)
        {
            QueryBuilder.Write(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
        public ref AlertBuilder Write(ulong first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
        public ref AlertBuilder Write(ulong first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
        public ref AlertBuilder Write(string first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
        public ref AlertBuilder Write(string first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
        public ref AlertBuilder Write<T>()
        {
            QueryBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
        public ref AlertBuilder Write<T>(T value) where T : Enum
        {
            QueryBuilder.Write(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
        public ref AlertBuilder Write<TFirst>(ulong second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
        public ref AlertBuilder Write<TFirst>(string second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
        public ref AlertBuilder Write<TFirst, TSecond>()
        {
            QueryBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
        public ref AlertBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
        public ref AlertBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
        public ref AlertBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
        public ref AlertBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
        public ref AlertBuilder WriteSecond<TSecond>(ulong first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
        public ref AlertBuilder WriteSecond<TSecond>(string first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
        public ref AlertBuilder Read(Term term)
        {
            QueryBuilder.Read(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
        public ref AlertBuilder Read(ulong id)
        {
            QueryBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string)"/>
        public ref AlertBuilder Read(string name)
        {
            QueryBuilder.Read(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
        public ref AlertBuilder Read(ulong first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
        public ref AlertBuilder Read(ulong first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
        public ref AlertBuilder Read(string first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
        public ref AlertBuilder Read(string first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
        public ref AlertBuilder Read<T>()
        {
            QueryBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
        public ref AlertBuilder Read<T>(T value) where T : Enum
        {
            QueryBuilder.Read(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
        public ref AlertBuilder Read<TFirst>(ulong second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
        public ref AlertBuilder Read<TFirst>(string second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
        public ref AlertBuilder Read<TFirst, TSecond>()
        {
            QueryBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
        public ref AlertBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
        public ref AlertBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
        public ref AlertBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
        public ref AlertBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
        public ref AlertBuilder ReadSecond<TSecond>(ulong first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
        public ref AlertBuilder ReadSecond<TSecond>(string first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
        public ref AlertBuilder ScopeOpen()
        {
            QueryBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
        public ref AlertBuilder ScopeClose()
        {
            QueryBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term()"/>
        public ref AlertBuilder Term()
        {
            QueryBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
        public ref AlertBuilder TermAt(int termIndex)
        {
            QueryBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
        public ref AlertBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy(component, compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
        public ref AlertBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy<T>(compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
        public ref AlertBuilder GroupBy(ulong component)
        {
            QueryBuilder.GroupBy(component);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
        public ref AlertBuilder GroupBy<T>()
        {
            QueryBuilder.GroupBy<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
        public ref AlertBuilder GroupBy(ulong component, Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
        public ref AlertBuilder GroupBy<T>(Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
        public ref AlertBuilder GroupBy(ulong component, Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
        public ref AlertBuilder GroupBy<T>(Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        ///
        public ref AlertBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            QueryBuilder.GroupByCtx(ctx, contextFree);
            return ref this;
        }

        ///
        public ref AlertBuilder GroupByCtx(void* ctx)
        {
            QueryBuilder.GroupByCtx(ctx);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
        public ref AlertBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            QueryBuilder.OnGroupCreate(onGroupCreate);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
        public ref AlertBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            QueryBuilder.OnGroupDelete(onGroupDelete);
            return ref this;
        }
    }
}
