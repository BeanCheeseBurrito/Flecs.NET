using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Flecs.NET.Codegen
{
    [Generator]
    public class Generator : IIncrementalGenerator
    {
        private const int GenericCount = 16;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
            {
                postContext.AddSource("RoutineBuilder/QueryBuilder.g.cs", CodeFormatter.Format(GenerateRoutineBuilder()));
                postContext.AddSource("ObserverBuilder/QueryBuilder.g.cs", CodeFormatter.Format(GenerateObserverBuilder()));
                postContext.AddSource("PipelineBuilder/QueryBuilder.g.cs", CodeFormatter.Format(GeneratePipelineBuilder()));
                postContext.AddSource("AlertBuilder/QueryBuilder.g.cs", CodeFormatter.Format(GenerateAlertBuilder()));
                postContext.AddSource("Flecs.NET.g.cs", CodeFormatter.Format(Generate()));
            });
        }

        private static string GenerateRoutineBuilder()
        {
            return GenerateQueryBuilderExtensions("RoutineBuilder");
        }

        private static string GenerateObserverBuilder()
        {
            return GenerateQueryBuilderExtensions("ObserverBuilder");
        }

        private static string GeneratePipelineBuilder()
        {
            return GenerateQueryBuilderExtensions("PipelineBuilder");
        }

        private static string GenerateAlertBuilder()
        {
            return GenerateQueryBuilderExtensions("AlertBuilder");
        }

        private static string GenerateQueryBuilderExtensions(string name)
        {
            return $$"""
                   namespace Flecs.NET.Core
                   {
                       using System;
                       using static Flecs.NET.Bindings.flecs;
                       
                       public unsafe partial struct {{name}}
                       {
                           /// <inheritdoc cref="QueryBuilder.Self()"/>
                           public ref {{name}} Self()
                           {
                               QueryBuilder.Self();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
                           public ref {{name}} Id(ulong id)
                           {
                               QueryBuilder.Id(id);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
                           public ref {{name}} Entity(ulong entity)
                           {
                               QueryBuilder.Entity(entity);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Name(string)"/>
                           public ref {{name}} Name(string name)
                           {
                               QueryBuilder.Name(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Var(string)"/>
                           public ref {{name}} Var(string name)
                           {
                               QueryBuilder.Var(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
                           public ref {{name}} Term(ulong id)
                           {
                               QueryBuilder.Term(id);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Src()"/>
                           public ref {{name}} Src()
                           {
                               QueryBuilder.Src();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.First()"/>
                           public ref {{name}} First()
                           {
                               QueryBuilder.First();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Second()"/>
                           public ref {{name}} Second()
                           {
                               QueryBuilder.Second();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
                           public ref {{name}} Src(ulong srcId)
                           {
                               QueryBuilder.Src(srcId);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
                           public ref {{name}} Src<T>()
                           {
                               QueryBuilder.Src<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
                           public ref {{name}} Src<T>(T value) where T : Enum
                           {
                               QueryBuilder.Src(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Src(string)"/>
                           public ref {{name}} Src(string name)
                           {
                               QueryBuilder.Src(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
                           public ref {{name}} First(ulong firstId)
                           {
                               QueryBuilder.First(firstId);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.First{T}()"/>
                           public ref {{name}} First<T>()
                           {
                               QueryBuilder.First<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
                           public ref {{name}} First<T>(T value) where T : Enum
                           {
                               QueryBuilder.First(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.First(string)"/>
                           public ref {{name}} First(string name)
                           {
                               QueryBuilder.First(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
                           public ref {{name}} Second(ulong secondId)
                           {
                               QueryBuilder.Second(secondId);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
                           public ref {{name}} Second<T>()
                           {
                               QueryBuilder.Second<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
                           public ref {{name}} Second<T>(T value) where T : Enum
                           {
                               QueryBuilder.Second(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Second(string)"/>
                           public ref {{name}} Second(string secondName)
                           {
                               QueryBuilder.Second(secondName);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
                           public ref {{name}} Up(ulong traverse = 0)
                           {
                               QueryBuilder.Up(traverse);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
                           public ref {{name}} Up<T>()
                           {
                               QueryBuilder.Up<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
                           public ref {{name}} Up<T>(T value) where T : Enum
                           {
                               QueryBuilder.Up(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
                           public ref {{name}} Cascade(ulong traverse = 0)
                           {
                               QueryBuilder.Cascade(traverse);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
                           public ref {{name}} Cascade<T>()
                           {
                               QueryBuilder.Cascade<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
                           public ref {{name}} Cascade<T>(T value) where T : Enum
                           {
                               QueryBuilder.Cascade(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Descend()"/>
                           public ref {{name}} Descend()
                           {
                               QueryBuilder.Descend();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Parent()"/>
                           public ref {{name}} Parent()
                           {
                               QueryBuilder.Parent();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
                           public ref {{name}} Trav(ulong traverse, uint flags = 0)
                           {
                               QueryBuilder.Trav(traverse, flags);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
                           public ref {{name}} Trav<T>(uint flags = 0)
                           {
                               QueryBuilder.Trav<T>(flags);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
                           public ref {{name}} Trav<T>(T value, uint flags = 0) where T : Enum
                           {
                               QueryBuilder.Trav(value, flags);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="Core.QueryBuilder.Flags"/>
                           public ref {{name}} Flags(ulong flags)
                           {
                               QueryBuilder.Flags(flags);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
                           public ref {{name}} InOut(ecs_inout_kind_t inOut)
                           {
                               QueryBuilder.InOut(inOut);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
                           public ref {{name}} InOutStage(ecs_inout_kind_t inOut)
                           {
                               QueryBuilder.InOutStage(inOut);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write()"/>
                           public ref {{name}} Write()
                           {
                               QueryBuilder.Write();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read()"/>
                           public ref {{name}} Read()
                           {
                               QueryBuilder.Read();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
                           public ref {{name}} ReadWrite()
                           {
                               QueryBuilder.ReadWrite();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.In()"/>
                           public ref {{name}} In()
                           {
                               QueryBuilder.In();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Out()"/>
                           public ref {{name}} Out()
                           {
                               QueryBuilder.Out();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.InOut()"/>
                           public ref {{name}} InOut()
                           {
                               QueryBuilder.InOut();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
                           public ref {{name}} InOutNone()
                           {
                               QueryBuilder.InOutNone();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
                           public ref {{name}} Oper(ecs_oper_kind_t oper)
                           {
                               QueryBuilder.Oper(oper);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.And()"/>
                           public ref {{name}} And()
                           {
                               QueryBuilder.And();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Or()"/>
                           public ref {{name}} Or()
                           {
                               QueryBuilder.Or();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Not()"/>
                           public ref {{name}} Not()
                           {
                               QueryBuilder.Not();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Optional()"/>
                           public ref {{name}} Optional()
                           {
                               QueryBuilder.Optional();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
                           public ref {{name}} AndFrom()
                           {
                               QueryBuilder.AndFrom();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
                           public ref {{name}} OrFrom()
                           {
                               QueryBuilder.OrFrom();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
                           public ref {{name}} NotFrom()
                           {
                               QueryBuilder.NotFrom();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Singleton()"/>
                           public ref {{name}} Singleton()
                           {
                               QueryBuilder.Singleton();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Filter()"/>
                           public ref {{name}} Filter()
                           {
                               QueryBuilder.Filter();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="Core.QueryBuilder.QueryFlags"/>
                           public ref {{name}} QueryFlags(uint flags)
                           {
                               QueryBuilder.QueryFlags(flags);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
                           public ref {{name}} CacheKind(ecs_query_cache_kind_t kind)
                           {
                               QueryBuilder.CacheKind(kind);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Cached()"/>
                           public ref {{name}} Cached()
                           {
                               QueryBuilder.Cached();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
                           public ref {{name}} Expr(string expr)
                           {
                               QueryBuilder.Expr(expr);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
                           public ref {{name}} With(Term term)
                           {
                               QueryBuilder.With(term);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
                           public ref {{name}} With(ulong id)
                           {
                               QueryBuilder.With(id);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(string)"/>
                           public ref {{name}} With(string name)
                           {
                               QueryBuilder.With(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
                           public ref {{name}} With(ulong first, ulong second)
                           {
                               QueryBuilder.With(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
                           public ref {{name}} With(ulong first, string second)
                           {
                               QueryBuilder.With(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
                           public ref {{name}} With(string first, ulong second)
                           {
                               QueryBuilder.With(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
                           public ref {{name}} With(string first, string second)
                           {
                               QueryBuilder.With(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T}()"/>
                           public ref {{name}} With<T>()
                           {
                               QueryBuilder.With<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
                           public ref {{name}} With<T>(T value) where T : Enum
                           {
                               QueryBuilder.With(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
                           public ref {{name}} With<TFirst>(ulong second)
                           {
                               QueryBuilder.With<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
                           public ref {{name}} With<TFirst>(string second)
                           {
                               QueryBuilder.With<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
                           public ref {{name}} With<TFirst, TSecond>()
                           {
                               QueryBuilder.With<TFirst, TSecond>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
                           public ref {{name}} With<TFirst, TSecond>(TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.With<TFirst, TSecond>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
                           public ref {{name}} With<TFirst, TSecond>(TFirst first) where TFirst : Enum
                           {
                               QueryBuilder.With<TFirst, TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
                           public ref {{name}} With<TFirst>(TFirst first, string second) where TFirst : Enum
                           {
                               QueryBuilder.With(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
                           public ref {{name}} With<TSecond>(string first, TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.With(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
                           public ref {{name}} WithSecond<TSecond>(ulong first)
                           {
                               QueryBuilder.WithSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
                           public ref {{name}} WithSecond<TSecond>(string first)
                           {
                               QueryBuilder.WithSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
                           public ref {{name}} Without(Term term)
                           {
                               QueryBuilder.Without(term);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
                           public ref {{name}} Without(ulong id)
                           {
                               QueryBuilder.Without(id);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(string)"/>
                           public ref {{name}} Without(string name)
                           {
                               QueryBuilder.Without(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
                           public ref {{name}} Without(ulong first, ulong second)
                           {
                               QueryBuilder.Without(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
                           public ref {{name}} Without(ulong first, string second)
                           {
                               QueryBuilder.Without(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
                           public ref {{name}} Without(string first, ulong second)
                           {
                               QueryBuilder.Without(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
                           public ref {{name}} Without(string first, string second)
                           {
                               QueryBuilder.Without(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
                           public ref {{name}} Without<T>()
                           {
                               QueryBuilder.Without<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
                           public ref {{name}} Without<T>(T value) where T : Enum
                           {
                               QueryBuilder.Without(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
                           public ref {{name}} Without<TFirst>(ulong second)
                           {
                               QueryBuilder.Without<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
                           public ref {{name}} Without<TFirst>(string second)
                           {
                               QueryBuilder.Without<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
                           public ref {{name}} Without<TFirst, TSecond>()
                           {
                               QueryBuilder.Without<TFirst, TSecond>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
                           public ref {{name}} Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.Without<TFirst, TSecond>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
                           public ref {{name}} Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
                           {
                               QueryBuilder.Without<TFirst, TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
                           public ref {{name}} Without<TFirst>(TFirst first, string second) where TFirst : Enum
                           {
                               QueryBuilder.Without(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
                           public ref {{name}} Without<TSecond>(string first, TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.Without(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
                           public ref {{name}} WithoutSecond<TSecond>(ulong first)
                           {
                               QueryBuilder.WithoutSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
                           public ref {{name}} WithoutSecond<TSecond>(string first)
                           {
                               QueryBuilder.WithoutSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
                           public ref {{name}} Write(Term term)
                           {
                               QueryBuilder.Write(term);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
                           public ref {{name}} Write(ulong id)
                           {
                               QueryBuilder.Write(id);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(string)"/>
                           public ref {{name}} Write(string name)
                           {
                               QueryBuilder.Write(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
                           public ref {{name}} Write(ulong first, ulong second)
                           {
                               QueryBuilder.Write(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
                           public ref {{name}} Write(ulong first, string second)
                           {
                               QueryBuilder.Write(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
                           public ref {{name}} Write(string first, ulong second)
                           {
                               QueryBuilder.Write(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
                           public ref {{name}} Write(string first, string second)
                           {
                               QueryBuilder.Write(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
                           public ref {{name}} Write<T>()
                           {
                               QueryBuilder.Write<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
                           public ref {{name}} Write<T>(T value) where T : Enum
                           {
                               QueryBuilder.Write(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
                           public ref {{name}} Write<TFirst>(ulong second)
                           {
                               QueryBuilder.Write<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
                           public ref {{name}} Write<TFirst>(string second)
                           {
                               QueryBuilder.Write<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
                           public ref {{name}} Write<TFirst, TSecond>()
                           {
                               QueryBuilder.Write<TFirst, TSecond>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
                           public ref {{name}} Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.Write<TFirst, TSecond>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
                           public ref {{name}} Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
                           {
                               QueryBuilder.Write<TFirst, TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
                           public ref {{name}} Write<TFirst>(TFirst first, string second) where TFirst : Enum
                           {
                               QueryBuilder.Write(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
                           public ref {{name}} Write<TSecond>(string first, TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.Write(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
                           public ref {{name}} WriteSecond<TSecond>(ulong first)
                           {
                               QueryBuilder.WriteSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
                           public ref {{name}} WriteSecond<TSecond>(string first)
                           {
                               QueryBuilder.WriteSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
                           public ref {{name}} Read(Term term)
                           {
                               QueryBuilder.Read(term);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
                           public ref {{name}} Read(ulong id)
                           {
                               QueryBuilder.Read(id);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(string)"/>
                           public ref {{name}} Read(string name)
                           {
                               QueryBuilder.Read(name);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
                           public ref {{name}} Read(ulong first, ulong second)
                           {
                               QueryBuilder.Read(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
                           public ref {{name}} Read(ulong first, string second)
                           {
                               QueryBuilder.Read(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
                           public ref {{name}} Read(string first, ulong second)
                           {
                               QueryBuilder.Read(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
                           public ref {{name}} Read(string first, string second)
                           {
                               QueryBuilder.Read(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
                           public ref {{name}} Read<T>()
                           {
                               QueryBuilder.Read<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
                           public ref {{name}} Read<T>(T value) where T : Enum
                           {
                               QueryBuilder.Read(value);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
                           public ref {{name}} Read<TFirst>(ulong second)
                           {
                               QueryBuilder.Read<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
                           public ref {{name}} Read<TFirst>(string second)
                           {
                               QueryBuilder.Read<TFirst>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
                           public ref {{name}} Read<TFirst, TSecond>()
                           {
                               QueryBuilder.Read<TFirst, TSecond>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
                           public ref {{name}} Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.Read<TFirst, TSecond>(second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
                           public ref {{name}} Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
                           {
                               QueryBuilder.Read<TFirst, TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
                           public ref {{name}} Read<TFirst>(TFirst first, string second) where TFirst : Enum
                           {
                               QueryBuilder.Read(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
                           public ref {{name}} Read<TSecond>(string first, TSecond second) where TSecond : Enum
                           {
                               QueryBuilder.Read(first, second);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
                           public ref {{name}} ReadSecond<TSecond>(ulong first)
                           {
                               QueryBuilder.ReadSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
                           public ref {{name}} ReadSecond<TSecond>(string first)
                           {
                               QueryBuilder.ReadSecond<TSecond>(first);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
                           public ref {{name}} ScopeOpen()
                           {
                               QueryBuilder.ScopeOpen();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
                           public ref {{name}} ScopeClose()
                           {
                               QueryBuilder.ScopeClose();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.Term()"/>
                           public ref {{name}} Term()
                           {
                               QueryBuilder.Term();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
                           public ref {{name}} TermAt(int termIndex)
                           {
                               QueryBuilder.TermAt(termIndex);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
                           public ref {{name}} OrderBy(ulong component, Ecs.OrderByAction compare)
                           {
                               QueryBuilder.OrderBy(component, compare);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
                           public ref {{name}} OrderBy<T>(Ecs.OrderByAction compare)
                           {
                               QueryBuilder.OrderBy<T>(compare);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
                           public ref {{name}} GroupBy(ulong component)
                           {
                               QueryBuilder.GroupBy(component);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
                           public ref {{name}} GroupBy<T>()
                           {
                               QueryBuilder.GroupBy<T>();
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
                           public ref {{name}} GroupBy(ulong component, Ecs.GroupByAction callback)
                           {
                               QueryBuilder.GroupBy(component, callback);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
                           public ref {{name}} GroupBy<T>(Ecs.GroupByAction callback)
                           {
                               QueryBuilder.GroupBy<T>(callback);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
                           public ref {{name}} GroupBy(ulong component, Ecs.GroupByCallback callback)
                           {
                               QueryBuilder.GroupBy(component, callback);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
                           public ref {{name}} GroupBy<T>(Ecs.GroupByCallback callback)
                           {
                               QueryBuilder.GroupBy<T>(callback);
                               return ref this;
                           }
                   
                           ///
                           public ref {{name}} GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
                           {
                               QueryBuilder.GroupByCtx(ctx, contextFree);
                               return ref this;
                           }
                   
                           ///
                           public ref {{name}} GroupByCtx(void* ctx)
                           {
                               QueryBuilder.GroupByCtx(ctx);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
                           public ref {{name}} OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
                           {
                               QueryBuilder.OnGroupCreate(onGroupCreate);
                               return ref this;
                           }
                   
                           /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
                           public ref {{name}} OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
                           {
                               QueryBuilder.OnGroupDelete(onGroupDelete);
                               return ref this;
                           }
                       }
                   }
                   """;
        }

        private static string Generate()
        {
            return $@"
                #pragma warning disable 1591
                #nullable enable              

                using System;
                using System.Runtime.CompilerServices;
                using System.Runtime.InteropServices;
                using Flecs.NET.Utilities; 
                using static Flecs.NET.Bindings.flecs;

                namespace Flecs.NET.Core 
                {{
                    {GenerateWorldExtensions()}
                    {GenerateEntityExtensions()}
                    {GenerateEcsExtensions()}
                    {GenerateInvokerExtensions()}
                    {GenerateBindingContextExtensions()}
                    {GenerateIterableExtensions()}
                    {GenerateObserverExtensions()}
                    {GenerateRoutineExtensions()}
                }}
                
                #pragma warning restore 1591
            ";
        }

        private static string GenerateWorldExtensions()
        {
            return $@"
                public unsafe partial struct World
                {{
                    {GenerateBuilderFactoryExtensions()}
                    {GenerateWorldEachCallbackFunctions()}
                }}
            ";
        }

        private static string GenerateEntityExtensions()
        {
            return $@"
                public unsafe partial struct Entity
                {{
                    {GenerateEntityReadCallbacks()}
                    {GenerateEntityWriteCallbacks()} 
                    {GenerateEntityEnsureCallbacks()}
                }}
            ";
        }

        private static string GenerateEcsExtensions()
        {
            return $@"
                public static unsafe partial class Ecs 
                {{
                    {GenerateDelegates()}
                }}
            ";
        }

        private static string GenerateInvokerExtensions()
        {
            return $@"
                public static unsafe partial class Invoker 
                {{
                    {GenerateIterInvokers()}
                    {GenerateEachInvokers()}
                    {GenerateGetPointers()}
                    {GenerateEnsurePointers()}
                    {GenerateReadInvokers()}
                    {GenerateWriteInvokers()}
                    {GenerateEnsureInvokers()}
                }}
            ";
        }

        private static string GenerateIterableExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeConstraints = ConcatString(i + 1, " ", index => $"where T{index} : unmanaged");
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.Append($@"
                    {GenerateIterableCallbackFunctions($"Iter<{typeParams}>", $"Ecs.IterFieldCallback<{typeParams}>", $"delegate*<Iter, {fieldParams}, void>")}
                    {GenerateIterableCallbackFunctions($"Iter<{typeParams}>", $"Ecs.IterSpanCallback<{typeParams}>", $"delegate*<Iter, {spanParams}, void>", typeConstraints)}
                    {GenerateIterableCallbackFunctions($"Iter<{typeParams}>", $"Ecs.IterPointerCallback<{typeParams}>", $"delegate*<Iter, {pointerParams}, void>", typeConstraints)}
                    {GenerateIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachRefCallback<{typeParams}>", $"delegate*<{refParams}, void>")} 
                    {GenerateIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachEntityRefCallback<{typeParams}>", $"delegate*<Entity, {refParams}, void>")} 
                    {GenerateIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachIterRefCallback<{typeParams}>", $"delegate*<Iter, int, {refParams}, void>")}
                    {GenerateIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachPointerCallback<{typeParams}>", $"delegate*<{pointerParams}, void>", typeConstraints)} 
                    {GenerateIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachEntityPointerCallback<{typeParams}>", $"delegate*<Entity, {pointerParams}, void>", typeConstraints)} 
                    {GenerateIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachIterPointerCallback<{typeParams}>", $"delegate*<Iter, int, {pointerParams}, void>", typeConstraints)}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindRefCallback<{typeParams}>", $"delegate*<{refParams}, bool>")}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindEntityRefCallback<{typeParams}>", $"delegate*<Entity, {refParams}, bool>")}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindIterRefCallback<{typeParams}>", $"delegate*<Iter, int, {refParams}, bool>")}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindPointerCallback<{typeParams}>", $"delegate*<{pointerParams}, bool>", typeConstraints)}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindEntityPointerCallback<{typeParams}>", $"delegate*<Entity, {pointerParams}, bool>", typeConstraints)}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindIterPointerCallback<{typeParams}>", $"delegate*<Iter, int, {pointerParams}, bool>", typeConstraints)}
                ");
            }

            return $@"
                public unsafe partial struct Query
                {{
                    {str}
                }}

                public unsafe partial struct IterIterable
                {{
                    {str}
                }}

                public unsafe partial struct PageIterable
                {{
                    {str}
                }}

                public unsafe partial struct WorkerIterable
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateNodeBuilderExtensions(string name)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeConstraints = ConcatString(i + 1, " ", index => $"where T{index} : unmanaged");
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public {name} Iter<{typeParams}>(Ecs.IterFieldCallback<{typeParams}> callback) 
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.IterFieldCallbackPointer).Build();
                    }}

                    public {name} Iter<{typeParams}>(Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.IterSpanCallbackPointer).Build();
                    }}

                    public {name} Iter<{typeParams}>(Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.IterPointerCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(Ecs.EachRefCallback<{typeParams}> callback) 
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.EachRefCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(Ecs.EachEntityRefCallback<{typeParams}> callback) 
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.EachEntityRefCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(Ecs.EachIterRefCallback<{typeParams}> callback) 
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.EachIterRefCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(Ecs.EachPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.EachPointerCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(Ecs.EachEntityPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.EachEntityPointerCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(Ecs.EachIterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return SetCallback(callback, BindingContext<{typeParams}>.EachIterPointerCallbackPointer).Build();
                    }}

                #if NET5_0_OR_GREATER
                    public {name} Iter<{typeParams}>(delegate*<Iter, {fieldParams}, void> callback) 
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.IterFieldCallbackPointer).Build();
                    }}

                    public {name} Iter<{typeParams}>(delegate*<Iter, {spanParams}, void> callback) 
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.IterSpanCallbackPointer).Build();
                    }}

                    public {name} Iter<{typeParams}>(delegate*<Iter, {pointerParams}, void> callback) 
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.IterPointerCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(delegate*<{refParams}, void> callback) 
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.EachRefCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(delegate*<Entity, {refParams}, void> callback) 
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.EachEntityRefCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(delegate*<Iter, int, {refParams}, void> callback) 
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.EachIterRefCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(delegate*<{pointerParams}, void> callback) {typeConstraints}
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.EachPointerCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(delegate*<Entity, {pointerParams}, void> callback) {typeConstraints}
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.EachEntityPointerCallbackPointer).Build();
                    }}

                    public {name} Each<{typeParams}>(delegate*<Iter, int, {pointerParams}, void> callback) {typeConstraints}
                    {{
                        return SetCallback((IntPtr)callback, BindingContext<{typeParams}>.EachIterPointerCallbackPointer).Build();
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateObserverExtensions()
        {
            return $@"
                public unsafe partial struct ObserverBuilder
                {{
                    {GenerateNodeBuilderExtensions("Observer")}
                }}
            ";
        }

        private static string GenerateRoutineExtensions()
        {
            return $@"
                public unsafe partial struct RoutineBuilder
                {{
                    {GenerateNodeBuilderExtensions("Routine")}
                }}
            ";
        }

        private static string GenerateBindingContextExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    internal static unsafe partial class BindingContext<{typeParams}>
                    {{
                        #if NET5_0_OR_GREATER
                        {GenerateBindingContextPointers("IterFieldCallback")}
                        {GenerateBindingContextPointers("IterSpanCallback")}
                        {GenerateBindingContextPointers("IterPointerCallback")}
                        {GenerateBindingContextPointers("EachRefCallback")}
                        {GenerateBindingContextPointers("EachEntityRefCallback")}
                        {GenerateBindingContextPointers("EachIterRefCallback")}
                        {GenerateBindingContextPointers("EachPointerCallback")}
                        {GenerateBindingContextPointers("EachEntityPointerCallback")}
                        {GenerateBindingContextPointers("EachIterPointerCallback")}
                        #else
                        {GenerateBindingContextDelegates("IterFieldCallback")}
                        {GenerateBindingContextDelegates("IterSpanCallback")}
                        {GenerateBindingContextDelegates("IterPointerCallback")}
                        {GenerateBindingContextDelegates("EachRefCallback")}
                        {GenerateBindingContextDelegates("EachEntityRefCallback")}
                        {GenerateBindingContextDelegates("EachIterRefCallback")}
                        {GenerateBindingContextDelegates("EachPointerCallback")}
                        {GenerateBindingContextDelegates("EachEntityPointerCallback")}
                        {GenerateBindingContextDelegates("EachIterPointerCallback")}
                        #endif
                        {GenerateBindingContextCallbacks("IterFieldCallback", $"Ecs.IterFieldCallback<{typeParams}>", $"delegate*<Iter, {fieldParams}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("IterSpanCallback", $"Ecs.IterSpanCallback<{typeParams}>", $"delegate*<Iter, {spanParams}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("IterPointerCallback", $"Ecs.IterPointerCallback<{typeParams}>", $"delegate*<Iter, {pointerParams}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("EachRefCallback", $"Ecs.EachRefCallback<{typeParams}>", $"delegate*<{refParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachEntityRefCallback", $"Ecs.EachEntityRefCallback<{typeParams}>", $"delegate*<Entity, {refParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachIterRefCallback", $"Ecs.EachIterRefCallback<{typeParams}>", $"delegate*<Iter, int, {refParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachPointerCallback", $"Ecs.EachPointerCallback<{typeParams}>", $"delegate*<{pointerParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachEntityPointerCallback", $"Ecs.EachEntityPointerCallback<{typeParams}>", $"delegate*<Entity, {pointerParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachIterPointerCallback", $"Ecs.EachIterPointerCallback<{typeParams}>", $"delegate*<Iter, int, {pointerParams}, void>", "Each")} 
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateBuilderFactoryExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string termBuilders = ConcatString(i + 1, "\n", index => $".With<T{index}>()");
                str.AppendLine($@"
                    public AlertBuilder AlertBuilder<{typeParams}>()
                    {{
                        return new AlertBuilder(Handle){termBuilders};
                    }}

                    public AlertBuilder AlertBuilder<{typeParams}>(string name)
                    {{
                        return new AlertBuilder(Handle, name){termBuilders};
                    }}

                    public AlertBuilder AlertBuilder<{typeParams}>(ulong entity)
                    {{
                        return new AlertBuilder(Handle, entity){termBuilders};
                    }}

                    public Alert Alert<{typeParams}>()
                    {{
                        return AlertBuilder<{typeParams}>().Build();
                    }}

                    public Alert Alert<{typeParams}>(string name)
                    {{
                        return AlertBuilder<{typeParams}>(name).Build();
                    }}

                    public Alert Alert<{typeParams}>(ulong entity)
                    {{
                        return AlertBuilder<{typeParams}>(entity).Build();
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>()
                    {{
                        return new QueryBuilder(Handle){termBuilders};
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>(string name)
                    {{
                        return new QueryBuilder(Handle, name){termBuilders};
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>(ulong entity)
                    {{
                        return new QueryBuilder(Handle, entity){termBuilders};
                    }}

                    public Query Query<{typeParams}>()
                    {{
                        return QueryBuilder<{typeParams}>().Build();
                    }}

                    public Query Query<{typeParams}>(string name)
                    {{
                        return QueryBuilder<{typeParams}>(name).Build();
                    }}

                    public Query Query<{typeParams}>(ulong entity)
                    {{
                        return QueryBuilder<{typeParams}>(entity).Build();
                    }}

                    public RoutineBuilder Routine<{typeParams}>()
                    {{
                        return new RoutineBuilder(Handle){termBuilders};
                    }}

                    public RoutineBuilder Routine<{typeParams}>(string name)
                    {{
                        return new RoutineBuilder(Handle, name){termBuilders};
                    }}

                    public ObserverBuilder Observer<{typeParams}>()
                    {{
                        return new ObserverBuilder(Handle){termBuilders};
                    }}

                    public ObserverBuilder Observer<{typeParams}>(string name)
                    {{
                        return new ObserverBuilder(Handle, name){termBuilders};
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWorldEachCallbackFunctions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string refArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public void Each<{typeParams}>(Ecs.EachRefCallback<{typeParams}> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);   
                    }}

                    public void Each<{typeParams}>(Ecs.EachEntityRefCallback<{typeParams}> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);
                    }}

                #if NET5_0_OR_GREATER
                    public void Each<{typeParams}>(delegate*<{refArgs}, void> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);   
                    }}

                    public void Each<{typeParams}>(delegate*<Entity, {refArgs}, void> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityReadCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public bool Read<{typeParams}>(Ecs.InvokeReadCallback<{typeParams}> callback)
                    {{
                        return Invoker.InvokeRead(World, Id, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityWriteCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public bool Write<{typeParams}>(Ecs.InvokeWriteCallback<{typeParams}> callback)
                    {{
                        return Invoker.InvokeWrite(World, Id, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityEnsureCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public ref Entity Insert<{typeParams}>(Ecs.InvokeInsertCallback<{typeParams}> callback)
                    {{
                        Invoker.InvokeEnsure(World, Id, callback);
                        return ref this;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}> field{index}");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}> span{index}");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}* pointer{index}");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                string inParams = ConcatString(i + 1, ", ", index => $"in T{index} comp{index}");

                str.AppendLine($"public delegate void IterFieldCallback<{typeParams}>(Iter it, {fieldParams});");
                str.AppendLine($"public delegate void IterSpanCallback<{typeParams}>(Iter it, {spanParams});");
                str.AppendLine($"public delegate void IterPointerCallback<{typeParams}>(Iter it, {pointerParams});");

                str.AppendLine($"public delegate void EachRefCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate void EachEntityRefCallback<{typeParams}>(Entity entity, {refParams});");
                str.AppendLine($"public delegate void EachIterRefCallback<{typeParams}>(Iter it, int i, {refParams});");
                str.AppendLine($"public delegate void EachPointerCallback<{typeParams}>({pointerParams});");
                str.AppendLine($"public delegate void EachEntityPointerCallback<{typeParams}>(Entity entity, {pointerParams});");
                str.AppendLine($"public delegate void EachIterPointerCallback<{typeParams}>(Iter it, int i, {pointerParams});");

                str.AppendLine($"public delegate bool FindRefCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate bool FindEntityRefCallback<{typeParams}>(Entity entity, {refParams});");
                str.AppendLine($"public delegate bool FindIterRefCallback<{typeParams}>(Iter it, int i, {refParams});");
                str.AppendLine($"public delegate bool FindPointerCallback<{typeParams}>({pointerParams});");
                str.AppendLine($"public delegate bool FindEntityPointerCallback<{typeParams}>(Entity entity, {pointerParams});");
                str.AppendLine($"public delegate bool FindIterPointerCallback<{typeParams}>(Iter it, int i, {pointerParams});");

                str.AppendLine($"public delegate void InvokeReadCallback<{typeParams}>({inParams});");
                str.AppendLine($"public delegate void InvokeWriteCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate void InvokeInsertCallback<{typeParams}>({refParams});");
            }

            return str.ToString();
        }

        private static string GenerateIterInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");

                string fieldArgs = ConcatString(i + 1, ", ", index => $"it.Field<T{index}>({index})");
                string spanArgs = ConcatString(i + 1, ", ", index => $"it.GetSpan<T{index}>({index})");
                string pointerArgs = ConcatString(i + 1, ", ", index => $"it.GetPointer<T{index}>({index})");

                string fieldBody = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {fieldArgs});
                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string spanBody = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {spanArgs});
                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string pointerBody = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {pointerArgs});
                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                str.AppendLine($@"
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        {fieldBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterSpanCallback<{typeParams}> callback)
                    {{
                        {spanBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterPointerCallback<{typeParams}> callback)
                    {{
                        {pointerBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {fieldParams}, void> callback)
                    {{
                        {fieldBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {spanParams}, void> callback)
                    {{
                        {spanBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {pointerParams}, void> callback)
                    {{
                        {pointerBody}
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateEachInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");

                string fieldAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertField<T{index}>(iter, {index});");

                string sizes = ConcatString(i + 1, "\n",
                    index => $"int stride{index} = (iter->sources == null || iter->sources[{index}] == 0) && (iter->set_fields & (1 << {index})) != 0 && !Type<T{index}>.IsTag ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"T{index}* pointer{index} = (T{index}*)iter->ptrs[{index}];");

                string refArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(pointer{index})");

                string pointerArgs = ConcatString(i + 1, ", ",
                    index => $"pointer{index}");

                string increments = ConcatString(i + 1, ", ",
                    index => $"pointer{index} = &pointer{index}[stride{index}]");

                string eachRef = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                        callback({refArgs});

                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string eachPointer = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                        callback({pointerArgs});

                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string eachEntityRef = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.Assert(iter->count > 0, ""No entities returned, use Iter() or Each() without the entity argument instead."");

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    for (int i = 0; i < iter->count; i++, {increments})
                        callback(new Entity(iter->world, iter->entities[i]), {refArgs});

                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string eachEntityPointer = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.Assert(iter->count > 0, ""No entities returned, use Iter() or Each() without the entity argument instead."");

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    for (int i = 0; i < iter->count; i++, {increments})
                        callback(new Entity(iter->world, iter->entities[i]), {pointerArgs});

                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string eachIterRef = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                        callback(new Iter(iter), i, {refArgs});

                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string eachIterPointer = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                        callback(new Iter(iter), i, {pointerArgs});

                    Ecs.TableUnlock(iter->world, iter->table);
                ";

                string findRef = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    Entity result = default;

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                    {{
                        if (!callback({refArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Ecs.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findPointer = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    Entity result = default;

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                    {{
                        if (!callback({pointerArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Ecs.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findEntityRef = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.Assert(iter->count > 0, ""No entities returned, use Find() without the Entity argument instead."");

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    Entity result = default;

                    for (int i = 0; i < iter->count; i++, {increments})
                    {{
                        if (!callback(new Entity(iter->world, iter->entities[i]), {refArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Ecs.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findEntityPointer = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});
                    Ecs.Assert(iter->count > 0, ""No entities returned, use Find() without the Entity argument instead."");

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    Entity result = default;

                    for (int i = 0; i < iter->count; i++, {increments})
                    {{
                        if (!callback(new Entity(iter->world, iter->entities[i]), {pointerArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Ecs.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findIterRef = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    Entity result = default;

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                    {{
                        if (!callback(new Iter(iter), i, {refArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Ecs.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findIterPointer = $@"
                    Core.Iter.AssertFieldCount(iter, {i + 1});

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Ecs.TableLock(iter->world, iter->table);

                    Entity result = default;

                    int count = iter->count == 0 && iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                    {{
                        if (!callback(new Iter(iter), i, {pointerArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Ecs.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachRefCallback<{typeParams}> callback)
                    {{
                        {eachRef}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachEntityRefCallback<{typeParams}> callback)
                    {{
                        {eachEntityRef}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachIterRefCallback<{typeParams}> callback)
                    {{
                        {eachIterRef}
                    }}
                    
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachPointerCallback<{typeParams}> callback)
                    {{
                        {eachPointer}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachEntityPointerCallback<{typeParams}> callback)
                    {{
                        {eachEntityPointer}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachIterPointerCallback<{typeParams}> callback)
                    {{
                        {eachIterPointer}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindRefCallback<{typeParams}> callback)
                    {{
                        {findRef}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindEntityRefCallback<{typeParams}> callback)
                    {{
                        {findEntityRef}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindIterRefCallback<{typeParams}> callback)
                    {{
                        {findIterRef}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindPointerCallback<{typeParams}> callback)
                    {{
                        {findPointer}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindEntityPointerCallback<{typeParams}> callback)
                    {{
                        {findEntityPointer}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindIterPointerCallback<{typeParams}> callback)
                    {{
                        {findIterPointer}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<{refParams}, void> callback)
                    {{
                        {eachRef}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {refParams}, void> callback)
                    {{
                        {eachEntityRef}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {refParams}, void> callback)
                    {{
                        {eachIterRef}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<{pointerParams}, void> callback)
                    {{
                        {eachPointer}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {pointerParams}, void> callback)
                    {{
                        {eachEntityPointer}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {pointerParams}, void> callback)
                    {{
                        {eachIterPointer}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<{refParams}, bool> callback)
                    {{
                        {findRef}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {refParams}, bool> callback)
                    {{
                        {findEntityRef}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {refParams}, bool> callback)
                    {{
                        {findIterRef}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<{pointerParams}, bool> callback)
                    {{
                        {findPointer}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {pointerParams}, bool> callback)
                    {{
                        {findEntityPointer}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {pointerParams}, bool> callback)
                    {{
                        {findIterPointer}
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateGetPointers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string columnIndexes = ConcatString(i + 1, "\n",
                    index => $"int t{index} = ecs_table_get_column_index(realWorld, table, Type<T{index}>.Id(world));");

                string populatePointers = ConcatString(i + 1, "\n",
                    index => $@"
                        if (t{index} == -1)
                        {{
                            void* ptr = ecs_get_mut_id(world, e, Type<T{index}>.Id(world));
                    
                            if (ptr == null)
                                return false;

                            ptrs[{index}] = ptr;
                        }}
                        else
                        {{
                            ptrs[{index}] = ecs_record_get_by_column(r, t{index}, default);
                        }}
                    "
                );

                str.AppendLine($@"
                    internal static bool GetPointers<{typeParams}>(ecs_world_t* world, ulong e, ecs_record_t* r, ecs_table_t* table, void** ptrs)
                    {{
                        Ecs.Assert(table != null, nameof(ECS_INTERNAL_ERROR));

                        if (ecs_table_column_count(table) == 0 && ecs_table_has_flags(table, EcsTableHasSparse) == 0)
                            return false;

                        ecs_world_t* realWorld = ecs_get_world(world);

                        {columnIndexes}
                        {populatePointers}

                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEnsurePointers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeIds = ConcatString(i + 1, "\n",
                    index => $"ptrs[{index}] = ecs_ensure_id(world, e, Type<T{index}>.Id(world));");

                str.AppendLine($@"
                    internal static bool EnsurePointers<{typeParams}>(ecs_world_t* world, ulong e, void** ptrs)
                    {{
                        {typeIds}
                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateReadInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"in Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                str.AppendLine($@"
                    internal static bool InvokeRead<{typeParams}>(ecs_world_t* world, ulong e, Ecs.InvokeReadCallback<{typeParams}> callback)
                    {{
                        ecs_record_t* r = ecs_read_begin(world, e);

                        if (r == null)
                            return false;

                        ecs_table_t *table = r->table;

                        if (table == null)
                            return false;

                        void** ptrs = stackalloc void*[{i + 1}];
                        bool hasComponents = GetPointers<{typeParams}>(world, e, r, table, ptrs);

                        if (hasComponents)
                            callback({callbackArgs});

                        ecs_read_end(r);

                        return hasComponents;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWriteInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                str.AppendLine($@"
                    internal static bool InvokeWrite<{typeParams}>(ecs_world_t* world, ulong e, Ecs.InvokeWriteCallback<{typeParams}> callback)
                    {{
                        ecs_record_t* r = ecs_write_begin(world, e);

                        if (r == null)
                            return false;

                        ecs_table_t *table = r->table;

                        if (table == null)
                            return false;

                        void** ptrs = stackalloc void*[{i + 1}];
                        bool hasComponents = GetPointers<{typeParams}>(world, e, r, table, ptrs);

                        if (hasComponents)
                            callback({callbackArgs});

                        ecs_write_end(r);

                        return hasComponents;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEnsureInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string addedIds = ConcatString(i + 1, "\n", index => $@"
                    next = ecs_table_add_id(world, prev, Type<T{index}>.Id(world));
                    if (prev != next) added[elem++] = Type<T{index}>.Id(world);
                    prev = next;
                ");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                string modified = ConcatString(i + 1, "\n",
                    index => $"ecs_modified_id(world, id, Type<T{index}>.Id(world));");

                str.AppendLine($@"
                    internal static bool InvokeEnsure<{typeParams}>(ecs_world_t* world, ulong id, Ecs.InvokeInsertCallback<{typeParams}> callback)
                    {{
                        World w = new World(world);

                        void** ptrs = stackalloc void*[{i + 1}];
                        ecs_table_t* table = null;

                        if (!w.IsDeferred())
                        {{
                            Ecs.Assert(!w.IsStage(), nameof(ECS_INVALID_PARAMETER));

                            ecs_record_t* r = ecs_record_find(world, id);
                            if (r != null)
                                table = r->table;

                            ecs_table_t* prev = table;
                            ecs_table_t* next;
                            int elem = 0;
                            ulong* added = stackalloc ulong[{i + 1}];

                            {addedIds}

                            if (table != next)
                            {{
                                ecs_type_t ids = default;
                                ids.array = added;
                                ids.count = elem;
                                ecs_commit(world, id, r, next, &ids, null);
                                table = next;
                            }}

                            if (!GetPointers<{typeParams}>(w, id, r, table, ptrs))
                                Ecs.Error(nameof(ECS_INTERNAL_ERROR));

                            Ecs.TableLock(world, table);
                        }}
                        else
                        {{
                            EnsurePointers<{typeParams}>(world, id, ptrs);
                        }}

                        callback({callbackArgs});

                        if (!w.IsDeferred())
                            Ecs.TableUnlock(world, table);

                        {modified}

                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateBindingContextPointers(string callbackName)
        {
            return $@"
                internal static readonly IntPtr {callbackName}Pointer =
                    (IntPtr)(delegate* <ecs_iter_t*, void>)&{callbackName};
            ";
        }

        private static string GenerateBindingContextDelegates(string functionName)
        {
            return $@"
                internal static readonly IntPtr {functionName}Pointer =
                    Marshal.GetFunctionPointerForDelegate({functionName}Reference = {functionName});
                private static readonly Ecs.IterAction {functionName}Reference;
            ";
        }

        private static string GenerateBindingContextCallbacks(
            string callbackName,
            string delegateName,
            string functionPointerName,
            string invokerName)
        {
            return $@"
                internal static void {callbackName}(ecs_iter_t* iter)
                {{
                    BindingContext.IteratorContext* context = (BindingContext.IteratorContext*)iter->callback_ctx;
            #if NET5_0_OR_GREATER
                    if (context->Callback.Pointer != default)
                    {{
                        Invoker.{invokerName}(iter, ({functionPointerName})context->Callback.Pointer);
                        return;
                    }}
            #endif
                    {delegateName} callback = ({delegateName})context->Callback.GcHandle.Target!;
                    Invoker.{invokerName}(iter, callback);
                }}
            ";
        }

        private static string GenerateIterableCallbackFunctions(
            string functionName,
            string delegateName,
            string functionPointerName,
            string typeConstraints = "")
        {
            return $@"
                public void {functionName}({delegateName} callback) {typeConstraints}
                {{
                    ecs_iter_t it = GetIter();
                    while (GetNext(&it))
                        Invoker.{functionName}(&it, callback);
                }}

                #if NET5_0_OR_GREATER
                public void {functionName}({functionPointerName} callback) {typeConstraints}
                {{
                    ecs_iter_t it = GetIter();
                    while (GetNext(&it))
                        Invoker.{functionName}(&it, callback);
                }}
                #endif
            ";
        }

        private static string GenerateIterableFindCallbackFunctions(string typeParams, string delegateName, string functionPointerName, string typeConstraints = "")
        {
            string methodBody = $@"
                ecs_iter_t it = GetIter();

                Entity result = default;
                while (result == 0 && GetNext(&it))
                    result = Invoker.Find(&it, callback);
                
                if (result != 0)
                    ecs_iter_fini(&it);

                return result;
            ";

            return $@"
                public Entity Find<{typeParams}>({delegateName} callback) {typeConstraints}
                {{
                    {methodBody}
                }}
            #if NET5_0_OR_GREATER
                public Entity Find<{typeParams}>({functionPointerName} callback) {typeConstraints}
                {{
                    {methodBody}
                }}
            #endif
            ";
        }

        private static string ConcatString(int count, string separator, Func<int, string> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                str.Append(callback(i));
                if (i < count - 1)
                    str.Append(separator);
            }

            return str.ToString();
        }

        private static string GenerateTypeParams(int num)
        {
            return ConcatString(num, ", ", index => $"T{index}");
        }
    }
}
