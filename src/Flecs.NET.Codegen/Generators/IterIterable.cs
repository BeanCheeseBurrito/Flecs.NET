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
                /// <inheritdoc cref="IIterIterable.Underlying"/>
                public IterIterable Underlying;
                
                /// <inheritdoc cref="IIterIterable.Iterator"/>
                public ref ecs_iter_t Iterator => ref Underlying.Iterator;
                
                /// <inheritdoc cref="IIterIterable.IterableType"/>
                public readonly IterableType IterableType => Underlying.IterableType;
                
                internal IterIterable(IterIterable handle)
                {
                    Underlying = handle;
                }
            
                /// <inheritdoc cref="IterIterable(ecs_iter_t, IterableType)"/>
                public IterIterable(ecs_iter_t iter, IterableType iterableType)
                {
                    Underlying = new IterIterable(iter, iterableType);
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(int, ulong)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    Underlying.SetVar(varId, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, ulong)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    Underlying.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_t*)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    Underlying.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_range_t)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    Underlying.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetVar(string, Table)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    Underlying.SetVar(name, value);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.ToJson(in IterToJsonDesc)"/>
                public string ToJson(in IterToJsonDesc desc)
                {
                    return Underlying.ToJson(in desc);
                }
                
                /// <inheritdoc cref="IterIterable.ToJson()"/>
                public string ToJson()
                {
                    return Underlying.ToJson();
                }
            
                /// <inheritdoc cref="IterIterable.Count()"/>
                public int Count()
                {
                    return Underlying.Count();
                }
            
                /// <inheritdoc cref="IterIterable.IsTrue()"/>
                public bool IsTrue()
                {
                    return Underlying.IsTrue();
                }
            
                /// <inheritdoc cref="IterIterable.First()"/>
                public Entity First()
                {
                    return Underlying.First();
                }
            
                /// <inheritdoc cref="IterIterable.SetGroup(ulong)"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    Underlying.SetGroup(groupId);
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.SetGroup{T}()"/>
                public ref {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    Underlying.SetGroup<T>();
                    return ref this;
                }
            
                /// <inheritdoc cref="IterIterable.Equals(IterIterable)"/>
                public bool Equals({{Generator.GetTypeName(Type.IterIterable, i)}} other)
                {
                    return Underlying.Equals(other.Underlying);
                }
            
                /// <inheritdoc cref="IterIterable.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.IterIterable, i)}} other && Equals(other);
                }
            
                /// <inheritdoc cref="IterIterable.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return Underlying.GetHashCode();
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
            
            // IIterIterable Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.IterIterable, i)}} : IIterIterable
            {
                ref IterIterable IIterIterable.Underlying => ref Underlying;
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.IterIterable, i)}} : IIterableBase
            {
                /// <inheritdoc cref="IIterableBase.World"/>
                ecs_world_t* IIterableBase.World => Iterator.world;
                
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
            public unsafe partial struct {{Generator.GetTypeName(Type.IterIterable, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="IterIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(Underlying.Page(offset, limit));
                }
                
                /// <inheritdoc cref="IterIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(Underlying.Worker(index, count));
                }
            
                /// <inheritdoc cref="IterIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new(Underlying.Iter(world));
                }
                
                /// <inheritdoc cref="IterIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new(Underlying.Iter(it));
                }
                
                /// <inheritdoc cref="IterIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new(Underlying.Iter(entity));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(int, ulong)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(int varId, ulong value)
                {
                    return new(Underlying.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, ulong)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, ulong value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_t*)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, ecs_table_t* value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, ecs_table_range_t)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, ecs_table_range_t value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetVar(string, Table)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetVar(string name, Table value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="IterIterable.SetGroup(ulong)"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetGroup(ulong groupId)
                {
                    return new(Underlying.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="IterIterable.SetGroup{T}()"/>
                {{Generator.GetTypeName(Type.IterIterable, i)}} {{Generator.GetTypeName(Type.IIterable, i)}}.SetGroup<T>()
                {
                    return new(Underlying.SetGroup<T>()); 
                }
            }
            """;
    }
}
