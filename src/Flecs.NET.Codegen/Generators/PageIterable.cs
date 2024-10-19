using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class PageIterable : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"PageIterable/T{i + 1}.g.cs", GeneratePageIterable(i));
            AddSource($"PageIterable.IIterable/T{i + 1}.g.cs",
                IIterable.GenerateExtensions(Type.PageIterable, i));
        }
    }

    private static string GeneratePageIterable(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using System.Runtime.CompilerServices;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <inheritdoc cref="IterIterable"/>
            /// {{Generator.XmlTypeParameters[i]}}
            public partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : IEquatable<{{Generator.GetTypeName(Type.PageIterable, i)}}>
            {
                private PageIterable _pageIterable;
            
                /// <summary>
                ///     Creates a page iterable.
                /// </summary>
                /// <param name="pageIterable">The page iterable.</param>
                public PageIterable(PageIterable pageIterable)
                {
                    _pageIterable = pageIterable;
                }
            
                /// <inheritdoc cref="PageIterable(ecs_iter_t, int, int)"/>
                public PageIterable(ecs_iter_t iter, int offset, int limit)
                {
                    _pageIterable = new PageIterable(iter, offset, limit);
                }
                
                /// <inheritdoc cref="PageIterable.Equals(PageIterable)"/>
                public bool Equals({{Generator.GetTypeName(Type.PageIterable, i)}} other)
                {
                    return _pageIterable.Equals(other._pageIterable);
                }
                
                /// <inheritdoc cref="PageIterable.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.PageIterable, i)}} other && Equals(other);
                }
                
                /// <inheritdoc cref="PageIterable.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _pageIterable.GetHashCode();
                }
                
                /// <inheritdoc cref="PageIterable.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.PageIterable, i)}} left, {{Generator.GetTypeName(Type.PageIterable, i)}} right)
                {
                    return left.Equals(right);
                }
                
                /// <inheritdoc cref="PageIterable.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.PageIterable, i)}} left, {{Generator.GetTypeName(Type.PageIterable, i)}} right)
                {
                    return !(left == right);
                }
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : IIterableBase
            {
                /// <inheritdoc cref="PageIterable.World"/>
                public ref ecs_world_t* World => ref _pageIterable.World;
            
                /// <inheritdoc cref="PageIterable.GetIter(ecs_world_t*)"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public ecs_iter_t GetIter(ecs_world_t* world = null)
                {
                    return _pageIterable.GetIter(world);
                }
                
                /// <inheritdoc cref="PageIterable.GetNext(ecs_iter_t*)"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool GetNext(ecs_iter_t* it)
                {
                    return _pageIterable.GetNext(it);
                }
            }

            // {{Generator.GetTypeName(Type.IIterable, i)}} Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="PageIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(_pageIterable.Page(offset, limit));
                }
                
                /// <inheritdoc cref="PageIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(_pageIterable.Worker(index, count));
                }
            
                /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new(_pageIterable.Iter(world));
                }
                
                /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new(_pageIterable.Iter(it));
                }
                
                /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new(_pageIterable.Iter(entity));
                }
                
                /// <inheritdoc cref="PageIterable.Count()"/>
                public int Count()
                {
                    return _pageIterable.Count();
                }
                
                /// <inheritdoc cref="PageIterable.IsTrue()"/>
                public bool IsTrue()
                {
                    return _pageIterable.IsTrue();
                }
                
                /// <inheritdoc cref="PageIterable.First()"/>
                public Entity First()
                {
                    return _pageIterable.First();
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(int, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    return new(_pageIterable.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    return new(_pageIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, ecs_table_t*)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    return new(_pageIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, ecs_table_range_t)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    return new(_pageIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, Table)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    return new(_pageIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetGroup(ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    return new(_pageIterable.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="PageIterable.SetGroup{T}()"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    return new(_pageIterable.SetGroup<T>()); 
                }
            }
            """;
    }
}
