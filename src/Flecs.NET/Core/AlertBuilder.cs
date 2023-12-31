using System;
using Flecs.NET.Collections;
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
        private NativeList<NativeString> _strings;
        private int _severityFilterCount;

        internal ecs_alert_desc_t AlertDesc;
        internal FilterBuilder FilterBuilder;

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
        /// <param name="name"></param>
        public AlertBuilder(ecs_world_t* world, string? name = null)
        {
            AlertDesc = default;
            FilterBuilder = new FilterBuilder(world);
            _world = world;
            _strings = default;
            _severityFilterCount = default;

            if (string.IsNullOrEmpty(name))
                return;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.sep = BindingContext.DefaultSeparator;
            desc.root_sep = BindingContext.DefaultRootSeparator;
            AlertDesc.entity = ecs_entity_init(World, &desc);
        }

        /// <summary>
        ///     Cleans up the alert builder's resources.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < _strings.Count; i++)
                _strings[i].Dispose();

            _strings.Dispose();
            FilterBuilder.Dispose();
        }

        /// <summary>
        ///     Builds a new alert.
        /// </summary>
        /// <returns></returns>
        public Alert Build()
        {
            fixed (ecs_alert_desc_t* alertDesc = &AlertDesc)
            {
                alertDesc->filter = FilterBuilder.Desc;
                alertDesc->filter.terms_buffer = FilterBuilder.Terms.Data;
                alertDesc->filter.terms_buffer_count = FilterBuilder.Terms.Count;

                Entity entity = new Entity(World, ecs_alert_init(World, alertDesc));

                if (entity == 0)
                    Ecs.Error("Alert failed to init.");

                Dispose();
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
            return Desc == other.Desc && FilterBuilder == other.FilterBuilder;
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
            return HashCode.Combine(AlertDesc.GetHashCode(), FilterBuilder.GetHashCode());
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

    // FilterBuilder extensions.
    public unsafe partial struct AlertBuilder
    {
        /// <inheritdoc cref="Core.FilterBuilder.Self"/>
        public ref AlertBuilder Self()
        {
            FilterBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up"/>
        public ref AlertBuilder Up(ulong traverse = 0)
        {
            FilterBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up{T}"/>
        public ref AlertBuilder Up<T>()
        {
            FilterBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade"/>
        public ref AlertBuilder Cascade(ulong traverse = 0)
        {
            FilterBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade{T}"/>
        public ref AlertBuilder Cascade<T>()
        {
            FilterBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Descend"/>
        public ref AlertBuilder Descend()
        {
            FilterBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Parent"/>
        public ref AlertBuilder Parent()
        {
            FilterBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Trav"/>
        public ref AlertBuilder Trav(ulong traverse, uint flags = 0)
        {
            FilterBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Entity"/>
        public ref AlertBuilder Entity(ulong entity)
        {
            FilterBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Name"/>
        public ref AlertBuilder Name(string name)
        {
            FilterBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Flags"/>
        public ref AlertBuilder Flags(uint flags)
        {
            FilterBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src()"/>
        public ref AlertBuilder Src()
        {
            FilterBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First()"/>
        public ref AlertBuilder First()
        {
            FilterBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second()"/>
        public ref AlertBuilder Second()
        {
            FilterBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(ulong)"/>
        public ref AlertBuilder Src(ulong srcId)
        {
            FilterBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src{T}"/>
        public ref AlertBuilder Src<T>()
        {
            FilterBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(string)"/>
        public ref AlertBuilder Src(string name)
        {
            FilterBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(ulong)"/>
        public ref AlertBuilder First(ulong firstId)
        {
            FilterBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First{T}"/>
        public ref AlertBuilder First<T>()
        {
            FilterBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(string)"/>
        public ref AlertBuilder First(string name)
        {
            FilterBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(ulong)"/>
        public ref AlertBuilder Second(ulong secondId)
        {
            FilterBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second{T}"/>
        public ref AlertBuilder Second<T>()
        {
            FilterBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(string)"/>
        public ref AlertBuilder Second(string secondName)
        {
            FilterBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Role"/>
        public ref AlertBuilder Role(ulong role)
        {
            FilterBuilder.Role(role);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut(Flecs.NET.Bindings.Native.ecs_inout_kind_t)"/>
        public ref AlertBuilder InOut(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutStage"/>
        public ref AlertBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write()"/>
        public ref AlertBuilder Write()
        {
            FilterBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read()"/>
        public ref AlertBuilder Read()
        {
            FilterBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadWrite"/>
        public ref AlertBuilder ReadWrite()
        {
            FilterBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.In"/>
        public ref AlertBuilder In()
        {
            FilterBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Out"/>
        public ref AlertBuilder Out()
        {
            FilterBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut()"/>
        public ref AlertBuilder InOut()
        {
            FilterBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutNone"/>
        public ref AlertBuilder InOutNone()
        {
            FilterBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Oper"/>
        public ref AlertBuilder Oper(ecs_oper_kind_t oper)
        {
            FilterBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.And"/>
        public ref AlertBuilder And()
        {
            FilterBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Or"/>
        public ref AlertBuilder Or()
        {
            FilterBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Not"/>
        public ref AlertBuilder Not()
        {
            FilterBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Optional"/>
        public ref AlertBuilder Optional()
        {
            FilterBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.AndFrom"/>
        public ref AlertBuilder AndFrom()
        {
            FilterBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.OrFrom"/>
        public ref AlertBuilder OrFrom()
        {
            FilterBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.NotFrom"/>
        public ref AlertBuilder NotFrom()
        {
            FilterBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Singleton"/>
        public ref AlertBuilder Singleton()
        {
            FilterBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Filter"/>
        public ref AlertBuilder Filter()
        {
            FilterBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Instanced"/>
        public ref AlertBuilder Instanced()
        {
            FilterBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.FilterFlags"/>
        public ref AlertBuilder FilterFlags(uint flags)
        {
            FilterBuilder.FilterFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Expr"/>
        public ref AlertBuilder Expr(string expr)
        {
            FilterBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong)"/>
        public ref AlertBuilder With(ulong id)
        {
            FilterBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, ulong)"/>
        public ref AlertBuilder With(ulong first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, string)"/>
        public ref AlertBuilder With(ulong first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, ulong)"/>
        public ref AlertBuilder With(string first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, string)"/>
        public ref AlertBuilder With(string first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}()"/>
        public ref AlertBuilder With<T>()
        {
            FilterBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(T)"/>
        public ref AlertBuilder With<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.With(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(ulong)"/>
        public ref AlertBuilder With<TFirst>(ulong second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(string)"/>
        public ref AlertBuilder With<TFirst>(string second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}()"/>
        public ref AlertBuilder With<TFirst, TSecond>()
        {
            FilterBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}(T2)"/>
        public ref AlertBuilder With<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.With<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(ulong)"/>
        public ref AlertBuilder WithSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(string)"/>
        public ref AlertBuilder WithSecond<TSecond>(string first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong)"/>
        public ref AlertBuilder Without(ulong id)
        {
            FilterBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, ulong)"/>
        public ref AlertBuilder Without(ulong first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, string)"/>
        public ref AlertBuilder Without(ulong first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, ulong)"/>
        public ref AlertBuilder Without(string first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, string)"/>
        public ref AlertBuilder Without(string first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}()"/>
        public ref AlertBuilder Without<T>()
        {
            FilterBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(T)"/>
        public ref AlertBuilder Without<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Without(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(ulong)"/>
        public ref AlertBuilder Without<TFirst>(ulong second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(string)"/>
        public ref AlertBuilder Without<TFirst>(string second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}()"/>
        public ref AlertBuilder Without<TFirst, TSecond>()
        {
            FilterBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}(T2)"/>
        public ref AlertBuilder Without<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Without<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(ulong)"/>
        public ref AlertBuilder WithoutSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(string)"/>
        public ref AlertBuilder WithoutSecond<TSecond>(string first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong)"/>
        public ref AlertBuilder Write(ulong id)
        {
            FilterBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, ulong)"/>
        public ref AlertBuilder Write(ulong first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, string)"/>
        public ref AlertBuilder Write(ulong first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, ulong)"/>
        public ref AlertBuilder Write(string first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, string)"/>
        public ref AlertBuilder Write(string first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}()"/>
        public ref AlertBuilder Write<T>()
        {
            FilterBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(T)"/>
        public ref AlertBuilder Write<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Write(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(ulong)"/>
        public ref AlertBuilder Write<TFirst>(ulong second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(string)"/>
        public ref AlertBuilder Write<TFirst>(string second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}()"/>
        public ref AlertBuilder Write<TFirst, TSecond>()
        {
            FilterBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}(T2)"/>
        public ref AlertBuilder Write<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Write<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(ulong)"/>
        public ref AlertBuilder WriteSecond<TSecond>(ulong first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(string)"/>
        public ref AlertBuilder WriteSecond<TSecond>(string first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong)"/>
        public ref AlertBuilder Read(ulong id)
        {
            FilterBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, ulong)"/>
        public ref AlertBuilder Read(ulong first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, string)"/>
        public ref AlertBuilder Read(ulong first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, ulong)"/>
        public ref AlertBuilder Read(string first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, string)"/>
        public ref AlertBuilder Read(string first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}()"/>
        public ref AlertBuilder Read<T>()
        {
            FilterBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(T)"/>
        public ref AlertBuilder Read<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Read(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(ulong)"/>
        public ref AlertBuilder Read<TFirst>(ulong second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(string)"/>
        public ref AlertBuilder Read<TFirst>(string second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}()"/>
        public ref AlertBuilder Read<TFirst, TSecond>()
        {
            FilterBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}(T2)"/>
        public ref AlertBuilder Read<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Read<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(ulong)"/>
        public ref AlertBuilder ReadSecond<TSecond>(ulong first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(string)"/>
        public ref AlertBuilder ReadSecond<TSecond>(string first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeOpen"/>
        public ref AlertBuilder ScopeOpen()
        {
            FilterBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeClose"/>
        public ref AlertBuilder ScopeClose()
        {
            FilterBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.IncrementTerm"/>
        public ref AlertBuilder IncrementTerm()
        {
            FilterBuilder.IncrementTerm();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermAt"/>
        public ref AlertBuilder TermAt(int termIndex)
        {
            FilterBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Arg"/>
        public ref AlertBuilder Arg(int termIndex)
        {
            FilterBuilder.Arg(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong)"/>
        public ref AlertBuilder Term(ulong id)
        {
            FilterBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string)"/>
        public ref AlertBuilder Term(string name)
        {
            FilterBuilder.Term(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, ulong)"/>
        public ref AlertBuilder Term(ulong first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, string)"/>
        public ref AlertBuilder Term(ulong first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, ulong)"/>
        public ref AlertBuilder Term(string first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, string)"/>
        public ref AlertBuilder Term(string first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}()"/>
        public ref AlertBuilder Term<T>()
        {
            FilterBuilder.Term<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(T)"/>
        public ref AlertBuilder Term<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Term(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(ulong)"/>
        public ref AlertBuilder Term<TFirst>(ulong second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(string)"/>
        public ref AlertBuilder Term<TFirst>(string second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}()"/>
        public ref AlertBuilder Term<TFirst, TSecond>()
        {
            FilterBuilder.Term<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}(T2)"/>
        public ref AlertBuilder Term<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Term<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(ulong)"/>
        public ref AlertBuilder TermSecond<TSecond>(ulong first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(string)"/>
        public ref AlertBuilder TermSecond<TSecond>(string first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }
    }
}
