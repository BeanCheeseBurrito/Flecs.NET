using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class IterIterable : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"IterIterable/T{i + 1}.g.cs", GenerateIterIterable(i));
            AddSource($"IterIterable.IIterable/T{i + 1}.g.cs",
                IIterable.GenerateExtensions(Type.IterIterable, i));
        }
    }

    private static string GenerateIterIterable(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using System.Runtime.CompilerServices;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <inheritdoc cref="IterIterable"/>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{Generator.GetTypeName(Type.IterIterable, i)}} : IEquatable<{{Generator.GetTypeName(Type.IterIterable, i)}}>
            {
                private IterIterable _iterIterable;
                
                internal IterIterable(IterIterable iterIterable)
                {
                    _iterIterable = iterIterable;
                }
            
                /// <inheritdoc cref="IterIterable(ecs_iter_t, IterableType)"/>
                public IterIterable(ecs_iter_t iter, IterableType iterableType)
                {
                    _iterIterable = new IterIterable(iter, iterableType);
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(int, ulong)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    _iterIterable.SetVar(varId, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, ulong)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    _iterIterable.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_t*)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    _iterIterable.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_range_t)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    _iterIterable.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, Table)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    _iterIterable.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.ToJson(ecs_iter_to_json_desc_t*)"/>
                public string ToJson(ecs_iter_to_json_desc_t* desc)
                {
                    return _iterIterable.ToJson(desc);
                }
            
                /// <inheritdoc cref="IterIterable.ToJson()"/>
                public string ToJson()
                {
                    return _iterIterable.ToJson();
                }
            
                /// <inheritdoc cref="IterIterable.ToJson(ref IterToJsonDesc)"/>
                public string ToJson(ref IterToJsonDesc desc)
                {
                    return _iterIterable.ToJson(desc);
                }
            
                /// <inheritdoc cref="IterIterable.ToJson(IterToJsonDesc)"/>
                public string ToJson(IterToJsonDesc desc)
                {
                    return _iterIterable.ToJson(desc);
                }
            
                /// <inheritdoc cref="IterIterable.Count()"/>
                public int Count()
                {
                    return _iterIterable.Count();
                }
            
                /// <inheritdoc cref="IterIterable.IsTrue()"/>
                public bool IsTrue()
                {
                    return _iterIterable.IsTrue();
                }
            
                /// <inheritdoc cref="IterIterable.First()"/>
                public Entity First()
                {
                    return _iterIterable.First();
                }
            
                /// <inheritdoc cref="IterIterable.SetGroup(ulong)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    _iterIterable.SetGroup(groupId);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetGroup{T}()"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    _iterIterable.SetGroup<T>();
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.Equals(IterIterable)"/>
                public bool Equals({{Generator.GetTypeName(Type.IterIterable, i)}} other)
                {
                    return _iterIterable.Equals(other._iterIterable);
                }
            
                /// <inheritdoc cref="IterIterable.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.IterIterable, i)}} other && Equals(other);
                }
            
                /// <inheritdoc cref="IterIterable.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _iterIterable.GetHashCode();
                }
            
                /// <inheritdoc cref="IterIterable.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.IterIterable, i)}} left, {{Generator.GetTypeName(Type.IterIterable, i)}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="IterIterable.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.IterIterable, i)}} left, {{Generator.GetTypeName(Type.IterIterable, i)}} right)
                {
                    return !(left == right);
                }
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.IterIterable, i)}} : IIterableBase
            {
                /// <inheritdoc cref="IterIterable.World"/>
                public ref ecs_world_t* World => ref _iterIterable.World;
                
                /// <inheritdoc cref="IterIterable.GetIter(ecs_world_t*)"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public ecs_iter_t GetIter(ecs_world_t* world = null)
                {
                    return _iterIterable.GetIter(world);
                }
                
                /// <inheritdoc cref="IterIterable.GetNext(ecs_iter_t*)"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool GetNext(ecs_iter_t* it)
                {
                    return _iterIterable.GetNext(it);
                }
            }

            // {{Generator.GetTypeName(Type.IIterable, i)}} Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.IterIterable, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="IterIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(_iterIterable.Page(offset, limit));
                }
                
                /// <inheritdoc cref="IterIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(_iterIterable.Worker(index, count));
                }
            
                /// <inheritdoc cref="IterIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new(_iterIterable.Iter(world));
                }
                
                /// <inheritdoc cref="IterIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new(_iterIterable.Iter(it));
                }
                
                /// <inheritdoc cref="IterIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new(_iterIterable.Iter(entity));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(int, ulong)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(int varId, ulong value)
                {
                    return new(_iterIterable.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, ulong)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, ulong value)
                {
                    return new(_iterIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_t*)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, ecs_table_t* value)
                {
                    return new(_iterIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_range_t)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, ecs_table_range_t value)
                {
                    return new(_iterIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, Table)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, Table value)
                {
                    return new(_iterIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetGroup(ulong)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetGroup(ulong groupId)
                {
                    return new(_iterIterable.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="IterIterable.SetGroup{T}()"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetGroup<T>()
                {
                    return new(_iterIterable.SetGroup<T>()); 
                }
            }
            """;
    }
}
