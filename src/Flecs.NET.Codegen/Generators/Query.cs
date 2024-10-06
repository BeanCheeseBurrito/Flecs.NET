using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class Query : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"Query/T{i + 1}.g.cs", GenerateQuery(i));
                Generator.AddSource(postContext, $"Query.IIterable/T{i + 1}.g.cs", IIterable.GenerateExtensions(Type.Query, i));
            }
        });
    }

    private static string GenerateQuery(int i)
    {
        return $$"""
        #nullable enable
        
        using System;
        using System.Runtime.CompilerServices;
        
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;
        
        /// <summary>
        ///     A type-safe wrapper around <see cref="Query"/> that takes {{i + 1}} type arguments.
        /// </summary>
        /// {{Generator.XmlTypeParameters[i]}}
        public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : IDisposable, IEquatable<{{Generator.GetTypeName(Type.Query, i)}}>
        {
            private Query _query;
        
            /// <inheritdoc cref="Query.Handle"/>
            public ref ecs_query_t* Handle => ref _query.Handle;
        
            /// <inheritdoc cref="Query(ecs_query_t*)"/>
            public Query(ecs_query_t* query)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _query = new Query(query);
            }
        
            /// <inheritdoc cref="Query(ecs_world_t*, ulong)"/>
            public Query(ecs_world_t* world, ulong entity)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _query = new Query(world, entity);
            }
        
            /// <inheritdoc cref="Query(Core.Entity)"/>
            public Query(Entity entity)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _query = new Query(entity);
            }
        
            /// <inheritdoc cref="Query.Dispose()"/>
            public void Dispose()
            {
                _query.Dispose();
            }
        
            /// <inheritdoc cref="Query.Destruct()"/>
            public void Destruct()
            {
                _query.Destruct();
            }
        
            /// <inheritdoc cref="Query.Entity()"/>
            public Entity Entity()
            {
                return _query.Entity();
            }
        
            /// <inheritdoc cref="Query.CPtr()"/>
            public ecs_query_t* CPtr()
            {
                return _query.CPtr();
            }
        
            /// <inheritdoc cref="Query.Changed()"/>
            public bool Changed()
            {
                return _query.Changed();
            }
        
            /// <inheritdoc cref="Query.GroupInfo(ulong)"/>
            public ecs_query_group_info_t* GroupInfo(ulong groupId)
            {
                return _query.GroupInfo(groupId);
            }
        
            /// <inheritdoc cref="Query.GroupCtx(ulong)"/>
            public void* GroupCtx(ulong groupId)
            {
                return _query.GroupCtx(groupId);
            }
        
            /// <inheritdoc cref="Query.EachTerm(Ecs.TermCallback)"/>
            public void EachTerm(Ecs.TermCallback callback)
            {
                _query.EachTerm(callback);
            }
        
            /// <inheritdoc cref="Query.Term(int)"/>
            public Term Term(int index)
            {
                return _query.Term(index);
            }
        
            /// <inheritdoc cref="Query.TermCount()"/>
            public int TermCount()
            {
                return _query.TermCount();
            }
        
            /// <inheritdoc cref="Query.FieldCount()"/>
            public int FieldCount()
            {
                return _query.FieldCount();
            }
        
            /// <inheritdoc cref="Query.FindVar(string)"/>
            public int FindVar(string name)
            {
                return _query.FindVar(name);
            }
        
            /// <inheritdoc cref="Query.Str()"/>
            public string Str()
            {
                return _query.Str();
            }
        
            /// <inheritdoc cref="Query.Plan()"/>
            public string Plan()
            {
                return _query.Plan();
            }
        
            /// <inheritdoc cref="Query.To(Query)"/>
            public static ecs_query_t* To(Query<{{Generator.TypeParameters[i]}}> query)
            {
                return query.Handle;
            }
        
            /// <inheritdoc cref="Query.ToBoolean(Query)"/>
            public static bool ToBoolean(Query<{{Generator.TypeParameters[i]}}> query)
            {
                return query.Handle != null;
            }
        
            /// <inheritdoc cref="Query.To(Query)"/>
            public static implicit operator ecs_query_t*(Query<{{Generator.TypeParameters[i]}}> query)
            {
                return To(query);
            }
        
            /// <inheritdoc cref="Query.ToBoolean(Query)"/>
            public static implicit operator bool(Query<{{Generator.TypeParameters[i]}}> query)
            {
                return ToBoolean(query);
            }
        
            /// <inheritdoc cref="Query.Equals(Query)"/>
            public bool Equals(Query<{{Generator.TypeParameters[i]}}> other)
            {
                return _query.Equals(other._query);
            }
        
            /// <inheritdoc cref="Query.Equals(object)"/>
            public override bool Equals(object? obj)
            {
                return obj is Query<{{Generator.TypeParameters[i]}}> other && Equals(other);
            }
        
            /// <inheritdoc cref="Query.GetHashCode()"/>
            public override int GetHashCode()
            {
                return _query.GetHashCode();
            }
        
            /// <inheritdoc cref="Query.op_Equality"/>
            public static bool operator ==(Query<{{Generator.TypeParameters[i]}}> left, Query<{{Generator.TypeParameters[i]}}> right)
            {
                return left.Equals(right);
            }
        
            /// <inheritdoc cref="Query.op_Inequality"/>
            public static bool operator !=(Query<{{Generator.TypeParameters[i]}}> left, Query<{{Generator.TypeParameters[i]}}> right)
            {
                return !(left == right);
            }
        }
        
        // Flecs.NET Extensions
        public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}}
        {
            /// <inheritdoc cref="Query.World()"/>
            public World World()
            {
                return _query.World();
            }
        
            /// <inheritdoc cref="Query.RealWorld()"/>
            public World RealWorld()
            {
                return _query.RealWorld();
            }
        }
        
        // IIterableBase Interface
        public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : IIterableBase
        {
            /// <inheritdoc cref="IIterableBase.World"/>
            ref ecs_world_t* IIterableBase.World => ref Ecs.GetIterableWorld(ref _query);
        
            /// <inheritdoc cref="Query.GetIter(ecs_world_t*)"/>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ecs_iter_t GetIter(ecs_world_t* world = null)
            {
                return _query.GetIter();
            }
            
            /// <inheritdoc cref="Query.GetNext(ecs_iter_t*)"/>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool GetNext(ecs_iter_t* it)
            {
                return _query.GetNext(it);
            }
        }
        
        // {{Generator.GetTypeName(Type.IIterable, i)}} Interface
        public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
        {
            /// <inheritdoc cref="Query.Page(int, int)"/>
            public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
            {
                return new {{Generator.GetTypeName(Type.PageIterable, i)}}(_query.Page(offset, limit));
            }
            
            /// <inheritdoc cref="Query.Worker(int, int)"/>
            public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
            {
                return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(_query.Worker(index, count));
            }
        
            /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.World)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.Iter(world));
            }
            
            /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.Iter)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.Iter(it));
            }
            
            /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.Entity)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.Iter(entity));
            }
            
            /// <inheritdoc cref="Query.Count()"/>
            public int Count()
            {
                return _query.Count();
            }
            
            /// <inheritdoc cref="Query.IsTrue()"/>
            public bool IsTrue()
            {
                return _query.IsTrue();
            }
            
            /// <inheritdoc cref="Query.First()"/>
            public Entity First()
            {
                return _query.First();
            }
            
            /// <inheritdoc cref="Query.SetVar(int, ulong)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetVar(varId, value));
            }
            
            /// <inheritdoc cref="Query.SetVar(string, ulong)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetVar(name, value));
            }
            
            /// <inheritdoc cref="Query.SetVar(string, ecs_table_t*)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetVar(name, value));
            }
            
            /// <inheritdoc cref="Query.SetVar(string, ecs_table_range_t)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetVar(name, value));
            }
            
            /// <inheritdoc cref="Query.SetVar(string, Table)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetVar(name, value));
            }
            
            /// <inheritdoc cref="Query.SetGroup(ulong)"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetGroup(groupId));
            }
            
            /// <inheritdoc cref="Query.SetGroup{T}()"/>
            public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
            {
                return new {{Generator.GetTypeName(Type.IterIterable, i)}}(_query.SetGroup<T>());
            }
        }
        """;
    }
}
