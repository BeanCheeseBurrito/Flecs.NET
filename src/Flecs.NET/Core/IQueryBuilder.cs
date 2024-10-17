using System;
using System.Diagnostics.CodeAnalysis;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Query builder interface.
/// </summary>
/// <typeparam name="TBuilder"></typeparam>
/// <typeparam name="TResult"></typeparam>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
public unsafe interface IQueryBuilder<TBuilder, TResult> : IQueryBuilderBase
{
    /// <summary>
    ///     Builds the query and returns the newly created TResult object.
    /// </summary>
    /// <returns>The created TResult object.</returns>
    public TResult Build();

    /// <inheritdoc cref="QueryBuilder.Self()"/>
    public ref TBuilder Self();

    /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
    public ref TBuilder Id(ulong id);

    /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
    public ref TBuilder Entity(ulong entity);

    /// <inheritdoc cref="QueryBuilder.Name(string)"/>
    public ref TBuilder Name(string name);

    /// <inheritdoc cref="QueryBuilder.Var(string)"/>
    public ref TBuilder Var(string name);

    /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
    public ref TBuilder Term(ulong id);

    /// <inheritdoc cref="QueryBuilder.Src()"/>
    public ref TBuilder Src();

    /// <inheritdoc cref="QueryBuilder.First()"/>
    public ref TBuilder First();

    /// <inheritdoc cref="QueryBuilder.Second()"/>
    public ref TBuilder Second();

    /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
    public ref TBuilder Src(ulong srcId);

    /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
    public ref TBuilder Src<T>();

    /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
    public ref TBuilder Src<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Src(string)"/>
    public ref TBuilder Src(string name);

    /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
    public ref TBuilder First(ulong firstId);

    /// <inheritdoc cref="QueryBuilder.First{T}()"/>
    public ref TBuilder First<T>();

    /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
    public ref TBuilder First<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.First(string)"/>
    public ref TBuilder First(string name);

    /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
    public ref TBuilder Second(ulong secondId);

    /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
    public ref TBuilder Second<T>();

    /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
    public ref TBuilder Second<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Second(string)"/>
    public ref TBuilder Second(string secondName);

    /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
    public ref TBuilder Up(ulong traverse = 0);

    /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
    public ref TBuilder Up<T>();

    /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
    public ref TBuilder Up<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
    public ref TBuilder Cascade(ulong traverse = 0);

    /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
    public ref TBuilder Cascade<T>();

    /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
    public ref TBuilder Cascade<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Descend()"/>
    public ref TBuilder Descend();

    /// <inheritdoc cref="QueryBuilder.Parent()"/>
    public ref TBuilder Parent();

    /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
    public ref TBuilder Trav(ulong traverse, uint flags = 0);

    /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
    public ref TBuilder Trav<T>(uint flags = 0);

    /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
    public ref TBuilder Trav<T>(T value, uint flags = 0) where T : Enum;

    /// <inheritdoc cref="Core.QueryBuilder.Flags"/>
    public ref TBuilder Flags(ulong flags);

    /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
    public ref TBuilder InOut(ecs_inout_kind_t inOut);

    /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
    public ref TBuilder InOutStage(ecs_inout_kind_t inOut);

    /// <inheritdoc cref="QueryBuilder.Write()"/>
    public ref TBuilder Write();

    /// <inheritdoc cref="QueryBuilder.Read()"/>
    public ref TBuilder Read();

    /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
    public ref TBuilder ReadWrite();

    /// <inheritdoc cref="QueryBuilder.In()"/>
    public ref TBuilder In();

    /// <inheritdoc cref="QueryBuilder.Out()"/>
    public ref TBuilder Out();

    /// <inheritdoc cref="QueryBuilder.InOut()"/>
    public ref TBuilder InOut();

    /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
    public ref TBuilder InOutNone();

    /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
    public ref TBuilder Oper(ecs_oper_kind_t oper);

    /// <inheritdoc cref="QueryBuilder.And()"/>
    public ref TBuilder And();

