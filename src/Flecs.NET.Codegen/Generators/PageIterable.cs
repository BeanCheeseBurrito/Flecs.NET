using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class PageIterable : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
            AddSource($"PageIterable/T{i + 1}.g.cs", GeneratePageIterable(i));

        for (int i = -1; i < Generator.GenericCount; i++)
            AddSource($"PageIterable.IIterable/T{i + 1}.g.cs", IIterable.GenerateIterators(Type.PageIterable, i));
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
            public unsafe partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : IEquatable<{{Generator.GetTypeName(Type.PageIterable, i)}}>
            {
                /// <inheritdoc cref="IPageIterable.Underlying"/>
                public PageIterable Underlying;
            
                /// <inheritdoc cref="IPageIterable.Iterator"/>
                public ref ecs_iter_t Iterator => ref Underlying.Iterator;
                
                /// <inheritdoc cref="IPageIterable.Offset"/>
                public int Offset => Underlying.Offset;
                
                /// <inheritdoc cref="IPageIterable.Limit"/>
                public int Limit => Underlying.Limit;
            
                /// <summary>
                ///     Creates a page iterable.
                /// </summary>
                /// <param name="handle">The page iterable.</param>
                public PageIterable(PageIterable handle)
                {
                    Underlying = handle;
                }
            
                /// <inheritdoc cref="PageIterable(ecs_iter_t, int, int)"/>
                public PageIterable(ecs_iter_t iter, int offset, int limit)
                {
                    Underlying = new PageIterable(iter, offset, limit);
                }
                
                /// <inheritdoc cref="PageIterable.Equals(PageIterable)"/>
                public bool Equals({{Generator.GetTypeName(Type.PageIterable, i)}} other)
                {
                    return Underlying.Equals(other.Underlying);
                }
                
                /// <inheritdoc cref="PageIterable.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.PageIterable, i)}} other && Equals(other);
                }
                
                /// <inheritdoc cref="PageIterable.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return Underlying.GetHashCode();
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
            
            // IPageIterable Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : IPageIterable
            {
                ref PageIterable IPageIterable.Underlying => ref Underlying;
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : IIterableBase
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
            public unsafe partial struct {{Generator.GetTypeName(Type.PageIterable, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="PageIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(Underlying.Page(offset, limit));
                }
                
                /// <inheritdoc cref="PageIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(Underlying.Worker(index, count));
                }
            
                /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new(Underlying.Iter(world));
                }
                
                /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new(Underlying.Iter(it));
                }
                
                /// <inheritdoc cref="PageIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new(Underlying.Iter(entity));
                }
                
                /// <inheritdoc cref="PageIterable.Count()"/>
                public int Count()
                {
                    return Underlying.Count();
                }
                
                /// <inheritdoc cref="PageIterable.IsTrue()"/>
                public bool IsTrue()
                {
                    return Underlying.IsTrue();
                }
                
                /// <inheritdoc cref="PageIterable.First()"/>
                public Entity First()
                {
                    return Underlying.First();
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(int, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    return new(Underlying.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, ecs_table_t*)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, ecs_table_range_t)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetVar(string, Table)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="PageIterable.SetGroup(ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    return new(Underlying.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="PageIterable.SetGroup{T}()"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    return new(Underlying.SetGroup<T>()); 
                }
            }
            """;
    }
}
