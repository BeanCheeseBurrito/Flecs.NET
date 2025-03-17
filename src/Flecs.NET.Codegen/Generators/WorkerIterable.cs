using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class WorkerIterable : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
            AddSource($"WorkerIterable/T{i + 1}.g.cs", GenerateWorkerIterable(i));

        for (int i = -1; i < Generator.GenericCount; i++)
            AddSource($"WorkerIterable.IIterable/T{i + 1}.g.cs", IIterable.GenerateIterators(Type.WorkerIterable, i));
    }

    private static string GenerateWorkerIterable(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using System.Runtime.CompilerServices;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <inheritdoc cref="IterIterable"/>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : IEquatable<{{Generator.GetTypeName(Type.WorkerIterable, i)}}>, {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="IWorkerIterable.Underlying"/>
                public WorkerIterable Underlying;
                
                /// <inheritdoc cref="IWorkerIterable.Iterator"/>
                public ref ecs_iter_t Iterator => ref Underlying.Iterator;
                
                /// <inheritdoc cref="IWorkerIterable.ThreadIndex"/>
                public int ThreadIndex => Underlying.ThreadIndex;
                
                /// <inheritdoc cref="IWorkerIterable.ThreadCount"/>
                public int ThreadCount => Underlying.ThreadCount;
            
                /// <summary>
                ///     Creates a worker iterable.
                /// </summary>
                /// <param name="handle">The worker iterable.</param>
                public WorkerIterable(WorkerIterable handle)
                {
                    Underlying = handle;
                }
            
                /// <inheritdoc cref="WorkerIterable(ecs_iter_t, int, int)"/>
                public WorkerIterable(ecs_iter_t iter, int index, int count)
                {
                    Underlying = new WorkerIterable(iter, index, count);
                }
                
                /// <inheritdoc cref="WorkerIterable.Equals(WorkerIterable)"/>
                public bool Equals({{Generator.GetTypeName(Type.WorkerIterable, i)}} other)
                {
                    return Underlying.Equals(other.Underlying);
                }
                
                /// <inheritdoc cref="WorkerIterable.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.WorkerIterable, i)}} other && Equals(other);
                }
                
                /// <inheritdoc cref="WorkerIterable.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return Underlying.GetHashCode();
                }
                
                /// <inheritdoc cref="WorkerIterable.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.WorkerIterable, i)}} left, {{Generator.GetTypeName(Type.WorkerIterable, i)}} right)
                {
                    return left.Equals(right);
                }
                
                /// <inheritdoc cref="WorkerIterable.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.WorkerIterable, i)}} left, {{Generator.GetTypeName(Type.WorkerIterable, i)}} right)
                {
                    return !(left == right);
                }
            }
            
            // IWorkerIterable Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : IWorkerIterable
            {
                ref WorkerIterable IWorkerIterable.Underlying => ref Underlying;
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : IIterableBase
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
            public unsafe partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="WorkerIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(Underlying.Page(offset, limit));
                }
                
                /// <inheritdoc cref="WorkerIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(Underlying.Worker(index, count));
                }
                
                /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new(Underlying.Iter(world));
                }
                
                /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new(Underlying.Iter(it));
                }
                
                /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new(Underlying.Iter(entity));
                }
                
                /// <inheritdoc cref="WorkerIterable.Count()"/>
                public int Count()
                {
                    return Underlying.Count();
                }
                
                /// <inheritdoc cref="WorkerIterable.IsTrue()"/>
                public bool IsTrue()
                {
                    return Underlying.IsTrue();
                }
                
                /// <inheritdoc cref="WorkerIterable.First()"/>
                public Entity First()
                {
                    return Underlying.First();
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(int, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    return new(Underlying.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, ecs_table_t*)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, ecs_table_range_t)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, Table)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    return new(Underlying.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetGroup(ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    return new(Underlying.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetGroup{T}()"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    return new(Underlying.SetGroup<T>()); 
                }
            }
            """;
    }
}