    /// <inheritdoc cref="QueryBuilder.Or()"/>
    public ref TBuilder Or();

    /// <inheritdoc cref="QueryBuilder.Not()"/>
    public ref TBuilder Not();

    /// <inheritdoc cref="QueryBuilder.Optional()"/>
    public ref TBuilder Optional();

    /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
    public ref TBuilder AndFrom();

    /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
    public ref TBuilder OrFrom();

    /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
    public ref TBuilder NotFrom();

    /// <inheritdoc cref="QueryBuilder.Singleton()"/>
    public ref TBuilder Singleton();

    /// <inheritdoc cref="QueryBuilder.Filter()"/>
    public ref TBuilder Filter();

    /// <inheritdoc cref="Core.QueryBuilder.QueryFlags"/>
    public ref TBuilder QueryFlags(uint flags);

    /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
    public ref TBuilder CacheKind(ecs_query_cache_kind_t kind);

    /// <inheritdoc cref="QueryBuilder.Cached()"/>
    public ref TBuilder Cached();

    /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
    public ref TBuilder Expr(string expr);

    /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
    public ref TBuilder With(Term term);

    /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
    public ref TBuilder With(ulong id);

    /// <inheritdoc cref="QueryBuilder.With(string)"/>
    public ref TBuilder With(string name);

    /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
    public ref TBuilder With(ulong first, ulong second);

    /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
    public ref TBuilder With(ulong first, string second);

    /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
    public ref TBuilder With(string first, ulong second);

    /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
    public ref TBuilder With(string first, string second);

    /// <inheritdoc cref="QueryBuilder.With{T}()"/>
    public ref TBuilder With<T>();

    /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
    public ref TBuilder With<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
    public ref TBuilder With<TFirst>(ulong second);

    /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
    public ref TBuilder With<TFirst>(string second);

    /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
    public ref TBuilder With<TFirst, TSecond>();

    /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
    public ref TBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
    public ref TBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
    public ref TBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
    public ref TBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
    public ref TBuilder WithSecond<TSecond>(ulong first);

    /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
    public ref TBuilder WithSecond<TSecond>(string first);

    /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
    public ref TBuilder Without(Term term);

    /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
    public ref TBuilder Without(ulong id);

    /// <inheritdoc cref="QueryBuilder.Without(string)"/>
    public ref TBuilder Without(string name);

    /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
    public ref TBuilder Without(ulong first, ulong second);

    /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
    public ref TBuilder Without(ulong first, string second);

    /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
    public ref TBuilder Without(string first, ulong second);

    /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
    public ref TBuilder Without(string first, string second);

    /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
    public ref TBuilder Without<T>();

    /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
    public ref TBuilder Without<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
    public ref TBuilder Without<TFirst>(ulong second);

    /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
    public ref TBuilder Without<TFirst>(string second);

    /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
    public ref TBuilder Without<TFirst, TSecond>();

    /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
    public ref TBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
    public ref TBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
    public ref TBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
    public ref TBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
    public ref TBuilder WithoutSecond<TSecond>(ulong first);

    /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
    public ref TBuilder WithoutSecond<TSecond>(string first);

    /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
    public ref TBuilder Write(Term term);

    /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
    public ref TBuilder Write(ulong id);

    /// <inheritdoc cref="QueryBuilder.Write(string)"/>
    public ref TBuilder Write(string name);

    /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
    public ref TBuilder Write(ulong first, ulong second);

    /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
    public ref TBuilder Write(ulong first, string second);

    /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
    public ref TBuilder Write(string first, ulong second);

    /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
    public ref TBuilder Write(string first, string second);

    /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
    public ref TBuilder Write<T>();

    /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
    public ref TBuilder Write<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
    public ref TBuilder Write<TFirst>(ulong second);

    /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
    public ref TBuilder Write<TFirst>(string second);

    /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
    public ref TBuilder Write<TFirst, TSecond>();

