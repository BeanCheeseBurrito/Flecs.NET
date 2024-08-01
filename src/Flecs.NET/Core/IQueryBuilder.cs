using System;
using System.Diagnostics.CodeAnalysis;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Query builder interface.
    /// </summary>
    /// <typeparam name="TQueryBuilder"></typeparam>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
    public unsafe interface IQueryBuilder<TQueryBuilder>
    {
        /// <summary>
        ///     A reference to the query builder.
        /// </summary>
        public ref QueryBuilder QueryBuilder { get; }

        /// <inheritdoc cref="QueryBuilder.Self()"/>
        public ref TQueryBuilder Self();

        /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
        public ref TQueryBuilder Id(ulong id);

        /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
        public ref TQueryBuilder Entity(ulong entity);

        /// <inheritdoc cref="QueryBuilder.Name(string)"/>
        public ref TQueryBuilder Name(string name);

        /// <inheritdoc cref="QueryBuilder.Var(string)"/>
        public ref TQueryBuilder Var(string name);

        /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
        public ref TQueryBuilder Term(ulong id);

        /// <inheritdoc cref="QueryBuilder.Src()"/>
        public ref TQueryBuilder Src();

        /// <inheritdoc cref="QueryBuilder.First()"/>
        public ref TQueryBuilder First();

        /// <inheritdoc cref="QueryBuilder.Second()"/>
        public ref TQueryBuilder Second();

        /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
        public ref TQueryBuilder Src(ulong srcId);

        /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
        public ref TQueryBuilder Src<T>();

        /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
        public ref TQueryBuilder Src<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Src(string)"/>
        public ref TQueryBuilder Src(string name);

        /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
        public ref TQueryBuilder First(ulong firstId);

        /// <inheritdoc cref="QueryBuilder.First{T}()"/>
        public ref TQueryBuilder First<T>();

        /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
        public ref TQueryBuilder First<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.First(string)"/>
        public ref TQueryBuilder First(string name);

        /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
        public ref TQueryBuilder Second(ulong secondId);

        /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
        public ref TQueryBuilder Second<T>();

        /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
        public ref TQueryBuilder Second<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Second(string)"/>
        public ref TQueryBuilder Second(string secondName);

        /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
        public ref TQueryBuilder Up(ulong traverse = 0);

        /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
        public ref TQueryBuilder Up<T>();

        /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
        public ref TQueryBuilder Up<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
        public ref TQueryBuilder Cascade(ulong traverse = 0);

        /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
        public ref TQueryBuilder Cascade<T>();

        /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
        public ref TQueryBuilder Cascade<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Descend()"/>
        public ref TQueryBuilder Descend();

        /// <inheritdoc cref="QueryBuilder.Parent()"/>
        public ref TQueryBuilder Parent();

        /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
        public ref TQueryBuilder Trav(ulong traverse, uint flags = 0);

        /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
        public ref TQueryBuilder Trav<T>(uint flags = 0);

        /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
        public ref TQueryBuilder Trav<T>(T value, uint flags = 0) where T : Enum;

        /// <inheritdoc cref="Core.QueryBuilder.Flags"/>
        public ref TQueryBuilder Flags(ulong flags);

        /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
        public ref TQueryBuilder InOut(ecs_inout_kind_t inOut);

        /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
        public ref TQueryBuilder InOutStage(ecs_inout_kind_t inOut);

        /// <inheritdoc cref="QueryBuilder.Write()"/>
        public ref TQueryBuilder Write();

        /// <inheritdoc cref="QueryBuilder.Read()"/>
        public ref TQueryBuilder Read();

        /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
        public ref TQueryBuilder ReadWrite();

        /// <inheritdoc cref="QueryBuilder.In()"/>
        public ref TQueryBuilder In();

        /// <inheritdoc cref="QueryBuilder.Out()"/>
        public ref TQueryBuilder Out();

        /// <inheritdoc cref="QueryBuilder.InOut()"/>
        public ref TQueryBuilder InOut();

        /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
        public ref TQueryBuilder InOutNone();

        /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
        public ref TQueryBuilder Oper(ecs_oper_kind_t oper);

        /// <inheritdoc cref="QueryBuilder.And()"/>
        public ref TQueryBuilder And();

        /// <inheritdoc cref="QueryBuilder.Or()"/>
        public ref TQueryBuilder Or();

        /// <inheritdoc cref="QueryBuilder.Not()"/>
        public ref TQueryBuilder Not();

        /// <inheritdoc cref="QueryBuilder.Optional()"/>
        public ref TQueryBuilder Optional();

        /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
        public ref TQueryBuilder AndFrom();

        /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
        public ref TQueryBuilder OrFrom();

        /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
        public ref TQueryBuilder NotFrom();

        /// <inheritdoc cref="QueryBuilder.Singleton()"/>
        public ref TQueryBuilder Singleton();

        /// <inheritdoc cref="QueryBuilder.Filter()"/>
        public ref TQueryBuilder Filter();

        /// <inheritdoc cref="Core.QueryBuilder.QueryFlags"/>
        public ref TQueryBuilder QueryFlags(uint flags);

        /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
        public ref TQueryBuilder CacheKind(ecs_query_cache_kind_t kind);

        /// <inheritdoc cref="QueryBuilder.Cached()"/>
        public ref TQueryBuilder Cached();

        /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
        public ref TQueryBuilder Expr(string expr);

        /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
        public ref TQueryBuilder With(Term term);

        /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
        public ref TQueryBuilder With(ulong id);

        /// <inheritdoc cref="QueryBuilder.With(string)"/>
        public ref TQueryBuilder With(string name);

        /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
        public ref TQueryBuilder With(ulong first, ulong second);

        /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
        public ref TQueryBuilder With(ulong first, string second);

        /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
        public ref TQueryBuilder With(string first, ulong second);

        /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
        public ref TQueryBuilder With(string first, string second);

        /// <inheritdoc cref="QueryBuilder.With{T}()"/>
        public ref TQueryBuilder With<T>();

        /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
        public ref TQueryBuilder With<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
        public ref TQueryBuilder With<TFirst>(ulong second);

        /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
        public ref TQueryBuilder With<TFirst>(string second);

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
        public ref TQueryBuilder With<TFirst, TSecond>();

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
        public ref TQueryBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
        public ref TQueryBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
        public ref TQueryBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
        public ref TQueryBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
        public ref TQueryBuilder WithSecond<TSecond>(ulong first);

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
        public ref TQueryBuilder WithSecond<TSecond>(string first);

        /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
        public ref TQueryBuilder Without(Term term);

        /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
        public ref TQueryBuilder Without(ulong id);

        /// <inheritdoc cref="QueryBuilder.Without(string)"/>
        public ref TQueryBuilder Without(string name);

        /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
        public ref TQueryBuilder Without(ulong first, ulong second);

        /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
        public ref TQueryBuilder Without(ulong first, string second);

        /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
        public ref TQueryBuilder Without(string first, ulong second);

        /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
        public ref TQueryBuilder Without(string first, string second);

        /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
        public ref TQueryBuilder Without<T>();

        /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
        public ref TQueryBuilder Without<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
        public ref TQueryBuilder Without<TFirst>(ulong second);

        /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
        public ref TQueryBuilder Without<TFirst>(string second);

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
        public ref TQueryBuilder Without<TFirst, TSecond>();

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
        public ref TQueryBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
        public ref TQueryBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
        public ref TQueryBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
        public ref TQueryBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
        public ref TQueryBuilder WithoutSecond<TSecond>(ulong first);

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
        public ref TQueryBuilder WithoutSecond<TSecond>(string first);

        /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
        public ref TQueryBuilder Write(Term term);

        /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
        public ref TQueryBuilder Write(ulong id);

        /// <inheritdoc cref="QueryBuilder.Write(string)"/>
        public ref TQueryBuilder Write(string name);

        /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
        public ref TQueryBuilder Write(ulong first, ulong second);

        /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
        public ref TQueryBuilder Write(ulong first, string second);

        /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
        public ref TQueryBuilder Write(string first, ulong second);

        /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
        public ref TQueryBuilder Write(string first, string second);

        /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
        public ref TQueryBuilder Write<T>();

        /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
        public ref TQueryBuilder Write<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
        public ref TQueryBuilder Write<TFirst>(ulong second);

        /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
        public ref TQueryBuilder Write<TFirst>(string second);

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
        public ref TQueryBuilder Write<TFirst, TSecond>();

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
        public ref TQueryBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
        public ref TQueryBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
        public ref TQueryBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
        public ref TQueryBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
        public ref TQueryBuilder WriteSecond<TSecond>(ulong first);

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
        public ref TQueryBuilder WriteSecond<TSecond>(string first);

        /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
        public ref TQueryBuilder Read(Term term);

        /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
        public ref TQueryBuilder Read(ulong id);

        /// <inheritdoc cref="QueryBuilder.Read(string)"/>
        public ref TQueryBuilder Read(string name);

        /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
        public ref TQueryBuilder Read(ulong first, ulong second);

        /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
        public ref TQueryBuilder Read(ulong first, string second);

        /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
        public ref TQueryBuilder Read(string first, ulong second);

        /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
        public ref TQueryBuilder Read(string first, string second);

        /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
        public ref TQueryBuilder Read<T>();

        /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
        public ref TQueryBuilder Read<T>(T value) where T : Enum;

        /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
        public ref TQueryBuilder Read<TFirst>(ulong second);

        /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
        public ref TQueryBuilder Read<TFirst>(string second);

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
        public ref TQueryBuilder Read<TFirst, TSecond>();

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
        public ref TQueryBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
        public ref TQueryBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
        public ref TQueryBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum;

        /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
        public ref TQueryBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum;

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
        public ref TQueryBuilder ReadSecond<TSecond>(ulong first);

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
        public ref TQueryBuilder ReadSecond<TSecond>(string first);

        /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
        public ref TQueryBuilder ScopeOpen();

        /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
        public ref TQueryBuilder ScopeClose();

        /// <inheritdoc cref="QueryBuilder.Term()"/>
        public ref TQueryBuilder Term();

        /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
        public ref TQueryBuilder TermAt(int termIndex);

        /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
        public ref TQueryBuilder OrderBy(ulong component, Ecs.OrderByAction compare);

        /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
        public ref TQueryBuilder OrderBy<T>(Ecs.OrderByAction compare);

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
        public ref TQueryBuilder GroupBy(ulong component);

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
        public ref TQueryBuilder GroupBy<T>();

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
        public ref TQueryBuilder GroupBy(ulong component, Ecs.GroupByAction callback);

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
        public ref TQueryBuilder GroupBy<T>(Ecs.GroupByAction callback);

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
        public ref TQueryBuilder GroupBy(ulong component, Ecs.GroupByCallback callback);

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
        public ref TQueryBuilder GroupBy<T>(Ecs.GroupByCallback callback);

        ///
        public ref TQueryBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree);

        ///
        public ref TQueryBuilder GroupByCtx(void* ctx);

        /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
        public ref TQueryBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate);

        /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
        public ref TQueryBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete);
    }
}
