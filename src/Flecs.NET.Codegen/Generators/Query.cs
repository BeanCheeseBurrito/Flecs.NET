using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Query : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
            AddSource($"Query/T{i + 1}.g.cs", GenerateQuery(i));

        for (int i = -1; i < Generator.GenericCount; i++)
            AddSource($"Query.IIterable/T{i + 1}.g.cs", IIterable.GenerateIterators(Type.Query, i));
    }

    private static string GenerateQuery(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using System.Runtime.CompilerServices;
            using Flecs.NET.Utilities;

            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="Query"/> that takes {{i + 1}} type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : IDisposable, IEquatable<{{Generator.GetTypeName(Type.Query, i)}}>
            {
                /// <inheritdoc cref="IQuery.Underlying"/>
                public Query Underlying;
            
                /// <inheritdoc cref="Query.Handle"/>
                public ecs_query_t* Handle => Underlying.Handle;
            
                /// <inheritdoc cref="Query(ecs_query_t*)"/>
                public Query(ecs_query_t* query)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertNoTags();
                    Underlying = new Query(query);
                }
            
                /// <inheritdoc cref="Query(ecs_world_t*, ulong)"/>
                public Query(ecs_world_t* world, ulong entity)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertNoTags();
                    Underlying = new Query(world, entity);
                }
            
                /// <inheritdoc cref="Query(Core.Entity)"/>
                public Query(Entity entity)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertNoTags();
                    Underlying = new Query(entity);
                }
            
                /// <inheritdoc cref="Query.Dispose()"/>
                public void Dispose()
                {
                    Underlying.Dispose();
                }
            
                /// <inheritdoc cref="Query.Destruct()"/>
                public void Destruct()
                {
                    Underlying.Destruct();
                }
            
                /// <inheritdoc cref="Query.Entity()"/>
                public Entity Entity()
                {
                    return Underlying.Entity();
                }
            
                /// <inheritdoc cref="Query.CPtr()"/>
                public ecs_query_t* CPtr()
                {
                    return Underlying.CPtr();
                }
            
                /// <inheritdoc cref="Query.Changed()"/>
                public bool Changed()
                {
                    return Underlying.Changed();
                }
            
                /// <inheritdoc cref="Query.GroupInfo(ulong)"/>
                public ecs_query_group_info_t* GroupInfo(ulong groupId)
                {
                    return Underlying.GroupInfo(groupId);
                }
            
                /// <inheritdoc cref="Query.GroupCtx{T}(ulong)"/>
                public ref T GroupCtx<T>(ulong group)
                {
                    return ref Underlying.GroupCtx<T>(group);
                }
            
                /// <inheritdoc cref="Query.EachTerm(Ecs.TermCallback)"/>
                public void EachTerm(Ecs.TermCallback callback)
                {
                    Underlying.EachTerm(callback);
                }
            
                /// <inheritdoc cref="Query.Term(int)"/>
                public Term Term(int index)
                {
                    return Underlying.Term(index);
                }
            
                /// <inheritdoc cref="Query.TermCount()"/>
                public int TermCount()
                {
                    return Underlying.TermCount();
                }
            
                /// <inheritdoc cref="Query.FieldCount()"/>
                public int FieldCount()
                {
                    return Underlying.FieldCount();
                }
            
                /// <inheritdoc cref="Query.FindVar(string)"/>
                public int FindVar(string name)
                {
                    return Underlying.FindVar(name);
                }
            
                /// <inheritdoc cref="Query.Str()"/>
                public string Str()
                {
                    return Underlying.Str();
                }
            
                /// <inheritdoc cref="Query.Plan()"/>
                public string Plan()
                {
                    return Underlying.Plan();
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
                    return Underlying.Equals(other.Underlying);
                }
            
                /// <inheritdoc cref="Query.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is Query<{{Generator.TypeParameters[i]}}> other && Equals(other);
                }
            
                /// <inheritdoc cref="Query.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return Underlying.GetHashCode();
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
                    return Underlying.World();
                }
            
                /// <inheritdoc cref="Query.RealWorld()"/>
                public World RealWorld()
                {
                    return Underlying.RealWorld();
                }
            }
            
            // IPageIterable Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : IQuery
            {
                ref Query IQuery.Underlying => ref Underlying;
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : IIterableBase
            {
                /// <inheritdoc cref="IIterableBase.World"/>
                ecs_world_t* IIterableBase.World => Handle->world;
                
                /// <inheritdoc cref="IIterableBase.GetIter"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public ecs_iter_t GetIter(World world = default)
                {
                    return Underlying.GetIter(world);
                }
                
                /// <inheritdoc cref="IIterableBase.GetNext"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool GetNext(Iter it)
                {
                    return Underlying.GetNext(it);
                }
            }

            // {{Generator.GetTypeName(Type.IIterable, i)}} Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.Query, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="Query.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(Underlying.Page(offset, limit));
                }
                
                /// <inheritdoc cref="Query.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(Underlying.Worker(index, count));
                }
            
                /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.Iter(world));
                }
                
                /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.Iter(it));
                }
                
                /// <inheritdoc cref="Query.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.Iter(entity));
                }
                
                /// <inheritdoc cref="Query.Count()"/>
                public int Count()
                {
                    return Underlying.Count();
                }
                
                /// <inheritdoc cref="Query.IsTrue()"/>
                public bool IsTrue()
                {
                    return Underlying.IsTrue();
                }
                
                /// <inheritdoc cref="Query.First()"/>
                public Entity First()
                {
                    return Underlying.First();
                }
                
                /// <inheritdoc cref="Query.SetVar(int, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="Query.SetVar(string, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="Query.SetVar(string, ecs_table_t*)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="Query.SetVar(string, ecs_table_range_t)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="Query.SetVar(string, Table)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="Query.SetGroup(ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="Query.SetGroup{T}()"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    return new {{Generator.GetTypeName(Type.IterIterable, i)}}(Underlying.SetGroup<T>());
                }
            }
            """;
    }
}
