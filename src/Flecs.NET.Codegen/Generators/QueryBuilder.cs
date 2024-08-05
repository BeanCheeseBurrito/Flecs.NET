using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;

[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public static class QueryBuilder
{
    public static string GenerateExtensions(Type type)
    {
        return $$"""
        using System;
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;
        
        public unsafe partial struct {{type}}
        {
            /// <inheritdoc cref="QueryBuilder.Self()"/>
            public ref {{type}} Self()
            {
                QueryBuilder.Self();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
            public ref {{type}} Id(ulong id)
            {
                QueryBuilder.Id(id);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
            public ref {{type}} Entity(ulong entity)
            {
                QueryBuilder.Entity(entity);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Name(string)"/>
            public ref {{type}} Name(string name)
            {
                QueryBuilder.Name(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Var(string)"/>
            public ref {{type}} Var(string name)
            {
                QueryBuilder.Var(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
            public ref {{type}} Term(ulong id)
            {
                QueryBuilder.Term(id);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Src()"/>
            public ref {{type}} Src()
            {
                QueryBuilder.Src();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.First()"/>
            public ref {{type}} First()
            {
                QueryBuilder.First();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Second()"/>
            public ref {{type}} Second()
            {
                QueryBuilder.Second();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
            public ref {{type}} Src(ulong srcId)
            {
                QueryBuilder.Src(srcId);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
            public ref {{type}} Src<T>()
            {
                QueryBuilder.Src<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
            public ref {{type}} Src<T>(T value) where T : Enum
            {
                QueryBuilder.Src(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Src(string)"/>
            public ref {{type}} Src(string name)
            {
                QueryBuilder.Src(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
            public ref {{type}} First(ulong firstId)
            {
                QueryBuilder.First(firstId);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.First{T}()"/>
            public ref {{type}} First<T>()
            {
                QueryBuilder.First<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
            public ref {{type}} First<T>(T value) where T : Enum
            {
                QueryBuilder.First(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.First(string)"/>
            public ref {{type}} First(string name)
            {
                QueryBuilder.First(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
            public ref {{type}} Second(ulong secondId)
            {
                QueryBuilder.Second(secondId);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
            public ref {{type}} Second<T>()
            {
                QueryBuilder.Second<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
            public ref {{type}} Second<T>(T value) where T : Enum
            {
                QueryBuilder.Second(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Second(string)"/>
            public ref {{type}} Second(string secondName)
            {
                QueryBuilder.Second(secondName);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
            public ref {{type}} Up(ulong traverse = 0)
            {
                QueryBuilder.Up(traverse);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
            public ref {{type}} Up<T>()
            {
                QueryBuilder.Up<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
            public ref {{type}} Up<T>(T value) where T : Enum
            {
                QueryBuilder.Up(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
            public ref {{type}} Cascade(ulong traverse = 0)
            {
                QueryBuilder.Cascade(traverse);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
            public ref {{type}} Cascade<T>()
            {
                QueryBuilder.Cascade<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
            public ref {{type}} Cascade<T>(T value) where T : Enum
            {
                QueryBuilder.Cascade(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Descend()"/>
            public ref {{type}} Descend()
            {
                QueryBuilder.Descend();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Parent()"/>
            public ref {{type}} Parent()
            {
                QueryBuilder.Parent();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
            public ref {{type}} Trav(ulong traverse, uint flags = 0)
            {
                QueryBuilder.Trav(traverse, flags);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
            public ref {{type}} Trav<T>(uint flags = 0)
            {
                QueryBuilder.Trav<T>(flags);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
            public ref {{type}} Trav<T>(T value, uint flags = 0) where T : Enum
            {
                QueryBuilder.Trav(value, flags);
                return ref this;
            }
    
            /// <inheritdoc cref="Core.QueryBuilder.Flags"/>
            public ref {{type}} Flags(ulong flags)
            {
                QueryBuilder.Flags(flags);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
            public ref {{type}} InOut(ecs_inout_kind_t inOut)
            {
                QueryBuilder.InOut(inOut);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
            public ref {{type}} InOutStage(ecs_inout_kind_t inOut)
            {
                QueryBuilder.InOutStage(inOut);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write()"/>
            public ref {{type}} Write()
            {
                QueryBuilder.Write();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read()"/>
            public ref {{type}} Read()
            {
                QueryBuilder.Read();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
            public ref {{type}} ReadWrite()
            {
                QueryBuilder.ReadWrite();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.In()"/>
            public ref {{type}} In()
            {
                QueryBuilder.In();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Out()"/>
            public ref {{type}} Out()
            {
                QueryBuilder.Out();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.InOut()"/>
            public ref {{type}} InOut()
            {
                QueryBuilder.InOut();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
            public ref {{type}} InOutNone()
            {
                QueryBuilder.InOutNone();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
            public ref {{type}} Oper(ecs_oper_kind_t oper)
            {
                QueryBuilder.Oper(oper);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.And()"/>
            public ref {{type}} And()
            {
                QueryBuilder.And();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Or()"/>
            public ref {{type}} Or()
            {
                QueryBuilder.Or();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Not()"/>
            public ref {{type}} Not()
            {
                QueryBuilder.Not();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Optional()"/>
            public ref {{type}} Optional()
            {
                QueryBuilder.Optional();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
            public ref {{type}} AndFrom()
            {
                QueryBuilder.AndFrom();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
            public ref {{type}} OrFrom()
            {
                QueryBuilder.OrFrom();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
            public ref {{type}} NotFrom()
            {
                QueryBuilder.NotFrom();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Singleton()"/>
            public ref {{type}} Singleton()
            {
                QueryBuilder.Singleton();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Filter()"/>
            public ref {{type}} Filter()
            {
                QueryBuilder.Filter();
                return ref this;
            }
    
            /// <inheritdoc cref="Core.QueryBuilder.QueryFlags"/>
            public ref {{type}} QueryFlags(uint flags)
            {
                QueryBuilder.QueryFlags(flags);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
            public ref {{type}} CacheKind(ecs_query_cache_kind_t kind)
            {
                QueryBuilder.CacheKind(kind);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Cached()"/>
            public ref {{type}} Cached()
            {
                QueryBuilder.Cached();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
            public ref {{type}} Expr(string expr)
            {
                QueryBuilder.Expr(expr);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
            public ref {{type}} With(Term term)
            {
                QueryBuilder.With(term);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
            public ref {{type}} With(ulong id)
            {
                QueryBuilder.With(id);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(string)"/>
            public ref {{type}} With(string name)
            {
                QueryBuilder.With(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
            public ref {{type}} With(ulong first, ulong second)
            {
                QueryBuilder.With(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
            public ref {{type}} With(ulong first, string second)
            {
                QueryBuilder.With(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
            public ref {{type}} With(string first, ulong second)
            {
                QueryBuilder.With(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
            public ref {{type}} With(string first, string second)
            {
                QueryBuilder.With(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T}()"/>
            public ref {{type}} With<T>()
            {
                QueryBuilder.With<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
            public ref {{type}} With<T>(T value) where T : Enum
            {
                QueryBuilder.With(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
            public ref {{type}} With<TFirst>(ulong second)
            {
                QueryBuilder.With<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
            public ref {{type}} With<TFirst>(string second)
            {
                QueryBuilder.With<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
            public ref {{type}} With<TFirst, TSecond>()
            {
                QueryBuilder.With<TFirst, TSecond>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
            public ref {{type}} With<TFirst, TSecond>(TSecond second) where TSecond : Enum
            {
                QueryBuilder.With<TFirst, TSecond>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
            public ref {{type}} With<TFirst, TSecond>(TFirst first) where TFirst : Enum
            {
                QueryBuilder.With<TFirst, TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
            public ref {{type}} With<TFirst>(TFirst first, string second) where TFirst : Enum
            {
                QueryBuilder.With(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
            public ref {{type}} With<TSecond>(string first, TSecond second) where TSecond : Enum
            {
                QueryBuilder.With(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
            public ref {{type}} WithSecond<TSecond>(ulong first)
            {
                QueryBuilder.WithSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
            public ref {{type}} WithSecond<TSecond>(string first)
            {
                QueryBuilder.WithSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
            public ref {{type}} Without(Term term)
            {
                QueryBuilder.Without(term);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
            public ref {{type}} Without(ulong id)
            {
                QueryBuilder.Without(id);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(string)"/>
            public ref {{type}} Without(string name)
            {
                QueryBuilder.Without(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
            public ref {{type}} Without(ulong first, ulong second)
            {
                QueryBuilder.Without(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
            public ref {{type}} Without(ulong first, string second)
            {
                QueryBuilder.Without(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
            public ref {{type}} Without(string first, ulong second)
            {
                QueryBuilder.Without(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
            public ref {{type}} Without(string first, string second)
            {
                QueryBuilder.Without(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
            public ref {{type}} Without<T>()
            {
                QueryBuilder.Without<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
            public ref {{type}} Without<T>(T value) where T : Enum
            {
                QueryBuilder.Without(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
            public ref {{type}} Without<TFirst>(ulong second)
            {
                QueryBuilder.Without<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
            public ref {{type}} Without<TFirst>(string second)
            {
                QueryBuilder.Without<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
            public ref {{type}} Without<TFirst, TSecond>()
            {
                QueryBuilder.Without<TFirst, TSecond>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
            public ref {{type}} Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
            {
                QueryBuilder.Without<TFirst, TSecond>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
            public ref {{type}} Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
            {
                QueryBuilder.Without<TFirst, TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
            public ref {{type}} Without<TFirst>(TFirst first, string second) where TFirst : Enum
            {
                QueryBuilder.Without(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
            public ref {{type}} Without<TSecond>(string first, TSecond second) where TSecond : Enum
            {
                QueryBuilder.Without(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
            public ref {{type}} WithoutSecond<TSecond>(ulong first)
            {
                QueryBuilder.WithoutSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
            public ref {{type}} WithoutSecond<TSecond>(string first)
            {
                QueryBuilder.WithoutSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
            public ref {{type}} Write(Term term)
            {
                QueryBuilder.Write(term);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
            public ref {{type}} Write(ulong id)
            {
                QueryBuilder.Write(id);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(string)"/>
            public ref {{type}} Write(string name)
            {
                QueryBuilder.Write(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
            public ref {{type}} Write(ulong first, ulong second)
            {
                QueryBuilder.Write(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
            public ref {{type}} Write(ulong first, string second)
            {
                QueryBuilder.Write(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
            public ref {{type}} Write(string first, ulong second)
            {
                QueryBuilder.Write(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
            public ref {{type}} Write(string first, string second)
            {
                QueryBuilder.Write(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
            public ref {{type}} Write<T>()
            {
                QueryBuilder.Write<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
            public ref {{type}} Write<T>(T value) where T : Enum
            {
                QueryBuilder.Write(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
            public ref {{type}} Write<TFirst>(ulong second)
            {
                QueryBuilder.Write<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
            public ref {{type}} Write<TFirst>(string second)
            {
                QueryBuilder.Write<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
            public ref {{type}} Write<TFirst, TSecond>()
            {
                QueryBuilder.Write<TFirst, TSecond>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
            public ref {{type}} Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
            {
                QueryBuilder.Write<TFirst, TSecond>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
            public ref {{type}} Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
            {
                QueryBuilder.Write<TFirst, TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
            public ref {{type}} Write<TFirst>(TFirst first, string second) where TFirst : Enum
            {
                QueryBuilder.Write(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
            public ref {{type}} Write<TSecond>(string first, TSecond second) where TSecond : Enum
            {
                QueryBuilder.Write(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
            public ref {{type}} WriteSecond<TSecond>(ulong first)
            {
                QueryBuilder.WriteSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
            public ref {{type}} WriteSecond<TSecond>(string first)
            {
                QueryBuilder.WriteSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
            public ref {{type}} Read(Term term)
            {
                QueryBuilder.Read(term);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
            public ref {{type}} Read(ulong id)
            {
                QueryBuilder.Read(id);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(string)"/>
            public ref {{type}} Read(string name)
            {
                QueryBuilder.Read(name);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
            public ref {{type}} Read(ulong first, ulong second)
            {
                QueryBuilder.Read(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
            public ref {{type}} Read(ulong first, string second)
            {
                QueryBuilder.Read(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
            public ref {{type}} Read(string first, ulong second)
            {
                QueryBuilder.Read(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
            public ref {{type}} Read(string first, string second)
            {
                QueryBuilder.Read(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
            public ref {{type}} Read<T>()
            {
                QueryBuilder.Read<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
            public ref {{type}} Read<T>(T value) where T : Enum
            {
                QueryBuilder.Read(value);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
            public ref {{type}} Read<TFirst>(ulong second)
            {
                QueryBuilder.Read<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
            public ref {{type}} Read<TFirst>(string second)
            {
                QueryBuilder.Read<TFirst>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
            public ref {{type}} Read<TFirst, TSecond>()
            {
                QueryBuilder.Read<TFirst, TSecond>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
            public ref {{type}} Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
            {
                QueryBuilder.Read<TFirst, TSecond>(second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
            public ref {{type}} Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
            {
                QueryBuilder.Read<TFirst, TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
            public ref {{type}} Read<TFirst>(TFirst first, string second) where TFirst : Enum
            {
                QueryBuilder.Read(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
            public ref {{type}} Read<TSecond>(string first, TSecond second) where TSecond : Enum
            {
                QueryBuilder.Read(first, second);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
            public ref {{type}} ReadSecond<TSecond>(ulong first)
            {
                QueryBuilder.ReadSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
            public ref {{type}} ReadSecond<TSecond>(string first)
            {
                QueryBuilder.ReadSecond<TSecond>(first);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
            public ref {{type}} ScopeOpen()
            {
                QueryBuilder.ScopeOpen();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
            public ref {{type}} ScopeClose()
            {
                QueryBuilder.ScopeClose();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.Term()"/>
            public ref {{type}} Term()
            {
                QueryBuilder.Term();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
            public ref {{type}} TermAt(int termIndex)
            {
                QueryBuilder.TermAt(termIndex);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
            public ref {{type}} OrderBy(ulong component, Ecs.OrderByAction compare)
            {
                QueryBuilder.OrderBy(component, compare);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
            public ref {{type}} OrderBy<T>(Ecs.OrderByAction compare)
            {
                QueryBuilder.OrderBy<T>(compare);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
            public ref {{type}} GroupBy(ulong component)
            {
                QueryBuilder.GroupBy(component);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
            public ref {{type}} GroupBy<T>()
            {
                QueryBuilder.GroupBy<T>();
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
            public ref {{type}} GroupBy(ulong component, Ecs.GroupByAction callback)
            {
                QueryBuilder.GroupBy(component, callback);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
            public ref {{type}} GroupBy<T>(Ecs.GroupByAction callback)
            {
                QueryBuilder.GroupBy<T>(callback);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
            public ref {{type}} GroupBy(ulong component, Ecs.GroupByCallback callback)
            {
                QueryBuilder.GroupBy(component, callback);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
            public ref {{type}} GroupBy<T>(Ecs.GroupByCallback callback)
            {
                QueryBuilder.GroupBy<T>(callback);
                return ref this;
            }
    
            ///
            public ref {{type}} GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
            {
                QueryBuilder.GroupByCtx(ctx, contextFree);
                return ref this;
            }
    
            ///
            public ref {{type}} GroupByCtx(void* ctx)
            {
                QueryBuilder.GroupByCtx(ctx);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
            public ref {{type}} OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
            {
                QueryBuilder.OnGroupCreate(onGroupCreate);
                return ref this;
            }
    
            /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
            public ref {{type}} OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
            {
                QueryBuilder.OnGroupDelete(onGroupDelete);
                return ref this;
            }
        }
        """;
    }
}