    /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
    public ref TBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
    public ref TBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
    public ref TBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
    public ref TBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
    public ref TBuilder WriteSecond<TSecond>(ulong first);

    /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
    public ref TBuilder WriteSecond<TSecond>(string first);

    /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
    public ref TBuilder Read(Term term);

    /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
    public ref TBuilder Read(ulong id);

    /// <inheritdoc cref="QueryBuilder.Read(string)"/>
    public ref TBuilder Read(string name);

    /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
    public ref TBuilder Read(ulong first, ulong second);

    /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
    public ref TBuilder Read(ulong first, string second);

    /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
    public ref TBuilder Read(string first, ulong second);

    /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
    public ref TBuilder Read(string first, string second);

    /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
    public ref TBuilder Read<T>();

    /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
    public ref TBuilder Read<T>(T value) where T : Enum;

    /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
    public ref TBuilder Read<TFirst>(ulong second);

    /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
    public ref TBuilder Read<TFirst>(string second);

    /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
    public ref TBuilder Read<TFirst, TSecond>();

    /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
    public ref TBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
    public ref TBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
    public ref TBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum;

    /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
    public ref TBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum;

    /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
    public ref TBuilder ReadSecond<TSecond>(ulong first);

    /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
    public ref TBuilder ReadSecond<TSecond>(string first);

    /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
    public ref TBuilder ScopeOpen();

    /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
    public ref TBuilder ScopeClose();

    /// <inheritdoc cref="QueryBuilder.Term()"/>
    public ref TBuilder Term();

    /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
    public ref TBuilder TermAt(int termIndex);

    /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByCallback)"/>
    public ref TBuilder OrderBy(ulong component, Ecs.OrderByCallback callback);

    /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByCallback)"/>
    public ref TBuilder OrderBy<T>(Ecs.OrderByCallback callback);

    /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
    public ref TBuilder GroupBy(ulong component);

    /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
    public ref TBuilder GroupBy(ulong component, Ecs.GroupByCallback callback);

    /// <inheritdoc cref="QueryBuilder.GroupBy{TContext}(ulong, Ecs.GroupByCallback{TContext})"/>
    public ref TBuilder GroupBy<TContext>(ulong component, Ecs.GroupByCallback<TContext> callback);

    /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
    public ref TBuilder GroupBy<T>();

    /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
    public ref TBuilder GroupBy<T>(Ecs.GroupByCallback callback);

    /// <inheritdoc cref="QueryBuilder.GroupBy{T, TContext}(Ecs.GroupByCallback{TContext})"/>
    public ref TBuilder GroupBy<T, TContext>(Ecs.GroupByCallback<TContext> callback);

    /// <inheritdoc cref="QueryBuilder.GroupByCtx{T}(T)"/>
    public ref TBuilder GroupByCtx<T>(T value);

    /// <inheritdoc cref="QueryBuilder.GroupByCtx{T}(T, Ecs.UserContextFinish{T})"/>
    public ref TBuilder GroupByCtx<T>(T value, Ecs.UserContextFinish<T> callback);

    /// <inheritdoc cref="QueryBuilder.GroupByCtx{T}(T, Ecs.UserContextFinish{T})"/>
    public ref TBuilder GroupByCtx<T>(T value, delegate*<ref T, void> callback);

    /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateCallback)"/>
    public ref TBuilder OnGroupCreate(Ecs.GroupCreateCallback callback);

    /// <inheritdoc cref="Core.QueryBuilder.OnGroupCreate{T}(Ecs.GroupCreateCallback{T})"/>
    public ref TBuilder OnGroupCreate<T>(Ecs.GroupCreateCallback<T> callback);

    /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteCallback)"/>
    public ref TBuilder OnGroupDelete(Ecs.GroupDeleteCallback callback);

    /// <inheritdoc cref="Core.QueryBuilder.OnGroupDelete{T}(Ecs.GroupDeleteCallback{T})"/>
    public ref TBuilder OnGroupDelete<T>(Ecs.GroupDeleteCallback<T> callback);
}
