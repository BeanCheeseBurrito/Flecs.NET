using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for building pipelines.
    /// </summary>
    public unsafe partial struct PipelineBuilder : IDisposable, IEquatable<PipelineBuilder>
    {
        private ecs_world_t* _world;
        private ecs_pipeline_desc_t _pipelineDesc;

        internal QueryBuilder QueryBuilder;

        /// <summary>
        ///     Reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Creates a pipeline builder with the provided world and entity id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="entity"></param>
        public PipelineBuilder(ecs_world_t* world, ulong entity)
        {
            _world = world;
            _pipelineDesc = default;
            QueryBuilder = new QueryBuilder(world);

            _pipelineDesc.entity = entity;
        }

        /// <summary>
        ///     Creates a pipeline builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public PipelineBuilder(ecs_world_t* world, string? name = null)
        {
            _world = world;
            _pipelineDesc = default;
            QueryBuilder = new QueryBuilder(world);

            if (string.IsNullOrEmpty(name))
                return;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;
            _pipelineDesc.entity = ecs_entity_init(world, &entityDesc);
        }

        /// <summary>
        ///     Disposes the pipeline builder. This should be called if the rule builder
        ///     will be discarded and .Build() isn't called.
        /// </summary>
        public void Dispose()
        {
            QueryBuilder.Dispose();
        }

        /// <summary>
        ///     Builds a new pipeline.
        /// </summary>
        /// <returns></returns>
        public Pipeline Build()
        {
            fixed (ecs_pipeline_desc_t* pipelineDesc = &_pipelineDesc)
            {
                BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
                queryContext[0] = QueryBuilder.Context;

                pipelineDesc->query = QueryBuilder.Desc;
                pipelineDesc->query.binding_ctx = queryContext;
                pipelineDesc->query.binding_ctx_free = BindingContext.QueryContextFreePointer;

                Entity entity = new Entity(World, ecs_pipeline_init(World, pipelineDesc));

                if (entity == 0)
                    Ecs.Error("Pipeline failed to init.");

                return new Pipeline(entity);
            }
        }

        /// <summary>
        ///     Checks if two <see cref="PipelineBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PipelineBuilder other)
        {
            return _pipelineDesc == other._pipelineDesc && QueryBuilder == other.QueryBuilder;
        }

        /// <summary>
        ///     Checks if two <see cref="PipelineBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is PipelineBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="PipelineBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_pipelineDesc.GetHashCode(), QueryBuilder.GetHashCode());
        }

        /// <summary>
        ///     Checks if two <see cref="PipelineBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(PipelineBuilder left, PipelineBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="PipelineBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(PipelineBuilder left, PipelineBuilder right)
        {
            return !(left == right);
        }
    }

    // QueryBuilder Extensions
    public unsafe partial struct PipelineBuilder
    {
        /// <inheritdoc cref="QueryBuilder.Self()"/>
        public ref PipelineBuilder Self()
        {
            QueryBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Id(ulong)"/>
        public ref PipelineBuilder Id(ulong id)
        {
            QueryBuilder.Id(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Entity(ulong)"/>
        public ref PipelineBuilder Entity(ulong entity)
        {
            QueryBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Name(string)"/>
        public ref PipelineBuilder Name(string name)
        {
            QueryBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Var(string)"/>
        public ref PipelineBuilder Var(string name)
        {
            QueryBuilder.Var(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term(ulong)"/>
        public ref PipelineBuilder Term(ulong id)
        {
            QueryBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src()"/>
        public ref PipelineBuilder Src()
        {
            QueryBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First()"/>
        public ref PipelineBuilder First()
        {
            QueryBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second()"/>
        public ref PipelineBuilder Second()
        {
            QueryBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(ulong)"/>
        public ref PipelineBuilder Src(ulong srcId)
        {
            QueryBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}()"/>
        public ref PipelineBuilder Src<T>()
        {
            QueryBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src{T}(T)"/>
        public ref PipelineBuilder Src<T>(T value) where T : Enum
        {
            QueryBuilder.Src(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Src(string)"/>
        public ref PipelineBuilder Src(string name)
        {
            QueryBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(ulong)"/>
        public ref PipelineBuilder First(ulong firstId)
        {
            QueryBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}()"/>
        public ref PipelineBuilder First<T>()
        {
            QueryBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First{T}(T)"/>
        public ref PipelineBuilder First<T>(T value) where T : Enum
        {
            QueryBuilder.First(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.First(string)"/>
        public ref PipelineBuilder First(string name)
        {
            QueryBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(ulong)"/>
        public ref PipelineBuilder Second(ulong secondId)
        {
            QueryBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}()"/>
        public ref PipelineBuilder Second<T>()
        {
            QueryBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second{T}(T)"/>
        public ref PipelineBuilder Second<T>(T value) where T : Enum
        {
            QueryBuilder.Second(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Second(string)"/>
        public ref PipelineBuilder Second(string secondName)
        {
            QueryBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up(ulong)"/>
        public ref PipelineBuilder Up(ulong traverse = 0)
        {
            QueryBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}()"/>
        public ref PipelineBuilder Up<T>()
        {
            QueryBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Up{T}(T)"/>
        public ref PipelineBuilder Up<T>(T value) where T : Enum
        {
            QueryBuilder.Up(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade(ulong)"/>
        public ref PipelineBuilder Cascade(ulong traverse = 0)
        {
            QueryBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}()"/>
        public ref PipelineBuilder Cascade<T>()
        {
            QueryBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cascade{T}(T)"/>
        public ref PipelineBuilder Cascade<T>(T value) where T : Enum
        {
            QueryBuilder.Cascade(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Descend()"/>
        public ref PipelineBuilder Descend()
        {
            QueryBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Parent()"/>
        public ref PipelineBuilder Parent()
        {
            QueryBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav(ulong, uint)"/>
        public ref PipelineBuilder Trav(ulong traverse, uint flags = 0)
        {
            QueryBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(uint)"/>
        public ref PipelineBuilder Trav<T>(uint flags = 0)
        {
            QueryBuilder.Trav<T>(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Trav{T}(T, uint)"/>
        public ref PipelineBuilder Trav<T>(T value, uint flags = 0) where T : Enum
        {
            QueryBuilder.Trav(value, flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.IdFlags(ulong)"/>
        public ref PipelineBuilder IdFlags(ulong flags)
        {
            QueryBuilder.IdFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut(ecs_inout_kind_t)"/>
        public ref PipelineBuilder InOut(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutStage(ecs_inout_kind_t)"/>
        public ref PipelineBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            QueryBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write()"/>
        public ref PipelineBuilder Write()
        {
            QueryBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read()"/>
        public ref PipelineBuilder Read()
        {
            QueryBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadWrite()"/>
        public ref PipelineBuilder ReadWrite()
        {
            QueryBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.In()"/>
        public ref PipelineBuilder In()
        {
            QueryBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Out()"/>
        public ref PipelineBuilder Out()
        {
            QueryBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOut()"/>
        public ref PipelineBuilder InOut()
        {
            QueryBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.InOutNone()"/>
        public ref PipelineBuilder InOutNone()
        {
            QueryBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Oper(ecs_oper_kind_t)"/>
        public ref PipelineBuilder Oper(ecs_oper_kind_t oper)
        {
            QueryBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.And()"/>
        public ref PipelineBuilder And()
        {
            QueryBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Or()"/>
        public ref PipelineBuilder Or()
        {
            QueryBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Not()"/>
        public ref PipelineBuilder Not()
        {
            QueryBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Optional()"/>
        public ref PipelineBuilder Optional()
        {
            QueryBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.AndFrom()"/>
        public ref PipelineBuilder AndFrom()
        {
            QueryBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrFrom()"/>
        public ref PipelineBuilder OrFrom()
        {
            QueryBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.NotFrom()"/>
        public ref PipelineBuilder NotFrom()
        {
            QueryBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Singleton()"/>
        public ref PipelineBuilder Singleton()
        {
            QueryBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Filter()"/>
        public ref PipelineBuilder Filter()
        {
            QueryBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Instanced()"/>
        public ref PipelineBuilder Instanced()
        {
            QueryBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Flags(uint)"/>
        public ref PipelineBuilder Flags(uint flags)
        {
            QueryBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.CacheKind(ecs_query_cache_kind_t)"/>
        public ref PipelineBuilder CacheKind(ecs_query_cache_kind_t kind)
        {
            QueryBuilder.CacheKind(kind);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Cached()"/>
        public ref PipelineBuilder Cached()
        {
            QueryBuilder.Cached();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Expr(string)"/>
        public ref PipelineBuilder Expr(string expr)
        {
            QueryBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(Core.Term)"/>
        public ref PipelineBuilder With(Term term)
        {
            QueryBuilder.With(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong)"/>
        public ref PipelineBuilder With(ulong id)
        {
            QueryBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string)"/>
        public ref PipelineBuilder With(string name)
        {
            QueryBuilder.With(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, ulong)"/>
        public ref PipelineBuilder With(ulong first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(ulong, string)"/>
        public ref PipelineBuilder With(ulong first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, ulong)"/>
        public ref PipelineBuilder With(string first, ulong second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With(string, string)"/>
        public ref PipelineBuilder With(string first, string second)
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}()"/>
        public ref PipelineBuilder With<T>()
        {
            QueryBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T}(T)"/>
        public ref PipelineBuilder With<T>(T value) where T : Enum
        {
            QueryBuilder.With(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(ulong)"/>
        public ref PipelineBuilder With<TFirst>(ulong second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(string)"/>
        public ref PipelineBuilder With<TFirst>(string second)
        {
            QueryBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}()"/>
        public ref PipelineBuilder With<TFirst, TSecond>()
        {
            QueryBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T2)"/>
        public ref PipelineBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1, T2}(T1)"/>
        public ref PipelineBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.With<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T1}(T1, string)"/>
        public ref PipelineBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.With{T2}(string, T2)"/>
        public ref PipelineBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(ulong)"/>
        public ref PipelineBuilder WithSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithSecond{T2}(string)"/>
        public ref PipelineBuilder WithSecond<TSecond>(string first)
        {
            QueryBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(Core.Term)"/>
        public ref PipelineBuilder Without(Term term)
        {
            QueryBuilder.Without(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong)"/>
        public ref PipelineBuilder Without(ulong id)
        {
            QueryBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string)"/>
        public ref PipelineBuilder Without(string name)
        {
            QueryBuilder.Without(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, ulong)"/>
        public ref PipelineBuilder Without(ulong first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(ulong, string)"/>
        public ref PipelineBuilder Without(ulong first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, ulong)"/>
        public ref PipelineBuilder Without(string first, ulong second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without(string, string)"/>
        public ref PipelineBuilder Without(string first, string second)
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}()"/>
        public ref PipelineBuilder Without<T>()
        {
            QueryBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T}(T)"/>
        public ref PipelineBuilder Without<T>(T value) where T : Enum
        {
            QueryBuilder.Without(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(ulong)"/>
        public ref PipelineBuilder Without<TFirst>(ulong second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(string)"/>
        public ref PipelineBuilder Without<TFirst>(string second)
        {
            QueryBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}()"/>
        public ref PipelineBuilder Without<TFirst, TSecond>()
        {
            QueryBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T2)"/>
        public ref PipelineBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1, T2}(T1)"/>
        public ref PipelineBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Without<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T1}(T1, string)"/>
        public ref PipelineBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Without{T2}(string, T2)"/>
        public ref PipelineBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(ulong)"/>
        public ref PipelineBuilder WithoutSecond<TSecond>(ulong first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WithoutSecond{T2}(string)"/>
        public ref PipelineBuilder WithoutSecond<TSecond>(string first)
        {
            QueryBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(Core.Term)"/>
        public ref PipelineBuilder Write(Term term)
        {
            QueryBuilder.Write(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong)"/>
        public ref PipelineBuilder Write(ulong id)
        {
            QueryBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string)"/>
        public ref PipelineBuilder Write(string name)
        {
            QueryBuilder.Write(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, ulong)"/>
        public ref PipelineBuilder Write(ulong first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(ulong, string)"/>
        public ref PipelineBuilder Write(ulong first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, ulong)"/>
        public ref PipelineBuilder Write(string first, ulong second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write(string, string)"/>
        public ref PipelineBuilder Write(string first, string second)
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}()"/>
        public ref PipelineBuilder Write<T>()
        {
            QueryBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T}(T)"/>
        public ref PipelineBuilder Write<T>(T value) where T : Enum
        {
            QueryBuilder.Write(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(ulong)"/>
        public ref PipelineBuilder Write<TFirst>(ulong second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(string)"/>
        public ref PipelineBuilder Write<TFirst>(string second)
        {
            QueryBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}()"/>
        public ref PipelineBuilder Write<TFirst, TSecond>()
        {
            QueryBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T2)"/>
        public ref PipelineBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1, T2}(T1)"/>
        public ref PipelineBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Write<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T1}(T1, string)"/>
        public ref PipelineBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Write{T2}(string, T2)"/>
        public ref PipelineBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(ulong)"/>
        public ref PipelineBuilder WriteSecond<TSecond>(ulong first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.WriteSecond{T2}(string)"/>
        public ref PipelineBuilder WriteSecond<TSecond>(string first)
        {
            QueryBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(Core.Term)"/>
        public ref PipelineBuilder Read(Term term)
        {
            QueryBuilder.Read(term);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong)"/>
        public ref PipelineBuilder Read(ulong id)
        {
            QueryBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string)"/>
        public ref PipelineBuilder Read(string name)
        {
            QueryBuilder.Read(name);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, ulong)"/>
        public ref PipelineBuilder Read(ulong first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(ulong, string)"/>
        public ref PipelineBuilder Read(ulong first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, ulong)"/>
        public ref PipelineBuilder Read(string first, ulong second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read(string, string)"/>
        public ref PipelineBuilder Read(string first, string second)
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}()"/>
        public ref PipelineBuilder Read<T>()
        {
            QueryBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T}(T)"/>
        public ref PipelineBuilder Read<T>(T value) where T : Enum
        {
            QueryBuilder.Read(value);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(ulong)"/>
        public ref PipelineBuilder Read<TFirst>(ulong second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(string)"/>
        public ref PipelineBuilder Read<TFirst>(string second)
        {
            QueryBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}()"/>
        public ref PipelineBuilder Read<TFirst, TSecond>()
        {
            QueryBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T2)"/>
        public ref PipelineBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1, T2}(T1)"/>
        public ref PipelineBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            QueryBuilder.Read<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T1}(T1, string)"/>
        public ref PipelineBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Read{T2}(string, T2)"/>
        public ref PipelineBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            QueryBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(ulong)"/>
        public ref PipelineBuilder ReadSecond<TSecond>(ulong first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ReadSecond{T2}(string)"/>
        public ref PipelineBuilder ReadSecond<TSecond>(string first)
        {
            QueryBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeOpen()"/>
        public ref PipelineBuilder ScopeOpen()
        {
            QueryBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.ScopeClose()"/>
        public ref PipelineBuilder ScopeClose()
        {
            QueryBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.Term()"/>
        public ref PipelineBuilder Term()
        {
            QueryBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.TermAt(int)"/>
        public ref PipelineBuilder TermAt(int termIndex)
        {
            QueryBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy(ulong, Ecs.OrderByAction)"/>
        public ref PipelineBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy(component, compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OrderBy{T}(Ecs.OrderByAction)"/>
        public ref PipelineBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            QueryBuilder.OrderBy<T>(compare);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong)"/>
        public ref PipelineBuilder GroupBy(ulong component)
        {
            QueryBuilder.GroupBy(component);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}()"/>
        public ref PipelineBuilder GroupBy<T>()
        {
            QueryBuilder.GroupBy<T>();
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByAction)"/>
        public ref PipelineBuilder GroupBy(ulong component, Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByAction)"/>
        public ref PipelineBuilder GroupBy<T>(Ecs.GroupByAction callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy(ulong, Ecs.GroupByCallback)"/>
        public ref PipelineBuilder GroupBy(ulong component, Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy(component, callback);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.GroupBy{T}(Ecs.GroupByCallback)"/>
        public ref PipelineBuilder GroupBy<T>(Ecs.GroupByCallback callback)
        {
            QueryBuilder.GroupBy<T>(callback);
            return ref this;
        }

        ///
        public ref PipelineBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            QueryBuilder.GroupByCtx(ctx, contextFree);
            return ref this;
        }

        ///
        public ref PipelineBuilder GroupByCtx(void* ctx)
        {
            QueryBuilder.GroupByCtx(ctx);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupCreate(Ecs.GroupCreateAction)"/>
        public ref PipelineBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            QueryBuilder.OnGroupCreate(onGroupCreate);
            return ref this;
        }

        /// <inheritdoc cref="QueryBuilder.OnGroupDelete(Ecs.GroupDeleteAction)"/>
        public ref PipelineBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            QueryBuilder.OnGroupDelete(onGroupDelete);
            return ref this;
        }
    }
}
